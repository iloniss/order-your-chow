using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SDay
    {
        public SDay()
        {
            DPlanRecipes = new HashSet<DPlanRecipe>();
            SDateDays = new HashSet<SDateDay>();
        }

        public int DayId { get; set; }
        public string Name { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DPlanRecipe> DPlanRecipes { get; set; }
        public virtual ICollection<SDateDay> SDateDays { get; set; }
    }
}
