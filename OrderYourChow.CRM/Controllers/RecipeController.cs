using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Contracts.Services;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using System.ComponentModel.DataAnnotations;

namespace OrderYourChow.CRM.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileProcessorValidator _fileProcessorValidator;
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeRepository recipeRepository, IFileProcessor fileProcessor,
            IFileProcessorValidator fileProcessorValidator, IRecipeService recipeService)
        {
            _recipeRepository = recipeRepository;
            _fileProcessor = fileProcessor;
            _fileProcessorValidator = fileProcessorValidator;
            _recipeService = recipeService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IList<RecipeCategoryDTO>>> GetCategories() => 
            Ok(await _recipeService.GetRecipeCategories());

        [HttpGet]
        public async Task<ActionResult<List<RecipeListDTO>>> GetRecipes(bool? isActive) =>
            Ok(await _recipeService.GetRecipes(isActive));

        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int recipeId) =>
            Ok(await _recipeRepository.GetRecipeAsync(new GetRecipeQuery { RecipeId = recipeId}));

        [HttpGet("{recipeId:int}/recipeProducts")]
        public async Task<ActionResult<RecipeProductListDTO>> GetRecipeProducts([FromRoute] int recipeId)
        {
            var result = await _recipeRepository.GetRecipeProductsAsync(recipeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> Add([Required] IFormFile imageFile, [FromForm] RecipeDTO recipeDTO)
        {
            if (!_fileProcessorValidator.IsImageFile(imageFile))
                return BadRequest();

            recipeDTO.MainImage = await _fileProcessor.SaveFileFromWebsite(imageFile, "ImageRecipe");

            return StatusCode(StatusCodes.Status201Created, await _recipeRepository.AddRecipeAsync(recipeDTO));
        }


        [HttpPost("{recipeId}/products")]
        public async Task<ActionResult<bool>> SaveProducts(int recipeId, [FromBody] RecipeProductListDTO recipeProductListDTO)
        {
            var insertResult = await _recipeService.SaveProducts(recipeId, recipeProductListDTO.RecipeProductList);
            if (!insertResult)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);

        }

        [HttpPut("description")]
        public async Task<ActionResult<bool>> AddDescription([FromForm] RecipeDescriptionDTO recipeDescriptionDTO)
        {
            var updateResult = await _recipeRepository.UpdateDescriptionAsync(recipeDescriptionDTO);
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

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var result = await _recipeService.DeleteRecipe(recipeId);

            if (result is ErrorRecipeDTO)
                return BadRequest(new { (result as ErrorRecipeDTO).Message });
            else if(result is EmptyRecipeDTO)
                return NotFound(new { (result as EmptyRecipeDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(IFormFile imageFile, [FromForm] RecipeDTO recipeDTO)
        {
            var result = await _recipeService.UpdateRecipe(imageFile, recipeDTO);

            if (result is ErrorRecipeDTO)
                return BadRequest(new { (result as ErrorRecipeDTO).Message });
            else if (result is EmptyRecipeDTO)
                return NotFound(new { (result as EmptyRecipeDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
