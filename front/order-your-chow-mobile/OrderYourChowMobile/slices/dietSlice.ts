import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {RecipesListItem} from '../models/recipe/recipeList';
import {RootState} from '../store';

export interface DietState {
  dietDay: RecipesListItem[];
}

const initialState: DietState = {
  dietDay: [],
};

export const dietSlice = createSlice({
  name: 'diet',
  initialState,
  reducers: {
    setList: (state, action: PayloadAction<RecipesListItem[]>) => {
      state.dietDay = action.payload;
    },

    setIsFavourite: (state, action: PayloadAction<number>) => {
      const recipe = state.dietDay.find(x => x.recipeId === action.payload);
      if (recipe) {
        recipe.favourite = !recipe.favourite;
      }
    },
  },
});

export const {setList, setIsFavourite} = dietSlice.actions;

export const selectDietDay = (state: RootState) => state.diet.dietDay;

export default dietSlice.reducer;
