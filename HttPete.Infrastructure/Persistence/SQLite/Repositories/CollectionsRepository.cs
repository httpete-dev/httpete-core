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
    internal class CollectionsRepository : ICollectionsRepository
    {
        private readonly AppDbContext _context;

        public CollectionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Collection> Add(Collection collection, CancellationToken cancellationToken)
        {
            await _context.Collections.AddAsync(collection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return collection;
        }

        public async Task<Collection?> Delete(int collectionId, CancellationToken cancellationToken)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection == null)
            {
                return null;
            }
            _context.Collections.Remove(collection);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0 ? collection : null;
        }

        public async Task<Collection?> GetCollection(int collectionId, CancellationToken cancellationToken)
        {
            return await _context.Collections
                .Where(x => x.Id == collectionId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Collection>> GetWorkspaceCollections(int workspaceId, CancellationToken cancellationToken)
        {
            return await _context.Collections.Where(x => x.WorkspaceId == workspaceId).ToArrayAsync(cancellationToken);
        }

        public async Task<Collection?> Update(Collection collection, CancellationToken cancellationToken)
        {
            _context.Collections.Update(collection);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return collection;
        }
    }
}
