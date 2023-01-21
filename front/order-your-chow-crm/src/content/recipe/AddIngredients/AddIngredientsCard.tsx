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
import { FC, useState, ChangeEvent } from 'react';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';
import recipeService from 'src/services/recipeService';
import 'src/styles.css';
import { RecipeProductArray } from 'src/models/recipe/recipe_product_array';
import { RecipeProduct } from 'src/models/recipe/recipe_product';
import { RecipesList } from 'src/models/recipe/recipes_list';
import RecipeIngredients from '../RecipeIngredients';

interface AddIngredientsCardProps {
  recipes: RecipesList[];
  products: Product[];
  productMeasures: ProductMeasure[];
  selectedRecipe: String;
  setSelectedRecipe: React.Dispatch<React.SetStateAction<String>>;
  recipeProducts: RecipeProduct[];
  setRecipeProducts: React.Dispatch<React.SetStateAction<RecipeProduct[]>>;
}
const AddIngredientsCard: FC<AddIngredientsCardProps> = ({
  recipes,
  products,
  productMeasures,
  selectedRecipe,
  setSelectedRecipe,
  recipeProducts,
  setRecipeProducts
}) => {
  const [formErrorRecipe, setFormErrorRecipe] = useState<boolean>(true);
  const [ingredientError, setIngredientError] = useState<boolean>(false);

  const handleChangeRecipe = (event: ChangeEvent<HTMLInputElement>): void => {
    setSelectedRecipe(event.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedRecipe === '') {
      setFormErrorRecipe(false);
      return;
    } else {
      setFormErrorRecipe(true);
    }

    const recipeProductArray: RecipeProductArray = {
      recipeProductList: recipeProducts
    };

    var result = await recipeService.postRecipeProduct(
      recipeProductArray,
      +selectedRecipe
    );

    if (result == null) {
      window.location.href = '/recipe/add/description';
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
            value={selectedRecipe}
          >
            {recipes?.map((recipe) => (
              <MenuItem key={recipe.recipeId} value={recipe.recipeId}>
                {recipe.name}
              </MenuItem>
            ))}
          </TextField>
          {!formErrorRecipe && (
            <div className="errorsForm">Należy wybrać kategorię.</div>
          )}
          <RecipeIngredients
            recipeProducts={recipeProducts}
            setRecipeProducts={setRecipeProducts}
            productMeasures={productMeasures}
            products={products}
            setIngredientError={setIngredientError}
          />
          <div style={{ margin: 20, marginLeft: 350 }}>
            <Button
              onClick={handleSubmit}
              variant="contained"
              size="large"
              disabled={ingredientError}
            >
              Dodaj składniki
            </Button>
          </div>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddIngredientsCard;
