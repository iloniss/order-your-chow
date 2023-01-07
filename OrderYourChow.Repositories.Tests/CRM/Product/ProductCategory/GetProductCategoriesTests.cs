using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("ProductCategoryRepository")]
    public class GetProductCategoriesTests : ProductCategoryBase
    {

        [Fact]
        public async void GetProductCategoriesAsync_ShouldReturnProductCategories()
        {
            // Arrange
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Entrees" });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoriesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Beverages", result[0].Name);
            Assert.Equal("Entrees", result[1].Name);

            //Clean
            Clear();
        }

        [Fact]
        public async void GetProductCategoriesAsync_ShouldReturnProductCategories_InAscendingOrder()
        {
            // Arrange
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Entrees" });
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoriesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Beverages", result[0].Name);
            Assert.Equal("Entrees", result[1].Name);

            //Clean
            Clear();
        }

        [Fact]
        public async void GetProductCategoriesAsync_ShouldNotReturnProductCategories()
        {
            // Arrange

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoriesAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
