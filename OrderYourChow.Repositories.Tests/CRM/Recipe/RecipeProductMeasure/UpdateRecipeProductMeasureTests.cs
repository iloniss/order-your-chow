using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    [Collection("Repository")]
    public class UpdateRecipeProductMeasureTests : RecipeProductMeasureBase
    {
        [Fact]
        public async Task UpdateRecipeProductMeasureAsync_ShouldUpdateRecipeProductMeasure_WhenGivenRecipeProductMeasure()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateRecipeProductMeasureAsync(new RecipeProductMeasureDTO() { ProductMeasureId = productMeasure.ProductMeasureId, Name = "gram" });
            var updatedRecipeProductMeasure = OrderYourChowContext.SProductMeasures.SingleOrDefault();

            // Assert
            result.Should().BeOfType<UpdatedRecipeProductMeasureDTO>();
            updatedRecipeProductMeasure.Name.Should().Be("gram");

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateRecipeProductMeasureAsync_ShouldNotUpdateRecipeProductMeasure_WhenGivenInvalidRecipeProductMeasure()
        {
            // Arrange
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            // Act
            var repository = new RecipeProductMeasureRepository(OrderYourChowContext, Mapper);
            var result = await repository.UpdateRecipeProductMeasureAsync(new RecipeProductMeasureDTO() { ProductMeasureId = productMeasure.ProductMeasureId + 1, Name = "gram" });

            // Assert
            result.Should().BeOfType<EmptyRecipeProductMeasureDTO>();
            (result as EmptyRecipeProductMeasureDTO).Message.Should().Be(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            // Clean
            Clear();
        }
    }
}
