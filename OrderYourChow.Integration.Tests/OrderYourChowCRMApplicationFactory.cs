using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Integration.Tests
{
    public class OrderYourChowCRMApplicationFactory<TStartup> :
        WebApplicationFactory<Program> where TStartup : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddEnvironmentVariables()
                 .Build();

                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OrderYourChowContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<OrderYourChowContext>((options, context) =>
                {
                    context.UseSqlServer(
                        configuration.GetConnectionString("OrderYourChowTest"));
                });
            });
        }
    }
}
