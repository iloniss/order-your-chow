using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DRecipe
    {
        public DRecipe()
        {
            DPlanRecipes = new HashSet<DPlanRecipe>();
            DRecipeImages = new HashSet<DRecipeImage>();
            DRecipeProducts = new HashSet<DRecipeProduct>();
            DDietDayRecipes = new HashSet<DDietDayRecipe>();
            DRecipeFavourite = new HashSet<DRecipeFavourite>();
        }

        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int CategoryId { get; set; }
        public decimal? Rating { get; set; }
        public bool Favourite { get; set; }
        public bool Meat { get; set; }
        public string MainImage { get; set; }
        public bool Active { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SRecipeCategory Category { get; set; }
        public virtual ICollection<DRecipeFavourite> DRecipeFavourite { get; set; }
        public virtual ICollection<DPlanRecipe> DPlanRecipes { get; set; }
        public virtual ICollection<DRecipeImage> DRecipeImages { get; set; }
        public virtual ICollection<DRecipeProduct> DRecipeProducts { get; set; }
        public virtual ICollection<DShoppingList> DShoppingLists { get; set; }
        public virtual ICollection<DDietDayRecipe> DDietDayRecipes { get; set; }
    }
}
