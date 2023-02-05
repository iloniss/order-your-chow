using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.Repositories.Queries.CRM.Product;
using LinqKit;

namespace OrderYourChow.Repositories.Repositories.CRM.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;
       
        public ProductRepository(OrderYourChowContext orderYourChowContext, IMapper mapper)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
        }

        public async Task<ProductDTO> AddProductAsync(AddProductDTO productDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var product = _mapper.Map<SProduct>(productDTO);
                await _orderYourChowContext.SProducts.AddAsync(product);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new CreatedProductDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<ProductDTO>> GetProductsAsync() => 
            _mapper.Map<IList<ProductDTO>>(await _orderYourChowContext.SProducts
                .Include(x => x.Category)
                .OrderBy(x => x.Name).ToListAsync());

        public async Task<ProductDTO> DeleteProductAsync(int productId)
        {
            var product = await _orderYourChowContext.SProducts
                .Where(x => x.ProductId == productId).SingleOrDefaultAsync();

            if (product == null)
                return new EmptyProductDTO(CORE.Const.CRM.Product.NotFoundProduct);

            string image = product.Image;
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(product);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new DeletedProductDTO() { Image = image };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
        {
            var product = await _orderYourChowContext.SProducts.Where(x => x.ProductId == productDTO.ProductId).SingleOrDefaultAsync();
            if (product == null)
                return new EmptyProductDTO(CORE.Const.CRM.Product.NotFoundProduct);
           
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var oldImage = product.Image;
                product.Name = productDTO.Name;
                product.Image = string.IsNullOrEmpty(productDTO.Image) ? product.Image : productDTO.Image;
                product.CategoryId = productDTO.ProductCategoryId;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new UpdatedProductDTO() { Image = oldImage };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> GetProductAsync(GetProductQuery getProductQuery) => 
            _mapper.Map<ProductDTO>(await _orderYourChowContext.SProducts
                .Include(x => x.Category)
                .Where(GetProductQuerySpec.Filter(getProductQuery).Expand())
                .SingleOrDefaultAsync());

        public async Task<bool> ProductIsUsed(int productId) => 
            await _orderYourChowContext.DRecipeProducts
                .AnyAsync(x => x.ProductId == productId);
    }
}
