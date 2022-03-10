using FileProcessor.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Directory = FileProcessor.Tests.Data.Directory;

namespace FileProcessor.Tests
{
    public class FileProcessorTests
    {
        private readonly Services.FileProcessor _fileProcessor;
        private readonly Mock<IConfiguration> _configurationMock = new();

        public FileProcessorTests()
        {
            _fileProcessor = new(_configurationMock.Object);
            Directory.CreateTestDirectory();
        }


        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorSuccesData))]
        public async Task SaveFileFromWebsite_ShouldReturnFileName(string file, string folderPath, string imageType)
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                .Returns(folderPath);
            
            //Act
            var path = await _fileProcessor.SaveFileFromWebsite(formFile, imageType);

            //Assert
            Assert.NotNull(path);

            //Clean
            _fileProcessor.DeleteFile(path, imageType);
        }

        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorFailureData))]
        public async Task SaveFileFromWebsite_ShouldReturnNull(string file, string folderPath, string imageType)
        {
            //Arrange
            IFormFile formFile = null;
            if (file != null)
            { 
                using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
                formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
            }
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                .Returns(folderPath);

            //Act
            var path = await _fileProcessor.SaveFileFromWebsite(formFile, imageType);

            //Assert
            Assert.Null(path);
        }

        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorSuccesExtendedData))]
        public async Task SaveFileFromWebsite_ShouldSaveFile(string file, string folderPath, string imageType, string destinationFile)
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                .Returns(folderPath);

            //Act
            var path = await _fileProcessor.SaveFileFromWebsite(formFile, imageType);

            //Assert
            Assert.True(File.Exists(destinationFile + path));

            //Clean
            _fileProcessor.DeleteFile(path, imageType);
        }

        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorFailureExtendedData))]
        public async Task SaveFileFromWebsite_ShouldNotSaveFile(string file, string folderPath, string imageType, string destinationFile)
        {
            //Arrange
            IFormFile formFile = null;
            if (file != null)
            {
                using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
                formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
            }
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                .Returns(folderPath);

            //Act
            var path = await _fileProcessor.SaveFileFromWebsite(formFile, imageType);

            //Assert
            Assert.False(File.Exists(destinationFile + path));
        }

        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorDeleteSuccesData))]
        public async Task DeleteFile_ShouldDeleteFile(string fileName, string path, string imageType, string filePath)
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                 .Returns(path);
            //Act
            _fileProcessor.DeleteFile(fileName, imageType);

            //Assert
            Assert.False(File.Exists(filePath));

            //Clean
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
        }

        [Theory]
        [ClassData(typeof(FileProcessorData.FileProcessorDeleteFailureNotExistsData))]
        public void DeleteFile_ShouldNotDeleteFile(string fileName, string path, string imageType, string filePath)
        {
            //Arrange
            _configurationMock.Setup(x => x.GetSection(imageType).Value)
                .Returns(path);
            //Act
            _fileProcessor.DeleteFile(fileName, imageType);

            //Assert
            Assert.False(File.Exists(filePath));
        }


    }
}
