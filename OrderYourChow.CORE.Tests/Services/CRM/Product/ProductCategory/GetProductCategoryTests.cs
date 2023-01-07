using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.ProductCategory
{
    public class GetProductCategoryTests
    {
        [Fact]
        public async Task GetProductCategory_ShouldReturnProductCategory_WhenProductCategoryExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(new ProductCategoryDTO() { Name = "Beverages", ProductCategoryId = 2 });
            var service = new ProductCategoryService(mockRepository.Object);

            //Act
            var result = await service.GetProductCategory(new GetProductCategoryQuery(productCategoryId: 2));

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Beverages");
        }

        [Fact]
        public async Task GetProductCategory_ShouldNotReturnProductCategory_WhenProductCategoryNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(value: null);
            var service = new ProductCategoryService(mockRepository.Object);

            //Act
            var result = await service.GetProductCategory(new GetProductCategoryQuery(productCategoryId: 2));

            //Assert
            result.Should().BeNull();
        }
    }
}
