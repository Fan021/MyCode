<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShortCutUI
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
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body3 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_BlindShot = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Body3_BlindShot = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_PLC_BlindTime = New System.Windows.Forms.Label()
        Me.HmiTextBox_BlindNo = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_BlindTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_PLC_BlindNo = New System.Windows.Forms.Label()
        Me.Label_BlindNo = New System.Windows.Forms.Label()
        Me.Label_BlindTime = New System.Windows.Forms.Label()
        Me.Label_PotLife = New System.Windows.Forms.Label()
        Me.HmiTextBox_PotLife = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog_Path = New System.Windows.Forms.SaveFileDialog()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TableLayoutPanel_Body3.SuspendLayout()
        Me.GroupBox_BlindShot.SuspendLayout()
        Me.HmiTableLayoutPanel_Body3_BlindShot.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabPage7)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.SelectedIndex = 0
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 498)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 498)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TableLayoutPanel_Body3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 28)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(459, 498)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body3
        '
        Me.TableLayoutPanel_Body3.ColumnCount = 3
        Me.TableLayoutPanel_Body3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body3.Controls.Add(Me.GroupBox_BlindShot, 0, 1)
        Me.TableLayoutPanel_Body3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body3.Name = "TableLayoutPanel_Body3"
        Me.TableLayoutPanel_Body3.RowCount = 3
        Me.TableLayoutPanel_Body3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.761905!))
        Me.TableLayoutPanel_Body3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.90476!))
        Me.TableLayoutPanel_Body3.Size = New System.Drawing.Size(459, 498)
        Me.TableLayoutPanel_Body3.TabIndex = 0
        '
        'GroupBox_BlindShot
        '
        Me.GroupBox_BlindShot.Controls.Add(Me.HmiTableLayoutPanel_Body3_BlindShot)
        Me.GroupBox_BlindShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_BlindShot.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_BlindShot.Location = New System.Drawing.Point(1, 24)
        Me.GroupBox_BlindShot.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox_BlindShot.Name = "GroupBox_BlindShot"
        Me.GroupBox_BlindShot.Size = New System.Drawing.Size(151, 163)
        Me.GroupBox_BlindShot.TabIndex = 0
        Me.GroupBox_BlindShot.TabStop = False
        Me.GroupBox_BlindShot.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Body3_BlindShot
        '
        Me.HmiTableLayoutPanel_Body3_BlindShot.ColumnCount = 3
        Me.HmiTableLayoutPanel_Body3_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.Label_PLC_BlindTime, 2, 1)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.HmiTextBox_BlindNo, 1, 2)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.HmiTextBox_BlindTime, 1, 1)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.Label_PLC_BlindNo, 2, 2)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.Label_BlindNo, 0, 2)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.Label_BlindTime, 0, 1)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.Label_PotLife, 0, 0)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Controls.Add(Me.HmiTextBox_PotLife, 1, 0)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body3_BlindShot.Location = New System.Drawing.Point(3, 20)
        Me.HmiTableLayoutPanel_Body3_BlindShot.Name = "HmiTableLayoutPanel_Body3_BlindShot"
        Me.HmiTableLayoutPanel_Body3_BlindShot.RowCount = 4
        Me.HmiTableLayoutPanel_Body3_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body3_BlindShot.Size = New System.Drawing.Size(145, 140)
        Me.HmiTableLayoutPanel_Body3_BlindShot.TabIndex = 0
        '
        'Label_PLC_BlindTime
        '
        Me.Label_PLC_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PLC_BlindTime.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PLC_BlindTime.Location = New System.Drawing.Point(102, 36)
        Me.Label_PLC_BlindTime.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PLC_BlindTime.Name = "Label_PLC_BlindTime"
        Me.Label_PLC_BlindTime.Size = New System.Drawing.Size(42, 33)
        Me.Label_PLC_BlindTime.TabIndex = 15
        Me.Label_PLC_BlindTime.Text = "0.00"
        Me.Label_PLC_BlindTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_BlindNo
        '
        Me.HmiTextBox_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_BlindNo.Location = New System.Drawing.Point(61, 73)
        Me.HmiTextBox_BlindNo.Name = "HmiTextBox_BlindNo"
        Me.HmiTextBox_BlindNo.Number = 0
        Me.HmiTextBox_BlindNo.Size = New System.Drawing.Size(37, 29)
        Me.HmiTextBox_BlindNo.TabIndex = 13
        Me.HmiTextBox_BlindNo.TextBoxReadOnly = False
        Me.HmiTextBox_BlindNo.ValueType = GetType(String)
        '
        'HmiTextBox_BlindTime
        '
        Me.HmiTextBox_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_BlindTime.Location = New System.Drawing.Point(61, 38)
        Me.HmiTextBox_BlindTime.Name = "HmiTextBox_BlindTime"
        Me.HmiTextBox_BlindTime.Number = 0
        Me.HmiTextBox_BlindTime.Size = New System.Drawing.Size(37, 29)
        Me.HmiTextBox_BlindTime.TabIndex = 11
        Me.HmiTextBox_BlindTime.TextBoxReadOnly = False
        Me.HmiTextBox_BlindTime.ValueType = GetType(String)
        '
        'Label_PLC_BlindNo
        '
        Me.Label_PLC_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PLC_BlindNo.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PLC_BlindNo.Location = New System.Drawing.Point(102, 71)
        Me.Label_PLC_BlindNo.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PLC_BlindNo.Name = "Label_PLC_BlindNo"
        Me.Label_PLC_BlindNo.Size = New System.Drawing.Size(42, 33)
        Me.Label_PLC_BlindNo.TabIndex = 8
        Me.Label_PLC_BlindNo.Text = "[ 0 ]"
        Me.Label_PLC_BlindNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_BlindNo
        '
        Me.Label_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BlindNo.Location = New System.Drawing.Point(1, 71)
        Me.Label_BlindNo.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_BlindNo.Name = "Label_BlindNo"
        Me.Label_BlindNo.Size = New System.Drawing.Size(56, 33)
        Me.Label_BlindNo.TabIndex = 6
        Me.Label_BlindNo.Text = "Label8"
        Me.Label_BlindNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_BlindTime
        '
        Me.Label_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BlindTime.Location = New System.Drawing.Point(1, 36)
        Me.Label_BlindTime.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_BlindTime.Name = "Label_BlindTime"
        Me.Label_BlindTime.Size = New System.Drawing.Size(56, 33)
        Me.Label_BlindTime.TabIndex = 3
        Me.Label_BlindTime.Text = "Label5"
        Me.Label_BlindTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PotLife
        '
        Me.Label_PotLife.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PotLife.Location = New System.Drawing.Point(1, 1)
        Me.Label_PotLife.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PotLife.Name = "Label_PotLife"
        Me.Label_PotLife.Size = New System.Drawing.Size(56, 33)
        Me.Label_PotLife.TabIndex = 0
        Me.Label_PotLife.Text = "Label2"
        Me.Label_PotLife.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HmiTextBox_PotLife
        '
        Me.HmiTextBox_PotLife.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PotLife.Location = New System.Drawing.Point(61, 3)
        Me.HmiTextBox_PotLife.Name = "HmiTextBox_PotLife"
        Me.HmiTextBox_PotLife.Number = 0
        Me.HmiTextBox_PotLife.Size = New System.Drawing.Size(37, 29)
        Me.HmiTextBox_PotLife.TabIndex = 9
        Me.HmiTextBox_PotLife.TextBoxReadOnly = False
        Me.HmiTextBox_PotLife.ValueType = GetType(String)
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 28)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(459, 498)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.TableLayoutPanel_Body4)
        Me.TabPage5.Location = New System.Drawing.Point(4, 28)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(459, 498)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.TableLayoutPanel_Body5)
        Me.TabPage6.Location = New System.Drawing.Point(4, 28)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(459, 498)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "TabPage6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.TableLayoutPanel_Body6)
        Me.TabPage7.Location = New System.Drawing.Point(4, 28)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(459, 498)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "TabPage7"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body6
        '
        Me.TableLayoutPanel_Body6.ColumnCount = 1
        Me.TableLayoutPanel_Body6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body6.Name = "TableLayoutPanel_Body6"
        Me.TableLayoutPanel_Body6.RowCount = 2
        Me.TableLayoutPanel_Body6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body6.Size = New System.Drawing.Size(459, 498)
        Me.TableLayoutPanel_Body6.TabIndex = 3
        '
        'TableLayoutPanel_Body5
        '
        Me.TableLayoutPanel_Body5.ColumnCount = 1
        Me.TableLayoutPanel_Body5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body5.Name = "TableLayoutPanel_Body5"
        Me.TableLayoutPanel_Body5.RowCount = 2
        Me.TableLayoutPanel_Body5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body5.Size = New System.Drawing.Size(459, 498)
        Me.TableLayoutPanel_Body5.TabIndex = 2
        '
        'TableLayoutPanel_Body4
        '
        Me.TableLayoutPanel_Body4.ColumnCount = 1
        Me.TableLayoutPanel_Body4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body4.Name = "TableLayoutPanel_Body4"
        Me.TableLayoutPanel_Body4.RowCount = 2
        Me.TableLayoutPanel_Body4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body4.Size = New System.Drawing.Size(459, 498)
        Me.TableLayoutPanel_Body4.TabIndex = 1
        '
        'ShortCutUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShortCutUI"
        Me.Text = "ShortCutUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TableLayoutPanel_Body3.ResumeLayout(False)
        Me.GroupBox_BlindShot.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body3_BlindShot.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog_Path As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TableLayoutPanel_Body3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_BlindShot As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Body3_BlindShot As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_PLC_BlindNo As System.Windows.Forms.Label
    Friend WithEvents Label_BlindNo As System.Windows.Forms.Label
    Friend WithEvents Label_BlindTime As System.Windows.Forms.Label
    Friend WithEvents Label_PotLife As System.Windows.Forms.Label
    Friend WithEvents Label_PLC_BlindTime As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_BlindNo As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_BlindTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PotLife As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body6 As System.Windows.Forms.TableLayoutPanel
End Class
