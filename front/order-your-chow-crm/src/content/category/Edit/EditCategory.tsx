import React from 'react';
import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductCategory } from 'src/models/product_category';
import CategoryService from '../../../services/categoryService';
import EditCategoryCard from './EditCategoryCard';

const EditCategory = () => {
  const [productCategory, setProductCategory] = useState<ProductCategory>({
    productCategoryId: 0,
    name: ''
  });

  const getProductCategory = async (productCategoryId: number) => {
    var result = await CategoryService.getCategory(productCategoryId);
    console.log(result);
    console.log('pa');
    setProductCategory(result.data);
  };

  useEffect(() => {
    console.log('dupa');
    const params = new URLSearchParams(window.location.search);
    let value = parseInt(params.get('productcategoryid'));
    getProductCategory(value);
  }, []);

  return (
    <Card>
      <EditCategoryCard
        productCategory={productCategory}
        setProductCategory={setProductCategory}
      />
    </Card>
  );
};

export default EditCategory;
