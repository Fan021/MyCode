<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PicturePosition
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
        Me.UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox_Body = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel_Body_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Y = New System.Windows.Forms.Label()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.Label_Radius = New System.Windows.Forms.Label()
        Me.HmiTextBox_Y = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_X = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Radius = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Confirm = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        CType(Me.PictureBox_Body, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Body_Left.SuspendLayout()
        Me.SuspendLayout()
        '
        'UI
        '
        Me.UI.BackColor = System.Drawing.Color.White
        Me.UI.Controls.Add(Me.TableLayoutPanel_Body)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.Size = New System.Drawing.Size(531, 544)
        Me.UI.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.PictureBox_Body, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Left, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(531, 544)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'PictureBox_Body
        '
        Me.PictureBox_Body.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_Body.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox_Body.Name = "PictureBox_Body"
        Me.PictureBox_Body.Size = New System.Drawing.Size(424, 544)
        Me.PictureBox_Body.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_Body.TabIndex = 0
        Me.PictureBox_Body.TabStop = False
        '
        'TableLayoutPanel_Body_Left
        '
        Me.TableLayoutPanel_Body_Left.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.HmiTextBox_Y, 0, 4)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.Label_Y, 0, 4)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.HmiTextBox_X, 0, 3)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.Label_X, 0, 2)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.Label_Radius, 0, 0)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.HmiTextBox_Radius, 0, 1)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.HmiButton_Confirm, 0, 6)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.HmiButton_Cancel, 0, 7)
        Me.TableLayoutPanel_Body_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left.Location = New System.Drawing.Point(427, 3)
        Me.TableLayoutPanel_Body_Left.Name = "TableLayoutPanel_Body_Left"
        Me.TableLayoutPanel_Body_Left.RowCount = 9
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Left.Size = New System.Drawing.Size(101, 538)
        Me.TableLayoutPanel_Body_Left.TabIndex = 1
        '
        'Label_Y
        '
        Me.Label_Y.AutoSize = True
        Me.Label_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Y.Location = New System.Drawing.Point(3, 159)
        Me.Label_Y.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Y.Name = "Label_Y"
        Me.Label_Y.Size = New System.Drawing.Size(95, 33)
        Me.Label_Y.TabIndex = 6
        Me.Label_Y.Text = "Label1"
        Me.Label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_X
        '
        Me.Label_X.AutoSize = True
        Me.Label_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_X.Location = New System.Drawing.Point(3, 81)
        Me.Label_X.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_X.Name = "Label_X"
        Me.Label_X.Size = New System.Drawing.Size(95, 33)
        Me.Label_X.TabIndex = 4
        Me.Label_X.Text = "Label1"
        Me.Label_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Radius
        '
        Me.Label_Radius.AutoSize = True
        Me.Label_Radius.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Radius.Location = New System.Drawing.Point(3, 3)
        Me.Label_Radius.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Radius.Name = "Label_Radius"
        Me.Label_Radius.Size = New System.Drawing.Size(95, 33)
        Me.Label_Radius.TabIndex = 0
        Me.Label_Radius.Text = "Label1"
        Me.Label_Radius.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_Y
        '
        Me.HmiTextBox_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Y.Location = New System.Drawing.Point(3, 198)
        Me.HmiTextBox_Y.Name = "HmiTextBox_Y"
        Me.HmiTextBox_Y.Size = New System.Drawing.Size(95, 33)
        Me.HmiTextBox_Y.TabIndex = 7
        Me.HmiTextBox_Y.TextBoxReadOnly = False
        '
        'HmiTextBox_X
        '
        Me.HmiTextBox_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_X.Location = New System.Drawing.Point(3, 120)
        Me.HmiTextBox_X.Name = "HmiTextBox_X"
        Me.HmiTextBox_X.Size = New System.Drawing.Size(95, 33)
        Me.HmiTextBox_X.TabIndex = 5
        Me.HmiTextBox_X.TextBoxReadOnly = False
        '
        'HmiTextBox_Radius
        '
        Me.HmiTextBox_Radius.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Radius.Location = New System.Drawing.Point(3, 42)
        Me.HmiTextBox_Radius.Name = "HmiTextBox_Radius"
        Me.HmiTextBox_Radius.Size = New System.Drawing.Size(95, 33)
        Me.HmiTextBox_Radius.TabIndex = 1
        Me.HmiTextBox_Radius.TextBoxReadOnly = False
        '
        'HmiButton_Confirm
        '
        Me.HmiButton_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Confirm.Location = New System.Drawing.Point(3, 237)
        Me.HmiButton_Confirm.MarginHeight = 6
        Me.HmiButton_Confirm.Name = "HmiButton_Confirm"
        Me.HmiButton_Confirm.Size = New System.Drawing.Size(95, 33)
        Me.HmiButton_Confirm.TabIndex = 2
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(3, 276)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(95, 33)
        Me.HmiButton_Cancel.TabIndex = 3
        '
        'PicturePosition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 544)
        Me.Controls.Add(Me.UI)
        Me.Name = "PicturePosition"
        Me.Text = "Form1"
        Me.UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        CType(Me.PictureBox_Body, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Body_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox_Body As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel_Body_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label_Radius As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_Radius As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Confirm As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Y As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_Y As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_X As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_X As System.Windows.Forms.Label
End Class
