Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScheduleSelectView
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAbort = New System.Windows.Forms.Button()
        Me.DG_Schedule = New System.Windows.Forms.DataGridView()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.cmbSchedules_01 = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCurrentScheduleName = New System.Windows.Forms.Label()
        Me.lblCurrentSchedule = New System.Windows.Forms.Label()
        Me.DesignPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.DesignPanel.Location = New System.Drawing.Point(45, 48)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(729, 537)
        Me.DesignPanel.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.DG_Schedule, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 1, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.47369!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(729, 537)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnAbort, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(75, 102)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(577, 34)
        Me.TableLayoutPanel3.TabIndex = 19
        '
        'btnAbort
        '
        Me.btnAbort.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnAbort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAbort.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnAbort.Location = New System.Drawing.Point(406, 3)
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Size = New System.Drawing.Size(74, 28)
        Me.btnAbort.TabIndex = 3
        Me.btnAbort.Text = "Abort"
        Me.btnAbort.UseVisualStyleBackColor = False
        '
        'DG_Schedule
        '
        Me.DG_Schedule.AllowUserToAddRows = False
        Me.DG_Schedule.AllowUserToDeleteRows = False
        Me.DG_Schedule.AllowUserToResizeColumns = False
        Me.DG_Schedule.AllowUserToResizeRows = False
        Me.DG_Schedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Schedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_Schedule.BackgroundColor = System.Drawing.Color.White
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
        Me.DG_Schedule.Dock = System.Windows.Forms.DockStyle.Top
        Me.DG_Schedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Schedule.Location = New System.Drawing.Point(75, 182)
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
        Me.DG_Schedule.Size = New System.Drawing.Size(577, 150)
        Me.DG_Schedule.TabIndex = 15
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(75, 22)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(577, 34)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "PLEASE SELECT A SCHEDULE:"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOK, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbSchedules_01, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(75, 62)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(577, 34)
        Me.TableLayoutPanel2.TabIndex = 17
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(497, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 28)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnOK.Location = New System.Drawing.Point(406, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(74, 28)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'cmbSchedules_01
        '
        Me.cmbSchedules_01.BackColor = System.Drawing.Color.White
        Me.cmbSchedules_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSchedules_01.Font = New System.Drawing.Font("Consolas", 20.0!)
        Me.cmbSchedules_01.FormattingEnabled = True
        Me.cmbSchedules_01.Location = New System.Drawing.Point(3, 3)
        Me.cmbSchedules_01.MaxDropDownItems = 20
        Me.cmbSchedules_01.Name = "cmbSchedules_01"
        Me.cmbSchedules_01.Size = New System.Drawing.Size(397, 40)
        Me.cmbSchedules_01.TabIndex = 4
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.lblCurrentScheduleName, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.lblCurrentSchedule, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(75, 142)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(577, 34)
        Me.TableLayoutPanel4.TabIndex = 21
        '
        'lblCurrentScheduleName
        '
        Me.lblCurrentScheduleName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentScheduleName.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentScheduleName.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentScheduleName.Location = New System.Drawing.Point(23, 0)
        Me.lblCurrentScheduleName.Name = "lblCurrentScheduleName"
        Me.lblCurrentScheduleName.Size = New System.Drawing.Size(272, 34)
        Me.lblCurrentScheduleName.TabIndex = 7
        Me.lblCurrentScheduleName.Text = "ProductionMode"
        Me.lblCurrentScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentSchedule.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCurrentSchedule.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(3, 0)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(14, 34)
        Me.lblCurrentSchedule.TabIndex = 6
        Me.lblCurrentSchedule.Text = "CURRENT SCHEDULE:"
        Me.lblCurrentSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ScheduleSelectView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 662)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "ScheduleSelectView"
        Me.Text = "ScheduleSelectView"
        Me.DesignPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.DG_Schedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents DesignPanel As Panel
    Public WithEvents TableLayoutPanel1 As TableLayoutPanel
    Public WithEvents lblMessage As Label
    Public WithEvents DG_Schedule As DataGridView
    Public WithEvents TableLayoutPanel2 As TableLayoutPanel
    Public WithEvents btnCancel As Button
    Public WithEvents btnOK As Button
    Public WithEvents cmbSchedules_01 As ComboBox
    Public WithEvents TableLayoutPanel3 As TableLayoutPanel
    Public WithEvents btnAbort As Button
    Public WithEvents TableLayoutPanel4 As TableLayoutPanel
    Public WithEvents lblCurrentScheduleName As Label
    Public WithEvents lblCurrentSchedule As Label
End Class
