using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;

        public WorkspaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Workspace> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Workspaces.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Workspace> AddAsync(Workspace workspace, CancellationToken cancellationToken = default)
        {
            await _context.Workspaces.AddAsync(workspace, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return workspace;
        }

        public async Task<Workspace> UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default)
        {
            _context.Workspaces.Update(workspace);
            await _context.SaveChangesAsync(cancellationToken);

            return workspace;
        }

        public async Task<Workspace> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var workspace = await GetByIdAsync(id, cancellationToken);
            if (workspace != null)
            {
                _context.Workspaces.Remove(workspace);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return workspace;
        }
    }
}
