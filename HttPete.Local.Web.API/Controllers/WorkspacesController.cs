using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using Workspace = HttPete.Model.Tenants.Workspace;
using HttPete.Application.Services;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspacesController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        /// <summary>
        /// Get a workspace by id.
        /// </summary>
        /// <param name="id">Workspace Id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Workspace</returns>
        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var workspace = await _workspaceService.GetWorkspace(id, cancellationToken);
                return new HttPeteResponse(workspace, 200, "Successfully retrieved workspace.");
            }
            catch (KeyNotFoundException e)
            {
                return new HttPeteResponse(null, 404, e.Message);
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Add a workspace.
        /// </summary>
        /// <param name="workspace">Workspace</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Workspace</returns>
        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            try
            {
                var added = await _workspaceService.AddWorkspace(workspace, cancellationToken);
                return new HttPeteResponse(added, 200, "Successfully added workspace.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Update a workspace.
        /// </summary>
        /// <param name="workspace">Workspace</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Workspace</returns>
        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            try
            {
                var updated = await _workspaceService.UpdateWorkspace(workspace, cancellationToken);
                return new HttPeteResponse(updated, 200, "Successfully updated workspace.");
            }
            catch (KeyNotFoundException e)
            {
                return new HttPeteResponse(null, 404, e.Message);
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Delete a workspace.
        /// </summary>
        /// <param name="id">Workspace Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Workspace</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteWorkspace(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await _workspaceService.DeleteWorkspace(id, cancellationToken);
                return new HttPeteResponse(deleted, 200, "Successfully deleted workspace.");
            }
            catch (KeyNotFoundException e)
            {
                return new HttPeteResponse(null, 404, e.Message);
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
