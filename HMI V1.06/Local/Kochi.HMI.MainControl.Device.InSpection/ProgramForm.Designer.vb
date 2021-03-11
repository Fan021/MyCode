<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgramForm
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
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.MachineListView_Program = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.Panel_UI.SuspendLayout()
        CType(Me.MachineListView_Program, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.MachineListView_Program)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(568, 519)
        Me.Panel_UI.TabIndex = 0
        '
        'MachineListView_Program
        '
        Me.MachineListView_Program.AllowUserToAddRows = False
        Me.MachineListView_Program.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Program.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Program.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Program.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Program.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Program.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Program.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Program.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Program.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Program.EnableHeadersVisualStyles = False
        Me.MachineListView_Program.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Program.Location = New System.Drawing.Point(0, 0)
        Me.MachineListView_Program.Name = "MachineListView_Program"
        Me.MachineListView_Program.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Program.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Program.RowTemplate.Height = 180
        Me.MachineListView_Program.Size = New System.Drawing.Size(568, 519)
        Me.MachineListView_Program.TabIndex = 2
        '
        'ProgramForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 519)
        Me.Controls.Add(Me.Panel_UI)
        Me.Name = "ProgramForm"
        Me.Text = "ProgramForm"
        Me.Panel_UI.ResumeLayout(False)
        CType(Me.MachineListView_Program, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents MachineListView_Program As Kochi.HMI.MainControl.UI.MachineListView
End Class
