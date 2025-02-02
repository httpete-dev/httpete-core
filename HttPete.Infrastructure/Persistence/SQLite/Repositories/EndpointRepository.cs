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
    internal class EndpointRepository : BaseRepository<Endpoint>, IEndpointRepository
    {
        public EndpointRepository(HttPeteDbContext context) : base(context) { }

        public async Task<IEnumerable<Endpoint>?> GetCollectionEndpoints(int collectionId)
           => await _context.Endpoints.Where(x => x.CollectionId == collectionId).ToArrayAsync();

        public async Task<IEnumerable<Endpoint>> GetWorkspaceEndpoints(int? workspaceId, int? collectionId, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();

            query = (workspaceId, collectionId) switch
            {
                (int w, int c) => query.Where(x => x.CollectionId == c && x.WorkspaceId == w),
                (int w, null) => query.Where(x => x.WorkspaceId == w),
                (null, int c) => query.Where(x => x.CollectionId == c),
                (null, null) => query
            };

            return await query.ToArrayAsync(cancellationToken);
        }

    }
}
