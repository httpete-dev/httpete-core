using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services
{
    public interface IEndpointService
    {
        Task<IEnumerable<Endpoint>> GetEndpointsAsync(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default);
        Task<Endpoint> AddEndpointAsync(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> UpdateEndpointAsync(Endpoint endpoint, CancellationToken cancellationToken = default);
        Task<Endpoint> DeleteEndpointAsync(int id, CancellationToken cancellationToken = default);
    }

    public class EndpointService : IEndpointService
    {
        private readonly IEndpointsRepository _endpointRepository;

        public EndpointService(IEndpointsRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<IEnumerable<Endpoint>> GetEndpointsAsync(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.GetEndpointsAsync(workspaceId, collectionId, cancellationToken);
        }

        public async Task<Endpoint> AddEndpointAsync(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.AddAsync(endpoint, cancellationToken);
        }

        public async Task<Endpoint> UpdateEndpointAsync(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.UpdateAsync(endpoint, cancellationToken);
        }

        public async Task<Endpoint> DeleteEndpointAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _endpointRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
