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
    public class BaseUrlRepository : IBaseUrlRepository
    {
        private readonly AppDbContext _context;

        public BaseUrlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseUrl>> GetByWorkspaceIdAsync(int workspaceId, CancellationToken cancellationToken = default)
        {
            return await _context.BaseUrls
                .Where(x => x.WorkspaceId == workspaceId)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<BaseUrl> AddAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            await _context.BaseUrls.AddAsync(baseUrl, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return baseUrl;
        }

        public async Task<BaseUrl> UpdateAsync(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            _context.BaseUrls.Update(baseUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return baseUrl;
        }

        public async Task<BaseUrl> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var baseUrl = await _context.BaseUrls.FindAsync(new object[] { id }, cancellationToken);
            if (baseUrl == null)
                throw new KeyNotFoundException($"BaseUrl with id {id} not found");

            _context.BaseUrls.Remove(baseUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return baseUrl;
        }
    }
}
