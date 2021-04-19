Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChildrenParameterForm
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Button = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiButton_Save = New Kostal.Las.Base.HMIButton()
        Me.MachineListView_Parameter = New Kostal.Las.Base.MachineListView()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Button.SuspendLayout()
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.Color.White
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Button, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.MachineListView_Parameter, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Button
        '
        Me.TableLayoutPanel_Button.ColumnCount = 3
        Me.TableLayoutPanel_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5!))
        Me.TableLayoutPanel_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5!))
        Me.TableLayoutPanel_Button.Controls.Add(Me.HmiButton_Save, 1, 0)
        Me.TableLayoutPanel_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Button.Location = New System.Drawing.Point(3, 480)
        Me.TableLayoutPanel_Button.Name = "TableLayoutPanel_Button"
        Me.TableLayoutPanel_Button.RowCount = 1
        Me.TableLayoutPanel_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.TableLayoutPanel_Button.Size = New System.Drawing.Size(461, 47)
        Me.TableLayoutPanel_Button.TabIndex = 0
        '
        'HmiButton_Save
        '
        Me.HmiButton_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Save.Location = New System.Drawing.Point(175, 3)
        Me.HmiButton_Save.MarginHeight = 6
        Me.HmiButton_Save.Name = "HmiButton_Save"
        Me.HmiButton_Save.Size = New System.Drawing.Size(109, 41)
        Me.HmiButton_Save.TabIndex = 0
        '
        'MachineListView_Parameter
        '
        Me.MachineListView_Parameter.AllowUserToAddRows = False
        Me.MachineListView_Parameter.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Parameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Parameter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Parameter.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Parameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Parameter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Parameter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Parameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Parameter.EnableHeadersVisualStyles = False
        Me.MachineListView_Parameter.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Parameter.Location = New System.Drawing.Point(3, 3)
        Me.MachineListView_Parameter.Name = "MachineListView_Parameter"
        Me.MachineListView_Parameter.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Parameter.RowTemplate.Height = 40
        Me.MachineListView_Parameter.Size = New System.Drawing.Size(461, 471)
        Me.MachineListView_Parameter.TabIndex = 1
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackColor = System.Drawing.SystemColors.Control
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(61, 4)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(275, 28)
        Me.miniToolStrip.TabIndex = 11
        '
        'ChildrenParameterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenParameterForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenShortCutForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Button.ResumeLayout(False)
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents miniToolStrip As ToolStrip
    Friend WithEvents TableLayoutPanel_Body As TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Button As TableLayoutPanel
    Friend WithEvents HmiButton_Save As HMIButton
    Friend WithEvents MachineListView_Parameter As MachineListView
End Class
