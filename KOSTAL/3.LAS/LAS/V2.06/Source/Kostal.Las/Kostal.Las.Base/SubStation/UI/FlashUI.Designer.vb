<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlashUI
    Inherits System.Windows.Forms.Form
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.UI = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.UI_Step = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._StepID = New System.Windows.Forms.Label()
        Me._Msg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DG_Laser = New System.Windows.Forms.DataGridView()
        Me.UI_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.btnCmd = New System.Windows.Forms.Button()
        Me.cmbCmd = New System.Windows.Forms.ComboBox()
        Me.lblCmd = New System.Windows.Forms.Label()
        Me.DockPanel.SuspendLayout()
        Me.UI.SuspendLayout()
        Me.UI_Step.SuspendLayout()
        CType(Me.DG_Laser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UI_Title.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.Controls.Add(Me.UI)
        Me.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(693, 267)
        Me.DockPanel.TabIndex = 0
        '
        'UI
        '
        Me.UI.ColumnCount = 1
        Me.UI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI.Controls.Add(Me.UI_Step, 0, 2)
        Me.UI.Controls.Add(Me.DG_Laser, 0, 1)
        Me.UI.Controls.Add(Me.UI_Title, 0, 0)
        Me.UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI.Location = New System.Drawing.Point(0, 0)
        Me.UI.Name = "UI"
        Me.UI.RowCount = 3
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.UI.Size = New System.Drawing.Size(693, 267)
        Me.UI.TabIndex = 82
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
        Me.UI_Step.Location = New System.Drawing.Point(3, 243)
        Me.UI_Step.Name = "UI_Step"
        Me.UI_Step.RowCount = 1
        Me.UI_Step.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Step.Size = New System.Drawing.Size(687, 21)
        Me.UI_Step.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 21)
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
        Me._StepID.Size = New System.Drawing.Size(62, 21)
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
        Me._Msg.Size = New System.Drawing.Size(477, 21)
        Me._Msg.TabIndex = 78
        Me._Msg.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(139, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 21)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Message:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DG_Laser
        '
        Me.DG_Laser.AllowUserToAddRows = False
        Me.DG_Laser.AllowUserToDeleteRows = False
        Me.DG_Laser.AllowUserToResizeColumns = False
        Me.DG_Laser.AllowUserToResizeRows = False
        Me.DG_Laser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG_Laser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Laser.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_Laser.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Laser.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Laser.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Laser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Laser.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Laser.Location = New System.Drawing.Point(3, 43)
        Me.DG_Laser.Name = "DG_Laser"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Laser.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Laser.RowHeadersWidth = 20
        Me.DG_Laser.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Laser.RowTemplate.Height = 23
        Me.DG_Laser.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Laser.Size = New System.Drawing.Size(687, 194)
        Me.DG_Laser.TabIndex = 80
        '
        'UI_Title
        '
        Me.UI_Title.ColumnCount = 5
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.UI_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.UI_Title.Controls.Add(Me.btnCmd, 0, 0)
        Me.UI_Title.Controls.Add(Me.cmbCmd, 0, 0)
        Me.UI_Title.Controls.Add(Me.lblCmd, 0, 0)
        Me.UI_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UI_Title.Location = New System.Drawing.Point(3, 3)
        Me.UI_Title.Name = "UI_Title"
        Me.UI_Title.RowCount = 1
        Me.UI_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.UI_Title.Size = New System.Drawing.Size(687, 34)
        Me.UI_Title.TabIndex = 81
        '
        'btnCmd
        '
        Me.btnCmd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCmd.Location = New System.Drawing.Point(346, 3)
        Me.btnCmd.Name = "btnCmd"
        Me.btnCmd.Size = New System.Drawing.Size(97, 28)
        Me.btnCmd.TabIndex = 86
        Me.btnCmd.Text = "btnChangeTemplate"
        Me.btnCmd.UseVisualStyleBackColor = True
        '
        'cmbCmd
        '
        Me.cmbCmd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbCmd.FormattingEnabled = True
        Me.cmbCmd.Items.AddRange(New Object() {"RUN", "FSEXIST"})
        Me.cmbCmd.Location = New System.Drawing.Point(140, 3)
        Me.cmbCmd.Name = "cmbCmd"
        Me.cmbCmd.Size = New System.Drawing.Size(200, 20)
        Me.cmbCmd.TabIndex = 85
        '
        'lblCmd
        '
        Me.lblCmd.AutoSize = True
        Me.lblCmd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCmd.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.lblCmd.Location = New System.Drawing.Point(3, 0)
        Me.lblCmd.Name = "lblCmd"
        Me.lblCmd.Size = New System.Drawing.Size(131, 34)
        Me.lblCmd.TabIndex = 84
        Me.lblCmd.Text = "lblTemplate"
        Me.lblCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlashUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 267)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "FlashUI"
        Me.Text = "FlashUI"
        Me.DockPanel.ResumeLayout(False)
        Me.UI.ResumeLayout(False)
        Me.UI_Step.ResumeLayout(False)
        Me.UI_Step.PerformLayout()
        CType(Me.DG_Laser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UI_Title.ResumeLayout(False)
        Me.UI_Title.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DockPanel As Windows.Forms.Panel
    Friend WithEvents UI As HMITableLayoutPanel
    Friend WithEvents UI_Step As HMITableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents _StepID As Windows.Forms.Label
    Friend WithEvents _Msg As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents DG_Laser As Windows.Forms.DataGridView
    Friend WithEvents UI_Title As HMITableLayoutPanel
    Friend WithEvents btnCmd As Windows.Forms.Button
    Friend WithEvents cmbCmd As Windows.Forms.ComboBox
    Friend WithEvents lblCmd As Windows.Forms.Label
End Class
