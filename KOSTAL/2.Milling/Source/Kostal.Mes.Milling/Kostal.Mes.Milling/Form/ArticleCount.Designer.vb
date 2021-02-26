<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArticleCount
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
        Me.components = New System.ComponentModel.Container()
        Me.TextBox_Count = New System.Windows.Forms.TextBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'TextBox_Count
        '
        Me.TextBox_Count.Font = New System.Drawing.Font("Calibri", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Count.Location = New System.Drawing.Point(12, 23)
        Me.TextBox_Count.Name = "TextBox_Count"
        Me.TextBox_Count.Size = New System.Drawing.Size(446, 86)
        Me.TextBox_Count.TabIndex = 0
        Me.TextBox_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button_OK
        '
        Me.Button_OK.Location = New System.Drawing.Point(464, 23)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(113, 86)
        Me.Button_OK.TabIndex = 1
        Me.Button_OK.Text = "Confirm"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'ArticleCount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 127)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.TextBox_Count)
        Me.Name = "ArticleCount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ArticleCount"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_Count As System.Windows.Forms.TextBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
