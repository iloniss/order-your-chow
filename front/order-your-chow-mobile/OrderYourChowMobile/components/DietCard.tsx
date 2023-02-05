import React, {useCallback, useEffect, useState} from 'react';
import {Text, View, Image, TouchableOpacity} from 'react-native';
import StarRating from 'react-native-star-rating-widget';
import {
  HeartIcon as HeartIconEmpty,
  ClockIcon,
} from 'react-native-heroicons/outline';
import {HeartIcon as HeartIconFull} from 'react-native-heroicons/solid';
import {useDispatch} from 'react-redux';
import {RecipesListItem} from '../models/recipe/recipeList';
import recipeService from '../services/recipeService';
import {setIsFavourite} from '../slices/dietSlice';
import {getImgUri} from '../helpers/firebaseHelper';

interface DietCardProps {
  dietDayRecipe: RecipesListItem;
}

const DietCard: React.FC<DietCardProps> = ({dietDayRecipe}) => {
  const dispatch = useDispatch();
  const toggleFavourite = async () => {
    var result = await recipeService.toggleFavourite(
      dietDayRecipe.recipeId,
      !dietDayRecipe.favourite,
    );
    if (result == null) {
      dispatch(setIsFavourite(dietDayRecipe.recipeId));
    } else {
      console.log(result);
    }
  };

  const [imageUrl, setImageUrl] = useState<string>('');

  const getImageUrl = useCallback(async () => {
    setImageUrl(await getImgUri(dietDayRecipe.mainImage));
  }, [dietDayRecipe.mainImage]);

  useEffect(() => {
    getImageUrl();
  }, [getImageUrl]);

  return (
    <View>
      <View className="bg-white border-zinc-600 border-b-2 border-t-sky-100 border-t-2">
        <Text className="px-4 pt-2 mb-2 font-bold text-xl">
          {dietDayRecipe.categoryName}
        </Text>
      </View>
      <View>
        <Image
          source={{
            uri: imageUrl,
            cache: 'force-cache',
          }}
          className="w-full bg-gray-300 p-4 h-60"
        />
      </View>
      <View className="bg-white border-zinc-600 border-t-2">
        <View className="px-2 pt-1">
          <View className="flex-row my-1 items-center justify-between">
            <Text className="text-2xl font-bold">
              {dietDayRecipe.recipeName}
            </Text>
            <TouchableOpacity onPress={() => toggleFavourite()}>
              {dietDayRecipe.favourite ? (
                <HeartIconFull size={36} color={'#a83f39'} />
              ) : (
                <HeartIconEmpty size={36} color={'#a83f39'} />
              )}
            </TouchableOpacity>
          </View>
        </View>
        <View className="flex-row items-center pb-4 px-2">
          <View className="flex-1">
            <View className="flex-row items-center">
              <StarRating
                starSize={24}
                rating={dietDayRecipe.rating}
                onChange={() => {}}
              />
              <Text className="text-[#fdd835] ml-2 font-bold text-md">
                {dietDayRecipe.rating.toFixed(2)}
              </Text>
            </View>
          </View>
          <ClockIcon size={24} color={'#000000'} />
          <Text className="pl-2 pr-2">{dietDayRecipe.duration} min</Text>
        </View>
        <View className="flex-row mx-20 space-x-20 items-center pb-4 justify-between">
          <TouchableOpacity className="rounded-lg bg-cyan-500 px-4 py-2 shadow-lg shadow-slate-600">
            <Text className="text-center text-white font-bold">Wymień</Text>
          </TouchableOpacity>
          <TouchableOpacity className="rounded-lg bg-cyan-500 px-4 py-2 shadow-lg shadow-slate-600">
            <Text className="text-center text-white font-bold">Zjedzone</Text>
          </TouchableOpacity>
        </View>
      </View>
    </View>
  );
};

export default DietCard;
