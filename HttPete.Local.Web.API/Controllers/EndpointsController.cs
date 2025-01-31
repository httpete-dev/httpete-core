using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HttPete.Services.Network;
using HttPete.Web.API.LocalDB;
using Endpoint = HttPete.Model.Tenants.Endpoint;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EndpointsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> Get(int? workspaceId = null, int? collectionId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                Endpoint[] endpoints = null;

                if (workspaceId == null && collectionId == null)
                    return new HttPeteResponse(null, 400, "WorkspaceId or CollectionId must be provided.");
                else if (workspaceId != null && collectionId != null)
                    endpoints = _context.Endpoints.Where(x => x.CollectionId == collectionId && x.WorkspaceId == workspaceId).ToArray();
                else if (workspaceId != null)
                    endpoints = _context.Endpoints.Where(x => x.WorkspaceId == workspaceId).ToArray();
                else if (collectionId != null)
                    endpoints = _context.Endpoints.Where(x => x.CollectionId == collectionId).ToArray();
                else // Theoretically will never happen
                    return new HttPeteResponse(null, 400, "WorkspaceId or CollectionId must be provided.");

                return new HttPeteResponse(endpoints, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Endpoints.AddAsync(endpoint, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(endpoint, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateEndpoint(Endpoint endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.Endpoints.Update(endpoint);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(endpoint, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteEndpoint(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var endpoint = await _context.Endpoints.FindAsync(id);
                _context.Endpoints.Remove(endpoint);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(endpoint, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
