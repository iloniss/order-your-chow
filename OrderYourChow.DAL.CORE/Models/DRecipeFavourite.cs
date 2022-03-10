using System.Collections.Generic;

namespace OrderYourChow.DAL.CORE.Models
{
    public class DRecipeFavourite
    {
        public int RecipeFavouriteId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual DRecipe DRecipe { get; set; }
    }
}
