using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Mappings.CRM.Recipe;
using OrderYourChow.Repositories.Tests.Shared;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.RecipeProductMeasure
{
    public class RecipeProductMeasureBase
    {
        public OrderYourChowContext OrderYourChowContext { get; set; }
        public IMapper Mapper { get; set; }

        public RecipeProductMeasureBase()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeProfile>();
            }));
            OrderYourChowContext = new OrderYourChowContext(new TestOrderYourChowContext().GetTestContextOptions());
            Clear();
        }

        protected void Clear()
        {
            OrderYourChowContext.SProductMeasures.RemoveRange(OrderYourChowContext.SProductMeasures);
            OrderYourChowContext.SaveChanges();
        }

        protected SProductMeasure Seed()
        {
            SProductMeasure productMeasure = new() { Name = "kilogram" };
            OrderYourChowContext.SProductMeasures.Add(productMeasure);
            OrderYourChowContext.SaveChanges();

            return productMeasure;
        }
    }
}
