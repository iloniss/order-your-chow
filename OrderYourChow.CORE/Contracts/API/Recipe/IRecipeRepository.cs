using OrderYourChow.CORE.Models.API.Recipe;

namespace OrderYourChow.CORE.Contracts.API.Recipe
{
    public interface IRecipeRepository
    {
        Task<IList<RecipeInfoDTO>> GetRecipeInfoAsync(int dietDayId);
        Task<RecipeDTO> GetRecipeAsync(int dietDayRecipeId);
        Task<bool> SetEatenAsync(int dietDayRecipeId);
        Task<RecipeDTO> SetMultiplierAsync(int dietDayRecipeId, decimal multiplier);
        Task<List<RecipeDayListDTO>> GetRecipeDayListAsync(int dietDayId);
        Task<List<RecipeExchangeDTO>> GetRecipeExchangeAsync(int recipeId);
        Task<bool> RecipeExchangeAsync(int recipeId, int dietDayRecipeId);
        Task<bool> AddRecipeFavouriteAsync(int recipeId);
        Task<bool> DeleteRecipeFavouriteAsync(int recipeId);
        Task<List<RecipeFavouriteListDTO>> GetRecipeFavouriteListAsync(int? categoryId);
    }
}
