<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenCylinderForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component List.
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TabControl_Cylinder = New HMITabControl(Me.components)
        Me.Panel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TabControl_Cylinder)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TabControl_Cylinder
        '
        Me.TabControl_Cylinder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Cylinder.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_Cylinder.Location = New System.Drawing.Point(3, 3)
        Me.TabControl_Cylinder.Name = "TabControl_Cylinder"
        Me.TabControl_Cylinder.SelectedIndex = 0
        Me.TabControl_Cylinder.Size = New System.Drawing.Size(461, 524)
        Me.TabControl_Cylinder.TabIndex = 1
        '
        'ChildrenCylinderForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenCylinderForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cylinder"
        Me.Panel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TabControl_Cylinder As HMITabControl
End Class
