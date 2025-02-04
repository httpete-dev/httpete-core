using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    public class Document : Entity, IEntity
    {
        public int OrganizationId { get; set; }

        public int WorkspaceId { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int AuthorId { get; set; }

        public int LastEditById { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastEdited { get; set; }

        public IEnumerable<Document> Children { get; set; } = [];

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