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
    [DoNotParallelize]
    public class BaseUrlRepositoryTests
    {
        private HttPeteDbContextFixture _fixture;
        private BaseUrlRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new HttPeteDbContextFixture();
            _repository = new BaseUrlRepository(_fixture.Context);
        }

        [TestMethod]
        public async Task GetByWorkspaceIdAsync_ShouldReturnMatchingBaseUrls()
        {
            // Arrange
            var workspaceId = 123;
            var baseUrls = new List<BaseUrl>
            {
                new BaseUrl { Id = 11, WorkspaceId = workspaceId, Value = "https://example.com/1" },
                new BaseUrl { Id = 21, WorkspaceId = workspaceId, Value = "https://example.com/2" },
                new BaseUrl { Id = 31, WorkspaceId = 234, Value = "https://example.com/3" } // Different workspace
            };

            _fixture.Context.BaseUrls.AddRange(baseUrls);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByWorkspaceIdAsync(workspaceId);

            // Assert
            result.Should().HaveCount(2)
                .And.OnlyContain(x => x.WorkspaceId == workspaceId);
        }

        [TestMethod]
        public async Task Add_ShouldPersistEntity()
        {
            // Arrange
            var baseUrl = new BaseUrl { Id = 10, WorkspaceId = 1, Value = "https://newurl.com" };

            // Act
            var result = await _repository.Add(baseUrl);

            // Assert
            result.Should().BeEquivalentTo(baseUrl);
            var persisted = await _fixture.Context.BaseUrls.FindAsync(10);
            persisted.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Update_ShouldModifyExistingEntity()
        {
            // Arrange
            var baseUrl = new BaseUrl { Id = 20, WorkspaceId = 1, Value = "https://oldurl.com" };
            _fixture.Context.BaseUrls.Add(baseUrl);
            await _fixture.Context.SaveChangesAsync();

            // Act
            baseUrl.Value = "https://updatedurl.com";
            var result = await _repository.Update(baseUrl);

            // Assert
            result.Value.Should().Be("https://updatedurl.com");
            var persisted = await _fixture.Context.BaseUrls.FindAsync(20);
            persisted.Value.Should().Be("https://updatedurl.com");
        }

        [TestMethod]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var baseUrl = new BaseUrl { Id = 30, WorkspaceId = 1, Value = "https://delete.com" };
            _fixture.Context.BaseUrls.Add(baseUrl);
            await _fixture.Context.SaveChangesAsync();

            // Act
            var deleted = await _repository.Delete(30);

            // Assert
            deleted.Should().BeEquivalentTo(baseUrl);
            var exists = await _fixture.Context.BaseUrls.FindAsync(30);
            exists.Should().BeNull();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }
    }

}
