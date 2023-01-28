import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Autocomplete, TextField, Button } from '@mui/material';
import { FC, ChangeEvent, Key, useState } from 'react';
import { Product } from 'src/models/product';
import { ProductStatus } from 'src/models/product_status';
import { ProductMeasure } from 'src/models/product_measure';
import {
  RecipeProduct,
  RecipeProductAdditional
} from 'src/models/recipe/recipe_product';
import 'src/styles.css';

interface IngredientCardProps {
  index: Key;
  products: Product[];
  productMeasures: ProductMeasure[];
  onDeleteIngredient: (id: Key) => void;
  valueIngredient: RecipeProductAdditional;
  recipeProducts: RecipeProduct[];
  setRecipeProducts: React.Dispatch<React.SetStateAction<RecipeProduct[]>>;
  setIngredientError: React.Dispatch<React.SetStateAction<boolean>>;
}

const Ingredient: FC<IngredientCardProps> = ({
  index,
  products,
  productMeasures,
  onDeleteIngredient,
  valueIngredient,
  recipeProducts,
  setRecipeProducts,
  setIngredientError
}) => {
  const optionsMeasure = productMeasures.map((measure) => measure.name);

  const optionsProduct = products.map((product) => product.name);

  const [productError, setProductError] = useState<boolean>(false);

  const handleChangeCount = (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    index
  ): void => {
    var data = [...recipeProducts];
    data[index]['count'] = +event.target.value;
    if (data[index]['recipeProductId'] !== 0)
      data[index]['status'] = ProductStatus.Updated;
    setRecipeProducts(data);
  };

  const handleChangeProduct = (event, value, index): void => {
    let data = [...recipeProducts];
    console.log(value);
    let obj = data.find(
      (item) =>
        item.productId === products.find((x) => x.name === value)?.productId
    );
    if (obj !== null) {
      data[index]['productId'] = products.find(
        (x) => x.name === value
      )?.productId;
      setProductError(false);
      setIngredientError(false);
    } else {
      setProductError(true);
      setIngredientError(true);
    }
    if (data[index]['recipeProductId'] !== 0)
      data[index]['status'] = ProductStatus.Updated;
    setRecipeProducts(data);
    console.log(valueIngredient.nameMeasure);
  };

  const handleChangeMeasure = (event, value, index): void => {
    let data = [...recipeProducts];
    data[index]['productMeasureId'] = productMeasures.find(
      (x) => x.name === value
    )?.productMeasureId;
    if (data[index]['recipeProductId'] !== 0)
      data[index]['status'] = ProductStatus.Updated;
    setRecipeProducts(data);
  };

  const deleteIngredient = () => {
    onDeleteIngredient(recipeProducts[index].productId);
  };

  return (
    <div>
      <li
        className={'ingredient' + index.toString()}
        style={{ display: 'flex', marginLeft: 10, alignItems: 'center' }}
      >
        <Autocomplete
          disableClearable
          options={optionsProduct}
          value={valueIngredient.nameProduct?.name ?? ''}
          onChange={(event, value) => handleChangeProduct(event, value, index)}
          renderInput={(params) => (
            <TextField {...params} label="Produkty" name="productId" />
          )}
        />
        <Autocomplete
          options={optionsMeasure}
          value={valueIngredient.nameMeasure?.name ?? ''}
          disableClearable
          onChange={(event, value) => handleChangeMeasure(event, value, index)}
          renderInput={(params) => (
            <TextField label="Miara" {...params} name="productMeasureId" />
          )}
        />
        <TextField
          name="count"
          type="number"
          label={'Ilość'}
          onChange={(event) => handleChangeCount(event, index)}
          value={valueIngredient.count === 0 ? '' : valueIngredient.count}
        />
        <Button onClick={deleteIngredient}>
          <DeleteForeverIcon fontSize="large" />
        </Button>
      </li>
      <li style={{ listStyle: 'none' }}>
        {productError && (
          <span className="errorsForm">
            Wybrany produkt już został dodany do przepisu.
          </span>
        )}
      </li>
    </div>
  );
};

export default Ingredient;
