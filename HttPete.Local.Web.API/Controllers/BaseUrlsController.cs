using HttPete.Model.Tenants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HttPete.Services.Network;
using HttPete.Web.API.LocalDB;

namespace HttPete.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUrlsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BaseUrlsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public async Task<HttPeteResponse> GetForWorkspace(int workspaceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var baseUrls = await _context.BaseUrls.Where(x => x.WorkspaceId == workspaceId).ToArrayAsync(cancellationToken);
                return new HttPeteResponse(baseUrls, 200, "Successfully retrieved base urls.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttPeteResponse> AddBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.BaseUrls.AddAsync(baseUrl, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(baseUrl, 200, $"Successfully added base url: {baseUrl.Value}.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpPatch]
        [Route("update")]
        public async Task<HttPeteResponse> UpdateBaseUrl(BaseUrl baseUrl, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.BaseUrls.Update(baseUrl);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(baseUrl, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttPeteResponse> DeleteBaseUrl(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var baseUrl = await _context.BaseUrls.FindAsync(id);
                _context.BaseUrls.Remove(baseUrl);
                await _context.SaveChangesAsync(cancellationToken);
                return new HttPeteResponse(baseUrl, 200, "");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
