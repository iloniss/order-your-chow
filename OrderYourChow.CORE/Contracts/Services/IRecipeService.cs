using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.Services
{
    public interface IRecipeService : IScopedService
    {
        Task<bool> SaveProducts(int recipeId, List<RecipeProductDTO> recipeProductDTOs);
        Task<RecipeDTO> DeleteRecipe(int recipeId);
        Task<RecipeDTO> UpdateRecipe(IFormFile imageFile, RecipeDTO recipeDTO);

        Task<IList<RecipeListDTO>> GetRecipes(bool? isActive);
        Task<IList<RecipeCategoryDTO>> GetRecipeCategories();
    }
}
