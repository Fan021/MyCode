
Partial Class ScheduleUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    Sub New()
        InitializeComponent()
    End Sub
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Protected Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.UI = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.DG_Schedule = New System.Windows.Forms.DataGridView()
        Me.UI_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label_Mode = New System.Windows.Forms.Label()
        Me.cmbSchedules_01 = New System.Windows.Forms.ComboBox()
        Me.btnAlternateScheduleAbort = New System.Windows.Forms.Button()
        Me.btnScheduleSelected = New System.Windows.Forms.Button()
        Me.UI_Name = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.lblCurrentScheduleName = New System.Windows.Forms.Label()
        Me.lblCurrentSchedule = New System.Windows.Forms.Label()
        Me.DockPanel.SuspendLayout()
        Me.UI.SuspendLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UI_Title.SuspendLayout()
        Me.UI_Name.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.BackColor = System.Drawing.Color.White
        Me.DockPanel.Controls.Add(Me.UI)
        Me.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(693, 267)
        Me.DockPanel.TabIndex = 0
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.DG_Schedule, 0, 2)
        Me.UI.Controls.Add(Me.UI_Title, 0, 0)
        Me.UI.Controls.Add(Me.UI_Name, 0, 1)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 3
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.UI.Size = New System.Drawing.Size(693, 267)
        Me.UI.TabIndex = 21
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Schedule.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Schedule.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Schedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Schedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Schedule.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Schedule.Location = New System.Drawing.Point(3, 122)
        Me.DG_Schedule.Name = "DG_Schedule"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
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
        Me.DG_Schedule.Size = New System.Drawing.Size(687, 142)
        Me.DG_Schedule.TabIndex = 15
        '
        'UI_Title
        '
        Me.UI_Title.ColumnCount = 2
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.Controls.Add(Me.Label_Mode, 0, 0)
        Me.UI_Title.Controls.Add(Me.cmbSchedules_01, 0, 1)
        Me.UI_Title.Controls.Add(Me.btnAlternateScheduleAbort, 1, 1)
        Me.UI_Title.Controls.Add(Me.btnScheduleSelected, 1, 0)
        Me.UI_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Title.Location = New System.Drawing.Point(3, 3)
        Me.UI_Title.Name = "UI_Title"
        Me.UI_Title.RowCount = 2
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.UI_Title.Size = New System.Drawing.Size(687, 60)
        Me.UI_Title.TabIndex = 0
        '
        'Label_Mode
        '
        Me.Label_Mode.AutoSize = True
        Me.Label_Mode.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label_Mode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label_Mode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label_Mode.Location = New System.Drawing.Point(3, 0)
        Me.Label_Mode.Name = "Label_Mode"
        Me.Label_Mode.Size = New System.Drawing.Size(126, 30)
        Me.Label_Mode.TabIndex = 19
        Me.Label_Mode.Text = "lblSelectSchedule"
        Me.Label_Mode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSchedules_01
        '
        Me.cmbSchedules_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSchedules_01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSchedules_01.Location = New System.Drawing.Point(3, 33)
        Me.cmbSchedules_01.Name = "cmbSchedules_01"
        Me.cmbSchedules_01.Size = New System.Drawing.Size(543, 20)
        Me.cmbSchedules_01.TabIndex = 12
        '
        'btnAlternateScheduleAbort
        '
        Me.btnAlternateScheduleAbort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAlternateScheduleAbort.Location = New System.Drawing.Point(552, 33)
        Me.btnAlternateScheduleAbort.Name = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.Size = New System.Drawing.Size(132, 24)
        Me.btnAlternateScheduleAbort.TabIndex = 0
        Me.btnAlternateScheduleAbort.Text = "btnAlternateScheduleAbort"
        Me.btnAlternateScheduleAbort.UseVisualStyleBackColor = True
        '
        'btnScheduleSelected
        '
        Me.btnScheduleSelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnScheduleSelected.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.btnScheduleSelected.Location = New System.Drawing.Point(552, 3)
        Me.btnScheduleSelected.Name = "btnScheduleSelected"
        Me.btnScheduleSelected.Size = New System.Drawing.Size(132, 24)
        Me.btnScheduleSelected.TabIndex = 15
        Me.btnScheduleSelected.Text = "btnScheduleSelected"
        Me.btnScheduleSelected.UseVisualStyleBackColor = True
        '
        'UI_Name
        '
        Me.UI_Name.ColumnCount = 2
        Me.UI_Name.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Name.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.UI_Name.Controls.Add(Me.lblCurrentScheduleName, 1, 0)
        Me.UI_Name.Controls.Add(Me.lblCurrentSchedule, 0, 0)
        Me.UI_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Name.Location = New System.Drawing.Point(3, 69)
        Me.UI_Name.Name = "UI_Name"
        Me.UI_Name.RowCount = 1
        Me.UI_Name.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Name.Size = New System.Drawing.Size(687, 47)
        Me.UI_Name.TabIndex = 1
        '
        'lblCurrentScheduleName
        '
        Me.lblCurrentScheduleName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentScheduleName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCurrentScheduleName.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentScheduleName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentScheduleName.Location = New System.Drawing.Point(140, 0)
        Me.lblCurrentScheduleName.Name = "lblCurrentScheduleName"
        Me.lblCurrentScheduleName.Size = New System.Drawing.Size(205, 47)
        Me.lblCurrentScheduleName.TabIndex = 18
        Me.lblCurrentScheduleName.Text = "CurrentScheduleName"
        Me.lblCurrentScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.AutoSize = True
        Me.lblCurrentSchedule.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCurrentSchedule.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(3, 0)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(126, 47)
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
        Me.UI.ResumeLayout(False)
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UI_Title.ResumeLayout(False)
        Me.UI_Title.PerformLayout()
        Me.UI_Name.ResumeLayout(False)
        Me.UI_Name.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents lblCurrentScheduleName As System.Windows.Forms.Label
    Friend WithEvents btnScheduleSelected As System.Windows.Forms.Button
    Friend WithEvents cmbSchedules_01 As System.Windows.Forms.ComboBox
    Friend WithEvents btnAlternateScheduleAbort As System.Windows.Forms.Button
    Friend WithEvents Label_Mode As System.Windows.Forms.Label
    Friend WithEvents lblCurrentSchedule As Windows.Forms.Label
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents DG_Schedule As Windows.Forms.DataGridView
    Friend WithEvents UI_Title As HMITableLayoutPanel
    Friend WithEvents UI_Name As HMITableLayoutPanel
    Private components As ComponentModel.IContainer
End Class
