Imports Kochi.HMI.MainControl.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActionUI
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
        Me.HmiLabel_PrintFile = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_FormatFile = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Printer = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_LKSN = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Printer = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_LKSN = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_FormatFile = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PrintFile = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_FormatFile = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_PrintFile = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 361)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 4
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PrintFile, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_FormatFile, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Printer, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_LKSN, 2, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Printer, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_LKSN, 3, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_FormatFile, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_PrintFile, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_FormatFile, 3, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_PrintFile, 3, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 4
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_PrintFile
        '
        Me.HmiLabel_PrintFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PrintFile.Location = New System.Drawing.Point(1, 79)
        Me.HmiLabel_PrintFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_PrintFile.Name = "HmiLabel_PrintFile"
        Me.HmiLabel_PrintFile.Size = New System.Drawing.Size(73, 37)
        Me.HmiLabel_PrintFile.TabIndex = 13
        '
        'HmiLabel_FormatFile
        '
        Me.HmiLabel_FormatFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_FormatFile.Location = New System.Drawing.Point(1, 40)
        Me.HmiLabel_FormatFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_FormatFile.Name = "HmiLabel_FormatFile"
        Me.HmiLabel_FormatFile.Size = New System.Drawing.Size(73, 37)
        Me.HmiLabel_FormatFile.TabIndex = 12
        '
        'HmiLabel_Printer
        '
        Me.HmiLabel_Printer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Printer.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Printer.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Printer.Name = "HmiLabel_Printer"
        Me.HmiLabel_Printer.Size = New System.Drawing.Size(73, 37)
        Me.HmiLabel_Printer.TabIndex = 4
        '
        'HmiLabel_LKSN
        '
        Me.HmiLabel_LKSN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_LKSN.Location = New System.Drawing.Point(151, 1)
        Me.HmiLabel_LKSN.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_LKSN.Name = "HmiLabel_LKSN"
        Me.HmiLabel_LKSN.Size = New System.Drawing.Size(73, 37)
        Me.HmiLabel_LKSN.TabIndex = 5
        '
        'HmiComboBox_Printer
        '
        Me.HmiComboBox_Printer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Printer.Location = New System.Drawing.Point(76, 1)
        Me.HmiComboBox_Printer.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Printer.Name = "HmiComboBox_Printer"
        Me.HmiComboBox_Printer.Size = New System.Drawing.Size(73, 37)
        Me.HmiComboBox_Printer.TabIndex = 6
        '
        'HmiComboBox_LKSN
        '
        Me.HmiComboBox_LKSN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_LKSN.Location = New System.Drawing.Point(226, 1)
        Me.HmiComboBox_LKSN.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_LKSN.Name = "HmiComboBox_LKSN"
        Me.HmiComboBox_LKSN.Size = New System.Drawing.Size(73, 37)
        Me.HmiComboBox_LKSN.TabIndex = 10
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 4)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 118)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 242)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiTextBox_FormatFile
        '
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.HmiTextBox_FormatFile, 2)
        Me.HmiTextBox_FormatFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_FormatFile.Location = New System.Drawing.Point(76, 40)
        Me.HmiTextBox_FormatFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_FormatFile.Name = "HmiTextBox_FormatFile"
        Me.HmiTextBox_FormatFile.Number = 0
        Me.HmiTextBox_FormatFile.Size = New System.Drawing.Size(148, 37)
        Me.HmiTextBox_FormatFile.TabIndex = 14
        Me.HmiTextBox_FormatFile.TextBoxReadOnly = False
        Me.HmiTextBox_FormatFile.ValueType = GetType(String)
        '
        'HmiTextBox_PrintFile
        '
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.HmiTextBox_PrintFile, 2)
        Me.HmiTextBox_PrintFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PrintFile.Location = New System.Drawing.Point(76, 79)
        Me.HmiTextBox_PrintFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PrintFile.Name = "HmiTextBox_PrintFile"
        Me.HmiTextBox_PrintFile.Number = 0
        Me.HmiTextBox_PrintFile.Size = New System.Drawing.Size(148, 37)
        Me.HmiTextBox_PrintFile.TabIndex = 15
        Me.HmiTextBox_PrintFile.TextBoxReadOnly = False
        Me.HmiTextBox_PrintFile.ValueType = GetType(String)
        '
        'HmiButton_FormatFile
        '
        Me.HmiButton_FormatFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_FormatFile.Location = New System.Drawing.Point(226, 40)
        Me.HmiButton_FormatFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_FormatFile.MarginHeight = 6
        Me.HmiButton_FormatFile.Name = "HmiButton_FormatFile"
        Me.HmiButton_FormatFile.Size = New System.Drawing.Size(73, 37)
        Me.HmiButton_FormatFile.TabIndex = 16
        '
        'HmiButton_PrintFile
        '
        Me.HmiButton_PrintFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_PrintFile.Location = New System.Drawing.Point(226, 79)
        Me.HmiButton_PrintFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_PrintFile.MarginHeight = 6
        Me.HmiButton_PrintFile.Name = "HmiButton_PrintFile"
        Me.HmiButton_PrintFile.Size = New System.Drawing.Size(73, 37)
        Me.HmiButton_PrintFile.TabIndex = 17
        '
        'ActionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 361)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ActionUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Printer As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_LKSN As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Printer As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_LKSN As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_PrintFile As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_FormatFile As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_FormatFile As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PrintFile As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_FormatFile As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_PrintFile As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
End Class
