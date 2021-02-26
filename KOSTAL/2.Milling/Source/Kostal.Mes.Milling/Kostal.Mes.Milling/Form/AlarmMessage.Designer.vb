<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlarmMessage
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
        Me.TextBox_Message = New System.Windows.Forms.TextBox()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label_ShowTime = New System.Windows.Forms.Label()
        Me.ElapseTime = New System.Windows.Forms.Label()
        Me.Timer_Show = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'TextBox_Message
        '
        Me.TextBox_Message.BackColor = System.Drawing.Color.White
        Me.TextBox_Message.Font = New System.Drawing.Font("Courier New", 32.0!)
        Me.TextBox_Message.ForeColor = System.Drawing.Color.Blue
        Me.TextBox_Message.Location = New System.Drawing.Point(-2, -1)
        Me.TextBox_Message.Multiline = True
        Me.TextBox_Message.Name = "TextBox_Message"
        Me.TextBox_Message.ReadOnly = True
        Me.TextBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Message.Size = New System.Drawing.Size(582, 411)
        Me.TextBox_Message.TabIndex = 0
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(280, 416)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(96, 75)
        Me.Button_Save.TabIndex = 1
        Me.Button_Save.Text = "Button_Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Label_ShowTime
        '
        Me.Label_ShowTime.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label_ShowTime.Location = New System.Drawing.Point(12, 429)
        Me.Label_ShowTime.Name = "Label_ShowTime"
        Me.Label_ShowTime.Size = New System.Drawing.Size(78, 49)
        Me.Label_ShowTime.TabIndex = 2
        Me.Label_ShowTime.Text = "剩余时间:"
        Me.Label_ShowTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ElapseTime
        '
        Me.ElapseTime.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ElapseTime.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ElapseTime.Location = New System.Drawing.Point(90, 429)
        Me.ElapseTime.Name = "ElapseTime"
        Me.ElapseTime.Size = New System.Drawing.Size(78, 49)
        Me.ElapseTime.TabIndex = 3
        Me.ElapseTime.Text = "00"
        Me.ElapseTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer_Show
        '
        '
        'AlarmMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 491)
        Me.ControlBox = False
        Me.Controls.Add(Me.ElapseTime)
        Me.Controls.Add(Me.Label_ShowTime)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.TextBox_Message)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AlarmMessage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AlarmMessage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_Message As System.Windows.Forms.TextBox
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Label_ShowTime As System.Windows.Forms.Label
    Friend WithEvents ElapseTime As System.Windows.Forms.Label
    Friend WithEvents Timer_Show As System.Windows.Forms.Timer
End Class
