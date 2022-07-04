<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tratamento_Ficheiros
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tratamento_Ficheiros))
        Me.BtnOpenFile = New System.Windows.Forms.Button()
        Me.TxtLocal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtNome = New System.Windows.Forms.TextBox()
        Me.TxtConteudo = New System.Windows.Forms.TextBox()
        Me.LabelFileContent = New System.Windows.Forms.Label()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ComboBoxExtFich = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbDataInput = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'BtnOpenFile
        '
        Me.BtnOpenFile.Location = New System.Drawing.Point(15, 36)
        Me.BtnOpenFile.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnOpenFile.Name = "BtnOpenFile"
        Me.BtnOpenFile.Size = New System.Drawing.Size(108, 32)
        Me.BtnOpenFile.TabIndex = 0
        Me.BtnOpenFile.Text = "Abrir Ficheiro"
        Me.BtnOpenFile.UseVisualStyleBackColor = True
        '
        'TxtLocal
        '
        Me.TxtLocal.Location = New System.Drawing.Point(235, 46)
        Me.TxtLocal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtLocal.Name = "TxtLocal"
        Me.TxtLocal.ReadOnly = True
        Me.TxtLocal.Size = New System.Drawing.Size(541, 22)
        Me.TxtLocal.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(235, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Localização do ficheiro"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(235, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Nome do Ficheiro"
        '
        'TxtNome
        '
        Me.TxtNome.Location = New System.Drawing.Point(235, 95)
        Me.TxtNome.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtNome.Name = "TxtNome"
        Me.TxtNome.ReadOnly = True
        Me.TxtNome.Size = New System.Drawing.Size(541, 22)
        Me.TxtNome.TabIndex = 3
        '
        'TxtConteudo
        '
        Me.TxtConteudo.Location = New System.Drawing.Point(15, 177)
        Me.TxtConteudo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TxtConteudo.Multiline = True
        Me.TxtConteudo.Name = "TxtConteudo"
        Me.TxtConteudo.ReadOnly = True
        Me.TxtConteudo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtConteudo.Size = New System.Drawing.Size(761, 221)
        Me.TxtConteudo.TabIndex = 5
        '
        'LabelFileContent
        '
        Me.LabelFileContent.AutoSize = True
        Me.LabelFileContent.Location = New System.Drawing.Point(12, 158)
        Me.LabelFileContent.Name = "LabelFileContent"
        Me.LabelFileContent.Size = New System.Drawing.Size(130, 16)
        Me.LabelFileContent.TabIndex = 6
        Me.LabelFileContent.Text = "Conteúdo do ficheiro"
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(239, 409)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(95, 34)
        Me.BtnCancel.TabIndex = 7
        Me.BtnCancel.Text = "Cancelar"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(431, 409)
        Me.BtnOK.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(115, 34)
        Me.BtnOK.TabIndex = 8
        Me.BtnOK.Text = "Enviar  Ficheiro"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ComboBoxExtFich
        '
        Me.ComboBoxExtFich.FormattingEnabled = True
        Me.ComboBoxExtFich.Location = New System.Drawing.Point(239, 146)
        Me.ComboBoxExtFich.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxExtFich.Name = "ComboBoxExtFich"
        Me.ComboBoxExtFich.Size = New System.Drawing.Size(77, 24)
        Me.ComboBoxExtFich.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(235, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Extensão do Ficheiro"
        '
        'cbDataInput
        '
        Me.cbDataInput.AutoSize = True
        Me.cbDataInput.Location = New System.Drawing.Point(15, 81)
        Me.cbDataInput.Margin = New System.Windows.Forms.Padding(4)
        Me.cbDataInput.Name = "cbDataInput"
        Me.cbDataInput.Size = New System.Drawing.Size(137, 36)
        Me.cbDataInput.TabIndex = 11
        Me.cbDataInput.Text = "Entrada de dados" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "manual"
        Me.cbDataInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cbDataInput.UseVisualStyleBackColor = True
        '
        'Tratamento_Ficheiros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.cbDataInput)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxExtFich)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.LabelFileContent)
        Me.Controls.Add(Me.TxtConteudo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtNome)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtLocal)
        Me.Controls.Add(Me.BtnOpenFile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Tratamento_Ficheiros"
        Me.Text = "Tratamento_Ficheiros"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnOpenFile As Button
    Friend WithEvents TxtLocal As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNome As TextBox
    Friend WithEvents TxtConteudo As TextBox
    Friend WithEvents LabelFileContent As Label
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ComboBoxExtFich As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbDataInput As CheckBox
End Class
