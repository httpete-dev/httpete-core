namespace HttPete.Model.Postman
{
    public record PostmanFormDataItem
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}
