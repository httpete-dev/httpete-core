namespace HttPete.Model.Postman
{
    public record PostmanUrl
    {
        public string? Raw { get; set; }
        public string? Protocol { get; set; }
        public List<string>? Host { get; set; }
        public List<string>? Path { get; set; }
        public List<PostmanQueryParameter>? Query { get; set; }
    }
}
