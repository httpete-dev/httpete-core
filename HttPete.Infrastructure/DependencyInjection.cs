using HttPete.Domain.Interfaces.Repositories;
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
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite($"Data Source={HttPeteSettings.CONFIG_PATH}/HttPete-local.db")
            );

            services.AddScoped<IWorkspaceRepository, Persistence.SQLite.Repositories.WorkspaceRepository>();
            services.AddScoped<IEndpointRepository, Persistence.SQLite.Repositories.EndpointRepository>();
            services.AddScoped<ICollectionRepository, Persistence.SQLite.Repositories.CollectionRepository>();
            services.AddScoped<IBaseUrlRepository, Persistence.SQLite.Repositories.BaseUrlRepository>();

            return services;
        }
    }
}
