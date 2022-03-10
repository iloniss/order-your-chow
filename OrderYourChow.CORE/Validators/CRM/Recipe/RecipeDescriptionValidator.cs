using FluentValidation;
using OrderYourChow.CORE.Models.CRM.Recipe;

namespace OrderYourChow.CORE.Validators.CRM.Recipe
{
    public class RecipeDescriptionValidator : AbstractValidator<RecipeDescriptionDTO>
    {
        public RecipeDescriptionValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
