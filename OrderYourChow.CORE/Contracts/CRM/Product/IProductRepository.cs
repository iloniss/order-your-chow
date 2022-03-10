using OrderYourChow.CORE.Models.CRM.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductRepository
    {
        Task<AddProductDTO> AddProductAsync(AddProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsAsync();
        Task<AddProductDTO> DeleteProductAsync(int productId);
        Task<AddProductDTO> UpdateProductAsync(int productId, AddProductDTO productDTO);
        Task<AddProductDTO> GetProductByNameAsync(string name);
        Task<AddProductDTO> GetProductByIdAsync(int productId);

    }
}
