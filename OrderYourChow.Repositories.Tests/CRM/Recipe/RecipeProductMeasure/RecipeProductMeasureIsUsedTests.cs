using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class RecipeProductMeasureIsUsedTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task RecipeProductMeasureIsUsedAsync_ShouldReturnTrue_WhenGivenRecipeProductMeasureIdUsedInProduct()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "liter" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            SProductCategory productCategory = new() { Name = "Beverages" };
            OrderYourChowContext.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky" };
            OrderYourChowContext.SProducts.Add(product);
            SRecipeCategory recipeCategory = new() { Name = "Drink" };
            OrderYourChowContext.SRecipeCategories.Add(recipeCategory);
            DRecipe recipe = new() { Name = "Whiskey sour", Category = recipeCategory };
            OrderYourChowContext.DRecipes.Add(recipe);
            DRecipeProduct recipeProduct = new() { Recipe = recipe, Product = product, Count = 1, ProductMeasure = productMeasure };
            OrderYourChowContext.DRecipeProducts.Add(recipeProduct);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.RecipeProductMeasureIsUsed(productMeasure.ProductMeasureId);

            // Assert
            result.Should().BeTrue();

            // Clean
            OrderYourChowContext.RemoveRange(OrderYourChowContext.DRecipeProducts);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.DRecipes);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.SRecipeCategories);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.SProducts);
            OrderYourChowContext.RemoveRange(OrderYourChowContext.SProductCategories);
            Clear();
        }

        [Fact]
        public async Task RecipeProductMeasureIsUsedAsync_ShouldReturnFalse_WhenGivenRecipeProductMeasureIdNotUsedInProduct()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "liter" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.RecipeProductMeasureIsUsed(productMeasure.ProductMeasureId);

            // Assert
            result.Should().BeFalse();

            // Clean
            Clear();
        }
    }
}
