using HttPete.Model.Tenants;
using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using HttPete.Services;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUrlsController : ControllerBase
    {
        private readonly IBaseUrlService _baseUrlService;

        public BaseUrlsController(IBaseUrlService baseUrlService)
        {
            _baseUrlService = baseUrlService;
        }

        /// <summary>
        /// Get workspace base urls.
        /// </summary>
        /// <param name="workspaceId">Workspace Id.</param>
        /// <returns>BaseUrls list.</returns>
        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> GetForWorkspace(int workspaceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var baseUrls = await _baseUrlService.GetBaseUrlsForWorkspace(workspaceId, cancellationToken);
                return new HttPeteResponse(baseUrls, 200, "Successfully retrieved base urls.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Add a base url to a workspace.
        /// </summary>
        /// <param name="baseUrl">BaseUrl</param>
        /// <returns>BaseUrl</returns>
        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                var added = await _baseUrlService.AddBaseUrl(baseUrl, cancellationToken);
                return new HttPeteResponse(added, 200, $"Successfully added base url: {added.Value}.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Update a base url.
        /// </summary>
        /// <param name="baseUrl">BaseUrl</param>
        /// <returns>BaseUrl</returns>
        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                var updated = await _baseUrlService.UpdateBaseUrl(baseUrl, cancellationToken);
                return new HttPeteResponse(updated, 200, "Successfully updated base url.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Delete a base url.
        /// </summary>
        /// <param name="id">BaseUrl Id</param>
        /// <returns>BaseUrl</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteBaseUrl(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await _baseUrlService.DeleteBaseUrl(id, cancellationToken);
                return new HttPeteResponse(deleted, 200, "Successfully deleted base url.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
