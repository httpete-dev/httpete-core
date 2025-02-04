using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Application.Services
{
    public interface IEndpointService
    {
        Task<IEnumerable<Endpoint>> GetEndpoints(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default);
        Task<Endpoint> AddEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> UpdateEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> DeleteEndpoint(int id, CancellationToken cancellationToken = default);
    }

    public class EndpointService : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;

        public EndpointService(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<IEnumerable<Endpoint>> GetEndpoints(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.GetWorkspaceEndpoints(workspaceId, collectionId, cancellationToken);
        }

        public async Task<Endpoint> AddEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.Add(endpoint, cancellationToken);
        }

        public async Task<Endpoint> UpdateEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.Update(endpoint, cancellationToken);
        }

        public async Task<Endpoint> DeleteEndpoint(int id, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.Delete(id, cancellationToken);
        }
    }
}
