import http from '../http-common';
import { Product } from 'src/models/product';
import { AxiosResponse, AxiosError } from 'axios';
import { AddProduct } from 'src/models/add_product';
import { ErrorResponse, handleError } from './serviceHelper';
import { ProductQuery } from 'src/models/product/query/product_query';

class ProductService {
  async getAll() {
    return await http.get<Array<Product>>('/product/list');
  }
  // zmienić model na AddProduct zgodnie z API

  async getProduct(productQuery: ProductQuery) {
    return await http.get<AddProduct>('/product', { params: productQuery });
  }

  async deleteProduct(productId: number) {
    return await http
      .delete('/product/' + productId.toString())
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
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
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }

  async putProduct(data: FormData) {
    return await http
      .put<Product>('/product', data, {
        headers: {
          'Content-type': 'multipart/form-data'
        }
      })
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }
}

export default new ProductService();
