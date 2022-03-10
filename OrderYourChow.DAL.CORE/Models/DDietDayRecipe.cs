using System;

namespace OrderYourChow.DAL.CORE.Models
{
    public class DDietDayRecipe
    {
            public int DietDayRecipeId { get; set; }
            public int DietDayId { get; set; }
            public int RecipeId { get; set; }
            public bool Eaten { get; set; }
            public decimal Multiplier { get; set; }
            public string Syslog { get; set; }
            public DateTime Sysdate { get; set; }

            public virtual DDietDay DDietDay { get; set; }
            public virtual DRecipe DRecipe { get; set; }

    }
}
