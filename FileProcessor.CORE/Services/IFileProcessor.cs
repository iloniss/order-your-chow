using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileProcessor.CORE.Services
{
    public interface IFileProcessor
    {
        Task<string> SaveFileFromWebsite(IFormFile file, string imageType);
        void DeleteFile(string name, string imageType);

    }
}
