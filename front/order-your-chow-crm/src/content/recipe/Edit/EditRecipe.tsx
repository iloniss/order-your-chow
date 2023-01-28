import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { Product } from 'src/models/product';
import { ProductMeasure } from 'src/models/product_measure';
import { Recipe } from 'src/models/recipe/recipe';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import { RecipeProduct } from 'src/models/recipe/recipe_product';
import measureService from 'src/services/measureService';
import productService from 'src/services/productService';
import recipeService from 'src/services/recipeService';
import EditRecipeCard from './EditRecipeCard';

const EditRecipe = () => {
  const [recipeInfo, setRecipeInfo] = useState<Recipe>({
    recipeCategoryId: 0,
    name: '',
    description: '',
    recipeId: 0,
    duration: 0,
    meat: false,
    mainImage: ''
  });
  const [recipeProducts, setRecipeProducts] = useState<RecipeProduct[]>([]);
  const [products, setProducts] = useState<Product[]>([]);
  const [measures, setMeasures] = useState<ProductMeasure[]>([]);
  const [recipeCategories, setRecipeCategories] = useState<RecipeCategory[]>(
    []
  );
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const getRecipeInfo = async (recipeId: number) => {
    var result = await recipeService.getRecipe(recipeId);
    setRecipeInfo(result.data);
  };

  const getRecipeProducts = async (recipeId: number) => {
    var result = await recipeService.getRecipeProducts(recipeId);
    setRecipeProducts(result.data.recipeProductList);
  };

  const getProduct = async () => {
    var result = await productService.getAll();
    setProducts(result.data);
  };

  const getMeasure = async () => {
    var result = await measureService.getAllMeasures();
    setMeasures(result.data);
  };

  const getRecipeCategories = async () => {
    var result = await recipeService.getAllRecipeCategories();
    setRecipeCategories(result.data);
  };
  useEffect(() => {
    (async () => {
      setIsLoading(true);
      const params = new URLSearchParams(window.location.search);
      let value = +params.get('recipeId');
      getRecipeInfo(value);
      getRecipeProducts(value);
      getProduct();
      getMeasure();
      getRecipeCategories();
      setIsLoading(false);
    })();
  }, []);

  return (
    <Card>
      {isLoading ? (
        ''
      ) : (
        <EditRecipeCard
          recipeInfo={recipeInfo}
          recipeProducts={recipeProducts}
          setRecipeInfo={setRecipeInfo}
          setRecipeProducts={setRecipeProducts}
          products={products}
          productMeasures={measures}
          recipeCategories={recipeCategories}
        />
      )}
    </Card>
  );
};

export default EditRecipe;
