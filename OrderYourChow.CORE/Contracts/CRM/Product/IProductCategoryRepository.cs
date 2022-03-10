using OrderYourChow.CORE.Models.CRM.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategoryDTO> AddProductCategoryAsync(ProductCategoryDTO productCategoryDTO);
        Task<List<ProductCategoryDTO>> GetProductCategoriesAsync();
        Task<ProductCategoryDTO> GetProductCategoryByNameAsync(string name);
        Task<ProductCategoryDTO> GetProductCategoryByIdAsync(int productCategoryId);
        Task<ProductCategoryDTO> DeleteProductCategoryAsync(int productCategoryid);
        Task<ProductCategoryDTO> UpdateProductCategoryAsync(int productCategoryId, ProductCategoryDTO productCategoryDTO);
    }
}
