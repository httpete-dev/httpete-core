namespace HttPete.Model.Postman
{
    public record PostmanBody
    {
        public string? Mode { get; set; }
        public string? Raw { get; set; }
        public List<PostmanFormDataItem>? Formdata { get; set; }
        public List<PostmanUrlEncodedItem>? Urlencoded { get; set; }
    }
}
