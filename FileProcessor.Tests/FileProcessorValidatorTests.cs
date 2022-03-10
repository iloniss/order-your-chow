using FileProcessor.Services;
using FileProcessor.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Linq;
using Xunit;

namespace FileProcessor.Tests
{
    public class FileProcessorValidatorTests
    {
        private readonly FileProcessorValidator _fileProcessorValidator;

        public FileProcessorValidatorTests()
        {
            _fileProcessorValidator = new();
        }

        [Theory]
        [ClassData(typeof(FIleProcessorValidatorData.FileProcessorValidatorSuccesData))]
        public void IsImageFile(string file, string contentType)
        {
            //Arrange
            IFormFile formFile = null;
            if (file != null)
            {
                using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
                formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last())
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
            }
            //Act
            bool isImage = _fileProcessorValidator.IsImageFile(formFile);

            //Assert
            Assert.True(isImage, "The file should be an image!");
        }

        [Theory]
        [ClassData(typeof(FIleProcessorValidatorData.FileProcessorValidatorFailureData))]
        public void IsNotImageFile(string file, string contentType)
        {
            //Arrange
            IFormFile formFile = null;
            if (file != null)
            {
                using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
                formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last())
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
            }
            //Act
            bool isImage = _fileProcessorValidator.IsImageFile(formFile);

            //Assert
            Assert.False(isImage, "The file should not be an image!");
        }
    }
}
