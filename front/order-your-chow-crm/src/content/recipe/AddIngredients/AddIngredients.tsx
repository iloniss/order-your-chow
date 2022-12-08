import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { Recipe } from 'src/models/recipe';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';
import productService from 'src/services/productService';
import recipeService from 'src/services/recipeService';
import AddIngredientsCard from './AddIngredientsCard';
import measureService from 'src/services/measureService';

const AddIngredients = () => {
  const [recipes, setRecipes] = useState<Recipe[]>([]);
  const [products, setProducts] = useState<Product[]>([]);
  const [measures, setMeasures] = useState<ProductMeasure[]>([]);

  const getRecipe = async () => {
    var result = await recipeService.getAll();
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
      />
    </Card>
  );
};
export default AddIngredients;
