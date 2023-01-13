using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Product;

namespace OrderYourChow.CORE.Tests.Services.CRM.Product.ProductCategory
{
    public class DeleteProductCategoryTests
    {
        [Fact]
        public async Task DeleteProductCategory_ShouldDeleteProductCategory_WhenProductCategoryExitsAndIsNotUsed()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.DeleteProductCategoryAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedProductCategoryDTO());
            mockRepository
                .Setup(x => x.ProductCategoryIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.DeleteProductCategory(2);

            // Assert
            result.Should().BeOfType<DeletedProductCategoryDTO>();
        }

        [Fact]
        public async Task DeleteProductCategory_ShouldNotDeleteProductCategory_WhenProductCategoryNotExits()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.DeleteProductCategoryAsync(It.IsAny<int>()))
                .ReturnsAsync(new EmptyProductCategoryDTO(Const.CRM.Product.NotFoundProductCategory));
            mockRepository
                .Setup(x => x.ProductCategoryIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.DeleteProductCategory(2);

            // Assert
            result.Should().BeOfType<EmptyProductCategoryDTO>();
            ((EmptyProductCategoryDTO)result).Message.Should().Be(Const.CRM.Product.NotFoundProductCategory);
        }

        [Fact]
        public async Task DeleteProductCategory_ShouldNotDeleteProductCategory_WhenProductCategoryIsUsed()
        {
            // Arrange
            var mockRepository = new Mock<IProductCategoryRepository>();
            mockRepository
                .Setup(x => x.DeleteProductCategoryAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedProductCategoryDTO());
            mockRepository
                .Setup(x => x.ProductCategoryIsUsed(It.IsAny<int>()))
                .ReturnsAsync(true);
            var service = new ProductCategoryService(mockRepository.Object);

            // Act
            var result = await service.DeleteProductCategory(2);

            // Assert
            result.Should().BeOfType<ErrorProductCategoryDTO>();
            ((ErrorProductCategoryDTO)result).Message.Should().Be(Const.CRM.Product.UsedProductCategory);
        }
    }
}
