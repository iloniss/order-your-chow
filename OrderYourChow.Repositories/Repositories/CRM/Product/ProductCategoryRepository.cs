using AutoMapper;
using FileProcessor.CORE.Services;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                return _mapper.Map<ProductCategoryDTO>(productCategory);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductCategoryDTO>> GetProductCategoriesAsync()
        {
            return _mapper.Map<List<ProductCategoryDTO>>(await _orderYourChowContext.SProductCategories.OrderBy(x => x.Name).ToListAsync());
        }

        public async Task<ProductCategoryDTO> GetProductCategoryByNameAsync(string name)
        {
            return _mapper.Map<ProductCategoryDTO>(await _orderYourChowContext.SProductCategories.Where(x => x.Name == name).SingleOrDefaultAsync());
        }

        public async Task<ProductCategoryDTO> GetProductCategoryByIdAsync(int productCategoryId)
        {
            return _mapper.Map<ProductCategoryDTO>(await _orderYourChowContext.SProductCategories.Where(x => x.ProductCategoryId == productCategoryId).SingleOrDefaultAsync());
        }

        public async Task<ProductCategoryDTO> DeleteProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await _orderYourChowContext.SProductCategories.Where(x => x.ProductCategoryId == productCategoryId).SingleOrDefaultAsync();
            if (productCategory == null)
                return new EmptyProductCategoryDTO();

            bool isUsed = await _orderYourChowContext.SProducts.Where(x => x.CategoryId == productCategoryId).AnyAsync();

            if (isUsed)
                return new ErrorProductCategoryDTO();

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(productCategory);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategoryDTO> UpdateProductCategoryAsync(int productCategoryId, ProductCategoryDTO productCategoryDTO)
        {
            var productCategory = await _orderYourChowContext.SProductCategories.Where(x => x.ProductCategoryId == productCategoryId).SingleOrDefaultAsync();
            if (productCategory == null)
                return new EmptyProductCategoryDTO();

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                productCategory.Name = productCategoryDTO.Name;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return _mapper.Map<ProductCategoryDTO>(productCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
