using FluentAssertions;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class GetProductTests : ProductBase
    {
        [Fact]
        public async Task GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductId()
        {
            // Arrange
            var image = Guid.NewGuid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            GetProductQuery query = new() { ProductId = product.ProductId};

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Coffee");
            result.ProductCategoryId.Should().Be(productCategory.ProductCategoryId);
            result.ProductCategory.Should().Be(productCategory.Name);
            result.Image.Should().Be(image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductId()
        {
            // Arrange
            var image = Guid.NewGuid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            GetProductQuery query = new() { ProductId = product.ProductId + 1 };

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductAsync(query);

            // Assert
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductName()
        {
            // Arrange
            var image = new Guid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            GetProductQuery query = new() { Name = "Coffee" };

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Coffee");
            result.ProductCategoryId.Should().Be(productCategory.ProductCategoryId);
            result.ProductCategory.Should().Be(productCategory.Name);
            result.Image.Should().Be(image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductName()
        {
            // Arrange
            var image = new Guid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            GetProductQuery query = new() { Name = "InvalidCoffee" };

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductAsync(query);

            // Assert
            result.Should().BeNull();

            // Clean
            Clear();
        }
    }
}
