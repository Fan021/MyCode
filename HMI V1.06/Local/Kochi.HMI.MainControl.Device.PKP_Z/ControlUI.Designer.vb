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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_Pro = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_AST = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.PictureBox_Pic = New System.Windows.Forms.PictureBox()
        Me.HmiSensor_Z = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel_SensorZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SensorY = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SensorX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Y = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_X = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTextBox_ToleranceZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceY = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ToleranceX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ToleranceX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Z = New System.Windows.Forms.Label()
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Y = New System.Windows.Forms.Label()
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Save = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Function = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_AST = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_MoveX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiButton_Variant = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_MoveX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveY = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Teach = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.Panel_Body_Bottom_Left = New System.Windows.Forms.Panel()
        Me.HmiDataView_Point = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        CType(Me.PictureBox_Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Body_Bottom_Left.SuspendLayout()
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(623, 530)
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Left, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(623, 504)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Panel_Body_Bottom_Right
        '
        Me.Panel_Body_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Right.Location = New System.Drawing.Point(249, 0)
        Me.Panel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Right.Name = "Panel_Body_Bottom_Right"
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(374, 504)
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Pro, 3, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_AST, 1, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.PictureBox_Pic, 1, 8)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Z, 5, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorZ, 4, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorY, 2, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SensorX, 0, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Y, 3, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_X, 1, 7)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceZ, 5, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceZ, 4, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceY, 3, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceY, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_ToleranceX, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_ToleranceX, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Z, 5, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Z, 4, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Y, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Y, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_X, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_X, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Save, 1, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Function, 0, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveZ, 5, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveZ, 4, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveY, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 2, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_AST, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveX, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_Variant, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Variant, 3, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveX, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveY, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Teach, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 9
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(372, 502)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiTextBox_Pro
        '
        Me.HmiTextBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pro.Location = New System.Drawing.Point(189, 203)
        Me.HmiTextBox_Pro.Name = "HmiTextBox_Pro"
        Me.HmiTextBox_Pro.Number = 0
        Me.HmiTextBox_Pro.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_Pro.TabIndex = 63
        Me.HmiTextBox_Pro.TextBoxReadOnly = False
        Me.HmiTextBox_Pro.ValueType = GetType(String)
        '
        'HmiTextBox_AST
        '
        Me.HmiTextBox_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AST.Location = New System.Drawing.Point(65, 203)
        Me.HmiTextBox_AST.Name = "HmiTextBox_AST"
        Me.HmiTextBox_AST.Number = 0
        Me.HmiTextBox_AST.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_AST.TabIndex = 62
        Me.HmiTextBox_AST.TextBoxReadOnly = False
        Me.HmiTextBox_AST.ValueType = GetType(String)
        '
        'PictureBox_Pic
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.PictureBox_Pic, 4)
        Me.PictureBox_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_Pic.Location = New System.Drawing.Point(65, 323)
        Me.PictureBox_Pic.Name = "PictureBox_Pic"
        Me.PictureBox_Pic.Size = New System.Drawing.Size(242, 176)
        Me.PictureBox_Pic.TabIndex = 61
        Me.PictureBox_Pic.TabStop = False
        '
        'HmiSensor_Z
        '
        Me.HmiSensor_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Z.Location = New System.Drawing.Point(313, 283)
        Me.HmiSensor_Z.Name = "HmiSensor_Z"
        Me.HmiSensor_Z.Size = New System.Drawing.Size(56, 34)
        Me.HmiSensor_Z.TabIndex = 60
        '
        'HmiLabel_SensorZ
        '
        Me.HmiLabel_SensorZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorZ.Location = New System.Drawing.Point(251, 283)
        Me.HmiLabel_SensorZ.Name = "HmiLabel_SensorZ"
        Me.HmiLabel_SensorZ.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_SensorZ.TabIndex = 59
        '
        'HmiLabel_SensorY
        '
        Me.HmiLabel_SensorY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorY.Location = New System.Drawing.Point(127, 283)
        Me.HmiLabel_SensorY.Name = "HmiLabel_SensorY"
        Me.HmiLabel_SensorY.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_SensorY.TabIndex = 56
        '
        'HmiLabel_SensorX
        '
        Me.HmiLabel_SensorX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SensorX.Location = New System.Drawing.Point(3, 283)
        Me.HmiLabel_SensorX.Name = "HmiLabel_SensorX"
        Me.HmiLabel_SensorX.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_SensorX.TabIndex = 55
        '
        'HmiSensor_Y
        '
        Me.HmiSensor_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Y.Location = New System.Drawing.Point(189, 283)
        Me.HmiSensor_Y.Name = "HmiSensor_Y"
        Me.HmiSensor_Y.Size = New System.Drawing.Size(56, 34)
        Me.HmiSensor_Y.TabIndex = 57
        '
        'HmiSensor_X
        '
        Me.HmiSensor_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_X.Location = New System.Drawing.Point(65, 283)
        Me.HmiSensor_X.Name = "HmiSensor_X"
        Me.HmiSensor_X.Size = New System.Drawing.Size(56, 34)
        Me.HmiSensor_X.TabIndex = 58
        '
        'HmiTextBox_ToleranceZ
        '
        Me.HmiTextBox_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceZ.Location = New System.Drawing.Point(313, 123)
        Me.HmiTextBox_ToleranceZ.Name = "HmiTextBox_ToleranceZ"
        Me.HmiTextBox_ToleranceZ.Number = 0
        Me.HmiTextBox_ToleranceZ.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_ToleranceZ.TabIndex = 42
        Me.HmiTextBox_ToleranceZ.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceZ.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceZ
        '
        Me.HmiLabel_ToleranceZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceZ.Location = New System.Drawing.Point(251, 123)
        Me.HmiLabel_ToleranceZ.Name = "HmiLabel_ToleranceZ"
        Me.HmiLabel_ToleranceZ.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_ToleranceZ.TabIndex = 41
        '
        'HmiTextBox_ToleranceY
        '
        Me.HmiTextBox_ToleranceY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceY.Location = New System.Drawing.Point(189, 123)
        Me.HmiTextBox_ToleranceY.Name = "HmiTextBox_ToleranceY"
        Me.HmiTextBox_ToleranceY.Number = 0
        Me.HmiTextBox_ToleranceY.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_ToleranceY.TabIndex = 40
        Me.HmiTextBox_ToleranceY.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceY.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceY
        '
        Me.HmiLabel_ToleranceY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceY.Location = New System.Drawing.Point(127, 123)
        Me.HmiLabel_ToleranceY.Name = "HmiLabel_ToleranceY"
        Me.HmiLabel_ToleranceY.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_ToleranceY.TabIndex = 39
        '
        'HmiTextBox_ToleranceX
        '
        Me.HmiTextBox_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ToleranceX.Location = New System.Drawing.Point(65, 123)
        Me.HmiTextBox_ToleranceX.Name = "HmiTextBox_ToleranceX"
        Me.HmiTextBox_ToleranceX.Number = 0
        Me.HmiTextBox_ToleranceX.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_ToleranceX.TabIndex = 38
        Me.HmiTextBox_ToleranceX.TextBoxReadOnly = False
        Me.HmiTextBox_ToleranceX.ValueType = GetType(String)
        '
        'HmiLabel_ToleranceX
        '
        Me.HmiLabel_ToleranceX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ToleranceX.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_ToleranceX.Name = "HmiLabel_ToleranceX"
        Me.HmiLabel_ToleranceX.Size = New System.Drawing.Size(56, 34)
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
        Me.Label_Z.Location = New System.Drawing.Point(315, 45)
        Me.Label_Z.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Z.Name = "Label_Z"
        Me.Label_Z.Size = New System.Drawing.Size(52, 30)
        Me.Label_Z.TabIndex = 36
        Me.Label_Z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(251, 43)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_Z.TabIndex = 35
        '
        'Label_Y
        '
        Me.Label_Y.AutoSize = True
        Me.Label_Y.BackColor = System.Drawing.Color.LightGray
        Me.Label_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Y.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_Y.ForeColor = System.Drawing.Color.Blue
        Me.Label_Y.Location = New System.Drawing.Point(191, 45)
        Me.Label_Y.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Y.Name = "Label_Y"
        Me.Label_Y.Size = New System.Drawing.Size(52, 30)
        Me.Label_Y.TabIndex = 34
        Me.Label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(127, 43)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_Y.TabIndex = 33
        '
        'Label_X
        '
        Me.Label_X.AutoSize = True
        Me.Label_X.BackColor = System.Drawing.Color.LightGray
        Me.Label_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_X.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_X.ForeColor = System.Drawing.Color.Blue
        Me.Label_X.Location = New System.Drawing.Point(67, 45)
        Me.Label_X.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_X.Name = "Label_X"
        Me.Label_X.Size = New System.Drawing.Size(52, 30)
        Me.Label_X.TabIndex = 32
        Me.Label_X.Text = "1000.00"
        Me.Label_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_X.TabIndex = 31
        '
        'HmiButton_Save
        '
        Me.HmiButton_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Save.Location = New System.Drawing.Point(63, 241)
        Me.HmiButton_Save.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Save.MarginHeight = 6
        Me.HmiButton_Save.Name = "HmiButton_Save"
        Me.HmiButton_Save.Size = New System.Drawing.Size(60, 38)
        Me.HmiButton_Save.TabIndex = 30
        '
        'HmiLabel_Function
        '
        Me.HmiLabel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function.Location = New System.Drawing.Point(3, 243)
        Me.HmiLabel_Function.Name = "HmiLabel_Function"
        Me.HmiLabel_Function.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_Function.TabIndex = 29
        '
        'HmiTextBox_MoveZ
        '
        Me.HmiTextBox_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveZ.Location = New System.Drawing.Point(313, 83)
        Me.HmiTextBox_MoveZ.Name = "HmiTextBox_MoveZ"
        Me.HmiTextBox_MoveZ.Number = 0
        Me.HmiTextBox_MoveZ.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_MoveZ.TabIndex = 28
        Me.HmiTextBox_MoveZ.TextBoxReadOnly = False
        Me.HmiTextBox_MoveZ.ValueType = GetType(String)
        '
        'HmiLabel_MoveZ
        '
        Me.HmiLabel_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveZ.Location = New System.Drawing.Point(251, 83)
        Me.HmiLabel_MoveZ.Name = "HmiLabel_MoveZ"
        Me.HmiLabel_MoveZ.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_MoveZ.TabIndex = 27
        '
        'HmiTextBox_MoveY
        '
        Me.HmiTextBox_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveY.Location = New System.Drawing.Point(189, 83)
        Me.HmiTextBox_MoveY.Name = "HmiTextBox_MoveY"
        Me.HmiTextBox_MoveY.Number = 0
        Me.HmiTextBox_MoveY.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_MoveY.TabIndex = 26
        Me.HmiTextBox_MoveY.TextBoxReadOnly = False
        Me.HmiTextBox_MoveY.ValueType = GetType(String)
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(127, 203)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_Pro.TabIndex = 21
        '
        'HmiLabel_AST
        '
        Me.HmiLabel_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AST.Location = New System.Drawing.Point(3, 203)
        Me.HmiLabel_AST.Name = "HmiLabel_AST"
        Me.HmiLabel_AST.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_AST.TabIndex = 19
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiLabel_MoveX
        '
        Me.HmiLabel_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveX.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_MoveX.Name = "HmiLabel_MoveX"
        Me.HmiLabel_MoveX.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_MoveX.TabIndex = 10
        '
        'HmiComboBox_Variant
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiComboBox_Variant, 2)
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(65, 1)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(118, 38)
        Me.HmiComboBox_Variant.TabIndex = 12
        '
        'HmiButton_Variant
        '
        Me.HmiButton_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Variant.Location = New System.Drawing.Point(187, 1)
        Me.HmiButton_Variant.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Variant.MarginHeight = 6
        Me.HmiButton_Variant.Name = "HmiButton_Variant"
        Me.HmiButton_Variant.Size = New System.Drawing.Size(60, 38)
        Me.HmiButton_Variant.TabIndex = 13
        '
        'HmiTextBox_MoveX
        '
        Me.HmiTextBox_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveX.Location = New System.Drawing.Point(65, 83)
        Me.HmiTextBox_MoveX.Name = "HmiTextBox_MoveX"
        Me.HmiTextBox_MoveX.Number = 0
        Me.HmiTextBox_MoveX.Size = New System.Drawing.Size(56, 34)
        Me.HmiTextBox_MoveX.TabIndex = 14
        Me.HmiTextBox_MoveX.TextBoxReadOnly = False
        Me.HmiTextBox_MoveX.ValueType = GetType(String)
        '
        'HmiLabel_MoveY
        '
        Me.HmiLabel_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveY.Location = New System.Drawing.Point(127, 83)
        Me.HmiLabel_MoveY.Name = "HmiLabel_MoveY"
        Me.HmiLabel_MoveY.Size = New System.Drawing.Size(56, 34)
        Me.HmiLabel_MoveY.TabIndex = 15
        '
        'HmiButton_Teach
        '
        Me.HmiButton_Teach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Teach.Location = New System.Drawing.Point(63, 161)
        Me.HmiButton_Teach.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Teach.MarginHeight = 6
        Me.HmiButton_Teach.Name = "HmiButton_Teach"
        Me.HmiButton_Teach.Size = New System.Drawing.Size(60, 38)
        Me.HmiButton_Teach.TabIndex = 16
        '
        'Panel_Body_Bottom_Left
        '
        Me.Panel_Body_Bottom_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Left.Controls.Add(Me.HmiDataView_Point)
        Me.Panel_Body_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Bottom_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Left.Name = "Panel_Body_Bottom_Left"
        Me.Panel_Body_Bottom_Left.Size = New System.Drawing.Size(249, 504)
        Me.Panel_Body_Bottom_Left.TabIndex = 0
        '
        'HmiDataView_Point
        '
        Me.HmiDataView_Point.AllowUserToAddRows = False
        Me.HmiDataView_Point.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Point.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Point.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Point.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Point.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Point.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.HmiDataView_Point.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HmiDataView_Point.DefaultCellStyle = DataGridViewCellStyle3
        Me.HmiDataView_Point.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Point.EnableHeadersVisualStyles = False
        Me.HmiDataView_Point.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Point.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Point.Name = "HmiDataView_Point"
        Me.HmiDataView_Point.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_Point.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.HmiDataView_Point.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Point.RowTemplate.Height = 40
        Me.HmiDataView_Point.RowTemplate.ReadOnly = True
        Me.HmiDataView_Point.Size = New System.Drawing.Size(247, 502)
        Me.HmiDataView_Point.TabIndex = 0
        '
        'ControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ControlUI"
        Me.Text = "ControlUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.PerformLayout()
        CType(Me.PictureBox_Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Body_Bottom_Left.ResumeLayout(False)
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Bottom_Left As System.Windows.Forms.Panel
    Friend WithEvents HmiDataView_Point As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_MoveX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiButton_Variant As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_MoveX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveY As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Teach As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_AST As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MoveZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Save As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Function As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_Z As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_Y As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_X As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceY As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ToleranceX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ToleranceX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Z As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_SensorZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SensorY As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SensorX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Y As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_X As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents PictureBox_Pic As System.Windows.Forms.PictureBox
    Friend WithEvents HmiTextBox_AST As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Pro As Kochi.HMI.MainControl.UI.HMITextBox
End Class
