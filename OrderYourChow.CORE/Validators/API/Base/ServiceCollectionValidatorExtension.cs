using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderYourChow.CORE.Models.API.User;
using OrderYourChow.CORE.Validators.API.User;

namespace OrderYourChow.CORE.Validators.API.Base
{
    public static class ServiceCollectionValidatorExtension
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserDTO>, UserValidator>();
           
        }
    }
}
