import { ProductStatus } from '../product_status';

export interface RecipeProduct {
  recipeProductId: number;
  productId: number;
  productMeasureId: number;
  count: number;
  status?: ProductStatus;
}
