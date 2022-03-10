using OrderYourChow.CORE.Models.API.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeRepository
    {
        Task<List<RecipeListDTO>> GetRecipesAsync();
        Task<List<RecipeCategoryDTO>> GetRecipeCategoriesAsync();
        Task<Models.CRM.Recipe.RecipeDTO> AddRecipeAsync(Models.CRM.Recipe.RecipeDTO recipeDTO);
        Task<bool> AddProductsAsync(int recipeId, List<RecipeProductDTO> recipeProductDTOs);
        Task<bool> AddDescriptionAsync(int recipeId, RecipeDescriptionDTO recipeDescriptionDTO);
        Task<bool> AddImagesAsync(int recipeId, List<string> images);
    }
}
