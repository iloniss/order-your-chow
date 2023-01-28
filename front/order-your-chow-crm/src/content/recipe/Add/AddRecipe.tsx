import { Card } from '@mui/material';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import RecipeService from './../../../services/recipeService';
import { useEffect, useState } from 'react';
import AddRecipesCard from './AddRecipesCard';

const AddRecipe = () => {
  const [recipeCategories, setRecipeCategories] = useState<RecipeCategory[]>(
    []
  );

  const getRecipeCategories = async () => {
    var result = await RecipeService.getAllRecipeCategories();
    setRecipeCategories(result.data);
  };

  useEffect(() => {
    getRecipeCategories();
  }, []);

  return (
    <Card>
      <AddRecipesCard recipeCategories={recipeCategories} />
    </Card>
  );
};

export default AddRecipe;
