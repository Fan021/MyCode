<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PassWordForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.NameNewPassword = New System.Windows.Forms.Label()
        Me.TextNewPassword = New System.Windows.Forms.TextBox()
        Me.NameOldPassword = New System.Windows.Forms.Label()
        Me.NameConfirmNewPassword = New System.Windows.Forms.Label()
        Me.TextConfirmNewPassword = New System.Windows.Forms.TextBox()
        Me.PasswordKeyPad = New KeyPad()
        Me.SuspendLayout()
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(4, 27)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(262, 21)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'NameNewPassword
        '
        Me.NameNewPassword.Location = New System.Drawing.Point(4, 294)
        Me.NameNewPassword.Name = "NameNewPassword"
        Me.NameNewPassword.Size = New System.Drawing.Size(260, 17)
        Me.NameNewPassword.TabIndex = 13
        Me.NameNewPassword.Text = "New Password"
        Me.NameNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextNewPassword
        '
        Me.TextNewPassword.Location = New System.Drawing.Point(4, 311)
        Me.TextNewPassword.Name = "TextNewPassword"
        Me.TextNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextNewPassword.Size = New System.Drawing.Size(262, 21)
        Me.TextNewPassword.TabIndex = 12
        Me.TextNewPassword.UseSystemPasswordChar = True
        '
        'NameOldPassword
        '
        Me.NameOldPassword.Location = New System.Drawing.Point(6, 8)
        Me.NameOldPassword.Name = "NameOldPassword"
        Me.NameOldPassword.Size = New System.Drawing.Size(260, 17)
        Me.NameOldPassword.TabIndex = 11
        Me.NameOldPassword.Text = "Old Password"
        Me.NameOldPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NameConfirmNewPassword
        '
        Me.NameConfirmNewPassword.Location = New System.Drawing.Point(4, 345)
        Me.NameConfirmNewPassword.Name = "NameConfirmNewPassword"
        Me.NameConfirmNewPassword.Size = New System.Drawing.Size(260, 17)
        Me.NameConfirmNewPassword.TabIndex = 9
        Me.NameConfirmNewPassword.Text = "Confirm New Password"
        Me.NameConfirmNewPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextConfirmNewPassword
        '
        Me.TextConfirmNewPassword.Location = New System.Drawing.Point(4, 362)
        Me.TextConfirmNewPassword.Name = "TextConfirmNewPassword"
        Me.TextConfirmNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextConfirmNewPassword.Size = New System.Drawing.Size(262, 21)
        Me.TextConfirmNewPassword.TabIndex = 8
        Me.TextConfirmNewPassword.UseSystemPasswordChar = True
        '
        'PasswordKeyPad
        '
        Me.PasswordKeyPad.Location = New System.Drawing.Point(4, 51)
        Me.PasswordKeyPad.Name = "PasswordKeyPad"
        Me.PasswordKeyPad.Size = New System.Drawing.Size(262, 236)
        Me.PasswordKeyPad.TabIndex = 0
        '
        'PassWordForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 391)
        Me.ControlBox = False
        Me.Controls.Add(Me.NameNewPassword)
        Me.Controls.Add(Me.TextNewPassword)
        Me.Controls.Add(Me.NameOldPassword)
        Me.Controls.Add(Me.NameConfirmNewPassword)
        Me.Controls.Add(Me.TextConfirmNewPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.PasswordKeyPad)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "PassWordForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PassWordForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PasswordKeyPad As KeyPad
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents NameNewPassword As System.Windows.Forms.Label
    Friend WithEvents TextNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents NameOldPassword As System.Windows.Forms.Label
    Friend WithEvents NameConfirmNewPassword As System.Windows.Forms.Label
    Friend WithEvents TextConfirmNewPassword As System.Windows.Forms.TextBox
End Class
