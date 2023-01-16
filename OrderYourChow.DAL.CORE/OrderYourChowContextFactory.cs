using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OrderYourChow.DAL.CORE.Models;
using System.IO;

namespace OrderYourChow.DAL.CORE
{
    public class OrderYourChowContextFactory : IDesignTimeDbContextFactory<OrderYourChowContext>
    {
        public OrderYourChowContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<OrderYourChowContext>();

            var connectionString = configuration.GetConnectionString("OrderYourChow");

            dbContextBuilder.UseSqlServer(connectionString);

            return new OrderYourChowContext(dbContextBuilder.Options);
        }
    }
}
