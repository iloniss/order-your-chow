using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeProductMeasureService : IScopedService
    {
        Task<RecipeProductMeasureDTO> GetRecipeProductMeasureById(int recipeProductMeasureId);
        Task<IList<RecipeProductMeasureDTO>> GetRecipeProductMeasures();
        Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasure(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<RecipeProductMeasureDTO> AddRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO);
    }
}
