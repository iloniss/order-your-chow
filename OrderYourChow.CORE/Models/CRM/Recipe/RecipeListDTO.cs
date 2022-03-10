namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeListDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public bool Favourite { get; set; }
        public string Image { get; set; }
    }
}
