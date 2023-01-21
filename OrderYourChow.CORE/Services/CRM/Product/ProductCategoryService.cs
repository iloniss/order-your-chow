using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.CORE.Services.CRM.Product
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<ProductCategoryDTO> UpdateProductCategory(ProductCategoryDTO productCategoryDTO)
        {
            var existCategory = await _productCategoryRepository.GetProductCategoryAsync(new GetProductCategoryQuery(name: productCategoryDTO.Name));

            if (existCategory != null && existCategory.ProductCategoryId != productCategoryDTO.ProductCategoryId)
                return new ErrorProductCategoryDTO(Const.CRM.Product.ExistedProductCategory);

            return await _productCategoryRepository.UpdateProductCategoryAsync(productCategoryDTO);
        }

        public async Task<ProductCategoryDTO> DeleteProductCategory(int productCategoryId)
        {
            if(await _productCategoryRepository.ProductCategoryIsUsed(productCategoryId))
                return new ErrorProductCategoryDTO(Const.CRM.Product.UsedProductCategory);
            return await _productCategoryRepository.DeleteProductCategoryAsync(productCategoryId);
        }

        public async Task<ProductCategoryDTO> GetProductCategory(GetProductCategoryQuery getProductCategoryQuery) => 
            await _productCategoryRepository.GetProductCategoryAsync(getProductCategoryQuery);

        public async Task<IList<ProductCategoryDTO>> GetProductCategories() => 
            await _productCategoryRepository.GetProductCategoriesAsync();

        public async Task<ProductCategoryDTO> AddProductCategory(ProductCategoryDTO productCategoryDTO)
        {
            var existCategory = await _productCategoryRepository.GetProductCategoryAsync(new GetProductCategoryQuery(name: productCategoryDTO.Name));

            if (existCategory != null)
                return new ErrorProductCategoryDTO(Const.CRM.Product.ExistedProductCategory);

            return await _productCategoryRepository.AddProductCategoryAsync(productCategoryDTO);
        }
    }
}
