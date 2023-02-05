using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Contracts.Services;
using OrderYourChow.CORE.Enums;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;

namespace OrderYourChow.CORE.Services.CRM.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileProcessorValidator _fileProcessorValidator;

        public RecipeService(IRecipeRepository recipeRepository, IFileProcessor fileProcessor, IFileProcessorValidator fileProcessorValidator)
        {
            _recipeRepository = recipeRepository;
            _fileProcessor = fileProcessor;
            _fileProcessorValidator = fileProcessorValidator;
        }

        public async Task<IList<RecipeCategoryDTO>> GetRecipeCategories() =>
            await _recipeRepository.GetRecipeCategoriesAsync();

        public async Task<IList<RecipeListDTO>> GetRecipes(bool? isActive) =>
            await _recipeRepository.GetRecipesAsync(isActive);

        public async Task<bool> SaveProducts(int recipeId, List<RecipeProductDTO> recipeProductDTOs)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(new GetRecipeQuery { RecipeId = recipeId });
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

        public async Task<RecipeDTO> UpdateRecipe(IFormFile imageFile, RecipeDTO recipeDTO)
        {
            if (imageFile != null && !_fileProcessorValidator.IsImageFile(imageFile))
            {
                return new ErrorRecipeDTO(Const.Shared.Global.InvalidFile);
            }

            var existedRecipe = await _recipeRepository.GetRecipeAsync(new GetRecipeQuery { Name = recipeDTO.Name });

            if (existedRecipe == null || existedRecipe.RecipeId == recipeDTO.RecipeId)
            {
                if (imageFile != null)
                {
                    _fileProcessor.DeleteFile(recipeDTO.MainImage, Const.Shared.Global.RecipeImagesPath);
                    recipeDTO.MainImage = await _fileProcessor.SaveFileFromWebsite(imageFile, Const.Shared.Global.RecipeImagesPath);
                }

                var result = await _recipeRepository.UpdateRecipeAsync(recipeDTO);

                return result;
            }
            return new ErrorRecipeDTO(Const.CRM.Recipe.ExistedRecipe);
        }
    }
}