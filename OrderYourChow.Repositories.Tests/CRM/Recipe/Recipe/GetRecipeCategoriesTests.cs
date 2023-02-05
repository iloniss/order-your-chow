using FluentAssertions;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.Recipe
{
    [Collection("Repository")]
    public class GetRecipeCategoriesTests : RecipeBase
    {
        [Fact]
        public async Task GetRecipeCategoriesAsync_ShouldReturnRecipeCategories_InAscendingOrderByRecipeCategoryId()
        {
            // Arrange
            OrderYourChowContext.SRecipeCategories.Add(new SRecipeCategory { Name = "dinner" });
            OrderYourChowContext.SRecipeCategories.Add(new SRecipeCategory { Name = "breakfast" });
            OrderYourChowContext.SRecipeCategories.Add(new SRecipeCategory { Name = "lunch" });
            OrderYourChowContext.SaveChanges();

            // Act

            var repository = new RecipeRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeCategoriesAsync();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);
            result.Select(x => x.Name).Should().Contain(new List<string>() { "dinner", "breakfast", "lunch" });
            result.Select(x => x.RecipeCategoryId).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeCategoriesAsync_ShouldNotReturnRecipeCategories()
        {
            // Arrange
            // Act
            var repository = new RecipeRepository(OrderYourChowContext, Mapper);
            var result = await repository.GetRecipeCategoriesAsync();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
