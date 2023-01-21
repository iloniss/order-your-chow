using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class GetRecipeProductMeasuresTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task GetRecipeProductMeasuresAsync_ShouldReturnRecipeProductMeasures_InAscendingOrder()
        {
            // Arrange
            OrderYourChowContext.SProductMeasures.Add(new SProductMeasure { Name = "kilogram" });
            OrderYourChowContext.SProductMeasures.Add(new SProductMeasure { Name = "gram" });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasuresAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).Should().Contain(new List<string>() { "kilogram", "gram" });
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasuresAsync_ShouldNotReturnRecipeProductMeasures()
        {
            // Arrange
            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeProductMeasuresAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
