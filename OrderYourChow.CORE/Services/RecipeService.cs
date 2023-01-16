using FileProcessor.CORE.Services;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Contracts.Services;
using OrderYourChow.CORE.Enums;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;

namespace OrderYourChow.CORE.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileProcessor _fileProcessor;
        public RecipeService(IRecipeRepository recipeRepository, IFileProcessor fileProcessor)
        {
            _recipeRepository = recipeRepository;
            _fileProcessor = fileProcessor;
        }

        public async Task<bool> SaveProducts(int recipeId, List<RecipeProductDTO> recipeProductDTOs)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            if (recipe == null)
                return false;

            var recipeProducts = await _recipeRepository.GetRecipeProductsAsync(recipeId);

            var newRecipeProducts = recipeProductDTOs.Where(x => x.Status == RecipeProductStatus.New);
            var updatedRecipeProducts = recipeProductDTOs.Where(x => x.Status == RecipeProductStatus.Updated);
            var deletedRecipeProducts = recipeProducts.RecipeProductList.Where(y => !recipeProductDTOs.Any(x => x.RecipeProductId == y.RecipeProductId));

            return await _recipeRepository.SaveProductsAsync(recipeId, newRecipeProducts, updatedRecipeProducts, deletedRecipeProducts);
        }

        public async Task<RecipeDTO> DeleteRecipe(int recipeId)
        {
            if (await _recipeRepository.RecipeIsUsed(recipeId))
                return new ErrorRecipeDTO(Const.CRM.Recipe.UsedRecipe);

            var result = await _recipeRepository.DeleteRecipeAsync(recipeId);

            if (result is not EmptyRecipeDTO)
            _fileProcessor.DeleteFile(result.MainImage, Const.Shared.Global.RecipeImagesPath);

            return result;
        }

    }
}
