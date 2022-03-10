using FluentValidation;
using OrderYourChow.CORE.Models.API.User;

namespace OrderYourChow.CORE.Validators.API.User
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Proszę wpisać nazwę użytkownika.")
                .MaximumLength(40).WithMessage("Nazwa użytkownika jest zbyt długa.")
                .MinimumLength(3).WithMessage("Nazwa użytkownika jest zbyt krótka");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Proszę podać adres e-mail.")
                .EmailAddress().WithMessage("Niepoprawny adres e-mail");


                
            

        }
        
    }
}
