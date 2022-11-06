import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProductMeasure } from 'src/models/product_measure';
import MeasureService from '../../../services/measureService';
import EditMeasureCard from './EditMeasureCard';

const EditMeasure = () => {
  const [productMeasure, setProductMeasure] = useState<ProductMeasure>({
    productMeasureId: 0,
    name: ''
  });

  const getProductMeasure = async (productMeasureId: number) => {
    var result = await MeasureService.getMeasure(productMeasureId);
    setProductMeasure(result.data);
  };

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    let value = parseInt(params.get('productmeasureid'));
    getProductMeasure(value);
  }, []);

  return (
    <Card>
      <EditMeasureCard
        productMeasure={productMeasure}
        setProductMeasure={setProductMeasure}
      />
    </Card>
  );
};

export default EditMeasure;
