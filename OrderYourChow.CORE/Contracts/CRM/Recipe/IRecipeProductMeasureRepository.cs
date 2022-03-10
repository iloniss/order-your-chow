using OrderYourChow.CORE.Models.CRM.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Recipe
{
    public interface IRecipeProductMeasureRepository
    {
        Task<RecipeProductMeasureDTO> AddProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO);
        Task<List<RecipeProductMeasureDTO>> GetProductMeasureAsync();
        Task<RecipeProductMeasureDTO> GetProductMeasureByIdAsync(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> DeleteProductMeasureAsync(int recipeProductMeasureId);
        Task<RecipeProductMeasureDTO> UpdateProductMeasureAsync(int recipeProductMeasureId, RecipeProductMeasureDTO recipeProductMeasureDTO);
    }
}
    