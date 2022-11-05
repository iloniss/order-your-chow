import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductCategory } from 'src/models/product_category';
import CategoryService from './../../../services/categoryService';
import AddProductsCard from './AddProductsCard';

const AddProduct = () => {
  const [productCategories, setProductCategories] = useState<ProductCategory[]>(
    []
  );

  const getProductCategories = async () => {
    var result = await CategoryService.getAllCategories();
    setProductCategories(result.data);
  };

  useEffect(() => {
    getProductCategories();
  }, []);

  return (
    <Card>
      <AddProductsCard productCategories={productCategories} />
    </Card>
  );
};

export default AddProduct;
