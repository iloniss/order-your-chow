import { useState, useEffect } from 'react';
import { Recipe } from 'src/models/recipe';
import recipeService from 'src/services/recipeService';
import { Card } from '@mui/material';
import AddRecipeDescriptionCard from './AddRecpeDescriptionCard';

const AddRecipeDescription = () => {
  const [recipes, setRecipes] = useState<Recipe[]>([]);

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
