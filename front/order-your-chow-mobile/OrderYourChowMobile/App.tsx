import React from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import DietScreen from './screens/DietScreen';
import {store} from './store';
import {Provider} from 'react-redux';

const Stack = createNativeStackNavigator();

function App(): JSX.Element {
  return (
    <NavigationContainer>
      <Provider store={store}>
        <Stack.Navigator>
          <Stack.Screen name="Diet" component={DietScreen} />
        </Stack.Navigator>
      </Provider>
    </NavigationContainer>
  );
}
export default App;
