using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SProductMeasure
    {
        public SProductMeasure()
        {
            DRecipeProducts = new HashSet<DRecipeProduct>();
        }

        public int ProductMeasureId { get; set; }
        public string Name { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DRecipeProduct> DRecipeProducts { get; set; }
    }
}
