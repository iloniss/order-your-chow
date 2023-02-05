using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.API.Recipe;
using OrderYourChow.CORE.Models.API.Recipe;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static OrderYourChow.CORE.Models.API.Recipe.RecipeDTO;

namespace OrderYourChow.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpPut("seteaten/{dietDayRecipeId}")]
        public async Task<ActionResult<bool>> SetEaten(int dietDayRecipeId)
        {
            var updateResult = await _recipeRepository.SetEatenAsync(dietDayRecipeId);
            if (!updateResult)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("setmultiplier")]
        public async Task<ActionResult<RecipeDTO>> SetMultiplier(int dietDayRecipeId, decimal multiplier)
        {
            var recipeDTO = await _recipeRepository.SetMultiplierAsync(dietDayRecipeId, multiplier);
            if (recipeDTO is EmptyRecipeDTO)
                return NotFound();

            return StatusCode(StatusCodes.Status201Created, recipeDTO);

        }

        [HttpGet("getRecipesForDietDayId")]
        public async Task<ActionResult<IList<RecipeInfoDTO>>> GetRecipesInfo(int dietDayId) => 
            Ok(await _recipeRepository.GetRecipeInfoAsync(dietDayId));

        [HttpGet("{dietDayRecipeId}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int dietDayRecipeId)
        {
            var recipeDTO = await _recipeRepository.GetRecipeAsync(dietDayRecipeId);

            if (recipeDTO is EmptyRecipeDTO)
                return NotFound();

            return recipeDTO;
        }

        [HttpGet("getRecipeDayList")]
        public async Task<ActionResult<List<RecipeDayListDTO>>> GetRecipeDayList(int dietDayId)
        {
            return await _recipeRepository.GetRecipeDayListAsync(dietDayId);
        }

        [HttpGet("getRecipeExchange")]
        public async Task<ActionResult<List<RecipeExchangeDTO>>> GetRecipeExchange([Required] int recipeId)
        {
            return await _recipeRepository.GetRecipeExchangeAsync(recipeId);
        }

        [HttpPut("recipeExchange")]
        public async Task<ActionResult<bool>> PutRecipeExchange([Required] int recipeId, [Required] int dietDayRecipeId)
        {
            var result = await _recipeRepository.RecipeExchangeAsync(recipeId, dietDayRecipeId);

            if (result == false)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("addRecipeToFavourite/{recipeId}")]
        public async Task<IActionResult> AddRecipeFavourite(int recipeId)
        {
            var result = await _recipeRepository.AddRecipeFavouriteAsync(recipeId);
            
            if (result == false)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("deleteRecipeFromFavourite/{recipeId}")]
        public async Task<IActionResult> DeleteRecipeFavourite(int recipeId)
        {
            var result = await _recipeRepository.DeleteRecipeFavouriteAsync(recipeId);

            if (result == false)
                return BadRequest();

            return NoContent();
        }

        [HttpGet("getRecipeFavourite")]
        public async Task<ActionResult<List<RecipeFavouriteListDTO>>> GetRecipeFavouriteList(int? categoryId)
        {
            return await _recipeRepository.GetRecipeFavouriteListAsync(categoryId);
        }
    }

}
