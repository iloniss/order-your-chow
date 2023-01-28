using LinqKit;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using System.Linq.Expressions;

namespace OrderYourChow.Repositories.Queries.CRM.Recipe
{
    public static class GetRecipeProductMeasureQuerySpec
    {
        public static Expression<Func<SProductMeasure, bool>> Filter(GetRecipeProductMeasureQuery getRecipeProductMeasureQuery)
        {
            var predicate = PredicateBuilder.New<SProductMeasure>();

            if (!string.IsNullOrEmpty(getRecipeProductMeasureQuery.Name))
                predicate = predicate.And(recipreProductMeasure => recipreProductMeasure.Name == getRecipeProductMeasureQuery.Name);

            if (getRecipeProductMeasureQuery.RecipeProductMeasureId != null)
                predicate = predicate.And(productCategory => productCategory.ProductMeasureId == getRecipeProductMeasureQuery.RecipeProductMeasureId);

            return predicate;
        }
    }
}
