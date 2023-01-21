using OrderYourChow.CORE.Models.Shared.Error;

namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeDTO
    {
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int RecipeCategoryId { get; set; }
        public bool Meat { get; set; } = false;
        public string MainImage { get; set; }
    }

    public sealed class EmptyRecipeDTO : RecipeDTO, IErrorOperationDto
    {
        public EmptyRecipeDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }

    }

    public sealed class ErrorRecipeDTO : RecipeDTO, IErrorOperationDto
    {
        public ErrorRecipeDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }

    }
    public sealed class DeletedRecipeDTO : RecipeDTO
    {
    }
    public sealed class UpdatedRecipeDTO : RecipeDTO
    {
    }
    public sealed class CreatedRecipeDTO : RecipeDTO
    {
    }
}
