namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeCategoryDTO
    {
        public int? RecipeCategoryId { get; set; }
        public string Name { get; set; }

        public sealed class EmptyRecipeCategoryDTO : RecipeCategoryDTO
        {

        }
    }
}

