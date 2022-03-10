using System;
using System.Collections.Generic;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class DShoppingList
    {
        public int ShoppingListId { get; set; }
        public int RecipeId { get; set; }
        public int ShoppingId { get; set; }
        public string Syslog { get; set; }
        public DateTime Sysdate { get; set; }

        public virtual DRecipe Recipe { get; set; }
        public virtual DShopping Shopping { get; set; }
    }
}
