<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenLoginForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChildrenLoginForm))
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Mid = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Mid = New Kostal.Las.UserInterface.HMITableLayoutPanel(Me.components)
        Me.TableLayoutPanel_Body_Mid_Password = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_PassWord = New System.Windows.Forms.Button()
        Me.Panel_PassWord = New System.Windows.Forms.Panel()
        Me.TextBox_PassWord = New System.Windows.Forms.TextBox()
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Body_Mid_UserName = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_UserName = New System.Windows.Forms.Button()
        Me.Panel_UserNane = New System.Windows.Forms.Panel()
        Me.Comobox_UserName = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Login = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Password.SuspendLayout()
        Me.Panel_PassWord.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_UserName.SuspendLayout()
        Me.Panel_UserNane.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Mid, 1, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.15789!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'Panel_Mid
        '
        Me.Panel_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.Panel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Mid.Location = New System.Drawing.Point(93, 55)
        Me.Panel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Mid.Name = "Panel_Mid"
        Me.Panel_Mid.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel_Mid.Size = New System.Drawing.Size(280, 334)
        Me.Panel_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_Password, 0, 2)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.Label_Title, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_UserName, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel1, 0, 3)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 5
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(276, 330)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid_Password
        '
        Me.TableLayoutPanel_Body_Mid_Password.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Body_Mid_Password.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid_Password.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Mid_Password.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid_Password.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.0!))
        Me.TableLayoutPanel_Body_Mid_Password.Controls.Add(Me.Button_PassWord, 1, 0)
        Me.TableLayoutPanel_Body_Mid_Password.Controls.Add(Me.Panel_PassWord, 2, 0)
        Me.TableLayoutPanel_Body_Mid_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Password.Location = New System.Drawing.Point(1, 131)
        Me.TableLayoutPanel_Body_Mid_Password.Margin = New System.Windows.Forms.Padding(1, 1, 1, 0)
        Me.TableLayoutPanel_Body_Mid_Password.Name = "TableLayoutPanel_Body_Mid_Password"
        Me.TableLayoutPanel_Body_Mid_Password.RowCount = 1
        Me.TableLayoutPanel_Body_Mid_Password.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Password.Size = New System.Drawing.Size(274, 84)
        Me.TableLayoutPanel_Body_Mid_Password.TabIndex = 5
        '
        'Button_PassWord
        '
        Me.Button_PassWord.BackgroundImage = CType(resources.GetObject("Button_PassWord.BackgroundImage"), System.Drawing.Image)
        Me.Button_PassWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_PassWord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_PassWord.FlatAppearance.BorderSize = 0
        Me.Button_PassWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_PassWord.Location = New System.Drawing.Point(30, 3)
        Me.Button_PassWord.Name = "Button_PassWord"
        Me.Button_PassWord.Size = New System.Drawing.Size(15, 78)
        Me.Button_PassWord.TabIndex = 3
        Me.Button_PassWord.UseVisualStyleBackColor = True
        '
        'Panel_PassWord
        '
        Me.Panel_PassWord.Controls.Add(Me.TextBox_PassWord)
        Me.Panel_PassWord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_PassWord.Location = New System.Drawing.Point(48, 0)
        Me.Panel_PassWord.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_PassWord.Name = "Panel_PassWord"
        Me.Panel_PassWord.Size = New System.Drawing.Size(226, 84)
        Me.Panel_PassWord.TabIndex = 4
        '
        'TextBox_PassWord
        '
        Me.TextBox_PassWord.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox_PassWord.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.TextBox_PassWord.Location = New System.Drawing.Point(0, 31)
        Me.TextBox_PassWord.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox_PassWord.Name = "TextBox_PassWord"
        Me.TextBox_PassWord.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox_PassWord.Size = New System.Drawing.Size(226, 23)
        Me.TextBox_PassWord.TabIndex = 6
        '
        'Label_Title
        '
        Me.Label_Title.AutoSize = True
        Me.Label_Title.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label_Title.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.Label_Title.Location = New System.Drawing.Point(3, 3)
        Me.Label_Title.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(104, 39)
        Me.Label_Title.TabIndex = 3
        Me.Label_Title.Text = "Please Login"
        Me.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel_Body_Mid_UserName
        '
        Me.TableLayoutPanel_Body_Mid_UserName.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Body_Mid_UserName.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid_UserName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Mid_UserName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid_UserName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.0!))
        Me.TableLayoutPanel_Body_Mid_UserName.Controls.Add(Me.Button_UserName, 1, 0)
        Me.TableLayoutPanel_Body_Mid_UserName.Controls.Add(Me.Panel_UserNane, 2, 0)
        Me.TableLayoutPanel_Body_Mid_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_UserName.Location = New System.Drawing.Point(1, 46)
        Me.TableLayoutPanel_Body_Mid_UserName.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Mid_UserName.Name = "TableLayoutPanel_Body_Mid_UserName"
        Me.TableLayoutPanel_Body_Mid_UserName.RowCount = 1
        Me.TableLayoutPanel_Body_Mid_UserName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_UserName.Size = New System.Drawing.Size(274, 83)
        Me.TableLayoutPanel_Body_Mid_UserName.TabIndex = 4
        '
        'Button_UserName
        '
        Me.Button_UserName.BackgroundImage = CType(resources.GetObject("Button_UserName.BackgroundImage"), System.Drawing.Image)
        Me.Button_UserName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_UserName.FlatAppearance.BorderSize = 0
        Me.Button_UserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_UserName.Location = New System.Drawing.Point(30, 3)
        Me.Button_UserName.Name = "Button_UserName"
        Me.Button_UserName.Size = New System.Drawing.Size(15, 77)
        Me.Button_UserName.TabIndex = 3
        Me.Button_UserName.UseVisualStyleBackColor = True
        '
        'Panel_UserNane
        '
        Me.Panel_UserNane.Controls.Add(Me.Comobox_UserName)
        Me.Panel_UserNane.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UserNane.Location = New System.Drawing.Point(48, 0)
        Me.Panel_UserNane.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UserNane.Name = "Panel_UserNane"
        Me.Panel_UserNane.Size = New System.Drawing.Size(226, 83)
        Me.Panel_UserNane.TabIndex = 4
        '
        'Comobox_UserName
        '
        Me.Comobox_UserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Comobox_UserName.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.Comobox_UserName.FormattingEnabled = True
        Me.Comobox_UserName.Location = New System.Drawing.Point(0, 32)
        Me.Comobox_UserName.Margin = New System.Windows.Forms.Padding(0)
        Me.Comobox_UserName.Name = "Comobox_UserName"
        Me.Comobox_UserName.Size = New System.Drawing.Size(226, 31)
        Me.Comobox_UserName.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Button_Cancel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button_Login, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 218)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(270, 59)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'Button_Cancel
        '
        Me.Button_Cancel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.Button_Cancel.ForeColor = System.Drawing.Color.Black
        Me.Button_Cancel.Location = New System.Drawing.Point(138, 3)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(129, 53)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = False
        '
        'Button_Login
        '
        Me.Button_Login.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Button_Login.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Login.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.Button_Login.ForeColor = System.Drawing.Color.Black
        Me.Button_Login.Location = New System.Drawing.Point(3, 3)
        Me.Button_Login.Name = "Button_Login"
        Me.Button_Login.Size = New System.Drawing.Size(129, 53)
        Me.Button_Login.TabIndex = 0
        Me.Button_Login.Text = "Login"
        Me.Button_Login.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'ChildrenLoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenLoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LoginForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_Password.ResumeLayout(False)
        Me.Panel_PassWord.ResumeLayout(False)
        Me.Panel_PassWord.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_UserName.ResumeLayout(False)
        Me.Panel_UserNane.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Mid As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Mid As HMITableLayoutPanel
    Friend WithEvents Label_Title As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Body_Mid_UserName As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_UserName As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel_Body_Mid_Password As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_PassWord As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Login As System.Windows.Forms.Button
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Panel_UserNane As System.Windows.Forms.Panel
    Friend WithEvents Panel_PassWord As System.Windows.Forms.Panel
    Friend WithEvents TextBox_PassWord As System.Windows.Forms.TextBox
    Friend WithEvents Comobox_UserName As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
