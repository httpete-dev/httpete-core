using HttPete.Infrastructure.Persistence.Interfaces.Repositories;
using HttPete.Model.Tenants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    internal class DocumentationRepository : BaseRepository<Document>, IDocumentationRepository
    {
        public DocumentationRepository(HttPeteDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Document>> GetRootDocuments(
            int organizationId, 
            int workspaceId, 
            bool includeContent, 
            CancellationToken cancellationToken = default)
        {
            var allDocs = await _dbSet
                    .AsNoTracking()
                    .Where(x => x.OrganizationId == organizationId &&
                               x.WorkspaceId == workspaceId)
                    .Select(doc => new Document
                    {
                        Id = doc.Id,
                        OrganizationId = doc.OrganizationId,
                        WorkspaceId = doc.WorkspaceId,
                        ParentId = doc.ParentId,
                        Title = doc.Title,
                        Text = includeContent ? doc.Text : null,
                        AuthorId = doc.AuthorId,
                        LastEditById = doc.LastEditById,
                        Created = doc.Created,
                        LastEdited = doc.LastEdited,
                        EndpointId = doc.EndpointId,
                        RelatedEndpointsIds = doc.RelatedEndpointsIds
                    })
                    .ToListAsync(cancellationToken);

            var docMap = allDocs.ToDictionary(d => d.Id);
            var rootDocs = new List<Document>();

            foreach (var doc in allDocs)
            {
                if (!doc.ParentId.HasValue)
                {
                    rootDocs.Add(doc);
                }
                else if (docMap.TryGetValue(doc.ParentId.Value, out var parent))
                {
                    ((List<Document>)parent.Children).Add(doc);
                }
            }

            return rootDocs.AsReadOnly();
        }
    }
}
