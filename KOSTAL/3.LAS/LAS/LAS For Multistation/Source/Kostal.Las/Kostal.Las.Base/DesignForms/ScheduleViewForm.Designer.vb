Imports System.Windows.Forms
Imports System.Windows.Forms.Form
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScheduleViewFrom
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScheduleViewFrom))
        Me.ContextMenuStrip_Schedule = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Hide_Row = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem_Reset = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.DG_Schedule = New Kostal.Las.Base.RowMergeView(Me.components)
        Me.ContextMenuStrip_Schedule.SuspendLayout()
        Me.DesignPanel.SuspendLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip_Schedule
        '
        Me.ContextMenuStrip_Schedule.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip_Schedule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Hide_Row, Me.ToolStripMenuItem_Reset})
        Me.ContextMenuStrip_Schedule.Name = "ContextMenuStrip_Schedule"
        Me.ContextMenuStrip_Schedule.Size = New System.Drawing.Size(236, 48)
        '
        'ToolStripMenuItem_Hide_Row
        '
        Me.ToolStripMenuItem_Hide_Row.Name = "ToolStripMenuItem_Hide_Row"
        Me.ToolStripMenuItem_Hide_Row.Size = New System.Drawing.Size(235, 22)
        Me.ToolStripMenuItem_Hide_Row.Text = "ToolStripMenuItem_Hide_Row"
        '
        'ToolStripMenuItem_Reset
        '
        Me.ToolStripMenuItem_Reset.Name = "ToolStripMenuItem_Reset"
        Me.ToolStripMenuItem_Reset.Size = New System.Drawing.Size(235, 22)
        Me.ToolStripMenuItem_Reset.Text = "ToolStripMenuItem_Reset"
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.DG_Schedule)
        Me.DesignPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignPanel.Location = New System.Drawing.Point(0, 0)
        Me.DesignPanel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(848, 453)
        Me.DesignPanel.TabIndex = 2
        '
        'DG_Schedule
        '
        Me.DG_Schedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_Schedule.ContextMenuStrip = Me.ContextMenuStrip_Schedule
        Me.DG_Schedule.Location = New System.Drawing.Point(12, 39)
        Me.DG_Schedule.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control
        Me.DG_Schedule.MergeColumnNames = CType(resources.GetObject("DG_Schedule.MergeColumnNames"), System.Collections.Generic.List(Of String))
        Me.DG_Schedule.Name = "DG_Schedule"
        Me.DG_Schedule.ReadOnly = True
        Me.DG_Schedule.RowTemplate.Height = 23
        Me.DG_Schedule.Size = New System.Drawing.Size(824, 401)
        Me.DG_Schedule.TabIndex = 1
        '
        'ScheduleView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(848, 453)
        Me.Controls.Add(Me.DesignPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ScheduleView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScheduleView"
        Me.ContextMenuStrip_Schedule.ResumeLayout(False)
        Me.DesignPanel.ResumeLayout(False)
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ContextMenuStrip_Schedule As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Hide_Row As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_Reset As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DG_Schedule As Kostal.Las.Base.RowMergeView
    Friend WithEvents DesignPanel As Panel
End Class
