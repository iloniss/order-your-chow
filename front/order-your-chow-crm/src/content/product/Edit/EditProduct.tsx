import React from 'react';
import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductCategory } from 'src/models/product_category';
import CategoryService from './../../../services/categoryService';
import EditProductCard from './EditProductCard';
import { AddProduct } from 'src/models/add_product';
import ProductService from 'src/services/productService';
import { ProductQuery } from 'src/models/product/query/product_query';

const EditProduct = () => {
  const [productCategories, setProductCategories] = useState<ProductCategory[]>(
    []
  );

  const [productData, setproductData] = useState<AddProduct>({
    productId: 0,
    productCategoryId: 0,
    image: '',
    name: ''
  });

  const getProductCategory = async () => {
    var result = await CategoryService.getAllCategories();
    setProductCategories(result.data);
  };

  const getProduct = async (productId: number) => {
    var result = await ProductService.getProduct({
      productId: productId
    } as ProductQuery);
    setproductData(result.data);
  };

  useEffect(() => {
    getProductCategory();
    const params = new URLSearchParams(window.location.search);

    let value = parseInt(params.get('productid'));

    getProduct(value);
  }, []);

  return (
    <Card>
      <EditProductCard
        productCategories={productCategories}
        productData={productData}
        setproductData={setproductData}
      />
    </Card>
  );
};

export default EditProduct;
