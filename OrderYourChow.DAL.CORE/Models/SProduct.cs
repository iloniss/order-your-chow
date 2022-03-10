using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SProduct
    {
        public SProduct()
        {
            DRecipeProducts = new HashSet<DRecipeProduct>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual SProductCategory Category { get; set; }
        public virtual ICollection<DRecipeProduct> DRecipeProducts { get; set; }
    }
}
