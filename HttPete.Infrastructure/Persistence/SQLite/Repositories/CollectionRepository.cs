using Abp.Domain.Repositories;
using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    internal class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {

        public CollectionRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Collection>> GetWorkspaceCollections(int workspaceId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => x.WorkspaceId == workspaceId).ToArrayAsync(cancellationToken);
        }
    }
}
