import http from '../http-common';
import { AxiosResponse, AxiosError } from 'axios';
import { ProductCategory } from 'src/models/product_category';

class CategoryService {
  async getAllCategories() {
    return await http.get<Array<ProductCategory>>('/productCategory');
  }

  async deleteCategory(productCategoryId: number) {
    return await http
      .delete('/productCategory/' + productCategoryId.toString())
      .then((response: AxiosResponse) => {
        console.log(response);
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return 'Nie można usunąć kategorii, która jest używana.';
        } else if (reason.response!.status === 404) {
          return 'Nie znaleziono pkategorii.';
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }
  async getCategory(productCategoryId: number) {
    return await http.get<ProductCategory>(
      '/productCategory/' + productCategoryId.toString()
    );
  }

  async postCategory(data: FormData) {
    return await http
      .post<ProductCategory>('/productCategory', data)
      .then((response: AxiosResponse) => {
        console.log(response);
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

  async putCategory(productCategoryId: number, data: FormData) {
    console.log(data);
    return await http
      .put<ProductCategory>(
        '/productCategory/' + productCategoryId.toString(),
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

export default new CategoryService();
