using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Domain.Interfaces.Repositories
{
    public interface IWorkspaceRepository
    {
        Task<Workspace> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Workspace> AddAsync(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
