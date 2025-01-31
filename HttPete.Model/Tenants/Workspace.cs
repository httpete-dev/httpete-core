using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    /// <summary>
    /// A workspace is a container for collections, settings and user data.
    /// </summary>
    public class Workspace : Entity, IEntity
    {
        /// <summary>
        /// The title of the workspace.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A description of the workspace.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The organization that this workspace belongs to.
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// The collections in this workspace.
        /// </summary>
        public IEnumerable<Collection>? Collections { get; set; }

        /// <summary>
        /// Constructor for creating a new workspace.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="collections"></param>
        public Workspace(string userName, int orgId, string? title = null, string? description = null, Collection[]? collections = null)
        {
            Title = title ?? (userName + "\'s Workspace");
            Description = description ?? "My first workspace.";
            OrganizationId = orgId;
            Collections = collections ?? Default.Collections;
        }

        /// <summary>
        /// Default constructor for Entity Framework.
        /// </summary>
        public Workspace()
        {
            Title = Default.Title;
            Description = Default.Description;
            OrganizationId = Default.OrganizationId;
            Collections = Default.Collections;
        }

        public static Workspace Default => Defaults.DEFAULT_WORKSPACE;
    }
}