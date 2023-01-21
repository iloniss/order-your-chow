using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class DeleteRecipeProductMeasureTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task DeleteRecipeProductMeasureAsync_ShouldDeleteRecipeProductMeasure_WhenGivenValidRecipeProductMeasureId()
        {
            // Arrange
            SProductMeasure productMeasure = Seed();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteRecipeProductMeasureAsync(productMeasure.ProductMeasureId);

            // Assert
            result.Should().BeOfType<DeletedRecipeProductMeasureDTO>();
        }

        [Fact]
        public async Task DeleteRecipeProductMeasureAsync_ShouldNotDeleteRecipeProductMeasure_WhenGivenInvalidRecipeProductMeasureId()
        {
            // Arrange
            SProductMeasure productMeasure = Seed();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.DeleteRecipeProductMeasureAsync(productMeasure.ProductMeasureId + 1);

            // Assert
            result.Should().BeOfType<EmptyRecipeProductMeasureDTO>();
            (result as EmptyRecipeProductMeasureDTO).Message.Should().Be(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            // Clean
            Clear();
        }
    }
}
