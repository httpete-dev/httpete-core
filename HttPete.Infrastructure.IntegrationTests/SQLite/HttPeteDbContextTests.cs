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
            Assert.IsNotNull(organization);
            Assert.AreEqual("Default Organization", organization.Name);
        }

        [TestMethod]
        public void Should_Have_Default_User()
        {
            var user = _fixture.Context.Users.FirstOrDefault(u => u.Id == 1);
            Assert.IsNotNull(user);
            Assert.AreEqual("Default User", user.Name);
            Assert.AreEqual(1, user.OrganizationId);
        }

        [TestMethod]
        public void Should_Have_Default_Workspace()
        {
            var workspace = _fixture.Context.Workspaces.FirstOrDefault(w => w.Id == 1);
            Assert.IsNotNull(workspace);
            Assert.AreEqual("Default Workspace", workspace.Title);
            Assert.AreEqual(1, workspace.OrganizationId);
        }

        [TestMethod]
        public void Should_Have_Default_Collection()
        {
            var collection = _fixture.Context.Collections.FirstOrDefault(c => c.Id == 1);
            Assert.IsNotNull(collection);
            Assert.AreEqual("Default Collection", collection.Name);
            Assert.AreEqual(1, collection.WorkspaceId);
        }

        [TestMethod]
        public void Should_Have_Default_BaseUrl()
        {
            var baseUrl = _fixture.Context.BaseUrls.FirstOrDefault(b => b.Id == 1);
            Assert.IsNotNull(baseUrl);
            Assert.AreEqual("httpete.dev", baseUrl.Value);
            Assert.AreEqual(1, baseUrl.WorkspaceId);
        }

        [TestMethod]
        public void Should_Have_Default_Endpoint()
        {
            var endpoint = _fixture.Context.Endpoints.FirstOrDefault(e => e.Id == 1);
            Assert.IsNotNull(endpoint);
            Assert.AreEqual(Defaults.DEFAULT_ENDPOINT_URL, endpoint.Url);
            Assert.AreEqual(1, endpoint.CollectionId);
            Assert.AreEqual(1, endpoint.BaseUrlId);
        }

        [TestMethod]
        public void Should_Have_Default_Document()
        {
            var document = _fixture.Context.Documents.FirstOrDefault(d => d.Id == 1);
            Assert.IsNotNull(document);
            Assert.AreEqual("README <3", document.Title);
            Assert.AreEqual(1, document.AuthorId);
            Assert.AreEqual(1, document.EndpointId);
        }

        [TestMethod]
        public void Should_Have_Correct_Workspace_Organization_Relationship()
        {
            var organization = _fixture.Context.Organizations
                .Include(w => w.Workspaces)
                .FirstOrDefault(w => w.Id == 1);

            Assert.IsNotNull(organization);
            Assert.IsNotNull(organization.Workspaces);
            Assert.AreEqual(1, organization.Workspaces.Count());
        }

        [TestMethod]
        public void Should_Have_Correct_Collection_Workspace_Relationship()
        {
            var workspace = _fixture.Context.Workspaces
                .Include(c => c.Collections)
                .FirstOrDefault(c => c.Id == 1);

            Assert.IsNotNull(workspace);
            Assert.IsNotNull(workspace.Collections);
            Assert.AreEqual(1, workspace.Collections.Count());
        }

        [TestMethod]
        public void Should_Have_Correct_Endpoint_Collection_Relationship()
        {
            var collection = _fixture.Context.Collections
                .Include(e => e.Endpoints)
                .FirstOrDefault(e => e.Id == 1);

            Assert.IsNotNull(collection);
            Assert.IsNotNull(collection.Endpoints);
            Assert.AreEqual(1, collection.Endpoints.Count());
        }

        [TestMethod]
        public void Should_Have_Correct_Document_Endpoint_Relationship()
        {
            var endpoint = _fixture.Context.Endpoints
                .Include(d => d.Documentation)
                .FirstOrDefault(d => d.Id == 1);

            Assert.IsNotNull(endpoint);
            Assert.IsNotNull(endpoint.Documentation);
            Assert.AreEqual(1, endpoint.Documentation.Id);
        }
    }
}
