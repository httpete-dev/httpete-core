namespace HttPete.Model.Postman
{
    public record PostmanRequestDetails
    {
        public string? Method { get; set; }
        public List<PostmanHeader>? Header { get; set; }
        public PostmanBody? Body { get; set; }
        public PostmanUrl? Url { get; set; }
    }
}
