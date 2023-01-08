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
using OrderYourChow.CORE.Enums;

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
        public async Task<List<RecipeListDTO>> GetRecipesAsync(bool? isActive)
        {
            return _mapper.Map<List<RecipeListDTO>>(await _orderYourChowContext.DRecipes
                .Where(recipe => isActive == null || (recipe.Active ?? false) == isActive)
                .ToListAsync());
        }

        public async Task<RecipeDTO> GetRecipeAsync(int recipeId)
        {
            return _mapper.Map<RecipeDTO>(await _orderYourChowContext.DRecipes
                .Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync());
        }

        public async Task<RecipeProductListDTO> GetRecipeProductsAsync(int recipeId)
        {
            var recipe = await _orderYourChowContext.DRecipes.Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync();
            if (recipe == null)
                return null;
            return new RecipeProductListDTO()
            {
                RecipeProductList = _mapper.Map<List<RecipeProductDTO>>(await _orderYourChowContext.DRecipeProducts
                .Where(recipe => recipe.RecipeId == recipeId).ToListAsync())
            };
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

        public async Task<bool> SaveProductsAsync(int recipeId, 
            IEnumerable<RecipeProductDTO> newRecipeProducts, 
            IEnumerable<RecipeProductDTO> updatedRecipeProducts, 
            IEnumerable<RecipeProductDTO> deletedRecipeProducts)
        {
            var recipe = await _orderYourChowContext.DRecipes.Where(x => x.RecipeId == recipeId).SingleOrDefaultAsync();
            using var tran = _orderYourChowContext.Database.BeginTransaction();
            try
            {
                await AddRecipeProductsAsync(recipeId, newRecipeProducts);
                await UpdateRecipeProductsAsync(updatedRecipeProducts);
                await DeleteRecipeProductsAsync(deletedRecipeProducts);
                recipe.Active = !string.IsNullOrEmpty(recipe.Description);
                await _orderYourChowContext.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task AddRecipeProductsAsync(int recipeId, IEnumerable<RecipeProductDTO> newRecipeProducts)
        {
            await _orderYourChowContext.DRecipeProducts.AddRangeAsync(newRecipeProducts.Select(x => new DRecipeProduct
            {
                Count = x.Count,
                ProductId = x.ProductId,
                ProductMeasureId = x.ProductMeasureId,
                RecipeId = recipeId,
            }).ToList());
        }

        private async Task UpdateRecipeProductsAsync(IEnumerable<RecipeProductDTO> updatedRecipeProducts)
        {
            foreach (var recipeProduct in updatedRecipeProducts)
            {
                var productToUpdate = await _orderYourChowContext.DRecipeProducts
                    .Where(x => x.RecipeProductId == recipeProduct.RecipeProductId)
                    .SingleOrDefaultAsync();
                productToUpdate.ProductMeasureId = recipeProduct.ProductMeasureId;
                productToUpdate.ProductId = recipeProduct.ProductId;
                productToUpdate.Count = recipeProduct.Count;
            };
        }
        private async Task DeleteRecipeProductsAsync(IEnumerable<RecipeProductDTO> deletedRecipeProducts)
        {
            foreach (var recipeProduct in deletedRecipeProducts)
            {
                var productsToDelete = await _orderYourChowContext.DRecipeProducts
                    .Where(x => x.RecipeProductId == recipeProduct.RecipeProductId)
                    .SingleOrDefaultAsync();
                _orderYourChowContext.DRecipeProducts.Remove(productsToDelete);
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
                recipe.Active = await _orderYourChowContext.DRecipeProducts.Where(x => x.RecipeId == recipeId).AnyAsync();
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
