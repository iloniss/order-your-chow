using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.API.Recipe;
using OrderYourChow.CORE.Models.API.Recipe;
using static OrderYourChow.CORE.Models.API.Recipe.RecipeDTO;

namespace OrderYourChow.Repositories.Repositories.API.Recipe
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly OrderYourChowContext _orderYourChowContext;
        private readonly IMapper _mapper;
        public RecipeRepository(OrderYourChowContext orderYourChowContext, IMapper mapper)
        {
            _orderYourChowContext = orderYourChowContext;
            _mapper = mapper;
        }
        
        public async Task<IList<RecipeInfoDTO>> GetRecipeInfoAsync(int dietDayId)
        {
            return _mapper.Map<IList<RecipeInfoDTO>>(await _orderYourChowContext.DDietDayRecipes
                .Include(x => x.DRecipe)
                .ThenInclude(x => x.Category)
                .Where(x => x.DietDayId == dietDayId)
                .Select(x => x.DRecipe)
                .ToListAsync());
        }

        public async Task<RecipeDTO> GetRecipeAsync(int dietDayRecipeId)
        {
            var recipeDTO = await _orderYourChowContext.DDietDayRecipes
                .Include(x => x.DRecipe)
                .Include(x => x.DRecipe)
                .ThenInclude(x => x.DRecipeProducts)
                .Where(recipe => recipe.DietDayRecipeId == dietDayRecipeId)
                .SingleOrDefaultAsync();
            
            //todo UserId
            var multiplierDiet = (await _orderYourChowContext.DUsers
                .Where(x => x.UserId == 1)
                .SingleOrDefaultAsync())
                .MultiplierDiet;

            if (recipeDTO == null)
                return new EmptyRecipeDTO();

            
            foreach(var item in recipeDTO.DRecipe.DRecipeProducts)
            {
                item.Count *= multiplierDiet;

                if (recipeDTO.Multiplier != 1)
                {
                    item.Count *= recipeDTO.Multiplier;
                }
            } 
            

            return _mapper.Map<RecipeDTO>(recipeDTO);
        }

        public async Task<bool> SetEatenAsync(int dietDayRecipeId)
        {
            var result = await _orderYourChowContext.DDietDayRecipes
                .Where(x => x.DietDayRecipeId == dietDayRecipeId)
                .SingleOrDefaultAsync();

            if (result == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                result.Eaten = !result.Eaten;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RecipeDTO> SetMultiplierAsync(int dietDayRecipeId, decimal multiplier)
        {
            var result = await _orderYourChowContext.DDietDayRecipes
                .Where(x => x.DietDayRecipeId == dietDayRecipeId)
                .SingleOrDefaultAsync();

            if (result == null)
                return new EmptyRecipeDTO();

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                result.Multiplier = multiplier;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return await GetRecipeAsync(dietDayRecipeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RecipeDayListDTO>> GetRecipeDayListAsync(int dietDayId)
        {
            return _mapper.Map<List<RecipeDayListDTO>>(await _orderYourChowContext.DDietDayRecipes
                .Include(x => x.DRecipe)
                .ThenInclude(x => x.Category)
                .Where(x => x.DietDayId == dietDayId)
                .ToListAsync());          
        }

        public async Task<List<RecipeExchangeDTO>> GetRecipeExchangeAsync(int recipeId)
        {
            var recipe = await _orderYourChowContext.DRecipes
                .Where(x => x.RecipeId == recipeId)
                .SingleOrDefaultAsync();

            if (recipe == null)
                return null;


            return _mapper.Map<List<RecipeExchangeDTO>>(await _orderYourChowContext.DRecipes
                .Where(x => x.RecipeId != recipeId && x.CategoryId == recipe.CategoryId)
                .ToListAsync());
                
        }

        public async Task<bool> RecipeExchangeAsync(int recipeId, int dietDayRecipeId)
        {
            var result = await _orderYourChowContext.DDietDayRecipes
                .Where(x => x.DietDayRecipeId == dietDayRecipeId)
                .SingleOrDefaultAsync();

            if (result == null)
                return false;

            var recipe = await _orderYourChowContext.DRecipes
                .Where(x => x.RecipeId == recipeId)
                .SingleOrDefaultAsync();

            if (recipe == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                result.RecipeId = recipeId;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddRecipeFavouriteAsync(int recipeId)
        {
            var result= await _orderYourChowContext.DRecipes
                .Where(x => x.RecipeId== recipeId)
                .SingleOrDefaultAsync();

            if (result == null)
                return false;

            //Walidacja czy można dodać czy już takiego nie ma

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                //todo UserId
                await _orderYourChowContext.DRecipeFavourites.AddAsync(new DRecipeFavourite() { RecipeId = recipeId, UserId = 2});
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRecipeFavouriteAsync(int recipeId)
        {
            //todo UserId
            var result = await _orderYourChowContext.DRecipeFavourites
                .Where(x => x.RecipeId == recipeId &&  x.UserId == 2)
                .SingleOrDefaultAsync();

            if (result == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                _orderYourChowContext.DRecipeFavourites.Remove(result);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<RecipeFavouriteListDTO>> GetRecipeFavouriteListAsync(int? categoryId)
        {
            //todo UserId
            var recipes = await _orderYourChowContext.DRecipeFavourites
                .Include(x => x.DRecipe)
                .Where(x => x.UserId == 2 && x.DRecipe.CategoryId == categoryId)
                .ToListAsync();

            if(recipes==null)
                return null;

            return  _mapper.Map<List<RecipeFavouriteListDTO>>(recipes);


        }

    }
}
