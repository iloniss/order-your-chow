import {
  TextField,
  Card,
  CardHeader,
  Divider,
  CardContent,
  Box,
  Button
} from '@mui/material';
import 'src/styles.css';
import MenuItem from '@mui/material/MenuItem';
import { useState, FC, ChangeEvent } from 'react';
import { ProductCategory } from 'src/models/product_category';
import ProductService from 'src/services/productService';
import { AddProduct } from 'src/models/add_product';

interface EditProductCardProps {
  productData: AddProduct;
  productCategories: ProductCategory[];
  setproductData: React.Dispatch<React.SetStateAction<AddProduct>>;
}

const EditProductCard: FC<EditProductCardProps> = ({
  productCategories,
  productData,
  setproductData
}) => {
  const categoryOptions = [];

  productCategories.forEach((element) => {
    categoryOptions.push({
      id: element.productCategoryId.toString(),
      name: element.name
    });
  });

  const [formErrorName, setFormErrorName] = useState(true);

  const [formErrorCategory, setFormErrorCategory] = useState(true);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setproductData({
      ...productData,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (productData.name === '') {
      setFormErrorName(false);
    } else {
      setFormErrorName(true);
    }

    if (productData.productCategoryId === 0) {
      setFormErrorCategory(false);
    } else {
      setFormErrorCategory(true);
    }

    if (formErrorCategory === false || formErrorName === false) return;

    const loginFromData = new FormData();
    loginFromData.append(
      'productCategoryId',
      productData.productCategoryId.toString()
    );
    loginFromData.append('name', productData.name);

    if (
      (document.querySelector("input[name='image']") as HTMLInputElement).files
        .length > 0
    ) {
      loginFromData.append(
        'ImageFile',
        (document.querySelector("input[name='image']") as HTMLInputElement)
          .files[0]
      );
    }

    var result = await ProductService.putProduct(
      productData.productId,
      loginFromData
    );
    console.log(result);
    if (result == null) {
      window.location.href = '/product/actions';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Edytuj produkt" />
      <Divider />
      <CardContent>
        <Box
          component="form"
          sx={{
            '& .MuiTextField-root': { m: 1, width: '25ch' }
          }}
          noValidate
          autoComplete="off"
        >
          <span className="textForm">Wybierz kategori?? produktu</span>
          <div>
            <TextField
              id="outlined-select-currency"
              select
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="productCategoryId"
              value={productData.productCategoryId}
              onChange={handleChange}
            >
              {productCategories.map((categoryOption) => (
                <MenuItem
                  key={categoryOption.productCategoryId}
                  value={categoryOption.productCategoryId}
                >
                  {categoryOption.name}
                </MenuItem>
              ))}
            </TextField>
            {!formErrorCategory && (
              <div className="errorsForm">Nale??y wybra?? kategori??.</div>
            )}
          </div>
          <span className="textForm">Podaj nazw?? produktu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="name"
              id="outlined-required"
              value={productData.name}
              onChange={handleChange}
            />
            {!formErrorName && (
              <div className="errorsForm">Nale??y poda?? nazw?? produktu.</div>
            )}
          </div>
          <span className="textForm">Edytuj zdj??cie produktu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="image"
              id="outlined-required"
              type="file"
            />
          </div>
          <Button
            type="submit"
            style={{ width: 200, marginLeft: 250, marginTop: 30 }}
            sx={{ margin: 1 }}
            variant="contained"
            size="large"
            color="primary"
            onClick={async (e) => {
              await handleSubmit(e);
            }}
          >
            Edytuj produkt
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EditProductCard;
