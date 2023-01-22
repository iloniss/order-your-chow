using AutoMapper;
using OrderYourChow.Repositories.Mappings.CRM.Product;

namespace OrderYourChow.CRM.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddAppAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> mapperExpression = null)
        {
            services.AddAutoMapper((provider, cfg) =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AllowNullCollections = true;
            },
             typeof(ProductProfile).Assembly);
        }
    }
}
