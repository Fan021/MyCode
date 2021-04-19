<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CylinderParameter
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
        Me.TabControl_IO = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_SensorB = New System.Windows.Forms.TableLayoutPanel()
        Me.ComboBoxEx_SensorBType = New Kostal.Las.Base.ComboBoxEx()
        Me.ComboBoxEx1_SensorB = New Kostal.Las.Base.ComboBoxEx()
        Me.TableLayoutPanel_SensorA = New System.Windows.Forms.TableLayoutPanel()
        Me.ComboBoxEx_SensorAType = New Kostal.Las.Base.ComboBoxEx()
        Me.ComboBoxEx1_SensorA = New Kostal.Las.Base.ComboBoxEx()
        Me.TextBox_NameB2 = New System.Windows.Forms.TextBox()
        Me.TextBox_NameB = New System.Windows.Forms.TextBox()
        Me.Label_SensorB = New System.Windows.Forms.Label()
        Me.Label_SensorA = New System.Windows.Forms.Label()
        Me.Label_NameB2 = New System.Windows.Forms.Label()
        Me.Label_NameB = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Reserve = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_YA = New System.Windows.Forms.RadioButton()
        Me.RadioButton_NA = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_YB = New System.Windows.Forms.RadioButton()
        Me.RadioButton_NB = New System.Windows.Forms.RadioButton()
        Me.Label_ReserveA = New System.Windows.Forms.Label()
        Me.Label_ReserveB = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_YOne = New System.Windows.Forms.RadioButton()
        Me.RadioButton_NOne = New System.Windows.Forms.RadioButton()
        Me.Label_OneCylinder = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Type = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Tap = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Toggle = New System.Windows.Forms.RadioButton()
        Me.Label_Trigger = New System.Windows.Forms.Label()
        Me.TextBox_NameA2 = New System.Windows.Forms.TextBox()
        Me.Label_NameA2 = New System.Windows.Forms.Label()
        Me.Label_ID = New System.Windows.Forms.Label()
        Me.TextBox_ID = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Label_NameA = New System.Windows.Forms.Label()
        Me.TextBox_NameA = New System.Windows.Forms.TextBox()
        Me.TabControl_IO.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel_SensorB.SuspendLayout()
        Me.TableLayoutPanel_SensorA.SuspendLayout()
        Me.TableLayoutPanel_Reserve.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel_Type.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_IO
        '
        Me.TabControl_IO.Controls.Add(Me.TabPage1)
        Me.TabControl_IO.Controls.Add(Me.TabPage2)
        Me.TabControl_IO.Controls.Add(Me.TabPage3)
        Me.TabControl_IO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_IO.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_IO.Name = "TabControl_IO"
        Me.TabControl_IO.SelectedIndex = 0
        Me.TabControl_IO.Size = New System.Drawing.Size(533, 602)
        Me.TabControl_IO.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_Body)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(525, 576)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.06358!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.93642!))
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 12
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(519, 570)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(525, 576)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(525, 576)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_SensorB
        '
        Me.TableLayoutPanel_SensorB.ColumnCount = 2
        Me.TableLayoutPanel_SensorB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorB.Controls.Add(Me.ComboBoxEx_SensorBType, 0, 0)
        Me.TableLayoutPanel_SensorB.Controls.Add(Me.ComboBoxEx1_SensorB, 1, 0)
        Me.TableLayoutPanel_SensorB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_SensorB.Location = New System.Drawing.Point(223, 400)
        Me.TableLayoutPanel_SensorB.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_SensorB.Name = "TableLayoutPanel_SensorB"
        Me.TableLayoutPanel_SensorB.RowCount = 1
        Me.TableLayoutPanel_SensorB.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorB.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorB.Size = New System.Drawing.Size(296, 40)
        Me.TableLayoutPanel_SensorB.TabIndex = 35
        '
        'ComboBoxEx_SensorBType
        '
        Me.ComboBoxEx_SensorBType.BackColor = System.Drawing.Color.White
        Me.ComboBoxEx_SensorBType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEx_SensorBType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx_SensorBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEx_SensorBType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxEx_SensorBType.FormattingEnabled = True
        Me.ComboBoxEx_SensorBType.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxEx_SensorBType.Name = "ComboBoxEx_SensorBType"
        Me.ComboBoxEx_SensorBType.Size = New System.Drawing.Size(142, 21)
        Me.ComboBoxEx_SensorBType.TabIndex = 0
        '
        'ComboBoxEx1_SensorB
        '
        Me.ComboBoxEx1_SensorB.BackColor = System.Drawing.Color.White
        Me.ComboBoxEx1_SensorB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEx1_SensorB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx1_SensorB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEx1_SensorB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxEx1_SensorB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.ComboBoxEx1_SensorB.FormattingEnabled = True
        Me.ComboBoxEx1_SensorB.Location = New System.Drawing.Point(151, 3)
        Me.ComboBoxEx1_SensorB.Name = "ComboBoxEx1_SensorB"
        Me.ComboBoxEx1_SensorB.Size = New System.Drawing.Size(142, 38)
        Me.ComboBoxEx1_SensorB.TabIndex = 1
        '
        'TableLayoutPanel_SensorA
        '
        Me.TableLayoutPanel_SensorA.ColumnCount = 2
        Me.TableLayoutPanel_SensorA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorA.Controls.Add(Me.ComboBoxEx_SensorAType, 0, 0)
        Me.TableLayoutPanel_SensorA.Controls.Add(Me.ComboBoxEx1_SensorA, 1, 0)
        Me.TableLayoutPanel_SensorA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_SensorA.Location = New System.Drawing.Point(223, 360)
        Me.TableLayoutPanel_SensorA.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_SensorA.Name = "TableLayoutPanel_SensorA"
        Me.TableLayoutPanel_SensorA.RowCount = 1
        Me.TableLayoutPanel_SensorA.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorA.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_SensorA.Size = New System.Drawing.Size(296, 40)
        Me.TableLayoutPanel_SensorA.TabIndex = 34
        '
        'ComboBoxEx_SensorAType
        '
        Me.ComboBoxEx_SensorAType.BackColor = System.Drawing.Color.White
        Me.ComboBoxEx_SensorAType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEx_SensorAType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx_SensorAType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEx_SensorAType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxEx_SensorAType.FormattingEnabled = True
        Me.ComboBoxEx_SensorAType.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxEx_SensorAType.Name = "ComboBoxEx_SensorAType"
        Me.ComboBoxEx_SensorAType.Size = New System.Drawing.Size(142, 21)
        Me.ComboBoxEx_SensorAType.TabIndex = 0
        '
        'ComboBoxEx1_SensorA
        '
        Me.ComboBoxEx1_SensorA.BackColor = System.Drawing.Color.White
        Me.ComboBoxEx1_SensorA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEx1_SensorA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx1_SensorA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEx1_SensorA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxEx1_SensorA.FormattingEnabled = True
        Me.ComboBoxEx1_SensorA.Location = New System.Drawing.Point(151, 3)
        Me.ComboBoxEx1_SensorA.Name = "ComboBoxEx1_SensorA"
        Me.ComboBoxEx1_SensorA.Size = New System.Drawing.Size(142, 21)
        Me.ComboBoxEx1_SensorA.TabIndex = 1
        '
        'TextBox_NameB2
        '
        Me.TextBox_NameB2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameB2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameB2.Location = New System.Drawing.Point(226, 163)
        Me.TextBox_NameB2.Name = "TextBox_NameB2"
        Me.TextBox_NameB2.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameB2.TabIndex = 33
        '
        'TextBox_NameB
        '
        Me.TextBox_NameB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameB.Location = New System.Drawing.Point(226, 123)
        Me.TextBox_NameB.Name = "TextBox_NameB"
        Me.TextBox_NameB.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameB.TabIndex = 32
        '
        'Label_SensorB
        '
        Me.Label_SensorB.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_SensorB.AutoSize = True
        Me.Label_SensorB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_SensorB.Location = New System.Drawing.Point(61, 405)
        Me.Label_SensorB.Name = "Label_SensorB"
        Me.Label_SensorB.Size = New System.Drawing.Size(101, 29)
        Me.Label_SensorB.TabIndex = 31
        Me.Label_SensorB.Text = "SensorB:"
        Me.Label_SensorB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_SensorA
        '
        Me.Label_SensorA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_SensorA.AutoSize = True
        Me.Label_SensorA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_SensorA.Location = New System.Drawing.Point(60, 365)
        Me.Label_SensorA.Name = "Label_SensorA"
        Me.Label_SensorA.Size = New System.Drawing.Size(103, 29)
        Me.Label_SensorA.TabIndex = 30
        Me.Label_SensorA.Text = "SensorA:"
        Me.Label_SensorA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_NameB2
        '
        Me.Label_NameB2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameB2.AutoSize = True
        Me.Label_NameB2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameB2.Location = New System.Drawing.Point(74, 165)
        Me.Label_NameB2.Name = "Label_NameB2"
        Me.Label_NameB2.Size = New System.Drawing.Size(74, 29)
        Me.Label_NameB2.TabIndex = 29
        Me.Label_NameB2.Text = "TextB:"
        Me.Label_NameB2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_NameB
        '
        Me.Label_NameB.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameB.AutoSize = True
        Me.Label_NameB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameB.Location = New System.Drawing.Point(74, 125)
        Me.Label_NameB.Name = "Label_NameB"
        Me.Label_NameB.Size = New System.Drawing.Size(74, 29)
        Me.Label_NameB.TabIndex = 28
        Me.Label_NameB.Text = "TextB:"
        Me.Label_NameB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Reserve
        '
        Me.TableLayoutPanel_Reserve.ColumnCount = 2
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_YA, 0, 0)
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_NA, 1, 0)
        Me.TableLayoutPanel_Reserve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Reserve.Location = New System.Drawing.Point(224, 241)
        Me.TableLayoutPanel_Reserve.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Reserve.Name = "TableLayoutPanel_Reserve"
        Me.TableLayoutPanel_Reserve.RowCount = 1
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel_Reserve.TabIndex = 27
        '
        'RadioButton_YA
        '
        Me.RadioButton_YA.AutoSize = True
        Me.RadioButton_YA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_YA.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_YA.Name = "RadioButton_YA"
        Me.RadioButton_YA.Size = New System.Drawing.Size(43, 32)
        Me.RadioButton_YA.TabIndex = 0
        Me.RadioButton_YA.TabStop = True
        Me.RadioButton_YA.Text = "Y"
        Me.RadioButton_YA.UseVisualStyleBackColor = True
        '
        'RadioButton_NA
        '
        Me.RadioButton_NA.AutoSize = True
        Me.RadioButton_NA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_NA.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_NA.Name = "RadioButton_NA"
        Me.RadioButton_NA.Size = New System.Drawing.Size(47, 32)
        Me.RadioButton_NA.TabIndex = 1
        Me.RadioButton_NA.TabStop = True
        Me.RadioButton_NA.Text = "N"
        Me.RadioButton_NA.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RadioButton_YB, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RadioButton_NB, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(224, 281)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel1.TabIndex = 37
        '
        'RadioButton_YB
        '
        Me.RadioButton_YB.AutoSize = True
        Me.RadioButton_YB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_YB.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_YB.Name = "RadioButton_YB"
        Me.RadioButton_YB.Size = New System.Drawing.Size(43, 32)
        Me.RadioButton_YB.TabIndex = 0
        Me.RadioButton_YB.TabStop = True
        Me.RadioButton_YB.Text = "Y"
        Me.RadioButton_YB.UseVisualStyleBackColor = True
        '
        'RadioButton_NB
        '
        Me.RadioButton_NB.AutoSize = True
        Me.RadioButton_NB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_NB.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_NB.Name = "RadioButton_NB"
        Me.RadioButton_NB.Size = New System.Drawing.Size(47, 32)
        Me.RadioButton_NB.TabIndex = 1
        Me.RadioButton_NB.TabStop = True
        Me.RadioButton_NB.Text = "N"
        Me.RadioButton_NB.UseVisualStyleBackColor = True
        '
        'Label_ReserveA
        '
        Me.Label_ReserveA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ReserveA.AutoSize = True
        Me.Label_ReserveA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ReserveA.Location = New System.Drawing.Point(54, 245)
        Me.Label_ReserveA.Name = "Label_ReserveA"
        Me.Label_ReserveA.Size = New System.Drawing.Size(115, 29)
        Me.Label_ReserveA.TabIndex = 26
        Me.Label_ReserveA.Text = "ReserveA:"
        Me.Label_ReserveA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_ReserveB
        '
        Me.Label_ReserveB.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ReserveB.AutoSize = True
        Me.Label_ReserveB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ReserveB.Location = New System.Drawing.Point(55, 285)
        Me.Label_ReserveB.Name = "Label_ReserveB"
        Me.Label_ReserveB.Size = New System.Drawing.Size(113, 29)
        Me.Label_ReserveB.TabIndex = 36
        Me.Label_ReserveB.Text = "ReserveB:"
        Me.Label_ReserveB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.RadioButton_YOne, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.RadioButton_NOne, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(224, 321)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel2.TabIndex = 39
        '
        'RadioButton_YOne
        '
        Me.RadioButton_YOne.AutoSize = True
        Me.RadioButton_YOne.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_YOne.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_YOne.Name = "RadioButton_YOne"
        Me.RadioButton_YOne.Size = New System.Drawing.Size(43, 32)
        Me.RadioButton_YOne.TabIndex = 0
        Me.RadioButton_YOne.TabStop = True
        Me.RadioButton_YOne.Text = "Y"
        Me.RadioButton_YOne.UseVisualStyleBackColor = True
        '
        'RadioButton_NOne
        '
        Me.RadioButton_NOne.AutoSize = True
        Me.RadioButton_NOne.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_NOne.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_NOne.Name = "RadioButton_NOne"
        Me.RadioButton_NOne.Size = New System.Drawing.Size(47, 32)
        Me.RadioButton_NOne.TabIndex = 1
        Me.RadioButton_NOne.TabStop = True
        Me.RadioButton_NOne.Text = "N"
        Me.RadioButton_NOne.UseVisualStyleBackColor = True
        '
        'Label_OneCylinder
        '
        Me.Label_OneCylinder.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_OneCylinder.AutoSize = True
        Me.Label_OneCylinder.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_OneCylinder.Location = New System.Drawing.Point(39, 325)
        Me.Label_OneCylinder.Name = "Label_OneCylinder"
        Me.Label_OneCylinder.Size = New System.Drawing.Size(144, 29)
        Me.Label_OneCylinder.TabIndex = 38
        Me.Label_OneCylinder.Text = "OneCylinder:"
        Me.Label_OneCylinder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Type
        '
        Me.TableLayoutPanel_Type.ColumnCount = 2
        Me.TableLayoutPanel_Type.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.Controls.Add(Me.RadioButton_Tap, 1, 0)
        Me.TableLayoutPanel_Type.Controls.Add(Me.RadioButton_Toggle, 0, 0)
        Me.TableLayoutPanel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Type.Location = New System.Drawing.Point(224, 201)
        Me.TableLayoutPanel_Type.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Type.Name = "TableLayoutPanel_Type"
        Me.TableLayoutPanel_Type.RowCount = 1
        Me.TableLayoutPanel_Type.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel_Type.TabIndex = 25
        '
        'RadioButton_Tap
        '
        Me.RadioButton_Tap.AutoSize = True
        Me.RadioButton_Tap.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Tap.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_Tap.Name = "RadioButton_Tap"
        Me.RadioButton_Tap.Size = New System.Drawing.Size(66, 32)
        Me.RadioButton_Tap.TabIndex = 1
        Me.RadioButton_Tap.TabStop = True
        Me.RadioButton_Tap.Text = "Tap"
        Me.RadioButton_Tap.UseVisualStyleBackColor = True
        '
        'RadioButton_Toggle
        '
        Me.RadioButton_Toggle.AutoSize = True
        Me.RadioButton_Toggle.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Toggle.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Toggle.Name = "RadioButton_Toggle"
        Me.RadioButton_Toggle.Size = New System.Drawing.Size(94, 32)
        Me.RadioButton_Toggle.TabIndex = 0
        Me.RadioButton_Toggle.TabStop = True
        Me.RadioButton_Toggle.Text = "Toggle"
        Me.RadioButton_Toggle.UseVisualStyleBackColor = True
        '
        'Label_Trigger
        '
        Me.Label_Trigger.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Trigger.AutoSize = True
        Me.Label_Trigger.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Trigger.Location = New System.Drawing.Point(67, 205)
        Me.Label_Trigger.Name = "Label_Trigger"
        Me.Label_Trigger.Size = New System.Drawing.Size(89, 29)
        Me.Label_Trigger.TabIndex = 24
        Me.Label_Trigger.Text = "Trigger:"
        Me.Label_Trigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_NameA2
        '
        Me.TextBox_NameA2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameA2.Location = New System.Drawing.Point(226, 83)
        Me.TextBox_NameA2.Name = "TextBox_NameA2"
        Me.TextBox_NameA2.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameA2.TabIndex = 23
        '
        'Label_NameA2
        '
        Me.Label_NameA2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA2.AutoSize = True
        Me.Label_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA2.Location = New System.Drawing.Point(73, 85)
        Me.Label_NameA2.Name = "Label_NameA2"
        Me.Label_NameA2.Size = New System.Drawing.Size(76, 29)
        Me.Label_NameA2.TabIndex = 22
        Me.Label_NameA2.Text = "TextA:"
        Me.Label_NameA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_ID
        '
        Me.Label_ID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ID.AutoSize = True
        Me.Label_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ID.Location = New System.Drawing.Point(86, 5)
        Me.Label_ID.Name = "Label_ID"
        Me.Label_ID.Size = New System.Drawing.Size(51, 29)
        Me.Label_ID.TabIndex = 9
        Me.Label_ID.Text = "  ID:"
        Me.Label_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_ID
        '
        Me.TextBox_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_ID.Location = New System.Drawing.Point(226, 3)
        Me.TextBox_ID.Name = "TextBox_ID"
        Me.TextBox_ID.ReadOnly = True
        Me.TextBox_ID.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_ID.TabIndex = 8
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Save, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Cancel, 1, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(226, 443)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(290, 78)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 11
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Save.Location = New System.Drawing.Point(3, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(139, 72)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Cancel.Location = New System.Drawing.Point(148, 3)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(139, 72)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label_NameA
        '
        Me.Label_NameA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA.AutoSize = True
        Me.Label_NameA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA.Location = New System.Drawing.Point(73, 45)
        Me.Label_NameA.Name = "Label_NameA"
        Me.Label_NameA.Size = New System.Drawing.Size(76, 29)
        Me.Label_NameA.TabIndex = 12
        Me.Label_NameA.Text = "TextA:"
        Me.Label_NameA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_NameA
        '
        Me.TextBox_NameA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameA.Location = New System.Drawing.Point(226, 43)
        Me.TextBox_NameA.Name = "TextBox_NameA"
        Me.TextBox_NameA.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameA.TabIndex = 13
        '
        'CylinderParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 602)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl_IO)
        Me.Name = "CylinderParameter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CylinderParameter"
        Me.TabControl_IO.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel_SensorB.ResumeLayout(False)
        Me.TableLayoutPanel_SensorA.ResumeLayout(False)
        Me.TableLayoutPanel_Reserve.ResumeLayout(False)
        Me.TableLayoutPanel_Reserve.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel_Type.ResumeLayout(False)
        Me.TableLayoutPanel_Type.PerformLayout()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl_IO As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body As HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_YOne As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_NOne As System.Windows.Forms.RadioButton
    Friend WithEvents Label_OneCylinder As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_SensorB As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxEx_SensorBType As ComboBoxEx
    Friend WithEvents ComboBoxEx1_SensorB As ComboBoxEx
    Friend WithEvents TableLayoutPanel_SensorA As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxEx_SensorAType As ComboBoxEx
    Friend WithEvents ComboBoxEx1_SensorA As ComboBoxEx
    Friend WithEvents TextBox_NameB2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_NameB As System.Windows.Forms.TextBox
    Friend WithEvents Label_SensorB As System.Windows.Forms.Label
    Friend WithEvents Label_SensorA As System.Windows.Forms.Label
    Friend WithEvents Label_NameB2 As System.Windows.Forms.Label
    Friend WithEvents Label_NameB As System.Windows.Forms.Label
    Friend WithEvents Label_ReserveA As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Reserve As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_YA As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_NA As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_YB As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_NB As System.Windows.Forms.RadioButton
    Friend WithEvents Label_ReserveB As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Type As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Tap As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Toggle As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Trigger As System.Windows.Forms.Label
    Friend WithEvents TextBox_NameA2 As System.Windows.Forms.TextBox
    Friend WithEvents Label_NameA2 As System.Windows.Forms.Label
    Friend WithEvents Label_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_ID As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label_NameA As System.Windows.Forms.Label
    Friend WithEvents TextBox_NameA As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
End Class
