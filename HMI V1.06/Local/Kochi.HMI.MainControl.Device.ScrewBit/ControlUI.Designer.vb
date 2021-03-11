<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlUI
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
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Result2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result1 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Pro = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Result1 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result2 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel_Result3 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result5 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result7 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result4 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result6 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Result8 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Result3 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result5 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result7 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result4 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result6 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Result8 = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.BackColor = System.Drawing.Color.White
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
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(467, 504)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Panel_Body_Bottom_Right
        '
        Me.Panel_Body_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Right.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Right.Name = "Panel_Body_Bottom_Right"
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(467, 504)
        Me.Panel_Body_Bottom_Right.TabIndex = 3
        '
        'HmiTableLayoutPanel_Body_Top_Right
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result8, 3, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result6, 3, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result4, 3, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result7, 1, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result5, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result3, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result8, 2, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result6, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result4, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result7, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result5, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result3, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result2, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result1, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Pro, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result1, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Result2, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 7
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(465, 502)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiLabel_Result2
        '
        Me.HmiLabel_Result2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result2.Location = New System.Drawing.Point(157, 83)
        Me.HmiLabel_Result2.Name = "HmiLabel_Result2"
        Me.HmiLabel_Result2.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result2.TabIndex = 46
        '
        'HmiLabel_Result1
        '
        Me.HmiLabel_Result1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result1.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_Result1.Name = "HmiLabel_Result1"
        Me.HmiLabel_Result1.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result1.TabIndex = 45
        '
        'HmiTextBox_Pro
        '
        Me.HmiTextBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pro.Location = New System.Drawing.Point(80, 43)
        Me.HmiTextBox_Pro.Name = "HmiTextBox_Pro"
        Me.HmiTextBox_Pro.Number = 0
        Me.HmiTextBox_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_Pro.TabIndex = 44
        Me.HmiTextBox_Pro.TextBoxReadOnly = False
        Me.HmiTextBox_Pro.ValueType = GetType(String)
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Pro.TabIndex = 21
        '
        'HmiSensor_Result1
        '
        Me.HmiSensor_Result1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result1.Location = New System.Drawing.Point(80, 83)
        Me.HmiSensor_Result1.Name = "HmiSensor_Result1"
        Me.HmiSensor_Result1.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result1.TabIndex = 47
        '
        'HmiSensor_Result2
        '
        Me.HmiSensor_Result2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result2.Location = New System.Drawing.Point(234, 83)
        Me.HmiSensor_Result2.Name = "HmiSensor_Result2"
        Me.HmiSensor_Result2.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result2.TabIndex = 48
        '
        'HmiLabel_Result3
        '
        Me.HmiLabel_Result3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result3.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_Result3.Name = "HmiLabel_Result3"
        Me.HmiLabel_Result3.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result3.TabIndex = 49
        '
        'HmiLabel_Result5
        '
        Me.HmiLabel_Result5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result5.Location = New System.Drawing.Point(3, 163)
        Me.HmiLabel_Result5.Name = "HmiLabel_Result5"
        Me.HmiLabel_Result5.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result5.TabIndex = 50
        '
        'HmiLabel_Result7
        '
        Me.HmiLabel_Result7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result7.Location = New System.Drawing.Point(3, 203)
        Me.HmiLabel_Result7.Name = "HmiLabel_Result7"
        Me.HmiLabel_Result7.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result7.TabIndex = 51
        '
        'HmiLabel_Result4
        '
        Me.HmiLabel_Result4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result4.Location = New System.Drawing.Point(157, 123)
        Me.HmiLabel_Result4.Name = "HmiLabel_Result4"
        Me.HmiLabel_Result4.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result4.TabIndex = 52
        '
        'HmiLabel_Result6
        '
        Me.HmiLabel_Result6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result6.Location = New System.Drawing.Point(157, 163)
        Me.HmiLabel_Result6.Name = "HmiLabel_Result6"
        Me.HmiLabel_Result6.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result6.TabIndex = 53
        '
        'HmiLabel_Result8
        '
        Me.HmiLabel_Result8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result8.Location = New System.Drawing.Point(157, 203)
        Me.HmiLabel_Result8.Name = "HmiLabel_Result8"
        Me.HmiLabel_Result8.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Result8.TabIndex = 54
        '
        'HmiSensor_Result3
        '
        Me.HmiSensor_Result3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result3.Location = New System.Drawing.Point(80, 123)
        Me.HmiSensor_Result3.Name = "HmiSensor_Result3"
        Me.HmiSensor_Result3.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result3.TabIndex = 55
        '
        'HmiSensor_Result5
        '
        Me.HmiSensor_Result5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result5.Location = New System.Drawing.Point(80, 163)
        Me.HmiSensor_Result5.Name = "HmiSensor_Result5"
        Me.HmiSensor_Result5.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result5.TabIndex = 56
        '
        'HmiSensor_Result7
        '
        Me.HmiSensor_Result7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result7.Location = New System.Drawing.Point(80, 203)
        Me.HmiSensor_Result7.Name = "HmiSensor_Result7"
        Me.HmiSensor_Result7.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result7.TabIndex = 57
        '
        'HmiSensor_Result4
        '
        Me.HmiSensor_Result4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result4.Location = New System.Drawing.Point(234, 123)
        Me.HmiSensor_Result4.Name = "HmiSensor_Result4"
        Me.HmiSensor_Result4.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result4.TabIndex = 58
        '
        'HmiSensor_Result6
        '
        Me.HmiSensor_Result6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result6.Location = New System.Drawing.Point(234, 163)
        Me.HmiSensor_Result6.Name = "HmiSensor_Result6"
        Me.HmiSensor_Result6.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result6.TabIndex = 59
        '
        'HmiSensor_Result8
        '
        Me.HmiSensor_Result8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Result8.Location = New System.Drawing.Point(234, 203)
        Me.HmiSensor_Result8.Name = "HmiSensor_Result8"
        Me.HmiSensor_Result8.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Result8.TabIndex = 60
        '
        'ControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ControlUI"
        Me.Text = "ShortCutUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Pro As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Result2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result1 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Result1 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result2 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result8 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result6 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result4 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result7 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result5 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Result3 As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_Result8 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result6 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result4 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result7 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result5 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Result3 As Kochi.HMI.MainControl.UI.HMILabel
End Class
