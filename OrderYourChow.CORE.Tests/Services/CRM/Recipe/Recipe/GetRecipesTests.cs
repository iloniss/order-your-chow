using FileProcessor.CORE.Services;
using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.Recipe
{
    public class GetRecipesTests
    {
        [Fact]
        public async Task GetRecipeCategories_ShouldReturnRecipeCategories_WhenRecipeCategoriesExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeRepository>();
            mockRepository
                .Setup(x => x.GetRecipesAsync(It.IsAny<bool?>()))
                .ReturnsAsync(new List<RecipeListDTO>() {
                    new RecipeListDTO() { Name = "Pancakes", RecipeId = 2 },
                    new RecipeListDTO() { Name = "Scrambled eggs", RecipeId = 3 }
                });
            var service = new RecipeService(mockRepository.Object, new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetRecipes(null);

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).ToList().Should().Contain(new List<string> { "Pancakes", "Scrambled eggs" });
        }

        [Fact]
        public async Task GetRecipeCategories_ShouldNotReturnRecipeCategories_WhenRecipeCategoriesNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeRepository>();
            mockRepository
                .Setup(x => x.GetRecipesAsync(It.IsAny<bool?>()))
                .ReturnsAsync(new List<RecipeListDTO>());

            var service = new RecipeService(mockRepository.Object, new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetRecipes(null);

            //Assert
            result.Should().BeEmpty();
        }
    }
}