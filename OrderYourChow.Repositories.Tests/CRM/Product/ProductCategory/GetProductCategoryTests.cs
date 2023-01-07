using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;
using OrderYourChow.Repositories.Tests.Shared;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("ProductCategoryRepository")]
    public class GetProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async void GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            GetProductCategoryQuery query = new(productCategoryId: productCategory.ProductCategoryId);

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoryAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Beverages", result.Name);

            //Clean
            Clear();
        }

        [Fact]
        public async void GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductCategoryId()
        {
            // Arrange
            var options = new TestOrderYourChowContext().GetTestContextOptions();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            GetProductCategoryQuery query = new(productCategoryId: productCategory.ProductCategoryId + 1);

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoryAsync(query);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductName()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            GetProductCategoryQuery query = new(name: "Beverages");

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoryAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productCategory.ProductCategoryId, result.ProductCategoryId);
            Assert.Equal(productCategory.Name, result.Name);
        }

        [Fact]
        public async void GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductCategoryName()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            GetProductCategoryQuery query = new(name: "InvalidBeverages");

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoryAsync(query);

            // Assert
            Assert.Null(result);
        }
    }
}
