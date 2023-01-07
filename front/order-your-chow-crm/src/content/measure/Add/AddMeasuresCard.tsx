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
import { AddMeasure } from 'src/models/add_measure';
import { ProductMeasure } from 'src/models/product_measure';
import MeasureService from 'src/services/measureService';

interface AddMeasuresCardProps {
  productMeasures: ProductMeasure[];
}

const AddMeasuresCard: FC<AddMeasuresCardProps> = ({ productMeasures }) => {
  const measureOptions = [
    {
      id: '0',
      name: 'Wszystkie'
    }
  ];

  productMeasures.forEach((element) => {
    measureOptions.push({
      id: element.productMeasureId.toString(),
      name: element.name
    });
  });

  const [formValue, setformValue] = useState<AddMeasure>({
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
    var result = await MeasureService.postMeasure(loginFromData);
    if (result == null) {
      window.location.href = '/product/measure';
    } else {
      alert(result);
    }
  };

  return (
    <Card>
      <CardHeader title="Nowa jednostka miary" />
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
              value={formValue.name}
              onChange={handleChange}
            />
            {!formError && (
              <div className="errorsForm">
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
            Dodaj miarę
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};

export default AddMeasuresCard;
