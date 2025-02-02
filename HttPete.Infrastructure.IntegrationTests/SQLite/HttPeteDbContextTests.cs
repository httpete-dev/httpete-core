using FluentAssertions;
using HttPete.Infrastructure.IntegrationTests.Utils;
using HttPete.Model;
using Microsoft.EntityFrameworkCore;

namespace HttPete.Infrastructure.IntegrationTests.SQLite
{
    [TestClass]
    [DoNotParallelize]
    public class HttPeteDbContextTests
    {
        private HttPeteDbContextFixture _fixture;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new HttPeteDbContextFixture();
        }

        [TestMethod]
        public void Should_Have_Default_Organization()
        {
            var organization = _fixture.Context.Organizations.FirstOrDefault(o => o.Id == 1);
            organization.Should().NotBeNull();
            organization.Name.Should().Be("Default Organization");
        }

        [TestMethod]
        public void Should_Have_Default_User()
        {
            var user = _fixture.Context.Users.FirstOrDefault(u => u.Id == 1);
            user.Should().NotBeNull();
            user.Name.Should().Be("Default User");
            user.OrganizationId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Default_Workspace()
        {
            var workspace = _fixture.Context.Workspaces.FirstOrDefault(w => w.Id == 1);
            workspace.Should().NotBeNull();
            workspace.Title.Should().Be("Default Workspace");
            workspace.OrganizationId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Default_Collection()
        {
            var collection = _fixture.Context.Collections.FirstOrDefault(c => c.Id == 1);
            collection.Should().NotBeNull();
            collection.Name.Should().Be("Default Collection");
            collection.WorkspaceId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Default_BaseUrl()
        {
            var baseUrl = _fixture.Context.BaseUrls.FirstOrDefault(b => b.Id == 1);
            baseUrl.Should().NotBeNull();
            baseUrl.Value.Should().Be("httpete.dev");
            baseUrl.WorkspaceId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Default_Endpoint()
        {
            var endpoint = _fixture.Context.Endpoints.FirstOrDefault(e => e.Id == 1);
            endpoint.Should().NotBeNull();
            endpoint.Url.Should().Be(Defaults.DEFAULT_ENDPOINT_URL);
            endpoint.CollectionId.Should().Be(1);
            endpoint.BaseUrlId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Default_Document()
        {
            var document = _fixture.Context.Documents.FirstOrDefault(d => d.Id == 1);
            document.Should().NotBeNull();
            document.Title.Should().Be("README <3");
            document.AuthorId.Should().Be(1);
            document.EndpointId.Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Correct_Workspace_Organization_Relationship()
        {
            var organization = _fixture.Context.Organizations
                .Include(w => w.Workspaces)
                .FirstOrDefault(w => w.Id == 1);

            organization.Should().NotBeNull();
            organization.Workspaces.Should().NotBeNull();
            organization.Workspaces.Count().Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Correct_Collection_Workspace_Relationship()
        {
            var workspace = _fixture.Context.Workspaces
                .Include(c => c.Collections)
                .FirstOrDefault(c => c.Id == 1);

            workspace.Should().NotBeNull();
            workspace.Collections.Should().NotBeNull();
            workspace.Collections.Count().Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Correct_Endpoint_Collection_Relationship()
        {
            var collection = _fixture.Context.Collections
                .Include(e => e.Endpoints)
                .FirstOrDefault(e => e.Id == 1);

            collection.Should().NotBeNull();
            collection.Endpoints.Should().NotBeNull();
            collection.Endpoints.Count().Should().Be(1);
        }

        [TestMethod]
        public void Should_Have_Correct_Document_Endpoint_Relationship()
        {
            var endpoint = _fixture.Context.Endpoints
                .Include(d => d.Documentation)
                .FirstOrDefault(d => d.Id == 1);

            endpoint.Should().NotBeNull();
            endpoint.Documentation.Should().NotBeNull();
            endpoint.Documentation.Id.Should().Be(1);
        }
    }
}
