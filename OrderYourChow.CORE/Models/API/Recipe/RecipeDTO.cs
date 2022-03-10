using OrderYourChow.CORE.Models.Shared.Recipe;
using System.Collections.Generic;

namespace OrderYourChow.CORE.Models.API.Recipe
{ 
    public class RecipeDTO
    {
        public RecipeDTO()
        {
            RecipeProducts = new List<RecipeProductDTO>();
        }
        public int RecipeId { get; set; } 
        public string RecipeName { get; set; } 
        public string Description { get; set; } 
        public int Duration { get; set; } 
        public string MainImage { get; set; } 
        public bool Eaten { get; set; } 
        public decimal Multiplier { get; set; }
        public IList<RecipeProductDTO> RecipeProducts { get; set; }

        public sealed class EmptyRecipeDTO : RecipeDTO
        {

        }

    }

}
