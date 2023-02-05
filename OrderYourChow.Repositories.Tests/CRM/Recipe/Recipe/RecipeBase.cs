using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Mappings.CRM.Recipe;
using OrderYourChow.Repositories.Tests.Shared;

namespace OrderYourChow.Repositories.Tests.CRM.Recipe.Recipe
{
    public class RecipeBase
    {
        public OrderYourChowContext OrderYourChowContext { get; set; }
        public IMapper Mapper { get; set; }

        public RecipeBase()
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
            OrderYourChowContext.DRecipes.RemoveRange(OrderYourChowContext.DRecipes);
            OrderYourChowContext.SRecipeCategories.RemoveRange(OrderYourChowContext.SRecipeCategories);
            OrderYourChowContext.SaveChanges();
        }
    }
}
