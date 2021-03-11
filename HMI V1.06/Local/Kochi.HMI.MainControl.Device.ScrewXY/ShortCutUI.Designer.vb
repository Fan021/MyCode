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
        Me.HmiTextBox_Pro = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_AST = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Speed = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Speed = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Y = New System.Windows.Forms.Label()
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_AST = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_MoveX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveY = New Kochi.HMI.MainControl.UI.HMILabel()
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Pro, 3, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_AST, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Speed, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Speed, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Y, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Y, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_X, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_X, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveY, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_AST, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveX, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveX, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveY, 2, 2)
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
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(465, 502)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiTextBox_Pro
        '
        Me.HmiTextBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pro.Location = New System.Drawing.Point(234, 163)
        Me.HmiTextBox_Pro.Name = "HmiTextBox_Pro"
        Me.HmiTextBox_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_Pro.TabIndex = 44
        Me.HmiTextBox_Pro.TextBoxReadOnly = False
        '
        'HmiTextBox_AST
        '
        Me.HmiTextBox_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AST.Location = New System.Drawing.Point(80, 163)
        Me.HmiTextBox_AST.Name = "HmiTextBox_AST"
        Me.HmiTextBox_AST.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_AST.TabIndex = 43
        Me.HmiTextBox_AST.TextBoxReadOnly = False
        '
        'HmiTextBox_Speed
        '
        Me.HmiTextBox_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Speed.Location = New System.Drawing.Point(80, 123)
        Me.HmiTextBox_Speed.Name = "HmiTextBox_Speed"
        Me.HmiTextBox_Speed.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_Speed.TabIndex = 38
        Me.HmiTextBox_Speed.TextBoxReadOnly = False
        '
        'HmiLabel_Speed
        '
        Me.HmiLabel_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Speed.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_Speed.Name = "HmiLabel_Speed"
        Me.HmiLabel_Speed.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Speed.TabIndex = 37
        '
        'Label_Y
        '
        Me.Label_Y.AutoSize = True
        Me.Label_Y.BackColor = System.Drawing.Color.LightGray
        Me.Label_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Y.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_Y.ForeColor = System.Drawing.Color.Blue
        Me.Label_Y.Location = New System.Drawing.Point(236, 45)
        Me.Label_Y.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Y.Name = "Label_Y"
        Me.Label_Y.Size = New System.Drawing.Size(67, 30)
        Me.Label_Y.TabIndex = 34
        Me.Label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(157, 43)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(71, 34)
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
        'HmiTextBox_MoveY
        '
        Me.HmiTextBox_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveY.Location = New System.Drawing.Point(234, 83)
        Me.HmiTextBox_MoveY.Name = "HmiTextBox_MoveY"
        Me.HmiTextBox_MoveY.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_MoveY.TabIndex = 26
        Me.HmiTextBox_MoveY.TextBoxReadOnly = False
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(157, 163)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Pro.TabIndex = 21
        '
        'HmiLabel_AST
        '
        Me.HmiLabel_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AST.Location = New System.Drawing.Point(3, 163)
        Me.HmiLabel_AST.Name = "HmiLabel_AST"
        Me.HmiLabel_AST.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_AST.TabIndex = 19
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
        Me.HmiTextBox_MoveX.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_MoveX.TabIndex = 14
        Me.HmiTextBox_MoveX.TextBoxReadOnly = False
        '
        'HmiLabel_MoveY
        '
        Me.HmiLabel_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveY.Location = New System.Drawing.Point(157, 83)
        Me.HmiLabel_MoveY.Name = "HmiLabel_MoveY"
        Me.HmiLabel_MoveY.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_MoveY.TabIndex = 15
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
    Friend WithEvents HmiLabel_MoveY As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_AST As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_Y As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_X As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Speed As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Speed As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Pro As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_AST As Kochi.HMI.MainControl.UI.HMITextBox
End Class
