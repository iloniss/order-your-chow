using FileProcessor.CORE.Services;
using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.Product
{
    public class DeleteProductTests
    {
        [Fact]
        public async Task DeleteProduct_ShouldDeleteProduct_WhenProductExitsAndIsNotUsed()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedProductDTO());
            mockRepository
                .Setup(x => x.ProductIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, new Mock<IFileProcessorValidator>().Object);

            // Act
            var result = await service.DeleteProduct(2);

            // Assert
            result.Should().BeOfType<DeletedProductDTO>();
        }

        [Fact]
        public async Task DeleteProduct_ShouldNotDeleteProduct_WhenProductNotExits()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new EmptyProductDTO(Const.CRM.Product.NotFoundProduct));
            mockRepository
                .Setup(x => x.ProductIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, new Mock<IFileProcessorValidator>().Object);

            // Act
            var result = await service.DeleteProduct(2);

            // Assert
            result.Should().BeOfType<EmptyProductDTO>();
            ((EmptyProductDTO)result).Message.Should().Be(Const.CRM.Product.NotFoundProduct);
        }

        [Fact]
        public async Task DeleteProduct_ShouldNotDeleteProduct_WhenProductIsUsed()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedProductDTO());
            mockRepository
                .Setup(x => x.ProductIsUsed(It.IsAny<int>()))
                .ReturnsAsync(true);
            var mockFileProcessor = new Mock<IFileProcessor>();
            mockFileProcessor.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var service = new ProductService(mockRepository.Object, mockFileProcessor.Object, new Mock<IFileProcessorValidator>().Object);

            // Act
            var result = await service.DeleteProduct(2);

            // Assert
            result.Should().BeOfType<ErrorProductDTO>();
            ((ErrorProductDTO)result).Message.Should().Be(Const.CRM.Product.UsedProduct);
        }
    }
}
