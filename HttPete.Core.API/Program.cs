using HttPete.Infrastructure;
using HttPete.Infrastructure.Persistence.SQLite;
using HttPete.Application;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace HttPete.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            RegisterHttPeteInfrastructure(builder);
            RegisterHttPeteServices(builder);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HttPete Core API", Version = "v1" });

                // Include XML comments for Swagger documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:56969")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply migrations and seed the database
            //NC_TODO: centralized solution independent of the database type
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HttPeteDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }

        private static void RegisterHttPeteServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttPeteServices();
        }

        private static void RegisterHttPeteInfrastructure(WebApplicationBuilder builder)
        {
            var database = builder.Configuration["DatabaseType"];
            if (database == "SQLite")
            {
                builder.Services.AddSQLiteRepositories();
            }
            else if (database == "PostgreSQL")
            {
                throw new Exception($"PostgreSQL is coming soon my friend.");
            }
            else
            {
                throw new Exception($"Database {database} not supported yet.");
            }
        }
    }
}
