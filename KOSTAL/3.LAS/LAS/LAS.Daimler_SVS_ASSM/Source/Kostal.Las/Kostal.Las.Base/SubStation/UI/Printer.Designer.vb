<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrinterUI
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Protected Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.UI = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.UI_Step = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._StepID = New System.Windows.Forms.Label()
        Me._Msg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DG_Printer = New System.Windows.Forms.DataGridView()
        Me.UI_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.btnReprint = New System.Windows.Forms.Button()
        Me.cbClearMask = New System.Windows.Forms.CheckBox()
        Me.TextBox_Count = New System.Windows.Forms.TextBox()
        Me.Label_Count = New System.Windows.Forms.Label()
        Me.DockPanel.SuspendLayout()
        Me.UI.SuspendLayout()
        Me.UI_Step.SuspendLayout()
        CType(Me.DG_Printer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UI_Title.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.BackColor = System.Drawing.Color.White
        Me.DockPanel.Controls.Add(Me.UI)
        Me.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(693, 289)
        Me.DockPanel.TabIndex = 1
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.UI_Step, 0, 2)
        Me.UI.Controls.Add(Me.DG_Printer, 0, 1)
        Me.UI.Controls.Add(Me.UI_Title, 0, 0)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 3
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.UI.Size = New System.Drawing.Size(693, 289)
        Me.UI.TabIndex = 87
        '
        'UI_Step
        '
        Me.UI_Step.ColumnCount = 4
        Me.UI_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.UI_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.UI_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.UI_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.UI_Step.Controls.Add(Me.Label1, 0, 0)
        Me.UI_Step.Controls.Add(Me._StepID, 1, 0)
        Me.UI_Step.Controls.Add(Me._Msg, 3, 0)
        Me.UI_Step.Controls.Add(Me.Label2, 2, 0)
        Me.UI_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Step.Location = New System.Drawing.Point(3, 267)
        Me.UI_Step.Name = "UI_Step"
        Me.UI_Step.RowCount = 1
        Me.UI_Step.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Step.Size = New System.Drawing.Size(687, 19)
        Me.UI_Step.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 19)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Step:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_StepID
        '
        Me._StepID.BackColor = System.Drawing.Color.White
        Me._StepID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._StepID.Dock = System.Windows.Forms.DockStyle.Fill
        Me._StepID.Location = New System.Drawing.Point(71, 0)
        Me._StepID.Name = "_StepID"
        Me._StepID.Size = New System.Drawing.Size(62, 19)
        Me._StepID.TabIndex = 76
        Me._StepID.Text = "0"
        '
        '_Msg
        '
        Me._Msg.BackColor = System.Drawing.Color.White
        Me._Msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._Msg.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Msg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._Msg.Location = New System.Drawing.Point(207, 0)
        Me._Msg.Name = "_Msg"
        Me._Msg.Size = New System.Drawing.Size(477, 19)
        Me._Msg.TabIndex = 78
        Me._Msg.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(139, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 19)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Message:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DG_Printer
        '
        Me.DG_Printer.AllowUserToAddRows = False
        Me.DG_Printer.AllowUserToDeleteRows = False
        Me.DG_Printer.AllowUserToResizeColumns = False
        Me.DG_Printer.AllowUserToResizeRows = False
        Me.DG_Printer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG_Printer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Printer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_Printer.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Printer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Printer.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Printer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Printer.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Printer.Location = New System.Drawing.Point(3, 46)
        Me.DG_Printer.Name = "DG_Printer"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Printer.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Printer.RowHeadersWidth = 20
        Me.DG_Printer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Printer.RowTemplate.Height = 23
        Me.DG_Printer.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Printer.Size = New System.Drawing.Size(687, 215)
        Me.DG_Printer.TabIndex = 80
        '
        'UI_Title
        '
        Me.UI_Title.ColumnCount = 4
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.UI_Title.Controls.Add(Me.btnReprint, 0, 0)
        Me.UI_Title.Controls.Add(Me.cbClearMask, 3, 0)
        Me.UI_Title.Controls.Add(Me.TextBox_Count, 2, 0)
        Me.UI_Title.Controls.Add(Me.Label_Count, 1, 0)
        Me.UI_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Title.Location = New System.Drawing.Point(3, 3)
        Me.UI_Title.Name = "UI_Title"
        Me.UI_Title.RowCount = 1
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Title.Size = New System.Drawing.Size(687, 37)
        Me.UI_Title.TabIndex = 81
        '
        'btnReprint
        '
        Me.btnReprint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReprint.Location = New System.Drawing.Point(3, 3)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(131, 31)
        Me.btnReprint.TabIndex = 81
        Me.btnReprint.Text = "btnReprint"
        Me.btnReprint.UseVisualStyleBackColor = True
        '
        'cbClearMask
        '
        Me.cbClearMask.AutoSize = True
        Me.cbClearMask.Dock = System.Windows.Forms.DockStyle.Left
        Me.cbClearMask.Location = New System.Drawing.Point(380, 3)
        Me.cbClearMask.Name = "cbClearMask"
        Me.cbClearMask.Size = New System.Drawing.Size(88, 31)
        Me.cbClearMask.TabIndex = 82
        Me.cbClearMask.Text = "cbClearMask"
        Me.cbClearMask.UseVisualStyleBackColor = True
        '
        'TextBox_Count
        '
        Me.TextBox_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Count.Location = New System.Drawing.Point(243, 3)
        Me.TextBox_Count.Name = "TextBox_Count"
        Me.TextBox_Count.Size = New System.Drawing.Size(131, 20)
        Me.TextBox_Count.TabIndex = 85
        Me.TextBox_Count.Text = "1"
        '
        'Label_Count
        '
        Me.Label_Count.AutoSize = True
        Me.Label_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Count.Location = New System.Drawing.Point(140, 0)
        Me.Label_Count.Name = "Label_Count"
        Me.Label_Count.Size = New System.Drawing.Size(97, 37)
        Me.Label_Count.TabIndex = 86
        Me.Label_Count.Text = "Count:"
        Me.Label_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PrinterUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 289)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "PrinterUI"
        Me.Text = "Printer"
        Me.DockPanel.ResumeLayout(False)
        Me.UI.ResumeLayout(False)
        Me.UI_Step.ResumeLayout(False)
        Me.UI_Step.PerformLayout()
        CType(Me.DG_Printer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UI_Title.ResumeLayout(False)
        Me.UI_Title.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents cbClearMask As System.Windows.Forms.CheckBox
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents TextBox_Count As Windows.Forms.TextBox
    Friend WithEvents Label_Count As Windows.Forms.Label
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents UI_Step As HMITableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents _StepID As Windows.Forms.Label
    Friend WithEvents _Msg As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents DG_Printer As Windows.Forms.DataGridView
    Friend WithEvents UI_Title As HMITableLayoutPanel
    Private components As ComponentModel.IContainer
End Class
