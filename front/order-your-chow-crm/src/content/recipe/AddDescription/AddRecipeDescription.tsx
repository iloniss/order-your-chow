import { useState, useEffect } from 'react';
import recipeService from 'src/services/recipeService';
import { Card } from '@mui/material';
import AddRecipeDescriptionCard from './AddRecpeDescriptionCard';
import { RecipesList } from 'src/models/recipe/recipes_list';

const AddRecipeDescription = () => {
  const [recipes, setRecipes] = useState<RecipesList[]>([]);

  const getRecipes = async () => {
    var result = await recipeService.getAll(false);
    setRecipes(result.data);
  };

  useEffect(() => {
    getRecipes();
  }, []);

  return (
    <Card>
      <AddRecipeDescriptionCard recipes={recipes} />
    </Card>
  );
};
export default AddRecipeDescription;
