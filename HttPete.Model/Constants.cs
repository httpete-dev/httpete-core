using HttPete.Model.Tenants;

namespace HttPete.Model
{
    /// <summary>
    /// Default values for quick object creation.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// The default base url value.
        /// </summary>
        public static readonly string DEFAULT_BASEURL_VALUE = "api.httpete.dev/api";

        /// <summary>
        /// The default base url protocol (HTTP/HTTPS)
        /// </summary>
        public static readonly string DEFAULT_BASEURL_PROTOCOL = "HTTPS";

        /// <summary>
        /// The default base url.
        /// </summary>
        public static readonly BaseUrl DEFAULT_BASEURL = new()
        {
            Protocol = DEFAULT_BASEURL_PROTOCOL,
            Value = DEFAULT_BASEURL_VALUE
        };

        /// <summary>
        /// The default document text.
        /// </summary>
        public static readonly string DEFAULT_DOCUMENT_TEXT = "# Welcome\nWelcome to HttPete!\nBrowse around to find a default Workspace we have created for you.\n" +
                                    "Feel free to edit anything or downright delete the workspace. You can always start fresh by clicking the \"Add New\", button " +
                                    "in the Workspaces dropdown in the top navigation.\n\nEach Workspace is comprised of Collections, which can have many Endpoints.\n" +
                                    "Each Endpoint has a Document, just like this one, with endpoint specific information, which you can import from any XML documentation file.\n" +
                                    "If you're coming over from Postman, feel free to use the import button next to the Collections in the sidebar, to get a head start with the endpoints." +
                                    "you had in Postman.\nAll of your data is stored locally on your device. If you'd like to enable backups, version control, and some [additional features](https://httpete.dev/cloud-features), " +
                                    "please create an account. Alternatively, you can also host your own version of HttPete, you can find a guide to doing so [here](https://httpete.dev/guides/self-host)." +
                                    "\n\nThank you for using HttPete, feel free to send any questions of feedback to [hello@httpete.dev](mailto:hello@httpete.dev).";
        /// <summary>
        /// The default document title.
        /// </summary>
        public static readonly string DEFAULT_DOCUMENT_TITLE = "Untitled Document";

        /// <summary>
        /// The default document.
        /// </summary>
        public static readonly Document DEFAULT_DOCUMENT = new()
        {
            Title = DEFAULT_DOCUMENT_TITLE,
            Text = DEFAULT_DOCUMENT_TEXT
        };

        /// <summary>
        /// The default endpoint url.
        /// </summary>
        public static readonly string DEFAULT_ENDPOINT_URL = "/default/hello-world";

        /// <summary>
        /// The default endpoint headers.
        /// </summary>
        public static readonly string DEFAULT_ENDPOINT_HEADERS = "{\n\"Accept\": \"application/json\"\n}";

        /// <summary>
        /// The default endpoint body.
        /// </summary>
        public static readonly string DEFAULT_ENDPOINT_BODY = "{\n\"Message\": \"Hello!\"\n}";

        /// <summary>
        /// The default endpoint method.
        /// </summary>
        public static readonly string DEFAULT_ENDPOINT_METHOD = "GET";

        /// <summary>
        /// The default endpoint.
        /// </summary>
        public static readonly Endpoint DEFAULT_ENDPOINT = new Endpoint
        {
            Url = DEFAULT_ENDPOINT_URL,
            Headers = DEFAULT_ENDPOINT_HEADERS,
            Body = DEFAULT_ENDPOINT_BODY,
            Documentation = DEFAULT_DOCUMENT,
            Method = DEFAULT_ENDPOINT_METHOD,
            BaseUrl = DEFAULT_BASEURL
        };

        /// <summary>
        /// The default collection name.
        /// </summary>
        public static readonly string DEFAULT_COLLECTION_NAME = "Default Collection";

        /// <summary>
        /// The default collection icon.
        /// </summary>
        public static readonly string DEFAULT_COLLECTION_ICON = "Globe";

        /// <summary>
        /// The default collection description.
        /// </summary>
        public static readonly string DEFAULT_COLLECTION_DESCRIPTION = "This is the default collection.";

        /// <summary>
        /// The default collection.
        /// </summary>
        public static readonly Collection DEFAULT_COLLECTION = new Collection
        {
            Name = DEFAULT_COLLECTION_NAME,
            Description = DEFAULT_COLLECTION_DESCRIPTION,
            Icon = DEFAULT_COLLECTION_ICON,
            Endpoints = [DEFAULT_ENDPOINT],
        };

        /// <summary>
        /// The default workspace name.
        /// </summary>
        public static readonly string DEFAULT_WORKSPACE_NAME = "Default Workspace";

        /// <summary>
        /// The default workspace description.
        /// </summary>
        public static readonly string DEFAULT_WORKSPACE_DESCRIPTION = "My new Workspace";

        /// <summary>
        /// The default workspace.
        /// </summary>
        public static readonly Workspace DEFAULT_WORKSPACE = new()
        {
            Title = DEFAULT_WORKSPACE_NAME,
            Collections = [DEFAULT_COLLECTION],
            Description = DEFAULT_WORKSPACE_DESCRIPTION
        };
    }

    public static class Constants
    {
        
    }
}
