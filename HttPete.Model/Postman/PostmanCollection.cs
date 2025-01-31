namespace HttPete.Model.Postman
{
    public record PostmanCollection
    {
        public PostmanInfo? Info { get; set; }
        public List<PostmanRequest>? Item { get; set; }
    }
}
