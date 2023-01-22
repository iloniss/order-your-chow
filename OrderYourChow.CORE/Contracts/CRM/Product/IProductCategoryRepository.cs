using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.Infrastructure.Services;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductCategoryRepository : IScopedRepository
    {
        Task<ProductCategoryDTO> AddProductCategoryAsync(ProductCategoryDTO productCategoryDTO);
        Task<List<ProductCategoryDTO>> GetProductCategoriesAsync();
        Task<ProductCategoryDTO> GetProductCategoryAsync(GetProductCategoryQuery getProductCategoryQuery);
        Task<ProductCategoryDTO> DeleteProductCategoryAsync(int productCategoryid);
        Task<ProductCategoryDTO> UpdateProductCategoryAsync(ProductCategoryDTO productCategoryDTO);
        Task<bool> ProductCategoryIsUsed(int productCategoryId);
    }
}
