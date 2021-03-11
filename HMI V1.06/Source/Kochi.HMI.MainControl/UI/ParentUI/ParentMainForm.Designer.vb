<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParentMainForm
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
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_TotalTime = New System.Windows.Forms.Label()
        Me.MainRightButton_Back = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.MainRightButton_ShortCut = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.HmiLabel_Count = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_FailRate = New System.Windows.Forms.Label()
        Me.Label_Total = New System.Windows.Forms.Label()
        Me.Label_SFC = New System.Windows.Forms.Label()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.MainRightButton_Login = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.MainRightButton_Variant = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Pass = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Fail = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_FailRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Pass = New System.Windows.Forms.Label()
        Me.Label_Fail = New System.Windows.Forms.Label()
        Me.Label_Variant = New System.Windows.Forms.Label()
        Me.MainButton_Clean = New Kochi.HMI.MainControl.UI.MainButton()
        Me.Panel_Mid = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.Panel_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.Color.Transparent
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(623, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Top, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 2
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Right, 1, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Mid, 0, 0)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body_Top.TabIndex = 5
        '
        'Panel_Right
        '
        Me.Panel_Right.Controls.Add(Me.TableLayoutPanel_Body_Top_Right)
        Me.Panel_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Right.Location = New System.Drawing.Point(498, 0)
        Me.Panel_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Right.Name = "Panel_Right"
        Me.Panel_Right.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.Panel_Right.Size = New System.Drawing.Size(125, 530)
        Me.Panel_Right.TabIndex = 5
        '
        'TableLayoutPanel_Body_Top_Right
        '
        Me.TableLayoutPanel_Body_Top_Right.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Body_Top_Right.ColumnCount = 1
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_TotalTime, 0, 13)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Back, 0, 19)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_ShortCut, 0, 16)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Count, 0, 4)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_FailRate, 0, 11)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Total, 0, 5)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_SFC, 0, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Login, 0, 18)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Variant, 0, 17)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SFC, 0, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pass, 0, 6)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Fail, 0, 8)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_FailRate, 0, 10)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Pass, 0, 7)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Fail, 0, 9)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Variant, 0, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainButton_Clean, 0, 15)
        Me.TableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(2, 0)
        Me.TableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top_Right.Name = "TableLayoutPanel_Body_Top_Right"
        Me.TableLayoutPanel_Body_Top_Right.RowCount = 21
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.205116!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.393958!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.205115!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.393958!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.205115!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.390628!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.24337!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.390628!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.245058!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.390628!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.243369!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.390628!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.246054!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.390628!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.961012!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.140948!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.140948!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.140948!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.140948!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.140948!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(123, 530)
        Me.TableLayoutPanel_Body_Top_Right.TabIndex = 2
        '
        'Label_TotalTime
        '
        Me.Label_TotalTime.AutoSize = True
        Me.Label_TotalTime.BackColor = System.Drawing.Color.White
        Me.Label_TotalTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_TotalTime.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_TotalTime.Location = New System.Drawing.Point(1, 269)
        Me.Label_TotalTime.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_TotalTime.Name = "Label_TotalTime"
        Me.Label_TotalTime.Size = New System.Drawing.Size(121, 26)
        Me.Label_TotalTime.TabIndex = 24
        Me.Label_TotalTime.Text = "0.00 s"
        Me.Label_TotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MainRightButton_Back
        '
        Me.MainRightButton_Back.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Back.FunctionEnable = True
        Me.MainRightButton_Back.Location = New System.Drawing.Point(1, 480)
        Me.MainRightButton_Back.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MainRightButton_Back.Name = "MainRightButton_Back"
        Me.MainRightButton_Back.Size = New System.Drawing.Size(121, 39)
        Me.MainRightButton_Back.TabIndex = 23
        '
        'MainRightButton_ShortCut
        '
        Me.MainRightButton_ShortCut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_ShortCut.FunctionEnable = True
        Me.MainRightButton_ShortCut.Location = New System.Drawing.Point(1, 351)
        Me.MainRightButton_ShortCut.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MainRightButton_ShortCut.Name = "MainRightButton_ShortCut"
        Me.MainRightButton_ShortCut.Size = New System.Drawing.Size(121, 39)
        Me.MainRightButton_ShortCut.TabIndex = 22
        '
        'HmiLabel_Count
        '
        Me.HmiLabel_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Count.Location = New System.Drawing.Point(1, 111)
        Me.HmiLabel_Count.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Count.Name = "HmiLabel_Count"
        Me.HmiLabel_Count.Size = New System.Drawing.Size(121, 20)
        Me.HmiLabel_Count.TabIndex = 20
        '
        'Label_FailRate
        '
        Me.Label_FailRate.AutoSize = True
        Me.Label_FailRate.BackColor = System.Drawing.Color.White
        Me.Label_FailRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_FailRate.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_FailRate.Location = New System.Drawing.Point(1, 235)
        Me.Label_FailRate.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_FailRate.Name = "Label_FailRate"
        Me.Label_FailRate.Size = New System.Drawing.Size(121, 26)
        Me.Label_FailRate.TabIndex = 19
        Me.Label_FailRate.Text = "0.00 %"
        Me.Label_FailRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Total
        '
        Me.Label_Total.AutoSize = True
        Me.Label_Total.BackColor = System.Drawing.Color.White
        Me.Label_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Total.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Total.Location = New System.Drawing.Point(1, 133)
        Me.Label_Total.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Total.Name = "Label_Total"
        Me.Label_Total.Size = New System.Drawing.Size(121, 26)
        Me.Label_Total.TabIndex = 18
        Me.Label_Total.Text = "0"
        Me.Label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_SFC
        '
        Me.Label_SFC.AutoSize = True
        Me.Label_SFC.BackColor = System.Drawing.Color.White
        Me.Label_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_SFC.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_SFC.Location = New System.Drawing.Point(1, 78)
        Me.Label_SFC.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_SFC.Name = "Label_SFC"
        Me.Label_SFC.Size = New System.Drawing.Size(121, 31)
        Me.Label_SFC.TabIndex = 17
        Me.Label_SFC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Variant.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(121, 20)
        Me.HmiLabel_Variant.TabIndex = 0
        '
        'MainRightButton_Login
        '
        Me.MainRightButton_Login.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Login.FunctionEnable = True
        Me.MainRightButton_Login.Location = New System.Drawing.Point(1, 437)
        Me.MainRightButton_Login.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MainRightButton_Login.Name = "MainRightButton_Login"
        Me.MainRightButton_Login.Size = New System.Drawing.Size(121, 39)
        Me.MainRightButton_Login.TabIndex = 2
        '
        'MainRightButton_Variant
        '
        Me.MainRightButton_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Variant.FunctionEnable = True
        Me.MainRightButton_Variant.Location = New System.Drawing.Point(1, 394)
        Me.MainRightButton_Variant.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MainRightButton_Variant.Name = "MainRightButton_Variant"
        Me.MainRightButton_Variant.Size = New System.Drawing.Size(121, 39)
        Me.MainRightButton_Variant.TabIndex = 3
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.BackColor = System.Drawing.Color.Transparent
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(1, 56)
        Me.HmiLabel_SFC.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(121, 20)
        Me.HmiLabel_SFC.TabIndex = 4
        '
        'HmiLabel_Pass
        '
        Me.HmiLabel_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pass.Location = New System.Drawing.Point(3, 163)
        Me.HmiLabel_Pass.Name = "HmiLabel_Pass"
        Me.HmiLabel_Pass.Size = New System.Drawing.Size(117, 1)
        Me.HmiLabel_Pass.TabIndex = 11
        '
        'HmiLabel_Fail
        '
        Me.HmiLabel_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Fail.Location = New System.Drawing.Point(3, 197)
        Me.HmiLabel_Fail.Name = "HmiLabel_Fail"
        Me.HmiLabel_Fail.Size = New System.Drawing.Size(117, 1)
        Me.HmiLabel_Fail.TabIndex = 12
        '
        'HmiLabel_FailRate
        '
        Me.HmiLabel_FailRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_FailRate.Location = New System.Drawing.Point(3, 231)
        Me.HmiLabel_FailRate.Name = "HmiLabel_FailRate"
        Me.HmiLabel_FailRate.Size = New System.Drawing.Size(117, 1)
        Me.HmiLabel_FailRate.TabIndex = 13
        '
        'Label_Pass
        '
        Me.Label_Pass.AutoSize = True
        Me.Label_Pass.BackColor = System.Drawing.Color.White
        Me.Label_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Pass.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Pass.Location = New System.Drawing.Point(1, 167)
        Me.Label_Pass.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Pass.Name = "Label_Pass"
        Me.Label_Pass.Size = New System.Drawing.Size(121, 26)
        Me.Label_Pass.TabIndex = 14
        Me.Label_Pass.Text = "0"
        Me.Label_Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Fail
        '
        Me.Label_Fail.AutoSize = True
        Me.Label_Fail.BackColor = System.Drawing.Color.White
        Me.Label_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Fail.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Fail.Location = New System.Drawing.Point(1, 201)
        Me.Label_Fail.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Fail.Name = "Label_Fail"
        Me.Label_Fail.Size = New System.Drawing.Size(121, 26)
        Me.Label_Fail.TabIndex = 15
        Me.Label_Fail.Text = "0"
        Me.Label_Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Variant
        '
        Me.Label_Variant.AutoSize = True
        Me.Label_Variant.BackColor = System.Drawing.Color.White
        Me.Label_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Variant.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Variant.Location = New System.Drawing.Point(1, 23)
        Me.Label_Variant.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Variant.Name = "Label_Variant"
        Me.Label_Variant.Size = New System.Drawing.Size(121, 31)
        Me.Label_Variant.TabIndex = 16
        Me.Label_Variant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainButton_Clean
        '
        Me.MainButton_Clean.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainButton_Clean.Location = New System.Drawing.Point(1, 308)
        Me.MainButton_Clean.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.MainButton_Clean.Name = "MainButton_Clean"
        Me.MainButton_Clean.Size = New System.Drawing.Size(121, 39)
        Me.MainButton_Clean.TabIndex = 21
        '
        'Panel_Mid
        '
        Me.Panel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Mid.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Mid.Name = "Panel_Mid"
        Me.Panel_Mid.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Mid.TabIndex = 6
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'ParentMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ParentMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ParentMainForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.Panel_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Right As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Panel_Mid As System.Windows.Forms.Panel
    Friend WithEvents MainRightButton_Login As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents MainRightButton_Variant As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Pass As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Fail As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_FailRate As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents Label_Pass As System.Windows.Forms.Label
    Public WithEvents Label_Fail As System.Windows.Forms.Label
    Public WithEvents Label_Variant As System.Windows.Forms.Label
    Public WithEvents Label_FailRate As System.Windows.Forms.Label
    Public WithEvents Label_Total As System.Windows.Forms.Label
    Public WithEvents Label_SFC As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Count As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents MainButton_Clean As Kochi.HMI.MainControl.UI.MainButton
    Friend WithEvents MainRightButton_ShortCut As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents MainRightButton_Back As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents Label_TotalTime As System.Windows.Forms.Label
End Class
