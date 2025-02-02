using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HttPete.Domain.Interfaces.Repositories;
using HttPete.Infrastructure.IntegrationTests.Utils;
using HttPete.Infrastructure.Persistence.SQLite.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttPete.Infrastructure.IntegrationTests.SQLite.Repositories
{


    [TestClass]
    public class CollectionRepositoryTests
    {
        private HttPeteDbContextFixture _fixture;
        private CollectionRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new HttPeteDbContextFixture();
            _repository = new CollectionRepository(_fixture.Context);
        }

        [TestMethod]
        public async Task GetWorkspaceCollections_ShouldReturnMatchingCollections()
        {
            // Arrange
            var workspaceId = 12;
            var collections = new List<Collection>
            {
                new Collection("User1", 1, workspaceId) { Id = 12 },
                new Collection("User2", 1, workspaceId) { Id = 22 },
                new Collection("User3", 2, 2) { Id = 32 } // Different workspace
            };

            _fixture.Context.Collections.AddRange(collections);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkspaceCollections(workspaceId, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.WorkspaceId == workspaceId);
        }

        [TestMethod]
        public async Task Add_ShouldPersistEntity()
        {
            // Arrange
            var collection = new Collection("NewUser", 1, 1) { Id = 10 };

            // Act
            var result = await _repository.Add(collection);

            // Assert
            result.Should().BeEquivalentTo(collection);
            var persisted = await _fixture.Context.Collections.FindAsync(10);
            persisted.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Update_ShouldModifyExistingEntity()
        {
            // Arrange
            var collection = new Collection("OldUser", 1, 1) { Id = 20, Name = "Old Collection" };
            _fixture.Context.Collections.Add(collection);
            await _fixture.Context.SaveChangesAsync();

            // Act
            collection.Name = "Updated Collection";
            var result = await _repository.Update(collection);

            // Assert
            result.Name.Should().Be("Updated Collection");
            var persisted = await _fixture.Context.Collections.FindAsync(20);
            persisted.Name.Should().Be("Updated Collection");
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var collection = new Collection("UserToDelete", 1, 1) { Id = 30 };
            _fixture.Context.Collections.Add(collection);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var deleted = await _repository.Delete(30);

            // Assert
            deleted.Should().BeEquivalentTo(collection);
            var exists = await _fixture.Context.Collections.FindAsync(30);
            exists.Should().BeNull();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }
    }

}
