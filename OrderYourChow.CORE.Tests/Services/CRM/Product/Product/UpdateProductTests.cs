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
    public class UpdateProductTests
    {
        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenImageFileInvalid()
        {
            // Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                .ReturnsAsync(new UpdatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(false);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
               .Verifiable();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            // Act
            var result = await service.UpdateProduct(new Mock<IFormFile>().Object, new ProductDTO { Name = "Coffee", ProductId = 2 });

            // Assert
            result.Should().BeOfType<ErrorProductDTO>();
            ((ErrorProductDTO)result).Message.Should().Be(Const.Shared.Global.InvalidFile);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenProductNameExists()
        {
            // Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(new ProductDTO() { Name = "Coffee", ProductId = 3 });
            mockRepository
                .Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                .ReturnsAsync(new UpdatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
               .Verifiable();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            // Act
            var result = await service.UpdateProduct(new Mock<IFormFile>().Object, new ProductDTO { Name = "Coffee", ProductId = 2 });

            // Assert
            result.Should().BeOfType<ErrorProductDTO>();
            ((ErrorProductDTO)result).Message.Should().Be(Const.CRM.Product.ExistedProduct);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenProductExitsAndProductNameIsEqual()
        {
            // Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(new ProductDTO() { Name = "Coffee", ProductId = 2 });
            mockRepository
                .Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                .ReturnsAsync(new UpdatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
               .Verifiable();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            // Act
            var result = await service.UpdateProduct(new Mock<IFormFile>().Object, new ProductDTO { Name = "Coffee", ProductId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedProductDTO>();
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenProductExistsAndProductNameNotExists()
        {
            // Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                .ReturnsAsync(new UpdatedProductDTO());
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
               .Verifiable();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            // Act
            var result = await service.UpdateProduct(new Mock<IFormFile>().Object, new ProductDTO { Name = "Coffee", ProductId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedProductDTO>();
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenProductNotExists()
        {
            // Arrange
            var imageName = Guid.NewGuid().ToString();
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(new ProductDTO() { Name = "Coffee", ProductId = 2 });
            mockRepository
                .Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                .ReturnsAsync(new EmptyProductDTO(Const.CRM.Product.NotFoundProduct));
            var mockFileProcessorValidator = new Mock<IFileProcessorValidator>();
            mockFileProcessorValidator
                .Setup(x => x.IsImageFile(It.IsAny<IFormFile>()))
                .Returns(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
               .Verifiable();
            mockFileProcessor.Setup(x => x.SaveFileFromWebsite(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(imageName);
            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, mockFileProcessorValidator.Object);

            // Act
            var result = await service.UpdateProduct(new Mock<IFormFile>().Object, new ProductDTO { Name = "Coffee", ProductId = 2 });

            // Assert
            result.Should().BeOfType<EmptyProductDTO>();
            ((EmptyProductDTO)result).Message.Should().Be(Const.CRM.Product.NotFoundProduct);
        }
    }
}
