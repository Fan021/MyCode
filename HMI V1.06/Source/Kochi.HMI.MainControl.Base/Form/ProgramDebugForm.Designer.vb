<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgramDebugForm
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
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.TableLayoutPanel_Reserve = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_B = New System.Windows.Forms.RadioButton()
        Me.RadioButton_A = New System.Windows.Forms.RadioButton()
        Me.RadioButton_AB = New System.Windows.Forms.RadioButton()
        Me.Label_Type = New System.Windows.Forms.Label()
        Me.Label_Name = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.ComboBox_Name = New Kochi.HMI.MainControl.UI.ComboBoxEx()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Reserve.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.92126!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.07874!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Reserve, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Type, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Name, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.ComboBox_Name, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(901, 187)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Reserve
        '
        Me.TableLayoutPanel_Reserve.ColumnCount = 3
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_B, 0, 0)
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_A, 0, 0)
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_AB, 1, 0)
        Me.TableLayoutPanel_Reserve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Reserve.Location = New System.Drawing.Point(270, 41)
        Me.TableLayoutPanel_Reserve.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Reserve.Name = "TableLayoutPanel_Reserve"
        Me.TableLayoutPanel_Reserve.RowCount = 1
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Reserve.Size = New System.Drawing.Size(630, 38)
        Me.TableLayoutPanel_Reserve.TabIndex = 28
        '
        'RadioButton_B
        '
        Me.RadioButton_B.AutoSize = True
        Me.RadioButton_B.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_B.Location = New System.Drawing.Point(213, 3)
        Me.RadioButton_B.Name = "RadioButton_B"
        Me.RadioButton_B.Size = New System.Drawing.Size(44, 32)
        Me.RadioButton_B.TabIndex = 2
        Me.RadioButton_B.TabStop = True
        Me.RadioButton_B.Text = "B"
        Me.RadioButton_B.UseVisualStyleBackColor = True
        '
        'RadioButton_A
        '
        Me.RadioButton_A.AutoSize = True
        Me.RadioButton_A.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_A.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_A.Name = "RadioButton_A"
        Me.RadioButton_A.Size = New System.Drawing.Size(46, 32)
        Me.RadioButton_A.TabIndex = 0
        Me.RadioButton_A.TabStop = True
        Me.RadioButton_A.Text = "A"
        Me.RadioButton_A.UseVisualStyleBackColor = True
        '
        'RadioButton_AB
        '
        Me.RadioButton_AB.AutoSize = True
        Me.RadioButton_AB.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_AB.Location = New System.Drawing.Point(423, 3)
        Me.RadioButton_AB.Name = "RadioButton_AB"
        Me.RadioButton_AB.Size = New System.Drawing.Size(59, 32)
        Me.RadioButton_AB.TabIndex = 1
        Me.RadioButton_AB.TabStop = True
        Me.RadioButton_AB.Text = "AB"
        Me.RadioButton_AB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadioButton_AB.UseVisualStyleBackColor = True
        '
        'Label_Type
        '
        Me.Label_Type.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Type.AutoSize = True
        Me.Label_Type.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Type.Location = New System.Drawing.Point(101, 45)
        Me.Label_Type.Name = "Label_Type"
        Me.Label_Type.Size = New System.Drawing.Size(67, 29)
        Me.Label_Type.TabIndex = 27
        Me.Label_Type.Text = "Type:"
        Me.Label_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Name
        '
        Me.Label_Name.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Name.AutoSize = True
        Me.Label_Name.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Name.Location = New System.Drawing.Point(94, 5)
        Me.Label_Name.Name = "Label_Name"
        Me.Label_Name.Size = New System.Drawing.Size(80, 29)
        Me.Label_Name.TabIndex = 20
        Me.Label_Name.Text = "Name:"
        Me.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Save, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Cancel, 1, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(272, 83)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(626, 101)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 11
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Save.Location = New System.Drawing.Point(3, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(307, 97)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Cancel.Location = New System.Drawing.Point(316, 3)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(307, 97)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'ComboBox_Name
        '
        Me.ComboBox_Name.BackColor = System.Drawing.Color.White
        Me.ComboBox_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox_Name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Name.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox_Name.Location = New System.Drawing.Point(272, 3)
        Me.ComboBox_Name.Name = "ComboBox_Name"
        Me.ComboBox_Name.Size = New System.Drawing.Size(626, 22)
        Me.ComboBox_Name.TabIndex = 26
        '
        'ProgramDebugForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 187)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Name = "ProgramDebugForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ProgramDebugForm"
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        Me.TableLayoutPanel_Reserve.ResumeLayout(False)
        Me.TableLayoutPanel_Reserve.PerformLayout()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox_Name As Kochi.HMI.MainControl.UI.ComboBoxEx
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_Name As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label_Type As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Reserve As System.Windows.Forms.TableLayoutPanel
    Public WithEvents RadioButton_B As System.Windows.Forms.RadioButton
    Public WithEvents RadioButton_A As System.Windows.Forms.RadioButton
    Public WithEvents RadioButton_AB As System.Windows.Forms.RadioButton
End Class
