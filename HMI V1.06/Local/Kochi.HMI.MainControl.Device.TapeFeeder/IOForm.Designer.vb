<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOForm
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Top_input = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Body_Top = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.InputIO17 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO16 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO15 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO10 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO14 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO13 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO11 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO20 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO12 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO19 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO9 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO8 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO18 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO7 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO1 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO2 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO3 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO4 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO5 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO6 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.GroupBox_Mid_Output = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.OutputIO1 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO2 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO3 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO4 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO5 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.HmiTableLayoutPanel_Bottom = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_OffDelay = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_OffDelay = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_indexDelay = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_indexDelay = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Top_input.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top.SuspendLayout()
        Me.GroupBox_Mid_Output.SuspendLayout()
        Me.HmiTableLayoutPanel_Mid.SuspendLayout()
        Me.HmiTableLayoutPanel_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 574)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTableLayoutPanel_Bottom, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Top_input, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Mid_Output, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(498, 574)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'GroupBox_Top_input
        '
        Me.GroupBox_Top_input.Controls.Add(Me.HmiTableLayoutPanel_Body_Top)
        Me.GroupBox_Top_input.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Top_input.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Top_input.Name = "GroupBox_Top_input"
        Me.GroupBox_Top_input.Size = New System.Drawing.Size(492, 395)
        Me.GroupBox_Top_input.TabIndex = 2
        Me.GroupBox_Top_input.TabStop = False
        Me.GroupBox_Top_input.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Body_Top
        '
        Me.HmiTableLayoutPanel_Body_Top.ColumnCount = 3
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO17, 2, 6)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO16, 2, 5)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO15, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO10, 0, 9)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO14, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO13, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO11, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO20, 2, 9)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO12, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO19, 2, 8)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO9, 0, 8)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO8, 0, 7)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO18, 2, 7)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO7, 0, 6)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO1, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO2, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO3, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO4, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO5, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO6, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Body_Top.Name = "HmiTableLayoutPanel_Body_Top"
        Me.HmiTableLayoutPanel_Body_Top.RowCount = 10
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.Size = New System.Drawing.Size(486, 376)
        Me.HmiTableLayoutPanel_Body_Top.TabIndex = 1
        '
        'InputIO17
        '
        Me.InputIO17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO17.Location = New System.Drawing.Point(294, 225)
        Me.InputIO17.Name = "InputIO17"
        Me.InputIO17.Size = New System.Drawing.Size(189, 31)
        Me.InputIO17.TabIndex = 31
        '
        'InputIO16
        '
        Me.InputIO16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO16.Location = New System.Drawing.Point(294, 188)
        Me.InputIO16.Name = "InputIO16"
        Me.InputIO16.Size = New System.Drawing.Size(189, 31)
        Me.InputIO16.TabIndex = 30
        '
        'InputIO15
        '
        Me.InputIO15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO15.Location = New System.Drawing.Point(294, 151)
        Me.InputIO15.Name = "InputIO15"
        Me.InputIO15.Size = New System.Drawing.Size(189, 31)
        Me.InputIO15.TabIndex = 29
        '
        'InputIO10
        '
        Me.InputIO10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO10.Location = New System.Drawing.Point(3, 336)
        Me.InputIO10.Name = "InputIO10"
        Me.InputIO10.Size = New System.Drawing.Size(188, 37)
        Me.InputIO10.TabIndex = 28
        '
        'InputIO14
        '
        Me.InputIO14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO14.Location = New System.Drawing.Point(294, 114)
        Me.InputIO14.Name = "InputIO14"
        Me.InputIO14.Size = New System.Drawing.Size(189, 31)
        Me.InputIO14.TabIndex = 27
        '
        'InputIO13
        '
        Me.InputIO13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO13.Location = New System.Drawing.Point(294, 77)
        Me.InputIO13.Name = "InputIO13"
        Me.InputIO13.Size = New System.Drawing.Size(189, 31)
        Me.InputIO13.TabIndex = 26
        '
        'InputIO11
        '
        Me.InputIO11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO11.Location = New System.Drawing.Point(294, 3)
        Me.InputIO11.Name = "InputIO11"
        Me.InputIO11.Size = New System.Drawing.Size(189, 31)
        Me.InputIO11.TabIndex = 25
        '
        'InputIO20
        '
        Me.InputIO20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO20.Location = New System.Drawing.Point(294, 336)
        Me.InputIO20.Name = "InputIO20"
        Me.InputIO20.Size = New System.Drawing.Size(189, 37)
        Me.InputIO20.TabIndex = 24
        '
        'InputIO12
        '
        Me.InputIO12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO12.Location = New System.Drawing.Point(294, 40)
        Me.InputIO12.Name = "InputIO12"
        Me.InputIO12.Size = New System.Drawing.Size(189, 31)
        Me.InputIO12.TabIndex = 23
        '
        'InputIO19
        '
        Me.InputIO19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO19.Location = New System.Drawing.Point(294, 299)
        Me.InputIO19.Name = "InputIO19"
        Me.InputIO19.Size = New System.Drawing.Size(189, 31)
        Me.InputIO19.TabIndex = 21
        '
        'InputIO9
        '
        Me.InputIO9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO9.Location = New System.Drawing.Point(3, 299)
        Me.InputIO9.Name = "InputIO9"
        Me.InputIO9.Size = New System.Drawing.Size(188, 31)
        Me.InputIO9.TabIndex = 18
        '
        'InputIO8
        '
        Me.InputIO8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO8.Location = New System.Drawing.Point(3, 262)
        Me.InputIO8.Name = "InputIO8"
        Me.InputIO8.Size = New System.Drawing.Size(188, 31)
        Me.InputIO8.TabIndex = 17
        '
        'InputIO18
        '
        Me.InputIO18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO18.Location = New System.Drawing.Point(294, 262)
        Me.InputIO18.Name = "InputIO18"
        Me.InputIO18.Size = New System.Drawing.Size(189, 31)
        Me.InputIO18.TabIndex = 16
        '
        'InputIO7
        '
        Me.InputIO7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO7.Location = New System.Drawing.Point(3, 225)
        Me.InputIO7.Name = "InputIO7"
        Me.InputIO7.Size = New System.Drawing.Size(188, 31)
        Me.InputIO7.TabIndex = 14
        '
        'InputIO1
        '
        Me.InputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO1.Location = New System.Drawing.Point(3, 3)
        Me.InputIO1.Name = "InputIO1"
        Me.InputIO1.Size = New System.Drawing.Size(188, 31)
        Me.InputIO1.TabIndex = 0
        '
        'InputIO2
        '
        Me.InputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO2.Location = New System.Drawing.Point(3, 40)
        Me.InputIO2.Name = "InputIO2"
        Me.InputIO2.Size = New System.Drawing.Size(188, 31)
        Me.InputIO2.TabIndex = 2
        '
        'InputIO3
        '
        Me.InputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO3.Location = New System.Drawing.Point(3, 77)
        Me.InputIO3.Name = "InputIO3"
        Me.InputIO3.Size = New System.Drawing.Size(188, 31)
        Me.InputIO3.TabIndex = 3
        '
        'InputIO4
        '
        Me.InputIO4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO4.Location = New System.Drawing.Point(3, 114)
        Me.InputIO4.Name = "InputIO4"
        Me.InputIO4.Size = New System.Drawing.Size(188, 31)
        Me.InputIO4.TabIndex = 4
        '
        'InputIO5
        '
        Me.InputIO5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO5.Location = New System.Drawing.Point(3, 151)
        Me.InputIO5.Name = "InputIO5"
        Me.InputIO5.Size = New System.Drawing.Size(188, 31)
        Me.InputIO5.TabIndex = 5
        '
        'InputIO6
        '
        Me.InputIO6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO6.Location = New System.Drawing.Point(3, 188)
        Me.InputIO6.Name = "InputIO6"
        Me.InputIO6.Size = New System.Drawing.Size(188, 31)
        Me.InputIO6.TabIndex = 6
        '
        'GroupBox_Mid_Output
        '
        Me.GroupBox_Mid_Output.Controls.Add(Me.HmiTableLayoutPanel_Mid)
        Me.GroupBox_Mid_Output.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Mid_Output.Location = New System.Drawing.Point(3, 404)
        Me.GroupBox_Mid_Output.Name = "GroupBox_Mid_Output"
        Me.GroupBox_Mid_Output.Size = New System.Drawing.Size(492, 126)
        Me.GroupBox_Mid_Output.TabIndex = 3
        Me.GroupBox_Mid_Output.TabStop = False
        Me.GroupBox_Mid_Output.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Mid
        '
        Me.HmiTableLayoutPanel_Mid.ColumnCount = 3
        Me.HmiTableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.OutputIO1, 0, 0)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.OutputIO2, 2, 0)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.OutputIO3, 0, 1)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.OutputIO4, 2, 1)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.OutputIO5, 0, 2)
        Me.HmiTableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Mid.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Mid.Name = "HmiTableLayoutPanel_Mid"
        Me.HmiTableLayoutPanel_Mid.RowCount = 3
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Mid.Size = New System.Drawing.Size(486, 107)
        Me.HmiTableLayoutPanel_Mid.TabIndex = 2
        '
        'OutputIO1
        '
        Me.OutputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO1.Location = New System.Drawing.Point(3, 3)
        Me.OutputIO1.Name = "OutputIO1"
        Me.OutputIO1.Size = New System.Drawing.Size(188, 29)
        Me.OutputIO1.TabIndex = 0
        '
        'OutputIO2
        '
        Me.OutputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO2.Location = New System.Drawing.Point(294, 3)
        Me.OutputIO2.Name = "OutputIO2"
        Me.OutputIO2.Size = New System.Drawing.Size(189, 29)
        Me.OutputIO2.TabIndex = 1
        '
        'OutputIO3
        '
        Me.OutputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO3.Location = New System.Drawing.Point(3, 38)
        Me.OutputIO3.Name = "OutputIO3"
        Me.OutputIO3.Size = New System.Drawing.Size(188, 29)
        Me.OutputIO3.TabIndex = 2
        '
        'OutputIO4
        '
        Me.OutputIO4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO4.Location = New System.Drawing.Point(294, 38)
        Me.OutputIO4.Name = "OutputIO4"
        Me.OutputIO4.Size = New System.Drawing.Size(189, 29)
        Me.OutputIO4.TabIndex = 3
        '
        'OutputIO5
        '
        Me.OutputIO5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO5.Location = New System.Drawing.Point(3, 73)
        Me.OutputIO5.Name = "OutputIO5"
        Me.OutputIO5.Size = New System.Drawing.Size(188, 31)
        Me.OutputIO5.TabIndex = 4
        '
        'HmiTableLayoutPanel_Bottom
        '
        Me.HmiTableLayoutPanel_Bottom.ColumnCount = 4
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiLabel_OffDelay, 0, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_OffDelay, 1, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiLabel_indexDelay, 2, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_indexDelay, 3, 0)
        Me.HmiTableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Bottom.Location = New System.Drawing.Point(3, 536)
        Me.HmiTableLayoutPanel_Bottom.Name = "HmiTableLayoutPanel_Bottom"
        Me.HmiTableLayoutPanel_Bottom.RowCount = 1
        Me.HmiTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Bottom.Size = New System.Drawing.Size(492, 35)
        Me.HmiTableLayoutPanel_Bottom.TabIndex = 4
        '
        'HmiLabel_OffDelay
        '
        Me.HmiLabel_OffDelay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_OffDelay.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_OffDelay.Name = "HmiLabel_OffDelay"
        Me.HmiLabel_OffDelay.Size = New System.Drawing.Size(141, 29)
        Me.HmiLabel_OffDelay.TabIndex = 0
        '
        'HmiTextBox_OffDelay
        '
        Me.HmiTextBox_OffDelay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_OffDelay.Location = New System.Drawing.Point(150, 3)
        Me.HmiTextBox_OffDelay.Name = "HmiTextBox_OffDelay"
        Me.HmiTextBox_OffDelay.Number = 0
        Me.HmiTextBox_OffDelay.Size = New System.Drawing.Size(92, 29)
        Me.HmiTextBox_OffDelay.TabIndex = 1
        Me.HmiTextBox_OffDelay.TextBoxReadOnly = False
        Me.HmiTextBox_OffDelay.ValueType = GetType(String)
        '
        'HmiLabel_indexDelay
        '
        Me.HmiLabel_indexDelay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_indexDelay.Location = New System.Drawing.Point(248, 3)
        Me.HmiLabel_indexDelay.Name = "HmiLabel_indexDelay"
        Me.HmiLabel_indexDelay.Size = New System.Drawing.Size(141, 29)
        Me.HmiLabel_indexDelay.TabIndex = 2
        '
        'HmiTextBox_indexDelay
        '
        Me.HmiTextBox_indexDelay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_indexDelay.Location = New System.Drawing.Point(395, 3)
        Me.HmiTextBox_indexDelay.Name = "HmiTextBox_indexDelay"
        Me.HmiTextBox_indexDelay.Number = 0
        Me.HmiTextBox_indexDelay.Size = New System.Drawing.Size(94, 29)
        Me.HmiTextBox_indexDelay.TabIndex = 3
        Me.HmiTextBox_indexDelay.TextBoxReadOnly = False
        Me.HmiTextBox_indexDelay.ValueType = GetType(String)
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 574)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IOForm"
        Me.Text = "IOForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Top_input.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.GroupBox_Mid_Output.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Mid.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Top_input As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Body_Top As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents InputIO7 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO1 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO2 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO3 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO4 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO5 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO6 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO12 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO19 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO9 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO8 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO18 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO17 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO16 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO15 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO10 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO14 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO13 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO11 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO20 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents GroupBox_Mid_Output As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Mid As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents OutputIO1 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO2 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO3 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO4 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO5 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents HmiTableLayoutPanel_Bottom As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_OffDelay As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_OffDelay As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_indexDelay As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_indexDelay As Kochi.HMI.MainControl.UI.HMITextBox
End Class
