import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {RootState} from '../store';

export interface CalendarState {
  activeDay: number;
}

const initialState: CalendarState = {
  activeDay: 0,
};

export const calendarSlice = createSlice({
  name: 'calendar',
  initialState,
  reducers: {
    setActiveDay: (state, action: PayloadAction<number>) => {
      state.activeDay = action.payload;
    },
  },
});

export const {setActiveDay} = calendarSlice.actions;

export const selectActiveDay = (state: RootState) => state.calendar.activeDay;

export default calendarSlice.reducer;
