using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using HttPete.Web.API.LocalDB;
using Workspace = HttPete.Model.Tenants.Workspace;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkspacesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var workspace = await _context.Workspaces.FindAsync(id);

                return new HttPeteResponse(workspace, 200, "");
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
                await _context.Workspaces.AddAsync(workspace, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(workspace, 200, "");
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
                _context.Workspaces.Update(workspace);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(workspace, 200, "");
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
                var workspace = await _context.Workspaces.FindAsync(id);
                _context.Workspaces.Remove(workspace);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(workspace, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
