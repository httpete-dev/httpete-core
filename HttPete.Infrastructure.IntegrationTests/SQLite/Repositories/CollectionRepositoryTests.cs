using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
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
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(x => x.WorkspaceId == workspaceId));
        }

        [TestMethod]
        public async Task Add_ShouldPersistEntity()
        {
            // Arrange
            var collection = new Collection("NewUser", 1, 1) { Id = 10 };

            // Act
            var result = await _repository.Add(collection);

            // Assert
            Assert.AreEqual(collection.Id, result.Id);
            Assert.AreEqual(collection.Name, result.Name);
            Assert.AreEqual(collection.WorkspaceId, result.WorkspaceId);

            var persisted = await _fixture.Context.Collections.FindAsync(10);
            Assert.IsNotNull(persisted);
            Assert.AreEqual(collection.Id, persisted.Id);
            Assert.AreEqual(collection.Name, persisted.Name);
            Assert.AreEqual(collection.WorkspaceId, persisted.WorkspaceId);
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
            Assert.AreEqual("Updated Collection", result.Name);

            var persisted = await _fixture.Context.Collections.FindAsync(20);
            Assert.IsNotNull(persisted);
            Assert.AreEqual("Updated Collection", persisted.Name);
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
            Assert.AreEqual(collection.Id, deleted.Id);
            Assert.AreEqual(collection.Name, deleted.Name);
            Assert.AreEqual(collection.WorkspaceId, deleted.WorkspaceId);

            var exists = await _fixture.Context.Collections.FindAsync(30);
            Assert.IsNull(exists);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }
    }
}