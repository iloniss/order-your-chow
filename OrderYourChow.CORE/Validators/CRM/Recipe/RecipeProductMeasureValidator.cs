using FluentValidation;
using OrderYourChow.CORE.Models.CRM.Recipe;

namespace OrderYourChow.CORE.Validators.CRM.Recipe
{
    public class RecipeProductMeasureValidator : AbstractValidator<RecipeProductMeasureDTO>
    {
        public RecipeProductMeasureValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
