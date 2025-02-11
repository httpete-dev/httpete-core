using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using HttPete.Services.DTOs;
using HttPete.Services.Services.Models;
using Mapster;

namespace HttPete.Services.Services
{
    public interface IDocumentationService
    {
        Task<Result<DocumentDto>> Get(int documentId, CancellationToken cancellationToken = default);
        
        Task<Result<IReadOnlyCollection<DocumentDto>>> GetRootDocuments(
            int organizationId, 
            int workspaceId, 
            bool includeContent, 
            CancellationToken cancellationToken = default);
        
        Task<Result<DocumentDto>> Create(CreateDocumentDto document, CancellationToken cancellationToken = default);
        
        Task<Result<DocumentDto>> Update(DocumentDto document, CancellationToken cancellationToken = default);
    }

    internal class DocumentationService : IDocumentationService
    {
        private readonly IDocumentationRepository _documentationRepository;

        public DocumentationService(IDocumentationRepository documentationRepository)
        {
            _documentationRepository = documentationRepository;
        }

        public async Task<Result<IReadOnlyCollection<DocumentDto>>> GetRootDocuments(
            int organizationId, 
            int workspaceId, 
            bool includeContent, 
            CancellationToken cancellationToken = default)
        {
            var rootDocuments = await _documentationRepository.GetRootDocuments(organizationId, workspaceId, includeContent, cancellationToken);
            if(rootDocuments == null || !rootDocuments.Any())
            {
                return new Result<IReadOnlyCollection<DocumentDto>>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"No documents found for organization {organizationId} and workspace {workspaceId}.",
                    Status: ResultStatus.NotFound);
            }

            return new Result<IReadOnlyCollection<DocumentDto>>(
                IsSuccess: true,
                Value: rootDocuments.Select(d => d.Adapt<DocumentDto>()).ToArray().AsReadOnly(),
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<DocumentDto>> Get(int documentId, CancellationToken cancellationToken = default)
        {
            var document = await _documentationRepository.GetById(documentId, cancellationToken);
            if(document == null)
            {
                return new Result<DocumentDto>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"Document with the id {documentId} doesn't exist.",
                    Status: ResultStatus.NotFound);
            }

            return new Result<DocumentDto>(
                IsSuccess: true, 
                Value: document.Adapt<DocumentDto>(),
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<DocumentDto>> Update(DocumentDto document, CancellationToken cancellationToken = default)
        {
            var existingDocument = await _documentationRepository.GetById(document.Id, cancellationToken);
            if (existingDocument == null)
            {
                return new Result<DocumentDto>(
                    IsSuccess: false,
                    Value: null,
                    Error: $"Document with the id {document.Id} doesn't exist.",
                    Status: ResultStatus.NotFound);
            }

            var updatedDocument = await _documentationRepository.Update(document.Adapt<Document>(), cancellationToken);
            return new Result<DocumentDto>(
                IsSuccess: true,
                Value: updatedDocument.Adapt<DocumentDto>(),
                Error: null,
                Status: ResultStatus.Success);
        }

        public async Task<Result<DocumentDto>> Create(CreateDocumentDto document, CancellationToken cancellationToken = default)
        {
            //NC_TODO: Validate parent - child relationships
            var createdDocument = await _documentationRepository.Add(document.Adapt<Document>(), cancellationToken);
            return new Result<DocumentDto>(
                IsSuccess: true,
                Value: createdDocument.Adapt<DocumentDto>(),
                Error: null,
                Status: ResultStatus.Success);
        }
    }
}
