using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeRepository : IScopedRepository
    {
        Task<List<RecipeListDTO>> GetRecipesAsync(bool? isActive);
        Task<Models.CRM.Recipe.RecipeDTO> GetRecipeAsync(int recipeId);
        Task<List<RecipeCategoryDTO>> GetRecipeCategoriesAsync();
        Task<RecipeProductListDTO> GetRecipeProductsAsync(int recipeId);
        Task<Models.CRM.Recipe.RecipeDTO> AddRecipeAsync(Models.CRM.Recipe.RecipeDTO recipeDTO);
        Task<bool> SaveProductsAsync(int recipeId, IEnumerable<RecipeProductDTO> newRecipeProducts,
            IEnumerable<RecipeProductDTO> updatedRecipeProducts, IEnumerable<RecipeProductDTO> deletedRecipeProducts);
        Task<bool> AddDescriptionAsync(RecipeDescriptionDTO recipeDescriptionDTO);
        Task<bool> AddImagesAsync(int recipeId, List<string> images);
    }
}
