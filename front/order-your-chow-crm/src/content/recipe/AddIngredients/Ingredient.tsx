import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Autocomplete, TextField, Button } from '@mui/material';
import { FC, ChangeEvent, Key, useState } from 'react';
import { Product } from 'src/models/product';
import { ProductStatus } from 'src/models/product_status';
import { ProductMeasure } from 'src/models/product_measure';
import { RecipeProduct } from 'src/models/recipe/recipe_product';
import 'src/styles.css';

interface IngredientCardProps {
  index: Key;
  products: Product[];
  productMeasures: ProductMeasure[];
  onDeleteIngredient: (id: Key) => void;
  valueIngredient: RecipeProduct;
  recipeProducts: RecipeProduct[];
  setRecipeProducts: React.Dispatch<React.SetStateAction<RecipeProduct[]>>;
  isError: (error: boolean) => void;
}

const Ingredient: FC<IngredientCardProps> = ({
  index,
  products,
  productMeasures,
  onDeleteIngredient,
  valueIngredient,
  recipeProducts,
  setRecipeProducts,
  isError
}) => {
  const optionsMeasure: { id: number; label: string }[] = productMeasures.map(
    (option) => ({
      id: option.productMeasureId,
      label: option.name
    })
  );

  const optionsProduct: { id: number; label: string }[] = products.map(
    (option) => ({
      id: option.productId,
      label: option.name
    })
  );

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
    let obj = data.find((item) => item.productId === value.id);
    if (!obj) {
      data[index]['productId'] = value.id;
      setProductError(false);
      isError(false);
    } else {
      setProductError(true);
      isError(true);
    }
    if (data[index]['recipeProductId'] !== 0)
      data[index]['status'] = ProductStatus.Updated;
    setRecipeProducts(data);
  };

  const handleChangeMeasure = (event, value, index): void => {
    let data = [...recipeProducts];
    data[index]['productMeasureId'] = value.id;
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
          onChange={(event, value) => handleChangeProduct(event, value, index)}
          renderInput={(params) => (
            <TextField {...params} label="Produkty" name="productId" />
          )}
          value={
            valueIngredient.productId !== 0
              ? {
                  id: valueIngredient.productId,
                  label: optionsProduct?.find(
                    (product) => product.id === valueIngredient.productId
                  ).label
                }
              : { id: '', label: '' }
          }
        />
        <Autocomplete
          disableClearable
          options={optionsMeasure}
          onChange={(event, value) => handleChangeMeasure(event, value, index)}
          renderInput={(params) => (
            <TextField label="Miara" {...params} name="productMeasureId" />
          )}
          value={
            valueIngredient.productMeasureId !== 0
              ? {
                  id: valueIngredient.productMeasureId,
                  label: optionsMeasure?.find(
                    (measure) => measure.id === valueIngredient.productMeasureId
                  ).label
                }
              : { id: '', label: '' }
          }
        />
        <div>
          <TextField
            name="count"
            label={'Ilość'}
            onChange={(event) => handleChangeCount(event, index)}
            value={valueIngredient.count === 0 ? '' : valueIngredient.count}
          />
        </div>
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
