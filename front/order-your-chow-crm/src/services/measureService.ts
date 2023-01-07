import http from '../http-common';
import { ProductMeasure } from 'src/models/product_measure';
import { AxiosResponse, AxiosError } from 'axios';

class MeasureService {
  async getAllMeasures() {
    return await http.get<Array<ProductMeasure>>('/recipeProductMeasure');
  }

  async deleteMeasure(productMeasureId: number) {
    return await http
      .delete('/recipeProductMeasure/' + productMeasureId.toString())
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return 'Nie można usunąć jednostki miary, która jest używana.';
        } else if (reason.response!.status === 404) {
          return 'Nie znaleziono produktu.';
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }

  async getMeasure(productMeasureId: number) {
    return await http.get<ProductMeasure>(
      '/recipeProductMeasure/' + productMeasureId.toString()
    );
  }

  async postMeasure(data: FormData) {
    return await http
      .post<ProductMeasure>('/recipeProductMeasure', data)
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return reason.response.data.errors.Name;
        } else if (reason.response!.status === 409) {
          return reason.response.data;
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }

  async putMeasure(productMeasureId: number, data: FormData) {
    return await http
      .put<ProductMeasure>(
        '/recipeProductMeasure/' + productMeasureId.toString(),
        data
      )
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return reason.response.data.errors.Name;
        } else if (reason.response!.status === 409) {
          return reason.response.data;
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }
}

export default new MeasureService();
