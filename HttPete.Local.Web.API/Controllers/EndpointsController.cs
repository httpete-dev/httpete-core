using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using Endpoint = HttPete.Model.Tenants.Endpoint;
using HttPete.Services;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointsController : ControllerBase
    {
        private readonly IEndpointService _endpointService;

        public EndpointsController(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> Get(int? workspaceId = null, int? collectionId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (workspaceId == null && collectionId == null)
                    return new HttPeteResponse(null, 400, "WorkspaceId or CollectionId must be provided.");

                var endpoints = await _endpointService.GetEndpoints(workspaceId, collectionId, cancellationToken);
                return new HttPeteResponse(endpoints, 200, "Successfully retrieved endpoints.");
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
                var added = await _endpointService.AddEndpoint(endpoint, cancellationToken);
                return new HttPeteResponse(added, 200, "Successfully added endpoint.");
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
                var updated = await _endpointService.UpdateEndpoint(endpoint, cancellationToken);
                return new HttPeteResponse(updated, 200, "Successfully updated endpoint.");
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
                var deleted = await _endpointService.DeleteEndpoint(id, cancellationToken);
                return new HttPeteResponse(deleted, 200, "Successfully deleted endpoint.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
