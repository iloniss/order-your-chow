import { Product } from '../product';
import { ProductMeasure } from '../product_measure';
import { ProductStatus } from '../product_status';

export interface RecipeProduct {
  recipeProductId: number;
  productId: number;
  productMeasureId: number;
  count: number;
  status?: ProductStatus;
}

export interface RecipeProductAdditional {
  recipeProductId: number;
  productId: number;
  nameProduct: Product;
  productMeasureId: number;
  nameMeasure: ProductMeasure;
  count: number;
  status?: ProductStatus;
}
