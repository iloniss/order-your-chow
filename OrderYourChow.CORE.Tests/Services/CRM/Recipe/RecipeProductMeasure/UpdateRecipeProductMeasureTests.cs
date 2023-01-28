using FluentAssertions;
using Moq;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.CORE.Services.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Tests.Services.CRM.Recipe.RecipeProductMeasure
{
    public class UpdateRecipeProductMeasureTests
    {
        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldUpdateRecipeProductMeasure_WhenRecipeProductMeasureWithEqualNameNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new UpdatedRecipeProductMeasureDTO());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.UpdateRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedRecipeProductMeasureDTO>();
        }

        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldNotUpdateRecipeProductMeasure_WhenRecipeProductMeasureNotExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(value: null);
            mockRepository
                .Setup(x => x.UpdateRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new EmptyRecipeProductMeasureDTO(Const.CRM.Recipe.NotFoundRecipeProductMeasure));
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.UpdateRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 2 });

            // Assert
            result.Should().BeOfType<EmptyRecipeProductMeasureDTO>();
            ((EmptyRecipeProductMeasureDTO)result).Message.Should().Be(Const.CRM.Recipe.NotFoundRecipeProductMeasure);
        }



        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldNotUpdateRecipeProductMeasure_WhenRecipeProductMeasureExists()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 3 });
            mockRepository
                .Setup(x => x.UpdateRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new UpdatedRecipeProductMeasureDTO());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.UpdateRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 2 });

            // Assert
            result.Should().BeOfType<ErrorRecipeProductMeasureDTO>();
            ((ErrorRecipeProductMeasureDTO)result).Message.Should().Be(Const.CRM.Recipe.ExistedRecipeProductMeasure);
        }

        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldUpdateRecipeProductMeasure_WhenRecipeProductMeasureIsEqual()
        {
            // Arrange
            var mockRepository = new Mock<IRecipeProductMeasureRepository>();
            mockRepository
                .Setup(x => x.GetRecipeProductMeasureAsync(It.IsAny<GetRecipeProductMeasureQuery>()))
                .ReturnsAsync(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 2 });
            mockRepository
                .Setup(x => x.UpdateRecipeProductMeasureAsync(It.IsAny<RecipeProductMeasureDTO>()))
                .ReturnsAsync(new UpdatedRecipeProductMeasureDTO());
            var service = new RecipeProductMeasureService(mockRepository.Object);

            // Act
            var result = await service.UpdateRecipeProductMeasure(new RecipeProductMeasureDTO { Name = "kilogram", ProductMeasureId = 2 });

            // Assert
            result.Should().BeOfType<UpdatedRecipeProductMeasureDTO>();
        }
    }
}
