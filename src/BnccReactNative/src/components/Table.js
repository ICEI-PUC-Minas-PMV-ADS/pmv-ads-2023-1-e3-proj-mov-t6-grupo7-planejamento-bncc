import React, { useState } from "react";
import axios from 'axios'; 
import { View, StyleSheet, Text, ScrollView, PermissionsAndroid, Dimensions } from "react-native";
import { Col, Row, Grid } from "react-native-easy-grid";
import RNFetchBlob from 'rn-fetch-blob';
import { Checkbox, IconButton } from "react-native-paper"; 
import  Spinner  from 'react-native-loading-spinner-overlay';
import Share from 'react-native-share';


const App = () => {

  //#region Variaveis
  const [checkedAll, setCheckedAll] = React.useState(false);
  const [checkedPort, setCheckedPort] = React.useState(false);
  const [checkedMat, setCheckedMat] = React.useState(false); 
  const [colorPort, setColorPort] = React.useState(styles.cell);
  const [colorMat, setColorMat] = React.useState(styles.cell2);
  const [port, setPortName] = React.useState("");
  const [mat, setMatName] = React.useState("");
  const [isLoading, setIsLoading] = useState(false)

  


  
  const [checkedAno1, setcheckedAno1] = React.useState(false);
  const [checkedAno2, setcheckedAno2] = React.useState(false);
  const [checkedAno3, setcheckedAno3] = React.useState(false);
  const [checkedAno4, setcheckedAno4] = React.useState(false);
  const [checkedAno5, setcheckedAno5] = React.useState(false);
  const [checkedAno6, setcheckedAno6] = React.useState(false);
  const [checkedAno7, setcheckedAno7] = React.useState(false);
  const [checkedAno8, setcheckedAno8] = React.useState(false);
  const [checkedAno9, setcheckedAno9] = React.useState(false);

  const [checkedAnoTodos, setCheckedAnoTodos] = React.useState(false);

  const [ano1p, setColorAno1p] = React.useState(styles.anop);
  const [ano2p, setColorAno2p] = React.useState(styles.anop);
  const [ano3p, setColorAno3p] = React.useState(styles.anop);
  const [ano4p, setColorAno4p] = React.useState(styles.anop);
  const [ano5p, setColorAno5p] = React.useState(styles.anop);
  const [ano6p, setColorAno6p] = React.useState(styles.anop);
  const [ano7p, setColorAno7p] = React.useState(styles.anop);
  const [ano8p, setColorAno8p] = React.useState(styles.anop);
  const [ano9p, setColorAno9p] = React.useState(styles.anop);

  const [ano1m, setColorAno1m] = React.useState(styles.anom);
  const [ano2m, setColorAno2m] = React.useState(styles.anom);
  const [ano3m, setColorAno3m] = React.useState(styles.anom);
  const [ano4m, setColorAno4m] = React.useState(styles.anom);
  const [ano5m, setColorAno5m] = React.useState(styles.anom);
  const [ano6m, setColorAno6m] = React.useState(styles.anom);
  const [ano7m, setColorAno7m] = React.useState(styles.anom);
  const [ano8m, setColorAno8m] = React.useState(styles.anom);
  const [ano9m, setColorAno9m] = React.useState(styles.anom); 

  const [colorAno, setColorCellAno] = React.useState(styles.cellAno);

//#endregion
  function limparChks() {
    setcheckedAno1(false);
    setcheckedAno2(false);
    setcheckedAno3(false);
    setcheckedAno4(false);
    setcheckedAno5(false);
    setcheckedAno6(false);
    setcheckedAno7(false);
    setcheckedAno8(false);
    setcheckedAno9(false);
    setCheckedAll(false);
    setCheckedPort(false);
    setCheckedMat(false);

    setColorPort(styles.cell)
    setColorMat(styles.cell2)

    setCheckedAnoTodos(false)
    setColorAno1p(styles.anop)
    setColorAno2p(styles.anop)
    setColorAno3p(styles.anop)
    setColorAno4p(styles.anop)
    setColorAno5p(styles.anop)
    setColorAno6p(styles.anop)
    setColorAno7p(styles.anop)
    setColorAno8p(styles.anop)
    setColorAno9p(styles.anop)
    setColorAno1m(styles.anom)
    setColorAno2m(styles.anom)
    setColorAno3m(styles.anom)
    setColorAno4m(styles.anom)
    setColorAno5m(styles.anom)
    setColorAno6m(styles.anom)
    setColorAno7m(styles.anom)
    setColorAno8m(styles.anom)
    setColorAno9m(styles.anom)

  }
  async function permitirDownload() {
    try {
      const granted = await PermissionsAndroid.requestMultiple([
        PermissionsAndroid.PERMISSIONS.READ_EXTERNAL_STORAGE,
        PermissionsAndroid.PERMISSIONS.WRITE_EXTERNAL_STORAGE,
      ], {
        title: 'Write to external storage',
        message:
          'Deseja permitir que o app armazene dados no seu dispositivo?',
        buttonNeutral: 'Pergunte depois',
        buttonNegative: 'Cancelar',
        buttonPositive: 'OK',
      });
  
      if (
        granted['android.permission.READ_EXTERNAL_STORAGE'] === PermissionsAndroid.RESULTS.GRANTED &&
        granted['android.permission.WRITE_EXTERNAL_STORAGE'] === PermissionsAndroid.RESULTS.GRANTED 
      ) {
        console.log('Permissões garantidas');
      } else {
        console.log('Permissões recusadas');
      }
    } catch (err) {
      console.warn(err);
    }
  }


  const share = async () => {
    const downloadDir = RNFetchBlob.fs.dirs.DownloadDir + '/BNCC.xlsx';
    const nome = 'BNCC.xlsx';
    const mimeType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    const path = `file://${downloadDir}`;
    const data = await RNFetchBlob.fs.readFile(downloadDir, 'base64');
    
    await Share.open({
      title: 'Compartilhar planilha',
      message: 'BNCC Componente(s): ' + (port !== "" ? "Português" : mat) + (mat !== "" ? ", " + "Matemática" : ""),
      url: path,
      type: mimeType,
    }).catch(err => {
      
    });
  };

  const excel = async (arr = [], todos, primeiroAno,segundoAno,terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno) => {
    todos = checkedAnoTodos;
    primeiroAno = checkedAno1;
    segundoAno = checkedAno2;
    terceiroAno = checkedAno3;
    quartoAno = checkedAno4;
    quintoAno = checkedAno5;
    sextoAno = checkedAno6;
    setimoAno = checkedAno7;
    oitavoAno = checkedAno8;
    nonoAno = checkedAno9;
    arr = [port, mat];

    var list = []; 
    for (var i = 0; i < arr.length; i++){
        if (arr[i]) {
            list.push( "materia="+arr[i]+"&");
        }
    } 
    var result = list.toString().replace(',', '');
    
    permitirDownload()

    const url = `http://gustavaosmarter-001-site1.htempurl.com/api/Excel?${result}todos=${todos}&primeiroAno=${primeiroAno}&segundoAno=${segundoAno}&terceiroAno=${terceiroAno}&quartoAno=${quartoAno}&quintoAno=${quintoAno}&sextoAno=${sextoAno}&setimoAno=${setimoAno}&oitavoAno=${oitavoAno}&nonoAno=${nonoAno}`;
    
    await axios.get(url,{        
          method: 'GET',
          responseType: 'arraybuffer', 
          maxContentLength: Infinity,
          maxBodyLength: Infinity,    
          Headers: {
              'accept': '*/*',
              'Access-Control-Allow-Origin': '*',
              'Content-type':'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
      }
    }).then(response => { 
      setIsLoading(true)
      RNFetchBlob.config({
        fileCache: true,
        addAndroidDownloads: {
          useDownloadManager: true,
          notification: true,
          path: RNFetchBlob.fs.dirs.DownloadDir + `/BNCC.xlsx`,
          description: 'Excel file'
        }
      }).fetch('GET', url, {        
        'accept': '*/*',
        'Access-Control-Allow-Origin': '*',
        'Content-type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        
      }).then(response => {
        console.log('Arquivo baixado com sucesso');
        limparChks()
        setIsLoading(false)
        
      }).catch(error => {
        console.log('Não foi possivel efetuar o download', error);
        setIsLoading(false)
      });       
    }) 
  }
  
  const compartilhar = async (arr = [], todos, primeiroAno, segundoAno, terceiroAno, quartoAno, quintoAno, sextoAno, setimoAno, oitavoAno, nonoAno) => {
    //#region variaveisUrl
    todos = checkedAnoTodos;
    primeiroAno = checkedAno1;
    segundoAno = checkedAno2;
    terceiroAno = checkedAno3;
    quartoAno = checkedAno4;
    quintoAno = checkedAno5;
    sextoAno = checkedAno6;
    setimoAno = checkedAno7;
    oitavoAno = checkedAno8;
    nonoAno = checkedAno9;
    arr = [port, mat];

    const url = `http://gustavaosmarter-001-site1.htempurl.com/api/Excel?${result}todos=${todos}&primeiroAno=${primeiroAno}&segundoAno=${segundoAno}&terceiroAno=${terceiroAno}&quartoAno=${quartoAno}&quintoAno=${quintoAno}&sextoAno=${sextoAno}&setimoAno=${setimoAno}&oitavoAno=${oitavoAno}&nonoAno=${nonoAno}`;
    //#endregion

    var list = []; 
    for (var i = 0; i < arr.length; i++){
        if (arr[i]) {
            list.push( "materia="+arr[i]+"&");
        }
    } 

    var result = list.toString().replace(',', '');

    permitirDownload()    
    
    fetch(url, {
      method: 'GET',
      headers: {
        'accept': '*/*',
        'Access-Control-Allow-Origin': '*',
        'Content-type':'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
      },
      responseType: 'arraybuffer',
      maxContentLength: Infinity,
      maxBodyLength: Infinity
    })
    .then(response => {
      setTimeout(() => {
        share();
      }, 1000);
      console.log('Arquivo compartilhado');
      setIsLoading(false);
    })
    .catch(error => {
      console.log('Não foi possivel efetuar o compartilhamento', error);
    });
  }

  return (    
    <View>
      <Spinner visible={isLoading} />
      <Text style={styles.textTitles}>Componentes</Text>
      <View style={styles.box}>
        <View style={styles.checkboxComp}>
          <View style={styles.checkboxAnos}>
            <Checkbox                 
              status={checkedAll ? "checked" : "unchecked"}
              onPress={() => {
                setCheckedAll(!checkedAll);
                
                !checkedAll ? setCheckedMat(!checkedMat) : setCheckedMat(false);
                !checkedAll ? setColorMat(styles.cellMat) : setColorMat(styles.cell2)

                !checkedAll ? setCheckedPort(!checkedPort) : setCheckedPort(false);
                !checkedAll ? setColorPort(styles.cellPort) : setColorPort(styles.cell)
              }}
            />
            <Text style={styles.textNames}>Todos</Text>
          </View>
          
          <View style={styles.checkboxAnos}>
            <Checkbox
              name = "materia"
              status={checkedPort ? "checked" : "unchecked"}
              onPress={() => {
                setCheckedPort(!checkedPort);
                !checkedPort ? setColorPort(styles.cellPort) : setColorPort(styles.cell);
                //#region LogicaAnosPort
                if(checkedAnoTodos == true && checkedMat == true){
                  setColorAno1p(styles.cellAno)                  
                  setColorAno2p(styles.cellAno)                  
                  setColorAno3p(styles.cellAno)                  
                  setColorAno4p(styles.cellAno)                  
                  setColorAno5p(styles.cellAno)
                  setColorAno6p(styles.cellAno)
                  setColorAno7p(styles.cellAno)
                  setColorAno8p(styles.cellAno)
                  setColorAno9p(styles.cellAno)                 

                  if (checkedAnoTodos == true && checkedPort == true)
                  {
                    setColorAno1p(styles.cellAno)                  
                    setColorAno2p(styles.cellAno)                  
                    setColorAno3p(styles.cellAno)                  
                    setColorAno4p(styles.cellAno)                  
                    setColorAno5p(styles.cellAno)
                    setColorAno6p(styles.cellAno)
                    setColorAno7p(styles.cellAno)
                    setColorAno8p(styles.cellAno)
                    setColorAno9p(styles.cellAno)
                  }

                  if (checkedPort) {
                    setColorAno1p(styles.anop)                  
                    setColorAno2p(styles.anop)                  
                    setColorAno3p(styles.anop)                  
                    setColorAno4p(styles.anop)                  
                    setColorAno5p(styles.anop)
                    setColorAno6p(styles.anop)
                    setColorAno7p(styles.anop)
                    setColorAno8p(styles.anop)
                    setColorAno9p(styles.anop)
                  }
                                   
                  if (checkedAno1 == false) {
                    setColorAno1p(styles.anop)
                  }
                  if (checkedAno2 == false) {
                    setColorAno2p(styles.anop)
                  }
                  if (checkedAno3 == false) {
                    setColorAno3p(styles.anop)
                  }
                  if (checkedAno4 == false) {
                    setColorAno4p(styles.anop)
                  }
                  if (checkedAno5 == false) {
                    setColorAno5p(styles.anop)
                  }
                  if (checkedAno6 == false) {
                    setColorAno6p(styles.anop)
                  }
                  if (checkedAno7 == false) {
                    setColorAno7p(styles.anop)
                  }
                  if (checkedAno8 == false) {
                    setColorAno8p(styles.anop)
                  }
                  if (checkedAno9 == false) {
                    setColorAno9p(styles.anop)
                  }
                } 
                //check anos separados
                if ( checkedAno1 == true && checkedMat == true) {
                  setColorAno1p(styles.cellAno)
                  if (checkedAno1 == true && checkedPort == true) {
                    setColorAno1p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno1p(styles.anop) 
                  }
                }
                if ( checkedAno2 == true && checkedMat == true) {
                  setColorAno2p(styles.cellAno)
                  if (checkedAno2 == true && checkedPort == true) {
                    setColorAno2p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno2p(styles.anop) 
                  }
                }
                if ( checkedAno3 == true && checkedMat == true) {
                  setColorAno3p(styles.cellAno)
                  if (checkedAno3 == true && checkedPort == true) {
                    setColorAno3p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno3p(styles.anop) 
                  }
                }
                if ( checkedAno4 == true && checkedMat == true) {
                  setColorAno4p(styles.cellAno)
                  if (checkedAno4 == true && checkedPort == true) {
                    setColorAno4p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno4p(styles.anop) 
                  }
                }
                if ( checkedAno5 == true && checkedMat == true) {
                  setColorAno5p(styles.cellAno)
                  if (checkedAno5 == true && checkedPort == true) {
                    setColorAno5p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno5p(styles.anop) 
                  }
                }
                if ( checkedAno6 == true && checkedMat == true) {
                  setColorAno6p(styles.cellAno)
                  if (checkedAno6 == true && checkedPort == true) {
                    setColorAno6p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno6p(styles.anop) 
                  }
                }
                if ( checkedAno7 == true && checkedMat == true) {
                  setColorAno7p(styles.cellAno)
                  if (checkedAno7 == true && checkedPort == true) {
                    setColorAno7p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno7p(styles.anop) 
                  }
                }
                if ( checkedAno8 == true && checkedMat == true) {
                  setColorAno8p(styles.cellAno)
                  if (checkedAno8 == true && checkedPort == true) {
                    setColorAno8p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno8p(styles.anop) 
                  }
                }
                if ( checkedAno9 == true && checkedMat == true) {
                  setColorAno9p(styles.cellAno)
                  if (checkedAno9 == true && checkedPort == true) {
                    setColorAno9p(styles.cellAno) 
                  }
                  if (checkedPort) {
                    setColorAno9p(styles.anop) 
                  }
                }

                if (checkedMat == false) {
                  !checkedPort ? setColorAno1m(styles.anom) : (checkedAno1 == true) ? setColorAno1m(styles.cellAno) : "";
                  !checkedPort ? setColorAno2m(styles.anom) : (checkedAno2 == true) ? setColorAno2m(styles.cellAno) : "";
                  !checkedPort ? setColorAno3m(styles.anom) : (checkedAno3 == true) ? setColorAno3m(styles.cellAno) : "";
                  !checkedPort ? setColorAno4m(styles.anom) : (checkedAno4 == true) ? setColorAno4m(styles.cellAno) : "";
                  !checkedPort ? setColorAno5m(styles.anom) : (checkedAno5 == true) ? setColorAno5m(styles.cellAno) : "";
                  !checkedPort ? setColorAno6m(styles.anom) : (checkedAno6 == true) ? setColorAno6m(styles.cellAno) : "";
                  !checkedPort ? setColorAno7m(styles.anom) : (checkedAno7 == true) ? setColorAno7m(styles.cellAno) : "";
                  !checkedPort ? setColorAno8m(styles.anom) : (checkedAno8 == true) ? setColorAno8m(styles.cellAno) : "";
                  !checkedPort ? setColorAno9m(styles.anom) : (checkedAno9 == true) ? setColorAno9m(styles.cellAno) : "";
                }
                //#endregion
                            
                if (!checkedPort) {
                  setPortName("portugues");
               } else {
                  setPortName("");
               }
              }}
              
            />
            <Text style={styles.textNames}>Português</Text>
          </View>

          <View style={styles.checkboxAnos}>
            <Checkbox
              status={checkedMat ? "checked" : "unchecked"}
              onPress={() => {
                setCheckedMat(!checkedMat);                
                !checkedMat ? setColorMat(styles.cellMat) : setColorMat(styles.cell2)
              //#region LogicaAnosMat
                if(checkedAnoTodos == true && checkedPort == true){
                  setColorAno1m(styles.cellAno)                  
                  setColorAno2m(styles.cellAno)                  
                  setColorAno3m(styles.cellAno)                  
                  setColorAno4m(styles.cellAno)                  
                  setColorAno5m(styles.cellAno)
                  setColorAno6m(styles.cellAno)
                  setColorAno7m(styles.cellAno)
                  setColorAno8m(styles.cellAno)
                  setColorAno9m(styles.cellAno)

                  if (checkedAnoTodos == true && checkedMat == true)
                  {
                    setColorAno1m(styles.cellAno)                  
                    setColorAno2m(styles.cellAno)                  
                    setColorAno3m(styles.cellAno)                  
                    setColorAno4m(styles.cellAno)                  
                    setColorAno5m(styles.cellAno)
                    setColorAno6m(styles.cellAno)
                    setColorAno7m(styles.cellAno)
                    setColorAno8m(styles.cellAno)
                    setColorAno9m(styles.cellAno)                  
                  }
                  
                  if (checkedMat) {
                    setColorAno1m(styles.anom)     
                    setColorAno2m(styles.anom)                  
                    setColorAno3m(styles.anom)                  
                    setColorAno4m(styles.anom)                  
                    setColorAno5m(styles.anom)
                    setColorAno6m(styles.anom)
                    setColorAno7m(styles.anom)
                    setColorAno8m(styles.anom)
                    setColorAno9m(styles.anom)
                  }         
                  
                  if (checkedAno1 == false) {
                    setColorAno1m(styles.anom)
                  }
                  if (checkedAno2 == false) {
                    setColorAno2m(styles.anom)
                  }
                  if (checkedAno3 == false) {
                    setColorAno3m(styles.anom)
                  }
                  if (checkedAno4 == false) {
                    setColorAno4m(styles.anom)
                  }
                  if (checkedAno5 == false) {
                    setColorAno5m(styles.anom)
                  }
                  if (checkedAno6 == false) {
                    setColorAno6m(styles.anom)
                  }
                  if (checkedAno7 == false) {
                    setColorAno7m(styles.anom)
                  }
                  if (checkedAno8 == false) {
                    setColorAno8m(styles.anom)
                  }
                  if (checkedAno9 == false) {
                    setColorAno9m(styles.anom)
                  }
                } 

                if ( checkedAno1 == true && checkedPort == true) {
                  setColorAno1m(styles.cellAno)
                  if (checkedAno1 == true && checkedMat == true) {
                    setColorAno1m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno1m(styles.anom) 
                  }
                }
                if ( checkedAno2 == true && checkedPort == true) {
                  setColorAno2m(styles.cellAno)
                  if (checkedAno2 == true && checkedMat == true) {
                    setColorAno2m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno2m(styles.anom) 
                  }
                }
                if ( checkedAno3 == true && checkedPort == true) {
                  setColorAno3m(styles.cellAno)
                  if (checkedAno3 == true && checkedMat == true) {
                    setColorAno3m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno3m(styles.anom) 
                  }
                }
                if ( checkedAno4 == true && checkedPort == true) {
                  setColorAno4m(styles.cellAno)
                  if (checkedAno4 == true && checkedMat == true) {
                    setColorAno4m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno4m(styles.anom) 
                  }
                }
                if ( checkedAno5 == true && checkedPort == true) {
                  setColorAno5m(styles.cellAno)
                  if (checkedAno5 == true && checkedMat == true) {
                    setColorAno5m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno5m(styles.anom) 
                  }
                }
                if ( checkedAno6 == true && checkedPort == true) {
                  setColorAno6m(styles.cellAno)
                  if (checkedAno6 == true && checkedMat == true) {
                    setColorAno6m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno6m(styles.anom) 
                  }
                }
                if ( checkedAno7 == true && checkedPort == true) {
                  setColorAno7m(styles.cellAno)
                  if (checkedAno7 == true && checkedMat == true) {
                    setColorAno7m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno7m(styles.anom) 
                  }
                }
                if ( checkedAno8 == true && checkedPort == true) {
                  setColorAno8m(styles.cellAno)
                  if (checkedAno8 == true && checkedMat == true) {
                    setColorAno8m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno8m(styles.anom) 
                  }
                }
                if ( checkedAno9 == true && checkedPort == true) {
                  setColorAno9m(styles.cellAno)
                  if (checkedAno9 == true && checkedMat == true) {
                    setColorAno9m(styles.cellAno) 
                  }
                  if (checkedMat) {
                    setColorAno9m(styles.anom) 
                  }
                }

                if(checkedPort == false){
                  !checkedMat ? setColorAno1p(styles.anop) : (checkedAno1 == true) ? setColorAno1p(styles.cellAno) : "";
                  !checkedMat ? setColorAno2p(styles.anop) : (checkedAno2 == true) ? setColorAno2p(styles.cellAno) : "";
                  !checkedMat ? setColorAno3p(styles.anop) : (checkedAno3 == true) ? setColorAno3p(styles.cellAno) : "";
                  !checkedMat ? setColorAno4p(styles.anop) : (checkedAno4 == true) ? setColorAno4p(styles.cellAno) : "";
                  !checkedMat ? setColorAno5p(styles.anop) : (checkedAno5 == true) ? setColorAno5p(styles.cellAno) : "";
                  !checkedMat ? setColorAno6p(styles.anop) : (checkedAno6 == true) ? setColorAno6p(styles.cellAno) : "";
                  !checkedMat ? setColorAno7p(styles.anop) : (checkedAno7 == true) ? setColorAno7p(styles.cellAno) : "";
                  !checkedMat ? setColorAno8p(styles.anop) : (checkedAno8 == true) ? setColorAno8p(styles.cellAno) : "";
                  !checkedMat ? setColorAno9p(styles.anop) : (checkedAno9 == true) ? setColorAno9p(styles.cellAno) : "";
                }
                //#endregion
               
                if (!checkedMat) {
                  setMatName("matematica");
               } else {
                  setMatName("");
               }
              }}
            />
            <Text style={styles.textNames}>Matemática</Text>
          </View>
        </View>
      </View>

      {/*Anos Checkboxes*/}
      <Text style={styles.textTitles}>Ano</Text>
      <View>
        <View style={styles.box}>

         {/*Linha1CheckBoxes */ }
        <View style={styles.checkboxAno}>
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAnoTodos ? "checked" : "unchecked"}
              onPress={() => {
                setCheckedAnoTodos(!checkedAnoTodos);

              //#region LogicaAnos                
                if (!checkedAno1) {
                  setcheckedAno1(!checkedAno1)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno1(false)
                }
                if (!checkedAno2) {
                  setcheckedAno2(!checkedAno2)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno2(false)
                }
                if (!checkedAno3) {
                  setcheckedAno3(!checkedAno3)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno3(false)
                }
                if (!checkedAno4) {
                  setcheckedAno4(!checkedAno4)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno4(false)
                }
                if (!checkedAno5) {
                  setcheckedAno5(!checkedAno5)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno5(false)
                }
                if (!checkedAno6) {
                  setcheckedAno6(!checkedAno6)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno6(false)
                }
                if (!checkedAno7) {
                  setcheckedAno7(!checkedAno7)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno7(false)
                }
                if (!checkedAno8) {
                  setcheckedAno8(!checkedAno8)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno8(false)
                } 
                if (!checkedAno9) {
                  setcheckedAno9(!checkedAno9)
                }              
                if (checkedAnoTodos) {
                  setcheckedAno9(false)
                }

                if(!checkedAnoTodos && checkedMat == false && checkedPort == false){
                   setColorAno1p(styles.cellAno)  
                   setColorAno2p(styles.cellAno)  
                   setColorAno3p(styles.cellAno)  
                   setColorAno4p(styles.cellAno)  
                   setColorAno5p(styles.cellAno)  
                   setColorAno6p(styles.cellAno)  
                   setColorAno7p(styles.cellAno)  
                   setColorAno8p(styles.cellAno)  
                   setColorAno9p(styles.cellAno)  
                  
                   setColorAno1m(styles.cellAno)   
                   setColorAno2m(styles.cellAno)   
                   setColorAno3m(styles.cellAno)   
                   setColorAno4m(styles.cellAno)   
                   setColorAno5m(styles.cellAno)   
                   setColorAno6m(styles.cellAno)   
                   setColorAno7m(styles.cellAno)   
                   setColorAno8m(styles.cellAno)   
                   setColorAno9m(styles.cellAno)       
                }  
                if (checkedAnoTodos && checkedMat == false && checkedPort == false) {
                  setColorAno1p(styles.anop)
                  setColorAno2p(styles.anop)
                  setColorAno3p(styles.anop)
                  setColorAno4p(styles.anop)
                  setColorAno5p(styles.anop)
                  setColorAno6p(styles.anop)
                  setColorAno7p(styles.anop)
                  setColorAno8p(styles.anop)
                  setColorAno9p(styles.anop)

                  setColorAno1m(styles.anom)
                  setColorAno2m(styles.anom)
                  setColorAno3m(styles.anom)
                  setColorAno4m(styles.anom)
                  setColorAno5m(styles.anom)
                  setColorAno6m(styles.anom)
                  setColorAno7m(styles.anom)
                  setColorAno8m(styles.anom)
                  setColorAno9m(styles.anom)
                }

                if(!checkedAnoTodos && checkedMat == true && checkedPort == true){
                  setColorAno1p(styles.cellAno)  
                  setColorAno2p(styles.cellAno)  
                  setColorAno3p(styles.cellAno)  
                  setColorAno4p(styles.cellAno)  
                  setColorAno5p(styles.cellAno)  
                  setColorAno6p(styles.cellAno)  
                  setColorAno7p(styles.cellAno)  
                  setColorAno8p(styles.cellAno)  
                  setColorAno9p(styles.cellAno)  
                 
                  setColorAno1m(styles.cellAno)   
                  setColorAno2m(styles.cellAno)   
                  setColorAno3m(styles.cellAno)   
                  setColorAno4m(styles.cellAno)   
                  setColorAno5m(styles.cellAno)   
                  setColorAno6m(styles.cellAno)   
                  setColorAno7m(styles.cellAno)   
                  setColorAno8m(styles.cellAno)   
                  setColorAno9m(styles.cellAno)       
                }  
                if (checkedAnoTodos && checkedMat == true && checkedPort == true) {
                  setColorAno1p(styles.anop)
                  setColorAno2p(styles.anop)
                  setColorAno3p(styles.anop)
                  setColorAno4p(styles.anop)
                  setColorAno5p(styles.anop)
                  setColorAno6p(styles.anop)
                  setColorAno7p(styles.anop)
                  setColorAno8p(styles.anop)
                  setColorAno9p(styles.anop)

                  setColorAno1m(styles.anom)
                  setColorAno2m(styles.anom)
                  setColorAno3m(styles.anom)
                  setColorAno4m(styles.anom)
                  setColorAno5m(styles.anom)
                  setColorAno6m(styles.anom)
                  setColorAno7m(styles.anom)
                  setColorAno8m(styles.anom)
                  setColorAno9m(styles.anom)
                }

                if (!checkedAnoTodos && checkedMat == true && checkedPort == false) { 
                  setColorAno1m(styles.cellAno)   
                  setColorAno2m(styles.cellAno)   
                  setColorAno3m(styles.cellAno)   
                  setColorAno4m(styles.cellAno)   
                  setColorAno5m(styles.cellAno)   
                  setColorAno6m(styles.cellAno)   
                  setColorAno7m(styles.cellAno)   
                  setColorAno8m(styles.cellAno)   
                  setColorAno9m(styles.cellAno)    
                }
                if (checkedAnoTodos && checkedMat == true && checkedPort == false) {
                  setColorAno1m(styles.anom)
                  setColorAno2m(styles.anom)
                  setColorAno3m(styles.anom)
                  setColorAno4m(styles.anom)
                  setColorAno5m(styles.anom)
                  setColorAno6m(styles.anom)
                  setColorAno7m(styles.anom)
                  setColorAno8m(styles.anom)
                  setColorAno9m(styles.anom)
                }

                if (!checkedAnoTodos && checkedMat == false && checkedPort == true) { 
                  setColorAno1p(styles.cellAno)  
                  setColorAno2p(styles.cellAno)  
                  setColorAno3p(styles.cellAno)  
                  setColorAno4p(styles.cellAno)  
                  setColorAno5p(styles.cellAno)  
                  setColorAno6p(styles.cellAno)  
                  setColorAno7p(styles.cellAno)  
                  setColorAno8p(styles.cellAno)  
                  setColorAno9p(styles.cellAno)  
                }
                if (checkedAnoTodos && checkedMat == false && checkedPort == true) {
                  setColorAno1p(styles.anop)
                  setColorAno2p(styles.anop)
                  setColorAno3p(styles.anop)
                  setColorAno4p(styles.anop)
                  setColorAno5p(styles.anop)
                  setColorAno6p(styles.anop)
                  setColorAno7p(styles.anop)
                  setColorAno8p(styles.anop)
                  setColorAno9p(styles.anop)
                 }

              
              //#endregion
              }}
            />
            <Text style={styles.textNames}>Todos</Text>
          </View>

          {/*1°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAno1 ? "checked" : "unchecked"}
              onPress={() => {
                setcheckedAno1(!checkedAno1);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno1 ? setColorAno1p(styles.cellAno) : setColorAno1p(styles.anop)
                  !checkedAno1 ? setColorAno1m(styles.cellAno) : setColorAno1m(styles.anom)            
                }                 

                if (checkedMat == true) {
                  !checkedAno1 ? setColorAno1m(styles.cellAno) : setColorAno1m(styles.anom);                 
                }
                if (checkedPort == true) {
                  !checkedAno1 ? setColorAno1p(styles.cellAno) : setColorAno1p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>1° Ano</Text>
          </View>

           {/*2°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAno2 ? "checked" : "unchecked"}
              onPress={() => {
                setcheckedAno2(!checkedAno2);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno2 ? setColorAno2p(styles.cellAno) : setColorAno2p(styles.anop)
                  !checkedAno2 ? setColorAno2m(styles.cellAno) : setColorAno2m(styles.anom)            
                }

                if (checkedMat == true) {
                  !checkedAno2 ? setColorAno2m(styles.cellAno) : setColorAno2m(styles.anom);
                }
                if (checkedPort == true) {
                  !checkedAno2 ? setColorAno2p(styles.cellAno) : setColorAno2p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>2° Ano</Text>
          </View>

           {/*3°Ano */ }   
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAno3 ? "checked" : "unchecked"}
              onPress={() => {
                setcheckedAno3(!checkedAno3);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno3 ? setColorAno3p(styles.cellAno) : setColorAno3p(styles.anop)
                  !checkedAno3 ? setColorAno3m(styles.cellAno) : setColorAno3m(styles.anom)            
                 }
                if (checkedMat == true) {
                  !checkedAno3 ? setColorAno3m(styles.cellAno) : setColorAno3m(styles.anom);
                }
                if (checkedPort == true) {
                  !checkedAno3 ? setColorAno3p(styles.cellAno) : setColorAno3p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>3° Ano</Text>
          </View>

           {/*4°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAno4 ? "checked" : "unchecked"}
              onPress={() => {
                setcheckedAno4(!checkedAno4);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno4 ? setColorAno4p(styles.cellAno) : setColorAno4p(styles.anop)
                  !checkedAno4 ? setColorAno4m(styles.cellAno) : setColorAno4m(styles.anom)            
                 }
                if (checkedMat == true) {
                  !checkedAno4 ? setColorAno4m(styles.cellAno) : setColorAno4m(styles.anom);
                }
                if (checkedPort == true) {
                  !checkedAno4 ? setColorAno4p(styles.cellAno) : setColorAno4p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>4° Ano</Text>
          </View>
        </View>
         {/*Linha2CheckBoxes */ }
        <View style={styles.checkboxAno}>
           {/*5°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
             status={checkedAno5 ? "checked" : "unchecked"}
             onPress={() => {
              setcheckedAno5(!checkedAno5);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno5 ? setColorAno5p(styles.cellAno) : setColorAno5p(styles.anop)
                  !checkedAno5 ? setColorAno5m(styles.cellAno) : setColorAno5m(styles.anom)            
                 }
                if (checkedMat == true) {
                  !checkedAno5 ? setColorAno5m(styles.cellAno) : setColorAno5m(styles.anom);
               }
               if (checkedPort == true) {
                !checkedAno5 ? setColorAno5p(styles.cellAno) : setColorAno5p(styles.anop);                 
              }
              }}
            />
            <Text style={styles.textNames}>5° Ano</Text>
          </View>

           {/*6°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
             status={checkedAno6 ? "checked" : "unchecked"}
             onPress={() => {
              setcheckedAno6(!checkedAno6);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno6 ? setColorAno6p(styles.cellAno) : setColorAno6p(styles.anop)
                  !checkedAno6 ? setColorAno6m(styles.cellAno) : setColorAno6m(styles.anom)            
                 }
                if (checkedMat == true) {
                  !checkedAno6 ? setColorAno6m(styles.cellAno) : setColorAno6m(styles.anom);
               }
               if (checkedPort == true) {
                !checkedAno6 ? setColorAno6p(styles.cellAno) : setColorAno6p(styles.anop);                 
              }
              }}
            />
            <Text style={styles.textNames}>6° Ano</Text>
          </View>

           {/*7°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
           status={checkedAno7 ? "checked" : "unchecked"}
           onPress={() => {
                setcheckedAno7(!checkedAno7);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno7 ? setColorAno7p(styles.cellAno) : setColorAno7p(styles.anop)
                  !checkedAno7 ? setColorAno7m(styles.cellAno) : setColorAno7m(styles.anom)            
                }
                if (checkedMat == true) {
                  !checkedAno7 ? setColorAno7m(styles.cellAno) : setColorAno7m(styles.anom);
                }
                if (checkedPort == true) {
                  !checkedAno7 ? setColorAno7p(styles.cellAno) : setColorAno7p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>7° Ano</Text>
          </View>

           {/*8°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
              status={checkedAno8 ? "checked" : "unchecked"}
              onPress={() => {
                setcheckedAno8(!checkedAno8);
                if (checkedMat == false && checkedPort == false) {                 
                  !checkedAno8 ? setColorAno8p(styles.cellAno) : setColorAno8p(styles.anop)
                  !checkedAno8 ? setColorAno8m(styles.cellAno) : setColorAno8m(styles.anom)            
                 }
                if (checkedMat == true) {
                  !checkedAno8 ? setColorAno8m(styles.cellAno) : setColorAno8m(styles.anom);
                }
                if (checkedPort == true) {
                  !checkedAno8 ? setColorAno8p(styles.cellAno) : setColorAno8p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>8° Ano</Text>
          </View>

           {/*9°Ano */ }
          <View style={styles.checkbox}>
            <Checkbox
               status={checkedAno9 ? "checked" : "unchecked"}
               onPress={() => {
                  setcheckedAno9(!checkedAno9);
                  if (checkedMat == false && checkedPort == false) {                 
                    !checkedAno9 ? setColorAno9p(styles.cellAno) : setColorAno9p(styles.anop)
                    !checkedAno9 ? setColorAno9m(styles.cellAno) : setColorAno9m(styles.anom)            
                  }
                  if (checkedMat == true) {
                    !checkedAno9 ? setColorAno9m(styles.cellAno) : setColorAno9m(styles.anom);
                 }
                 if (checkedPort == true) {
                  !checkedAno9 ? setColorAno9p(styles.cellAno) : setColorAno9p(styles.anop);                 
                }
              }}
            />
            <Text style={styles.textNames}>9° Ano</Text>
          </View>
        </View>

        </View>
      </View>
   
      <View style={{ flexDirection: "row", justifyContent: 'center', alignContent: 'center' }}>
        {/* 
        <View style={{ justifyContent:'center', alignContent:'center'} }>
          <IconButton icon="email" iconColor="#FFF" mode="contained" style={{marginTop:15,  marginBottom:10, width:100,backgroundColor:'#B2B2B2'}} onPress={compartilhar}>Compartilhar</IconButton>
        </View>*/ }
        <View style={{ justifyContent:'center', alignContent:'center'} }>
          <IconButton icon="download" iconColor="#FFF" mode="contained" style={{marginTop:15, marginBottom:10, width:130, backgroundColor:'#B2B2B2',}} onPress={excel}>Download</IconButton>
          </View>
      </View>

      {/*Tabela */}
      <ScrollView style={{marginBottom:220,} }>
        <Grid style={styles.grid}>
          <Col size={50}>
            <Row style={colorPort}>
              <Text style={styles.tableColumnTitle}>Português</Text>
            </Row>
            <Row style={colorPort}>
              <Text style={styles.text}>
                Competências específicas de componente
              </Text>
            </Row>
            <Row style={colorPort}>
              <Text style={styles.text}>Campos de atuação</Text>
            </Row>
            <Row style={colorPort}>
              <Text style={styles.text}>Práticas de linguagem</Text>
            </Row>
            <Row style={colorPort}>
              <Text style={styles.text}>Objetos de conhecimento</Text>
            </Row>
            <Row style={colorPort}>
              <Text style={styles.text}>HABILIDADES</Text>
            </Row>

            {/*PORTUGUES Fim */}
            {/*ANOS PORT */}
            <Row style={ano1p}>
              <Text style={styles.text}>1°Ano</Text>
            </Row>
            <Row style={ano2p}>
              <Text style={styles.text}>2°Ano</Text>
            </Row>
            <Row style={ano3p}>
              <Text style={styles.text}>3°Ano</Text>
            </Row>
            <Row style={ano4p}>
              <Text style={styles.text}>4°Ano</Text>
            </Row>
            <Row style={ano5p}>
              <Text style={styles.text}>5°Ano</Text>
            </Row>
            <Row style={ano6p}>
              <Text style={styles.text}>6°Ano</Text>
            </Row>
            <Row style={ano7p}>
              <Text style={styles.text}>7°Ano</Text>
            </Row>
            <Row style={ano8p}>
              <Text style={styles.text}>8°Ano</Text>
            </Row>
            <Row style={ano9p}>
              <Text style={styles.text}>9°Ano</Text>
            </Row>
          </Col>

          {/*MATEMATICA */}
          <Col size={50}>
            <Row style={colorMat}>
              <Text style={styles.tableColumnTitle}>Matemática</Text>
            </Row>
            <Row style={colorMat}>
              <Text></Text>
            </Row>
            <Row style={colorMat}>
              <Text style={styles.text}>Unidades temáticas</Text>
            </Row>
            <Row style={colorMat}>
              <Text style={styles.text}></Text>
            </Row>
            <Row style={colorMat}>
              <Text style={styles.text}>Objetos de conhecimento</Text>
            </Row>
            <Row style={colorMat}>
              <Text style={styles.text}>HABILIDADES</Text>
            </Row>

            {/*ANOS MAT */}
            <Row style={ano1m}>
              <Text style={styles.text}>1°Ano</Text>
            </Row>
            <Row style={ano2m}>
              <Text style={styles.text}>2°Ano</Text>
            </Row>
            <Row style={ano3m}>
              <Text style={styles.text}>3°Ano</Text>
            </Row>
            <Row style={ano4m}>
              <Text style={styles.text}>4°Ano</Text>
            </Row>
            <Row style={ano5m}>
              <Text style={styles.text}>5°Ano</Text>
            </Row>
            <Row style={ano6m}>
              <Text style={styles.text}>6°Ano</Text>
            </Row>
            <Row style={ano7m}>
              <Text style={styles.text}>7°Ano</Text>
            </Row>
            <Row style={ano8m}>
              <Text style={styles.text}>8°Ano</Text>
            </Row>
            <Row style={ano9m}>
              <Text style={styles.text}>9°Ano</Text>
            </Row>
          </Col>
        </Grid>
      </ScrollView>  
      
    </View>
  );
}

