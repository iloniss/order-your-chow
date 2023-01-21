using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class DeleteProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async Task DeleteProductCategoryAsync_ShouldDeleteProductCategory_WhenGivenValidProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductCategoryAsync(productCategory.ProductCategoryId);

            // Assert
            result.Should().BeOfType<DeletedProductCategoryDTO>();
        }

        [Fact]
        public async Task DeleteProductCategoryAsync_ShouldNotDeleteProductCategory_WhenGivenInvalidProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductCategoryAsync(productCategory.ProductCategoryId + 1);

            // Assert
            result.Should().BeOfType<EmptyProductCategoryDTO>();
            (result as EmptyProductCategoryDTO).Message.Should().Be(CORE.Const.CRM.Product.NotFoundProductCategory);

            // Clean
            Clear();
        }
    }
}
