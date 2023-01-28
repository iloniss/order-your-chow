export interface Recipe {
  recipeId: number;
  name: string;
  description: string;
  duration: number;
  recipeCategoryId: number;
  meat: boolean;
  mainImage: string;
}
