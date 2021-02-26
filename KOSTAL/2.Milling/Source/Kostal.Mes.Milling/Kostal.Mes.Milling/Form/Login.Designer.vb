<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.GroupBox_Login = New System.Windows.Forms.GroupBox()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Button_Login = New System.Windows.Forms.Button()
        Me.TextBox_Password = New System.Windows.Forms.TextBox()
        Me.TextBox_Name = New System.Windows.Forms.TextBox()
        Me.Label_Password = New System.Windows.Forms.Label()
        Me.Label_Name = New System.Windows.Forms.Label()
        Me.GroupBox_Login.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Login
        '
        Me.GroupBox_Login.Controls.Add(Me.Button_Reset)
        Me.GroupBox_Login.Controls.Add(Me.Button_Login)
        Me.GroupBox_Login.Controls.Add(Me.TextBox_Password)
        Me.GroupBox_Login.Controls.Add(Me.TextBox_Name)
        Me.GroupBox_Login.Controls.Add(Me.Label_Password)
        Me.GroupBox_Login.Controls.Add(Me.Label_Name)
        Me.GroupBox_Login.Location = New System.Drawing.Point(10, 12)
        Me.GroupBox_Login.Name = "GroupBox_Login"
        Me.GroupBox_Login.Size = New System.Drawing.Size(251, 167)
        Me.GroupBox_Login.TabIndex = 0
        Me.GroupBox_Login.TabStop = False
        Me.GroupBox_Login.Text = "登陆"
        '
        'Button_Reset
        '
        Me.Button_Reset.Location = New System.Drawing.Point(152, 108)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(75, 42)
        Me.Button_Reset.TabIndex = 5
        Me.Button_Reset.Text = "复位"
        Me.Button_Reset.UseVisualStyleBackColor = True
        '
        'Button_Login
        '
        Me.Button_Login.Location = New System.Drawing.Point(43, 108)
        Me.Button_Login.Name = "Button_Login"
        Me.Button_Login.Size = New System.Drawing.Size(75, 42)
        Me.Button_Login.TabIndex = 4
        Me.Button_Login.Text = "登陆"
        Me.Button_Login.UseVisualStyleBackColor = True
        '
        'TextBox_Password
        '
        Me.TextBox_Password.Location = New System.Drawing.Point(91, 69)
        Me.TextBox_Password.Name = "TextBox_Password"
        Me.TextBox_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox_Password.Size = New System.Drawing.Size(136, 21)
        Me.TextBox_Password.TabIndex = 3
        '
        'TextBox_Name
        '
        Me.TextBox_Name.Location = New System.Drawing.Point(91, 25)
        Me.TextBox_Name.Name = "TextBox_Name"
        Me.TextBox_Name.Size = New System.Drawing.Size(136, 21)
        Me.TextBox_Name.TabIndex = 2
        '
        'Label_Password
        '
        Me.Label_Password.AutoSize = True
        Me.Label_Password.Location = New System.Drawing.Point(41, 72)
        Me.Label_Password.Name = "Label_Password"
        Me.Label_Password.Size = New System.Drawing.Size(35, 12)
        Me.Label_Password.TabIndex = 1
        Me.Label_Password.Text = "密码:"
        '
        'Label_Name
        '
        Me.Label_Name.AutoSize = True
        Me.Label_Name.Location = New System.Drawing.Point(29, 28)
        Me.Label_Name.Name = "Label_Name"
        Me.Label_Name.Size = New System.Drawing.Size(47, 12)
        Me.Label_Name.TabIndex = 0
        Me.Label_Name.Text = "用户名:"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(272, 186)
        Me.Controls.Add(Me.GroupBox_Login)
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kostal.Mes.Milling.Config"
        Me.GroupBox_Login.ResumeLayout(False)
        Me.GroupBox_Login.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox_Login As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Reset As System.Windows.Forms.Button
    Friend WithEvents Button_Login As System.Windows.Forms.Button
    Friend WithEvents TextBox_Password As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Name As System.Windows.Forms.TextBox
    Friend WithEvents Label_Password As System.Windows.Forms.Label
    Friend WithEvents Label_Name As System.Windows.Forms.Label

End Class
