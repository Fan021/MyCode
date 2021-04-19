<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowMaintenance
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
        Me.HmiTableLayoutPanel_Body = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.TextBox_Msg = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Confirm = New System.Windows.Forms.Button()
        Me.HmiTableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'HmiTableLayoutPanel_Body
        '
        Me.HmiTableLayoutPanel_Body.ColumnCount = 1
        Me.HmiTableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Body.Controls.Add(Me.TextBox_Msg, 0, 0)
        Me.HmiTableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 1)
        Me.HmiTableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body.Name = "HmiTableLayoutPanel_Body"
        Me.HmiTableLayoutPanel_Body.RowCount = 2
        Me.HmiTableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.HmiTableLayoutPanel_Body.Size = New System.Drawing.Size(600, 495)
        Me.HmiTableLayoutPanel_Body.TabIndex = 0
        '
        'TextBox_Msg
        '
        Me.TextBox_Msg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Msg.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_Msg.Multiline = True
        Me.TextBox_Msg.Name = "TextBox_Msg"
        Me.TextBox_Msg.ReadOnly = True
        Me.TextBox_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Msg.Size = New System.Drawing.Size(594, 449)
        Me.TextBox_Msg.TabIndex = 0
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 3
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Controls.Add(Me.Button_Confirm, 1, 0)
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(3, 458)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(594, 34)
        Me.TableLayoutPanel_Bottom.TabIndex = 1
        '
        'Button_Confirm
        '
        Me.Button_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Confirm.Location = New System.Drawing.Point(200, 3)
        Me.Button_Confirm.Name = "Button_Confirm"
        Me.Button_Confirm.Size = New System.Drawing.Size(194, 28)
        Me.Button_Confirm.TabIndex = 0
        Me.Button_Confirm.Text = "Maintenance"
        Me.Button_Confirm.UseVisualStyleBackColor = True
        '
        'ShowMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.HmiTableLayoutPanel_Body)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ShowMaintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ShowMaintenance"
        Me.HmiTableLayoutPanel_Body.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body.PerformLayout()
        Me.TableLayoutPanel_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HmiTableLayoutPanel_Body As HMITableLayoutPanel
    Public WithEvents TextBox_Msg As Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Bottom As Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Confirm As Windows.Forms.Button
End Class
