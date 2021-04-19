<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LAS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LAS))
        Me.GroupBox_NewPart_Msg = New System.Windows.Forms.GroupBox()
        Me.Label_Msg = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ContextMenuStrip_Pic = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Open = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Select = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Cut = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Move = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Style_Zoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemd_Default = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Save = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_StretchImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_AutoSize = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_CenterImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Zoom = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog_Pic = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox_NewPart_Msg.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.ContextMenuStrip_Pic.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_NewPart_Msg
        '
        Me.GroupBox_NewPart_Msg.Controls.Add(Me.Label_Msg)
        Me.GroupBox_NewPart_Msg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_NewPart_Msg.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox_NewPart_Msg.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox_NewPart_Msg.Name = "GroupBox_NewPart_Msg"
        Me.GroupBox_NewPart_Msg.Size = New System.Drawing.Size(1264, 170)
        Me.GroupBox_NewPart_Msg.TabIndex = 1
        Me.GroupBox_NewPart_Msg.TabStop = False
        Me.GroupBox_NewPart_Msg.Text = "GroupBox1"
        '
        'Label_Msg
        '
        Me.Label_Msg.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label_Msg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Msg.Font = New System.Drawing.Font("Arial", 48.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Msg.Location = New System.Drawing.Point(3, 17)
        Me.Label_Msg.Name = "Label_Msg"
        Me.Label_Msg.Size = New System.Drawing.Size(1258, 150)
        Me.Label_Msg.TabIndex = 0
        Me.Label_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.72727!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox_NewPart_Msg, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 816)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1264, 170)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'ContextMenuStrip_Pic
        '
        Me.ContextMenuStrip_Pic.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Open, Me.ToolStripMenuItem_Select, Me.ToolStripMenuItem_Cut, Me.ToolStripMenuItem_Move, Me.ToolStripMenuItem_Style_Zoom, Me.ToolStripMenuItemd_Default, Me.ToolStripMenuItem_Save, Me.ToolStripMenuItem_StretchImage, Me.ToolStripMenuItem_AutoSize, Me.ToolStripMenuItem_CenterImage, Me.ToolStripMenuItem_Zoom})
        Me.ContextMenuStrip_Pic.Name = "ContextMenuStrip_Pic"
        Me.ContextMenuStrip_Pic.Size = New System.Drawing.Size(253, 246)
        '
        'ToolStripMenuItem_Open
        '
        Me.ToolStripMenuItem_Open.BackColor = System.Drawing.Color.White
        Me.ToolStripMenuItem_Open.Name = "ToolStripMenuItem_Open"
        Me.ToolStripMenuItem_Open.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Open.Text = "ToolStripMenuItem_Open"
        '
        'ToolStripMenuItem_Select
        '
        Me.ToolStripMenuItem_Select.Name = "ToolStripMenuItem_Select"
        Me.ToolStripMenuItem_Select.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Select.Text = "ToolStripMenuItem_Select"
        '
        'ToolStripMenuItem_Cut
        '
        Me.ToolStripMenuItem_Cut.Name = "ToolStripMenuItem_Cut"
        Me.ToolStripMenuItem_Cut.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Cut.Text = "ToolStripMenuItem_Cut"
        '
        'ToolStripMenuItem_Move
        '
        Me.ToolStripMenuItem_Move.Name = "ToolStripMenuItem_Move"
        Me.ToolStripMenuItem_Move.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Move.Text = "ToolStripMenuItem_Move"
        '
        'ToolStripMenuItem_Style_Zoom
        '
        Me.ToolStripMenuItem_Style_Zoom.Name = "ToolStripMenuItem_Style_Zoom"
        Me.ToolStripMenuItem_Style_Zoom.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Style_Zoom.Text = "ToolStripMenuItem_Style_Zoom"
        '
        'ToolStripMenuItemd_Default
        '
        Me.ToolStripMenuItemd_Default.Name = "ToolStripMenuItemd_Default"
        Me.ToolStripMenuItemd_Default.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItemd_Default.Text = "ToolStripMenuItemd_Default"
        '
        'ToolStripMenuItem_Save
        '
        Me.ToolStripMenuItem_Save.Name = "ToolStripMenuItem_Save"
        Me.ToolStripMenuItem_Save.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Save.Text = "ToolStripMenuItem_Save"
        '
        'ToolStripMenuItem_StretchImage
        '
        Me.ToolStripMenuItem_StretchImage.Name = "ToolStripMenuItem_StretchImage"
        Me.ToolStripMenuItem_StretchImage.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_StretchImage.Text = "ToolStripMenuItem_StretchImage"
        '
        'ToolStripMenuItem_AutoSize
        '
        Me.ToolStripMenuItem_AutoSize.Name = "ToolStripMenuItem_AutoSize"
        Me.ToolStripMenuItem_AutoSize.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_AutoSize.Text = "ToolStripMenuItem_AutoSize"
        '
        'ToolStripMenuItem_CenterImage
        '
        Me.ToolStripMenuItem_CenterImage.Name = "ToolStripMenuItem_CenterImage"
        Me.ToolStripMenuItem_CenterImage.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_CenterImage.Text = "ToolStripMenuItem_CenterImage"
        '
        'ToolStripMenuItem_Zoom
        '
        Me.ToolStripMenuItem_Zoom.Name = "ToolStripMenuItem_Zoom"
        Me.ToolStripMenuItem_Zoom.Size = New System.Drawing.Size(252, 22)
        Me.ToolStripMenuItem_Zoom.Text = "ToolStripMenuItem_Zoom"
        '
        'OpenFileDialog_Pic
        '
        Me.OpenFileDialog_Pic.FileName = "OpenFileDialog_Pic"
        '
        'LAS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 986)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LAS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LAS"
        Me.GroupBox_NewPart_Msg.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ContextMenuStrip_Pic.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox_NewPart_Msg As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label_Msg As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip_Pic As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Open As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Cut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Move As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Style_Zoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Save As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_StretchImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_AutoSize As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_CenterImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Zoom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog_Pic As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStripMenuItem_Select As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemd_Default As System.Windows.Forms.ToolStripMenuItem
End Class
