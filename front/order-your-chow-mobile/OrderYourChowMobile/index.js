/**
 * @format
 */

import {AppRegistry} from 'react-native';
import App from './App';
import {name as appName} from './app.json';
import {firebase} from '@react-native-firebase/storage';

global.firebase = firebase.initializeApp({
  projectId: 'orderyourchow',
});

AppRegistry.registerComponent(appName, () => App);
