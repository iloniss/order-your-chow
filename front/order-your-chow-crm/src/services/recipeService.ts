import { AxiosError, AxiosResponse } from 'axios';
import { AddDescription } from 'src/models/add_description';
import { Recipe } from 'src/models/recipe';
import { RecipeCategory } from 'src/models/recipe_category';
import { RecipeProductArray } from 'src/models/recipe_product_array';
import http from '../http-common';

class RecipeService {
  async getAll(isActive: boolean) {
    return await http.get<Array<Recipe>>('/recipe', {
      params: { isActive: isActive }
    });
  }

  async getAllRecipeCategories() {
    return await http.get<Array<RecipeCategory>>('/recipe/category');
  }

  async getRecipeProducts(recipeId: Number) {
    return await http.get<RecipeProductArray>(
      '/recipe/' + recipeId.toString() + '/recipeProducts'
    );
  }

  async postRecipe(data: FormData) {
    return await http
      .post<Recipe>('/recipe', data, {
        headers: {
          'Content-type': 'multipart/form-data'
        }
      })
      .then((response: AxiosResponse<Recipe>) => {
        return response.data.recipeId;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return reason.response.data.errors.Name;
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }

  async putRecipeDescription(data: FormData) {
    return await http
      .put<AddDescription>('/recipe/description', data, {
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
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }

  async postRecipeProduct(data: RecipeProductArray, recipeId: number) {
    return await http
      .post<RecipeProductArray>(
        '/recipe/' + recipeId.toString() + '/products',
        JSON.stringify(data),
        {
          headers: {
            'Content-type': 'application/JSON'
          }
        }
      )
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError) => {
        if (reason.response!.status === 400) {
          return reason.response.data.errors.Name;
        } else {
          return 'Nieoczekiwany problem.';
        }
      });
  }
}

export default new RecipeService();
