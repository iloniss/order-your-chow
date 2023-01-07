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
import { AddCategory } from 'src/models/add_category';

interface AddCategoriesCardProps {
  productCategories: ProductCategory[];
}

const AddCategoriesCard: FC<AddCategoriesCardProps> = ({
  productCategories
}) => {
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

  const [formValue, setformValue] = useState<AddCategory>({
    name: ''
  });

  const [formError, setFormError] = useState(true);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setformValue({
      ...formValue,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (formValue.name === '') {
      setFormError(false);
    } else {
      setFormError(true);
    }
    const loginFromData = new FormData();
    loginFromData.append('name', formValue.name);
    var result = await categoryService.postCategory(loginFromData);
    if (result == null) {
      window.location.href = '/product/category';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Nowa kategoria" />
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
              value={formValue.name}
              onChange={handleChange}
            />
            {!formError && (
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
            Dodaj kategorię
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddCategoriesCard;
