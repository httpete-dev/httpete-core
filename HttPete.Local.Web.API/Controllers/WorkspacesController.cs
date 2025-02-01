using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using Workspace = HttPete.Model.Tenants.Workspace;
using HttPete.Services;

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

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var workspace = await _workspaceService.GetWorkspaceAsync(id, cancellationToken);
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

        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            try
            {
                var added = await _workspaceService.AddWorkspaceAsync(workspace, cancellationToken);
                return new HttPeteResponse(added, 200, "Successfully added workspace.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateWorkspace(Workspace workspace, CancellationToken cancellationToken = default)
        {
            try
            {
                var updated = await _workspaceService.UpdateWorkspaceAsync(workspace, cancellationToken);
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

        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteWorkspace(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await _workspaceService.DeleteWorkspaceAsync(id, cancellationToken);
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
