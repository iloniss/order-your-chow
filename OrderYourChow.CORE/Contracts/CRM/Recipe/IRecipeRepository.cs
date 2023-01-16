using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeRepository
    {
        Task<List<RecipeListDTO>> GetRecipesAsync(bool? isActive);
        Task<RecipeDTO> GetRecipeAsync(int recipeId);
        Task<RecipeDTO> DeleteRecipeAsync(int recipeId);
        Task<List<RecipeCategoryDTO>> GetRecipeCategoriesAsync();
        Task<RecipeProductListDTO> GetRecipeProductsAsync(int recipeId);
        Task<RecipeDTO> AddRecipeAsync(RecipeDTO recipeDTO);
        Task<bool> SaveProductsAsync(int recipeId, IEnumerable<RecipeProductDTO> newRecipeProducts,
            IEnumerable<RecipeProductDTO> updatedRecipeProducts, IEnumerable<RecipeProductDTO> deletedRecipeProducts);
        Task<bool> AddDescriptionAsync(RecipeDescriptionDTO recipeDescriptionDTO);
        Task<bool> AddImagesAsync(int recipeId, List<string> images);
        Task<bool> RecipeIsUsed(int recipeId);
    }
}
