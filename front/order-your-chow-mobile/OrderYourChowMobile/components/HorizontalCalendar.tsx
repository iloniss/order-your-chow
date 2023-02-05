import {ScrollView, TouchableOpacity} from 'react-native';
import React, {useCallback, useEffect, useState} from 'react';
import HorizonalCalendarDay from './HorizontalCalendarDay';
import {
  ChevronRightIcon,
  ChevronLeftIcon,
} from 'react-native-heroicons/outline';
import calendarService from '../services/calendarService';
import {CalendarDay} from '../models/calendar/calendarDay';
import {CalendarQuery} from '../models/calendar/calendarQuery';
import {useDispatch, useSelector} from 'react-redux';
import {selectActiveDay, setActiveDay} from '../slices/calendarSlice';

const HorizonalCalendar: React.FC = () => {
  const dispatch = useDispatch();
  const activeDay = useSelector(selectActiveDay);

  const [calendarIsLoading, setCalendarIsLoading] = useState<boolean>(true);
  const [dietDays, setDietDays] = useState<CalendarDay[]>([]);
  const [calendarPeriod, setCalendarPeriod] = useState<CalendarQuery>({
    dateMin: undefined,
    dateMax: undefined,
  });

  const fetchDietDays = useCallback(async () => {
    setCalendarIsLoading(true);
    try {
      const dietDaysResult = await calendarService.getAll(calendarPeriod);
      setDietDays(dietDaysResult.data);
      if (calendarPeriod.dateMin === undefined) {
        dispatch(
          setActiveDay(
            dietDaysResult.data[dietDaysResult.data.length / 2].dietDayId,
          ),
        );
      }
    } catch (error) {
      console.log(error);
    } finally {
      setCalendarIsLoading(false);
    }
  }, [calendarPeriod, dispatch]);

  useEffect(() => {
    fetchDietDays();
  }, [fetchDietDays]);

  function getDate(day: string) {
    var date = day
      .split(/(\s+)/)
      .filter(function (e) {
        return e.trim().length > 0;
      })[1]
      .split('.');
    return new Date(
      parseInt('20' + date[2], 10),
      parseInt(date[1], 10) - 1,
      parseInt(date[0], 10),
    );
  }

  function addDays(date: Date, days: number) {
    date.setDate(date.getDate() + days);
    return date;
  }

  return (
    <ScrollView
      className="pb-2 bg-fuchsia-50"
      // eslint-disable-next-line react-native/no-inline-styles
      contentContainerStyle={{paddingHorizontal: 15, paddingTop: 10}}
      horizontal
      showsHorizontalScrollIndicator={false}>
      <>
        <TouchableOpacity
          className="items-center flex-row"
          onPress={() => {
            setCalendarPeriod({
              dateMin: addDays(getDate(dietDays[0].day), -15),
              dateMax: addDays(getDate(dietDays[0].day), -1),
            });
          }}>
          <ChevronLeftIcon size={36} color={'#000000'} />
        </TouchableOpacity>
        {!calendarIsLoading ? (
          dietDays?.map(dietDay => (
            <HorizonalCalendarDay
              dietDay={dietDay}
              key={dietDay.dietDayId}
              isActiveDay={activeDay === dietDay.dietDayId}
            />
          ))
        ) : (
          <></>
        )}

        <TouchableOpacity
          className="items-center flex-row"
          onPress={() => {
            setCalendarPeriod({
              dateMin: addDays(getDate(dietDays[dietDays.length - 1].day), 1),
              dateMax: addDays(getDate(dietDays[dietDays.length - 1].day), 15),
            });
          }}>
          <ChevronRightIcon size={36} color={'#000000'} />
        </TouchableOpacity>
      </>
    </ScrollView>
  );
};

export default HorizonalCalendar;
