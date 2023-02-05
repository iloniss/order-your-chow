using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeProductMeasureRepository : IScopedRepository
    {
        Task<RecipeProductMeasureDTO> AddRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<IList<RecipeProductMeasureDTO>> GetRecipeProductMeasuresAsync();
        Task<RecipeProductMeasureDTO> GetRecipeProductMeasureAsync(GetRecipeProductMeasureQuery getRecipeProductMeasureQuery);
        Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasureAsync(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<bool> RecipeProductMeasureIsUsed(int recipeProductMeasureId);
    }
}
    