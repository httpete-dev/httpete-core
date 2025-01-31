using HttPete.Model;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using Constants = HttPete.Model.Constants;
using Endpoint = HttPete.Model.Tenants.Endpoint;

namespace HttPete.Web.API.LocalDB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Workspace> Workspaces{ get; set; }
        public DbSet<Collection> Collections{ get; set; }
        public DbSet<Endpoint> Endpoints{ get; set; }
        public DbSet<Document> Documents{ get; set; }
        public DbSet<BaseUrl> BaseUrls{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1,
                    Name = "Default Organization",
                    Created = DateTime.UtcNow,
                });

            modelBuilder.Entity<Workspace>().HasData(
                new Workspace
                {
                    Id = 1,
                    OrganizationId = 1,
                    Title = "Default Workspace",
                    Description = "This is the default workspace."
                }
            );

            modelBuilder.Entity<Collection>().HasData(
                new Collection
                {
                    Id = 1,
                    WorkspaceId = 1,
                    OrganizationId = 1,
                    Name = "Default Collection",
                    Description = "This is the default collection.",
                    Icon = "Globe",
                }
            );

            modelBuilder.Entity<BaseUrl>().HasData(new BaseUrl
            {
                Id = 1,
                WorkspaceId = 1,
                Protocol = "https",
                Value = "httpete.dev"
            });

            modelBuilder.Entity<Endpoint>().HasData(new Endpoint
            {
                Id = 1,
                Url = Defaults.DEFAULT_ENDPOINT_URL,
                Headers = Defaults.DEFAULT_ENDPOINT_HEADERS,
                Body = Defaults.DEFAULT_ENDPOINT_BODY,
                CollectionId = 1,
                OrganizationId = 1,
                WorkspaceId = 1,
                Method = "GET",
                BaseUrlId = 1
            });

            modelBuilder.Entity<Document>().HasData(
                new Document
                {
                    Id = 1,
                    Title = "README <3",
                    Text = Defaults.DEFAULT_DOCUMENT_TEXT,
                }
            );
        }
    }
}
