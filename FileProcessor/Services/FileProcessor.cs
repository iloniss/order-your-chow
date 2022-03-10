using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FileProcessor.Services
{
    public partial class FileProcessor : IFileProcessor
    {
        public async Task<string> SaveFileFromWebsite(IFormFile file, string imageType)
        {
            if (IsFile(file))
            {
                string fileName = CreateFileName(file.FileName);
                string filePath = GetFilePath(fileName, imageType);
                if (filePath != null)
                {
                    await Save(file, filePath, FileMode.Create);
                    return fileName;
                }
                return null;
            }
            return null;
        }

        public void DeleteFile(string fileName, string imageType)
        {
            string path = GetFilePath(fileName, imageType);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
