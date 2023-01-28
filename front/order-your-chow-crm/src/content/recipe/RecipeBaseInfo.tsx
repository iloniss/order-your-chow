import {
  TextField,
  FormGroup,
  FormControlLabel,
  Switch,
  MenuItem,
  Box
} from '@mui/material';
import { ChangeEvent, FC, Fragment, useState } from 'react';
import { Recipe } from 'src/models/recipe/recipe';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import { recipesPath } from '../../http-common';

interface RecipeBaseInfoProps {
  formValue: Recipe;
  setFormValue: React.Dispatch<React.SetStateAction<Recipe>>;
  formErrorName: boolean;
  formErrorDuration: boolean;
  formErrorCategory: boolean;
  handleChange: (event: ChangeEvent<HTMLInputElement>) => void;
  formErrorImage: boolean;
  recipeCategories: RecipeCategory[];
}

const RecipeBaseInfo: FC<RecipeBaseInfoProps> = ({
  formValue,
  setFormValue,
  formErrorName,
  formErrorDuration,
  formErrorImage,
  handleChange,
  recipeCategories,
  formErrorCategory
}) => {
  const [file, setFile] = useState(undefined);
  const handleChangeFile = (event) => {
    setFile(URL.createObjectURL(event.target.files[0]));
  };

  const handleChangeSwitch = () => {
    setFormValue({
      ...formValue,
      meat: !formValue.meat
    });
  };
  return (
    <Fragment>
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
          <div className="errorsForm">Należy czas przygotowania przepisu.</div>
        )}
      </div>
      <div style={{ width: 160, margin: 20, marginTop: 5, marginBottom: 10 }}>
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
      {formValue.mainImage || file ? (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          style={{ maxWidth: '50vh' }}
        >
          {file ? (
            <img
              style={{ marginLeft: 200, marginBottom: 10 }}
              src={file}
              width="100%"
              height="100%"
              alt="zdjęcie"
            />
          ) : (
            <img
              style={{ marginLeft: 200, marginBottom: 10 }}
              src={recipesPath + formValue.mainImage}
              width="100%"
              height="100%"
              alt="zdjęcie"
            />
          )}
        </Box>
      ) : null}
      <span className="textForm">
        {formValue.mainImage || file ? 'Edytuj' : 'Dodaj'} zdjęcie przepisu
      </span>
      <div>
        <TextField
          required
          style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
          name="image"
          id="outlined-required"
          type="file"
          onChange={handleChangeFile}
        />
        {!formErrorImage && (
          <div className="errorsForm">Należy dodać zdjęcie przepisu.</div>
        )}
      </div>
    </Fragment>
  );
};

export default RecipeBaseInfo;
