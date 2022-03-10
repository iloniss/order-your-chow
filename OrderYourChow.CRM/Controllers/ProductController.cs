using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OrderYourChow.CRM.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileProcessorValidator _fileProcessorValidator;
        public ProductController(IProductRepository productRepository, IFileProcessor fileProcessor,
            IFileProcessorValidator fileProcessorValidator)
        {
            _productRepository = productRepository;
            _fileProcessor = fileProcessor;
            _fileProcessorValidator = fileProcessorValidator;
        }

        [HttpPost]
        public async Task<ActionResult<AddProductDTO>> Add([Required] IFormFile ImageFile, [FromForm] AddProductDTO productDTO)
        {
            if (!_fileProcessorValidator.IsImageFile(ImageFile))
                return BadRequest("Nieprawidłowy format pliku.");

            var existProduct = await _productRepository.GetProductByNameAsync(productDTO.Name);

            if (existProduct == null)
            {
                productDTO.Image = await _fileProcessor.SaveFileFromWebsite(ImageFile, "ImageProduct");

                return StatusCode(StatusCodes.Status201Created, await _productRepository.AddProductAsync(productDTO));
            }

            return Conflict("Produkt, który próbujesz dodać już istnieje."); 
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<AddProductDTO>> GetByName(string name)
        {
            return Ok(await _productRepository.GetProductByNameAsync(name));
        }

        [HttpGet("getById/{productId}")]
        public async Task<ActionResult<AddProductDTO>> GetById(int productId)
        {
            return Ok(await _productRepository.GetProductByIdAsync(productId));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            return Ok(await _productRepository.GetProductsAsync());
        }


        [HttpDelete("{productId}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var deleteResult = await _productRepository.DeleteProductAsync(productId);

            if (deleteResult is ErrorAddProductDTO)
                return BadRequest();

            if (deleteResult is EmptyAddProductDTO)
                return NotFound();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult<AddProductDTO>> Update(int productId, IFormFile ImageFile, [FromForm] AddProductDTO productDTO)
        {
            var existProduct = await _productRepository.GetProductByNameAsync(productDTO.Name);

            if (existProduct == null || existProduct.ProductId == productId)
            {
                if (ImageFile != null && _fileProcessorValidator.IsImageFile(ImageFile))
                {
                    _fileProcessor.DeleteFile(productDTO.Image, "ImageProduct");
                    productDTO.Image = await _fileProcessor.SaveFileFromWebsite(ImageFile, "ImageProduct");
                }

                var updateResult = await _productRepository.UpdateProductAsync(productId, productDTO);
                if (updateResult is EmptyAddProductDTO)
                    return NotFound("Nie znaleziono produktu.");

                return Ok(updateResult);
            }
            return Conflict("Produkt, który próbujesz dodać już istnieje.");
        }
    }
}
