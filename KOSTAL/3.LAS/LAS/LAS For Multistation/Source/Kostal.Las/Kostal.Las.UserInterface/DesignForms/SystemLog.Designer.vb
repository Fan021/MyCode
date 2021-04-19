<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SystemLog
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
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.MainLogger = New System.Windows.Forms.ListBox()
        Me.DesignPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.MainLogger)
        Me.DesignPanel.Location = New System.Drawing.Point(50, 36)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(689, 620)
        Me.DesignPanel.TabIndex = 0
        '
        'MainLogger
        '
        Me.MainLogger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MainLogger.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLogger.FormattingEnabled = True
        Me.MainLogger.ItemHeight = 12
        Me.MainLogger.Location = New System.Drawing.Point(0, 0)
        Me.MainLogger.Name = "MainLogger"
        Me.MainLogger.Size = New System.Drawing.Size(689, 620)
        Me.MainLogger.TabIndex = 24
        '
        'SystemLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 757)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "SystemLog"
        Me.Text = "SystemLog"
        Me.DesignPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Public WithEvents MainLogger As ListBox
End Class
