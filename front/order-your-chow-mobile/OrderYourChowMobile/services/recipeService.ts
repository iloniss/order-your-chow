import http from '../http-common';
import {RecipesList} from '../models/recipe/recipe_list';

class RecipeService {
  async getAll(isActive: boolean) {
    return await http.get<Array<RecipesList>>('/recipe', {
      params: {isActive: isActive},
    });
  }
}

export default new RecipeService();
