using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Mappings.CRM.Product;
using OrderYourChow.Repositories.Tests.Shared;

namespace OrderYourChow.Repositories.Tests.CRM.Product.Product
{
    public class ProductBase
    {
        public OrderYourChowContext OrderYourChowContext { get; set; }
        public IMapper Mapper { get; set; }

        public ProductBase()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            }));
            OrderYourChowContext = new OrderYourChowContext(new TestOrderYourChowContext().GetTestContextOptions());
            Clear();
            Seed();
        }

        protected void Clear()
        {
            OrderYourChowContext.SProducts.RemoveRange(OrderYourChowContext.SProducts);
            OrderYourChowContext.SProductCategories.RemoveRange(OrderYourChowContext.SProductCategories);
            OrderYourChowContext.SaveChanges();
        }

        protected void Seed()
        {
            OrderYourChowContext.SProductCategories.Add(new SProductCategory() { Name = "Beverages" });
            OrderYourChowContext.SaveChanges();
        }
    }
}
