using FluentValidation;
using OrderYourChow.CORE.Models.CRM.Recipe;

namespace OrderYourChow.CORE.Validators.CRM.Recipe
{
    public class RecipeValidator : AbstractValidator<RecipeDTO>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Duration).NotEmpty();
            RuleFor(x => x.RecipeCategoryId).NotEmpty();
        }
    }
}
