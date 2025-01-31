using HttPete.Services.Network;

namespace HttPete.Services.IO
{
    public interface IFileService 
    {
        Task<HttPeteResponse> ReadText(string path);
    }
    public class FileService : IFileService
    {
        public async Task<HttPeteResponse> ReadText(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return HttPeteResponseFactory.FileDoesNotExist(path);

                var text = await File.ReadAllTextAsync(path);

                return HttPeteResponseFactory.FileContents(path, text: text);
            }
            catch (Exception e)
            {
                return HttPeteResponseFactory.Unexpected($"{nameof(IFileService)}:{nameof(ReadText)}", e);
            }
        }
    }

}
