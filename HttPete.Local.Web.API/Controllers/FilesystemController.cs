using System.Text;
using System.Text.Json;
using HttPete.Model.Tenants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HttPete.Services.IO;
using HttPete.Services.Network;
using HttPete.Web.API.LocalDB;
using Endpoint = HttPete.Model.Tenants.Endpoint;

namespace HttPete.Web.API.Controllers
{

    using Files = System.IO.File;


    [ApiController]
    [Route("api/[controller]")]
    public class FilesystemController : ControllerBase
    {
        private readonly ILogger<FilesystemController> _logger;
        private readonly IFileService _fileService;
        private readonly AppDbContext _context;

        public FilesystemController(ILogger<FilesystemController> logger, IFileService fileService, AppDbContext context)
        {
            _logger = logger;
            _fileService = fileService;
            _context = context;
        }

        [HttpGet]
        [Route("workspaces")]
        public async Task<HttPeteResponse> GetWorkspaces(CancellationToken cancellationToken = default)
        {
            try
            {
                //var workspaces = await ReadLocalFileSystem(cancellationToken);
                var workspaces = await ReadSQLite(cancellationToken);

                return new HttPeteResponse(workspaces, 200, "");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async Task<Workspace[]> ReadSQLite(CancellationToken cancellationToken)
        {
            var ws = await _context.Workspaces.Include(x=>x.Collections).ThenInclude(x=>x.Endpoints).ThenInclude(x=>x.Documentation).ToArrayAsync(cancellationToken);

            if (ws.Length > 0)
                return ws;
            
            var wss = await _context.Workspaces.AddAsync(Workspace.Default);
            var col = await _context.Collections.AddAsync(wss.Entity.Collections.First());
            var ep = await _context.Endpoints.AddAsync(wss.Entity.Collections.First().Endpoints.First());
            var doc = await _context.Documents.AddAsync(wss.Entity.Collections.First().Endpoints.First().Documentation);

            await _context.SaveChangesAsync();
            
            col.Entity.WorkspaceId = wss.Entity.Id;
            ep.Entity.CollectionId = col.Entity.Id;
            doc.Entity.EndpointId = ep.Entity.Id;

            await _context.SaveChangesAsync();

            return [wss.Entity];
        }

        private async Task<Workspace[]> ReadLocalFileSystem(CancellationToken cancellationToken)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.httpete";
            string wsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.httpete/workspaces.json";



            string? workspacesJson = null;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var workspaces = JsonSerializer.Deserialize<Workspace[]>(await Files.ReadAllTextAsync(path + "/workspaces.json"));
            var collections = JsonSerializer.Deserialize<Collection[]>(await Files.ReadAllTextAsync(path + "/collections.json"));
            var endpoints = JsonSerializer.Deserialize<Endpoint[]>(await Files.ReadAllTextAsync(path + "/endpoints.json"));

            if (!Files.Exists(wsPath))
            {
                using var s = Files.Create(wsPath);
                using var httpClient = new HttpClient();
                var result = await httpClient.GetAsync("http://localhost:5273/api/workspace/get-default", cancellationToken);
                workspacesJson = await result.Content.ReadAsStringAsync();

                // Do not pass cancellation token here since if the operation is
                // interrupted we may end up with an unstable base file.
                // In the future we will write code to fix this situation should
                // it arrive, but for now keep this as it.
                await s.WriteAsync(Encoding.UTF8.GetBytes(workspacesJson));
            }

            workspacesJson ??= await Files.ReadAllTextAsync(wsPath);
            return workspaces;
        }
    }
}
