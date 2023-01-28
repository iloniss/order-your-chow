using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class UpdateProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async Task UpdateProductCategoryAsync_ShouldUpdateProductCategory_WhenGivenProductCategory()
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
            result.Should().BeOfType<UpdatedProductCategoryDTO>();
            updatedProductCategory.Name.Should().Be("Alcohols");

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_ShouldNotUpdateProductCategory_WhenGivenInvalidProductCategory()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateProductCategoryAsync(new ProductCategoryDTO() { ProductCategoryId = productCategory.ProductCategoryId + 1, Name = "Alcohols" });

            // Assert
            result.Should().BeOfType<EmptyProductCategoryDTO>();
            (result as EmptyProductCategoryDTO).Message.Should().Be(CORE.Const.CRM.Product.NotFoundProductCategory);

            // Clean
            Clear();
        }
    }
}
