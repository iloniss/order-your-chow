import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { RecipesList } from 'src/models/recipe/recipes_list';
import recipeService from 'src/services/recipeService';
import RecipesTable from './RecipesTable';

function Recipes() {
  const [recipesList, setRecipesList] = useState<RecipesList[]>([]);

  const getRecipes = async () => {
    var result = await recipeService.getAll(true);
    setRecipesList(result.data);
  };

  useEffect(() => {
    getRecipes();
  }, []);

  return (
    <Card>
      <RecipesTable recipesList={recipesList} setRecipesList={setRecipesList} />
    </Card>
  );
}

export default Recipes;
