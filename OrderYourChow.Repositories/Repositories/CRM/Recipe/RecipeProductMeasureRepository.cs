using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using LinqKit;
using OrderYourChow.Repositories.Queries.CRM.Recipe;

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

        public async Task<RecipeProductMeasureDTO> AddRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var recipeProductMeasure = _mapper.Map<SProductMeasure>(recipeProductMeasureDTO);
                await _orderYourChowContext.SProductMeasures.AddAsync(recipeProductMeasure);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new CreatedRecipeProductMeasureDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<RecipeProductMeasureDTO>> GetRecipeProductMeasuresAsync() => 
            _mapper.Map<List<RecipeProductMeasureDTO>>(await _orderYourChowContext.SProductMeasures.OrderBy(x => x.Name).ToListAsync());

        public async Task<RecipeProductMeasureDTO> GetRecipeProductMeasureAsync(GetRecipeProductMeasureQuery getRecipeProductMeasureQuery) => 
            _mapper.Map<RecipeProductMeasureDTO>(await _orderYourChowContext.SProductMeasures.Where(GetRecipeProductMeasureQuerySpec.Filter(getRecipeProductMeasureQuery).Expand())
                .SingleOrDefaultAsync());

        public async Task<RecipeProductMeasureDTO> DeleteRecipeProductMeasureAsync(int recipeProductMeasureId)
        {
            var recipeProductMeasure = await _orderYourChowContext.SProductMeasures.Where(x => x.ProductMeasureId == recipeProductMeasureId).SingleOrDefaultAsync();
            if (recipeProductMeasure == null)
                return new EmptyRecipeProductMeasureDTO(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.Remove(recipeProductMeasure);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new DeletedRecipeProductMeasureDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RecipeProductMeasureDTO> UpdateRecipeProductMeasureAsync(RecipeProductMeasureDTO recipeProductMeasureDTO)
        {
            var recipeProductMeasure = await _orderYourChowContext.SProductMeasures
                .Where(x => x.ProductMeasureId == recipeProductMeasureDTO.ProductMeasureId)
                .SingleOrDefaultAsync();
            if (recipeProductMeasure == null)
                return new EmptyRecipeProductMeasureDTO(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                recipeProductMeasure.Name = recipeProductMeasureDTO.Name;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return new UpdatedRecipeProductMeasureDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RecipeProductMeasureIsUsed(int recipeProductMeasureId) => 
            await _orderYourChowContext.DRecipeProducts.AnyAsync(x => x.ProductMeasureId == recipeProductMeasureId);
    }
}
