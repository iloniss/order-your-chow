using System;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DRecipeProduct
    {
        public int RecipeProductId { get; set; }
        public int RecipeId { get; set; }
        public int ProductId { get; set; }
        public int ProductMeasureId { get; set; }
        public decimal Count { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SProduct Product { get; set; }
        public virtual SProductMeasure ProductMeasure { get; set; }
        public virtual DRecipe Recipe { get; set; }
    }
}