export default App;
var width = Dimensions.get('window').width; 
const styles = StyleSheet.create({
  container: {
    width: "100%",
    height: 600,
    backgroundColor: "#fff",
  },
  cell: {
    height: 80,
    color: "#FFF",
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    backgroundColor: "#00a6b2",
    justifyContent: "center",
    alignItems: "center",
  },
  cell2: {
    height: 80,
    color: "#FFF",
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    backgroundColor: "#6f42c1",
    justifyContent: "center",
    alignItems: "center",
  },
  anop: {
    height: 80,
    color: "#FFF",
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    backgroundColor: "#00a6b2",
    justifyContent: "center",
    alignItems: "center",
  },
  anom: {
    height: 80,
    color: "#FFF",
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    backgroundColor: "#6f42c1",
    justifyContent: "center",
    alignItems: "center",
  },
  cellAno: {
    height: 80,
    color: "#FFF",
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    backgroundColor: "#777",
    justifyContent: "center",
    alignItems: "center",
  },
  cellPort: {
    height: 80,
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#777",
  },
  cellMat: {
    height: 80,
    borderWidth: 1,
    borderColor: "#ddd",
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#777",
  },
  grid: {
    marginTop: 0,
  },
  text: {
    color: "#FFF",
    padding: 5,
  },
  textAno: {
    color: "#000000",
    padding: 5,
  },
  textTitles: {
    fontStyle: "italic",
    fontWeight: 500,
    fontSize: 15,
    color: "#838383",
    textDecorationLine: "underline",
    marginTop: 3,
    marginLeft: 10,
  },
  tableColumnTitle: {
    color: "#FFF",
    fontWeight: 500,
    fontSize: 15,
    textDecorationLine: "underline",  
  },
  checkbox: {
    flexDirection: "row",
    alignItems: "center",
    width:75,
    
  },
  checkboxAnos: {
    flexDirection: "row",
    alignItems: "center",
    
  },
  checkboxComp: {
    marginTop: 4,
    flexDirection: "row",
    alignItems: "center",
  },
  checkboxAno: {
    marginTop: 5,
    flexDirection: "row",
    alignItems: "center",
    width: 45,    
  },
  box: {
    borderBottomWidth: 0.7,
    width: width,

  },
  textNames: {
    color:"#434242"
  }
});
