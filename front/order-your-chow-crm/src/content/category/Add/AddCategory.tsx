import React from 'react';
import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductCategory } from 'src/models/product_category';
import CategoryService from '../../../services/categoryService';
import AddCategoriesCard from './AddCategoriesCard';

const AddCategory = () => {
  const [productCategories, setProductCategories] = useState<ProductCategory[]>(
    []
  );

  const getProductCategory = async () => {
    var result = await CategoryService.getAllCategories();
    setProductCategories(result.data);
  };

  useEffect(() => {
    getProductCategory();
  }, []);

  return (
    <Card>
      <AddCategoriesCard productCategories={productCategories} />
    </Card>
  );
};

export default AddCategory;
