import { AxiosError, AxiosResponse } from 'axios';
import { Recipe } from 'src/models/recipe';
import { RecipeCategory } from 'src/models/recipe_category';
import http from '../http-common';

class RecipeService {
  async getAll() {
    return await http.get<Array<Recipe>>('/recipe');
  }

  async getAllRecipeCategories() {
    return await http.get<Array<RecipeCategory>>('/recipe/category');
  }

  async postRecipe(data: FormData) {
    return await http
      .post<Recipe>('/recipe', data, {
        headers: {
          'Content-type': 'multipart/form-data'
        }
      })
      .then((response: AxiosResponse) => {
        console.log(response);
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
