//bibliotecas para o wifi
#include <ESP8266WiFi.h>

//Variáveis para fazer a concção ao wifi
const char* ssid = "default"; // Nome da rede Wireless
const char* password = ""; // Password da rede Wireless
const char* host = "192.168.1.6"; // Endereço do PC do Apache
String url, url1; // Irá de conter a msg HTTP enviada para o Apache
String s; // Irá conter o estado do botão nº4 (0 ou 1)
WiFiClient client; // WiFiClient, permite enviar ou receber dados por TCP/IP 
const int httpPort = 80; 

//Variáveis de controlo da leitura das mensagens
String ESPrx = "";

//Variáveis de controlo para implementação do Protocolo LSV2
String telegramaRecebido = "";
String MensagemEnviar = "";
//um bloco, na fase de transferência de dados, tem no máximo 128 caracters neste tipo de funções
byte buffByte_tx[128];
bool espEmissor = false;
bool espRecetor = false;

//Varáiveis de controlo para simulação da tecla
int SimularTecla_Counter = 0;
bool SimularTecla_Controlo = false;

//Quando se clica no botão do start, esta variável é que vai "indicar" para começar a realizar o start romoto
bool Percebi_StartRomoto = 0;
//Serve para eviar que mais que 1 caracter <ENQ> seja enviado
bool Nao_Comecar = 1;

void setup() {
  /* Configuração da porta série
      115 200 bits por segundo
      8 bits de dados
      Sem paridade
      1 stop bit
  */
  Serial.begin(115200, SERIAL_8N1);

  // O ESP irá como Client Wireless
  WiFi.mode(WIFI_STA); // O ESP irá como Client Wireless
  WiFi.begin(ssid, password); // ESP tenta ligar ao Router


  // status da ligação Wifi com o router= 0 - desligado 4- ligado ...
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
  }

  // Led built in para saber que a conecção aconteceu
  pinMode(BUILTIN_LED, OUTPUT);

}

