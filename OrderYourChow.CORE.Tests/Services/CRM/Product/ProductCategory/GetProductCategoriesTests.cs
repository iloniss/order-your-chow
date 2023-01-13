using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.ProductCategory
{
    public class GetProductCategoriesTests
    {
        [Fact]
        public async Task GetProductCategories_ShouldReturnProductCategories_WhenProductCategoriesExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoriesAsync())
                .ReturnsAsync(new List<ProductCategoryDTO>() {
                    new ProductCategoryDTO() { Name = "Beverages", ProductCategoryId = 2 },
                    new ProductCategoryDTO() { Name = "Entrees", ProductCategoryId = 3 }
                });
            var service = new ProductCategoryService(mockRepository.Object);

            //Act
            var result = await service.GetProductCategories();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).ToList().Should().Contain(new List<string> { "Beverages", "Entrees" });
        }

        [Fact]
        public async Task GetProductCategories_ShouldNotReturnProductCategories_WhenProductCategoriesNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoriesAsync())
                .ReturnsAsync(new List<ProductCategoryDTO>());
            var service = new ProductCategoryService(mockRepository.Object);

            //Act
            var result = await service.GetProductCategories();

            //Assert
            result.Should().BeEmpty();
        }
    }
}
