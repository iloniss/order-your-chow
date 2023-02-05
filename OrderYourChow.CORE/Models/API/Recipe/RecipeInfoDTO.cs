namespace OrderYourChow.CORE.Models.API.Recipe
{
    public class RecipeInfoDTO
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string RecipeName { get; set;}
        public string Duration { get; set;}
        public decimal? Rating { get; set;}
        public bool Favourite { get; set;}
        public string MainImage { get; set;}
        public bool Eaten { get; set; }

    }
}
