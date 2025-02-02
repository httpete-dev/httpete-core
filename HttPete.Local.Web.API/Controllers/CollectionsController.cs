using HttPete.Model.Tenants;
using Microsoft.AspNetCore.Mvc;
using HttPete.Services.Network;
using HttPete.Application.Services;

namespace HttPete.Web.API.Controllers
{
    //NC_TODO: remove verbs from the URL
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _service;

        public CollectionsController(ICollectionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a collection by id.
        /// </summary>
        /// <param name="id">Collection Id.</param>
        /// <returns>Collection</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<HttPeteResponse> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = await _service.GetCollection(id, cancellationToken);
                if (collection == null)
                {
                    return new HttPeteResponse(null, 404, "Collection not found.");
                }
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        /// <summary>
        /// Add a collection.
        /// </summary>
        /// <param name="collection">Collection</param>
        /// <returns>Collection</returns>
        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddCollection(Collection collection, CancellationToken cancellationToken = default)
        {
            try
            {
                await _service.Create(collection, cancellationToken);
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        /// <summary>
        /// Update a collection.
        /// </summary>
        /// <param name="collection">Collection</param>
        /// <returns>Collection</returns>
        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateCollection(Collection collection, CancellationToken cancellationToken = default)
        {
            try
            {
                await _service.Update(collection, cancellationToken);
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="id">Collection Id</param>
        /// <returns>Collection</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteCollection(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = await _service.Delete(id, cancellationToken);
                if(collection != null)
                {
                    return new HttPeteResponse(null, 404, "Collection not found.");
                }

                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }
    }
}
