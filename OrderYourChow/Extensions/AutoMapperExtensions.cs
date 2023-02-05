using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrderYourChow.CORE.Mappings.API.Calendar;
using System;

namespace OrderYourChow.Extensions
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
             typeof(CalendarProfile).Assembly);
        }
    }
}
