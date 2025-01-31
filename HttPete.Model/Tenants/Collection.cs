using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    /// <summary>
    /// A collection of endpoints.
    /// </summary>
    public class Collection : Entity, IEntity
    {
        /// <summary>
        /// The name of the collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A description of the collection.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The icon for the collection.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// The organization that this collection belongs to.
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// The workspace that this collection belongs to.
        /// </summary>
        public int WorkspaceId { get; set; }

        /// <summary>
        /// The endpoints in this collection.
        /// </summary>
        public IEnumerable<Endpoint>? Endpoints { get; set; }

        /// <summary>
        /// Constructor for creating a new collection.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgId"></param>
        /// <param name="workspaceId"></param>
        public Collection(string userName, int orgId, int workspaceId)
        {
            OrganizationId = orgId;
            Description = "My first collection.";
            WorkspaceId = workspaceId;
            Name = userName + "\'s Collection";
        }

        /// <summary>
        /// Default constructor for Entity Framework.
        /// </summary>
        public Collection()
        {

        }
    }
}