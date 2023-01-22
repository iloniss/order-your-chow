using Microsoft.AspNetCore.Http;
using OrderYourChow.Infrastructure.Services;
using System.Threading.Tasks;

namespace FileProcessor.CORE.Services
{
    public interface IFileProcessor : IScopedService
    {
        Task<string> SaveFileFromWebsite(IFormFile file, string imageType);
        void DeleteFile(string name, string imageType);
    }
}
