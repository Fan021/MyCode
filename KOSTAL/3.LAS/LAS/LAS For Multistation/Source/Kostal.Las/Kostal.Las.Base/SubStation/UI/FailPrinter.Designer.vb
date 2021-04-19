<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FailPrinterUI
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.UI = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.UI_Step = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._StepID = New System.Windows.Forms.Label()
        Me._Msg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DG_FAIL = New System.Windows.Forms.DataGridView()
        Me.UI_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.TextBox_Send = New System.Windows.Forms.TextBox()
        Me.Button_Send = New System.Windows.Forms.Button()
        Me.DockPanel.SuspendLayout()
        Me.UI.SuspendLayout()
        Me.UI_Step.SuspendLayout()
        CType(Me.DG_FAIL, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.DockPanel.TabIndex = 3
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.UI_Step, 0, 2)
        Me.UI.Controls.Add(Me.DG_FAIL, 0, 1)
        Me.UI.Controls.Add(Me.UI_Title, 0, 0)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 3
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.UI.Size = New System.Drawing.Size(693, 289)
        Me.UI.TabIndex = 88
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
        'DG_FAIL
        '
        Me.DG_FAIL.AllowUserToAddRows = False
        Me.DG_FAIL.AllowUserToDeleteRows = False
        Me.DG_FAIL.AllowUserToResizeColumns = False
        Me.DG_FAIL.AllowUserToResizeRows = False
        Me.DG_FAIL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG_FAIL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_FAIL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_FAIL.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_FAIL.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_FAIL.DefaultCellStyle = DataGridViewCellStyle5
        Me.DG_FAIL.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_FAIL.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_FAIL.Location = New System.Drawing.Point(3, 41)
        Me.DG_FAIL.Name = "DG_FAIL"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_FAIL.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DG_FAIL.RowHeadersWidth = 20
        Me.DG_FAIL.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_FAIL.RowTemplate.Height = 23
        Me.DG_FAIL.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_FAIL.Size = New System.Drawing.Size(687, 220)
        Me.DG_FAIL.TabIndex = 80
        '
        'UI_Title
        '
        Me.UI_Title.ColumnCount = 2
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.UI_Title.Controls.Add(Me.TextBox_Send, 0, 0)
        Me.UI_Title.Controls.Add(Me.Button_Send, 1, 0)
        Me.UI_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Title.Location = New System.Drawing.Point(3, 3)
        Me.UI_Title.Name = "UI_Title"
        Me.UI_Title.RowCount = 1
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Title.Size = New System.Drawing.Size(687, 32)
        Me.UI_Title.TabIndex = 81
        '
        'TextBox_Send
        '
        Me.TextBox_Send.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Send.Location = New System.Drawing.Point(3, 3)
        Me.TextBox_Send.Name = "TextBox_Send"
        Me.TextBox_Send.Size = New System.Drawing.Size(337, 20)
        Me.TextBox_Send.TabIndex = 87
        '
        'Button_Send
        '
        Me.Button_Send.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button_Send.Location = New System.Drawing.Point(346, 3)
        Me.Button_Send.Name = "Button_Send"
        Me.Button_Send.Size = New System.Drawing.Size(101, 26)
        Me.Button_Send.TabIndex = 86
        Me.Button_Send.Text = "Send"
        Me.Button_Send.UseVisualStyleBackColor = True
        '
        'FailPrinterUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 289)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "FailPrinterUI"
        Me.Text = "FailPrinter"
        Me.DockPanel.ResumeLayout(False)
        Me.UI.ResumeLayout(False)
        Me.UI_Step.ResumeLayout(False)
        Me.UI_Step.PerformLayout()
        CType(Me.DG_FAIL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UI_Title.ResumeLayout(False)
        Me.UI_Title.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents Button_Send As System.Windows.Forms.Button
    Friend WithEvents TextBox_Send As Windows.Forms.TextBox
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents UI_Step As HMITableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents _StepID As Windows.Forms.Label
    Friend WithEvents _Msg As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents DG_FAIL As Windows.Forms.DataGridView
    Friend WithEvents UI_Title As HMITableLayoutPanel
End Class
