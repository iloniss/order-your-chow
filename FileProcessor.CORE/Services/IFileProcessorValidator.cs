using Microsoft.AspNetCore.Http;
using OrderYourChow.Infrastructure.Services;

namespace FileProcessor.CORE.Services
{
    public interface IFileProcessorValidator : IScopedService
    {
        bool IsImageFile(IFormFile file);
    }
}
