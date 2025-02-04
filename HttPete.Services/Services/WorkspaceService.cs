using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Application.Services
{
    public interface IWorkspaceService
    {
        Task<Workspace> GetWorkspace(int id, CancellationToken cancellationToken = default);
        Task<Workspace> AddWorkspace(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> UpdateWorkspace(Workspace workspace, CancellationToken cancellationToken = default);
        Task<Workspace> DeleteWorkspace(int id, CancellationToken cancellationToken = default);
    }

    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceService(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        public async Task<Workspace> GetWorkspace(int id, CancellationToken cancellationToken = default)
        {
            var workspace = await _workspaceRepository.GetById(id, cancellationToken);
            if (workspace == null)
                throw new KeyNotFoundException($"Workspace with id {id} not found");

            return workspace;
        }

        public async Task<Workspace> AddWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            return await _workspaceRepository.Add(workspace, cancellationToken);
        }

        public async Task<Workspace> UpdateWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            var exists = await _workspaceRepository.GetById(workspace.Id, cancellationToken);
            if (exists == null)
                throw new KeyNotFoundException($"Workspace with id {workspace.Id} not found");

            return await _workspaceRepository.Update(workspace, cancellationToken);
        }

        public async Task<Workspace> DeleteWorkspace(int id, CancellationToken cancellationToken = default)
        {
            var workspace = await _workspaceRepository.GetById(id, cancellationToken);
            if (workspace == null)
                throw new KeyNotFoundException($"Workspace with id {id} not found");

            return await _workspaceRepository.Delete(id, cancellationToken);
        }
    }

}
