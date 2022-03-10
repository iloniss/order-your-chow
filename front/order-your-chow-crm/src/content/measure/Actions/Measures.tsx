import { Card } from '@mui/material';
import { useEffect, useState } from 'react';
import MeasuresTable from './MeasuresTable';
import { ProductMeasure } from 'src/models/product_measure';
import MeasureService from 'src/services/measureService';

function Measures() {
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
      <MeasuresTable
        productMeasures={productMeasures}
        setProductMeasures={setProductMeasures}
      />
    </Card>
  );
}

export default Measures;
