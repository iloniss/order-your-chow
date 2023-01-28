using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;

namespace OrderYourChow.CRM.Controllers
{
    public class RecipeProductMeasureController : BaseController
    {
        private readonly IRecipeProductMeasureService _productMeasureService;
       
        public RecipeProductMeasureController(IRecipeProductMeasureService productMeasureService)
        {
            _productMeasureService = productMeasureService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipeProductMeasure([FromForm] RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var result = await _productMeasureService.AddRecipeProductMeasure(recipeProductMeasureDTO);

            if (result is ErrorRecipeProductMeasureDTO)
                return BadRequest(new { (result as ErrorRecipeProductMeasureDTO).Message });
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<ActionResult<List<RecipeProductMeasureDTO>>> GetRecipeProductMeasures() => 
            Ok(await _productMeasureService.GetRecipeProductMeasures());

        [HttpGet("{recipeProductMeasureId}")]
        public async Task<ActionResult<RecipeProductMeasureDTO>> GetRecipeProductMeasureById(int recipeProductMeasureId) => 
            Ok(await _productMeasureService.GetRecipeProductMeasureById(recipeProductMeasureId));

        [HttpDelete("{recipeProductMeasureId}")]
        public async Task<IActionResult> DeleteRecipeProductMeasure(int recipeProductMeasureId)
        {
            var result = await _productMeasureService.DeleteRecipeProductMeasure(recipeProductMeasureId);

            if (result is EmptyRecipeProductMeasureDTO)
                return NotFound(new { (result as EmptyRecipeProductMeasureDTO).Message });
            else if (result is ErrorRecipeProductMeasureDTO)
                return BadRequest(new { (result as ErrorRecipeProductMeasureDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipeProductMeasure([FromForm] RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var result = await _productMeasureService.UpdateRecipeProductMeasure(recipeProductMeasureDTO);

            if (result is ErrorRecipeProductMeasureDTO)
                return BadRequest(new { (result as ErrorRecipeProductMeasureDTO).Message });
            else if (result is EmptyRecipeProductMeasureDTO)
                return NotFound(new { (result as EmptyRecipeProductMeasureDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
