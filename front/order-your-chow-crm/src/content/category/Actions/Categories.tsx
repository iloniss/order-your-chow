import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductCategory } from 'src/models/product_category';
import CategoriesTable from './CategoriesTable';
import CategoryService from '../../../services/categoryService';

function Categories() {
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
      <CategoriesTable
        productCategories={productCategories}
        setProductCategories={setProductCategories}
      />
    </Card>
  );
}

export default Categories;
