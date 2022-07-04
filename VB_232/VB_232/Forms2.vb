Public Class Form2
    ' -----------------------------------------------------------------------------------

    ' Inicialização do forms

    ' -----------------------------------------------------------------------------------
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NewConfigAvailable = False
        Me.Text = "Configuração LSV2"

        ' Vê quais são as portas disponíveis 
        Dim available_ports() As String = IO.Ports.SerialPort.GetPortNames

        'Apagar os items da combobox que possam existir de configurações anterioes
        ComboBoxPort.Items.Clear()
        'Adicionar os novos itmes à combobox
        For i As Integer = 0 To (available_ports.Length - 1)
            ComboBoxPort.Items.Add(available_ports(i))
        Next
        ComboBoxPort.SelectedIndex = 0 ' porta que está selecionada por defeito

        'Apagar os items da combobox que possam existir de configurações anterioes
        ComboBoxBaudRate.Items.Clear()
        'Adicioanar as diferents BaudRates
        ComboBoxBaudRate.Items.Add("2400")
        ComboBoxBaudRate.Items.Add("4000")
        ComboBoxBaudRate.Items.Add("9600")
        ComboBoxBaudRate.Items.Add("115200")
        ComboBoxBaudRate.SelectedIndex = 3 ' Baud Rate que está selecionada por defeito

    End Sub
    ' -----------------------------------------------------------------------------------

    ' Processamento dos dados quando user pressiona "cancelar"

    ' -----------------------------------------------------------------------------------
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        ' User clicou em "Cancel" logo não há uma nova configuração
        NewConfigAvailable = False
        Me.Close()
    End Sub
    ' -----------------------------------------------------------------------------------

    ' Processamento dos dados quando user pressiona "OK"

    ' -----------------------------------------------------------------------------------
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        ' User clicou em "Ok" logo há uma nova configuração e é necessário atualizar as variáveis globais
        ' para poderem ser utilizadas em outros forms
        Port = ComboBoxPort.Text
        Baud_Rate = ComboBoxBaudRate.Text
        NewConfigAvailable = True
        Me.Close()

    End Sub
End Class