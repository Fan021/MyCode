<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlUI
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Top_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonYDec = New System.Windows.Forms.Button()
        Me.ButtonXAdd = New System.Windows.Forms.Button()
        Me.ButtonXDec = New System.Windows.Forms.Button()
        Me.ButtonYAdd = New System.Windows.Forms.Button()
        Me.ButtonZAdd = New System.Windows.Forms.Button()
        Me.ButtonZDec = New System.Windows.Forms.Button()
        Me.Panel_Body_Top_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.Panel_Body_Bottom_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Speed = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Y = New System.Windows.Forms.Label()
        Me.Label_Z = New System.Windows.Forms.Label()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.TrackBar_Speed = New System.Windows.Forms.TrackBar()
        Me.HmiTextBox_Speed = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiButton_Save = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Function = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Screw = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Pro = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_AST = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_AST = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_MoveX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiButton_Variant = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_MoveX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveY = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Teach = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Move = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDataView_Point = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.Panel_Body_Top_Left.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Left.SuspendLayout()
        Me.Panel_Body_Top_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
        Me.Panel_Body_Bottom_Left.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Right.SuspendLayout()
        CType(Me.TrackBar_Speed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Top, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 2
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body_Top_Left, 0, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body_Top_Right, 1, 0)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel_Body_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(623, 132)
        Me.TableLayoutPanel_Body_Top.TabIndex = 0
        '
        'Panel_Body_Top_Left
        '
        Me.Panel_Body_Top_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Top_Left.Controls.Add(Me.TableLayoutPanel_Body_Top_Left)
        Me.Panel_Body_Top_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Top_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Top_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Top_Left.Name = "Panel_Body_Top_Left"
        Me.Panel_Body_Top_Left.Size = New System.Drawing.Size(249, 132)
        Me.Panel_Body_Top_Left.TabIndex = 1
        '
        'TableLayoutPanel_Body_Top_Left
        '
        Me.TableLayoutPanel_Body_Top_Left.ColumnCount = 6
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonYDec, 2, 0)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonXAdd, 1, 1)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonXDec, 3, 1)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonYAdd, 2, 2)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonZAdd, 4, 0)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonZDec, 4, 2)
        Me.TableLayoutPanel_Body_Top_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Left.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top_Left.Name = "TableLayoutPanel_Body_Top_Left"
        Me.TableLayoutPanel_Body_Top_Left.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.Size = New System.Drawing.Size(247, 130)
        Me.TableLayoutPanel_Body_Top_Left.TabIndex = 1
        '
        'ButtonYDec
        '
        Me.ButtonYDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonYDec.Location = New System.Drawing.Point(78, 3)
        Me.ButtonYDec.Name = "ButtonYDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonYDec, 2)
        Me.ButtonYDec.Size = New System.Drawing.Size(41, 58)
        Me.ButtonYDec.TabIndex = 0
        Me.ButtonYDec.Text = "Y-"
        Me.ButtonYDec.UseVisualStyleBackColor = True
        '
        'ButtonXAdd
        '
        Me.ButtonXAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonXAdd.Location = New System.Drawing.Point(31, 35)
        Me.ButtonXAdd.Name = "ButtonXAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonXAdd, 2)
        Me.ButtonXAdd.Size = New System.Drawing.Size(41, 58)
        Me.ButtonXAdd.TabIndex = 1
        Me.ButtonXAdd.Text = "X+"
        Me.ButtonXAdd.UseVisualStyleBackColor = True
        '
        'ButtonXDec
        '
        Me.ButtonXDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonXDec.Location = New System.Drawing.Point(125, 35)
        Me.ButtonXDec.Name = "ButtonXDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonXDec, 2)
        Me.ButtonXDec.Size = New System.Drawing.Size(41, 58)
        Me.ButtonXDec.TabIndex = 3
        Me.ButtonXDec.Text = "X-"
        Me.ButtonXDec.UseVisualStyleBackColor = True
        '
        'ButtonYAdd
        '
        Me.ButtonYAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonYAdd.Location = New System.Drawing.Point(78, 67)
        Me.ButtonYAdd.Name = "ButtonYAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonYAdd, 2)
        Me.ButtonYAdd.Size = New System.Drawing.Size(41, 60)
        Me.ButtonYAdd.TabIndex = 2
        Me.ButtonYAdd.Text = "Y+"
        Me.ButtonYAdd.UseVisualStyleBackColor = True
        '
        'ButtonZAdd
        '
        Me.ButtonZAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonZAdd.Location = New System.Drawing.Point(172, 3)
        Me.ButtonZAdd.Name = "ButtonZAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonZAdd, 2)
        Me.ButtonZAdd.Size = New System.Drawing.Size(41, 58)
        Me.ButtonZAdd.TabIndex = 4
        Me.ButtonZAdd.Text = "Z+"
        Me.ButtonZAdd.UseVisualStyleBackColor = True
        '
        'ButtonZDec
        '
        Me.ButtonZDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonZDec.Location = New System.Drawing.Point(172, 67)
        Me.ButtonZDec.Name = "ButtonZDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonZDec, 2)
        Me.ButtonZDec.Size = New System.Drawing.Size(41, 60)
        Me.ButtonZDec.TabIndex = 5
        Me.ButtonZDec.Text = "Z-"
        Me.ButtonZDec.UseVisualStyleBackColor = True
        '
        'Panel_Body_Top_Right
        '
        Me.Panel_Body_Top_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Top_Right.Location = New System.Drawing.Point(249, 0)
        Me.Panel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Top_Right.Name = "Panel_Body_Top_Right"
        Me.Panel_Body_Top_Right.Size = New System.Drawing.Size(374, 132)
        Me.Panel_Body_Top_Right.TabIndex = 2
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Left, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 158)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(623, 372)
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
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(374, 372)
        Me.Panel_Body_Bottom_Right.TabIndex = 3
        '
        'Panel_Body_Bottom_Left
        '
        Me.Panel_Body_Bottom_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Left.Controls.Add(Me.HmiDataView_Point)
        Me.Panel_Body_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Bottom_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Left.Name = "Panel_Body_Bottom_Left"
        Me.Panel_Body_Bottom_Left.Size = New System.Drawing.Size(249, 372)
        Me.Panel_Body_Bottom_Left.TabIndex = 0
        '
        'TableLayoutPanel_Body_Top_Right
        '
        Me.TableLayoutPanel_Body_Top_Right.ColumnCount = 6
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Z, 4, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Y, 2, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Step, 0, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_X, 0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Speed, 0, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Y, 3, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Z, 5, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_X, 1, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton1, 1, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton2, 2, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton3, 3, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton4, 4, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.TrackBar_Speed, 1, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Speed, 4, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label1, 5, 2)
        Me.TableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Top_Right.Name = "TableLayoutPanel_Body_Top_Right"
        Me.TableLayoutPanel_Body_Top_Right.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(372, 130)
        Me.TableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(247, 3)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(55, 26)
        Me.HmiLabel_Z.TabIndex = 12
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(125, 3)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(55, 26)
        Me.HmiLabel_Y.TabIndex = 11
        '
        'HmiLabel_Step
        '
        Me.HmiLabel_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step.Location = New System.Drawing.Point(3, 35)
        Me.HmiLabel_Step.Name = "HmiLabel_Step"
        Me.HmiLabel_Step.Size = New System.Drawing.Size(55, 26)
        Me.HmiLabel_Step.TabIndex = 10
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(55, 26)
        Me.HmiLabel_X.TabIndex = 9
        '
        'HmiLabel_Speed
        '
        Me.HmiLabel_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Speed.Location = New System.Drawing.Point(3, 67)
        Me.HmiLabel_Speed.Name = "HmiLabel_Speed"
        Me.HmiLabel_Speed.Size = New System.Drawing.Size(55, 26)
        Me.HmiLabel_Speed.TabIndex = 8
        '
        'Label_Y
        '
        Me.Label_Y.AutoSize = True
        Me.Label_Y.BackColor = System.Drawing.Color.LightGray
        Me.Label_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Y.ForeColor = System.Drawing.Color.Blue
        Me.Label_Y.Location = New System.Drawing.Point(188, 5)
        Me.Label_Y.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Y.Name = "Label_Y"
        Me.Label_Y.Size = New System.Drawing.Size(51, 22)
        Me.Label_Y.TabIndex = 7
        Me.Label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Z
        '
        Me.Label_Z.AutoSize = True
        Me.Label_Z.BackColor = System.Drawing.Color.LightGray
        Me.Label_Z.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Z.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Z.ForeColor = System.Drawing.Color.Blue
        Me.Label_Z.Location = New System.Drawing.Point(310, 5)
        Me.Label_Z.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Z.Name = "Label_Z"
        Me.Label_Z.Size = New System.Drawing.Size(57, 22)
        Me.Label_Z.TabIndex = 6
        Me.Label_Z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_X
        '
        Me.Label_X.AutoSize = True
        Me.Label_X.BackColor = System.Drawing.Color.LightGray
        Me.Label_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_X.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_X.ForeColor = System.Drawing.Color.Blue
        Me.Label_X.Location = New System.Drawing.Point(66, 5)
        Me.Label_X.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_X.Name = "Label_X"
        Me.Label_X.Size = New System.Drawing.Size(51, 22)
        Me.Label_X.TabIndex = 5
        Me.Label_X.Text = "1000.00"
        Me.Label_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RadioButton1.Location = New System.Drawing.Point(64, 33)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(57, 30)
        Me.RadioButton1.TabIndex = 13
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "0.01"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton2.Location = New System.Drawing.Point(125, 33)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(57, 30)
        Me.RadioButton2.TabIndex = 14
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "1.00"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton3.Location = New System.Drawing.Point(184, 33)
        Me.RadioButton3.Margin = New System.Windows.Forms.Padding(1)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(59, 30)
        Me.RadioButton3.TabIndex = 15
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "10.00"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.RadioButton4, 2)
        Me.RadioButton4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton4.Location = New System.Drawing.Point(247, 33)
        Me.RadioButton4.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(124, 30)
        Me.RadioButton4.TabIndex = 16
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Continue"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'TrackBar_Speed
        '
        Me.TrackBar_Speed.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TrackBar_Speed, 3)
        Me.TrackBar_Speed.Location = New System.Drawing.Point(64, 65)
        Me.TrackBar_Speed.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.TrackBar_Speed.Maximum = 100
        Me.TrackBar_Speed.Name = "TrackBar_Speed"
        Me.TrackBar_Speed.Size = New System.Drawing.Size(179, 30)
        Me.TrackBar_Speed.TabIndex = 17
        '
        'HmiTextBox_Speed
        '
        Me.HmiTextBox_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Speed.Location = New System.Drawing.Point(245, 65)
        Me.HmiTextBox_Speed.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_Speed.Name = "HmiTextBox_Speed"
        Me.HmiTextBox_Speed.Size = New System.Drawing.Size(59, 30)
        Me.HmiTextBox_Speed.TabIndex = 18
        Me.HmiTextBox_Speed.TextBoxReadOnly = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(306, 65)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 30)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "%"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Save, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Function, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveZ, 5, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveZ, 4, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveY, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Screw, 4, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_Pro, 3, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_AST, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_AST, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveX, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_Variant, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Variant, 3, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveX, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveY, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Teach, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Move, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Modify, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(372, 370)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiButton_Save
        '
        Me.HmiButton_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Save.Location = New System.Drawing.Point(62, 161)
        Me.HmiButton_Save.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Save.MarginHeight = 6
        Me.HmiButton_Save.Name = "HmiButton_Save"
        Me.HmiButton_Save.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Save.TabIndex = 30
        '
        'HmiLabel_Function
        '
        Me.HmiLabel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function.Location = New System.Drawing.Point(3, 163)
        Me.HmiLabel_Function.Name = "HmiLabel_Function"
        Me.HmiLabel_Function.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_Function.TabIndex = 29
        '
        'HmiTextBox_MoveZ
        '
        Me.HmiTextBox_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveZ.Location = New System.Drawing.Point(308, 43)
        Me.HmiTextBox_MoveZ.Name = "HmiTextBox_MoveZ"
        Me.HmiTextBox_MoveZ.Size = New System.Drawing.Size(61, 34)
        Me.HmiTextBox_MoveZ.TabIndex = 28
        Me.HmiTextBox_MoveZ.TextBoxReadOnly = False
        '
        'HmiLabel_MoveZ
        '
        Me.HmiLabel_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveZ.Location = New System.Drawing.Point(247, 43)
        Me.HmiLabel_MoveZ.Name = "HmiLabel_MoveZ"
        Me.HmiLabel_MoveZ.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_MoveZ.TabIndex = 27
        '
        'HmiTextBox_MoveY
        '
        Me.HmiTextBox_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveY.Location = New System.Drawing.Point(186, 43)
        Me.HmiTextBox_MoveY.Name = "HmiTextBox_MoveY"
        Me.HmiTextBox_MoveY.Size = New System.Drawing.Size(55, 34)
        Me.HmiTextBox_MoveY.TabIndex = 26
        Me.HmiTextBox_MoveY.TextBoxReadOnly = False
        '
        'HmiButton_Screw
        '
        Me.HmiButton_Screw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Screw.Location = New System.Drawing.Point(245, 121)
        Me.HmiButton_Screw.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Screw.MarginHeight = 6
        Me.HmiButton_Screw.Name = "HmiButton_Screw"
        Me.HmiButton_Screw.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Screw.TabIndex = 23
        '
        'HmiComboBox_Pro
        '
        Me.HmiComboBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Pro.Location = New System.Drawing.Point(186, 121)
        Me.HmiComboBox_Pro.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_Pro.Name = "HmiComboBox_Pro"
        Me.HmiComboBox_Pro.Size = New System.Drawing.Size(55, 38)
        Me.HmiComboBox_Pro.TabIndex = 22
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(125, 123)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_Pro.TabIndex = 21
        '
        'HmiComboBox_AST
        '
        Me.HmiComboBox_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_AST.Location = New System.Drawing.Point(64, 121)
        Me.HmiComboBox_AST.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_AST.Name = "HmiComboBox_AST"
        Me.HmiComboBox_AST.Size = New System.Drawing.Size(55, 38)
        Me.HmiComboBox_AST.TabIndex = 20
        '
        'HmiLabel_AST
        '
        Me.HmiLabel_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AST.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_AST.Name = "HmiLabel_AST"
        Me.HmiLabel_AST.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_AST.TabIndex = 19
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiLabel_MoveX
        '
        Me.HmiLabel_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveX.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_MoveX.Name = "HmiLabel_MoveX"
        Me.HmiLabel_MoveX.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_MoveX.TabIndex = 10
        '
        'HmiComboBox_Variant
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiComboBox_Variant, 2)
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(64, 1)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(116, 38)
        Me.HmiComboBox_Variant.TabIndex = 12
        '
        'HmiButton_Variant
        '
        Me.HmiButton_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Variant.Location = New System.Drawing.Point(184, 1)
        Me.HmiButton_Variant.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Variant.MarginHeight = 6
        Me.HmiButton_Variant.Name = "HmiButton_Variant"
        Me.HmiButton_Variant.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Variant.TabIndex = 13
        '
        'HmiTextBox_MoveX
        '
        Me.HmiTextBox_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveX.Location = New System.Drawing.Point(64, 43)
        Me.HmiTextBox_MoveX.Name = "HmiTextBox_MoveX"
        Me.HmiTextBox_MoveX.Size = New System.Drawing.Size(55, 34)
        Me.HmiTextBox_MoveX.TabIndex = 14
        Me.HmiTextBox_MoveX.TextBoxReadOnly = False
        '
        'HmiLabel_MoveY
        '
        Me.HmiLabel_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveY.Location = New System.Drawing.Point(125, 43)
        Me.HmiLabel_MoveY.Name = "HmiLabel_MoveY"
        Me.HmiLabel_MoveY.Size = New System.Drawing.Size(55, 34)
        Me.HmiLabel_MoveY.TabIndex = 15
        '
        'HmiButton_Teach
        '
        Me.HmiButton_Teach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Teach.Location = New System.Drawing.Point(62, 81)
        Me.HmiButton_Teach.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Teach.MarginHeight = 6
        Me.HmiButton_Teach.Name = "HmiButton_Teach"
        Me.HmiButton_Teach.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Teach.TabIndex = 16
        '
        'HmiButton_Move
        '
        Me.HmiButton_Move.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Move.Location = New System.Drawing.Point(123, 81)
        Me.HmiButton_Move.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Move.MarginHeight = 6
        Me.HmiButton_Move.Name = "HmiButton_Move"
        Me.HmiButton_Move.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Move.TabIndex = 17
        '
        'HmiButton_Modify
        '
        Me.HmiButton_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Modify.Location = New System.Drawing.Point(184, 81)
        Me.HmiButton_Modify.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Modify.MarginHeight = 6
        Me.HmiButton_Modify.Name = "HmiButton_Modify"
        Me.HmiButton_Modify.Size = New System.Drawing.Size(59, 38)
        Me.HmiButton_Modify.TabIndex = 18
        '
        'HmiDataView_Point
        '
        Me.HmiDataView_Point.AllowUserToAddRows = False
        Me.HmiDataView_Point.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Point.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
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
        Me.HmiDataView_Point.Size = New System.Drawing.Size(247, 370)
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
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.Panel_Body_Top_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Left.ResumeLayout(False)
        Me.Panel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.Panel_Body_Bottom_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.PerformLayout()
        CType(Me.TrackBar_Speed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Top_Left As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonYDec As System.Windows.Forms.Button
    Friend WithEvents ButtonXAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonXDec As System.Windows.Forms.Button
    Friend WithEvents ButtonYAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonZAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonZDec As System.Windows.Forms.Button
    Friend WithEvents Panel_Body_Top_Right As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_Y As System.Windows.Forms.Label
    Friend WithEvents Label_Z As System.Windows.Forms.Label
    Friend WithEvents Label_X As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Speed As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents TrackBar_Speed As System.Windows.Forms.TrackBar
    Friend WithEvents HmiTextBox_Speed As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents HmiButton_Move As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Screw As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiComboBox_Pro As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_AST As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_AST As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MoveZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Save As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Function As Kochi.HMI.MainControl.UI.HMILabel
End Class
