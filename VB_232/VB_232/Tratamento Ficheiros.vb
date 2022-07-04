' Importação da package necessária
Imports System.IO

Public Class Tratamento_Ficheiros
    ' -----------------------------------------------------------------------------------

    ' Inicialização do forms

    ' -----------------------------------------------------------------------------------
    Private Sub Tratamento_Ficheiros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Aplicar o filtro aos ficheiros que se podem abrir
        OpenFileDialog1.Filter = "txt files (*.txt)|*.txt|CN files (*.H)|*.H|ISO files (*.I)|*.I"
        OpenFileDialog1.FileName = ""
        'Reinicializar as variáveis de texto para que não tenha nada escrito
        TxtNome.Text = ""
        TxtConteudo.Text = ""
        TxtLocal.Text = ""
        'Titulo para a janela
        Me.Text = "Selecionar/Editar Ficheiro"

        ' Escolher a extensão do Ficheiro
        '.H, linguagem própria da Heid.
        '.I, linguagem ISO
        ComboBoxExtFich.Items.Clear()
        ComboBoxExtFich.Items.Add(".H")
        ComboBoxExtFich.Items.Add(".I")
        ComboBoxExtFich.SelectedIndex = 0

        'Atualização de variáveis de controlo
        FicheiroValido = False
        cbDataInput.Checked = False
    End Sub
    ' -----------------------------------------------------------------------------------

    ' Seleção de ficheiros

    ' -----------------------------------------------------------------------------------

    Dim FicheiroValido As Boolean
    Private Sub BtnOpenFile_Click(sender As Object, e As EventArgs) Handles BtnOpenFile.Click
        Dim ii As Integer
        ' Mostra ao utilizador a janela de diálogo
        ' Na janela de diálogo o utilizador pode retornar "Cancel"
        ' sem selecionar nenhum ficheiro.
        ' Se o ficheiro foi seleccionado a funçao retorna 1 caso contrário retorna 2
        ii = OpenFileDialog1.ShowDialog()
        ' Quando o utilizador sai da janela de diálogo,
        ' o nome e a localização do ficheiro seleccionado
        ' está memorizado na propriedade "FileName"

        If ii = 1 Then
            'O user clicou em Ok, logo temos caminhos válidos
            'Deve-se identifica-los na caixa de texto para inspeção visual do user
            'Nome do ficheiro:
            TxtNome.Text = OpenFileDialog1.SafeFileName
            'Tratar a string para retirar o .txt/.H/.I
            TxtNome.Text = Replace(TxtNome.Text, ".txt", "")
            TxtNome.Text = Replace(TxtNome.Text, ".H", "")
            TxtNome.Text = Replace(TxtNome.Text, ".I", "")

            'Obter a localização do ficheiro no PC
            TxtLocal.Text = OpenFileDialog1.FileName

            'Ler o ficheiro para guardar a mensagem para enviar ao TNC e para que o user
            'possa reparar se selecionou o ficheiro correto
            Dim ficheiroler As StreamReader
            'localalização e nome do ficheiro que vai ler
            ficheiroler = New StreamReader(TxtLocal.Text)
            'Leitura do ficheiro para a caixa de texto
            TxtConteudo.Text = ficheiroler.ReadToEnd()
            ' No final é preciso sempre "fechar"
            ficheiroler.Close()
            FicheiroValido = True
        End If

    End Sub
    ' -----------------------------------------------------------------------------------

    ' Controlo da entrada manual de dadod

    ' -----------------------------------------------------------------------------------

    Private Sub cbDataInput_CheckedChanged(sender As Object, e As EventArgs) Handles cbDataInput.CheckedChanged

        ' Se tem a alteração de dados manual ativa/desativa
        ' Ativar/desativar a edicação das respetivas caixas de textp
        If cbDataInput.Checked = True Then
            TxtNome.ReadOnly = False
            TxtConteudo.ReadOnly = False
        Else
            TxtNome.ReadOnly = True
            TxtConteudo.ReadOnly = True
        End If

    End Sub
    ' -----------------------------------------------------------------------------------

    ' Processamento dos dados quando user pressiona "OK"

    ' -----------------------------------------------------------------------------------
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        'Não deixar enviar um ficheiro sem nome ou conteúdo
        If TxtConteudo.Text.Length = 0 Or TxtNome.Text.Length = 0 Then
            FicheiroValido = False
            MsgBox("Conteudo do texto ou nome está vazio")
            TxtConteudo.Text = ""
            TxtLocal.Text = ""
            TxtNome.Text = ""
        Else
            FicheiroValido = True
        End If

        ' Não deixar enviar um ficheiro com mais de 78 caracteres
        ' (o máximo é 80, mas tem que se contabilizar axtensão do ficheiro .H/.I)
        If TxtNome.Text.Length > 78 Then
            MsgBox("Nome do Ficheiro muito grande! Deve ter apenas 78 caracteres")
            TxtConteudo.Text = ""
            TxtLocal.Text = ""
            TxtNome.Text = ""
            FicheiroValido = False
        End If


        If FicheiroValido = True Then
            ' O ficheiro é válido e portanto deve-se atualizar as variáveis globais
            NomeFicheiro = TxtNome.Text & ComboBoxExtFich.Text

            'À luz do protocolo não se pode enviar mensagens com line break caracteres mas sim com nullchars a substitui-los
            Dim FicheiroNcTratado As String = Replace(TxtConteudo.Text, vbCrLf, vbNullChar)

            'Fazer o Tratamento da mensagem para que o teelgrama tenha no máximo 1020 caracteres
            ' Devido à constante que se tem que respeitar (MAXLLENGTH-4)
            Dim TamanhoMensagem As Integer = FicheiroNcTratado.Length
            Dim NumeroMensagens As Integer = Math.Ceiling(TamanhoMensagem / 1020)

            'Colocar as mensagens para um array para saber a sequência
            Dim Position As Integer = 1
            Dim textoSeparado(0 To NumeroMensagens - 1) As String
            For i As Integer = 0 To NumeroMensagens - 1
                textoSeparado(i) = Mid(FicheiroNcTratado, Position, 1020)
                Position += (1020)
            Next

            MensagemFicheiro = textoSeparado
            NewFile = True
            Me.Close()
        Else
            NewFile = False
            Me.Close()
        End If
    End Sub
    ' -----------------------------------------------------------------------------------

    ' Processamento dos dados quando user pressiona "cancelar"

    ' -----------------------------------------------------------------------------------

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        ' O user clicou em "Cancel" e portanto não não há um ficheiro para ser enviado para o TNC
        NewFile = False
        Me.Close()
    End Sub

End Class