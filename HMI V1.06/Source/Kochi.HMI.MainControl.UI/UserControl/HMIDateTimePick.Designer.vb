<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HMIDateTimePick
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component List.
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
        Me.MonthCalendar = New System.Windows.Forms.MonthCalendar()
        Me.Label_Hour = New System.Windows.Forms.Label()
        Me.ComboBox_Hour = New System.Windows.Forms.ComboBox()
        Me.ComboBox_Minute = New System.Windows.Forms.ComboBox()
        Me.ComboBox_Second = New System.Windows.Forms.ComboBox()
        Me.Button_OK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'MonthCalendar
        '
        Me.MonthCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MonthCalendar.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MonthCalendar.Location = New System.Drawing.Point(0, 0)
        Me.MonthCalendar.Margin = New System.Windows.Forms.Padding(0)
        Me.MonthCalendar.MaxSelectionCount = 1
        Me.MonthCalendar.Name = "MonthCalendar"
        Me.MonthCalendar.ShowToday = False
        Me.MonthCalendar.ShowTodayCircle = False
        Me.MonthCalendar.TabIndex = 2
        '
        'Label_Hour
        '
        Me.Label_Hour.AutoSize = True
        Me.Label_Hour.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Hour.Location = New System.Drawing.Point(-2, 177)
        Me.Label_Hour.Name = "Label_Hour"
        Me.Label_Hour.Size = New System.Drawing.Size(44, 19)
        Me.Label_Hour.TabIndex = 3
        Me.Label_Hour.Text = "Hour:"
        '
        'ComboBox_Hour
        '
        Me.ComboBox_Hour.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ComboBox_Hour.FormattingEnabled = True
        Me.ComboBox_Hour.Location = New System.Drawing.Point(41, 174)
        Me.ComboBox_Hour.Name = "ComboBox_Hour"
        Me.ComboBox_Hour.Size = New System.Drawing.Size(41, 27)
        Me.ComboBox_Hour.TabIndex = 4
        '
        'ComboBox_Minute
        '
        Me.ComboBox_Minute.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ComboBox_Minute.FormattingEnabled = True
        Me.ComboBox_Minute.Location = New System.Drawing.Point(86, 174)
        Me.ComboBox_Minute.Name = "ComboBox_Minute"
        Me.ComboBox_Minute.Size = New System.Drawing.Size(41, 27)
        Me.ComboBox_Minute.TabIndex = 5
        '
        'ComboBox_Second
        '
        Me.ComboBox_Second.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ComboBox_Second.FormattingEnabled = True
        Me.ComboBox_Second.Location = New System.Drawing.Point(128, 174)
        Me.ComboBox_Second.Name = "ComboBox_Second"
        Me.ComboBox_Second.Size = New System.Drawing.Size(41, 27)
        Me.ComboBox_Second.TabIndex = 6
        '
        'Button_OK
        '
        Me.Button_OK.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Button_OK.Location = New System.Drawing.Point(172, 174)
        Me.Button_OK.Name = "Button_OK"
        Me.Button_OK.Size = New System.Drawing.Size(66, 26)
        Me.Button_OK.TabIndex = 7
        Me.Button_OK.Text = "confirm"
        Me.Button_OK.UseVisualStyleBackColor = True
        '
        'HMIDateTimePick
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button_OK)
        Me.Controls.Add(Me.ComboBox_Second)
        Me.Controls.Add(Me.ComboBox_Minute)
        Me.Controls.Add(Me.ComboBox_Hour)
        Me.Controls.Add(Me.Label_Hour)
        Me.Controls.Add(Me.MonthCalendar)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "HMIDateTimePick"
        Me.Size = New System.Drawing.Size(241, 206)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MonthCalendar As System.Windows.Forms.MonthCalendar
    Friend WithEvents Label_Hour As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Hour As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_Minute As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_Second As System.Windows.Forms.ComboBox
    Friend WithEvents Button_OK As System.Windows.Forms.Button

End Class
