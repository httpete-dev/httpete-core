using HttPete.Services.Config;

namespace HttPete.Services.Network
{
    public record HttPeteResponse
    {
        public HttPeteResponse(dynamic content, int statusCode, string? message)
        {
            Content = content;
            StatusCode = statusCode;
            Message = message ?? HttPeteSettings.DEFAULT_RESPONSE_MESSAGE;
        }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public dynamic Content { get; set; }
    }
}
