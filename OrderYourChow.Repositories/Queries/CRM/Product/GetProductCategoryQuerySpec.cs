using LinqKit;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using System.Linq.Expressions;

namespace OrderYourChow.Repositories.Queries.CRM.Product
{
    public static class GetProductCategoryQuerySpec 
    {
        public static Expression<Func<SProductCategory, bool>> Filter(GetProductCategoryQuery getProductCategoryQuery)
        {
            var predicate = PredicateBuilder.New<SProductCategory>();

            if(!string.IsNullOrEmpty(getProductCategoryQuery.Name))
                predicate = predicate.And(productCategory => productCategory.Name == getProductCategoryQuery.Name);

            if(getProductCategoryQuery.ProductCategoryId != null)
                predicate = predicate.And(productCategory => productCategory.ProductCategoryId == getProductCategoryQuery.ProductCategoryId);

            return predicate;
        }
    }
}
