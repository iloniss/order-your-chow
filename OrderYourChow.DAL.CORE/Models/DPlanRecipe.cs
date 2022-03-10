using System;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DPlanRecipe
    {
        public int PlanRecipeId { get; set; }
        public int RecipeId { get; set; }
        public int DayId { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SDay Day { get; set; }
        public virtual DRecipe Recipe { get; set; }
    }
}
