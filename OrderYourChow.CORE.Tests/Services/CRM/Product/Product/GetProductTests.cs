using FileProcessor.CORE.Services;
using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.Product
{
    public class GetProductTests
    {
        [Fact]
        public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(new ProductDTO() { Name = "Coffee", ProductId = 1 });
            var service = new ProductService(mockRepository.Object, 
                new Mock<IFileProcessor>().Object, 
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetProduct(new GetProductQuery() { ProductId = 1});

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Coffee");
        }

        [Fact]
        public async Task GetProduct_ShouldNotReturnProduct_WhenProductNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(x => x.GetProductAsync(It.IsAny<GetProductQuery>()))
                .ReturnsAsync(value: null);
            var service = new ProductService(mockRepository.Object,
                new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetProduct(new GetProductQuery() { ProductId = 2 });

            //Assert
            result.Should().BeNull();
        }
    }
}
