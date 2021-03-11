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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid_Button = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiButton_On = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Off = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TextBox_Result = New System.Windows.Forms.TextBox()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Button.SuspendLayout()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_Button, 1, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TextBox_Result, 2, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(3, 29)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(461, 498)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid_Button
        '
        Me.TableLayoutPanel_Body_Mid_Button.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Button.Controls.Add(Me.HmiButton_On, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Button.Controls.Add(Me.HmiButton_Off, 0, 2)
        Me.TableLayoutPanel_Body_Mid_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Button.Location = New System.Drawing.Point(95, 3)
        Me.TableLayoutPanel_Body_Mid_Button.Name = "TableLayoutPanel_Body_Mid_Button"
        Me.TableLayoutPanel_Body_Mid_Button.RowCount = 4
        Me.TableLayoutPanel_Body_Mid_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Mid_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Mid_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Mid_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Mid_Button.Size = New System.Drawing.Size(86, 243)
        Me.TableLayoutPanel_Body_Mid_Button.TabIndex = 0
        '
        'HmiButton_On
        '
        Me.HmiButton_On.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_On.Location = New System.Drawing.Point(3, 83)
        Me.HmiButton_On.MarginHeight = 6
        Me.HmiButton_On.Name = "HmiButton_On"
        Me.HmiButton_On.Size = New System.Drawing.Size(80, 34)
        Me.HmiButton_On.TabIndex = 0
        '
        'HmiButton_Off
        '
        Me.HmiButton_Off.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Off.Location = New System.Drawing.Point(3, 123)
        Me.HmiButton_Off.MarginHeight = 6
        Me.HmiButton_Off.Name = "HmiButton_Off"
        Me.HmiButton_Off.Size = New System.Drawing.Size(80, 34)
        Me.HmiButton_Off.TabIndex = 1
        '
        'TextBox_Result
        '
        Me.TextBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Result.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TextBox_Result.Location = New System.Drawing.Point(187, 3)
        Me.TextBox_Result.Multiline = True
        Me.TextBox_Result.Name = "TextBox_Result"
        Me.TextBox_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Result.Size = New System.Drawing.Size(271, 243)
        Me.TextBox_Result.TabIndex = 1
        Me.TextBox_Result.WordWrap = False
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
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_Button.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Mid_Button As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiButton_On As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Off As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents TextBox_Result As System.Windows.Forms.TextBox
End Class
