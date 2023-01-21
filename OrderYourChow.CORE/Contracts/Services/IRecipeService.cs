using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;

namespace OrderYourChow.CORE.Contracts.Services
{
    public interface IRecipeService
    {
        Task<bool> SaveProducts(int recipeId, List<RecipeProductDTO> recipeProductDTOs);
        Task<RecipeDTO> DeleteRecipe(int recipeId);
        Task<RecipeDTO> UpdateRecipe(IFormFile imageFile, RecipeDTO recipeDTO);
    }
}
