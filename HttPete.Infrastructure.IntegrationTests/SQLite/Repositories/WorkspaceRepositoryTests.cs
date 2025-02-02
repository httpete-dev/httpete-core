using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HttPete.Infrastructure.IntegrationTests.Utils;
using HttPete.Infrastructure.Persistence.SQLite.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttPete.Infrastructure.IntegrationTests.SQLite.Repositories
{
    [TestClass]
    public class WorkspaceRepositoryTests
    {
        private HttPeteDbContextFixture _fixture;
        private WorkspaceRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new HttPeteDbContextFixture();
            _repository = new WorkspaceRepository(_fixture.Context);
        }

        [TestMethod]
        public async Task Add_ShouldPersistWorkspace()
        {
            // Arrange
            var workspace = new Workspace("User1", orgId: 1, collections: Array.Empty<Collection>()) { Id = 15 };

            // Act
            var result = await _repository.Add(workspace);

            // Assert
            result.Should().BeEquivalentTo(workspace);
            var persisted = await _fixture.Context.Workspaces.FindAsync(15);
            persisted.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetById_ShouldReturnWorkspace()
        {
            // Arrange
            var workspace = new Workspace("User2", orgId: 1, collections: Array.Empty<Collection>()) { Id = 25 };
            _fixture.Context.Workspaces.Add(workspace);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetById(25);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(workspace);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllWorkspaces()
        {
            // Arrange
            var workspaces = new List<Workspace>
            {
                new Workspace("UserA", orgId: 1, collections: Array.Empty<Collection>()) { Id = 105 },
                new Workspace("UserB", orgId: 1, collections: Array.Empty<Collection>()) { Id = 115 }
            };

            _fixture.Context.Workspaces.AddRange(workspaces);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAll();

            // Assert
            result.Should().HaveCount(3)
                .And.Contain(x => x.Id == 1)
                .And.Contain(x => x.Id == 105)
                .And.Contain(x => x.Id == 115);
        }

        [TestMethod]
        public async Task Update_ShouldModifyExistingWorkspace()
        {
            // Arrange
            var workspace = new Workspace("User3", 3) { Id = 20, Title = "Old Title" };
            _fixture.Context.Workspaces.Add(workspace);
            await _fixture.Context.SaveChangesAsync();

            // Act
            workspace.Title = "New Title";
            var result = await _repository.Update(workspace);

            // Assert
            result.Title.Should().Be("New Title");
            var persisted = await _fixture.Context.Workspaces.FindAsync(20);
            persisted.Title.Should().Be("New Title");
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveWorkspace()
        {
            // Arrange
            var workspace = new Workspace("User4", orgId: 4, collections: Array.Empty<Collection>()) { Id = 30 };
            _fixture.Context.Workspaces.Add(workspace);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var deleted = await _repository.Delete(30);

            // Assert
            deleted.Should().BeEquivalentTo(workspace);
            var exists = await _fixture.Context.Workspaces.FindAsync(30);
            exists.Should().BeNull();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }
    }

}
