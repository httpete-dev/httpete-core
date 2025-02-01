using Abp.Domain.Repositories;
using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    public class EndpointsRepository : IEndpointsRepository
    {
        private readonly AppDbContext _context;

        public EndpointsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endpoint>?> GetCollectionEndpoints(int collectionId)
           => await _context.Endpoints.Where(x => x.CollectionId == collectionId).ToArrayAsync();

        public async Task<IEnumerable<Endpoint>> GetEndpointsAsync(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default)
        {
            var query = _context.Endpoints.AsQueryable();

            if (workspaceId.HasValue && collectionId.HasValue)
                query = query.Where(x => x.CollectionId == collectionId && x.WorkspaceId == workspaceId);
            else if (workspaceId.HasValue)
                query = query.Where(x => x.WorkspaceId == workspaceId);
            else if (collectionId.HasValue)
                query = query.Where(x => x.CollectionId == collectionId);

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Endpoint> AddAsync(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            await _context.Endpoints.AddAsync(endpoint, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return endpoint;
        }

        public async Task<Endpoint> UpdateAsync(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            _context.Endpoints.Update(endpoint);
            await _context.SaveChangesAsync(cancellationToken);
            return endpoint;
        }

        public async Task<Endpoint> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var endpoint = await _context.Endpoints.FindAsync(new object[] { id }, cancellationToken);
            if (endpoint == null)
                throw new KeyNotFoundException($"Endpoint with id {id} not found");

            _context.Endpoints.Remove(endpoint);
            await _context.SaveChangesAsync(cancellationToken);
            return endpoint;
        }
    }
}
