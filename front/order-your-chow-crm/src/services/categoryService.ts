import http from '../http-common';
import { AxiosResponse, AxiosError } from 'axios';
import { ProductCategory } from 'src/models/product_category';
import { ErrorResponse, handleError } from './serviceHelper';

class CategoryService {
  async getAllCategories() {
    return await http.get<Array<ProductCategory>>('/productCategory');
  }

  async deleteCategory(productCategoryId: number) {
    return await http
      .delete('/productCategory/' + productCategoryId.toString())
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
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
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }

  async putCategory(data: FormData) {
    return await http
      .put<ProductCategory>('/productCategory', data)
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }
}

export default new CategoryService();
