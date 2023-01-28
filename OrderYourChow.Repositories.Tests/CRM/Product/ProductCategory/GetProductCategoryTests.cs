using FluentAssertions;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class GetProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async Task GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductCategoryId()
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
            result.Should().NotBeNull();
            result.Name.Should().Be("Beverages");

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductCategoryId()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            GetProductCategoryQuery query = new(productCategoryId: productCategory.ProductCategoryId + 1);

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetProductCategoryAsync(query);

            // Assert
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldReturnProductCategory_WhenGivenProductName()
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
            result.Should().NotBeNull();
            result.Name.Should().Be(productCategory.Name);
            result.ProductCategoryId.Should().Be(productCategory.ProductCategoryId);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategoryAsync_ShouldNotReturnProductCategory_WhenGivenInvalidProductCategoryName()
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
            result.Should().BeNull();

            // Clean
            Clear();
        }
    }
}
