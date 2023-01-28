using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.RecipeProductMeasure
{
    public class GetRecipeProductMeasuresTests
    {
        [Fact]
        public async Task GetRecipeProductMeasures_ShouldReturnRecipeProductMeasures_WhenRecipeProductMeasuresExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasuresAsync())
                .ReturnsAsync(new List<RecipeProductMeasureDTO>() {
                    new RecipeProductMeasureDTO() { Name = "kilogram", ProductMeasureId = 2 },
                    new RecipeProductMeasureDTO() { Name = "gram", ProductMeasureId = 3 }
                });
            var service = new RecipeProductMeasureService(mockRepository.Object);

            //Act
            var result = await service.GetRecipeProductMeasures();

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).ToList().Should().Contain(new List<string> { "kilogram", "gram" });
        }

        [Fact]
        public async Task GetRecipeProductMeasures_ShouldNotReturnRecipeProductMeasures_WhenRecipeProductMeasuresNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasuresAsync())
                .ReturnsAsync(new List<RecipeProductMeasureDTO>());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            //Act
            var result = await service.GetRecipeProductMeasures();

            //Assert
            result.Should().BeEmpty();
        }
    }
}
