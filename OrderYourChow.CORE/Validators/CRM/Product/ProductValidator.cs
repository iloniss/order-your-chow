using FluentValidation;
using OrderYourChow.CORE.Models.CRM.Product;

namespace OrderYourChow.CORE.Validators.CRM.Product
{
    public class ProductValidator : AbstractValidator<AddProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Należy uzupełnić wymagane pola.");
            RuleFor(x => x.ProductCategoryId).NotEmpty().WithMessage("Należy uzupełnić wymagane pola.");
        }
    }
}
