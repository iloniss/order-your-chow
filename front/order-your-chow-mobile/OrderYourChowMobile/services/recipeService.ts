import {AxiosError} from 'axios';
import http from '../http-common';
import {RecipesListItem} from '../models/recipe/recipeList';
import {ErrorResponse, handleError} from './serviceHelper';

class RecipeService {
  async getRecipesForDietDayId(dietDayId: number) {
    return await http.get<Array<RecipesListItem>>(
      'recipe/getRecipesForDietDayId',
      {
        params: {dietDayId: dietDayId},
      },
    );
  }

  async toggleFavourite(recipeId: number, favourite: boolean) {
    if (favourite) {
      return await this.addRecipeToFavourite(recipeId);
    } else {
      return await this.deleteRecipeFromFavourite(recipeId);
    }
  }

  async deleteRecipeFromFavourite(recipeId: number) {
    return await http
      .delete('recipe/deleteRecipeFromFavourite/' + recipeId.toString())
      .then(() => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }

  async addRecipeToFavourite(recipeId: number) {
    return await http
      .post('recipe/addRecipeToFavourite/' + recipeId.toString())
      .then(() => {
        return null;
      })
      .catch((reason: AxiosError<ErrorResponse>) => {
        return handleError(reason);
      });
  }
}

export default new RecipeService();
