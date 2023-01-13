using FileProcessor.CORE.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.Product
{
    public class AddProductTests
    {
        [Fact]
        public async Task AddProduct_ShouldNotAddProduct_WhenImageFileInvalid()
        {
            //Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.AddProductAsync(It.IsAny<AddProductDTO>()))
                .ReturnsAsync(new CreatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(false);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            //Act
            var result = await service.AddProduct(new Mock<IFormFile>().Object, new AddProductDTO() { Name = "Coffee", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<ErrorProductDTO>();
            ((ErrorProductDTO)result).Message.Should().Be(Const.Shared.Global.InvalidFile);
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProduct_WhenProductExits()
        {
            //Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(new ProductDTO() { Name = "Coffee", ProductCategoryId = 2 });
            mockRepository
                .Setup(x => x.AddProductAsync(It.IsAny<AddProductDTO>()))
                .ReturnsAsync(new CreatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            //Act
            var result = await service.AddProduct(new Mock<IFormFile>().Object, new AddProductDTO() { Name = "Coffee", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<ErrorProductDTO>();
            ((ErrorProductDTO)result).Message.Should().Be(Const.CRM.Product.ExistedProduct);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct_WhenProductNotExitsAndFileIsValid()
        {
            //Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.AddProductAsync(It.IsAny<AddProductDTO>()))
                .ReturnsAsync(new CreatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            //Act
            var result = await service.AddProduct(new Mock<IFormFile>().Object, new AddProductDTO() { Name = "Coffee", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<CreatedProductDTO>();
        }
    }
}
