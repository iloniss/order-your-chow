using FluentValidation;
using OrderYourChow.CORE.Models.Shared.Recipe;

namespace OrderYourChow.CORE.Validators.CRM.Recipe
{
    public class RecipeProductValidator : AbstractValidator<RecipeProductDTO>
    {
        public RecipeProductValidator()
        {
            RuleFor(x => x.Count).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ProductMeasureId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