void loop() {
  /*-------------------------------------------------------------

   Implementação de pressionar o botão no website e ele fazer o strat romoto

   ------------------------------------------------------------
   */

  // ------------------- TENTA LIGAR POR TCP/IP ao APACHE
  // Se o status da ligação TCP/IP entre o ESP e o PC remoto for 0 (client.status= 0 - desligado)

  // Neste caso, o ESP tenta estabelecer ligacao TCP/IP com o Apache
  if (client.status() == 0) {
    if (!client.connect(host, httpPort)) { // ------------------- Tenta LIGAÇAO TCP
      client.flush();
      delay(1000);
      //Desligar o LED para saber que não há conecção
      digitalWrite(BUILTIN_LED, HIGH);
    }
  }


  if (client.status() == 4) {

    //Ligar o LED para saber que há conecção
    digitalWrite(BUILTIN_LED, LOW);

    url = "GET /heid2.php HTTP/1.1\r\nHost: host\r\nConnection: keep-alive\r\n\r\n";
    client.print(url); // Envia pedido HTTP, da linha anterior "url", para o Apache
    // Espera pela resposta do Apache
    delay(100);

    //Iniciação para fazer o strat remoto
    if ( Percebi_StartRomoto == 1 && Nao_Comecar == 1) {
      espEmissor = true;
      SimularTecla_Controlo = true;
      // para enviar o <ENQ> e assim iniciar a conversação
      lsv2_FaseInquerito(); 
      Percebi_StartRomoto = 0;
      Nao_Comecar = 0;

      // inserir na base de dados para dizer que se está a fazer um start romoto
      //através de um pedido GET ao ficheiro Update2 presente no htdocs do XAMPP
      url = "GET /Update2.php";
      client.println(url);
      client.println(" HTTP/1.1");
      client.println("Host: 192.168.1.6");
      client.println("Connection: close");

      delay(50);
    }
  }

    // ------------------- SE CHEGARAM DADOS por TCP/IP ....
  if (client.available() > 0) { // Se o ESP tiver recebido bytes
    // Le todos os carateres enviados pelo Apache até receber o caracter 'ç' ou exceder um tempo máximo.
    String line = client.readStringUntil('ç');
    //Se ao ler, percebeu a existência de "Y1:start_remoto", quer dizer que se pressionou o botão no website e é para começar a fazer o start romoto
    if (line.indexOf("Y1:start_remoto")>0){
      Percebi_StartRomoto = 1;
    }
    else {
      Percebi_StartRomoto = 0;
    }
    
    client.flush();
    delay(200);
  }


/*-------------------------------------------------------------

   Leitura da porta série

   ------------------------------------------------------------

  /* Recepção pela porta série (CNC que comunicar) */
  int bytestoread = Serial.available();
  if (bytestoread > 0) {
    // delay para garantir correta leitura de todos os bytes
    delay(100);

    // Leitura dos dados:
    // Vai guardar no array "rx" todos os bytes que chegarem
    byte rx[bytestoread];
    Serial.readBytes(rx, bytestoread);

    // Interpretar a mesnagem
    // Passagem de bytes para caracter
    for (int i = 0; i < (bytestoread) ; i++) {
      // (char) é um função da linguagem de programação que permite dar "cast"
      // Converte um valor para o "data type" tipo caracter
      ESPrx += (char)rx[i];
    }


    //Caso estejamos a enviar um programa, ele vai à função
    // "SimularTecla_Controlo" para saber as mesangens que tem que enviar
    if (SimularTecla_Controlo) {
      lsv2_SimularTecla();
    }

    //Ver a que corresponde a mensagem que chegou, segundo o LSV2
    //int(x) passa o caracter x para o decimal correspondente (tabela ASCII)

    // compara o caracter que chegou com ao decimal 5
    if ( int(ESPrx[0]) == 5 ) {

      //Como percebeu o caracter 5 (<ENQ>), O tnc está a pedir para comunciar e portanto o ESP é:
      //Recetor, e relativamente aos lsv2 corresponde à fase de inquerito
      espRecetor = true;
      lsv2_FaseInquerito();
    }

    // compara o caracter que chegou com o hexadecimal 4
    if ( int(ESPrx[0]) == 4 ) {

      //Como percebeu o caracter 4, não precisa de fazer nada
      //apenas limpar a variável de entrada para não haver mais nenhuma comparação desnecessária
      ESPrx = "";
    }

    // compara os caracteres que chegaram com o hexadecimal 10 e 30
    // é uma condição AND (&&), logo ambas têm que ser verdadeiras para o "if" correr
    if ( int(ESPrx[0]) == 16 && int(ESPrx[1]) == 48 ) {

      //Como percebeu os caracteres <DLE><0>, a cnc percebeu o pedido de inquerito (<ENQ>) logo o ESP é
      //emissor para responder com a mensagem, e relativamente aos lsv2 corresponde à fase de transferência de dados
      espEmissor = true;
      lsv2_TransferenciaDeDados();
    }

    // compara os caracteres que chegaram com o hexadecimal 10 e 31
    // é uma condição AND (&&), logo ambas têm que ser verdadeiras para o "if" correr
    if ( int(ESPrx[0]) == 16 && int(ESPrx[1]) == 49 ) {

      //Como percebeu os caracteres <DLE><1>, a cnc percebeu a mensagem logo o ESP é
      //emissor para responder com <EOT>, e relativamente aos lsv2 corresponde à fase de repouso
      espEmissor = true;
      lsv2_FaseRepouso();
    }

    // TNC está a enviar um telegrama
    // Só para não dar erros de tentar aceder caracteres que não existem na comparação seguinte (uma mensagem tem sempre no minimo 5 caracteres)
    if (ESPrx.length() >= 5) {
      // Esperar pelos últimos caracteres sem ser o BCC (Assumir que BCC está sempre certo)
      if ( int(ESPrx[ESPrx.length() - 3]) == 16 && int(ESPrx[ESPrx.length() - 2]) == 3 ) {

        //Como percebeu uma mensagem, logo o ESP é
        //recetor para responder com <DLE><1>, e relativamente aos lsv2 corresponde à fase de transferência de dados
        espRecetor = true;
        lsv2_TransferenciaDeDados();
      }
    }

  }
}



