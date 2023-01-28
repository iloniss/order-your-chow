using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class AddRecipeProductMeasureTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task AddRecipeProductMeasureAsync_ShouldAddRecipeProductMeasure()
        {
            // Arrange
            var recipeProductMeasureDTO = new RecipeProductMeasureDTO
            {
                Name = "kilogram"
            };

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.AddRecipeProductMeasureAsync(recipeProductMeasureDTO);
            var entityAdded = OrderYourChowContext.SProductMeasures.SingleOrDefault();

            // Assert
            result.Should().BeOfType<CreatedRecipeProductMeasureDTO>();
            entityAdded.Name.Should().Be(recipeProductMeasureDTO.Name);

            // Clean
            Clear();
        }
    }
}
