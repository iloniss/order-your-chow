using LinqKit;
using OrderYourChow.CORE.Queries.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using System.Linq.Expressions;


namespace OrderYourChow.Repositories.Queries.CRM.Recipe
{
    public static class GetRecipeQuerySpec
    {
        public static Expression<Func<DRecipe, bool>> Filter(GetRecipeQuery getRecipeQuery)
        {
            var predicate = PredicateBuilder.New<DRecipe>();

            if (!string.IsNullOrEmpty(getRecipeQuery.Name))
                predicate = predicate.And(recipe => recipe.Name == getRecipeQuery.Name);

            if(getRecipeQuery.RecipeId != null)
                predicate = predicate.And(recipe => recipe.RecipeId == getRecipeQuery.RecipeId);

            return predicate;
        }
    }
}
