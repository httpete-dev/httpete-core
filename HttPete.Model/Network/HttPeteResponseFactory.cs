namespace HttPete.Services.Network
{
    public record FileContents
    {
        public string Path { get; set; }

        public string[]? Lines { get; set; }

        public string? Text { get; set; }
    }

    public static class HttPeteResponseFactory
    {
        public static HttPeteResponse FileDoesNotExist(string path)
            => new HttPeteResponse(new Exception(), 404, $"File does not exist: {path}");
        
        public static HttPeteResponse FileContents(string path, string[]? lines = null, string? text = null)
            => new HttPeteResponse(new FileContents 
            {
                Path = path,
                Lines = lines,
                Text = text
            }, 200, $"File: {path}");


            public static HttPeteResponse Unexpected(string v, Exception? e = null)
                => new HttPeteResponse(e, 500, $"An unexpected error occurred in {v}" + e != null ? $" - {e!.Message}" : "");
    }

}
