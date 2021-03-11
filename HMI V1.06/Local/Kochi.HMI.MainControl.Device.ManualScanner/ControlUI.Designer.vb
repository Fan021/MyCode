<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlUI
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
        Me.GroupBox_Result = New System.Windows.Forms.GroupBox()
        Me.TextBox_Result = New System.Windows.Forms.TextBox()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.GroupBox_Result.SuspendLayout()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(623, 530)
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.GroupBox_Result, 1, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(3, 29)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(617, 498)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'GroupBox_Result
        '
        Me.TableLayoutPanel_Body_Mid.SetColumnSpan(Me.GroupBox_Result, 2)
        Me.GroupBox_Result.Controls.Add(Me.TextBox_Result)
        Me.GroupBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Result.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Result.Location = New System.Drawing.Point(126, 3)
        Me.GroupBox_Result.Name = "GroupBox_Result"
        Me.GroupBox_Result.Size = New System.Drawing.Size(488, 243)
        Me.GroupBox_Result.TabIndex = 3
        Me.GroupBox_Result.TabStop = False
        Me.GroupBox_Result.Text = "GroupBox1"
        '
        'TextBox_Result
        '
        Me.TextBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Result.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TextBox_Result.Location = New System.Drawing.Point(3, 23)
        Me.TextBox_Result.Multiline = True
        Me.TextBox_Result.Name = "TextBox_Result"
        Me.TextBox_Result.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Result.Size = New System.Drawing.Size(482, 217)
        Me.TextBox_Result.TabIndex = 2
        '
        'ControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ControlUI"
        Me.Text = "ControlUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.GroupBox_Result.ResumeLayout(False)
        Me.GroupBox_Result.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Result As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_Result As System.Windows.Forms.TextBox
End Class
