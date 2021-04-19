<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PLCAlarmUI
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelUI = New System.Windows.Forms.Panel()
        Me.UI = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.UI_Step = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._StepID = New System.Windows.Forms.Label()
        Me._Msg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DG_Fail = New System.Windows.Forms.DataGridView()
        Me.PanelUI.SuspendLayout()
        Me.UI.SuspendLayout()
        Me.UI_Step.SuspendLayout()
        CType(Me.DG_Fail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelUI
        '
        Me.PanelUI.BackColor = System.Drawing.Color.White
        Me.PanelUI.Controls.Add(Me.UI)
        Me.PanelUI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelUI.Location = New System.Drawing.Point(0, 0)
        Me.PanelUI.Name = "PanelUI"
        Me.PanelUI.Size = New System.Drawing.Size(693, 289)
        Me.PanelUI.TabIndex = 8
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.UI_Step, 0, 2)
        Me.UI.Controls.Add(Me.DG_Fail, 0, 1)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 3
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.UI.Size = New System.Drawing.Size(693, 289)
        Me.UI.TabIndex = 81
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
        'DG_Fail
        '
        Me.DG_Fail.AllowUserToAddRows = False
        Me.DG_Fail.AllowUserToDeleteRows = False
        Me.DG_Fail.AllowUserToResizeColumns = False
        Me.DG_Fail.AllowUserToResizeRows = False
        Me.DG_Fail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG_Fail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Fail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_Fail.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Fail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Fail.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Fail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Fail.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Fail.Location = New System.Drawing.Point(3, 33)
        Me.DG_Fail.Name = "DG_Fail"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Fail.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Fail.RowHeadersWidth = 20
        Me.DG_Fail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Fail.RowTemplate.Height = 23
        Me.DG_Fail.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Fail.Size = New System.Drawing.Size(687, 228)
        Me.DG_Fail.TabIndex = 80
        '
        'PLCAlarmUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 289)
        Me.Controls.Add(Me.PanelUI)
        Me.Name = "PLCAlarmUI"
        Me.Text = "PLCAlarm"
        Me.PanelUI.ResumeLayout(False)
        Me.UI.ResumeLayout(False)
        Me.UI_Step.ResumeLayout(False)
        Me.UI_Step.PerformLayout()
        CType(Me.DG_Fail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelUI As Windows.Forms.Panel
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents UI_Step As HMITableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents _StepID As Windows.Forms.Label
    Friend WithEvents _Msg As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents DG_Fail As Windows.Forms.DataGridView
End Class
