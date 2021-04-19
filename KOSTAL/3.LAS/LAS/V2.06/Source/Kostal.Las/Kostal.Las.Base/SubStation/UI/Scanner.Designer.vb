<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScannerUI
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
        Me._lblScanResult = New System.Windows.Forms.TextBox()
        Me.DG_SCAN = New System.Windows.Forms.DataGridView()
        Me.UI_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me._btnON = New System.Windows.Forms.Button()
        Me.DockPanel.SuspendLayout()
        Me.UI.SuspendLayout()
        Me.UI_Step.SuspendLayout()
        CType(Me.DG_SCAN, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.DockPanel.TabIndex = 0
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.UI_Step, 0, 3)
        Me.UI.Controls.Add(Me._lblScanResult, 0, 1)
        Me.UI.Controls.Add(Me.DG_SCAN, 0, 2)
        Me.UI.Controls.Add(Me.UI_Title, 0, 0)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 4
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.UI.Size = New System.Drawing.Size(693, 289)
        Me.UI.TabIndex = 86
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
        '_lblScanResult
        '
        Me._lblScanResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me._lblScanResult.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me._lblScanResult.Location = New System.Drawing.Point(3, 41)
        Me._lblScanResult.Name = "_lblScanResult"
        Me._lblScanResult.ReadOnly = True
        Me._lblScanResult.Size = New System.Drawing.Size(687, 22)
        Me._lblScanResult.TabIndex = 85
        '
        'DG_SCAN
        '
        Me.DG_SCAN.AllowUserToAddRows = False
        Me.DG_SCAN.AllowUserToDeleteRows = False
        Me.DG_SCAN.AllowUserToResizeColumns = False
        Me.DG_SCAN.AllowUserToResizeRows = False
        Me.DG_SCAN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG_SCAN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_SCAN.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_SCAN.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_SCAN.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_SCAN.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_SCAN.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_SCAN.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_SCAN.Location = New System.Drawing.Point(3, 71)
        Me.DG_SCAN.Name = "DG_SCAN"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_SCAN.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_SCAN.RowHeadersWidth = 20
        Me.DG_SCAN.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_SCAN.RowTemplate.Height = 23
        Me.DG_SCAN.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_SCAN.Size = New System.Drawing.Size(687, 190)
        Me.DG_SCAN.TabIndex = 80
        '
        'UI_Title
        '
        Me.UI_Title.ColumnCount = 5
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.UI_Title.Controls.Add(Me._btnON, 0, 0)
        Me.UI_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Title.Location = New System.Drawing.Point(3, 3)
        Me.UI_Title.Name = "UI_Title"
        Me.UI_Title.RowCount = 1
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Title.Size = New System.Drawing.Size(687, 32)
        Me.UI_Title.TabIndex = 81
        '
        '_btnON
        '
        Me._btnON.Dock = System.Windows.Forms.DockStyle.Fill
        Me._btnON.Location = New System.Drawing.Point(3, 3)
        Me._btnON.Name = "_btnON"
        Me._btnON.Size = New System.Drawing.Size(131, 26)
        Me._btnON.TabIndex = 71
        Me._btnON.Text = "ON"
        Me._btnON.UseVisualStyleBackColor = True
        '
        'ScannerUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 289)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "ScannerUI"
        Me.Text = "Scanner"
        Me.DockPanel.ResumeLayout(False)
        Me.UI.ResumeLayout(False)
        Me.UI.PerformLayout()
        Me.UI_Step.ResumeLayout(False)
        Me.UI_Step.PerformLayout()
        CType(Me.DG_SCAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UI_Title.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents _btnON As System.Windows.Forms.Button
    Friend WithEvents _lblScanResult As Windows.Forms.TextBox
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents UI_Step As HMITableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents _StepID As Windows.Forms.Label
    Friend WithEvents _Msg As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents DG_SCAN As Windows.Forms.DataGridView
    Friend WithEvents UI_Title As HMITableLayoutPanel
    Private components As ComponentModel.IContainer
End Class
