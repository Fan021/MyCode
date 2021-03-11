<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionForm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox_Msg = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox_Stack = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox_Msg)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(506, 106)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Msg:"
        '
        'TextBox_Msg
        '
        Me.TextBox_Msg.BackColor = System.Drawing.Color.White
        Me.TextBox_Msg.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.TextBox_Msg.Location = New System.Drawing.Point(6, 20)
        Me.TextBox_Msg.Multiline = True
        Me.TextBox_Msg.Name = "TextBox_Msg"
        Me.TextBox_Msg.ReadOnly = True
        Me.TextBox_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Msg.Size = New System.Drawing.Size(494, 80)
        Me.TextBox_Msg.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox_Stack)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 124)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(506, 342)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Stack:"
        '
        'TextBox_Stack
        '
        Me.TextBox_Stack.BackColor = System.Drawing.Color.White
        Me.TextBox_Stack.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TextBox_Stack.Location = New System.Drawing.Point(6, 19)
        Me.TextBox_Stack.Multiline = True
        Me.TextBox_Stack.Name = "TextBox_Stack"
        Me.TextBox_Stack.ReadOnly = True
        Me.TextBox_Stack.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_Stack.Size = New System.Drawing.Size(494, 317)
        Me.TextBox_Stack.TabIndex = 0
        '
        'ExceptionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 478)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExceptionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ExceptionMsg"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents TextBox_Msg As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents TextBox_Stack As System.Windows.Forms.TextBox
End Class
