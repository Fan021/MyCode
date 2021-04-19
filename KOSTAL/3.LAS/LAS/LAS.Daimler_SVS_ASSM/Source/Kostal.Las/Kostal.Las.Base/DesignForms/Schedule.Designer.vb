<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Schedule
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.grpSchedule_01 = New System.Windows.Forms.GroupBox()
        Me.lblCurrentScheduleName = New System.Windows.Forms.Label()
        Me.lblCurrentSchedule = New System.Windows.Forms.Label()
        Me.btnScheduleSelected = New System.Windows.Forms.Button()
        Me.DG_Schedule = New System.Windows.Forms.DataGridView()
        Me.btnDummyRequest_01 = New System.Windows.Forms.Button()
        Me.cmbSchedules_01 = New System.Windows.Forms.ComboBox()
        Me.btnAlternateScheduleAbort = New System.Windows.Forms.Button()
        Me.DesignPanel.SuspendLayout()
        Me.grpSchedule_01.SuspendLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.grpSchedule_01)
        Me.DesignPanel.Location = New System.Drawing.Point(-6, 31)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(764, 495)
        Me.DesignPanel.TabIndex = 1
        '
        'grpSchedule_01
        '
        Me.grpSchedule_01.BackColor = System.Drawing.Color.White
        Me.grpSchedule_01.Controls.Add(Me.lblCurrentScheduleName)
        Me.grpSchedule_01.Controls.Add(Me.lblCurrentSchedule)
        Me.grpSchedule_01.Controls.Add(Me.btnScheduleSelected)
        Me.grpSchedule_01.Controls.Add(Me.DG_Schedule)
        Me.grpSchedule_01.Controls.Add(Me.btnDummyRequest_01)
        Me.grpSchedule_01.Controls.Add(Me.cmbSchedules_01)
        Me.grpSchedule_01.Controls.Add(Me.btnAlternateScheduleAbort)
        Me.grpSchedule_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSchedule_01.Location = New System.Drawing.Point(0, 0)
        Me.grpSchedule_01.Name = "grpSchedule_01"
        Me.grpSchedule_01.Size = New System.Drawing.Size(764, 495)
        Me.grpSchedule_01.TabIndex = 18
        Me.grpSchedule_01.TabStop = False
        Me.grpSchedule_01.Text = "Schedule"
        '
        'lblCurrentScheduleName
        '
        Me.lblCurrentScheduleName.AutoSize = True
        Me.lblCurrentScheduleName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentScheduleName.Font = New System.Drawing.Font("SimSun", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentScheduleName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentScheduleName.Location = New System.Drawing.Point(254, 266)
        Me.lblCurrentScheduleName.Name = "lblCurrentScheduleName"
        Me.lblCurrentScheduleName.Size = New System.Drawing.Size(205, 15)
        Me.lblCurrentScheduleName.TabIndex = 18
        Me.lblCurrentScheduleName.Text = "lblCurrentScheduleName"
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.AutoSize = True
        Me.lblCurrentSchedule.Font = New System.Drawing.Font("SimSun", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(149, 266)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(133, 13)
        Me.lblCurrentSchedule.TabIndex = 17
        Me.lblCurrentSchedule.Text = "lblCurrentSchedule"
        Me.lblCurrentSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnScheduleSelected
        '
        Me.btnScheduleSelected.Enabled = False
        Me.btnScheduleSelected.Font = New System.Drawing.Font("SimSun", 10.0!)
        Me.btnScheduleSelected.Location = New System.Drawing.Point(531, 115)
        Me.btnScheduleSelected.Name = "btnScheduleSelected"
        Me.btnScheduleSelected.Size = New System.Drawing.Size(149, 27)
        Me.btnScheduleSelected.TabIndex = 15
        Me.btnScheduleSelected.Text = "btnScheduleSelected"
        Me.btnScheduleSelected.UseVisualStyleBackColor = True
        '
        'DG_Schedule
        '
        Me.DG_Schedule.AllowUserToAddRows = False
        Me.DG_Schedule.AllowUserToDeleteRows = False
        Me.DG_Schedule.AllowUserToResizeColumns = False
        Me.DG_Schedule.AllowUserToResizeRows = False
        Me.DG_Schedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Schedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Schedule.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Schedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Schedule.Location = New System.Drawing.Point(151, 284)
        Me.DG_Schedule.Name = "DG_Schedule"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Schedule.RowHeadersWidth = 20
        Me.DG_Schedule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Schedule.RowTemplate.Height = 23
        Me.DG_Schedule.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Schedule.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.DG_Schedule.Size = New System.Drawing.Size(534, 65)
        Me.DG_Schedule.TabIndex = 14
        '
        'btnDummyRequest_01
        '
        Me.btnDummyRequest_01.Location = New System.Drawing.Point(531, 174)
        Me.btnDummyRequest_01.Name = "btnDummyRequest_01"
        Me.btnDummyRequest_01.Size = New System.Drawing.Size(149, 27)
        Me.btnDummyRequest_01.TabIndex = 13
        Me.btnDummyRequest_01.Text = "btnDummyRequest_01"
        Me.btnDummyRequest_01.UseVisualStyleBackColor = True
        Me.btnDummyRequest_01.Visible = False
        '
        'cmbSchedules_01
        '
        Me.cmbSchedules_01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSchedules_01.Location = New System.Drawing.Point(152, 115)
        Me.cmbSchedules_01.Name = "cmbSchedules_01"
        Me.cmbSchedules_01.Size = New System.Drawing.Size(373, 20)
        Me.cmbSchedules_01.TabIndex = 12
        '
        'btnAlternateScheduleAbort
        '
        Me.btnAlternateScheduleAbort.Location = New System.Drawing.Point(531, 145)
        Me.btnAlternateScheduleAbort.Name = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.Size = New System.Drawing.Size(149, 27)
        Me.btnAlternateScheduleAbort.TabIndex = 0
        Me.btnAlternateScheduleAbort.Text = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.UseVisualStyleBackColor = True
        '
        'Schedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 568)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "Schedule"
        Me.Text = "ScheduleView"
        Me.DesignPanel.ResumeLayout(False)
        Me.grpSchedule_01.ResumeLayout(False)
        Me.grpSchedule_01.PerformLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Friend WithEvents grpSchedule_01 As GroupBox
    Friend WithEvents lblCurrentScheduleName As Label
    Friend WithEvents lblCurrentSchedule As Label
    Friend WithEvents btnScheduleSelected As Button
    Friend WithEvents DG_Schedule As DataGridView
    Friend WithEvents btnDummyRequest_01 As Button
    Friend WithEvents cmbSchedules_01 As ComboBox
    Friend WithEvents btnAlternateScheduleAbort As Button
End Class
