using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductRepository
    {
        Task<ProductDTO> AddProductAsync(AddProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> DeleteProductAsync(int productId);
        Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);
        Task<ProductDTO> GetProductAsync(GetProductQuery getProductQuery);
        Task<bool> ProductIsUsed(int productId);
    }
}
