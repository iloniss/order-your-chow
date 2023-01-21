using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class DeleteProductTests : ProductBase
    {
        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct_WhenGivenValidProductId()
        {
            // Arrange
            var image = Guid.NewGuid().ToString(); ;
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductAsync(product.ProductId);

            // Assert
            result.Should().BeOfType<DeletedProductDTO>();
            result.Image.Should().Be(image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldNotDeleteProduct_WhenGivenInvalidProductId()
        {
            // Arrange
            var image = Guid.NewGuid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteProductAsync(product.ProductId + 1);

            // Assert
            result.Should().BeOfType<EmptyProductDTO>();
            (result as EmptyProductDTO).Message.Should().Be(CORE.Const.CRM.Product.NotFoundProduct);

            // Clean
            Clear();
       }
    }
}
