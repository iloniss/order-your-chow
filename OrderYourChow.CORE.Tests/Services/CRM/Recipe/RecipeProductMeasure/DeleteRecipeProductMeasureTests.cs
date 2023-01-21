using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Recipe;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.RecipeProductMeasure
{
    public class DeleteRecipeProductMeasureTests
    {
        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldDeleteRecipeProductMeasure_WhenRecipeProductMeasureExitsAndIsNotUsed()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.DeleteRecipeProductMeasureAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedRecipeProductMeasureDTO());
            mockRepository
                .Setup(x => x.RecipeProductMeasureIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.DeleteRecipeProductMeasure(2);

            // Assert
            result.Should().BeOfType<DeletedRecipeProductMeasureDTO>();
        }

        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldNotDeleteRecipeProductMeasure_WhenRecipeProductMeasureNotExits()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.DeleteRecipeProductMeasureAsync(It.IsAny<int>()))
                .ReturnsAsync(new EmptyRecipeProductMeasureDTO(Const.CRM.Recipe.NotFoundRecipeProductMeasure));
            mockRepository
                .Setup(x => x.RecipeProductMeasureIsUsed(It.IsAny<int>()))
                .ReturnsAsync(false);
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.DeleteRecipeProductMeasure(2);

            // Assert
            result.Should().BeOfType<EmptyRecipeProductMeasureDTO>();
            ((EmptyRecipeProductMeasureDTO)result).Message.Should().Be(Const.CRM.Recipe.NotFoundRecipeProductMeasure);
        }

        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldNotDeleteRecipeProductMeasure_WhenRecipeProductMeasureIsUsed()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.DeleteRecipeProductMeasureAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeletedRecipeProductMeasureDTO());
            mockRepository
                .Setup(x => x.RecipeProductMeasureIsUsed(It.IsAny<int>()))
                .ReturnsAsync(true);
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.DeleteRecipeProductMeasure(2);

            // Assert
            result.Should().BeOfType<ErrorRecipeProductMeasureDTO>();
            ((ErrorRecipeProductMeasureDTO)result).Message.Should().Be(Const.CRM.Recipe.UsedRecipeProductMeasure);
        }
    }
}
