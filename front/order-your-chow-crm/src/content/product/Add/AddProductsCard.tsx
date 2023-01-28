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

interface AddProductsCardProps {
  productCategories: ProductCategory[];
}

const AddProductsCard: FC<AddProductsCardProps> = ({ productCategories }) => {
  const categoryOptions = [
    {
      id: '0',
      name: 'Wszystkie'
    }
  ];

  productCategories.forEach((element) => {
    categoryOptions.push({
      id: element.productCategoryId.toString(),
      name: element.name
    });
  });

  const [formValue, setformValue] = useState<AddProduct>({
    productCategoryId: 0,
    name: '',
    productId: 0,
    image: ''
  });

  const [formErrorName, setFormErrorName] = useState(true);

  const [formErrorCategory, setFormErrorCategory] = useState(true);

  const [formErrorImage, setFormErrorImage] = useState(true);
  const [file, setFile] = useState(undefined);

  const handleChangeFile = (event) => {
    setFile(URL.createObjectURL(event.target.files[0]));
  };
  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setformValue({
      ...formValue,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formValue.name === '') {
      setFormErrorName(false);
    } else {
      setFormErrorName(true);
    }

    if (formValue.productCategoryId === 0) {
      setFormErrorCategory(false);
    } else {
      setFormErrorCategory(true);
    }

    if (
      (document.querySelector("input[name='image']") as HTMLInputElement).files
        .length === 0
    ) {
      setFormErrorImage(false);
    } else {
      setFormErrorImage(true);
    }

    if (
      formErrorCategory === false ||
      formErrorImage === false ||
      formErrorName === false
    )
      return;

    const loginFromData = new FormData();
    loginFromData.append(
      'productCategoryId',
      formValue.productCategoryId.toString()
    );
    loginFromData.append('name', formValue.name);
    loginFromData.append(
      'ImageFile',
      (document.querySelector("input[name='image']") as HTMLInputElement)
        .files[0]
    );
    var result = await ProductService.postProduct(loginFromData);
    if (result == null) {
      window.location.href = '/product/actions';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Nowy produkt" />
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
          <span className="textForm">Wybierz kategorię produktu</span>
          <div>
            <TextField
              id="outlined-select-currency"
              select
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="productCategoryId"
              value={formValue.productCategoryId}
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
              <div className="errorsForm">Należy wybrać kategorię.</div>
            )}
          </div>
          <span className="textForm">Podaj nazwę produktu</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="name"
              id="outlined-required"
              value={formValue.name}
              onChange={handleChange}
            />
            {!formErrorName && (
              <div className="errorsForm">Należy podać nazwę produktu.</div>
            )}
          </div>
          {file ? (
            <Box
              display="flex"
              justifyContent="center"
              alignItems="center"
              style={{ maxWidth: '50vh' }}
            >
              <img
                style={{ marginLeft: 200, marginBottom: 10 }}
                src={file}
                width="100%"
                height="100%"
                alt="zdjęcie"
              />
            </Box>
          ) : null}
          <span className="textForm">
            {file != null || file ? 'Edytuj' : 'Dodaj'} zdjęcie produktu
          </span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="image"
              id="outlined-required"
              type="file"
              onChange={handleChangeFile}
            />
            {!formErrorImage && (
              <div className="errorsForm">Należy dodać zdjęcie produktu.</div>
            )}
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
            Dodaj produkt
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddProductsCard;
