using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class GetProductCategoriesTests : ProductCategoryBase
    {

        [Fact]
        public async Task GetProductCategoriesAsync_ShouldReturnProductCategories_InAscendingOrder()
        {
            // Arrange
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Entrees" });
            OrderYourChowContext.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoriesAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).Should().Contain(new List<string>() { "Beverages", "Entrees" });
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoriesAsync_ShouldNotReturnProductCategories()
        {
            // Arrange
            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoriesAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
