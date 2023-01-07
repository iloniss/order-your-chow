using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("ProductCategoryRepository")]
    public class DeleteProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async void DeleteProductCategoryAsync_ShouldDeleteProductCategory_WhenGivenProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductCategoryAsync(productCategory.ProductCategoryId);

            // Assert
            Assert.IsType<DeletedProductCategoryDTO>(result);
        }

        [Fact]
        public async void DeleteProductCategoryAsync_ShouldNotDeleteProductCategory_WhenGivenInvalidProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductCategoryAsync(productCategory.ProductCategoryId + 1);

            // Assert
            Assert.IsType<EmptyProductCategoryDTO>(result);
        }
    }
}
