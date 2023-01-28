import { Button } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { FC, Fragment } from 'react';
import Ingredient from './AddIngredients/Ingredient';
import { ProductStatus } from 'src/models/product_status';
import { RecipeProduct } from 'src/models/recipe/recipe_product';
import { ProductMeasure } from 'src/models/product_measure';
import { Product } from 'src/models/product';

interface RecipeIngredientsProps {
  recipeProducts: RecipeProduct[];
  setRecipeProducts: React.Dispatch<React.SetStateAction<RecipeProduct[]>>;
  products: Product[];
  productMeasures: ProductMeasure[];
  setIngredientError: React.Dispatch<React.SetStateAction<boolean>>;
}
const RecipeIngredients: FC<RecipeIngredientsProps> = ({
  recipeProducts,
  setRecipeProducts,
  products,
  productMeasures,
  setIngredientError
}) => {
  const onAddNextIngredient = () => {
    let newIngredient = {
      recipeProductId: 0,
      productId: 0,
      productMeasureId: 0,
      count: 0,
      status: ProductStatus.New
    };
    setRecipeProducts((prevState) => {
      return [...prevState, newIngredient];
    });
  };

  const onDeleteIngredient = (key: number): void => {
    const updatedIngredient = recipeProducts.filter(
      (ingredient) => ingredient.productId !== key
    );
    setRecipeProducts(updatedIngredient);
  };

  const y = recipeProducts.map((product) => {
    return {
      recipeProductId: product.recipeProductId,
      productId: product.productId,
      nameProduct: products.find((x) => x.productId === product.productId),
      productMeasureId: product.productMeasureId,
      nameMeasure: productMeasures.find(
        (x) => x.productMeasureId === product.productMeasureId
      ),
      count: product.count,
      status: product.status
    };
  });

  return (
    <Fragment>
      <ul className="ingredients" style={{ padding: 0 }}>
        {y?.map((ingredient, index) => (
          <Ingredient
            key={index}
            index={index}
            products={products}
            productMeasures={productMeasures}
            valueIngredient={ingredient}
            recipeProducts={recipeProducts}
            setRecipeProducts={setRecipeProducts}
            onDeleteIngredient={onDeleteIngredient}
            setIngredientError={setIngredientError}
          />
        ))}
      </ul>
      <Button
        onClick={onAddNextIngredient}
        style={{ marginLeft: 15, marginTop: 10 }}
      >
        <AddIcon fontSize="large" />
        Dodaj następny produkt
      </Button>
    </Fragment>
  );
};

export default RecipeIngredients;
