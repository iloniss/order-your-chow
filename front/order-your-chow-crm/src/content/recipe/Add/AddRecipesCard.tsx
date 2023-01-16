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
import { AddRecipe } from 'src/models/add_recipe';
import Switch from '@mui/material/Switch';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';

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

  const [formValue, setformValue] = useState<AddRecipe>({
    recipeCategoryId: 0,
    name: '',
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
    setformValue({
      ...formValue,
      [event.target.name]: event.target.value
    });
  };

  const handleChangeSwitch = () => {
    setformValue({
      ...formValue,
      meat: !formValue.meat
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
          <span className="textForm">Wybierz kategorię przepisu</span>
          <div>
            <TextField
              id="outlined-select-currency"
              select
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="recipeCategoryId"
              value={formValue.recipeCategoryId}
              onChange={handleChange}
            >
              {recipeCategories.map((categoryOption) => (
                <MenuItem
                  key={categoryOption.recipeCategoryId}
                  value={categoryOption.recipeCategoryId}
                >
                  {categoryOption.name}
                </MenuItem>
              ))}
            </TextField>
            {!formErrorCategory && (
              <div className="errorsForm">Należy wybrać kategorię.</div>
            )}
          </div>
          <span className="textForm">Podaj nazwę przepisu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="name"
              id="outlined-required"
              value={formValue.name}
              onChange={handleChange}
            />
            {!formErrorName && (
              <div className="errorsForm">Należy podać nazwę przepisu.</div>
            )}
          </div>
          <span className="textForm">Podaj czas przygotowania przepisu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="duration"
              id="outlined-required"
              value={formValue.duration}
              onChange={handleChange}
              type="number"
            />
            {!formErrorDuration && (
              <div className="errorsForm">
                Należy czas przygotowania przepisu.
              </div>
            )}
          </div>
          <div
            style={{ width: 160, margin: 20, marginTop: 5, marginBottom: 10 }}
          >
            <FormGroup>
              <FormControlLabel
                style={{ margin: 0 }}
                control={
                  <Switch
                    checked={formValue.meat}
                    name="meat"
                    size="medium"
                    onChange={handleChangeSwitch}
                  />
                }
                label="Zawiera mięso"
                labelPlacement="start"
              />
            </FormGroup>
          </div>
          <span className="textForm">Dodaj zdjęcie przepisu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="image"
              id="outlined-required"
              type="file"
            />
            {!formErrorImage && (
              <div className="errorsForm">Należy dodać zdjęcie przepisu.</div>
            )}
          </div>
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
