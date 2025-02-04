using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Infrastructure.Persistence.SQLite;
using HttPete.Services.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HttPete.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSQLiteRepositories(this IServiceCollection services)
        {
            var configDirectory = HttPeteSettings.CONFIG_PATH;
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }
            var dbPath = Path.Combine(configDirectory, "HttPete-local.db");
            services.AddDbContext<HttPeteDbContext>(options =>
                options
                    .UseSqlite($"Data Source={dbPath}")
                    .EnableSensitiveDataLogging()
            );

            services.AddScoped<IWorkspaceRepository, Persistence.SQLite.Repositories.WorkspaceRepository>();
            services.AddScoped<IEndpointRepository, Persistence.SQLite.Repositories.EndpointRepository>();
            services.AddScoped<ICollectionRepository, Persistence.SQLite.Repositories.CollectionRepository>();
            services.AddScoped<IBaseUrlRepository, Persistence.SQLite.Repositories.BaseUrlRepository>();

            return services;
        }
    }
}
