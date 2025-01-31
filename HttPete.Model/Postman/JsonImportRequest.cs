namespace HttPete.Model.Postman
{
    public record JsonImportRequest
    {
        public int UserId { get; set; }
        
        public string UserName { get; set; }
                
        public int WorkspaceId { get; set; }

        public PostmanCollection Data { get; set; }
    }
}
