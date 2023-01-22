using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.Services
{
    public interface IRecipeService : IScopedService
    {
        Task<bool> SaveProductsAsync(int recipeId, List<RecipeProductDTO> recipeProductDTOs);
    }
}
