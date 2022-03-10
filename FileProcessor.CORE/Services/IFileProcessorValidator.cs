using Microsoft.AspNetCore.Http;

namespace FileProcessor.CORE.Services
{
    public interface IFileProcessorValidator
    {
        bool IsImageFile(IFormFile file);
    }
}
