using LinqKit;
using OrderYourChow.CORE.Queries.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using System;
using System.Linq.Expressions;

namespace OrderYourChow.Repositories.Queries.CRM.Product
{
    public static class GetProductQuerySpec
    {
        public static Expression<Func<SProduct, bool>> Filter(GetProductQuery getProductQuery)
        {
            var predicate = PredicateBuilder.New<SProduct>();

            if(getProductQuery.ProductCategoryId != null)
                predicate = predicate.And(product => product.CategoryId == getProductQuery.ProductCategoryId);

            if (string.IsNullOrEmpty(getProductQuery.Name))
                predicate = predicate.And(product => product.Name == getProductQuery.Name);

            if(getProductQuery.ProductId != null)
                predicate = predicate.And(product => product.ProductId == getProductQuery.ProductId);

            return predicate;
        }
    }
}
