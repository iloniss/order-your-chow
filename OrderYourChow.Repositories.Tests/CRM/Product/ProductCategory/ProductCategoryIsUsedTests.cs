using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class ProductCategoryIsUsedTests : ProductCategoryBase
    {
        [Fact]
        public async Task ProductCategoryIsUsedAsync_ShouldReturnTrue_WhenGivenProductCategoryIdUsedInProduct()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SProducts.Add(new SProduct() { Category = productCategory, Name = "Whisky" });
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.ProductCategoryIsUsed(productCategory.ProductCategoryId);

            // Assert
            result.Should().BeTrue();

            // Clean
            OrderYourChowContext.RemoveRange(OrderYourChowContext.SProducts);
            Clear();
        }

        [Fact]
        public async Task ProductCategoryIsUsedAsync_ShouldReturnFalse_WhenGivenProductCategoryIdNotUsedInProduct()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.ProductCategoryIsUsed(productCategory.ProductCategoryId);

            // Assert
            result.Should().BeFalse();

            // Clean
            Clear();
        }
    }
}
