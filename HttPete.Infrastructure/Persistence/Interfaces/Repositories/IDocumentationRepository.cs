using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.Interfaces.Repositories
{
    public interface IDocumentationRepository
    {
        Task<Document?> GetById(int id, CancellationToken cancellationToken = default);
        
        Task<Document> Add(Document entity, CancellationToken cancellationToken = default);
        
        Task<Document> Update(Document entity, CancellationToken cancellationToken = default);
        
        Task<IReadOnlyCollection<Document>> GetRootDocuments(
            int organizationId,
            int workspaceId,
            bool includeContent,
            CancellationToken cancellationToken = default);
    }
}
