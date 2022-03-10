namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeProductMeasureDTO
    {
        public int? ProductMeasureId { get; set; }
        public string Name { get; set; }
    }

    public sealed class EmptyRecipeProductMeasureDTO : RecipeProductMeasureDTO
    {

    }
    public sealed class ErrorRecipeProductMeasureDTO : RecipeProductMeasureDTO
    {

    }
}
