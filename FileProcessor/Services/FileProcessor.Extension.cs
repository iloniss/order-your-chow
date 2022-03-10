using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileProcessor.Services
{
    public partial class FileProcessor 
    {
        private readonly IConfiguration _configuration;

        public FileProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetFilePath(string fileName, string imageType)
        {
            string imagePath = _configuration.GetSection(imageType).Value;
            if (string.IsNullOrEmpty(imagePath))
                return null;
            string path = Path.Combine(Directory.GetCurrentDirectory(), imagePath);
            if (!CheckDirectory(path))
                return null;
            return Path.Combine(path, fileName ?? "");
        } 

        public static string CreateFileName(string fileName) => Guid.NewGuid().ToString() + Path.GetExtension(fileName);

        private static async Task Save(IFormFile file, string path, FileMode fileMode)
        {
            using var fileStream = new FileStream(path, fileMode);
            await file.CopyToAsync(fileStream);
        }

        private static bool IsFile(IFormFile formFile) => formFile != null && formFile.Length > 0;
        private static bool CheckDirectory(string path) => Directory.Exists(path);

    }
}
