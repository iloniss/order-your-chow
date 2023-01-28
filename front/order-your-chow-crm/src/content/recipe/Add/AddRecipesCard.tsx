import {
  TextField,
  Card,
  CardHeader,
  Divider,
  CardContent,
  Box,
  Button
} from '@mui/material';
import 'src/styles.css';
import MenuItem from '@mui/material/MenuItem';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import { useState, FC, ChangeEvent } from 'react';
import RecipeService from 'src/services/recipeService';
import { Recipe } from 'src/models/recipe/recipe';
import RecipeBaseInfo from '../RecipeBaseInfo';

interface AddRecipesCardProps {
  recipeCategories: RecipeCategory[];
}

const AddRecipesCard: FC<AddRecipesCardProps> = ({ recipeCategories }) => {
  const categoryOptions = [
    {
      id: '0',
      name: 'Wszystkie'
    }
  ];

  recipeCategories.forEach((element) => {
    categoryOptions.push({
      id: element.recipeCategoryId.toString(),
      name: element.name
    });
  });

  const [formValue, setFormValue] = useState<Recipe>({
    recipeCategoryId: 0,
    name: '',
    description: '',
    recipeId: 0,
    duration: 0,
    meat: false,
    mainImage: ''
  });

  const [formErrorName, setFormErrorName] = useState(true);
  const [formErrorDuration, setFormErrorDuration] = useState(true);
  const [formErrorCategory, setFormErrorCategory] = useState(true);
  const [formErrorImage, setFormErrorImage] = useState(true);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setFormValue({
      ...formValue,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formValue.name === '') {
      setFormErrorName(false);
    } else {
      setFormErrorName(true);
    }

    if (formValue.recipeCategoryId === 0) {
      setFormErrorCategory(false);
    } else {
      setFormErrorCategory(true);
    }

    if (formValue.recipeCategoryId === 0) {
      setFormErrorDuration(false);
    } else {
      setFormErrorDuration(true);
    }

    if (
      (document.querySelector("input[name='image']") as HTMLInputElement).files
        .length === 0
    ) {
      setFormErrorImage(false);
    } else {
      setFormErrorImage(true);
    }

    if (
      formErrorCategory === false ||
      formErrorImage === false ||
      formErrorName === false
    )
      return;

    const loginFromData = new FormData();
    loginFromData.append(
      'recipeCategoryId',
      formValue.recipeCategoryId.toString()
    );
    loginFromData.append('name', formValue.name);
    loginFromData.append('duration', formValue.duration.toString());
    loginFromData.append('meat', formValue.meat.toString());
    loginFromData.append(
      'ImageFile',
      (document.querySelector("input[name='image']") as HTMLInputElement)
        .files[0]
    );
    var result = await RecipeService.postRecipe(loginFromData);

    if (Number.isFinite(result)) {
      window.location.href = '/recipe/add/ingredients';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Nowy przepis" />
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
          <RecipeBaseInfo
            formValue={formValue}
            setFormValue={setFormValue}
            formErrorDuration={formErrorDuration}
            formErrorImage={formErrorImage}
            formErrorName={formErrorName}
            handleChange={handleChange}
            recipeCategories={recipeCategories}
            formErrorCategory={formErrorCategory}
          />
          <Button
            type="submit"
            style={{ width: 200, marginLeft: 250, marginTop: 30 }}
            sx={{ margin: 1 }}
            variant="contained"
            size="large"
            color="primary"
            onClick={async (e) => {
              await handleSubmit(e);
            }}
          >
            Dodaj przepis
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddRecipesCard;
