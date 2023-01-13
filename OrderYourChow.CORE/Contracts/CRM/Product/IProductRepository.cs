using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductRepository
    {
        Task<ProductDTO> AddProductAsync(AddProductDTO productDTO);
        Task<IList<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> DeleteProductAsync(int productId);
        Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);
        Task<ProductDTO> GetProductAsync(GetProductQuery getProductQuery);
        Task<bool> ProductIsUsed(int productId);
    }
}
