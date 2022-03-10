namespace OrderYourChow.CORE.Models.API.Recipe
{
    public class RecipeExchangeDTO
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int Duration { get; set; }
        public decimal? Rating { get; set; }
        public bool Favourite { get; set; }

    }
}
