<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScannerControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Y = New System.Windows.Forms.RadioButton()
        Me.Label_Enable = New System.Windows.Forms.Label()
        Me.N = New System.Windows.Forms.RadioButton()
        Me.Label_Type = New System.Windows.Forms.Label()
        Me.Type = New System.Windows.Forms.ComboBox()
        Me.Label_Port = New System.Windows.Forms.Label()
        Me.Port = New System.Windows.Forms.TextBox()
        Me.Bandrate = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Parity = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Databits = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Stopbit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.IP = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.IPPort = New System.Windows.Forms.TextBox()
        Me.Label_IPPort = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Y
        '
        Me.Y.AutoSize = True
        Me.Y.Location = New System.Drawing.Point(44, 19)
        Me.Y.Name = "Y"
        Me.Y.Size = New System.Drawing.Size(47, 16)
        Me.Y.TabIndex = 0
        Me.Y.TabStop = True
        Me.Y.Text = "打开"
        Me.Y.UseVisualStyleBackColor = True
        '
        'Label_Enable
        '
        Me.Label_Enable.AutoSize = True
        Me.Label_Enable.Location = New System.Drawing.Point(3, 21)
        Me.Label_Enable.Name = "Label_Enable"
        Me.Label_Enable.Size = New System.Drawing.Size(35, 12)
        Me.Label_Enable.TabIndex = 1
        Me.Label_Enable.Text = "开启:"
        '
        'N
        '
        Me.N.AutoSize = True
        Me.N.Location = New System.Drawing.Point(118, 21)
        Me.N.Name = "N"
        Me.N.Size = New System.Drawing.Size(47, 16)
        Me.N.TabIndex = 2
        Me.N.TabStop = True
        Me.N.Text = "关闭"
        Me.N.UseVisualStyleBackColor = True
        '
        'Label_Type
        '
        Me.Label_Type.AutoSize = True
        Me.Label_Type.Location = New System.Drawing.Point(3, 62)
        Me.Label_Type.Name = "Label_Type"
        Me.Label_Type.Size = New System.Drawing.Size(35, 12)
        Me.Label_Type.TabIndex = 3
        Me.Label_Type.Text = "类型:"
        '
        'Type
        '
        Me.Type.FormattingEnabled = True
        Me.Type.Items.AddRange(New Object() {"LAN", "RS232"})
        Me.Type.Location = New System.Drawing.Point(44, 59)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(121, 20)
        Me.Type.TabIndex = 4
        '
        'Label_Port
        '
        Me.Label_Port.AutoSize = True
        Me.Label_Port.Location = New System.Drawing.Point(3, 97)
        Me.Label_Port.Name = "Label_Port"
        Me.Label_Port.Size = New System.Drawing.Size(35, 12)
        Me.Label_Port.TabIndex = 5
        Me.Label_Port.Text = "串口:"
        '
        'Port
        '
        Me.Port.Location = New System.Drawing.Point(44, 94)
        Me.Port.Name = "Port"
        Me.Port.Size = New System.Drawing.Size(121, 21)
        Me.Port.TabIndex = 6
        '
        'Bandrate
        '
        Me.Bandrate.Location = New System.Drawing.Point(44, 121)
        Me.Bandrate.Name = "Bandrate"
        Me.Bandrate.Size = New System.Drawing.Size(121, 21)
        Me.Bandrate.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-2, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "波特率:"
        '
        'Parity
        '
        Me.Parity.Location = New System.Drawing.Point(44, 148)
        Me.Parity.Name = "Parity"
        Me.Parity.Size = New System.Drawing.Size(121, 21)
        Me.Parity.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "校验:"
        '
        'Databits
        '
        Me.Databits.Location = New System.Drawing.Point(44, 177)
        Me.Databits.Name = "Databits"
        Me.Databits.Size = New System.Drawing.Size(121, 21)
        Me.Databits.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "长度:"
        '
        'Stopbit
        '
        Me.Stopbit.Location = New System.Drawing.Point(44, 206)
        Me.Stopbit.Name = "Stopbit"
        Me.Stopbit.Size = New System.Drawing.Size(121, 21)
        Me.Stopbit.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-2, 207)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 12)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "停止位:"
        '
        'IP
        '
        Me.IP.Location = New System.Drawing.Point(44, 233)
        Me.IP.Name = "IP"
        Me.IP.Size = New System.Drawing.Size(121, 21)
        Me.IP.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 236)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 12)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "IP:"
        '
        'IPPort
        '
        Me.IPPort.Location = New System.Drawing.Point(44, 260)
        Me.IPPort.Name = "IPPort"
        Me.IPPort.Size = New System.Drawing.Size(121, 21)
        Me.IPPort.TabIndex = 18
        '
        'Label_IPPort
        '
        Me.Label_IPPort.AutoSize = True
        Me.Label_IPPort.Location = New System.Drawing.Point(2, 263)
        Me.Label_IPPort.Name = "Label_IPPort"
        Me.Label_IPPort.Size = New System.Drawing.Size(35, 12)
        Me.Label_IPPort.TabIndex = 17
        Me.Label_IPPort.Text = "端口:"
        '
        'ScannerControl1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.IPPort)
        Me.Controls.Add(Me.Label_IPPort)
        Me.Controls.Add(Me.IP)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Stopbit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Databits)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Parity)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Bandrate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Port)
        Me.Controls.Add(Me.Label_Port)
        Me.Controls.Add(Me.Type)
        Me.Controls.Add(Me.Label_Type)
        Me.Controls.Add(Me.N)
        Me.Controls.Add(Me.Label_Enable)
        Me.Controls.Add(Me.Y)
        Me.Name = "ScannerControl1"
        Me.Size = New System.Drawing.Size(182, 295)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Y As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Enable As System.Windows.Forms.Label
    Friend WithEvents N As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Type As System.Windows.Forms.Label
    Friend WithEvents Type As System.Windows.Forms.ComboBox
    Friend WithEvents Label_Port As System.Windows.Forms.Label
    Friend WithEvents Port As System.Windows.Forms.TextBox
    Friend WithEvents Bandrate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Parity As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Databits As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Stopbit As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IP As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents IPPort As System.Windows.Forms.TextBox
    Friend WithEvents Label_IPPort As System.Windows.Forms.Label

End Class
