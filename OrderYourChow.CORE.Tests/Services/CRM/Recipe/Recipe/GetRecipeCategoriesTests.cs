using FileProcessor.CORE.Services;
using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.Recipe
{
    public class GetRecipeCategoriesTests
    {
        [Fact]
        public async Task GetRecipeCategories_ShouldReturnRecipeCategories_WhenRecipeCategoriesExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeRepository>();
            mockRepository
                .Setup(x => x.GetRecipeCategoriesAsync())
                .ReturnsAsync(new List<RecipeCategoryDTO>() {
                    new RecipeCategoryDTO() { Name = "Breakfast", RecipeCategoryId = 2 },
                    new RecipeCategoryDTO() { Name = "Dinner", RecipeCategoryId = 3 }
                });
            var service = new RecipeService(mockRepository.Object, new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetRecipeCategories();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).ToList().Should().Contain(new List<string> { "Breakfast", "Dinner" });
        }

        [Fact]
        public async Task GetRecipeCategories_ShouldNotReturnRecipeCategories_WhenRecipeCategoriesNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeRepository>();
            mockRepository
                .Setup(x => x.GetRecipeCategoriesAsync())
                .ReturnsAsync(new List<RecipeCategoryDTO>());
            var service = new RecipeService(mockRepository.Object, new Mock<IFileProcessor>().Object,
                new Mock<IFileProcessorValidator>().Object);

            //Act
            var result = await service.GetRecipeCategories();

            //Assert
            result.Should().BeEmpty();
        }
    }
}