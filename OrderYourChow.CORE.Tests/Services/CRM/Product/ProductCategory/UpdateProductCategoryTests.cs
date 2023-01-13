using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.ProductCategory
{
    public class UpdateProductCategoryTests
    {
        [Fact]
        public async Task UpdateProductCategory_ShouldUpdateProductCategory_WhenProductCategoryWithEqualNameNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new UpdatedProductCategoryDTO());
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.UpdateProductCategory(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedProductCategoryDTO>();
        }

        [Fact]
        public async Task UpdateProductCategory_ShouldNotUpdateProductCategory_WhenProductCategoryNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new EmptyProductCategoryDTO(Const.CRM.Product.NotFoundProductCategory));
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.UpdateProductCategory(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<EmptyProductCategoryDTO>();
            ((EmptyProductCategoryDTO)result).Message.Should().Be(Const.CRM.Product.NotFoundProductCategory);
        }



        [Fact]
        public async Task UpdateProductCategory_ShouldNotUpdateProductCategory_WhenProductCategoryExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 3 });
            mockRepository
                .Setup(x => x.UpdateProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new UpdatedProductCategoryDTO());
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.UpdateProductCategory(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<ErrorProductCategoryDTO>();
            ((ErrorProductCategoryDTO)result).Message.Should().Be(Const.CRM.Product.ExistedProductCategory);
        }

        [Fact]
        public async Task UpdateProductCategory_ShouldUpdateProductCategory_WhenProductCategoryIsEqual()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.GetProductCategoryAsync(It.IsAny<GetProductCategoryQuery>()))
                .ReturnsAsync(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 2 });
            mockRepository
                .Setup(x => x.UpdateProductCategoryAsync(It.IsAny<ProductCategoryDTO>()))
                .ReturnsAsync(new UpdatedProductCategoryDTO());
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.UpdateProductCategory(new ProductCategoryDTO { Name = "Beverages", ProductCategoryId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedProductCategoryDTO>();
        }
    }
}
