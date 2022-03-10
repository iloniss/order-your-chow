using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class SProductCategory
    {
        public SProductCategory()
        {
            SProducts = new HashSet<SProduct>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<SProduct> SProducts { get; set; }
    }
}
