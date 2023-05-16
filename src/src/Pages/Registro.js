import React, {useState} from 'react';
import {StyleSheet, View, Image} from 'react-native';
import {Button, Text} from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import  Spinner  from 'react-native-loading-spinner-overlay';

import Container from '../components/Container';
import Header from '../components/Header';
import Body from '../components/Body';
import Input from '../components/Input';
import Home from './Home';

const Registro = () => {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [res, setRes] = useState('');
  const [nome, setNome] = useState('');

  const [valid, setValid] = useState();
  const [validEmail, setValidEmail] = useState();
  const [validNome, setValidNome] = useState();
  
  const [isLoading, setIsLoading] = useState(false)

  const navigation = useNavigation();

  const Registrar = () => {
    fetch('http://gustavaosmarter-001-site1.htempurl.com/api/Registrar', {
      method: 'POST',
      headers: {
        accept: '*/*',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        nome: nome,
        email: email,
        senha: senha,
        tipo: "cliente"
      }),
    })
      .then(async response => response.json())
      .then(result => {
        setRes(result.message);
        setTimeout(
          function () {
            setRes('');
            setValid("")
            setValidNome("")
            setValidEmail("")
          }.bind(this),
          2000,
        );
        var cadastroValido = result.acaoValida;       

        if (cadastroValido == true) {
          setIsLoading(true);
          setTimeout(() => {
            setIsLoading(false);
            navigation.navigate('Login');           
          }, 800);         
        }
          if (result.message == "Email inválido!" || result.message == "Email já existe!") {
            setValidEmail("err")
            setIsLoading(false);
        }        
        if (senha == "" && email == "" && nome == "") {
            setRes(result.message)
            setValidEmail("err")
            setValidNome("err") 
            setValid("err")
            setIsLoading(false);                                 
        }
        if (nome == "" && email != "" && senha != "" && result.message != "Email já existe!") {
          setValidNome("err") 
          setIsLoading(false);
          console.log("nome vazio")
        }
        if (nome != "" && email == "" && senha != "" && result.message != "Email já existe!") {
          setValidEmail("err")  
          console.log("email vazio")
          setIsLoading(false);
        }
        if (nome != "" && email != "" && senha == "" && result.message != "Email já existe!") {
          setValid("err")
          console.log("senha vazio")
          setIsLoading(false);
        }  
      })
      .catch(error => {
        setIsLoading(false);
        console.log(error);
      });
  };

  return (
    <Container>
      <Body>
      <Spinner visible={isLoading} />
        <View>
          <Image
            style={styles.tinyLogo}
            source={require('../Img/BnccLogo.jpg')}
          />
        </View>
        <View style={styles.margin}>
          <Text style={styles.text2}>Cadastro</Text>
          <Input
            label="Nome"
            autoCapitalize={ true}
            error={ validNome}
            value={nome}
            onChangeText={text => setNome(text)}
            style={{marginTop: 10}}
          />
          <Input
            label="Email"  
            error={ validEmail}          
            value={email}
            onChangeText={text => setEmail(text)}
            style={{marginTop: 10}}
          />
          <Input
            label="Senha"
            error={ valid}
            secureTextEntry={true}
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
            Registrar();
          }}>
          Cadastrar
        </Button>       
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
    textDecorationLine: 'underline',
    fontWeight: 500,
    fontSize: 15,
    color: '#838383',
  },
});

export default Registro;
