<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UI
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label_Msg = New System.Windows.Forms.Label()
        Me.GroupBox_Scap = New System.Windows.Forms.GroupBox()
        Me.TextBox_Scap = New System.Windows.Forms.TextBox()
        Me.ListBox_Msg = New System.Windows.Forms.ListBox()
        Me.GroupBox_ErrorMsg = New System.Windows.Forms.GroupBox()
        Me.TextBox_ErrorMsg = New System.Windows.Forms.TextBox()
        Me.GroupBox_SN = New System.Windows.Forms.GroupBox()
        Me.TextBox_SN = New System.Windows.Forms.TextBox()
        Me.grpCounter = New System.Windows.Forms.GroupBox()
        Me.Button_Clean = New System.Windows.Forms.Button()
        Me.Button_Abort = New System.Windows.Forms.Button()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Label_Fail = New System.Windows.Forms.Label()
        Me.Label_Pass = New System.Windows.Forms.Label()
        Me.Label_Total = New System.Windows.Forms.Label()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.lblfail = New System.Windows.Forms.Label()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.GroupBox_Scap.SuspendLayout()
        Me.GroupBox_ErrorMsg.SuspendLayout()
        Me.GroupBox_SN.SuspendLayout()
        Me.grpCounter.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_Msg
        '
        Me.Label_Msg.BackColor = System.Drawing.Color.LightGray
        Me.Label_Msg.Font = New System.Drawing.Font("Calibri", 36.0!)
        Me.Label_Msg.Location = New System.Drawing.Point(3, 0)
        Me.Label_Msg.Name = "Label_Msg"
        Me.Label_Msg.Size = New System.Drawing.Size(625, 93)
        Me.Label_Msg.TabIndex = 29
        Me.Label_Msg.Text = "Waiting"
        Me.Label_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox_Scap
        '
        Me.GroupBox_Scap.Controls.Add(Me.TextBox_Scap)
        Me.GroupBox_Scap.Location = New System.Drawing.Point(3, 202)
        Me.GroupBox_Scap.Name = "GroupBox_Scap"
        Me.GroupBox_Scap.Size = New System.Drawing.Size(625, 110)
        Me.GroupBox_Scap.TabIndex = 27
        Me.GroupBox_Scap.TabStop = False
        Me.GroupBox_Scap.Text = "Scrap Location:"
        '
        'TextBox_Scap
        '
        Me.TextBox_Scap.BackColor = System.Drawing.Color.White
        Me.TextBox_Scap.Font = New System.Drawing.Font("Calibri", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Scap.ForeColor = System.Drawing.Color.Red
        Me.TextBox_Scap.Location = New System.Drawing.Point(4, 20)
        Me.TextBox_Scap.Multiline = True
        Me.TextBox_Scap.Name = "TextBox_Scap"
        Me.TextBox_Scap.ReadOnly = True
        Me.TextBox_Scap.Size = New System.Drawing.Size(613, 81)
        Me.TextBox_Scap.TabIndex = 0
        Me.TextBox_Scap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ListBox_Msg
        '
        Me.ListBox_Msg.FormattingEnabled = True
        Me.ListBox_Msg.HorizontalScrollbar = True
        Me.ListBox_Msg.ItemHeight = 12
        Me.ListBox_Msg.Location = New System.Drawing.Point(7, 693)
        Me.ListBox_Msg.Name = "ListBox_Msg"
        Me.ListBox_Msg.Size = New System.Drawing.Size(613, 100)
        Me.ListBox_Msg.TabIndex = 28
        '
        'GroupBox_ErrorMsg
        '
        Me.GroupBox_ErrorMsg.Controls.Add(Me.TextBox_ErrorMsg)
        Me.GroupBox_ErrorMsg.Location = New System.Drawing.Point(3, 318)
        Me.GroupBox_ErrorMsg.Name = "GroupBox_ErrorMsg"
        Me.GroupBox_ErrorMsg.Size = New System.Drawing.Size(625, 309)
        Me.GroupBox_ErrorMsg.TabIndex = 26
        Me.GroupBox_ErrorMsg.TabStop = False
        Me.GroupBox_ErrorMsg.Text = "Error Message:"
        '
        'TextBox_ErrorMsg
        '
        Me.TextBox_ErrorMsg.BackColor = System.Drawing.Color.White
        Me.TextBox_ErrorMsg.Font = New System.Drawing.Font("Calibri", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_ErrorMsg.ForeColor = System.Drawing.Color.Red
        Me.TextBox_ErrorMsg.Location = New System.Drawing.Point(6, 20)
        Me.TextBox_ErrorMsg.Multiline = True
        Me.TextBox_ErrorMsg.Name = "TextBox_ErrorMsg"
        Me.TextBox_ErrorMsg.ReadOnly = True
        Me.TextBox_ErrorMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox_ErrorMsg.Size = New System.Drawing.Size(613, 268)
        Me.TextBox_ErrorMsg.TabIndex = 0
        '
        'GroupBox_SN
        '
        Me.GroupBox_SN.Controls.Add(Me.TextBox_SN)
        Me.GroupBox_SN.Location = New System.Drawing.Point(3, 96)
        Me.GroupBox_SN.Name = "GroupBox_SN"
        Me.GroupBox_SN.Size = New System.Drawing.Size(625, 100)
        Me.GroupBox_SN.TabIndex = 25
        Me.GroupBox_SN.TabStop = False
        Me.GroupBox_SN.Text = "SN:"
        '
        'TextBox_SN
        '
        Me.TextBox_SN.Font = New System.Drawing.Font("Calibri", 32.0!)
        Me.TextBox_SN.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_SN.Name = "TextBox_SN"
        Me.TextBox_SN.ReadOnly = True
        Me.TextBox_SN.Size = New System.Drawing.Size(613, 60)
        Me.TextBox_SN.TabIndex = 0
        '
        'grpCounter
        '
        Me.grpCounter.Controls.Add(Me.Button_Clean)
        Me.grpCounter.Controls.Add(Me.Button_Abort)
        Me.grpCounter.Controls.Add(Me.Button_Reset)
        Me.grpCounter.Controls.Add(Me.Label_Fail)
        Me.grpCounter.Controls.Add(Me.Label_Pass)
        Me.grpCounter.Controls.Add(Me.Label_Total)
        Me.grpCounter.Controls.Add(Me.lbltotal)
        Me.grpCounter.Controls.Add(Me.lblfail)
        Me.grpCounter.Controls.Add(Me.lblPass)
        Me.grpCounter.Location = New System.Drawing.Point(3, 633)
        Me.grpCounter.Name = "grpCounter"
        Me.grpCounter.Size = New System.Drawing.Size(625, 55)
        Me.grpCounter.TabIndex = 30
        Me.grpCounter.TabStop = False
        Me.grpCounter.Text = "Production"
        '
        'Button_Clean
        '
        Me.Button_Clean.Location = New System.Drawing.Point(452, 14)
        Me.Button_Clean.Name = "Button_Clean"
        Me.Button_Clean.Size = New System.Drawing.Size(75, 30)
        Me.Button_Clean.TabIndex = 29
        Me.Button_Clean.Text = "Clean"
        Me.Button_Clean.UseVisualStyleBackColor = True
        '
        'Button_Abort
        '
        Me.Button_Abort.Location = New System.Drawing.Point(537, 14)
        Me.Button_Abort.Name = "Button_Abort"
        Me.Button_Abort.Size = New System.Drawing.Size(75, 30)
        Me.Button_Abort.TabIndex = 28
        Me.Button_Abort.Text = "Stop"
        Me.Button_Abort.UseVisualStyleBackColor = True
        '
        'Button_Reset
        '
        Me.Button_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Button_Reset.Location = New System.Drawing.Point(638, 14)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(75, 33)
        Me.Button_Reset.TabIndex = 27
        Me.Button_Reset.Text = "Reset"
        Me.Button_Reset.UseVisualStyleBackColor = False
        '
        'Label_Fail
        '
        Me.Label_Fail.AutoSize = True
        Me.Label_Fail.Location = New System.Drawing.Point(313, 24)
        Me.Label_Fail.Name = "Label_Fail"
        Me.Label_Fail.Size = New System.Drawing.Size(35, 12)
        Me.Label_Fail.TabIndex = 26
        Me.Label_Fail.Text = "Fail:"
        '
        'Label_Pass
        '
        Me.Label_Pass.AutoSize = True
        Me.Label_Pass.Location = New System.Drawing.Point(158, 23)
        Me.Label_Pass.Name = "Label_Pass"
        Me.Label_Pass.Size = New System.Drawing.Size(35, 12)
        Me.Label_Pass.TabIndex = 25
        Me.Label_Pass.Text = "Good:"
        '
        'Label_Total
        '
        Me.Label_Total.AutoSize = True
        Me.Label_Total.Location = New System.Drawing.Point(6, 24)
        Me.Label_Total.Name = "Label_Total"
        Me.Label_Total.Size = New System.Drawing.Size(41, 12)
        Me.Label_Total.TabIndex = 2
        Me.Label_Total.Text = "Total:"
        '
        'lbltotal
        '
        Me.lbltotal.BackColor = System.Drawing.Color.Gainsboro
        Me.lbltotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltotal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lbltotal.Location = New System.Drawing.Point(49, 14)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(84, 30)
        Me.lbltotal.TabIndex = 0
        Me.lbltotal.Text = "0"
        Me.lbltotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfail
        '
        Me.lblfail.BackColor = System.Drawing.Color.Gainsboro
        Me.lblfail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblfail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblfail.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfail.ForeColor = System.Drawing.Color.Red
        Me.lblfail.Location = New System.Drawing.Point(352, 15)
        Me.lblfail.Name = "lblfail"
        Me.lblfail.Size = New System.Drawing.Size(84, 30)
        Me.lblfail.TabIndex = 1
        Me.lblfail.Text = "0"
        Me.lblfail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPass
        '
        Me.lblPass.BackColor = System.Drawing.Color.Gainsboro
        Me.lblPass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPass.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPass.Location = New System.Drawing.Point(199, 14)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(84, 30)
        Me.lblPass.TabIndex = 0
        Me.lblPass.Text = "0"
        Me.lblPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpCounter)
        Me.Controls.Add(Me.Label_Msg)
        Me.Controls.Add(Me.GroupBox_Scap)
        Me.Controls.Add(Me.ListBox_Msg)
        Me.Controls.Add(Me.GroupBox_ErrorMsg)
        Me.Controls.Add(Me.GroupBox_SN)
        Me.Name = "UI"
        Me.Size = New System.Drawing.Size(635, 799)
        Me.GroupBox_Scap.ResumeLayout(False)
        Me.GroupBox_Scap.PerformLayout()
        Me.GroupBox_ErrorMsg.ResumeLayout(False)
        Me.GroupBox_ErrorMsg.PerformLayout()
        Me.GroupBox_SN.ResumeLayout(False)
        Me.GroupBox_SN.PerformLayout()
        Me.grpCounter.ResumeLayout(False)
        Me.grpCounter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Label_Msg As System.Windows.Forms.Label
    Public WithEvents GroupBox_Scap As System.Windows.Forms.GroupBox
    Public WithEvents TextBox_Scap As System.Windows.Forms.TextBox
    Public WithEvents ListBox_Msg As System.Windows.Forms.ListBox
    Public WithEvents GroupBox_ErrorMsg As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox_SN As System.Windows.Forms.GroupBox
    Public WithEvents TextBox_SN As System.Windows.Forms.TextBox
    Public WithEvents TextBox_ErrorMsg As System.Windows.Forms.TextBox
    Public WithEvents grpCounter As System.Windows.Forms.GroupBox
    Public WithEvents Button_Reset As System.Windows.Forms.Button
    Public WithEvents Label_Fail As System.Windows.Forms.Label
    Public WithEvents Label_Pass As System.Windows.Forms.Label
    Public WithEvents Label_Total As System.Windows.Forms.Label
    Public WithEvents lbltotal As System.Windows.Forms.Label
    Public WithEvents lblfail As System.Windows.Forms.Label
    Public WithEvents lblPass As System.Windows.Forms.Label
    Friend WithEvents Button_Abort As System.Windows.Forms.Button
    Friend WithEvents Button_Clean As System.Windows.Forms.Button

End Class
