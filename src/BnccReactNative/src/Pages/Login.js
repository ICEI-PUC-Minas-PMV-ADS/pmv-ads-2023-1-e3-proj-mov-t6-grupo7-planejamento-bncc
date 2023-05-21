import React, {useState} from 'react';
import { StyleSheet, View, Image} from 'react-native';
import { Button, Text} from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import  Spinner  from 'react-native-loading-spinner-overlay';


import Container from '../components/Container';
import Header from '../components/Header';
import Body from '../components/Body';
import Input from '../components/Input';
import Home from './Home';

const Login = () => {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');

  const [isLoading, setIsLoading] = useState(false)

  const [res, setRes] = useState("");
  const [valid, setValid] = useState(false);
  const [validEmail, setValidEmail] = useState(false);
  const navigation = useNavigation();

  const Logar = () => {

    fetch("http://gustavaosmarter-001-site1.htempurl.com/api/Login", {        
      method: 'POST',
      headers: {
        'accept': '*/*',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({      
        "email": email,
        "senha": senha
      })        
    })
      .then(async response => response.json())
      .then(result => {                
        result.message == "Usuário encontrado!" ? setRes("") : setRes(result.message);  
        console.log(result.data.email)
        setTimeout(
          function() {
            setRes("");
            setValid('')
            setValidEmail('')
          }
          .bind(this),
          2000
        );
        if (result.message == "Email inválido!") {
          setValidEmail('err')     
        }
        if (email == "" && senha == "") {
          setRes(result.message = "Preencha todos os campos!")
          senha == "" ? setValid('err') : setValid('')
          email == "" ? setValidEmail('err') : setValidEmail('')
        }
        if (result.message == "Email/Senha inválidos!" && email != "" && senha != "") {
          setValid('err')
          setValidEmail('err')
        } else if (senha == "" && result.message != "Email inválido!") {
          setRes(result.message = "Preencha todos os campos!")
          setValid('err')
        } else if (email == "" && result.message == "Email inválido!") {
          setRes(result.message = "Preencha todos os campos!")
          setValidEmail('err')
        }
       
       var loginValido = result.acaoValida;
        
        if (loginValido) {
          setIsLoading(true);
          setTimeout(() => {
            setIsLoading(false);
            navigation.navigate('Home');            
          }, 800);
          
        }        
      }).catch(error =>{
          console.log(error)
      }) 
  }

  return (
    <Container>
      <Header title={'Login'} />
      <Body>
      <Spinner visible={isLoading} />
        <View>
          <Image
            style={styles.tinyLogo}
            source={require('../Img/BnccLogo.jpg')}
          />
        </View>
        
        <View style={styles.margin}>
        
          <Text style={styles.text2}>Login</Text>
          <Input
            label="Email"
            value={email}
            error={validEmail}
            onChangeText={text => setEmail(text)}
            style={{ marginTop: 10 }}
            autoCorrect={true }
          />         
          <Input
            label="Senha"
            secureTextEntry={true}
            error={valid}
            value={senha}
            onChangeText={text => setSenha(text)}
            style={{marginTop: 10}}
          />
        </View>
        <Text style={{ textAlign:"left", marginLeft:5, marginTop:10, color: 'red' }}  >
            {res}
          </Text>
        <Button
          mode="contained"
          style={styles.marginBtn}
          onPress={() => {
            Logar()

          }}>
          Login
        </Button>       
        <Text style={{marginLeft: 82, marginTop: 170}}>
          Não tem uma conta? <Text onPress={() => {  navigation.navigate('Registro');}} style={{color:'#246787'}}>Cadastre-se</Text>
        </Text>       
      </Body>
    </Container>
  );
};

const styles = StyleSheet.create({
  margin: {
    marginTop: 20,
  },
  marginBtn: {
    backgroundColor: '#7eab4d',
    marginTop: 50,
  },
  text: {
    textAlign: 'center',
    margin: 8,
  },
  tinyLogo: {
    marginLeft: 10,
    width: 300,
    height: 200,
  },
  text2: {
    fontSize: 20,
    textDecorationLine: "underline",
    fontWeight: 500,
    fontSize: 15,
    color: "#838383"
  },
  centeredView: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 22,
  },
  modalView: {
    margin: 20,
    backgroundColor: '#FF6D60',
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
    textAlign: 'center',
    fontWeight: 'bold',
    color:'red',
  },
  buttonDiv: {
    flex: 1,
    maxHeight: 70,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
  }
});

export default Login;
