using Microsoft.AspNetCore.Http;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(IFormFile ImageFile, AddProductDTO addProductDTO);
        Task<ProductDTO> GetProduct(GetProductQuery getProductQuery);
        Task<List<ProductDTO>> GetProducts();
        Task<ProductDTO> DeleteProduct(int productId);
        Task<ProductDTO> UpdateProduct(IFormFile ImageFile, ProductDTO productDTO);
    }
}
