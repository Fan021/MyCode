<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActionUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component List.
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
        Me.HmiTextBox_ToleranceX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PicturePosition = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_PicturePosition = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_PKS = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_PKS = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_X = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_R = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_R = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceR = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceR = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Z = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Pic = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_PKS = New Kochi.HMI.MainControl.UI.HMIButton()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 446)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 4
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ToleranceX, 3, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_PicturePosition, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PicturePosition, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PKS, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_PKS, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_X, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_X, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ToleranceX, 2, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_R, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_R, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ToleranceR, 2, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ToleranceR, 3, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Z, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Z, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ToleranceZ, 2, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ToleranceZ, 3, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_Pic, 3, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_PKS, 3, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 6
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 446)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_ToleranceX
        '
        Me.HmiTextBox_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceX.Location = New System.Drawing.Point(228, 81)
        Me.HmiTextBox_ToleranceX.Name = "HmiTextBox_ToleranceX"
        Me.HmiTextBox_ToleranceX.Number = 0
        Me.HmiTextBox_ToleranceX.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_ToleranceX.TabIndex = 14
        Me.HmiTextBox_ToleranceX.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceX.ValueType = GetType(String)
        '
        'HmiTextBox_PicturePosition
        '
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.HmiTextBox_PicturePosition, 2)
        Me.HmiTextBox_PicturePosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PicturePosition.Location = New System.Drawing.Point(81, 3)
        Me.HmiTextBox_PicturePosition.Name = "HmiTextBox_PicturePosition"
        Me.HmiTextBox_PicturePosition.Number = 0
        Me.HmiTextBox_PicturePosition.Size = New System.Drawing.Size(141, 33)
        Me.HmiTextBox_PicturePosition.TabIndex = 0
        Me.HmiTextBox_PicturePosition.TextBoxReadOnly = False
        Me.HmiTextBox_PicturePosition.ValueType = GetType(String)
        '
        'HmiLabel_PicturePosition
        '
        Me.HmiLabel_PicturePosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PicturePosition.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_PicturePosition.Name = "HmiLabel_PicturePosition"
        Me.HmiLabel_PicturePosition.Size = New System.Drawing.Size(72, 33)
        Me.HmiLabel_PicturePosition.TabIndex = 3
        '
        'HmiLabel_PKS
        '
        Me.HmiLabel_PKS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PKS.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_PKS.Name = "HmiLabel_PKS"
        Me.HmiLabel_PKS.Size = New System.Drawing.Size(72, 33)
        Me.HmiLabel_PKS.TabIndex = 5
        '
        'HmiComboBox_PKS
        '
        Me.HmiComboBox_PKS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_PKS.Location = New System.Drawing.Point(81, 42)
        Me.HmiComboBox_PKS.Name = "HmiComboBox_PKS"
        Me.HmiComboBox_PKS.Size = New System.Drawing.Size(66, 33)
        Me.HmiComboBox_PKS.TabIndex = 10
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(72, 33)
        Me.HmiLabel_X.TabIndex = 11
        '
        'HmiTextBox_X
        '
        Me.HmiTextBox_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_X.Location = New System.Drawing.Point(81, 81)
        Me.HmiTextBox_X.Name = "HmiTextBox_X"
        Me.HmiTextBox_X.Number = 0
        Me.HmiTextBox_X.Size = New System.Drawing.Size(66, 33)
        Me.HmiTextBox_X.TabIndex = 12
        Me.HmiTextBox_X.TextBoxReadOnly = False
        Me.HmiTextBox_X.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceX
        '
        Me.HmiLabel_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceX.Location = New System.Drawing.Point(153, 81)
        Me.HmiLabel_ToleranceX.Name = "HmiLabel_ToleranceX"
        Me.HmiLabel_ToleranceX.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_ToleranceX.TabIndex = 13
        '
        'HmiLabel_R
        '
        Me.HmiLabel_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_R.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_R.Name = "HmiLabel_R"
        Me.HmiLabel_R.Size = New System.Drawing.Size(72, 33)
        Me.HmiLabel_R.TabIndex = 15
        '
        'HmiTextBox_R
        '
        Me.HmiTextBox_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_R.Location = New System.Drawing.Point(81, 120)
        Me.HmiTextBox_R.Name = "HmiTextBox_R"
        Me.HmiTextBox_R.Number = 0
        Me.HmiTextBox_R.Size = New System.Drawing.Size(66, 33)
        Me.HmiTextBox_R.TabIndex = 16
        Me.HmiTextBox_R.TextBoxReadOnly = False
        Me.HmiTextBox_R.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceR
        '
        Me.HmiLabel_ToleranceR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceR.Location = New System.Drawing.Point(153, 120)
        Me.HmiLabel_ToleranceR.Name = "HmiLabel_ToleranceR"
        Me.HmiLabel_ToleranceR.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_ToleranceR.TabIndex = 17
        '
        'HmiTextBox_ToleranceR
        '
        Me.HmiTextBox_ToleranceR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceR.Location = New System.Drawing.Point(228, 120)
        Me.HmiTextBox_ToleranceR.Name = "HmiTextBox_ToleranceR"
        Me.HmiTextBox_ToleranceR.Number = 0
        Me.HmiTextBox_ToleranceR.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_ToleranceR.TabIndex = 18
        Me.HmiTextBox_ToleranceR.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceR.ValueType = GetType(String)
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(72, 33)
        Me.HmiLabel_Z.TabIndex = 19
        '
        'HmiTextBox_Z
        '
        Me.HmiTextBox_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Z.Location = New System.Drawing.Point(81, 159)
        Me.HmiTextBox_Z.Name = "HmiTextBox_Z"
        Me.HmiTextBox_Z.Number = 0
        Me.HmiTextBox_Z.Size = New System.Drawing.Size(66, 33)
        Me.HmiTextBox_Z.TabIndex = 20
        Me.HmiTextBox_Z.TextBoxReadOnly = False
        Me.HmiTextBox_Z.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceZ
        '
        Me.HmiLabel_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceZ.Location = New System.Drawing.Point(153, 159)
        Me.HmiLabel_ToleranceZ.Name = "HmiLabel_ToleranceZ"
        Me.HmiLabel_ToleranceZ.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_ToleranceZ.TabIndex = 21
        '
        'HmiTextBox_ToleranceZ
        '
        Me.HmiTextBox_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceZ.Location = New System.Drawing.Point(228, 159)
        Me.HmiTextBox_ToleranceZ.Name = "HmiTextBox_ToleranceZ"
        Me.HmiTextBox_ToleranceZ.Number = 0
        Me.HmiTextBox_ToleranceZ.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_ToleranceZ.TabIndex = 22
        Me.HmiTextBox_ToleranceZ.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceZ.ValueType = GetType(String)
        '
        'HmiButton_Pic
        '
        Me.HmiButton_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Pic.Location = New System.Drawing.Point(228, 3)
        Me.HmiButton_Pic.MarginHeight = 6
        Me.HmiButton_Pic.Name = "HmiButton_Pic"
        Me.HmiButton_Pic.Size = New System.Drawing.Size(69, 33)
        Me.HmiButton_Pic.TabIndex = 23
        '
        'HmiButton_PKS
        '
        Me.HmiButton_PKS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_PKS.Location = New System.Drawing.Point(228, 42)
        Me.HmiButton_PKS.MarginHeight = 6
        Me.HmiButton_PKS.Name = "HmiButton_PKS"
        Me.HmiButton_PKS.Size = New System.Drawing.Size(69, 33)
        Me.HmiButton_PKS.TabIndex = 24
        '
        'ActionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 446)
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
    Friend WithEvents HmiTextBox_PicturePosition As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_PicturePosition As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_PKS As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_PKS As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_X As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_R As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_R As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceR As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceR As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Z As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Pic As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_PKS As Kochi.HMI.MainControl.UI.HMIButton
End Class
