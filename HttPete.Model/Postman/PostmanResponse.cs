namespace HttPete.Model.Postman
{
    public record PostmanResponse
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public int? Code { get; set; }
        public string? Body { get; set; }
        public List<PostmanHeader>? Header { get; set; }
    }
}
