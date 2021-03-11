<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgramDebug
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
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TabControl_IO = New System.Windows.Forms.TabControl()
        Me.Panel_UI.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.TabControl_IO)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(653, 507)
        Me.Panel_UI.TabIndex = 0
        '
        'TabControl_IO
        '
        Me.TabControl_IO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_IO.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_IO.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_IO.Name = "TabControl_IO"
        Me.TabControl_IO.SelectedIndex = 0
        Me.TabControl_IO.Size = New System.Drawing.Size(653, 507)
        Me.TabControl_IO.TabIndex = 0
        '
        'ProgramDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 507)
        Me.Controls.Add(Me.Panel_UI)
        Me.Name = "ProgramDebug"
        Me.Text = "ProgramDebug"
        Me.Panel_UI.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TabControl_IO As System.Windows.Forms.TabControl
End Class
