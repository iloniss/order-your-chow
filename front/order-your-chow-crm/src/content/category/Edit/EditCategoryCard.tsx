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
import { useState, FC, ChangeEvent } from 'react';
import { ProductCategory } from 'src/models/product_category';
import categoryService from 'src/services/categoryService';

interface EditCategoryCardProps {
  productCategory: ProductCategory;
  setProductCategory: React.Dispatch<React.SetStateAction<ProductCategory>>;
}

const EditCategoryCard: FC<EditCategoryCardProps> = ({
  productCategory,
  setProductCategory
}) => {
  const [formErrorName, setFormErrorName] = useState(true);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setProductCategory({
      ...productCategory,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (productCategory.name === '') {
      setFormErrorName(false);
    } else {
      setFormErrorName(true);
    }

    if (formErrorName === false) return;

    const loginFromData = new FormData();
    loginFromData.append('name', productCategory.name);
    loginFromData.append(
      'productCategoryId',
      productCategory.productCategoryId.toString()
    );

    var result = await categoryService.putCategory(loginFromData);
    if (result == null) {
      window.location.href = '/product/category';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Edytuj kategorię" />
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
          <span className="textForm">Podaj nazwę kategorii</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="name"
              id="outlined-required"
              value={productCategory.name}
              onChange={handleChange}
            />
            {!formErrorName && (
              <div className="errorsForm">Należy podać nazwę kategorii.</div>
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
            Edytuj kategorię
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EditCategoryCard;
