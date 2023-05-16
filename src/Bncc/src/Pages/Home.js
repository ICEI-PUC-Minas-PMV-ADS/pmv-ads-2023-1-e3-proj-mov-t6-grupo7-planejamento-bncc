import React, { useState } from 'react';
import {StyleSheet, View, Image,ScrollView} from 'react-native';
import {Button, Text } from 'react-native-paper';

import Container from '../components/Container';
import Header from '../components/Header';
import Body from '../components/Body';
import TableExample from '../components/Table';



const Home = () => {
  
  return (
    <Container>   
      <Body>
          <TableExample />
      </Body>
    </Container>
  );
};

export default Home