import { ProductStatus } from './ProductStatus';

export interface RecipeProduct {
  recipeProductId: number;
  productId: number;
  productMeasureId: number;
  count: number;
  status?: ProductStatus;
}
