/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */
import 'react-native-gesture-handler';
import React from 'react';
import {NavigationContainer} from '@react-navigation/native';
import { SafeAreaProvider } from 'react-native-safe-area-context';


import Route from '../BnccReactNative/src/Navigations/Route';

const App = () => {
  return (
    <SafeAreaProvider>
      <NavigationContainer>
        <Route />
      </NavigationContainer>
    </SafeAreaProvider>
  );
};

export default App;
