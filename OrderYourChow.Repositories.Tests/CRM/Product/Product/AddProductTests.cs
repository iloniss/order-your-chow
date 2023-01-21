using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class AddProductTests : ProductBase
    {
        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            //Arrange
            var image = Guid.NewGuid().ToString();
            var productDTO = new AddProductDTO()
            {
                Name = "Coffee",
                ProductCategoryId = OrderYourChowContext.SProductCategories.SingleOrDefault().ProductCategoryId,
                Image = image
            };

            //Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.AddProductAsync(productDTO);
            var entityAdded = OrderYourChowContext.SProducts.SingleOrDefault();

            //Assert
            result.Should().BeOfType<CreatedProductDTO>();
            entityAdded.CategoryId.Should().Be(productDTO.ProductCategoryId);
            entityAdded.Name.Should().Be("Coffee");
            entityAdded.Image.Should().Be(image);
            entityAdded.Category.Should().NotBeNull();

            // Clean
            Clear();
        }
    }
}
