using HttPete.Model.Tenants;
using HttPete.Services.Network;
using HttPete.Services.Services;
using HttPete.Services.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HttPete.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {

        /* TODO:
         * - Not receive the actual entity in the body, but a DTO
         * - Add tests.
         * - Add cloud support.
         * - GET /documentation/{docId}/childs
         * - PATCH /documentation/{docId}/relatedEndpoints
         */

        public IDocumentationService _documentationService;

        public DocumentationController(IDocumentationService documentationService)
        {
            _documentationService = documentationService;
        }

        /// <summary>
        /// Get root documents for an organization and workspace.
        /// Include the first level of children documents.
        /// </summary>
        /// <param name="organizationId">Organization Id</param>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="includeContent">Include the documents' content</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Documents list</returns>
        [HttpGet]
        [Route("root")]
        public async Task<HttPeteResponse> GetRootDocuments(
            [FromQuery] int organizationId, 
            [FromQuery] int workspaceId,
            [FromQuery] bool includeContent = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (organizationId == 0 && workspaceId == 0)
                {
                    return new HttPeteResponse(null, 400, "OrganizationId and WorkspaceId must be provided.");
                }

                var documents = await _documentationService.GetRootDocuments(
                    organizationId, 
                    workspaceId, 
                    includeContent, 
                    cancellationToken);

                return new HttPeteResponse(documents, 200, "Successfully retrieved documents.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Get document by id.
        /// </summary>
        /// <param name="documentId">Document Id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Document</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<HttPeteResponse> GetDocumentById(
            [FromRoute] int documentId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _documentationService.Get(documentId, cancellationToken);

                if(result.Status == ResultStatus.NotFound)
                {
                    return new HttPeteResponse(null, (int)HttpStatusCode.NotFound, result.Error);
                }

                return new HttPeteResponse(result.Value, 200, "Successfully retrieved documents.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Create a new document.
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Created document</returns>
        [HttpPost]
        [Route("")]
        public async Task<HttPeteResponse> CreateDocument(
            [FromBody] Document document,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (document.OrganizationId == 0 || document.WorkspaceId == 0)
                {
                    return new HttPeteResponse(null, (int)HttpStatusCode.BadRequest, "OrganizationId and WorkspaceId must be provided.");
                }

                var createdDocument = await _documentationService.Create(document, cancellationToken);

                //NC_TODO: Return the appropriate status code for non-happy paths

                return new HttPeteResponse(createdDocument, (int)HttpStatusCode.Created, "Successfully retrieved documents.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }

        /// <summary>
        /// Updates an existing document.
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Updated document</returns>
        [HttpPut]
        [Route("")]
        public async Task<HttPeteResponse> UpdateDocument(
            [FromBody] Document document,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (document.OrganizationId == 0 || document.WorkspaceId == 0)
                {
                    return new HttPeteResponse(null, (int)HttpStatusCode.BadRequest, "OrganizationId and WorkspaceId must be provided.");
                }

                var result = await _documentationService.Update(document, cancellationToken);

                if (result.Status == ResultStatus.NotFound)
                {
                    return new HttPeteResponse(null, (int)HttpStatusCode.NotFound, result.Error);
                }

                return new HttPeteResponse(result.Value, (int)HttpStatusCode.Created, "Successfully retrieved documents.");
            }
            catch (Exception e)
            {
                return new HttPeteResponse(e, 500, e.Message);
            }
        }
    }
}
