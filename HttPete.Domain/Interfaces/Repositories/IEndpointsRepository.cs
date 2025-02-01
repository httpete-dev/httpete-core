using Abp.Domain.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Domain.Interfaces.Repositories
{
    public interface IEndpointsRepository
    {
        Task<IEnumerable<Endpoint>?> GetCollectionEndpoints(int id);
        Task<IEnumerable<Endpoint>> GetEndpointsAsync(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default);
        Task<Endpoint> AddAsync(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> UpdateAsync(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
