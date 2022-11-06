using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;

namespace OrderYourChow.CRM.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTO>> AddCategory([FromForm] ProductCategoryDTO productCategoryDTO)
        {
            var existCategory = await _productCategoryRepository.GetProductCategoryByNameAsync(productCategoryDTO.Name);

            if (existCategory == null)
            {
                return StatusCode(StatusCodes.Status201Created, await _productCategoryRepository.AddProductCategoryAsync(productCategoryDTO));
            }
            return Conflict("Kategoria, którą próbujesz dodać już istnieje.");

        }

        [HttpGet]
        [ResponseCache(Duration = 500)]
        public async Task<ActionResult<List<ProductCategoryDTO>>> GetCategories()
        {
            return Ok(await _productCategoryRepository.GetProductCategoriesAsync());
        }

        [HttpGet("{productCategoryId}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetCategory(int productCategoryId)
        {
            return Ok(await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId));
        }

        [HttpDelete("{productCategoryId}")]
        public async Task<ActionResult> DeleteCategory(int productCategoryId)
        {
            var deleteResult = await _productCategoryRepository.DeleteProductCategoryAsync(productCategoryId);

            if (deleteResult is EmptyProductCategoryDTO)
                return NotFound();

            if (deleteResult is ErrorProductCategoryDTO)
                return BadRequest();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{productCategoryId}")]
        public async Task<ActionResult<ProductCategoryDTO>> UpdateCategory(int productCategoryId, [FromForm] ProductCategoryDTO productCategoryDTO)
        {
            var existCategory = await _productCategoryRepository.GetProductCategoryByNameAsync(productCategoryDTO.Name);

            if(existCategory == null)
            {
                var updateResult = await _productCategoryRepository.UpdateProductCategoryAsync(productCategoryId, productCategoryDTO);
                if (updateResult is EmptyProductCategoryDTO)
                    return NotFound();
                return Ok(updateResult);
            }
            return Conflict("Kategoria, którą próbujesz dodać już istnieje.");
        }
    }
}
