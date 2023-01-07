using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("ProductCategoryRepository")]
    public class UpdateProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async void UpdateProductCategoryAsync_ShouldUpdateProductCategory_WhenGivenProductCategory()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateProductCategoryAsync(new ProductCategoryDTO() { ProductCategoryId = productCategory.ProductCategoryId, Name = "Alcohols" });
            var updatedProductCategory = OrderYourChowContext.SProductCategories.SingleOrDefault();
            // Assert
            Assert.IsType<UpdatedProductCategoryDTO>(result);
            Assert.Equal("Alcohols", updatedProductCategory.Name);
        }

        [Fact]
        public async void UpdateProductCategoryAsync_ShouldNotUpdateProductCategory_WhenGivenInvalidProductCategory()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateProductCategoryAsync(new ProductCategoryDTO() { ProductCategoryId = productCategory.ProductCategoryId + 1, Name = "Alcohols" });

            // Assert
            Assert.IsType<EmptyProductCategoryDTO>(result);
        }
    }
}
