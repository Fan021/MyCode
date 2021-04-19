<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOGroupParameter
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
        Me.TableLayoutPanel_Body = New Kostal.Las.UserInterface.HMITableLayoutPanel(Me.components)
        Me.ComboBoxEx_Position = New Kostal.Las.UserInterface.ComboBoxEx()
        Me.Label_Position = New System.Windows.Forms.Label()
        Me.TextBox_NameA2 = New System.Windows.Forms.TextBox()
        Me.Label_NameA2 = New System.Windows.Forms.Label()
        Me.Label_Model = New System.Windows.Forms.Label()
        Me.Label_ID = New System.Windows.Forms.Label()
        Me.TextBox_ID = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Label_NameA = New System.Windows.Forms.Label()
        Me.TextBox_NameA = New System.Windows.Forms.TextBox()
        Me.ComboBox_Model = New Kostal.Las.UserInterface.ComboBoxEx()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.06358!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.93642!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.ComboBoxEx_Position, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Position, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA2, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA2, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Model, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ID, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.ComboBox_Model, 1, 3)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 6
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(533, 306)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'ComboBoxEx_Position
        '
        Me.ComboBoxEx_Position.BackColor = System.Drawing.Color.White
        Me.ComboBoxEx_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEx_Position.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx_Position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEx_Position.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxEx_Position.Location = New System.Drawing.Point(232, 163)
        Me.ComboBoxEx_Position.Name = "ComboBoxEx_Position"
        Me.ComboBoxEx_Position.Size = New System.Drawing.Size(298, 22)
        Me.ComboBoxEx_Position.TabIndex = 29
        '
        'Label_Position
        '
        Me.Label_Position.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Position.AutoSize = True
        Me.Label_Position.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Position.Location = New System.Drawing.Point(32, 165)
        Me.Label_Position.Name = "Label_Position"
        Me.Label_Position.Size = New System.Drawing.Size(164, 29)
        Me.Label_Position.TabIndex = 27
        Me.Label_Position.Text = "Move Position:"
        Me.Label_Position.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_NameA2
        '
        Me.TextBox_NameA2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameA2.Location = New System.Drawing.Point(232, 83)
        Me.TextBox_NameA2.Name = "TextBox_NameA2"
        Me.TextBox_NameA2.Size = New System.Drawing.Size(298, 37)
        Me.TextBox_NameA2.TabIndex = 23
        '
        'Label_NameA2
        '
        Me.Label_NameA2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA2.AutoSize = True
        Me.Label_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA2.Location = New System.Drawing.Point(76, 85)
        Me.Label_NameA2.Name = "Label_NameA2"
        Me.Label_NameA2.Size = New System.Drawing.Size(76, 29)
        Me.Label_NameA2.TabIndex = 22
        Me.Label_NameA2.Text = "TextA:"
        Me.Label_NameA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Model
        '
        Me.Label_Model.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Model.AutoSize = True
        Me.Label_Model.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Model.Location = New System.Drawing.Point(81, 125)
        Me.Label_Model.Name = "Label_Model"
        Me.Label_Model.Size = New System.Drawing.Size(67, 29)
        Me.Label_Model.TabIndex = 20
        Me.Label_Model.Text = "Type:"
        Me.Label_Model.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_ID
        '
        Me.Label_ID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ID.AutoSize = True
        Me.Label_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ID.Location = New System.Drawing.Point(89, 5)
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
        Me.TextBox_ID.Location = New System.Drawing.Point(232, 3)
        Me.TextBox_ID.Name = "TextBox_ID"
        Me.TextBox_ID.ReadOnly = True
        Me.TextBox_ID.Size = New System.Drawing.Size(298, 37)
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
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(232, 203)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(298, 100)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 11
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Save.Location = New System.Drawing.Point(3, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(143, 97)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Cancel.Location = New System.Drawing.Point(152, 3)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(143, 97)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Label_NameA
        '
        Me.Label_NameA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA.AutoSize = True
        Me.Label_NameA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA.Location = New System.Drawing.Point(76, 45)
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
        Me.TextBox_NameA.Location = New System.Drawing.Point(232, 43)
        Me.TextBox_NameA.Name = "TextBox_NameA"
        Me.TextBox_NameA.Size = New System.Drawing.Size(298, 37)
        Me.TextBox_NameA.TabIndex = 13
        '
        'ComboBox_Model
        '
        Me.ComboBox_Model.BackColor = System.Drawing.Color.White
        Me.ComboBox_Model.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox_Model.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox_Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Model.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox_Model.Location = New System.Drawing.Point(232, 123)
        Me.ComboBox_Model.Name = "ComboBox_Model"
        Me.ComboBox_Model.Size = New System.Drawing.Size(298, 22)
        Me.ComboBox_Model.TabIndex = 26
        '
        'IOGroupParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 306)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Name = "IOGroupParameter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "IOParameter"
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox_Model As ComboBoxEx
    Friend WithEvents TextBox_NameA As System.Windows.Forms.TextBox
    Friend WithEvents Label_NameA As System.Windows.Forms.Label
    Friend WithEvents TextBox_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label_ID As System.Windows.Forms.Label
    Friend WithEvents Label_NameA2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_NameA2 As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Body As HMITableLayoutPanel
    Friend WithEvents Label_Model As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Label_Position As System.Windows.Forms.Label
    Friend WithEvents ComboBoxEx_Position As ComboBoxEx
End Class
