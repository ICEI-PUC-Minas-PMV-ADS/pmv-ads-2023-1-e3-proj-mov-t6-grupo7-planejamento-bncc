import React from 'react';
import {StyleSheet, View} from 'react-native';
import { Appbar, Button} from 'react-native-paper';

const Header = ({title}) => {
  return(
    <Appbar.Header style={{ backgroundColor: '#7eab4d' }}>    
      <Appbar.Content style={{marginLeft:20} } title={title} color="#FFF" />
    </Appbar.Header>   
    
  );
};

export default Header;