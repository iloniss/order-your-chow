using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.CRM.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] ProductCategoryDTO productCategoryDTO)
        {
            var result = await _productCategoryService.AddProductCategory(productCategoryDTO);

            if(result is ErrorProductCategoryDTO)
                return BadRequest(new { (result as ErrorProductCategoryDTO).Message });
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        //[ResponseCache(Duration = 500)] //TODO
        public async Task<ActionResult<IList<ProductCategoryDTO>>> GetCategories() => 
            Ok(await _productCategoryService.GetProductCategories());

        [HttpGet("{productCategoryId}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetCategory(int productCategoryId) => 
            Ok(await _productCategoryService.GetProductCategory(new GetProductCategoryQuery(productCategoryId: productCategoryId)));



        [HttpDelete("{productCategoryId}")]
        public async Task<IActionResult> DeleteCategory(int productCategoryId)
        {
            var result = await _productCategoryService.DeleteProductCategory(productCategoryId);

            if (result is EmptyProductCategoryDTO)
                return NotFound(new { (result as EmptyProductCategoryDTO).Message } );
            else if (result is ErrorProductCategoryDTO)
                return BadRequest(new { (result as ErrorProductCategoryDTO).Message });
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromForm] ProductCategoryDTO productCategoryDTO)
        {
            var result = await _productCategoryService.UpdateProductCategory(productCategoryDTO);

            if (result is ErrorProductCategoryDTO)
                return BadRequest(new { (result as ErrorProductCategoryDTO).Message });
            else if (result is EmptyProductCategoryDTO)
                return NotFound(new { (result as EmptyProductCategoryDTO).Message });
            return NoContent();
        }
    }
}
