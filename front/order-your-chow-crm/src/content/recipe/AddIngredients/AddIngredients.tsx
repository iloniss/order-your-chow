import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { Recipe } from 'src/models/recipe';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';
import productService from 'src/services/productService';
import recipeService from 'src/services/recipeService';
import AddIngredientsCard from './AddIngredientsCard';
import measureService from 'src/services/measureService';
import { RecipeProduct } from 'src/models/recipe_product';

const AddIngredients = () => {
  const [recipes, setRecipes] = useState<Recipe[]>([]);
  const [products, setProducts] = useState<Product[]>([]);
  const [measures, setMeasures] = useState<ProductMeasure[]>([]);
  const [recipeProducts, setRecipeProducts] = useState<RecipeProduct[]>();
  const [selectedRecipe, setSelectedRecipe] = useState<String>('');

  const getRecipe = async () => {
    var result = await recipeService.getAll(false);
    setRecipes(result.data);
  };

  const getProduct = async () => {
    var result = await productService.getAll();
    setProducts(result.data);
  };

  const getMeasure = async () => {
    var result = await measureService.getAllMeasures();
    setMeasures(result.data);
  };

  const getRecipeProducts = async (selectedRecipe: Number) => {
    if (+selectedRecipe !== 0) {
      var result = await recipeService.getRecipeProducts(+selectedRecipe);
      setRecipeProducts(result.data.recipeProductList);
    }
  };

  useEffect(() => {
    getRecipeProducts(+selectedRecipe);
  }, [selectedRecipe]);

  useEffect(() => {
    getRecipe();
    getProduct();
    getMeasure();
  }, []);

  return (
    <Card>
      <AddIngredientsCard
        recipes={recipes}
        products={products}
        productMeasures={measures}
        selectedRecipe={selectedRecipe}
        setSelectedRecipe={setSelectedRecipe}
        recipeProducts={recipeProducts}
        setRecipeProducts={setRecipeProducts}
      />
    </Card>
  );
};
export default AddIngredients;
