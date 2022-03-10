using FluentValidation;
using OrderYourChow.CORE.Models.CRM.Product;

namespace OrderYourChow.CORE.Validators.CRM.Product
{
    public class ProductCategoryValidator : AbstractValidator<ProductCategoryDTO>
    {
        public ProductCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Należy podać nazwę kategorii.");
        }
    }
}
