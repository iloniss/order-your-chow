import http from '../http-common';
import { Product } from 'src/models/product';
import { AxiosResponse, AxiosError } from 'axios';
import { AddProduct } from 'src/models/add_product';

class ProductService {
  async getAll() {
    return await http.get<Array<Product>>('/product');
  }
  // zmienić model na AddProduct zgodnie z API

  async getProductById(productId: number) {
    return await http.get<AddProduct>('/product/getById/' + productId);
  }

  async deleteProduct(productId: number) {
    return await http
      .delete('/product/' + productId.toString())
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return 'Nie można usunąć produktu, który jest używany.';
        } else if (reason.response!.status === 404) {
          return 'Nie znaleziono produktu.';
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }

  async postProduct(data: FormData) {
    return await http
      .post<Product>('/product', data, {
        headers: {
          'Content-type': 'multipart/form-data'
        }
      })
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

  async putProduct(productId: number, data: FormData) {
    return await http
      .put<Product>('/product/' + productId.toString(), data, {
        headers: {
          'Content-type': 'multipart/form-data'
        }
      })
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (
          reason.response!.status === 400 ||
          reason.response!.status === 404
        ) {
          return reason.response.data.errors.Name;
        } else if (reason.response!.status === 409) {
          return reason.response.data;
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }
}

export default new ProductService();
