using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderYourChow.Repositories.Repositories.CRM.Recipe
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

        public async Task<List<RecipeCategoryDTO>> GetRecipeCategoriesAsync() =>
            _mapper.Map<List<RecipeCategoryDTO>>(await _orderYourChowContext.SRecipeCategories.OrderBy(x => x.RecipeCategoryId).ToListAsync());

        //do zmiany
        public async Task<List<RecipeListDTO>> GetRecipesAsync()
        {
            return _mapper.Map<List<RecipeListDTO>>(await _orderYourChowContext.DRecipes
                .Where(recipe => recipe.Active == true)
                .ToListAsync());
        }

        public async Task<RecipeDTO> AddRecipeAsync(RecipeDTO recipeDTO)
        {
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                var recipe = _mapper.Map<DRecipe>(recipeDTO);
                await _orderYourChowContext.DRecipes.AddAsync(recipe);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return _mapper.Map<RecipeDTO>(recipe);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddProductsAsync(int recipeId, List<RecipeProductDTO> recipeProductDTOs)
        {
            var recipe = await _orderYourChowContext.DRecipes.Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                foreach (var productDTO in recipeProductDTOs)
                {
                    var product = _mapper.Map<DRecipeProduct>(productDTO);
                    product.RecipeId = recipeId;
                    await _orderYourChowContext.DRecipeProducts.AddAsync(product);
                }
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddDescriptionAsync(int recipeId, RecipeDescriptionDTO recipeDescriptionDTO)
        {
            var recipe = await _orderYourChowContext.DRecipes.Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                recipe.Description = recipeDescriptionDTO.Description;
                recipe.Active = true;
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddImagesAsync(int recipeId, List<string> images)
        {
            var recipe = await _orderYourChowContext.DRecipes.Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
                return false;

            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                foreach(var image in images)
                {
                    await _orderYourChowContext.DRecipeImages.AddAsync(new DRecipeImage { 
                        RecipeId = recipeId,
                        Image = image
                    });
                }
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
