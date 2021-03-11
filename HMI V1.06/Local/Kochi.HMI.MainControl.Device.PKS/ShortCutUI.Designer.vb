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
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiSensor_Z = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel_SensorZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SensorR = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SensorX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_R = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_X = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTextBox_ToleranceZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceR = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceR = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Z = New System.Windows.Forms.Label()
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_R = New System.Windows.Forms.Label()
        Me.HmiLabel_R = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveR = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveR = New Kochi.HMI.MainControl.UI.HMILabel()
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Z, 5, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorZ, 4, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorR, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorX, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_R, 3, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_X, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceZ, 5, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceZ, 4, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceR, 3, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceR, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceX, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceX, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Z, 5, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Z, 4, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_R, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_R, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_X, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_X, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveZ, 5, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveZ, 4, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveR, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveX, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveX, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveR, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(465, 502)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiSensor_Z
        '
        Me.HmiSensor_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Z.Location = New System.Drawing.Point(388, 163)
        Me.HmiSensor_Z.Name = "HmiSensor_Z"
        Me.HmiSensor_Z.Size = New System.Drawing.Size(74, 34)
        Me.HmiSensor_Z.TabIndex = 54
        '
        'HmiLabel_SensorZ
        '
        Me.HmiLabel_SensorZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorZ.Location = New System.Drawing.Point(311, 163)
        Me.HmiLabel_SensorZ.Name = "HmiLabel_SensorZ"
        Me.HmiLabel_SensorZ.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_SensorZ.TabIndex = 53
        '
        'HmiLabel_SensorR
        '
        Me.HmiLabel_SensorR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorR.Location = New System.Drawing.Point(157, 163)
        Me.HmiLabel_SensorR.Name = "HmiLabel_SensorR"
        Me.HmiLabel_SensorR.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_SensorR.TabIndex = 50
        '
        'HmiLabel_SensorX
        '
        Me.HmiLabel_SensorX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorX.Location = New System.Drawing.Point(3, 163)
        Me.HmiLabel_SensorX.Name = "HmiLabel_SensorX"
        Me.HmiLabel_SensorX.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_SensorX.TabIndex = 49
        '
        'HmiSensor_R
        '
        Me.HmiSensor_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_R.Location = New System.Drawing.Point(234, 163)
        Me.HmiSensor_R.Name = "HmiSensor_R"
        Me.HmiSensor_R.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_R.TabIndex = 51
        '
        'HmiSensor_X
        '
        Me.HmiSensor_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_X.Location = New System.Drawing.Point(80, 163)
        Me.HmiSensor_X.Name = "HmiSensor_X"
        Me.HmiSensor_X.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_X.TabIndex = 52
        '
        'HmiTextBox_ToleranceZ
        '
        Me.HmiTextBox_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceZ.Location = New System.Drawing.Point(388, 123)
        Me.HmiTextBox_ToleranceZ.Name = "HmiTextBox_ToleranceZ"
        Me.HmiTextBox_ToleranceZ.Number = 0
        Me.HmiTextBox_ToleranceZ.Size = New System.Drawing.Size(74, 34)
        Me.HmiTextBox_ToleranceZ.TabIndex = 42
        Me.HmiTextBox_ToleranceZ.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceZ.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceZ
        '
        Me.HmiLabel_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceZ.Location = New System.Drawing.Point(311, 123)
        Me.HmiLabel_ToleranceZ.Name = "HmiLabel_ToleranceZ"
        Me.HmiLabel_ToleranceZ.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_ToleranceZ.TabIndex = 41
        '
        'HmiTextBox_ToleranceR
        '
        Me.HmiTextBox_ToleranceR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceR.Location = New System.Drawing.Point(234, 123)
        Me.HmiTextBox_ToleranceR.Name = "HmiTextBox_ToleranceR"
        Me.HmiTextBox_ToleranceR.Number = 0
        Me.HmiTextBox_ToleranceR.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_ToleranceR.TabIndex = 40
        Me.HmiTextBox_ToleranceR.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceR.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceR
        '
        Me.HmiLabel_ToleranceR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceR.Location = New System.Drawing.Point(157, 123)
        Me.HmiLabel_ToleranceR.Name = "HmiLabel_ToleranceR"
        Me.HmiLabel_ToleranceR.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_ToleranceR.TabIndex = 39
        '
        'HmiTextBox_ToleranceX
        '
        Me.HmiTextBox_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceX.Location = New System.Drawing.Point(80, 123)
        Me.HmiTextBox_ToleranceX.Name = "HmiTextBox_ToleranceX"
        Me.HmiTextBox_ToleranceX.Number = 0
        Me.HmiTextBox_ToleranceX.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_ToleranceX.TabIndex = 38
        Me.HmiTextBox_ToleranceX.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceX.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceX
        '
        Me.HmiLabel_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceX.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_ToleranceX.Name = "HmiLabel_ToleranceX"
        Me.HmiLabel_ToleranceX.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_ToleranceX.TabIndex = 37
        '
        'Label_Z
        '
        Me.Label_Z.AutoSize = True
        Me.Label_Z.BackColor = System.Drawing.Color.LightGray
        Me.Label_Z.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Z.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_Z.ForeColor = System.Drawing.Color.Blue
        Me.Label_Z.Location = New System.Drawing.Point(390, 45)
        Me.Label_Z.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Z.Name = "Label_Z"
        Me.Label_Z.Size = New System.Drawing.Size(70, 30)
        Me.Label_Z.TabIndex = 36
        Me.Label_Z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(311, 43)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Z.TabIndex = 35
        '
        'Label_R
        '
        Me.Label_R.AutoSize = True
        Me.Label_R.BackColor = System.Drawing.Color.LightGray
        Me.Label_R.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_R.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_R.ForeColor = System.Drawing.Color.Blue
        Me.Label_R.Location = New System.Drawing.Point(236, 45)
        Me.Label_R.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_R.Name = "Label_R"
        Me.Label_R.Size = New System.Drawing.Size(67, 30)
        Me.Label_R.TabIndex = 34
        Me.Label_R.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_R
        '
        Me.HmiLabel_R.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_R.Location = New System.Drawing.Point(157, 43)
        Me.HmiLabel_R.Name = "HmiLabel_R"
        Me.HmiLabel_R.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_R.TabIndex = 33
        '
        'Label_X
        '
        Me.Label_X.AutoSize = True
        Me.Label_X.BackColor = System.Drawing.Color.LightGray
        Me.Label_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_X.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_X.ForeColor = System.Drawing.Color.Blue
        Me.Label_X.Location = New System.Drawing.Point(82, 45)
        Me.Label_X.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_X.Name = "Label_X"
        Me.Label_X.Size = New System.Drawing.Size(67, 30)
        Me.Label_X.TabIndex = 32
        Me.Label_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_X.TabIndex = 31
        '
        'HmiTextBox_MoveZ
        '
        Me.HmiTextBox_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveZ.Location = New System.Drawing.Point(388, 83)
        Me.HmiTextBox_MoveZ.Name = "HmiTextBox_MoveZ"
        Me.HmiTextBox_MoveZ.Number = 0
        Me.HmiTextBox_MoveZ.Size = New System.Drawing.Size(74, 34)
        Me.HmiTextBox_MoveZ.TabIndex = 28
        Me.HmiTextBox_MoveZ.TextBoxReadOnly = False
        Me.HmiTextBox_MoveZ.ValueType = GetType(String)
        '
        'HmiLabel_MoveZ
        '
        Me.HmiLabel_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveZ.Location = New System.Drawing.Point(311, 83)
        Me.HmiLabel_MoveZ.Name = "HmiLabel_MoveZ"
        Me.HmiLabel_MoveZ.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_MoveZ.TabIndex = 27
        '
        'HmiTextBox_MoveR
        '
        Me.HmiTextBox_MoveR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveR.Location = New System.Drawing.Point(234, 83)
        Me.HmiTextBox_MoveR.Name = "HmiTextBox_MoveR"
        Me.HmiTextBox_MoveR.Number = 0
        Me.HmiTextBox_MoveR.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_MoveR.TabIndex = 26
        Me.HmiTextBox_MoveR.TextBoxReadOnly = False
        Me.HmiTextBox_MoveR.ValueType = GetType(String)
        '
        'HmiLabel_MoveX
        '
        Me.HmiLabel_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveX.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_MoveX.Name = "HmiLabel_MoveX"
        Me.HmiLabel_MoveX.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_MoveX.TabIndex = 10
        '
        'HmiTextBox_MoveX
        '
        Me.HmiTextBox_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveX.Location = New System.Drawing.Point(80, 83)
        Me.HmiTextBox_MoveX.Name = "HmiTextBox_MoveX"
        Me.HmiTextBox_MoveX.Number = 0
        Me.HmiTextBox_MoveX.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_MoveX.TabIndex = 14
        Me.HmiTextBox_MoveX.TextBoxReadOnly = False
        Me.HmiTextBox_MoveX.ValueType = GetType(String)
        '
        'HmiLabel_MoveR
        '
        Me.HmiLabel_MoveR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveR.Location = New System.Drawing.Point(157, 83)
        Me.HmiLabel_MoveR.Name = "HmiLabel_MoveR"
        Me.HmiLabel_MoveR.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_MoveR.TabIndex = 15
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
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_MoveX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveR As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveR As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MoveZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_Z As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_R As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_R As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_X As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceR As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceR As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Z As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_SensorZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SensorR As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SensorX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_R As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_X As Kochi.HMI.MainControl.UI.HMISensor
End Class
