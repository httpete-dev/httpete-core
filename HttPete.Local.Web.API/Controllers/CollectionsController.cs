using HttPete.Model.Tenants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HttPete.Services.Network;
using HttPete.Web.API.LocalDB;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> GetForWorkspace(int workspaceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var collections = await _context.Collections.Where(x => x.WorkspaceId == workspaceId).ToArrayAsync(cancellationToken);
                return new HttPeteResponse(collections, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddCollection(Collection collection, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Collections.AddAsync(collection, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateCollection(Collection collection, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.Collections.Update(collection);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteCollection(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = await _context.Collections.FindAsync(id);
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(collection, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(null, 500, e.Message);
            }
        }
    }
}
