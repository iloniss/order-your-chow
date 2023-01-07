using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Integration.Tests
{
    public class TestOrderYourChowContext
    {
        public static DbContextOptions<OrderYourChowContext> GetTestContextOptions()
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddEnvironmentVariables()
                 .Build();

            return (DbContextOptions<OrderYourChowContext>?)new DbContextOptionsBuilder<OrderYourChowContext>()
                .UseSqlServer(configuration.GetConnectionString("OrderYourChowTest"))
                .Options;
        }
    }
}
