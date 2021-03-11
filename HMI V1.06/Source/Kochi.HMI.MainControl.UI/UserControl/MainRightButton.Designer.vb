<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainRightButton
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component List.
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
        Me.Panel_Bottom = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Indicate = New System.Windows.Forms.Panel()
        Me.Button_Name = New System.Windows.Forms.Button()
        Me.Panel_Bottom.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Bottom
        '
        Me.Panel_Bottom.BackColor = System.Drawing.Color.White
        Me.Panel_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Bottom.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Bottom.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Bottom.Name = "Panel_Bottom"
        Me.Panel_Bottom.Size = New System.Drawing.Size(147, 36)
        Me.Panel_Bottom.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Indicate, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_Name, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(145, 34)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Panel_Indicate
        '
        Me.Panel_Indicate.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel_Indicate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Indicate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Indicate.Location = New System.Drawing.Point(1, 1)
        Me.Panel_Indicate.Margin = New System.Windows.Forms.Padding(1, 1, 0, 1)
        Me.Panel_Indicate.Name = "Panel_Indicate"
        Me.Panel_Indicate.Size = New System.Drawing.Size(13, 32)
        Me.Panel_Indicate.TabIndex = 2
        '
        'Button_Name
        '
        Me.Button_Name.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Button_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Name.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button_Name.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Name.Location = New System.Drawing.Point(14, 1)
        Me.Button_Name.Margin = New System.Windows.Forms.Padding(0, 1, 1, 1)
        Me.Button_Name.Name = "Button_Name"
        Me.Button_Name.Size = New System.Drawing.Size(130, 32)
        Me.Button_Name.TabIndex = 1
        Me.Button_Name.UseVisualStyleBackColor = False
        '
        'MainRightButton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Bottom)
        Me.Name = "MainRightButton"
        Me.Size = New System.Drawing.Size(147, 36)
        Me.Panel_Bottom.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Bottom As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Indicate As System.Windows.Forms.Panel
    Friend WithEvents Button_Name As System.Windows.Forms.Button
End Class
