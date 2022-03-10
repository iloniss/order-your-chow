namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeDTO
    {
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int RecipeCategoryId { get; set; }
        public bool Meat { get; set; } = false;
        public string MainImage { get; set; }
    }

    public sealed class EmptyRecipeDTO : RecipeDTO
    {

    }
}
