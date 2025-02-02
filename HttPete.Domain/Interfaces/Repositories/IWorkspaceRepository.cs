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
        Task<Workspace?> GetById(int id, CancellationToken cancellationToken = default);
        Task<Workspace> Add(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> Update(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> Delete(int id, CancellationToken cancellationToken = default);
    }
}
