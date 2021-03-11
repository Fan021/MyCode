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
        Me.HmiTableLayoutPanel_Bottom = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_Variant = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Button_Complete = New System.Windows.Forms.Button()
        Me.Button_Start = New System.Windows.Forms.Button()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Button_Abort = New System.Windows.Forms.Button()
        Me.HmiTextBox_Result = New System.Windows.Forms.TextBox()
        Me.HmiTextBox_ErrorMessage = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ErrorMessage = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
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
        Me.Panel_Body.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTableLayoutPanel_Bottom, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(498, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'HmiTableLayoutPanel_Bottom
        '
        Me.HmiTableLayoutPanel_Bottom.ColumnCount = 3
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiLabel_ErrorMessage, 2, 1)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_ErrorMessage, 2, 2)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_Variant, 2, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.Button_Complete, 0, 3)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.Button_Start, 0, 2)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiLabel_SFC, 0, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_SFC, 1, 0)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.Button_Abort, 0, 1)
        Me.HmiTableLayoutPanel_Bottom.Controls.Add(Me.HmiTextBox_Result, 1, 1)
        Me.HmiTableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Bottom.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Bottom.Name = "HmiTableLayoutPanel_Bottom"
        Me.HmiTableLayoutPanel_Bottom.RowCount = 4
        Me.HmiTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Bottom.Size = New System.Drawing.Size(492, 142)
        Me.HmiTableLayoutPanel_Bottom.TabIndex = 4
        '
        'HmiTextBox_Variant
        '
        Me.HmiTextBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Variant.Location = New System.Drawing.Point(331, 3)
        Me.HmiTextBox_Variant.Name = "HmiTextBox_Variant"
        Me.HmiTextBox_Variant.Number = 0
        Me.HmiTextBox_Variant.Size = New System.Drawing.Size(158, 29)
        Me.HmiTextBox_Variant.TabIndex = 7
        Me.HmiTextBox_Variant.TextBoxReadOnly = False
        Me.HmiTextBox_Variant.ValueType = GetType(String)
        '
        'Button_Complete
        '
        Me.Button_Complete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Complete.Location = New System.Drawing.Point(3, 108)
        Me.Button_Complete.Name = "Button_Complete"
        Me.Button_Complete.Size = New System.Drawing.Size(158, 31)
        Me.Button_Complete.TabIndex = 5
        Me.Button_Complete.Text = "Button1"
        Me.Button_Complete.UseVisualStyleBackColor = True
        '
        'Button_Start
        '
        Me.Button_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Start.Location = New System.Drawing.Point(3, 73)
        Me.Button_Start.Name = "Button_Start"
        Me.Button_Start.Size = New System.Drawing.Size(158, 29)
        Me.Button_Start.TabIndex = 4
        Me.Button_Start.Text = "Button1"
        Me.Button_Start.UseVisualStyleBackColor = True
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(158, 29)
        Me.HmiLabel_SFC.TabIndex = 0
        '
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(167, 3)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(158, 29)
        Me.HmiTextBox_SFC.TabIndex = 1
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'Button_Abort
        '
        Me.Button_Abort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Abort.Location = New System.Drawing.Point(3, 38)
        Me.Button_Abort.Name = "Button_Abort"
        Me.Button_Abort.Size = New System.Drawing.Size(158, 29)
        Me.Button_Abort.TabIndex = 2
        Me.Button_Abort.Text = "Button1"
        Me.Button_Abort.UseVisualStyleBackColor = True
        '
        'HmiTextBox_Result
        '
        Me.HmiTextBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Result.Location = New System.Drawing.Point(167, 38)
        Me.HmiTextBox_Result.Multiline = True
        Me.HmiTextBox_Result.Name = "HmiTextBox_Result"
        Me.HmiTableLayoutPanel_Bottom.SetRowSpan(Me.HmiTextBox_Result, 3)
        Me.HmiTextBox_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.HmiTextBox_Result.Size = New System.Drawing.Size(158, 101)
        Me.HmiTextBox_Result.TabIndex = 6
        '
        'HmiTextBox_ErrorMessage
        '
        Me.HmiTextBox_ErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ErrorMessage.Location = New System.Drawing.Point(331, 73)
        Me.HmiTextBox_ErrorMessage.Name = "HmiTextBox_ErrorMessage"
        Me.HmiTextBox_ErrorMessage.Number = 0
        Me.HmiTextBox_ErrorMessage.Size = New System.Drawing.Size(158, 29)
        Me.HmiTextBox_ErrorMessage.TabIndex = 8
        Me.HmiTextBox_ErrorMessage.TextBoxReadOnly = False
        Me.HmiTextBox_ErrorMessage.ValueType = GetType(String)
        '
        'HmiLabel_ErrorMessage
        '
        Me.HmiLabel_ErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ErrorMessage.Location = New System.Drawing.Point(331, 38)
        Me.HmiLabel_ErrorMessage.Name = "HmiLabel_ErrorMessage"
        Me.HmiLabel_ErrorMessage.Size = New System.Drawing.Size(158, 29)
        Me.HmiLabel_ErrorMessage.TabIndex = 9
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
        Me.HmiTableLayoutPanel_Bottom.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Bottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Bottom As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Button_Abort As System.Windows.Forms.Button
    Friend WithEvents Button_Complete As System.Windows.Forms.Button
    Friend WithEvents Button_Start As System.Windows.Forms.Button
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Result As System.Windows.Forms.TextBox
    Friend WithEvents HmiTextBox_Variant As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_ErrorMessage As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ErrorMessage As Kochi.HMI.MainControl.UI.HMILabel
End Class
