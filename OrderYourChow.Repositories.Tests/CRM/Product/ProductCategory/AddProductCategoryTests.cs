using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    [Collection("Repository")]
    public class AddProductCategoryTests : ProductCategoryBase
    {
        [Fact]
        public async Task AddProductCategoryAsync_ShouldAddProductCategory()
        {
            // Arrange
            var productCategoryDTO = new ProductCategoryDTO
            {
                Name = "Beverages"
            };

            // Act
            var repository = new ProductCategoryRepository(OrderYourChowContext, Mapper);
            var result = await repository.AddProductCategoryAsync(productCategoryDTO);
            var entityAdded = OrderYourChowContext.SProductCategories.SingleOrDefault();

            // Assert
            result.Should().BeOfType<CreatedProductCategoryDTO>();
            entityAdded.Name.Should().Be(productCategoryDTO.Name);

            // Clean
            Clear();
        }
    }
}
