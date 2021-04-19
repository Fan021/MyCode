<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EpsonDebug
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.textBoxTitle = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(28, 105)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(308, 101)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = """TS1080.05; BETWEEN; 0; 1 ;; 9.9E+37"""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(378, 154)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(131, 49)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'textBoxTitle
        '
        Me.textBoxTitle.Location = New System.Drawing.Point(28, 50)
        Me.textBoxTitle.Name = "textBoxTitle"
        Me.textBoxTitle.Size = New System.Drawing.Size(308, 21)
        Me.textBoxTitle.TabIndex = 2
        Me.textBoxTitle.Text = "-----------SGM_358 Failure Part ----------"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Title"
        '
        'EpsonDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 250)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.textBoxTitle)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "EpsonDebug"
        Me.Text = "EpsonDebug"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents textBoxTitle As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
