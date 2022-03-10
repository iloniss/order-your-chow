import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { Product } from 'src/models/product';
import { ProductCategory } from 'src/models/product_category';
import ProductsTable from './ProductsTable';
import CategoryService from './../../../services/categoryService';
import ProductService from './../../../services/productService';

function Products() {

  const [products, setProducts] =  useState<Product[]>([]);
  const [productCategories, setProductCategories] =  useState<ProductCategory[]>([]);

  const getProducts = async () => {
    var result = await ProductService.getAll();
    setProducts(result.data);
  }

  const getProductCategory = async () => {
    var result = await CategoryService.getAllCategories();
    setProductCategories(result.data);
  }
 
  useEffect(() => {
    getProducts();
    getProductCategory();
  }, []);

  return (
    <Card>
      <ProductsTable products={products} setProducts={setProducts} productCategories={productCategories} />
    </Card>
  );
}

export default Products;
