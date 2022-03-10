using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SRecipeCategory
    {
        public SRecipeCategory()
        {
            DRecipes = new HashSet<DRecipe>();
        }

        public int RecipeCategoryId { get; set; }
        public string Name { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DRecipe> DRecipes { get; set; }
    }
}
