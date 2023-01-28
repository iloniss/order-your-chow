using FluentAssertions;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class GetRecipeProductMeasureTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task GetRecipeProductMeasureAsync_ShouldReturnRecipeProductMeasure_WhenGivenRecipeProductMeasureId()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            GetRecipeProductMeasureQuery query = new(recipeProductMeasureId: productMeasure.ProductMeasureId);

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasureAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("kilogram");

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasureAsync_ShouldNotReturnRecipeProductMeasure_WhenGivenInvalidRecipeProductMeasureId()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            GetRecipeProductMeasureQuery query = new(recipeProductMeasureId: productMeasure.ProductMeasureId + 1);

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasureAsync(query);

            // Assert
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasureAsync_ShouldReturnRecipeProductMeasure_WhenGivenProductName()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            GetRecipeProductMeasureQuery query = new(name: "kilogram");

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasureAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(productMeasure.Name);
            result.ProductMeasureId.Should().Be(productMeasure.ProductMeasureId);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasureAsync_ShouldNotReturnRecipeProductMeasure_WhenGivenInvalidRecipeProductMeasureName()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            GetRecipeProductMeasureQuery query = new(name: "Invalidkilogram");

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasureAsync(query);

            // Assert
            result.Should().BeNull();

            // Clean
            Clear();
        }
    }
}
