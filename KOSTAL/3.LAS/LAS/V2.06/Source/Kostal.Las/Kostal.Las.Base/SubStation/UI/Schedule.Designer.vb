<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScheduleUI
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
    Protected components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Protected Sub InitializeComponent()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCurrentScheduleName = New System.Windows.Forms.Label()
        Me.DG_Schedule = New System.Windows.Forms.DataGridView()
        Me.Label_Mode = New System.Windows.Forms.Label()
        Me.btnAlternateScheduleAbort = New System.Windows.Forms.Button()
        Me.cmbSchedules_01 = New System.Windows.Forms.ComboBox()
        Me.btnScheduleSelected = New System.Windows.Forms.Button()
        Me.lblCurrentSchedule = New System.Windows.Forms.Label()
        Me.DockPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.BackColor = System.Drawing.Color.White
        Me.DockPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.DockPanel.Controls.Add(Me.Label_Mode)
        Me.DockPanel.Controls.Add(Me.btnAlternateScheduleAbort)
        Me.DockPanel.Controls.Add(Me.cmbSchedules_01)
        Me.DockPanel.Controls.Add(Me.btnScheduleSelected)
        Me.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(693, 267)
        Me.DockPanel.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DG_Schedule, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 123)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.74074!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.25926!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(693, 144)
        Me.TableLayoutPanel1.TabIndex = 20
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.4294!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.5706!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblCurrentSchedule, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.lblCurrentScheduleName, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(687, 23)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'lblCurrentScheduleName
        '
        Me.lblCurrentScheduleName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCurrentScheduleName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentScheduleName.Font = New System.Drawing.Font("SimSun", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentScheduleName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentScheduleName.Location = New System.Drawing.Point(122, 2)
        Me.lblCurrentScheduleName.Name = "lblCurrentScheduleName"
        Me.lblCurrentScheduleName.Size = New System.Drawing.Size(205, 21)
        Me.lblCurrentScheduleName.TabIndex = 18
        Me.lblCurrentScheduleName.Text = "CurrentScheduleName"
        Me.lblCurrentScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DG_Schedule
        '
        Me.DG_Schedule.AllowUserToAddRows = False
        Me.DG_Schedule.AllowUserToDeleteRows = False
        Me.DG_Schedule.AllowUserToResizeColumns = False
        Me.DG_Schedule.AllowUserToResizeRows = False
        Me.DG_Schedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Schedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Schedule.DefaultCellStyle = DataGridViewCellStyle11
        Me.DG_Schedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Schedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Schedule.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Schedule.Location = New System.Drawing.Point(3, 32)
        Me.DG_Schedule.Name = "DG_Schedule"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DG_Schedule.RowHeadersWidth = 20
        Me.DG_Schedule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Schedule.RowTemplate.Height = 23
        Me.DG_Schedule.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Schedule.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.DG_Schedule.Size = New System.Drawing.Size(687, 109)
        Me.DG_Schedule.TabIndex = 14
        '
        'Label_Mode
        '
        Me.Label_Mode.AutoSize = True
        Me.Label_Mode.Font = New System.Drawing.Font("SimSun", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label_Mode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label_Mode.Location = New System.Drawing.Point(6, 9)
        Me.Label_Mode.Name = "Label_Mode"
        Me.Label_Mode.Size = New System.Drawing.Size(126, 13)
        Me.Label_Mode.TabIndex = 19
        Me.Label_Mode.Text = "lblSelectSchedule"
        Me.Label_Mode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAlternateScheduleAbort
        '
        Me.btnAlternateScheduleAbort.Location = New System.Drawing.Point(528, 60)
        Me.btnAlternateScheduleAbort.Name = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.Size = New System.Drawing.Size(149, 27)
        Me.btnAlternateScheduleAbort.TabIndex = 0
        Me.btnAlternateScheduleAbort.Text = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.UseVisualStyleBackColor = True
        '
        'cmbSchedules_01
        '
        Me.cmbSchedules_01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSchedules_01.Location = New System.Drawing.Point(4, 30)
        Me.cmbSchedules_01.Name = "cmbSchedules_01"
        Me.cmbSchedules_01.Size = New System.Drawing.Size(518, 20)
        Me.cmbSchedules_01.TabIndex = 12
        '
        'btnScheduleSelected
        '
        Me.btnScheduleSelected.Font = New System.Drawing.Font("SimSun", 10.0!)
        Me.btnScheduleSelected.Location = New System.Drawing.Point(528, 30)
        Me.btnScheduleSelected.Name = "btnScheduleSelected"
        Me.btnScheduleSelected.Size = New System.Drawing.Size(149, 27)
        Me.btnScheduleSelected.TabIndex = 15
        Me.btnScheduleSelected.Text = "btnScheduleSelected"
        Me.btnScheduleSelected.UseVisualStyleBackColor = True
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCurrentSchedule.AutoSize = True
        Me.lblCurrentSchedule.Font = New System.Drawing.Font("SimSun", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(3, 2)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(112, 21)
        Me.lblCurrentSchedule.TabIndex = 17
        Me.lblCurrentSchedule.Text = "lblCurrentSchedule"
        Me.lblCurrentSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ScheduleUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 267)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "ScheduleUI"
        Me.Text = "Schedule"
        Me.DockPanel.ResumeLayout(False)
        Me.DockPanel.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents lblCurrentScheduleName As System.Windows.Forms.Label
    Friend WithEvents btnScheduleSelected As System.Windows.Forms.Button
    Friend WithEvents DG_Schedule As System.Windows.Forms.DataGridView
    Friend WithEvents cmbSchedules_01 As System.Windows.Forms.ComboBox
    Friend WithEvents btnAlternateScheduleAbort As System.Windows.Forms.Button
    Friend WithEvents Label_Mode As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCurrentSchedule As Windows.Forms.Label
End Class
