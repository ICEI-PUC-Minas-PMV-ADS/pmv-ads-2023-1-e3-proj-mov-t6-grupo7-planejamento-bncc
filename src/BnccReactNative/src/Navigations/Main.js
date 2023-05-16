import React, { useState} from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { NavigationContainer } from '@react-navigation/native';
import { View, Modal, Text, TouchableOpacity,  StyleSheet } from 'react-native';
import { IconButton } from 'react-native-paper';
import { useNavigationContainerRef} from '@react-navigation/native';
import Spinner from 'react-native-loading-spinner-overlay';



import Home from '../Pages/Home';
import Login from '../Pages/Login';
import Registro from '../Pages/Registro';

const Stack = createNativeStackNavigator();



const Main = () => {
  
  const [modalVisible, setModalVisible] = useState(false);
  const [nav, setNav] = useState("");
  const [isLoading, setIsLoading] = useState(false)

  const navigation = useNavigationContainerRef();

  const navegar = () => {
    setTimeout(() => {
      navigation.navigate('Login')
    }, 500);
    setModalVisible(false)    
  }
  
  
  const openModal = () => {
    setModalVisible(true);
  }

  return (
    <NavigationContainer independent={true} ref={navigation}>
      <Stack.Navigator initialRouteName="Login">
      <Stack.Screen
          name="Registro"
          component={Registro}
          options={{ 
            headerStyle: {
              backgroundColor: '#7eab4d',
            },
            headerTitleStyle: {
              color: 'white'
            }
            
          }}
        />       

        <Stack.Screen
          name="Login"
          component={Login}
          options={{           
          header: () => null  
          }}
        />
        
        <Stack.Screen
          name='Home'
          component={Home}
          options={() => ({
            headerRight: () => <IconButton
            icon="logout" 
            iconColor='white'
            style={{ height:50,} }
            onPress={openModal} 
          >Sair</IconButton>,
            headerLeft: () => <View />,
            headerStyle: {
              backgroundColor: '#7eab4d',
            },
            headerTitleStyle: {
              color: 'white'
            }
          })}
        />
      </Stack.Navigator>

      <Modal
        animationType="slide"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => {
          setModalVisible(false);
        }}
      >
        <View style={styles.centeredView}>
        <Spinner visible={isLoading} /> 
          <View style={styles.modalView}>     
           
            <Text style={styles.modalText}>Tem certeza que deseja sair?</Text>

            <View style={styles.buttonDiv}>
              <TouchableOpacity style={[styles.button1, styles.buttonLogout]} onPress={() =>                 
               navegar()
              }>
                <Text style={styles.textStyle}>Sim</Text>              
              </TouchableOpacity>

              <TouchableOpacity style={[styles.button, styles.buttonClose]} onPress={() => setModalVisible(false)}>
              <Text style={styles.textStyle}>Cancelar</Text>             
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>      
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  centeredView: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 22,
  },
  modalView: {
    margin: 20,
    backgroundColor: 'white',
    borderRadius: 20,
    padding: 35,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5,
  },
  button: {
    borderRadius: 20,
    padding: 10,
    elevation: 2,
  },
  button1: {
    borderRadius: 20,
    padding: 10,
    width:75,
    elevation: 2,
  },
  buttonOpen: {
    backgroundColor: '#F194FF',
  },
  buttonClose: {
    backgroundColor: '#9BA4B5',
  },
  buttonLogout: {
    backgroundColor: '#E76161',
    marginRight:8,
  },
  textStyle: {
    color: 'white',
    fontWeight: 'bold',
    textAlign: 'center',
  },
  modalText: {
    marginBottom: 15,
    color:'red',
    textAlign: 'center',
    fontWeight:'bold',
  },
  buttonDiv: {
    flex: 1,
    maxHeight: 70,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
  }

});


export default Main;
