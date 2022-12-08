import {
  Card,
  CardContent,
  CardHeader,
  Divider,
  Box,
  MenuItem,
  TextField,
  Button
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { FC, useState, ChangeEvent } from 'react';
import { Recipe } from 'src/models/recipe';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';
import Ingredient from './Ingredient';
import recipeService from 'src/services/recipeService';
import 'src/styles.css';

interface AddIngredientsCardProps {
  recipes: Recipe[];
  products: Product[];
  productMeasures: ProductMeasure[];
}
const AddIngredientsCard: FC<AddIngredientsCardProps> = ({
  recipes,
  products,
  productMeasures
}) => {
  const [selectedRecipe, setSelectedRecipe] = useState<String>();

  const [formErrorRecipe, setFormErrorRecipe] = useState<boolean>(true);

  const [ingredientForm, setIngredientForm] = useState<
    {
      id: number;
      productId: number;
      productMeasureId: number;
      count: number;
    }[]
  >([{ id: Math.random(), productId: 0, productMeasureId: 0, count: 0 }]);

  const handleChangeRecipe = (event: ChangeEvent<HTMLInputElement>): void => {
    setSelectedRecipe(event.target.id);
  };

  const onAddNextIngredient = () => {
    let newIngredient = {
      id: Math.random(),
      productId: 0,
      productMeasureId: 0,
      count: 0
    };
    setIngredientForm((prevState) => {
      return [...prevState, newIngredient];
    });
    console.log(ingredientForm);
  };

  const onDeleteIngredient = (key: number): void => {
    const updatedIngredientKey = ingredientForm.filter(
      (ingredient) => ingredient.id !== key
    );
    setIngredientForm(updatedIngredientKey);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(selectedRecipe);
    if (selectedRecipe === undefined) {
      setFormErrorRecipe(false);
    } else {
      setFormErrorRecipe(true);
    }

    const loginFromData = new FormData();
    ingredientForm.forEach((ingredient) => {
      loginFromData.append('productId', ingredient.productId.toString());
      loginFromData.append(
        'productMeasureId',
        ingredient.productMeasureId.toString()
      );
      loginFromData.append('count', ingredient.count.toString());
    });

    var result = await recipeService.postRecipeProduct(
      loginFromData,
      +selectedRecipe
    );
    console.log(result);
    if (result == null) {
      window.location.href = '/product/actions';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Dodaj składniki do przepisu" />
      <Divider />
      <CardContent>
        <Box
          component="form"
          sx={{
            '& .MuiTextField-root': { m: 1, width: '25ch' }
          }}
          noValidate
          autoComplete="off"
        >
          <TextField
            select
            required
            label="Wybierz przepis"
            style={{ width: 690, margin: 20, marginTop: 5, marginBottom: 10 }}
            name="recipeId"
            onChange={handleChangeRecipe}
          >
            {recipes.map((recipe) => (
              <MenuItem key={recipe.recipeId} value={recipe.name}>
                {recipe.name}
              </MenuItem>
            ))}
          </TextField>
          {!formErrorRecipe && (
            <div className="errorsForm">
              Należy wybrać przepis, do którego chcesz dodać składniki.
            </div>
          )}
          <ul className="ingredients" style={{ padding: 0 }}>
            {ingredientForm.map((ingredient, index) => (
              <Ingredient
                key={index}
                index={index}
                products={products}
                productMeasures={productMeasures}
                valueIngredient={ingredient}
                ingredientForm={ingredientForm}
                setIngredientForm={setIngredientForm}
                onDeleteIngredient={onDeleteIngredient}
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
          <div style={{ margin: 20, marginLeft: 350 }}>
            <Button onClick={handleSubmit} variant="contained" size="large">
              Dodaj składniki
            </Button>
          </div>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddIngredientsCard;
