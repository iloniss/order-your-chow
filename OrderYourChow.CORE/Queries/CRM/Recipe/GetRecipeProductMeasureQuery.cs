namespace OrderYourChow.CORE.Queries.CRM.Recipe
{
    public class GetRecipeProductMeasureQuery
    {
        public GetRecipeProductMeasureQuery(string name)
        {
            Name = name;
        }
        public GetRecipeProductMeasureQuery(int? recipeProductMeasureId)
        {
            RecipeProductMeasureId = recipeProductMeasureId;
        }

        public string Name { get; }
        public int? RecipeProductMeasureId { get; }
    }
}
