using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class GetProductsTests : ProductBase
    {
        [Fact]
        public async Task GetProductsAsync_ShouldReturnProducts_InAscendingOrder()
        {
            // Arrange
            string image = Guid.NewGuid().ToString();
            var productCategory = new SProductCategory { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SProducts.Add(new SProduct { Name = "Tea", Category = productCategory, Image = image });
            OrderYourChowContext.SProducts.Add(new SProduct { Name = "Coffee", Category = productCategory, Image = image });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductsAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).Should().Contain(new List<string>() { "Coffee", "Tea" });
            result.Select(x => x.Name).Should().BeInAscendingOrder();
            result.Select(x => x.ProductCategoryId).Should().OnlyContain(x => x == productCategory.ProductCategoryId);
            result.Select(x => x.ProductCategory).Should().OnlyContain(x => x == productCategory.Name);
            result.Select(x => x.Image).Should().OnlyContain(x => x == image);
   
            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductsAsync_ShouldNotReturnProducts()
        {
            // Arrange
            // Act

            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductsAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
