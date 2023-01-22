using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductService : IScopedService
    {
        Task<ProductDTO> AddProduct(IFormFile ImageFile, AddProductDTO addProductDTO);
        Task<ProductDTO> GetProduct(GetProductQuery getProductQuery);
        Task<IList<ProductDTO>> GetProducts();
        Task<ProductDTO> DeleteProduct(int productId);
        Task<ProductDTO> UpdateProduct(IFormFile ImageFile, ProductDTO productDTO);
    }
}
