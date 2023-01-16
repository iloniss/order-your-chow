import {
  Card,
  CardContent,
  CardHeader,
  Divider,
  Box,
  MenuItem,
  TextField,
  TextareaAutosize,
  Button
} from '@mui/material';
import { ChangeEvent, FC, useState } from 'react';
import { AddDescription } from 'src/models/add_description';
import { RecipesList } from 'src/models/recipe/recipes_list';
import recipeService from 'src/services/recipeService';
import 'src/styles.css';

interface AddRecipeDescriptionCardProps {
  recipes: RecipesList[];
}
const AddRecipeDescriptionCard: FC<AddRecipeDescriptionCardProps> = ({
  recipes
}) => {
  const [formValue, setFormValue] = useState<AddDescription>({
    recipeId: 0,
    description: ''
  });

  const [formErrorRecipe, setFormErrorRecipe] = useState(true);
  const [formErrorDescription, setFormErrorDescription] = useState(true);

  const handleChangeRecipe = (event: ChangeEvent<HTMLInputElement>): void => {
    setFormValue({
      ...formValue,
      [event.target.name]: event.target.value
    });
  };

  const handleChangeDescription = (
    event: ChangeEvent<HTMLTextAreaElement>
  ): void => {
    setFormValue({
      ...formValue,
      description: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formValue.description === '') {
      setFormErrorDescription(false);
    } else {
      setFormErrorDescription(true);
    }

    if (formValue.recipeId === undefined || formValue.recipeId === 0) {
      setFormErrorRecipe(false);
    } else {
      setFormErrorRecipe(true);
    }

    if (formErrorDescription === false || formErrorRecipe === false) return;

    const loginFromData = new FormData();

    loginFromData.append('recipeId', formValue.recipeId.toString());
    loginFromData.append('description', formValue.description);

    var result = await recipeService.putRecipeDescription(loginFromData);

    if (result == null) {
      window.location.href = '/recipe/list';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Opis przepisu" />
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
          <div>
            <TextField
              select
              required
              label="Wybierz przepis"
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 25 }}
              name="recipeId"
              value={formValue.recipeId !== 0 ? formValue.recipeId : ''}
              onChange={handleChangeRecipe}
            >
              {recipes.map((recipe) => (
                <MenuItem key={recipe.recipeId} value={recipe.recipeId}>
                  {recipe.name}
                </MenuItem>
              ))}
            </TextField>
            {!formErrorRecipe && (
              <div className="errorsForm">Należy wybrać przepis.</div>
            )}
          </div>
          <span className="textForm">Dodaj opis przepisu</span>
          <div>
            <TextareaAutosize
              className="textArea"
              onChange={handleChangeDescription}
            />
            {!formErrorDescription && (
              <div className="errorsForm">Należy uzupełnić opis.</div>
            )}
          </div>
        </Box>
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
          Dodaj opis
        </Button>
      </CardContent>
    </Card>
  );
};
export default AddRecipeDescriptionCard;
