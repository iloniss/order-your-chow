using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Product;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    [Collection("Repository")]
    public class ProductIsUsedTests : ProductBase
    {
        [Fact]
        public async Task ProductIsUsedAsync_ShouldReturnTrue_WhenGivenProductUsedInRecipe()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky" };
            OrderYourChowContext.SProducts.Add(product);
            SRecipeCategory recipeCategory = new() { Name = "Drink" };
            OrderYourChowContext.SRecipeCategories.Add(recipeCategory);
            DRecipe recipe = new() { Name = "Whiskey sour", Category = recipeCategory };
            OrderYourChowContext.DRecipes.Add(recipe);
            SProductMeasure productMeasure = new() { Name = "Glass" };
            DRecipeProduct recipeProduct = new() { Recipe = recipe, Product = product, Count = 1, ProductMeasure = productMeasure };
            OrderYourChowContext.DRecipeProducts.Add(recipeProduct);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.ProductIsUsed(product.ProductId);

            // Assert
            result.Should().BeTrue();

            // Clean
            OrderYourChowContext.RemoveRange(OrderYourChowContext.DRecipeProducts);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.DRecipes);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.SRecipeCategories);
            Clear();
        }

        [Fact]
        public async Task ProductIsUsedAsync_ShouldReturnFalse_WhenGivenProductNotUsedInRecipe()
        {
            // Arrange
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky" };
            OrderYourChowContext.SProducts.Add(new SProduct() { Category = productCategory, Name = "Whisky" });
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new ProductRepository(OrderYourChowContext, Mapper);
            var result = await repository.ProductIsUsed(product.ProductId);

            // Assert
            result.Should().BeFalse();

            // Clean
            Clear();
        }
    }
}
