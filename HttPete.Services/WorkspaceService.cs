using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Services
{
    public interface IWorkspaceService
    {
        Task<Workspace> GetWorkspaceAsync(int id, CancellationToken cancellationToken = default);
        Task<Workspace> AddWorkspaceAsync(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> UpdateWorkspaceAsync(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> DeleteWorkspaceAsync(int id, CancellationToken cancellationToken = default);
    }

    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceService(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        public async Task<Workspace> GetWorkspaceAsync(int id, CancellationToken cancellationToken = default)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(id, cancellationToken);
            if (workspace == null)
                throw new KeyNotFoundException($"Workspace with id {id} not found");

            return workspace;
        }

        public async Task<Workspace> AddWorkspaceAsync(Workspace workspace, CancellationToken cancellationToken = default)
        {
            return await _workspaceRepository.AddAsync(workspace, cancellationToken);
        }

        public async Task<Workspace> UpdateWorkspaceAsync(Workspace workspace, CancellationToken cancellationToken = default)
        {
            var exists = await _workspaceRepository.GetByIdAsync(workspace.Id, cancellationToken);
            if (exists == null)
                throw new KeyNotFoundException($"Workspace with id {workspace.Id} not found");

            return await _workspaceRepository.UpdateAsync(workspace, cancellationToken);
        }

        public async Task<Workspace> DeleteWorkspaceAsync(int id, CancellationToken cancellationToken = default)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(id, cancellationToken);
            if (workspace == null)
                throw new KeyNotFoundException($"Workspace with id {id} not found");

            return await _workspaceRepository.DeleteAsync(id, cancellationToken);
        }
    }

}
