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
    'Do not modify it using the code ediScrewFeeder.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Top_input = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Body_Top = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.InputIO1 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO2 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO3 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.GroupBox_Mid_Output = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.OutputIO1 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO2 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO3 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Top_input.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top.SuspendLayout()
        Me.GroupBox_Mid_Output.SuspendLayout()
        Me.HmiTableLayoutPanel_Mid.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Top_input, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Mid_Output, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(498, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'GroupBox_Top_input
        '
        Me.GroupBox_Top_input.Controls.Add(Me.HmiTableLayoutPanel_Body_Top)
        Me.GroupBox_Top_input.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Top_input.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Top_input.Name = "GroupBox_Top_input"
        Me.GroupBox_Top_input.Size = New System.Drawing.Size(492, 100)
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
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO1, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO2, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO3, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel_Body_Top.Name = "HmiTableLayoutPanel_Body_Top"
        Me.HmiTableLayoutPanel_Body_Top.RowCount = 2
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.Size = New System.Drawing.Size(486, 80)
        Me.HmiTableLayoutPanel_Body_Top.TabIndex = 1
        '
        'InputIO1
        '
        Me.InputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO1.Location = New System.Drawing.Point(3, 3)
        Me.InputIO1.Name = "InputIO1"
        Me.InputIO1.Size = New System.Drawing.Size(188, 34)
        Me.InputIO1.TabIndex = 0
        '
        'InputIO2
        '
        Me.InputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO2.Location = New System.Drawing.Point(294, 3)
        Me.InputIO2.Name = "InputIO2"
        Me.InputIO2.Size = New System.Drawing.Size(189, 34)
        Me.InputIO2.TabIndex = 2
        '
        'InputIO3
        '
        Me.InputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO3.Location = New System.Drawing.Point(3, 43)
        Me.InputIO3.Name = "InputIO3"
        Me.InputIO3.Size = New System.Drawing.Size(188, 34)
        Me.InputIO3.TabIndex = 3
        '
        'GroupBox_Mid_Output
        '
        Me.GroupBox_Mid_Output.Controls.Add(Me.HmiTableLayoutPanel_Mid)
        Me.GroupBox_Mid_Output.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Mid_Output.Location = New System.Drawing.Point(3, 109)
        Me.GroupBox_Mid_Output.Name = "GroupBox_Mid_Output"
        Me.GroupBox_Mid_Output.Size = New System.Drawing.Size(492, 100)
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
        Me.HmiTableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Mid.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel_Mid.Name = "HmiTableLayoutPanel_Mid"
        Me.HmiTableLayoutPanel_Mid.RowCount = 2
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Mid.Size = New System.Drawing.Size(486, 80)
        Me.HmiTableLayoutPanel_Mid.TabIndex = 2
        '
        'OutputIO1
        '
        Me.OutputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO1.Location = New System.Drawing.Point(3, 3)
        Me.OutputIO1.Name = "OutputIO1"
        Me.OutputIO1.Size = New System.Drawing.Size(188, 25)
        Me.OutputIO1.TabIndex = 0
        '
        'OutputIO2
        '
        Me.OutputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO2.Location = New System.Drawing.Point(294, 3)
        Me.OutputIO2.Name = "OutputIO2"
        Me.OutputIO2.Size = New System.Drawing.Size(189, 25)
        Me.OutputIO2.TabIndex = 1
        '
        'OutputIO3
        '
        Me.OutputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO3.Location = New System.Drawing.Point(3, 34)
        Me.OutputIO3.Name = "OutputIO3"
        Me.OutputIO3.Size = New System.Drawing.Size(188, 25)
        Me.OutputIO3.TabIndex = 2
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 530)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Top_input As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Body_Top As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents InputIO1 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO2 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO3 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents GroupBox_Mid_Output As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Mid As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents OutputIO1 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO2 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO3 As Kochi.HMI.MainControl.UI.OutputIO
End Class
