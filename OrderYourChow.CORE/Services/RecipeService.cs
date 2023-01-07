using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Contracts.Services;
using OrderYourChow.CORE.Enums;
using OrderYourChow.CORE.Models.Shared.Recipe;

namespace OrderYourChow.CORE.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<bool> SaveProductsAsync(int recipeId, List<RecipeProductDTO> recipeProductDTOs)
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
    }
}
