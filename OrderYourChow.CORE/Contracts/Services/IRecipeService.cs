using OrderYourChow.CORE.Models.Shared.Recipe;

namespace OrderYourChow.CORE.Contracts.Services
{
    public interface IRecipeService
    {
        Task<bool> SaveProductsAsync(int recipeId, List<RecipeProductDTO> recipeProductDTOs);
    }
}
