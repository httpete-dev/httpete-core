using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    internal class BaseUrlRepository : BaseRepository<BaseUrl>, IBaseUrlRepository
    {
        public BaseUrlRepository(HttPeteDbContext context) : base(context) { }

        public async Task<IEnumerable<BaseUrl>> GetByWorkspaceIdAsync(int workspaceId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(x => x.WorkspaceId == workspaceId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
