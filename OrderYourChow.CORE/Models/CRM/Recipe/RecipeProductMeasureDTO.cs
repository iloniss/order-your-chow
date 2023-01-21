using OrderYourChow.CORE.Models.Shared.Error;

namespace OrderYourChow.CORE.Models.CRM.Recipe
{
    public class RecipeProductMeasureDTO
    {
        public int? ProductMeasureId { get; set; }
        public string Name { get; set; }
    }

    public sealed class EmptyRecipeProductMeasureDTO : RecipeProductMeasureDTO, IErrorOperationDto
    {
        public EmptyRecipeProductMeasureDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
    public sealed class ErrorRecipeProductMeasureDTO : RecipeProductMeasureDTO, IErrorOperationDto
    {
        public ErrorRecipeProductMeasureDTO(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public sealed class DeletedRecipeProductMeasureDTO : RecipeProductMeasureDTO
    {
    }
    public sealed class UpdatedRecipeProductMeasureDTO : RecipeProductMeasureDTO
    {
    }
    public sealed class CreatedRecipeProductMeasureDTO : RecipeProductMeasureDTO
    {
    }
}
