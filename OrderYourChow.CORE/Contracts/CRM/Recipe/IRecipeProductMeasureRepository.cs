using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeProductMeasureRepository
    {
        Task<RecipeProductMeasureDTO> AddRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<List<RecipeProductMeasureDTO>> GetRecipeProductMeasuresAsync();
        Task<RecipeProductMeasureDTO> GetRecipeProductMeasureAsync(GetRecipeProductMeasureQuery getRecipeProductMeasureQuery);
        Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasureAsync(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<bool> RecipeProductMeasureIsUsed(int recipeProductMeasureId);
    }
}
    