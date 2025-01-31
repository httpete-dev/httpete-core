using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    /// <summary>
    /// The base url for a workspace.
    /// </summary>
    public class BaseUrl : Entity, IEntity
    {
        /// <summary>
        /// The workspace this base url belongs to.
        /// </summary>
        public int WorkspaceId { get; set; }

        /// <summary>
        /// HTTP/HTTPS
        /// </summary>
        public string Protocol { get; set; } = "https";

        /// <summary>
        /// The url itself: www.example.com
        /// </summary>
        public string Value { get; set; } = "httpete.dev";
    }
}