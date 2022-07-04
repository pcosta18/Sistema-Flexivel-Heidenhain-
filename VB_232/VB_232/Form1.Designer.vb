<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.BtnOpenSerial = New System.Windows.Forms.Button()
        Me.BtnConfigPort = New System.Windows.Forms.Button()
        Me.txtBRshow = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtnNC = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pbHeid = New System.Windows.Forms.PictureBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbHeid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'SerialPort2
        '
        '
        'BtnOpenSerial
        '
        Me.BtnOpenSerial.Location = New System.Drawing.Point(367, 58)
        Me.BtnOpenSerial.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnOpenSerial.Name = "BtnOpenSerial"
        Me.BtnOpenSerial.Size = New System.Drawing.Size(136, 31)
        Me.BtnOpenSerial.TabIndex = 9
        Me.BtnOpenSerial.Text = "Abrir Porta"
        Me.BtnOpenSerial.UseVisualStyleBackColor = True
        '
        'BtnConfigPort
        '
        Me.BtnConfigPort.Location = New System.Drawing.Point(367, 21)
        Me.BtnConfigPort.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnConfigPort.Name = "BtnConfigPort"
        Me.BtnConfigPort.Size = New System.Drawing.Size(136, 31)
        Me.BtnConfigPort.TabIndex = 10
        Me.BtnConfigPort.Text = "Configurar Porta"
        Me.BtnConfigPort.UseVisualStyleBackColor = True
        '
        'txtBRshow
        '
        Me.txtBRshow.Location = New System.Drawing.Point(367, 114)
        Me.txtBRshow.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBRshow.Name = "txtBRshow"
        Me.txtBRshow.ReadOnly = True
        Me.txtBRshow.Size = New System.Drawing.Size(136, 22)
        Me.txtBRshow.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(397, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 16)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Baud Rate"
        '
        'BtnNC
        '
        Me.BtnNC.Location = New System.Drawing.Point(8, 21)
        Me.BtnNC.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnNC.Name = "BtnNC"
        Me.BtnNC.Size = New System.Drawing.Size(131, 31)
        Me.BtnNC.TabIndex = 20
        Me.BtnNC.Text = "Enviar Programa"
        Me.BtnNC.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.BtnNC)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(329, 68)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Controlo"
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelStatus.Location = New System.Drawing.Point(144, 36)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(73, 16)
        Me.LabelStatus.TabIndex = 25
        Me.LabelStatus.Text = "Status Text"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(172, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 16)
        Me.Label3.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(144, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 16)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Status"
        '
        'pbHeid
        '
        Me.pbHeid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbHeid.Image = Global.VB_232.My.Resources.Resources.Heid_
        Me.pbHeid.Location = New System.Drawing.Point(15, 95)
        Me.pbHeid.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pbHeid.Name = "pbHeid"
        Me.pbHeid.Size = New System.Drawing.Size(325, 235)
        Me.pbHeid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbHeid.TabIndex = 24
        Me.pbHeid.TabStop = False
        '
        'Timer2
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 345)
        Me.Controls.Add(Me.pbHeid)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBRshow)
        Me.Controls.Add(Me.BtnConfigPort)
        Me.Controls.Add(Me.BtnOpenSerial)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbHeid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents SerialPort2 As IO.Ports.SerialPort
    Friend WithEvents BtnOpenSerial As Button
    Friend WithEvents BtnConfigPort As Button
    Friend WithEvents txtBRshow As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents BtnNC As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents pbHeid As PictureBox
    Friend WithEvents LabelStatus As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer2 As Timer
End Class
