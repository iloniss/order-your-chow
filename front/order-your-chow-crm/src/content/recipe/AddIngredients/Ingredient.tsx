import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import { Autocomplete, TextField, Button } from '@mui/material';
import { FC, ChangeEvent, Key } from 'react';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';

interface IngredientCardProps {
  index: Key;
  products: Product[];
  productMeasures: ProductMeasure[];
  onDeleteIngredient: (id: Key) => void;
  valueIngredient: {
    id: number;
    productId: number;
    productMeasureId: number;
    count: number;
  };
  ingredientForm: {
    id: number;
    productId: number;
    productMeasureId: number;
    count: number;
  }[];
  setIngredientForm: React.Dispatch<
    React.SetStateAction<
      {
        id: number;
        productId: number;
        productMeasureId: number;
        count: number;
      }[]
    >
  >;
}

const Ingredient: FC<IngredientCardProps> = ({
  index,
  products,
  productMeasures,
  onDeleteIngredient,
  valueIngredient,
  ingredientForm,
  setIngredientForm
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

  const handleChangeCount = (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    index
  ): void => {
    var data = [...ingredientForm];
    data[index]['count'] = +event.target.value;
    setIngredientForm(data);
  };

  const handleChangeProduct = (event, value, index): void => {
    let data = [...ingredientForm];
    data[index]['productId'] = value.id;
    console.log(data);
    setIngredientForm(data);
  };

  const handleChangeMeasure = (event, value, index): void => {
    let data = [...ingredientForm];
    data[index]['productMeasureId'] = value.id;
    setIngredientForm(data);
  };

  const deleteIngredient = () => {
    onDeleteIngredient(ingredientForm[index].id);
  };

  return (
    <li
      className={'ingredient' + index.toString()}
      style={{ display: 'flex', marginLeft: 10, alignItems: 'center' }}
    >
      <Autocomplete
        disableClearable
        options={optionsProduct}
        onChange={(event, value) => handleChangeProduct(event, value, index)}
        renderInput={(params) => (
          <TextField
            {...params}
            label="Produkty"
            value={valueIngredient.productId}
            name="productId"
          />
        )}
      />
      <Autocomplete
        disableClearable
        options={optionsMeasure}
        onChange={(event, value) => handleChangeMeasure(event, value, index)}
        renderInput={(params) => (
          <TextField
            {...params}
            label="Miara"
            value={valueIngredient.productMeasureId}
            name="productMeasureId"
          />
        )}
      />
      <div>
        <TextField
          label="Ilość"
          name="count"
          onChange={(event) => handleChangeCount(event, index)}
        />
      </div>
      <Button onClick={deleteIngredient}>
        <DeleteForeverIcon fontSize="large" />
      </Button>
    </li>
  );
};

export default Ingredient;
