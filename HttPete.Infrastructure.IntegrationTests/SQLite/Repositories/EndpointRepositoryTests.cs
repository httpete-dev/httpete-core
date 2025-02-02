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
    public class EndpointRepositoryTests
    {
        private HttPeteDbContextFixture _fixture;
        private EndpointRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new HttPeteDbContextFixture();
            _repository = new EndpointRepository(_fixture.Context);
        }

        [TestMethod]
        public async Task GetCollectionEndpoints_ShouldReturnEndpointsOfCollection()
        {
            // Arrange
            var collectionId = 13;
            var endpoints = new List<Endpoint>
            {
                new Endpoint(1, 1, collectionId) { Id = 13 },
                new Endpoint(1, 1, collectionId) { Id = 24 },
                new Endpoint(1, 2, 2) { Id = 35 } // Different collection
            };

            _fixture.Context.Endpoints.AddRange(endpoints);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCollectionEndpoints(collectionId);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.CollectionId == collectionId);
        }

        [TestMethod]
        public async Task GetWorkspaceEndpoints_WithWorkspaceIdOnly_ShouldReturnAllWorkspaceEndpoints()
        {
            // Arrange
            var workspaceId = 156;
            var endpoints = new List<Endpoint>
            {
                new Endpoint(1, workspaceId, 1) { Id = 156 },
                new Endpoint(1, workspaceId, 2) { Id = 256 },
                new Endpoint(1, 2, 3) { Id = 356 } // Different workspace
            };

            _fixture.Context.Endpoints.AddRange(endpoints);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkspaceEndpoints(workspaceId, null, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.WorkspaceId == workspaceId);
        }

        [TestMethod]
        public async Task GetWorkspaceEndpoints_WithCollectionIdOnly_ShouldReturnAllCollectionEndpoints()
        {
            // Arrange
            var collectionId = 223;
            var endpoints = new List<Endpoint>
            {
                new Endpoint(1, 1, collectionId) { Id = 123 },
                new Endpoint(1, 2, collectionId) { Id = 223 },
                new Endpoint(1, 3, 3) { Id = 323 } // Different collection
            };

            _fixture.Context.Endpoints.AddRange(endpoints);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkspaceEndpoints(null, collectionId, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.CollectionId == collectionId);
        }

        [TestMethod]
        public async Task GetWorkspaceEndpoints_WithBothIds_ShouldReturnMatchingEndpoints()
        {
            // Arrange
            var workspaceId = 122;
            var collectionId = 222;
            var endpoints = new List<Endpoint>
            {
                new Endpoint(1, workspaceId, collectionId) { Id = 122 },
                new Endpoint(1, workspaceId, collectionId) { Id = 222 },
                new Endpoint(1, 2, collectionId) { Id = 322 }, // Different workspace
                new Endpoint(1, workspaceId, 3) { Id = 422 } // Different collection
            };

            _fixture.Context.Endpoints.AddRange(endpoints);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkspaceEndpoints(workspaceId, collectionId, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.WorkspaceId == workspaceId && x.CollectionId == collectionId);
        }

        [TestMethod]
        public async Task Add_ShouldPersistEntity()
        {
            // Arrange
            var endpoint = new Endpoint(1, 1, 1) { Id = 10 };

            // Act
            var result = await _repository.Add(endpoint);

            // Assert
            result.Should().BeEquivalentTo(endpoint);
            var persisted = await _fixture.Context.Endpoints.FindAsync(10);
            persisted.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Update_ShouldModifyExistingEntity()
        {
            // Arrange
            var endpoint = new Endpoint(1, 1, 1) { Id = 20, Url = "/old-url" };
            _fixture.Context.Endpoints.Add(endpoint);
            await _fixture.Context.SaveChangesAsync();

            // Act
            endpoint.Url = "/new-url";
            var result = await _repository.Update(endpoint);

            // Assert
            result.Url.Should().Be("/new-url");
            var persisted = await _fixture.Context.Endpoints.FindAsync(20);
            persisted.Url.Should().Be("/new-url");
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var endpoint = new Endpoint(1, 1, 1) { Id = 30 };
            _fixture.Context.Endpoints.Add(endpoint);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var deleted = await _repository.Delete(30);

            // Assert
            deleted.Should().BeEquivalentTo(endpoint);
            var exists = await _fixture.Context.Endpoints.FindAsync(30);
            exists.Should().BeNull();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }
    }

}
