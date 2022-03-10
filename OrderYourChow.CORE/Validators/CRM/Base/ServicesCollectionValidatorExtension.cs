using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.CORE.Validators.CRM.Product;
using OrderYourChow.CORE.Validators.CRM.Recipe;

namespace OrderYourChow.CORE.Validators.CRM.Base
{
    public static class ServicesCollectionValidatorExtension
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ProductCategoryDTO>, ProductCategoryValidator>();
            services.AddTransient<IValidator<AddProductDTO>, ProductValidator>();
            services.AddTransient<IValidator<RecipeDTO>, RecipeValidator>();
            services.AddTransient<IValidator<RecipeDescriptionDTO>, RecipeDescriptionValidator>();
            services.AddTransient<IValidator<RecipeProductDTO>, RecipeProductValidator>();
            services.AddTransient<IValidator<RecipeProductMeasureDTO>, RecipeProductMeasureValidator>();
        }
    }
}
