using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.CORE.Contracts.CRM.Product
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryDTO> UpdateProductCategory(ProductCategoryDTO productCategoryDTO);
        Task<ProductCategoryDTO> DeleteProductCategory(int productCategoryId);
        Task<ProductCategoryDTO> GetProductCategory(GetProductCategoryQuery getProductCategoryQuery);
        Task<IList<ProductCategoryDTO>> GetProductCategories();
        Task<ProductCategoryDTO> AddProductCategory(ProductCategoryDTO productCategoryDTO);
    }
}
