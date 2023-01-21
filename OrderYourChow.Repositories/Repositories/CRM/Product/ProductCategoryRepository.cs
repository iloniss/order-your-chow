using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.Repositories.Queries.CRM.Product;
using LinqKit;
using OrderYourChow.CORE.Queries.CRM.Product;

namespace OrderYourChow.Repositories.Repositories.CRM.Product
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;

        public ProductCategoryRepository(OrderYourChowContext orderYourChowContext, IMapper mapper)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
        }

        public async Task<ProductCategoryDTO> AddProductCategoryAsync(ProductCategoryDTO productCategoryDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try 
            {
                var productCategory = _mapper.Map<SProductCategory>(productCategoryDTO);
                await _orderYourChowContext.SProductCategories.AddAsync(productCategory);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new CreatedProductCategoryDTO();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductCategoryDTO>> GetProductCategoriesAsync() => 
            _mapper.Map<List<ProductCategoryDTO>>(await _orderYourChowContext.SProductCategories
                .OrderBy(x => x.Name)
                .ToListAsync());

        public async Task<ProductCategoryDTO> GetProductCategoryAsync(GetProductCategoryQuery getProductCategoryQuery) => 
            _mapper.Map<ProductCategoryDTO>(await _orderYourChowContext.SProductCategories
                .Where(GetProductCategoryQuerySpec.Filter(getProductCategoryQuery).Expand())
                .SingleOrDefaultAsync());

        public async Task<ProductCategoryDTO> DeleteProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await _orderYourChowContext.SProductCategories.Where(x => x.ProductCategoryId == productCategoryId).SingleOrDefaultAsync();
            if (productCategory == null)
                return new EmptyProductCategoryDTO(CORE.Const.CRM.Product.NotFoundProductCategory);

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(productCategory);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new DeletedProductCategoryDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategoryDTO> UpdateProductCategoryAsync(ProductCategoryDTO productCategoryDTO)
        {
            var productCategory = await _orderYourChowContext.SProductCategories.Where(x => x.ProductCategoryId == productCategoryDTO.ProductCategoryId).SingleOrDefaultAsync();
            if (productCategory == null)
                return new EmptyProductCategoryDTO(CORE.Const.CRM.Product.NotFoundProductCategory);

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                productCategory.Name = productCategoryDTO.Name;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return new UpdatedProductCategoryDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ProductCategoryIsUsed(int productCategoryId) => 
            await _orderYourChowContext.SProducts.AnyAsync(x => x.CategoryId == productCategoryId);
    }
}
