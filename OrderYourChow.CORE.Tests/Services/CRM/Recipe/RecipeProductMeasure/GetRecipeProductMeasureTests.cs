using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.RecipeProductMeasure
{
    public class GetRecipeProductMeasureTests
    {
        [Fact]
        public async Task GetRecipeProductMeasure_ShouldReturnRecipeProductMeasure_WhenRecipeProductMeasureExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(new RecipeProductMeasureDTO() { Name = "kilogram", ProductMeasureId = 2 });
            var service = new RecipeProductMeasureService(mockRepository.Object);

            //Act
            var result = await service.GetRecipeProductMeasureById(2);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("kilogram");
        }

        [Fact]
        public async Task GetRecipeProductMeasure_ShouldNotReturnRecipeProductMeasure_WhenRecipeProductMeasureNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(value: null);
            var service = new RecipeProductMeasureService(mockRepository.Object);

            //Act
            var result = await service.GetRecipeProductMeasureById(2);

            //Assert
            result.Should().BeNull();
        }
    }
}
