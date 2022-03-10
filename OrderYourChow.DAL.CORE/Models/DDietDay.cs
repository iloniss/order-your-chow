using System;
using System.Collections.Generic;

namespace OrderYourChow.DAL.CORE.Models
{
    public class DDietDay
    {
        public DDietDay()
        {
            DDietDayRecipes = new HashSet<DDietDayRecipe>();
        }

        public int DietDayId { get; set; }
        public int DateDayId { get; set; }
        public int UserId { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SDateDay SDateDay { get; set; }
        public virtual ICollection<DDietDayRecipe> DDietDayRecipes { get; set; }
        public virtual DUser DUser { get; set; }

    }
}
