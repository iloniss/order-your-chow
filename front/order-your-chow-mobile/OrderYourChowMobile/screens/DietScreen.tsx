import {SafeAreaView} from 'react-native';
import React, {useLayoutEffect} from 'react';
import {useNavigation} from '@react-navigation/native';
import Header from '../components/Header';
import HorizonalCalendar from '../components/HorizontalCalendar';
import Diet from '../components/Diet';

export default function DietScreen(): JSX.Element {
  const navigation = useNavigation();

  useLayoutEffect(() => {
    navigation.setOptions({
      headerShown: false,
    });
  });

  return (
    <SafeAreaView>
      <Header title={'Dieta'} />
      <HorizonalCalendar />
      <Diet />
    </SafeAreaView>
  );
}
