using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CRM.Controllers
{
    public class RecipeProductMeasureController : BaseController
    {
        private readonly IRecipeProductMeasureRepository _recipeProductMeasureRepository;
       
        public RecipeProductMeasureController(IRecipeProductMeasureRepository recipeProductMeasureRepository)
        {
            _recipeProductMeasureRepository = recipeProductMeasureRepository;
        }

        [HttpPost]
        public async Task<ActionResult<RecipeProductMeasureDTO>> AddRecipeProductMeasure([FromForm] RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            return StatusCode(StatusCodes.Status201Created, await _recipeProductMeasureRepository.AddProductMeasureAsync(recipeProductMeasureDTO));
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeProductMeasureDTO>>> GetRecipeProductMeasure()
        {
            return Ok(await _recipeProductMeasureRepository.GetProductMeasureAsync());
        }

        [HttpGet("{recipeProductMeasureId}")]
        public async Task<ActionResult<RecipeProductMeasureDTO>> GetRecipeProductMeasureById(int recipeProductMeasureId)
        {
            return Ok(await _recipeProductMeasureRepository.GetProductMeasureByIdAsync(recipeProductMeasureId));
        }

        [HttpDelete("{recipeProductMeasureId}")]
        public async Task<ActionResult> DeleteRecipeProductMeasure(int recipeProductMeasureId)
        {
            var deleteResult = await _recipeProductMeasureRepository.DeleteProductMeasureAsync(recipeProductMeasureId);
            if (deleteResult is EmptyRecipeProductMeasureDTO)
                return NotFound();

            if (deleteResult is ErrorRecipeProductMeasureDTO)
                return BadRequest();

         

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{recipeProductMeasureId}")]
        public async Task<ActionResult<RecipeProductMeasureDTO>> UpdateRecipeProductMeasure(int recipeProductMeasureId, [FromBody] RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var updateResult = await _recipeProductMeasureRepository.UpdateProductMeasureAsync(recipeProductMeasureId, recipeProductMeasureDTO);
            if (updateResult is EmptyRecipeProductMeasureDTO)
                return NotFound();

            return Ok(updateResult);
        }
    }
}
