Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HMIComboBox
    Inherits System.Windows.Forms.UserControl
    ' Dim toolTip1 As New ToolTip
    'UserControl overrides dispose to clean up the component List.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                'toolTip1.Active = False
                'toolTip1.Dispose()
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
        Me.ComboBoxValue = New Kochi.HMI.MainControl.UI.ComboBoxEx()
        Me.Panel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.ComboBoxValue)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.TabIndex = 0
        '
        'ComboBoxValue
        '
        Me.ComboBoxValue.BackColor = System.Drawing.Color.White
        Me.ComboBoxValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxValue.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ComboBoxValue.FormattingEnabled = True
        Me.ComboBoxValue.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxValue.Margin = New System.Windows.Forms.Padding(0)
        Me.ComboBoxValue.Name = "ComboBoxValue"
        Me.ComboBoxValue.Size = New System.Drawing.Size(106, 28)
        Me.ComboBoxValue.TabIndex = 0
        '
        'HMIComboBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Body)
        Me.Name = "HMIComboBox"
        Me.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents ComboBoxValue As ComboBoxEx
End Class
