using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.RecipeProductMeasure
{
    public class AddRecipeProductMeasureTests
    {
        [Fact]
        public async Task AddRecipeProductMeasure_ShouldAddRecipeProductMeasure_WhenRecipeProductMeasureNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.AddRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new CreatedRecipeProductMeasureDTO());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.AddRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram" });

            // Assert
            result.Should().BeOfType<CreatedRecipeProductMeasureDTO>();
        }

        [Fact]
        public async Task AddRecipeProductMeasure_ShouldNotAddRecipeProductMeasure_WhenRecipeProductMeasureExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(new RecipeProductMeasureDTO() { Name = "kilogram", ProductMeasureId = 2 });
            mockRepository
                .Setup(x => x.AddRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new CreatedRecipeProductMeasureDTO());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.AddRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram" });

            // Assert
            result.Should().BeOfType<ErrorRecipeProductMeasureDTO>();
            ((ErrorRecipeProductMeasureDTO)result).Message.Should().Be(Const.CRM.Recipe.ExistedRecipeProductMeasure);
        }
    }
}
