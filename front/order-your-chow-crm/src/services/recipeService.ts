import { AxiosError, AxiosResponse } from 'axios';
import { AddDescription } from 'src/models/add_description';
import { Recipe } from 'src/models/recipe/recipe';
import { RecipesList } from 'src/models/recipe/recipes_list';
import { RecipeCategory } from 'src/models/recipe/recipe_category';
import { RecipeProductArray } from 'src/models/recipe/recipe_product_array';
import http from '../http-common';
import { ErrorResponse, handleError } from './serviceHelper';

class RecipeService {
  async getAll(isActive: boolean) {
    return await http.get<Array<RecipesList>>('/recipe', {
      params: { isActive: isActive }
    });
  }

  async getRecipe(reciepId: number) {
    return await http.get<Recipe>('/recipe/' + reciepId.toString(), {
      params: { reciepId: reciepId }
    });
  }

  async getAllRecipeCategories() {
    return await http.get<Array<RecipeCategory>>('/recipe/categories');
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

  async deleteRecipe(recipeId: number) {
    return await http
      .delete('/recipe/' + recipeId.toString())
      .then((response: AxiosResponse) => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }

  async updateRecipe(data: FormData) {
    return await http
      .put('/recipe/', data, {
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

export default new RecipeService();
