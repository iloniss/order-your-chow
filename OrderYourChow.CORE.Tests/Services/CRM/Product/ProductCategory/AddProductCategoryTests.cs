using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.ProductCategory
{
    public class AddProductCategoryTests
    {
        [Fact]
        public async Task AddProductCategory_ShouldAddProductCategory_WhenProductCategoryNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.AddProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new CreatedProductCategoryDTO());
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.AddProductCategory(new ProductCategoryDTO { Name = "Beverages" });

            // Assert
            result.Should().BeOfType<CreatedProductCategoryDTO>();
        }

        [Fact]
        public async Task AddProductCategory_ShouldNotAddProductCategory_WhenProductCategoryExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(new ProductCategoryDTO() { Name = "Beverages", ProductCategoryId = 2 });
            mockRepository
                .Setup(x => x.AddProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new CreatedProductCategoryDTO());
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.AddProductCategory(new ProductCategoryDTO { Name = "Beverages" });

            // Assert
            result.Should().BeOfType<ErrorProductCategoryDTO>();
            ((ErrorProductCategoryDTO)result).Message.Should().Be(Const.CRM.Product.ExistedProductCategory);
        }
    }
}