/*-------------------------------------------------------------

   Funções para para implementar o LSV2

   ------------------------------------------------------------
   Nota geral:
   "Escrever" para a porta sempre em bytes! (Serial.write e não Serial.print)
*/


void lsv2_FaseInquerito() {
  if (espRecetor) {
    //<DLE><0>
    byte buff[] = {16, 48};
    Serial.write(buff, 2);
    espRecetor = false;
  }
  if (espEmissor) {
    //<ENQ>
    Serial.write(5);
    espEmissor = false;
  }
  ESPrx = "";
}

//ESP recebeu uma mensagem da CNC e quer interperta-la para depois mandar DLE 1 --> "recebei a mensagem com sucesso"
// Ou
// quer enviar um telegrama para a CNC
void lsv2_TransferenciaDeDados() {

  if (espRecetor) {
    //Retirar os caracteres especiais para comparação facilitada
    // -3 para retirar o  <DLE>, <ETX> e BCC
    // 2 para começar depois do <DLE> e <STX>
    telegramaRecebido = ESPrx.substring(2, ESPrx.length() - 3);

    //<DLE><1>
    byte buff[2] = {16, 49};
    Serial.write(buff, 2);
    espRecetor = false;

  }

  if (espEmissor) {
    // Pré criar um array para acomodar a mensagem + os caracteres especiais (são 5: <DLE><STX>MENSAGEM<DLE><ETX><BCC>)
    int tamanhoMensagem = MensagemEnviar.length();
    byte buffByte[tamanhoMensagem + 5];
    for (int i = 0; i < tamanhoMensagem; i++) {

      // No inicio da mensagem colocar os caracteres <DLE><STX>
      if (i == 0) {
        buffByte[0] = 16;
        buffByte[1] = 2;
      }

      buffByte[i + 2] = buffByte_tx[i];

      // No fim da mensagem colocar <DLE><ETX><BCC>
      if (i == (tamanhoMensagem - 1)) {
        buffByte[tamanhoMensagem + 2] = 16;
        buffByte[tamanhoMensagem + 3] = 3;
        buffByte[tamanhoMensagem + 4] = BCC(MensagemEnviar);
      }
    }

    Serial.write(buffByte, (tamanhoMensagem + 5));
    espEmissor = false;
    // É preciso limpar sempre as variáveis para não haver erros de concatenação ou de declaração de strings
    MensagemEnviar = "";
    //limpar o array para evitar confusões (apesar de não ser necessário)
    memset(buffByte_tx, 0, 128);
  }

  // Limpeza para não haver mais comparações desnecessárias e erros de concatenação ou de declaração de Strings
  ESPrx = "";
}

//Se ESP recebeu <EOT> da CNC quer dizer que a comunicação vai terminar e não precisa de fazer nada, apenas apagar o ESPrx (a mensagem)
//Se recebeu <DLE><1> então deve mandar para a CNC <EOT> para terminar a comunicação
void lsv2_FaseRepouso() {

  if (espEmissor) {
    // Enviar <EOT>
    Serial.write(4);
    espEmissor = false;

  }

  // Limpeza para não haver mais comparações desnecessárias e erros de concatenação ou de declaração de Strings
  ESPrx = "";
}

/*-------------------------------------------------------------

   Função para simular o Start Romoto

   ------------------------------------------------------------
*/


