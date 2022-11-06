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
import { ProductMeasure } from 'src/models/product_measure';
import MeasureService from 'src/services/measureService';

interface EditMeasureCardProps {
  productMeasure: ProductMeasure;
  setProductMeasure: React.Dispatch<React.SetStateAction<ProductMeasure>>;
}

const EditMeasureCard: FC<EditMeasureCardProps> = ({
  productMeasure,
  setProductMeasure
}) => {
  const [formErrorName, setFormErrorName] = useState(true);

  const handleChange = (event: ChangeEvent<HTMLInputElement>): void => {
    setProductMeasure({
      ...productMeasure,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (productMeasure.name === '') {
      setFormErrorName(false);
      return;
    } else {
      setFormErrorName(true);
    }

    const loginFromData = new FormData();
    loginFromData.append('name', productMeasure.name);
    loginFromData.append(
      'productMeasureId',
      productMeasure.productMeasureId.toString()
    );

    var result = await MeasureService.putMeasure(
      productMeasure.productMeasureId,
      loginFromData
    );
    if (result == null) {
      window.location.href = '/product/measure';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Edytuj jednostkę miary" />
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
          <span className="textForm">Podaj skrót jednostki miary</span>
          <div>
            <TextField
              required
              style={{ width: 700, margin: 20, marginTop: 5, marginBottom: 10 }}
              name="name"
              id="outlined-required"
              value={productMeasure.name}
              onChange={handleChange}
            />
            {!formErrorName && (
              <div className="errorsForm">
                {' '}
                Należy podać nazwę skrótu jednostki miary.
              </div>
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
            Edytuj miarę
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default EditMeasureCard;
