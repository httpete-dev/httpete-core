using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    public class Document : Entity, IEntity
    {
        public int OrganizationId { get; set; }

        public int WorkspaceId { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        /* NC_TODO:
         * - The documentation content should not be stored on a SQL database, only the metadata.
         * - As a future improvement, the content (column Text) should be renamed to ContentReference and 
         *   be stored on a object storage (BLOB/S3) or search-optimzied store (ElasticSearch) for cloud solution 
         *   and on the OS file system for the localhost solution.
         * - Generate embeddings to allow similarity search and indexing.
         */
        public string Text { get; set; }

        public int AuthorId { get; set; }

        public int LastEditById { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastEdited { get; set; }

        public IEnumerable<Document> Children { get; set; } = [];

        public IReadOnlyCollection<int> RelatedEndpointsIds { get; set; } = [];

        public Document() 
        {
            Created = DateTime.UtcNow;
            LastEdited = DateTime.UtcNow;
            Title = Defaults.DEFAULT_DOCUMENT_TITLE;
            Text = Defaults.DEFAULT_DOCUMENT_TEXT;
        }
        
        public Document(User user, int endpointId, int orgId, int workspaceId)
        {
            AuthorId = user.Id;
            Created = DateTime.UtcNow;
            EndpointId = endpointId;
            LastEditById = user.Id;
            LastEdited = DateTime.UtcNow;
            Title = Defaults.DEFAULT_DOCUMENT_TITLE;
            WorkspaceId = workspaceId;
            OrganizationId = orgId;
            Text = Defaults.DEFAULT_DOCUMENT_TEXT;
        }

        public int? EndpointId { get; set; }
    }
}