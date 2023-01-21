using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class UpdateProductTests : ProductBase
    {
        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct_WhenGivenProduct()
        {
            // Arrange
            var image = Guid.NewGuid().ToString();
            SProductCategory productCategory = new() { Name = "Beverages" };
            SProductCategory newProductCategory = new() { Name = "Drinks" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            OrderYourChowContext.SProductCategories.Add(newProductCategory);
            SProduct product = new() { Name = "Coffee", Category = productCategory, Image = image };
            OrderYourChowContext.SProducts.Add(product);
            OrderYourChowContext.SaveChanges();
            var newImage = Guid.NewGuid().ToString();

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateProductAsync(new ProductDTO() { ProductId = product.ProductId, ProductCategoryId = newProductCategory.ProductCategoryId, Name = "Alcohols", Image = newImage });
            var updatedProduct = OrderYourChowContext.SProducts.SingleOrDefault();

            // Assert
            result.Should().BeOfType<UpdatedProductDTO>();
            updatedProduct.Name.Should().Be("Alcohols");
            updatedProduct.CategoryId.Should().Be(newProductCategory.ProductCategoryId);
            updatedProduct.Category.Name.Should().Be(newProductCategory.Name);
            updatedProduct.Image.Should().Be(newImage);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldNotUpdateProduct_WhenGivenInvalidProduct()
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
            var result = await repository.UpdateProductAsync(new ProductDTO() { ProductId = product.ProductId + 1, ProductCategoryId = productCategory.ProductCategoryId, Name = "Alcohols", Image = image });
            var updatedProduct = OrderYourChowContext.SProducts.SingleOrDefault();

            // Assert
            result.Should().BeOfType<EmptyProductDTO>();
            (result as EmptyProductDTO).Message.Should().Be(CORE.Const.CRM.Product.NotFoundProduct);

            // Clean
            Clear();
        }
    }
}
