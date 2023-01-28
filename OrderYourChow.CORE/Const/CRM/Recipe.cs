namespace OrderYourChow.CORE.Const.CRM
{
    public static class Recipe
    {
        public const string NotFoundRecipe = "Przepis nie istnieje.";
        public const string UsedRecipe = "Przepis, który próbujesz usunąć jest używany w planie diety lub został dodany do ulubionych przez użytkownika.";
        public const string ExistedRecipe = "Przepis o takiej nazwie już istnieje.";
        
        #region RecipeProductMeasure
        public const string ExistedRecipeProductMeasure = "Podana miara produktu już istnieje.";
        public const string NotFoundRecipeProductMeasure = "Miara produktu nie istnieje.";
        public const string UsedRecipeProductMeasure = "Miara produktu jest już wykorzystana w przepisie.";
        #endregion
    }
}
