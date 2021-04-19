
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenShortCutForm
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTableLayoutPanel_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.btnResetFail = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnResetReference = New System.Windows.Forms.Button()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.HmiTableLayoutPanel_Left.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.Color.White
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Right, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTableLayoutPanel_Left, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Right
        '
        Me.TableLayoutPanel_Right.ColumnCount = 1
        Me.TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Right.Location = New System.Drawing.Point(140, 0)
        Me.TableLayoutPanel_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Right.Name = "TableLayoutPanel_Right"
        Me.TableLayoutPanel_Right.RowCount = 1
        Me.TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.Size = New System.Drawing.Size(233, 530)
        Me.TableLayoutPanel_Right.TabIndex = 1
        '
        'HmiTableLayoutPanel_Left
        '
        Me.HmiTableLayoutPanel_Left.ColumnCount = 3
        Me.HmiTableLayoutPanel_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.HmiTableLayoutPanel_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Left.Controls.Add(Me.btnResetReference, 1, 2)
        Me.HmiTableLayoutPanel_Left.Controls.Add(Me.btnResetFail, 1, 1)
        Me.HmiTableLayoutPanel_Left.Controls.Add(Me.btnReset, 1, 0)
        Me.HmiTableLayoutPanel_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Left.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Left.Name = "HmiTableLayoutPanel_Left"
        Me.HmiTableLayoutPanel_Left.RowCount = 4
        Me.HmiTableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.HmiTableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.HmiTableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.HmiTableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Left.Size = New System.Drawing.Size(134, 524)
        Me.HmiTableLayoutPanel_Left.TabIndex = 2
        '
        'btnResetFail
        '
        Me.btnResetFail.BackColor = System.Drawing.Color.White
        Me.btnResetFail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnResetFail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetFail.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnResetFail.Location = New System.Drawing.Point(29, 43)
        Me.btnResetFail.Name = "btnResetFail"
        Me.btnResetFail.Size = New System.Drawing.Size(74, 34)
        Me.btnResetFail.TabIndex = 9
        Me.btnResetFail.Text = "重置不良"
        Me.btnResetFail.UseVisualStyleBackColor = False
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.White
        Me.btnReset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnReset.Location = New System.Drawing.Point(29, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(74, 34)
        Me.btnReset.TabIndex = 8
        Me.btnReset.Text = "重置"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.SystemColors.Control
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(61, 4)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(275, 28)
        Me.miniToolStrip.TabIndex = 11
        '
        'btnResetReference
        '
        Me.btnResetReference.BackColor = System.Drawing.Color.White
        Me.btnResetReference.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnResetReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetReference.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold)
        Me.btnResetReference.Location = New System.Drawing.Point(29, 83)
        Me.btnResetReference.Name = "btnResetReference"
        Me.btnResetReference.Size = New System.Drawing.Size(74, 34)
        Me.btnResetReference.TabIndex = 10
        Me.btnResetReference.Text = "重置样件"
        Me.btnResetReference.UseVisualStyleBackColor = False
        '
        'ChildrenShortCutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenShortCutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenShortCutForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Left.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel_Body As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Public WithEvents miniToolStrip As ToolStrip
    Public WithEvents TableLayoutPanel_Right As TableLayoutPanel
    Public WithEvents HmiTableLayoutPanel_Left As TableLayoutPanel
    Public WithEvents btnReset As Button
    Public WithEvents btnResetFail As Button
    Public WithEvents btnResetReference As Button
End Class
