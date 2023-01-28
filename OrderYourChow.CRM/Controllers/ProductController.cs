using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using System.ComponentModel.DataAnnotations;

namespace OrderYourChow.CRM.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([Required] IFormFile imageFile, [FromForm] AddProductDTO productDTO)
        {
            var result = await _productService.AddProduct(imageFile, productDTO);

            if(result is ErrorProductDTO)
                return BadRequest(new { (result as ErrorProductDTO).Message });
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProduct([FromQuery]GetProductQuery getProductQuery) => 
            Ok(await _productService.GetProduct(getProductQuery));

        [HttpGet("list")]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts() => 
            Ok(await _productService.GetProducts());


        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);

            if (result is ErrorProductDTO)
                return BadRequest(new { (result as ErrorProductDTO).Message });
            else if (result is EmptyProductDTO)
                return NotFound(new { (result as EmptyProductDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(IFormFile imageFile, [FromForm] ProductDTO productDTO)
        {
            var result = await _productService.UpdateProduct(imageFile, productDTO);

            if(result is ErrorProductDTO)
                return BadRequest(new { (result as ErrorProductDTO).Message });
            else if (result is EmptyProductDTO)
                return NotFound(new { (result as EmptyProductDTO).Message });
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
