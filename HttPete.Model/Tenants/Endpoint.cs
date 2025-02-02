using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    /// <summary>
    /// Represents an API endpoint.
    /// </summary>
    public class Endpoint : Entity, IEntity
    {
        /// <summary>
        /// The URL of the endpoint. Does not include the base URL.
        /// Ex: /users/get
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The headers of the endpoint.
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// The method of the endpoint. (GET, POST, PATCH, PUT, DELETE)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The base URL of the endpoint.
        /// </summary>
        public BaseUrl BaseUrl { get; set; }

        /// <summary>
        /// The ID of the base URL.
        /// </summary>
        public int BaseUrlId { get; set; }

        /// <summary>
        /// The body of the request.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The ID of the collection this endpoint belongs to.
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// The ID of the workspace this endpoint belongs to.
        /// </summary>
        public int WorkspaceId { get; set; }

        /// <summary>
        /// The ID of the organization this endpoint belongs to.
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// The documentation of the endpoint.
        /// </summary>
        public Document Documentation { get; set; }
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Endpoint() 
        {
            Url = Defaults.DEFAULT_ENDPOINT_URL;
            Method = Defaults.DEFAULT_ENDPOINT_METHOD;
            Headers = Defaults.DEFAULT_ENDPOINT_HEADERS;
            Body = Defaults.DEFAULT_ENDPOINT_BODY;
        }

        /// <summary>
        /// Default Endpoint definition, used for new accounts.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="workspaceId"></param>
        /// <param name="collectionId"></param>
        public Endpoint(int organizationId, int workspaceId, int collectionId)
        {
            Url = Defaults.DEFAULT_ENDPOINT_URL;
            Method = Defaults.DEFAULT_ENDPOINT_METHOD;
            Headers = Defaults.DEFAULT_ENDPOINT_HEADERS;
            Body = Defaults.DEFAULT_ENDPOINT_BODY;
            CollectionId = collectionId;
            OrganizationId = organizationId;
            WorkspaceId = workspaceId;
            Documentation = Defaults.DEFAULT_DOCUMENT;
        }
    }
}