using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;

namespace OrderYourChow.CORE.Services.CRM.Recipe
{
    public class RecipeProductMeasureService : IRecipeProductMeasureService
    {
        private readonly IRecipeProductMeasureRepository _recipeProductMeasureRepository;
        public RecipeProductMeasureService(IRecipeProductMeasureRepository recipeProductMeasureRepository)
        {
            _recipeProductMeasureRepository = recipeProductMeasureRepository;
        }


        public async Task<RecipeProductMeasureDTO> AddRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var existrecipeProductMeasure = await _recipeProductMeasureRepository.GetRecipeProductMeasureAsync(new GetRecipeProductMeasureQuery(name: recipeProductMeasureDTO.Name));

            if (existrecipeProductMeasure != null)
                return new ErrorRecipeProductMeasureDTO(Const.CRM.Recipe.ExistedRecipeProductMeasure);

            return await _recipeProductMeasureRepository.AddRecipeProductMeasureAsync(recipeProductMeasureDTO);
        }

        public async Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasure(int recipeProductMeasureId)
        {
            if (await _recipeProductMeasureRepository.RecipeProductMeasureIsUsed(recipeProductMeasureId))
                return new ErrorRecipeProductMeasureDTO(Const.CRM.Recipe.UsedRecipeProductMeasure);
            return await _recipeProductMeasureRepository.DeleteRecipeProductMeasureAsync(recipeProductMeasureId);
        }

        public async Task<IList<RecipeProductMeasureDTO>> GetRecipeProductMeasures() =>
            await _recipeProductMeasureRepository.GetRecipeProductMeasuresAsync();

        public async Task<RecipeProductMeasureDTO> GetRecipeProductMeasureById(int recipeProductMeasureId) =>
            await _recipeProductMeasureRepository.GetRecipeProductMeasureAsync(new GetRecipeProductMeasureQuery(recipeProductMeasureId: recipeProductMeasureId));

        public async Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasure(RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var existrecipeProductMeasure = await _recipeProductMeasureRepository.GetRecipeProductMeasureAsync(new GetRecipeProductMeasureQuery(name: recipeProductMeasureDTO.Name));

            if (existrecipeProductMeasure != null && existrecipeProductMeasure.ProductMeasureId != recipeProductMeasureDTO.ProductMeasureId)
                return new ErrorRecipeProductMeasureDTO(Const.CRM.Recipe.ExistedRecipeProductMeasure);

            return await _recipeProductMeasureRepository.UpdateRecipeProductMeasureAsync(recipeProductMeasureDTO);
        }
    }
}
