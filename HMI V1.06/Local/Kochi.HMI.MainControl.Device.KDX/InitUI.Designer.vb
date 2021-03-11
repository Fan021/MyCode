<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_Description = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Description = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ProcessName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ProcessName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.HmiLabel_Enable = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_TraceID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_TraceID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MachineName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MachineName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Adress = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Adress = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Head_Detail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 246)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Description, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Description, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ProcessName, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ProcessName, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Enable, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_TraceID, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_TraceID, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_MachineName, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_MachineName, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Adress, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Adress, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 7
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_Description
        '
        Me.HmiTextBox_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Description.Location = New System.Drawing.Point(124, 120)
        Me.HmiTextBox_Description.Name = "HmiTextBox_Description"
        Me.HmiTextBox_Description.Number = 0
        Me.HmiTextBox_Description.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Description.TabIndex = 32
        Me.HmiTextBox_Description.TextBoxReadOnly = False
        Me.HmiTextBox_Description.ValueType = GetType(String)
        '
        'HmiLabel_Description
        '
        Me.HmiLabel_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Description.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_Description.Name = "HmiLabel_Description"
        Me.HmiLabel_Description.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_Description.TabIndex = 31
        '
        'HmiTextBox_ProcessName
        '
        Me.HmiTextBox_ProcessName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ProcessName.Location = New System.Drawing.Point(124, 81)
        Me.HmiTextBox_ProcessName.Name = "HmiTextBox_ProcessName"
        Me.HmiTextBox_ProcessName.Number = 0
        Me.HmiTextBox_ProcessName.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_ProcessName.TabIndex = 30
        Me.HmiTextBox_ProcessName.TextBoxReadOnly = False
        Me.HmiTextBox_ProcessName.ValueType = GetType(String)
        '
        'HmiLabel_ProcessName
        '
        Me.HmiLabel_ProcessName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ProcessName.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_ProcessName.Name = "HmiLabel_ProcessName"
        Me.HmiLabel_ProcessName.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_ProcessName.TabIndex = 29
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(124, 198)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(145, 33)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 28
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_N.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_N.Location = New System.Drawing.Point(75, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(67, 27)
        Me.RadioButton_N.TabIndex = 1
        Me.RadioButton_N.Text = "N"
        Me.RadioButton_N.UseVisualStyleBackColor = True
        '
        'RadioButton_Y
        '
        Me.RadioButton_Y.AutoSize = True
        Me.RadioButton_Y.Checked = True
        Me.RadioButton_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Y.Name = "RadioButton_Y"
        Me.RadioButton_Y.Size = New System.Drawing.Size(66, 27)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'HmiLabel_Enable
        '
        Me.HmiLabel_Enable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Enable.Location = New System.Drawing.Point(3, 198)
        Me.HmiLabel_Enable.Name = "HmiLabel_Enable"
        Me.HmiLabel_Enable.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_Enable.TabIndex = 12
        '
        'HmiTextBox_TraceID
        '
        Me.HmiTextBox_TraceID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TraceID.Location = New System.Drawing.Point(124, 159)
        Me.HmiTextBox_TraceID.Name = "HmiTextBox_TraceID"
        Me.HmiTextBox_TraceID.Number = 0
        Me.HmiTextBox_TraceID.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_TraceID.TabIndex = 10
        Me.HmiTextBox_TraceID.TextBoxReadOnly = False
        Me.HmiTextBox_TraceID.ValueType = GetType(String)
        '
        'HmiLabel_TraceID
        '
        Me.HmiLabel_TraceID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TraceID.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_TraceID.Name = "HmiLabel_TraceID"
        Me.HmiLabel_TraceID.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_TraceID.TabIndex = 7
        '
        'HmiTextBox_MachineName
        '
        Me.HmiTextBox_MachineName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MachineName.Location = New System.Drawing.Point(124, 42)
        Me.HmiTextBox_MachineName.Name = "HmiTextBox_MachineName"
        Me.HmiTextBox_MachineName.Number = 0
        Me.HmiTextBox_MachineName.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_MachineName.TabIndex = 6
        Me.HmiTextBox_MachineName.TextBoxReadOnly = False
        Me.HmiTextBox_MachineName.ValueType = GetType(String)
        '
        'HmiLabel_MachineName
        '
        Me.HmiLabel_MachineName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MachineName.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_MachineName.Name = "HmiLabel_MachineName"
        Me.HmiLabel_MachineName.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_MachineName.TabIndex = 5
        '
        'HmiTextBox_Adress
        '
        Me.HmiTextBox_Adress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Adress.Location = New System.Drawing.Point(124, 3)
        Me.HmiTextBox_Adress.Name = "HmiTextBox_Adress"
        Me.HmiTextBox_Adress.Number = 0
        Me.HmiTextBox_Adress.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Adress.TabIndex = 1
        Me.HmiTextBox_Adress.TextBoxReadOnly = False
        Me.HmiTextBox_Adress.ValueType = GetType(String)
        '
        'HmiLabel_Adress
        '
        Me.HmiLabel_Adress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Adress.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Adress.Name = "HmiLabel_Adress"
        Me.HmiLabel_Adress.Size = New System.Drawing.Size(115, 33)
        Me.HmiLabel_Adress.TabIndex = 4
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 246)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InitUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_Adress As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Adress As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_MachineName As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MachineName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_TraceID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_TraceID As Kochi.HMI.MainControl.UI.HMITextBox
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiLabel_Enable As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
    Friend WithEvents HmiTextBox_Description As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Description As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ProcessName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ProcessName As Kochi.HMI.MainControl.UI.HMILabel
End Class
