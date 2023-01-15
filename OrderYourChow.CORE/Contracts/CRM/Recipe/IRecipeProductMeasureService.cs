using OrderYourChow.CORE.Models.CRM.Recipe;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeProductMeasureService
    {
        Task<RecipeProductMeasureDTO> GetRecipeProductMeasureById(int recipeProductMeasureId);
        Task<List<RecipeProductMeasureDTO>> GetRecipeProductMeasure();
        Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasure(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<RecipeProductMeasureDTO> AddRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO);
    }
}
