using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("ProductCategoryRepository")]
    public class ProductCategoryIsUsedTests : ProductCategoryBase
    {

        [Fact]
        public async void ProductCategoryIsUsedAsync_ShouldReturnTrue_WhenGivenProductCategoryIdUsedInProduct()
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
            Assert.True(result);

            //Clean
            OrderYourChowContext.Remove(OrderYourChowContext.SProducts.SingleOrDefault());
            OrderYourChowContext.SaveChanges();
        }

        [Fact]
        public async void ProductCategoryIsUsedAsync_ShouldReturnFalse_WhenGivenProductCategoryIdNotUsedInProduct()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.ProductCategoryIsUsed(productCategory.ProductCategoryId);

            // Assert
            Assert.False(result);
        }
    }
}
