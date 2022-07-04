'Importação das diferentes packages
Imports System.IO.Ports                 ' Leitura das portas série
Imports MySql.Data.MySqlClient          ' Base de dados

Public Class Form1
    ' inicialização das variáveis para a base de dados
    Dim cn As New MySqlConnection
    Dim cmd As New MySqlCommand
    Dim data_reader As MySqlDataReader

    'Varáiveis de controlo para o Envio de ficheiro
    Dim VarControloNC As Boolean = False
    Dim CounterDeMensagens As Integer
    Dim EnviarFicheiro_Counter As Integer
    Dim lsv2_EnvioMensagem_Completo As Boolean = False ' Para atualização da label

    'Variáveis de controlo para implementação do Protocolo LSV2
    Dim Enviar As Boolean
    Dim Receber As Boolean
    Dim MensagemRecebida As String
    Dim MensagemAEnviar As String

    'Variáveis de controlo da leitura de dados que chega pela porta série
    Dim rx As String
    Dim rx2 As String
    'Delay para o visual basic ler os dados corretamente
    Dim sw As New Stopwatch
    ' -----------------------------------------------------------------------------------

    ' Inicialização do programa

    ' -----------------------------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Título do forms
        Me.Text = "Comunicação LSV-2"

        'Enquanto não houve uma primeira configuração não se pode abrir a porta
        BtnOpenSerial.Enabled = False
        ' Enquanto não abrir a porta não pode enviar ficheiros
        BtnNC.Enabled = False

        ' Inicializar a configuração da porta série 
        With SerialPort2
            .Parity = Parity.None
            .StopBits = IO.Ports.StopBits.One
            .DataBits = 8
            .Handshake = IO.Ports.Handshake.RequestToSend
            .Encoding = System.Text.Encoding.UTF8
            .DtrEnable = True
            .RtsEnable = True
        End With

        'Atualizar o botão para melhor identificar a porta que está atualmente selecionada
        BtnOpenSerial.Text = BtnOpenSerial.Text & " " & SerialPort2.PortName
        txtBRshow.Text = SerialPort2.BaudRate

        ' Inicialização da base de dados "alunos"
        cn.ConnectionString = "Server=localhost; User Id=root; Database=alunos“

        ' Enviar uma mensgem ao inicializar o programa de forma ao user perceber se existiu alguma fallha ao conectar à base de dados
        Try
            If cn.State = ConnectionState.Closed Then
                cn.Open()
                MsgBox("Ligação Correcta à Base de Dados alunos...")
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox("Ligação Incorrecta à Base de Dados alunos...")
        End Try

        cmd.Connection = cn

        ' No inicio a label não tem nada porque também não se enviou nada
        LabelStatus.Text = ""
        LabelStatus.ForeColor = Color.Black

    End Sub

    ' -----------------------------------------------------------------------------------

    ' Configuração da Porta

    ' -----------------------------------------------------------------------------------
    Private Sub BtnConfigPort_Click(sender As Object, e As EventArgs) Handles BtnConfigPort.Click

        ' Abrir a interface "forms2" que corresponde à configuração da porta série
        Form2.StartPosition = FormStartPosition.CenterParent
        Form2.ShowDialog()

        ' Caso haja uma nova configuração (o user clicou em OK), a configuração da porta atualiza os respetivos parâmetros
        If NewConfigAvailable Then
            SerialPort2.PortName = Port
            BtnOpenSerial.Text = "Open Port " & SerialPort2.PortName
            SerialPort2.BaudRate = Baud_Rate
            txtBRshow.Text = SerialPort2.BaudRate
            NewConfigAvailable = False ' já atualizamos os parametros portanto é preciso voltar a colocar em falso 
            'habilitar novamente o botão para abrir a porta
            BtnOpenSerial.Enabled = True
        End If

    End Sub
    ' -----------------------------------------------------------------------------------

    ' Abertura/Fecho da porta serial

    ' -----------------------------------------------------------------------------------
    Private Sub BtnOpenSerial_Click(sender As Object, e As EventArgs) Handles BtnOpenSerial.Click

        ' Se está aberta
        If SerialPort2.IsOpen Then

            'Fecha
            SerialPort2.Close()
            BtnNC.Enabled = False
            ' atulizar o texto e a cor do botão 
            BtnOpenSerial.Text = "Open Port " & SerialPort2.PortName
            BtnOpenSerial.BackColor = Color.LightGray
            ' Enviar mensagem ao user a dizer que a porta foi fechada com sucesso
            MessageBox.Show("A porta " + SerialPort2.PortName + " foi Fechada", "Status",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else ' Se está fechada, abre

            SerialPort2.Open()
            BtnNC.Enabled = True
            ' atulizar o texto e a cor do botão 
            BtnOpenSerial.Text = "Close Port " & SerialPort2.PortName ' atulizar o texto do botão
            BtnOpenSerial.BackColor = Color.Red ' atualizar a cor do botão
            ' Enviar mensagem ao user a dizer que a porta foi aberta com sucesso
            MessageBox.Show("A porta foi aberta", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
    '---------------------------------------------------------------------
    '
    'Botão Para Enviar Ficheiro
    '
    '---------------------------------------------------------------------
    Private Sub BtnNC_Click(sender As Object, e As EventArgs) Handles BtnNC.Click

        ' Abrir a interface "Tratamento Ficheiro" que corresponde à que o user tem que selecionar o ficheiro que pretende enviar
        Tratamento_Ficheiros.StartPosition = FormStartPosition.CenterParent
        Tratamento_Ficheiros.ShowDialog()

        ' Caso o user clique em "OK" é porque há um novo ficheiro a ser enviado e portanto a tranferência de dados começa
        If NewFile = True Then

            'Atualizar o status
            LabelStatus.Text = "A enviar"
            LabelStatus.ForeColor = Color.DarkOrange

            'Começar um timer para aletar caso a mensagem não tinha sido enviada ao fim de 20 segundos
            'Normalmente uma mensagem é enviada em torno de 1 segundo
            Timer2.Interval = 20000
            Timer2.Start()

            'Timer para atualizar a label para indicar que o programa já foi enviado
            Timer1.Interval = 100
            Timer1.Start()

            'Estabelecer comunicação
            '5 decimal, &H_C9 201 em hexadecimal e &B1100_1001 em binário
            Dim tx() As Byte = {5}
            SerialPort2.Write(tx, 0, 1)

            'inserir dados na base de dados para que se saiba que um novo programa
            ' foi enviado do emissor "PC" 
            Dim Emissor As String = "PC"
            Dim MensagemBD As String = "Envio de programa " & NomeFicheiro
            cmd.CommandText = "Insert lsv2(Emissor,Mensagem) Values('" & Emissor & "','" & MensagemBD & "')"
            cmd.ExecuteNonQuery()

            ' Já enviamos a mensagem, é necessário voltar a colocar a variável de novo ficheiro a falso
            NewFile = False

            ' Atualizar a variáveis de controlo para implementação da função
            EnviarFicheiro_Counter = 0
            VarControloNC = True
            CounterDeMensagens = 0
            'Variáveis de controlo para implementação do Protocolo LSV2
            Enviar = False
            Receber = False
            MensagemRecebida = ""
            MensagemAEnviar = ""
            rx2 = ""

        End If
    End Sub
    '---------------------------------------------------------------------
    '
    'Ler mensagems e comparação das mesmas
    '
    '---------------------------------------------------------------------
    Private Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort2.DataReceived
        'Delay
        ' 700 milisegundos, é o limite para que os timers do protocolo não "disparem"
        sw.Start()
        Do While sw.ElapsedMilliseconds < 700
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()

        'Variável para ler a mensagem recebida
        rx = ""
        'Ver quantos bytes há para ler
        Dim BytesToRead As Integer = SerialPort2.BytesToRead()
        ' Inicialização do array da mensagem
        Dim Mensagem(0 To (BytesToRead - 1)) As Byte
        ' Leitura da porta série
        SerialPort2.Read(Mensagem, 0, BytesToRead)
        ' Conversão de bytes para Caracter (para que seja vísivel em caixas de texto, por exmeplo)
        ' tratamento de dados e debug fica mais fácil
        For i As Integer = 0 To BytesToRead - 1
            rx &= Chr(Mensagem(i))
        Next
        ' Acrescentar à variável que estão a agrupar as mensagens que vão chegando até haver uma comparação válida
        rx2 &= rx

        ' Variável para saber quais mensagens enviar para o TNC
        If VarControloNC = True Then
            'guarda a mensagem para quando a fase de tranferência de dados for chamada
            Enviar_Ficheiro_NC()
        End If

        ' Se foi o TNC a enviar-te mensagem:
        'É preciso acautelar o BCC, na fase te transferência de dados, que também pode assumir o valor 5
        'dái a utlização do indexOf
        If rx2.IndexOf(Chr(5)) = 0 Then ' recebi o ENQ
            'Só para controlo
            Receber = True
            Debug.WriteLine("Recebi caracter ENQ")
            Fase_De_Inquerito()

            'É preciso acautelar o BCC, na fase te transferência de dados, que também pode assumir o valor 4
            'dái a utlização do indexOf
        ElseIf rx2.IndexOf(Chr(4)) = 0 Then ' recebi o EOT
            Receber = True
            'Só para controlo
            Debug.WriteLine("Recebi caracter EOT")
            Fase_De_Repouso()
        End If

        ' Se foste tu a mandar mensagem:

        If rx2.Contains(Chr(16) & Chr(48)) Then ' DLE 0, podes passar para a fase de transferência
            Enviar = True
            'Só para controlo
            Debug.WriteLine("Recebi caracter DLE0")
            ' Vamos passar a esta função já com a mensagem que queremos enviar guardada
            Fase_TransferenciaDeDados()

        ElseIf rx2.Contains(Chr(16) & Chr(49)) Then 'DLE 1, recebi sem erros, podes passar para a fase de repouso
            Enviar = True
            'Só para controlo
            Debug.WriteLine("Recebi caracter DLE1")
            Fase_De_Repouso()

        End If

        ' independentemente de ter sido a enviar ou a receber se começar por DLE,STX e acabar com
        ' DLE,ETX,BCC é transferencia de dados
        'assumir sempre que o BCC está bem escrito
        '"*" é o equivalente a dizer que pode ser um conjunto qualquer de caracteres
        '"?" é o equivalente a dizer que pode ser um caracter qualquer
        '<16><2>T_OK<16><3><3> iria funcionar, <6><2>T_OK<16><3><3> já não
        If rx2 Like Chr(16) & Chr(2) & "*" & Chr(16) & Chr(3) & "?" Then
            Receber = True
            Debug.WriteLine("Recebi a mensagem:" & rx2)
            Fase_TransferenciaDeDados()

        End If

    End Sub
    '---------------------------------------------------------------------
    '
    'Implementação do LSV2
    '
    '---------------------------------------------------------------------
    Public Sub Fase_De_Inquerito()

        If Receber = True Then
            ' DLE 0, Pode enviar o telegrama
            Dim tx() As Byte = {16, 48}
            SerialPort2.Write(tx, 0, tx.Length)
            Receber = False
            'Só para controlo
            Debug.WriteLine("Enviei o caracter DLE 0")
        End If
        rx2 = ""

    End Sub
    Public Sub Fase_TransferenciaDeDados()

        If Receber = True Then

            'Começar na posição 2 para nao contar com DEL,STX e retirar o DEL,ETX, BCC
            ',Isto é,
            ' 3 -> DLE,ETX,BCC / 2 -> DLE,STX
            MensagemRecebida = rx.Substring(2, rx.Length - 3 - 2)
            ' responder que recebeste sem erros
            Dim mensagem() As Byte = {16, 49} ' DLE 1
            SerialPort2.Write(mensagem, 0, mensagem.Length)
            Receber = False
            'Só para controlo
            Debug.WriteLine("Enviei os caracteres DLE 1")

        End If

        If Enviar = True Then

            ' a mensagem a enviar
            Dim testString1 As String = Chr(16) & Chr(2) & MensagemAEnviar & Chr(16) & Chr(3) & Chr(BCC(MensagemAEnviar))

            'Pode não seja necessário enviar em Bytes, mas para ter a certeza
            Dim charArray() As Char = testString1.ToCharArray

            'Enviar a mensagem
            'Cria um arry com o tamanho da mensagem (não esquecer que o index começa em 0)
            'colocar em cada posição do array o byte correspondete ao caracter que se quer enviar
            'enviar a mensagem
            Dim txlenght As Integer = charArray.Length
            Dim ByteArrayMensagem(0 To txlenght - 1) As Byte
            For i As Integer = 0 To txlenght - 1
                ByteArrayMensagem(i) = Asc(charArray(i))
            Next
            SerialPort2.Write(ByteArrayMensagem, 0, txlenght)

            MensagemAEnviar = ""
            Enviar = False
            'Só para controlo
            Debug.WriteLine("Enviei os caracteres " & testString1)

        End If

        rx2 = ""
    End Sub
    Public Sub Fase_De_Repouso()

        If Receber = True Then
            Receber = False
            'Acabou o que tinha de mandar
            ' não precisa de fazer nada
        End If

        If Enviar = True Then
            ' <EOT>
            SerialPort2.Write(Chr(4))
            'Acabou o que tinha de mandar
            Enviar = False

            'Só para controlo
            Debug.WriteLine("Enviei os caracteres EOT")
        End If
        rx2 = ""
    End Sub
    '---------------------------------------------------------------------
    '
    'Timer para indicar o envio da mensagem bem sucedido
    '
    '---------------------------------------------------------------------
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If lsv2_EnvioMensagem_Completo Then
            'atualização da label
            LabelStatus.Text = "Enviado"
            LabelStatus.ForeColor = Color.Green
            'atualização das variáveis de controlo
            lsv2_EnvioMensagem_Completo = False
            ' Paragem dos timers para não haver mais eventos
            Timer1.Stop()
            Timer2.Stop()
        End If

    End Sub
    '---------------------------------------------------------------------
    '
    'Timer caso não se consiga enviar a mensagem
    '
    '---------------------------------------------------------------------
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ' Paragem dos timers para não haver mais eventos
        Timer2.Stop()
        Timer1.Stop()
        'atualização da label
        LabelStatus.Text = "Tempo excedido"
        LabelStatus.ForeColor = Color.Red
        'atualização das variáveis de controlo
        VarControloNC = False
        EnviarFicheiro_Counter = 0
        'mensagem para alertar o user
        MsgBox("Tempo excedido para envio de mensagem, tenta outra vez")
    End Sub

    '---------------------------------------------------------------------
    '
    'Função para que o Visual Basic saiba quais é que são as mensagem para enviar
    '
    '---------------------------------------------------------------------
    Public Sub Enviar_Ficheiro_NC()

        ' Notas gerais
        ' As mensagens são contabilizadas pelo "EnviarFicheiro_Counter"
        ' Sempre que uma mensagem é interpretada incrementa-se o counter
        ' A próxima mensagem só é interpretada sempre que "T_OK" é recebido pelo PC
        ' Sempre que se interpreta uma mensagem deve-se limpar a variável "MensagemRecebida" para não interpretar a mensagem seguinte antes do tempo

        ' Ocorreu um erro na comunicação, acabar com tudo
        If MensagemRecebida Like "T_ER" & "*" Then
            VarControloNC = False
            EnviarFicheiro_Counter = 0
            MessageBox.Show("Envio de ficheiro", "Erro no envio do ficheiro!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LabelStatus.ForeColor = Color.Red
            MensagemRecebida = ""
        End If

        'Mensagem para o PC obter permissões para tratamento de ficheiros
        If EnviarFicheiro_Counter = 0 Then
            MensagemAEnviar = "A_LGFILE"
            EnviarFicheiro_Counter += 1
        End If

        'Mensagem que contem o nome do ficheiro
        'No final da mensagem tem que ter um caracter nulo, daí o "Constants.vbNullChar"
        If EnviarFicheiro_Counter = 1 And MensagemRecebida = "T_OK" Then
            MensagemAEnviar = "C_FL" & NomeFicheiro & Constants.vbNullChar
            EnviarFicheiro_Counter += 1
            MensagemRecebida = ""
        End If

        'Mensagem para Envio do conteúdo do ficheiro
        'Como o máximo de caracteres que a mensagem pode ter é de 1024, ela dá loop nas várias mensagens que tem que enviar
        'depois de uma resposta positiva T_OK
        'Ver relatório para perceber esta parte
        If EnviarFicheiro_Counter = 2 And MensagemRecebida = "T_OK" Then
            If CounterDeMensagens <= (MensagemFicheiro.Length - 1) Then
                MensagemAEnviar = "S_FL" & MensagemFicheiro(CounterDeMensagens)
                CounterDeMensagens += 1
            Else
                CounterDeMensagens = 0
                EnviarFicheiro_Counter += 1
                MensagemRecebida = ""
            End If
        End If

        'Indicar que acabou de mandar ficheiro
        If EnviarFicheiro_Counter = 3 And MensagemRecebida = "T_OK" Then
            ' Depois do T_FD não é suposto o TNC responder outra vez, pelo que é preciso voltar a enviar 
            MensagemAEnviar = "T_FD"
            EnviarFicheiro_Counter += 1
            MensagemRecebida = ""
            lsv2_EnvioMensagem_Completo = True
        End If

        ' Depois do T_FD não é suposto o TNC responder outra vez, pelo que é preciso voltar a enviar 
        ' o caracter <ENQ> (estabelecer uma nova comunicação) para realizar o logout 
        If (EnviarFicheiro_Counter = 4) And rx2.Contains(Chr(16) & Chr(49)) Then
            Enviar = True
            Fase_De_Repouso()

            'Delay
            sw.Start()
            Do While sw.ElapsedMilliseconds < 3000 ' 3 segundos
                ' Permite que o UI se mantanha responsivo (pressionar botões e assim...)
                Application.DoEvents()
            Loop
            sw.Stop()

            Dim tx() As Byte = {5}
            ' Envio do caracter <ENQ> para fazer o logout
            SerialPort2.Write(tx, 0, 1)
            MensagemAEnviar = "A_LO"
            EnviarFicheiro_Counter += 1
            'Só para controlo
            Debug.WriteLine("Enviei os caracteres ENQ para iniciar o Logout")
            Debug.WriteLine("O ficheiro acabou")

            VarControloNC = False
            EnviarFicheiro_Counter = 0

        End If

        ' Verificar se ainda há mensagem a ser enviadas
        'Se houver, voltar a enviar <ENQ> para estabelecer nova comunicação
        If (VarControloNC = True) And (rx2.IndexOf(Chr(4)) = 0) Then
            rx2 = "" ' para não haver mais verificações dentro da leitura
            Dim tx() As Byte = {5}
            ' Envio do caracter 5 para nova mensagem
            SerialPort2.Write(tx, 0, 1)
            'Só para controlo
            Debug.WriteLine("Enviei o caracter ENQ")
        End If

    End Sub
End Class
