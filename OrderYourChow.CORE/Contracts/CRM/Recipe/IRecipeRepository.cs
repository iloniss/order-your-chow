using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeRepository : IScopedRepository
    {
        Task<IList<RecipeListDTO>> GetRecipesAsync(bool? isActive);
        Task<RecipeDTO> GetRecipeAsync(GetRecipeQuery getRecipeQuery);
        Task<RecipeDTO> DeleteRecipeAsync(int recipeId);
        Task<IList<RecipeCategoryDTO>> GetRecipeCategoriesAsync();
        Task<RecipeProductListDTO> GetRecipeProductsAsync(int recipeId);
        Task<RecipeDTO> AddRecipeAsync(RecipeDTO recipeDTO);
        Task<bool> SaveProductsAsync(int recipeId, IEnumerable<RecipeProductDTO> newRecipeProducts,
            IEnumerable<RecipeProductDTO> updatedRecipeProducts, IEnumerable<RecipeProductDTO> deletedRecipeProducts);
        Task<bool> UpdateDescriptionAsync(RecipeDescriptionDTO recipeDescriptionDTO);
        Task<RecipeDTO> UpdateRecipeAsync(RecipeDTO recipeDTO);
        Task<bool> AddImagesAsync(int recipeId, List<string> images);
        Task<bool> RecipeIsUsed(int recipeId);
    }
}
