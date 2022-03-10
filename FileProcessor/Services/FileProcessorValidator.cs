using FileProcessor.CORE.Constans;
using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;

namespace FileProcessor.Services
{
    public class FileProcessorValidator : IFileProcessorValidator
    {
        public bool IsImageFile(IFormFile file)
        {
            if (file == null)
                return false;
            return ImageContentType.ContentTypes.Contains(file.ContentType.ToLower());
        }
    }
}
