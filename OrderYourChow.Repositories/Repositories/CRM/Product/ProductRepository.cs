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
    public class ProductRepository : IProductRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;
        private readonly IFileProcessor _fileProcessor;
        public ProductRepository(OrderYourChowContext orderYourChowContext, IMapper mapper, IFileProcessor fileProcessor)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
            _fileProcessor = fileProcessor;
        }

        public async Task<AddProductDTO> AddProductAsync(AddProductDTO productDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var product = _mapper.Map<SProduct>(productDTO);
                await _orderYourChowContext.SProducts.AddAsync(product);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return _mapper.Map<AddProductDTO>(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            return _mapper.Map<List<ProductDTO>>(await _orderYourChowContext.SProducts
                .Include(x => x.Category)
                .OrderBy(x => x.Name).ToListAsync());
        }

        public async Task<AddProductDTO> GetProductByNameAsync(string name)
        {
            return _mapper.Map<AddProductDTO>(await _orderYourChowContext.SProducts
                .Where(x => x.Name == name).SingleOrDefaultAsync());
        }

        public async Task<AddProductDTO> GetProductByIdAsync(int productId)
        {
            return _mapper.Map<AddProductDTO>(await _orderYourChowContext.SProducts
                .Where(x => x.ProductId == productId).SingleOrDefaultAsync());
        }

        public async Task<AddProductDTO> DeleteProductAsync(int productId)
        {
            var product = await _orderYourChowContext.SProducts
                .Where(x => x.ProductId == productId).SingleOrDefaultAsync();

            if (product == null)
                return new EmptyAddProductDTO();

            bool isUsed = await _orderYourChowContext.DRecipeProducts
                .Where(x => x.ProductId == productId).AnyAsync();

            if (isUsed)
                return new ErrorAddProductDTO();

            _fileProcessor.DeleteFile(product.Image, "ImageProduct");

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(product);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AddProductDTO> UpdateProductAsync(int productId, AddProductDTO productDTO)
        {
            var product = await _orderYourChowContext.SProducts.Where(x => x.ProductId == productId).SingleOrDefaultAsync();
            if (product == null)
                return new EmptyAddProductDTO();
           
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                product.Name = productDTO.Name;
                product.Image = productDTO.Image ?? product.Image;
                product.CategoryId = productDTO.ProductCategoryId;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
