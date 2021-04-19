<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FlashUI
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
        Me.DockPanel = New System.Windows.Forms.Panel()
        Me.DG_Laser = New System.Windows.Forms.DataGridView()
        Me.cmbCmd = New System.Windows.Forms.ComboBox()
        Me.btnCmd = New System.Windows.Forms.Button()
        Me._Msg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._StepID = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCmd = New System.Windows.Forms.Label()
        Me.DockPanel.SuspendLayout()
        CType(Me.DG_Laser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockPanel
        '
        Me.DockPanel.BackColor = System.Drawing.Color.White
        Me.DockPanel.Controls.Add(Me.DG_Laser)
        Me.DockPanel.Controls.Add(Me.cmbCmd)
        Me.DockPanel.Controls.Add(Me.btnCmd)
        Me.DockPanel.Controls.Add(Me._Msg)
        Me.DockPanel.Controls.Add(Me.Label2)
        Me.DockPanel.Controls.Add(Me._StepID)
        Me.DockPanel.Controls.Add(Me.Label1)
        Me.DockPanel.Controls.Add(Me.lblCmd)
        Me.DockPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Size = New System.Drawing.Size(693, 289)
        Me.DockPanel.TabIndex = 3
        '
        'DG_Laser
        '
        Me.DG_Laser.AllowUserToAddRows = False
        Me.DG_Laser.AllowUserToDeleteRows = False
        Me.DG_Laser.AllowUserToResizeColumns = False
        Me.DG_Laser.AllowUserToResizeRows = False
        Me.DG_Laser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DG_Laser.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_Laser.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Laser.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Laser.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Laser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Laser.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DG_Laser.Location = New System.Drawing.Point(21, 68)
        Me.DG_Laser.Name = "DG_Laser"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Laser.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Laser.RowHeadersWidth = 20
        Me.DG_Laser.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Laser.RowTemplate.Height = 23
        Me.DG_Laser.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Laser.Size = New System.Drawing.Size(650, 172)
        Me.DG_Laser.TabIndex = 84
        '
        'cmbCmd
        '
        Me.cmbCmd.FormattingEnabled = True
        Me.cmbCmd.Items.AddRange(New Object() {"RUN", "FSEXIST"})
        Me.cmbCmd.Location = New System.Drawing.Point(122, 24)
        Me.cmbCmd.Name = "cmbCmd"
        Me.cmbCmd.Size = New System.Drawing.Size(198, 21)
        Me.cmbCmd.TabIndex = 80
        '
        'btnCmd
        '
        Me.btnCmd.Location = New System.Drawing.Point(343, 21)
        Me.btnCmd.Name = "btnCmd"
        Me.btnCmd.Size = New System.Drawing.Size(155, 25)
        Me.btnCmd.TabIndex = 79
        Me.btnCmd.Text = "btnChangeTemplate"
        Me.btnCmd.UseVisualStyleBackColor = True
        '
        '_Msg
        '
        Me._Msg.BackColor = System.Drawing.Color.White
        Me._Msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._Msg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me._Msg.Location = New System.Drawing.Point(166, 260)
        Me._Msg.Name = "_Msg"
        Me._Msg.Size = New System.Drawing.Size(506, 22)
        Me._Msg.TabIndex = 78
        Me._Msg.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(107, 260)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Message:"
        '
        '_StepID
        '
        Me._StepID.BackColor = System.Drawing.Color.White
        Me._StepID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._StepID.Location = New System.Drawing.Point(62, 260)
        Me._StepID.Name = "_StepID"
        Me._StepID.Size = New System.Drawing.Size(38, 22)
        Me._StepID.TabIndex = 76
        Me._StepID.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 260)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Step:"
        '
        'lblCmd
        '
        Me.lblCmd.AutoSize = True
        Me.lblCmd.Location = New System.Drawing.Point(19, 26)
        Me.lblCmd.Name = "lblCmd"
        Me.lblCmd.Size = New System.Drawing.Size(61, 13)
        Me.lblCmd.TabIndex = 83
        Me.lblCmd.Text = "lblTemplate"
        '
        'FlashUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 289)
        Me.Controls.Add(Me.DockPanel)
        Me.Name = "FlashUI"
        Me.Text = "Flash"
        Me.DockPanel.ResumeLayout(False)
        Me.DockPanel.PerformLayout()
        CType(Me.DG_Laser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockPanel As System.Windows.Forms.Panel
    Friend WithEvents _Msg As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents _StepID As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCmd As System.Windows.Forms.Label
    Friend WithEvents cmbCmd As System.Windows.Forms.ComboBox
    Friend WithEvents btnCmd As System.Windows.Forms.Button
    Friend WithEvents DG_Laser As System.Windows.Forms.DataGridView
End Class
