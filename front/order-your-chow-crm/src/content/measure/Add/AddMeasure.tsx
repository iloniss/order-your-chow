import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import AddMeasuresCard from './AddMeasuresCard';
import MeasureService from 'src/services/measureService';
import { ProductMeasure } from 'src/models/product_measure';

const AddMeasure = () => {
  const [productMeasures, setProductMeasures] = useState<ProductMeasure[]>([]);

  const getProductMeasures = async () => {
    var result = await MeasureService.getAllMeasures();
    setProductMeasures(result.data);
  };

  useEffect(() => {
    getProductMeasures();
  }, []);

  return (
    <Card>
      <AddMeasuresCard productMeasures={productMeasures} />
    </Card>
  );
};

export default AddMeasure;
