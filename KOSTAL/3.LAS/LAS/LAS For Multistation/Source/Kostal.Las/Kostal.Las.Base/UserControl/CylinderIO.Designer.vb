<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CylinderIO
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
        Me.Button_NameB = New System.Windows.Forms.Button()
        Me.Panel_IndicateA = New System.Windows.Forms.Panel()
        Me.Panel_IndicateB = New System.Windows.Forms.Panel()
        Me.Button_NameA = New System.Windows.Forms.Button()
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
        Me.TableLayoutPanel_Body.ColumnCount = 4
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_NameB, 2, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_IndicateA, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_IndicateB, 3, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_NameA, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(145, 34)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Button_NameB
        '
        Me.Button_NameB.BackColor = System.Drawing.Color.White
        Me.Button_NameB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_NameB.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button_NameB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NameB.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button_NameB.Location = New System.Drawing.Point(72, 1)
        Me.Button_NameB.Margin = New System.Windows.Forms.Padding(1, 1, 0, 1)
        Me.Button_NameB.Name = "Button_NameB"
        Me.Button_NameB.Size = New System.Drawing.Size(62, 32)
        Me.Button_NameB.TabIndex = 4
        Me.Button_NameB.UseVisualStyleBackColor = False
        '
        'Panel_IndicateA
        '
        Me.Panel_IndicateA.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel_IndicateA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_IndicateA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_IndicateA.Location = New System.Drawing.Point(0, 1)
        Me.Panel_IndicateA.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.Panel_IndicateA.Name = "Panel_IndicateA"
        Me.Panel_IndicateA.Size = New System.Drawing.Size(8, 32)
        Me.Panel_IndicateA.TabIndex = 3
        '
        'Panel_IndicateB
        '
        Me.Panel_IndicateB.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel_IndicateB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_IndicateB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_IndicateB.Location = New System.Drawing.Point(134, 1)
        Me.Panel_IndicateB.Margin = New System.Windows.Forms.Padding(0, 1, 1, 1)
        Me.Panel_IndicateB.Name = "Panel_IndicateB"
        Me.Panel_IndicateB.Size = New System.Drawing.Size(10, 32)
        Me.Panel_IndicateB.TabIndex = 2
        '
        'Button_NameA
        '
        Me.Button_NameA.BackColor = System.Drawing.Color.White
        Me.Button_NameA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_NameA.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button_NameA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_NameA.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button_NameA.Location = New System.Drawing.Point(8, 1)
        Me.Button_NameA.Margin = New System.Windows.Forms.Padding(0, 1, 1, 1)
        Me.Button_NameA.Name = "Button_NameA"
        Me.Button_NameA.Size = New System.Drawing.Size(62, 32)
        Me.Button_NameA.TabIndex = 1
        Me.Button_NameA.UseVisualStyleBackColor = False
        '
        'CylinderIO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Bottom)
        Me.Name = "CylinderIO"
        Me.Size = New System.Drawing.Size(147, 36)
        Me.Panel_Bottom.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Bottom As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_IndicateB As System.Windows.Forms.Panel
    Friend WithEvents Button_NameA As System.Windows.Forms.Button
    Friend WithEvents Panel_IndicateA As System.Windows.Forms.Panel
    Friend WithEvents Button_NameB As System.Windows.Forms.Button
End Class
