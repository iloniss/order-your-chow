import {configureStore} from '@reduxjs/toolkit';
import dietReducer from './slices/dietSlice';
import calendarReducer from './slices/calendarSlice';

export const store = configureStore({
  reducer: {
    diet: dietReducer,
    calendar: calendarReducer,
  },
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
