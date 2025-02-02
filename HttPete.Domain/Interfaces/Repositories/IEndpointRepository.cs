using Abp.Domain.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Domain.Interfaces.Repositories
{
    public interface IEndpointRepository
    {
        Task<IEnumerable<Endpoint>?> GetCollectionEndpoints(int id);
        Task<IEnumerable<Endpoint>> GetWorkspaceEndpoints(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default);
        Task<Endpoint> Add(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> Update(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> Delete(int id, CancellationToken cancellationToken = default);
    }
}
