using FileProcessor.CORE.Services;
using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.Product
{
    public class GetProductsTests
    {
        [Fact]
        public async Task GetProducts_ShouldReturnProducts_WhenProductsExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new List<ProductDTO>() {
                    new ProductDTO() { Name = "Coffee", ProductId = 2 },
                    new ProductDTO() { Name = "Tea", ProductId = 3 }
                });
            var service = new ProductService(mockRepository.Object,
                new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetProducts();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).ToList().Should().Contain(new List<string> { "Coffee", "Tea" });
        }

        [Fact]
        public async Task GetProducts_ShouldNotReturnProducts_WhenProductsNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new List<ProductDTO>());
            var service = new ProductService(mockRepository.Object,
                new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetProducts();

            //Assert
            result.Should().BeEmpty();
        }
    }
}
