using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderYourChow.Repositories.Repositories.CRM.Recipe
{
    public class RecipeProductMeasureRepository : IRecipeProductMeasureRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;
        public RecipeProductMeasureRepository(OrderYourChowContext orderYourChowContext, IMapper mapper)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
        }

        public async Task<RecipeProductMeasureDTO> AddProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var recipeProductMeasure = _mapper.Map<SProductMeasure>(recipeProductMeasureDTO);
                await _orderYourChowContext.SProductMeasures.AddAsync(recipeProductMeasure);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return _mapper.Map<RecipeProductMeasureDTO>(recipeProductMeasure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RecipeProductMeasureDTO>> GetProductMeasureAsync()
        {
            return _mapper.Map<List<RecipeProductMeasureDTO>>(await _orderYourChowContext.SProductMeasures.OrderBy(x => x.Name).ToListAsync());
        }

        public async Task<RecipeProductMeasureDTO> GetProductMeasureByIdAsync(int recipeProductMeasureId)
        {
            return _mapper.Map<RecipeProductMeasureDTO>(await _orderYourChowContext.SProductMeasures.Where(x => x.ProductMeasureId == recipeProductMeasureId).SingleOrDefaultAsync());
        }

        public async Task<RecipeProductMeasureDTO> DeleteProductMeasureAsync(int recipeProductMeasureId)
        {
            var recipeProductMeasure = await _orderYourChowContext.SProductMeasures.Where(x => x.ProductMeasureId == recipeProductMeasureId).SingleOrDefaultAsync();
            if (recipeProductMeasure == null)
                return new EmptyRecipeProductMeasureDTO();

            bool isUsed = await _orderYourChowContext.DRecipeProducts.Where(x=>x.ProductMeasureId == recipeProductMeasureId).AnyAsync();

            if (isUsed)
                return new ErrorRecipeProductMeasureDTO();

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(recipeProductMeasure);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RecipeProductMeasureDTO> UpdateProductMeasureAsync(int recipeProductMeasureId, RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var recipeProductMeasure = await _orderYourChowContext.SProductMeasures.Where(x => x.ProductMeasureId == recipeProductMeasureId).SingleOrDefaultAsync();
            if (recipeProductMeasure == null)
                return new EmptyRecipeProductMeasureDTO();

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                recipeProductMeasure.Name = recipeProductMeasureDTO.Name;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return _mapper.Map<RecipeProductMeasureDTO>(recipeProductMeasure);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
