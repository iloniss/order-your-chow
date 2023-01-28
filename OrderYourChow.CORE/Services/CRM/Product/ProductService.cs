using FileProcessor.CORE.Services;
using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.CORE.Services.CRM.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileProcessorValidator _fileProcessorValidator;

        public ProductService(IProductRepository productRepository, IFileProcessor fileProcessor, IFileProcessorValidator fileProcessorValidator)
        {
            _productRepository = productRepository;
            _fileProcessor = fileProcessor;
            _fileProcessorValidator = fileProcessorValidator;
        }

        public async Task<ProductDTO> AddProduct(IFormFile ImageFile, AddProductDTO addProductDTO)
        {
            if (!_fileProcessorValidator.IsImageFile(ImageFile))
                return new ErrorProductDTO(Const.Shared.Global.InvalidFile);

            var existProduct = await _productRepository.GetProductAsync(new GetProductQuery() { Name = addProductDTO.Name });

            if(existProduct == null)
            {
                addProductDTO.Image = await _fileProcessor.SaveFileFromWebsite(ImageFile, Const.Shared.Global.ProductImagesPath);

                return await _productRepository.AddProductAsync(addProductDTO);
            }
            return new ErrorProductDTO(Const.CRM.Product.ExistedProduct);
        }

        public async Task<ProductDTO> GetProduct(GetProductQuery getProductQuery) => 
            await _productRepository.GetProductAsync(getProductQuery);

        public async Task<IList<ProductDTO>> GetProducts() =>
            await _productRepository.GetProductsAsync();

        public async Task<ProductDTO> DeleteProduct(int productId)
        {
            if (await _productRepository.ProductIsUsed(productId))
                return new ErrorProductDTO(Const.CRM.Product.UsedProduct);

            var result = await _productRepository.DeleteProductAsync(productId);

            if(result is not EmptyProductDTO)
            _fileProcessor.DeleteFile(result.Image, Const.Shared.Global.ProductImagesPath);

            return result;
        }

        public async Task<ProductDTO> UpdateProduct(IFormFile imageFile, ProductDTO productDTO)
        {
            if(imageFile != null && !_fileProcessorValidator.IsImageFile(imageFile))
            {
                return new ErrorProductDTO(Const.Shared.Global.InvalidFile);
            }

            var existedProduct = await _productRepository.GetProductAsync(new GetProductQuery() { Name = productDTO.Name });

            if(existedProduct == null || existedProduct.ProductId == productDTO.ProductId)
            {

                if (imageFile != null)
                {
                    productDTO.Image = await _fileProcessor.SaveFileFromWebsite(imageFile, Const.Shared.Global.ProductImagesPath);
                    var result = await _productRepository.UpdateProductAsync(productDTO);
                    _fileProcessor.DeleteFile(result.Image, Const.Shared.Global.ProductImagesPath);
                    return result;
                }
                else
                {
                    return await _productRepository.UpdateProductAsync(productDTO);
                }
            }
            return new ErrorProductDTO(Const.CRM.Product.ExistedProduct);
        }
    }
}
