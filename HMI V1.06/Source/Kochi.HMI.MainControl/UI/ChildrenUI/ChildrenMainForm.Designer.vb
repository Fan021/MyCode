<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenMainForm
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.cTabControl_Station = New System.Windows.Forms.TabControl()
        Me.Panel_Body.SuspendLayout()
        Me.Panel_UI.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.Panel_UI)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.cTabControl_Station)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(3, 3)
        Me.Panel_UI.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(461, 524)
        Me.Panel_UI.TabIndex = 0
        '
        'cTabControl_Station
        '
        Me.cTabControl_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cTabControl_Station.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.cTabControl_Station.Location = New System.Drawing.Point(0, 0)
        Me.cTabControl_Station.Margin = New System.Windows.Forms.Padding(0)
        Me.cTabControl_Station.Name = "cTabControl_Station"
        Me.cTabControl_Station.SelectedIndex = 0
        Me.cTabControl_Station.Size = New System.Drawing.Size(461, 524)
        Me.cTabControl_Station.TabIndex = 6
        '
        'ChildrenMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenMainForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.Panel_UI.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Public WithEvents cTabControl_Station As System.Windows.Forms.TabControl
End Class
