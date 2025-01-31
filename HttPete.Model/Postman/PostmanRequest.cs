namespace HttPete.Model.Postman
{
    public record PostmanRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public PostmanRequestDetails? Request { get; set; }
        public List<PostmanResponse>? Response { get; set; }
        public List<PostmanRequest>? Item { get; set; }

        public bool IsFolder()
        {
            return Item != null && Item.Count > 0;
        }
    }
}