void lsv2_SimularTecla() {

  // Notas gerais:
  // As mensagens são contabilizadas pelo "SimularTecla_Counter"
  // Sempre que uma mensagem é interpretada incrementa-se o counter
  // A próxima mensagem só é interpretada sempre que "T_OK" é recebido pelo ESP
  // Sempre que se interpreta uma mensagem deve-se limpar a variável "telegramaRecebido" para não interpretar a mensagem seguinte antes do tempo
  // "SimularTecla_Counter++" é a mesma coisa que "SimularTecla_Counter = SimularTecla_Counter + 1"
  // MsgToByteArray Função para converter uma array de caracteres para um array em decimal para a função "lsv2_TransferenciaDeDados()" conseguir enviar a mensagem


  // Mensagem para o TNC dar permissões ao ESP
  if (SimularTecla_Counter == 0) {
    telegramaRecebido = "";
    MensagemEnviar = "A_LGMONITOR";
    MsgToByteArray(MensagemEnviar);
    SimularTecla_Counter++;
  }

  // Mensagem para o TNC desativar o teclado físico
  if (SimularTecla_Counter == 1 && telegramaRecebido == "T_OK" ) {
    telegramaRecebido = "";
    MensagemEnviar = "C_LK1";
    MsgToByteArray(MensagemEnviar);
    SimularTecla_Counter++;
  }

  // Mensagem para o TNC simular a tecla Start NC
  //"\Xnn", em C/C++ são denominadas de Escape sequences em que nn é o hexadecimal do caracter pretendido
  //"\X30", corresponde ao decimal 48 e ao caracter "0" (Tabela ASCII)
  // o "\xF0" é o caracter que codifica a tecla de acordo com o lsv2
  // Os código vêm-se no manual do LSV2
  if (SimularTecla_Counter == 2 && telegramaRecebido == "T_OK" ) {
    telegramaRecebido = "";
    String identificador = "C_EK1";
    String CaracterTecla = "\xF0";
    MensagemEnviar = identificador + CaracterTecla;
    MsgToByteArray(MensagemEnviar);
    SimularTecla_Counter++;
  }

  // Mensagem para o TNC retirar as permissões ao ESP
  // Ativa o teclado físico automáticamente
  if (SimularTecla_Counter == 3 && telegramaRecebido == "T_OK" ) {
    telegramaRecebido = "";
    MensagemEnviar = "A_LO";
    MsgToByteArray(MensagemEnviar);
    SimularTecla_Counter++;
  }

  //Acabou
  if (SimularTecla_Counter == 4 && telegramaRecebido == "T_OK" ) {
    telegramaRecebido = "";
    SimularTecla_Counter = 0;
    SimularTecla_Controlo = false;
    Nao_Comecar = 1;

  }

  // chegou o caracter <EOT>
  // O ESP tem que enviar <ENQ> obrigatóriamente para poder enviar a mensagem seguinte
  if (int(ESPrx[0]) == 4 && SimularTecla_Controlo == true) {
    delay(100);
    Serial.write(5);
  }

}



/*
    -------------------------------------------------------------

    Função para calcular o BCC do LSV2

    -------------------------------------------------------------

    Uma tipica mensagem em LSV2 é algo como <DLE><STX>telegrama<DLE><ETX><BCC>
    Para o cálculo do BCC os caracteres <DLE><STX> não contam
    O input da função é o telegrama a ser convertido sem contar com os caracteres especiais
    No incio da função convertemos tudo para letras maiusculas para evitar erros de comparação uma vez que é case sensitive
    O operador ^ (bitwise EXCLUSIVE OR) faz a comparação entre os respetivos bits, neste caso, de dois caracteres
    0  0  0  0  0  0  1  1    caracter 1
    0  0  0  0  0  1  0  1    caracter 2
    ----------------------
    0  0  0  0  0  1  1  0    ^ Result
    No loop faz-se a comparação do telegrama
    No final faz-se a comparação com o decimal 3 correspondente ao caracter <ETX>
    A função retorna o DECIMAL do caracter correspondente à tabela ASCII
*/
int BCC(String strToConvert) {

  strToConvert.toUpperCase();
  int comparingResult = 0;
  for (int i = 0; i < (strToConvert.length()) ; i++) {
    comparingResult = comparingResult ^ int(strToConvert[i]);
  }
  comparingResult = comparingResult ^ 3;

  //returna o decimal correspondete ao BCC
  return comparingResult;
}


/*-------------------------------------------------------------
   Função para converter uma array de caracteres para
   um array em decimal de acordo com a tabela ASCII
   para enviar as mensagens em lsv2
   ------------------------------------------------------------
*/

// int(x) passa o caracter x para o decimal correspondente (tabela ASCII)

void MsgToByteArray(String strToConvert) {
  for (int i = 0; i < MensagemEnviar.length(); i++) {
    buffByte_tx[i] = int(MensagemEnviar[i]);
  }
}
