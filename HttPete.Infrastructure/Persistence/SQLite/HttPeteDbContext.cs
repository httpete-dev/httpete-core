using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::HttPete.Model.Tenants;
using global::HttPete.Model;
using HttPete.Model;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Constants = HttPete.Model.Constants;
using Endpoint = HttPete.Model.Tenants.Endpoint;

namespace HttPete.Infrastructure.Persistence.SQLite
{

    public class HttPeteDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<BaseUrl> BaseUrls { get; set; }

        public HttPeteDbContext(DbContextOptions<HttPeteDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Organization relationships
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Workspaces)
                .WithOne()
                .HasForeignKey(w => w.OrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Users)
                .WithOne()
                .HasForeignKey(u => u.OrganizationId);

            // Workspace relationships
            modelBuilder.Entity<Workspace>()
                .HasMany(w => w.Collections)
                .WithOne()
                .HasForeignKey(c => c.WorkspaceId);

            // Collection relationships
            modelBuilder.Entity<Collection>()
                .HasMany(c => c.Endpoints)
                .WithOne()
                .HasForeignKey(e => e.CollectionId);

            // Endpoint relationships
            modelBuilder.Entity<Endpoint>()
                .HasOne(e => e.BaseUrl)
                .WithMany()
                .HasForeignKey(e => e.BaseUrlId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Endpoint>()
                .HasOne(e => e.Documentation)
                .WithOne()
                .HasForeignKey<Document>("EndpointId")
                .IsRequired(false);

            // Document relationships
            modelBuilder.Entity<Document>()
                .HasMany(d => d.Children)
                .WithOne()
                .HasForeignKey("ParentId") 
                .IsRequired(false);

            // User-Document relationships
            modelBuilder.Entity<Document>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.LastEditById)
                .OnDelete(DeleteBehavior.Restrict);

            //Seed

            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1,
                    Name = "Default Organization",
                    Created = DateTime.UtcNow,
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Default User",
                    OrganizationId = 1,
                    Email = "defaultuser@email.com",
                    Image = "https://www.gravatar.com/avatar/",
                    LastLogin = DateTime.UtcNow,
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
                    AuthorId = 1,
                    EndpointId = 1,
                    OrganizationId = 1,
                    WorkspaceId = 1,
                    LastEditById = 1,
                    Created = DateTime.UtcNow,
                    Title = "README <3",
                    Text = Defaults.DEFAULT_DOCUMENT_TEXT,
                }
            );
        }
    }

}
