using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DShopping
    {
        public DShopping()
        {
            DShoppingLists = new HashSet<DShoppingList>();
        }

        public int ShoppingId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Cost { get; set; }
        public string Shop { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual ICollection<DShoppingList> DShoppingLists { get; set; }
    }
}
