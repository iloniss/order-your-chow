import React, {useCallback, useEffect, useState} from 'react';
import {ScrollView, View} from 'react-native';
import {useDispatch, useSelector} from 'react-redux';
import recipeService from '../services/recipeService';
import {selectActiveDay} from '../slices/calendarSlice';
import {selectDietDay, setList} from '../slices/dietSlice';
import DietCard from './DietCard';

const Diet: React.FC = () => {
  const activeDay = useSelector(selectActiveDay);
  const dispatch = useDispatch();
  const [dietIsLoading, setDietIsLoading] = useState<boolean>(true);
  const dietDayRecipes = useSelector(selectDietDay);

  const fetchDietDayRecipes = useCallback(async () => {
    setDietIsLoading(true);
    try {
      if (activeDay === 0) {
        return;
      }
      const dietDayRecipesResult = await recipeService.getRecipesForDietDayId(
        activeDay,
      );
      dispatch(setList(dietDayRecipesResult.data));
    } catch (error) {
      console.log(error);
    } finally {
      setDietIsLoading(false);
    }
  }, [activeDay, dispatch]);

  useEffect(() => {
    fetchDietDayRecipes();
  }, [fetchDietDayRecipes]);

  return (
    <ScrollView>
      {!dietIsLoading ? (
        <View className="mb-40">
          {dietDayRecipes?.map(dietDayRecipe => (
            <DietCard
              dietDayRecipe={dietDayRecipe}
              key={dietDayRecipe.recipeId}
            />
          ))}
        </View>
      ) : (
        <></>
      )}
    </ScrollView>
  );
};

export default Diet;
