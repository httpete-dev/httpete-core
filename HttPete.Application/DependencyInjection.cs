using HttPete.Application.Services;
using HttPete.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttPeteServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseUrlService, BaseUrlService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IEndpointService, EndpointService>();
            services.AddScoped<IWorkspaceService, WorkspaceService>();

            return services;
        }
    }
}
