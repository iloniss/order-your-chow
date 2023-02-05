using FluentAssertions;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.Recipe
{
    [Collection("Repository")]
    public class GetRecipesTests : RecipeBase
    {
        [Fact]
        public async Task GetRecipeAsync_ShouldReturnRecipeCategories_WhenRecipesExist()
        {
            // Arrange
            var image = Seed();

            // Act
            var result = await GetRecipesAsync(null);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).Should().Contain(new List<string>() { "Scrambled eggs", "Pancakes" });
            result.Select(x => x.Name).Should().BeInAscendingOrder();
            result.Select(x => x.Image).Should().OnlyContain(x => x == image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldReturnActiveRecipeCategories_WhenActiveRecipesExist()
        {
            // Arrange
            var image = Seed();

            // Act
            var result = await GetRecipesAsync(true);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.Select(x => x.Name).Should().Contain("Scrambled eggs");
            result.Select(x => x.Image).Should().OnlyContain(x => x == image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldNotReturnActiveRecipeCategories_WhenActiveRecipesNotExist()
        {
            // Arrange
            _ = Seed(false);

            // Act
            var result = await GetRecipesAsync(true);

            // Assert
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldReturnNotActiveRecipeCategories_WhenNotActiveRecipesExist()
        {
            // Arrange
            var image = Seed();

            // Act
            var result = await GetRecipesAsync(false);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.Select(x => x.Name).Should().Contain("Pancakes");
            result.Select(x => x.Image).Should().OnlyContain(x => x == image);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldNotReturnNotActiveRecipeCategories_WhenNotActiveRecipesNotExist()
        {
            // Arrange
            _ = Seed(true);

            // Act
            var result = await GetRecipesAsync(false);

            // Assert
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeAsync_ShouldNotReturnRecipeCategories_WhenRecipesNotExist()
        {
            // Arrange
            // Act
            var result = await GetRecipesAsync(true);

            // Assert
            result.Should().BeEmpty();
        }

        protected async Task<IList<RecipeListDTO>> GetRecipesAsync(bool? isActive = null)
        {
            var repository = new RecipeRepository(OrderYourChowContext, Mapper);
            return await repository.GetRecipesAsync(isActive);
        }

        protected string Seed(bool? active = null)
        {
            string image = Guid.NewGuid().ToString();
            var recipeCategory = new SRecipeCategory { Name = "Breakfast" };
            OrderYourChowContext.SRecipeCategories.Add(recipeCategory);
            OrderYourChowContext.DRecipes.Add(new DRecipe()
            {
                Category = recipeCategory,
                Active = active ?? true,
                MainImage = image,
                Name = "Scrambled eggs"
            });
            OrderYourChowContext.DRecipes.Add(new DRecipe()
            {
                Category = recipeCategory,
                Active = active ?? false,
                MainImage = image,
                Name = "Pancakes"
            });
            OrderYourChowContext.SaveChanges();

            return image;
        }
    }
}