using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OrderYourChow.CRM.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileProcessorValidator _fileProcessorValidator;
        public RecipeController(IRecipeRepository recipeRepository, IFileProcessor fileProcessor,
            IFileProcessorValidator fileProcessorValidator)
        {
            _recipeRepository = recipeRepository;
            _fileProcessor = fileProcessor;
            _fileProcessorValidator = fileProcessorValidator;
        }

        [HttpGet("category")]
        public async Task<ActionResult<List<RecipeCategoryDTO>>> GetCategories()
        {
            return Ok(await _recipeRepository.GetRecipeCategoriesAsync());
        }

        //[HttpGet]
        //public async Task<ActionResult<List<RecipeListDTO>>> GetRecipes()
        //{
        //    return Ok(await _recipeRepository.GetRecipesAsync());
        //}

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> Add([Required] IFormFile imageFile, [FromForm] RecipeDTO recipeDTO)
        {
            if (!_fileProcessorValidator.IsImageFile(imageFile))
                return BadRequest();

            recipeDTO.MainImage = await _fileProcessor.SaveFileFromWebsite(imageFile, "ImageRecipe");

            return StatusCode(StatusCodes.Status201Created, await _recipeRepository.AddRecipeAsync(recipeDTO));
        }


        [HttpPost("{recipeId}/products")]
        public async Task<ActionResult<bool>> AddProducts(int recipeId, [FromBody] List<RecipeProductDTO> recipeProductDTOs)
        {
            var insertResult = await _recipeRepository.AddProductsAsync(recipeId, recipeProductDTOs);
            if (!insertResult)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);

        }

        [HttpPut("{recipeId}/description")]
        public async Task<ActionResult<bool>> AddDescription(int recipeId, [FromBody] RecipeDescriptionDTO recipeDescriptionDTO)
        {
            var updateResult = await _recipeRepository.AddDescriptionAsync(recipeId, recipeDescriptionDTO);
            if (!updateResult)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost("{recipeId}/images")]
        public async Task<ActionResult<bool>> AddImages(int recipeId, List<IFormFile> imageFiles)
        {
            foreach (var image in imageFiles)
            {
                if (!_fileProcessorValidator.IsImageFile(image))
                    return BadRequest();
            }

            List<string> images = new();
            foreach (var image in imageFiles)
            {
                images.Add(await _fileProcessor.SaveFileFromWebsite(image, "ImageRecipe"));
            }

            var insertResult = await _recipeRepository.AddImagesAsync(recipeId, images);
            if (!insertResult)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
