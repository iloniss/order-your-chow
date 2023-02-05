export interface RecipesListItem {
  recipeId: number;
  categoryId: number;
  categoryName: string;
  recipeName: string;
  duration: string;
  rating: number;
  favourite: boolean;
  mainImage: string;
}
