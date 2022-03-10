using System;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DRecipeImage
    {
        public int RecipeImageId { get; set; }
        public int RecipeId { get; set; }
        public string Image { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual DRecipe Recipe { get; set; }
    }
}
