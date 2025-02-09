using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using HttPete.Services.Services.Models;

namespace HttPete.Services.Services
{
    public interface IDocumentationService
    {
        Task<Result<Document>> Get(int documentId, CancellationToken cancellationToken = default);
        
        Task<Result<IReadOnlyCollection<Document>>> GetRootDocuments(
            int organizationId, 
            int workspaceId, 
            bool includeContent, 
            CancellationToken cancellationToken = default);
        
        Task<Result<Document>> Create(Document document, CancellationToken cancellationToken = default);
        
        Task<Result<Document>> Update(Document document, CancellationToken cancellationToken = default);
    }

    internal class DocumentationService : IDocumentationService
    {
        private readonly IDocumentationRepository _documentationRepository;

        public DocumentationService(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task<Result<IReadOnlyCollection<Document>>> GetRootDocuments(
            int organizationId, 
            int workspaceId, 
            bool includeContent, 
            CancellationToken cancellationToken = default)
        {
            var rootDocuments = await _documentationRepository.GetRootDocuments(organizationId, workspaceId, includeContent, cancellationToken);
            if(rootDocuments == null || rootDocuments.Any())
            {
                return new Result<IReadOnlyCollection<Document>>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"No documents found for organization {organizationId} and workspace {workspaceId}.",
                    Status: ResultStatus.NotFound);
            }

            return new Result<IReadOnlyCollection<Document>>(
                IsSuccess: true,
                Value: rootDocuments,
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<Document>> Get(int documentId, CancellationToken cancellationToken = default)
        {
            var document = await _documentationRepository.GetById(documentId, cancellationToken);
            if(document == null)
            {
                return new Result<Document>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"Document with the id {documentId} doesn't exist.",
                    Status: ResultStatus.NotFound);
            }

            return new Result<Document>(
                IsSuccess: true, 
                Value: document,
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<Document>> Update(Document document, CancellationToken cancellationToken = default)
        {
            var existingDocument = await _documentationRepository.GetById(document.Id, cancellationToken);
            if (existingDocument == null)
            {
                return new Result<Document>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"Document with the id {document.Id} doesn't exist.",
                    Status: ResultStatus.NotFound);
            }

            var updatedDocument = await _documentationRepository.Update(existingDocument, cancellationToken);
            return new Result<Document>(
                IsSuccess: true,
                Value: updatedDocument,
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<Document>> Create(Document document, CancellationToken cancellationToken = default)
        {
            //NC_TODO: Validate parent - child relationships
            var createdDocument = await _documentationRepository.Add(document, cancellationToken);
            return new Result<Document>(
                IsSuccess: true,
                Value: createdDocument,
                Error: null,
                Status: ResultStatus.Success);
        }
    }
}
