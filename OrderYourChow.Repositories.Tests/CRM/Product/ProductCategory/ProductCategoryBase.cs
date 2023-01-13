using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Mappings.CRM.Product;
using OrderYourChow.Repositories.Tests.Shared;

namespace OrderYourChow.Repositories.Tests.CRM.Product.ProductCategory
{
    public class ProductCategoryBase
    {
        public OrderYourChowContext OrderYourChowContext { get; set; }
        public IMapper Mapper { get; set; }
        public ProductCategoryBase()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            }));
            OrderYourChowContext = new OrderYourChowContext(new TestOrderYourChowContext().GetTestContextOptions());
            Clear();
        }

        protected void Clear()
        {
            OrderYourChowContext.SProductCategories.RemoveRange(OrderYourChowContext.SProductCategories);
            OrderYourChowContext.SaveChanges();
        }
    }
}
