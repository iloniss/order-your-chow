import {
  Box,
  Button,
  Card,
  CardContent,
  CardHeader,
  Divider,
  Paper,
  TextareaAutosize,
  Typography
} from '@mui/material';
import { ChangeEvent, FC, useState } from 'react';
import { Recipe } from 'src/models/recipe/recipe';
import RecipeBaseInfo from '../RecipeBaseInfo';
import 'src/styles.css';
import { RecipeProduct } from 'src/models/recipe/recipe_product';
import { ProductMeasure } from 'src/models/product_measure';
import { Product } from 'src/models/product';
import RecipeIngredients from '../RecipeIngredients';
import recipeService from 'src/services/recipeService';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import { RecipeProductArray } from 'src/models/recipe/recipe_product_array';

interface EditRecipeProps {
  recipeInfo: Recipe;
  setRecipeInfo: React.Dispatch<React.SetStateAction<Recipe>>;
  recipeProducts: RecipeProduct[];
  setRecipeProducts: React.Dispatch<React.SetStateAction<RecipeProduct[]>>;
  products: Product[];
  productMeasures: ProductMeasure[];
  recipeCategories: RecipeCategory[];
}
const EditRecipeCard: FC<EditRecipeProps> = ({
  recipeInfo,
  recipeProducts,
  setRecipeProducts,
  setRecipeInfo,
  products,
  productMeasures,
  recipeCategories
}) => {
  const [formErrorName, setFormErrorName] = useState(true);
  const [formErrorDuration, setFormErrorDuration] = useState(true);
  const [formErrorCategory, setFormErrorCategory] = useState(true);
  const [formErrorImage, setFormErrorImage] = useState(true);
  const [ingredientError, setIngredientError] = useState<boolean>(false);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setRecipeInfo({
      ...recipeInfo,
      [event.target.name]: event.target.value
    });
  };

  const handleChangeDescription = (
    event: ChangeEvent<HTMLTextAreaElement>
  ): void => {
    setRecipeInfo((prevState) => ({
      ...prevState,
      description: event.target.value
    }));
  };

  const handleSubmitRecipeInfo = async (e) => {
    e.preventDefault();
    if (recipeInfo.name === '') {
      setFormErrorName(false);
    } else {
      setFormErrorName(true);
    }

    if (recipeInfo.recipeCategoryId === 0) {
      setFormErrorCategory(false);
    } else {
      setFormErrorCategory(true);
    }

    if (recipeInfo.duration === 0) {
      setFormErrorDuration(false);
    } else {
      setFormErrorDuration(true);
    }

    const loginFromData = new FormData();
    loginFromData.append(
      'recipeCategoryId',
      recipeInfo.recipeCategoryId.toString()
    );
    loginFromData.append(
      'recipeCategoryId',
      recipeInfo.recipeCategoryId.toString()
    );
    loginFromData.append('recipeId', recipeInfo.recipeId.toString());
    loginFromData.append('name', recipeInfo.name);
    loginFromData.append('duration', recipeInfo.duration.toString());
    loginFromData.append('meat', recipeInfo.meat.toString());
    loginFromData.append(
      'ImageFile',
      (document.querySelector("input[name='image']") as HTMLInputElement)
        .files[0]
    );
    var result = await recipeService.updateRecipe(loginFromData);
  };

  const handleSubmitRecipeIngredients = async (e) => {
    e.preventDefault();
    const recipeProductArray: RecipeProductArray = {
      recipeProductList: recipeProducts
    };
    var result = await recipeService.postRecipeProduct(
      recipeProductArray,
      recipeInfo.recipeId
    );
  };

  const handleSubmitRecipeDescription = async (e) => {
    e.preventDefault();

    const loginFromData = new FormData();
    loginFromData.append('recipeId', recipeInfo.recipeId.toString());
    loginFromData.append('description', recipeInfo.description);

    var result = await recipeService.putRecipeDescription(loginFromData);
  };

  return (
    <Card>
      <CardHeader title="Edytuj przepis" />
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
          <Paper elevation={2} className="paperOption">
            <Typography variant="h5" className="title">
              Edytuj podstawowe informacje o przepisie
            </Typography>
            <RecipeBaseInfo
              formValue={recipeInfo}
              setFormValue={setRecipeInfo}
              formErrorDuration={formErrorDuration}
              formErrorImage={formErrorImage}
              formErrorName={formErrorName}
              formErrorCategory={formErrorCategory}
              handleChange={handleChange}
              recipeCategories={recipeCategories}
            />
            <div style={{ margin: 20, marginLeft: 400 }}>
              <Button
                variant="contained"
                size="large"
                disabled={ingredientError}
                onClick={handleSubmitRecipeInfo}
              >
                Zapisz
              </Button>
            </div>
          </Paper>

          <Paper elevation={2} className="paperOption">
            <Typography variant="h5" className="title">
              Edytuj składniki dodane do przepisu
            </Typography>
            <RecipeIngredients
              recipeProducts={recipeProducts}
              setRecipeProducts={setRecipeProducts}
              productMeasures={productMeasures}
              products={products}
              setIngredientError={setIngredientError}
            />
            <div style={{ margin: 20, marginLeft: 400 }}>
              <Button
                variant="contained"
                size="large"
                onClick={handleSubmitRecipeIngredients}
              >
                Zapisz
              </Button>
            </div>
          </Paper>
          <Paper elevation={2} className="paperOption">
            <Typography variant="h5" className="title">
              Edytuj opis
            </Typography>
            <TextareaAutosize
              className="textArea"
              onChange={handleChangeDescription}
              value={recipeInfo.description}
            />
            <div style={{ margin: 20, marginLeft: 400 }}>
              <Button
                variant="contained"
                size="large"
                onClick={handleSubmitRecipeDescription}
              >
                Zapisz
              </Button>
            </div>
          </Paper>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EditRecipeCard;
