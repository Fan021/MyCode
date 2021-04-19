<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HMITextBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component List.
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TextBoxValue = New TextBoxEx()
        Me.Panel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TextBoxValue)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.TabIndex = 0
        '
        'TextBoxValue
        '
        Me.TextBoxValue.BackColor = System.Drawing.Color.White
        Me.TextBoxValue.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TextBoxValue.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxValue.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxValue.Name = "TextBoxValue"
        Me.TextBoxValue.Size = New System.Drawing.Size(100, 27)
        Me.TextBoxValue.TabIndex = 0
        '
        'HMITextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Body)
        Me.Name = "HMITextBox"
        Me.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.ResumeLayout(False)
        Me.Panel_Body.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TextBoxValue As TextBoxEx

End Class
