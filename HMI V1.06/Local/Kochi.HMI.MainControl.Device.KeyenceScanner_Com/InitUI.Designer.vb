<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.components = New System.ComponentModel.Container()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_StopBits = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_DataBits = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Parity = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_StopBits = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_DataBits = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Parity = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_BaudRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_BaudRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Port = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Port = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_TimeOut = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_TimeOut = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 246)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_TimeOut, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_TimeOut, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_StopBits, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_DataBits, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Parity, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_StopBits, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_DataBits, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Parity, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_BaudRate, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_BaudRate, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Port, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Port, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 7
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_StopBits
        '
        Me.HmiTextBox_StopBits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_StopBits.Location = New System.Drawing.Point(78, 159)
        Me.HmiTextBox_StopBits.Name = "HmiTextBox_StopBits"
        Me.HmiTextBox_StopBits.Number = 0
        Me.HmiTextBox_StopBits.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_StopBits.TabIndex = 12
        Me.HmiTextBox_StopBits.TextBoxReadOnly = False
        Me.HmiTextBox_StopBits.ValueType = GetType(String)
        '
        'HmiTextBox_DataBits
        '
        Me.HmiTextBox_DataBits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_DataBits.Location = New System.Drawing.Point(78, 120)
        Me.HmiTextBox_DataBits.Name = "HmiTextBox_DataBits"
        Me.HmiTextBox_DataBits.Number = 0
        Me.HmiTextBox_DataBits.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_DataBits.TabIndex = 11
        Me.HmiTextBox_DataBits.TextBoxReadOnly = False
        Me.HmiTextBox_DataBits.ValueType = GetType(String)
        '
        'HmiTextBox_Parity
        '
        Me.HmiTextBox_Parity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Parity.Location = New System.Drawing.Point(78, 81)
        Me.HmiTextBox_Parity.Name = "HmiTextBox_Parity"
        Me.HmiTextBox_Parity.Number = 0
        Me.HmiTextBox_Parity.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Parity.TabIndex = 10
        Me.HmiTextBox_Parity.TextBoxReadOnly = False
        Me.HmiTextBox_Parity.ValueType = GetType(String)
        '
        'HmiLabel_StopBits
        '
        Me.HmiLabel_StopBits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StopBits.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_StopBits.Name = "HmiLabel_StopBits"
        Me.HmiLabel_StopBits.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_StopBits.TabIndex = 9
        '
        'HmiLabel_DataBits
        '
        Me.HmiLabel_DataBits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_DataBits.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_DataBits.Name = "HmiLabel_DataBits"
        Me.HmiLabel_DataBits.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_DataBits.TabIndex = 8
        '
        'HmiLabel_Parity
        '
        Me.HmiLabel_Parity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Parity.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Parity.Name = "HmiLabel_Parity"
        Me.HmiLabel_Parity.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Parity.TabIndex = 7
        '
        'HmiTextBox_BaudRate
        '
        Me.HmiTextBox_BaudRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_BaudRate.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_BaudRate.Name = "HmiTextBox_BaudRate"
        Me.HmiTextBox_BaudRate.Number = 0
        Me.HmiTextBox_BaudRate.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_BaudRate.TabIndex = 6
        Me.HmiTextBox_BaudRate.TextBoxReadOnly = False
        Me.HmiTextBox_BaudRate.ValueType = GetType(String)
        '
        'HmiLabel_BaudRate
        '
        Me.HmiLabel_BaudRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_BaudRate.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_BaudRate.Name = "HmiLabel_BaudRate"
        Me.HmiLabel_BaudRate.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_BaudRate.TabIndex = 5
        '
        'HmiTextBox_Port
        '
        Me.HmiTextBox_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Port.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_Port.Name = "HmiTextBox_Port"
        Me.HmiTextBox_Port.Number = 0
        Me.HmiTextBox_Port.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Port.TabIndex = 1
        Me.HmiTextBox_Port.TextBoxReadOnly = False
        Me.HmiTextBox_Port.ValueType = GetType(String)
        '
        'HmiLabel_Port
        '
        Me.HmiLabel_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Port.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Port.Name = "HmiLabel_Port"
        Me.HmiLabel_Port.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Port.TabIndex = 4
        '
        'HmiLabel_TimeOut
        '
        Me.HmiLabel_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TimeOut.Location = New System.Drawing.Point(3, 198)
        Me.HmiLabel_TimeOut.Name = "HmiLabel_TimeOut"
        Me.HmiLabel_TimeOut.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_TimeOut.TabIndex = 13
        '
        'HmiTextBox_TimeOut
        '
        Me.HmiTextBox_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TimeOut.Location = New System.Drawing.Point(78, 198)
        Me.HmiTextBox_TimeOut.Name = "HmiTextBox_TimeOut"
        Me.HmiTextBox_TimeOut.Number = 0
        Me.HmiTextBox_TimeOut.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_TimeOut.TabIndex = 14
        Me.HmiTextBox_TimeOut.TextBoxReadOnly = False
        Me.HmiTextBox_TimeOut.ValueType = GetType(String)
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 246)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InitUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents HmiTextBox_Port As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Port As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_BaudRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_BaudRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_StopBits As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_DataBits As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Parity As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_StopBits As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_DataBits As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Parity As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_TimeOut As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_TimeOut As Kochi.HMI.MainControl.UI.HMILabel
End Class
