<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.StatusForm = New System.Windows.Forms.StatusStrip()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Left_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.Timer_Logo = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Head = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Head_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Titile = New System.Windows.Forms.Label()
        Me.Panel_Head_Body_Left = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Head_Body_Left = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_UserLevelValue = New System.Windows.Forms.Label()
        Me.Label_UserLevel = New System.Windows.Forms.Label()
        Me.Label_UserName = New System.Windows.Forms.Label()
        Me.Label_CellValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Head_Body_Left_UserName = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_UserLoginOut = New System.Windows.Forms.Label()
        Me.Label_UserNameValue = New System.Windows.Forms.Label()
        Me.Label_Cell = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Body_Head_Logo = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Time = New System.Windows.Forms.Label()
        Me.PictureBox_Log = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Left.SuspendLayout()
        Me.TableLayoutPanel_Left.SuspendLayout()
        Me.TableLayoutPanel_Head.SuspendLayout()
        Me.Panel_Head.SuspendLayout()
        Me.TableLayoutPanel_Head_Body.SuspendLayout()
        Me.Panel_Head_Body_Left.SuspendLayout()
        Me.HmiTableLayoutPanel_Head_Body_Left.SuspendLayout()
        Me.TableLayoutPanel_Head_Body_Left_UserName.SuspendLayout()
        Me.TableLayoutPanel_Body_Head_Logo.SuspendLayout()
        CType(Me.PictureBox_Log, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusForm
        '
        Me.StatusForm.Location = New System.Drawing.Point(0, 616)
        Me.StatusForm.Name = "StatusForm"
        Me.StatusForm.Size = New System.Drawing.Size(780, 22)
        Me.StatusForm.TabIndex = 1
        Me.StatusForm.Text = "StatusStrip1"
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Left, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Body, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 85)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(780, 531)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'Panel_Left
        '
        Me.Panel_Left.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel_Left.Controls.Add(Me.TableLayoutPanel_Left)
        Me.Panel_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Left.Location = New System.Drawing.Point(1, 0)
        Me.Panel_Left.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.Panel_Left.Name = "Panel_Left"
        Me.Panel_Left.Padding = New System.Windows.Forms.Padding(2, 1, 2, 2)
        Me.Panel_Left.Size = New System.Drawing.Size(155, 531)
        Me.Panel_Left.TabIndex = 0
        '
        'TableLayoutPanel_Left
        '
        Me.TableLayoutPanel_Left.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Left.ColumnCount = 1
        Me.TableLayoutPanel_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Left.Controls.Add(Me.TableLayoutPanel_Left_Head, 0, 0)
        Me.TableLayoutPanel_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Left.Location = New System.Drawing.Point(2, 1)
        Me.TableLayoutPanel_Left.Name = "TableLayoutPanel_Left"
        Me.TableLayoutPanel_Left.RowCount = 8
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Left.Size = New System.Drawing.Size(151, 528)
        Me.TableLayoutPanel_Left.TabIndex = 0
        '
        'TableLayoutPanel_Left_Head
        '
        Me.TableLayoutPanel_Left_Head.ColumnCount = 2
        Me.TableLayoutPanel_Left_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Left_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Left_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Left_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Left_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Left_Head.Name = "TableLayoutPanel_Left_Head"
        Me.TableLayoutPanel_Left_Head.RowCount = 6
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.2!))
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.2!))
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.2!))
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.2!))
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.2!))
        Me.TableLayoutPanel_Left_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Left_Head.Size = New System.Drawing.Size(151, 232)
        Me.TableLayoutPanel_Left_Head.TabIndex = 6
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Font = New System.Drawing.Font("SimSun", 8.0!)
        Me.Panel_Body.Location = New System.Drawing.Point(156, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Padding = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.Panel_Body.Size = New System.Drawing.Size(623, 531)
        Me.Panel_Body.TabIndex = 1
        '
        'Timer_Logo
        '
        Me.Timer_Logo.Enabled = True
        '
        'TableLayoutPanel_Head
        '
        Me.TableLayoutPanel_Head.ColumnCount = 1
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Head.Controls.Add(Me.Panel_Head, 0, 0)
        Me.TableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head.Name = "TableLayoutPanel_Head"
        Me.TableLayoutPanel_Head.RowCount = 1
        Me.TableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Head.Size = New System.Drawing.Size(780, 85)
        Me.TableLayoutPanel_Head.TabIndex = 0
        '
        'Panel_Head
        '
        Me.Panel_Head.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel_Head.Controls.Add(Me.TableLayoutPanel_Head_Body)
        Me.Panel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Head.Location = New System.Drawing.Point(1, 0)
        Me.Panel_Head.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Panel_Head.Name = "Panel_Head"
        Me.Panel_Head.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel_Head.Size = New System.Drawing.Size(778, 85)
        Me.Panel_Head.TabIndex = 0
        '
        'TableLayoutPanel_Head_Body
        '
        Me.TableLayoutPanel_Head_Body.ColumnCount = 3
        Me.TableLayoutPanel_Head_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Head_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.0!))
        Me.TableLayoutPanel_Head_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
        Me.TableLayoutPanel_Head_Body.Controls.Add(Me.Label_Titile, 1, 0)
        Me.TableLayoutPanel_Head_Body.Controls.Add(Me.Panel_Head_Body_Left, 0, 0)
        Me.TableLayoutPanel_Head_Body.Controls.Add(Me.TableLayoutPanel_Body_Head_Logo, 2, 0)
        Me.TableLayoutPanel_Head_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Body.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel_Head_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head_Body.Name = "TableLayoutPanel_Head_Body"
        Me.TableLayoutPanel_Head_Body.RowCount = 1
        Me.TableLayoutPanel_Head_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Head_Body.Size = New System.Drawing.Size(774, 81)
        Me.TableLayoutPanel_Head_Body.TabIndex = 0
        '
        'Label_Titile
        '
        Me.Label_Titile.AutoSize = True
        Me.Label_Titile.BackColor = System.Drawing.Color.Transparent
        Me.Label_Titile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Titile.Font = New System.Drawing.Font("Calibri", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Titile.ForeColor = System.Drawing.Color.Black
        Me.Label_Titile.Location = New System.Drawing.Point(154, 0)
        Me.Label_Titile.Margin = New System.Windows.Forms.Padding(0)
        Me.Label_Titile.Name = "Label_Titile"
        Me.Label_Titile.Size = New System.Drawing.Size(495, 81)
        Me.Label_Titile.TabIndex = 1
        Me.Label_Titile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel_Head_Body_Left
        '
        Me.Panel_Head_Body_Left.BackColor = System.Drawing.Color.White
        Me.Panel_Head_Body_Left.Controls.Add(Me.HmiTableLayoutPanel_Head_Body_Left)
        Me.Panel_Head_Body_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Head_Body_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Head_Body_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Head_Body_Left.Name = "Panel_Head_Body_Left"
        Me.Panel_Head_Body_Left.Size = New System.Drawing.Size(154, 81)
        Me.Panel_Head_Body_Left.TabIndex = 2
        '
        'HmiTableLayoutPanel_Head_Body_Left
        '
        Me.HmiTableLayoutPanel_Head_Body_Left.ColumnCount = 2
        Me.HmiTableLayoutPanel_Head_Body_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Head_Body_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.Label_UserLevelValue, 1, 2)
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.Label_UserLevel, 0, 2)
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.Label_UserName, 0, 1)
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.Label_CellValue, 1, 0)
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.TableLayoutPanel_Head_Body_Left_UserName, 1, 1)
        Me.HmiTableLayoutPanel_Head_Body_Left.Controls.Add(Me.Label_Cell, 0, 0)
        Me.HmiTableLayoutPanel_Head_Body_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Head_Body_Left.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Head_Body_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Head_Body_Left.Name = "HmiTableLayoutPanel_Head_Body_Left"
        Me.HmiTableLayoutPanel_Head_Body_Left.RowCount = 3
        Me.HmiTableLayoutPanel_Head_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Head_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Head_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.HmiTableLayoutPanel_Head_Body_Left.Size = New System.Drawing.Size(154, 81)
        Me.HmiTableLayoutPanel_Head_Body_Left.TabIndex = 3
        '
        'Label_UserLevelValue
        '
        Me.Label_UserLevelValue.AutoSize = True
        Me.Label_UserLevelValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_UserLevelValue.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label_UserLevelValue.Location = New System.Drawing.Point(54, 57)
        Me.Label_UserLevelValue.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_UserLevelValue.Name = "Label_UserLevelValue"
        Me.Label_UserLevelValue.Size = New System.Drawing.Size(97, 21)
        Me.Label_UserLevelValue.TabIndex = 5
        Me.Label_UserLevelValue.Text = "Label1"
        Me.Label_UserLevelValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_UserLevel
        '
        Me.Label_UserLevel.AutoSize = True
        Me.Label_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_UserLevel.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_UserLevel.Location = New System.Drawing.Point(3, 57)
        Me.Label_UserLevel.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_UserLevel.Name = "Label_UserLevel"
        Me.Label_UserLevel.Size = New System.Drawing.Size(45, 21)
        Me.Label_UserLevel.TabIndex = 4
        Me.Label_UserLevel.Text = "Label1"
        Me.Label_UserLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_UserName
        '
        Me.Label_UserName.AutoSize = True
        Me.Label_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_UserName.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_UserName.Location = New System.Drawing.Point(3, 30)
        Me.Label_UserName.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_UserName.Name = "Label_UserName"
        Me.Label_UserName.Size = New System.Drawing.Size(45, 21)
        Me.Label_UserName.TabIndex = 3
        Me.Label_UserName.Text = "Label1"
        Me.Label_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_CellValue
        '
        Me.Label_CellValue.AutoSize = True
        Me.Label_CellValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_CellValue.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label_CellValue.Location = New System.Drawing.Point(54, 3)
        Me.Label_CellValue.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_CellValue.Name = "Label_CellValue"
        Me.Label_CellValue.Size = New System.Drawing.Size(97, 21)
        Me.Label_CellValue.TabIndex = 2
        Me.Label_CellValue.Text = "Label1"
        Me.Label_CellValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel_Head_Body_Left_UserName
        '
        Me.TableLayoutPanel_Head_Body_Left_UserName.ColumnCount = 2
        Me.TableLayoutPanel_Head_Body_Left_UserName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Body_Left_UserName.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Body_Left_UserName.Controls.Add(Me.Label_UserLoginOut, 0, 0)
        Me.TableLayoutPanel_Head_Body_Left_UserName.Controls.Add(Me.Label_UserNameValue, 0, 0)
        Me.TableLayoutPanel_Head_Body_Left_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Body_Left_UserName.Location = New System.Drawing.Point(51, 27)
        Me.TableLayoutPanel_Head_Body_Left_UserName.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head_Body_Left_UserName.Name = "TableLayoutPanel_Head_Body_Left_UserName"
        Me.TableLayoutPanel_Head_Body_Left_UserName.RowCount = 1
        Me.TableLayoutPanel_Head_Body_Left_UserName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Body_Left_UserName.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Body_Left_UserName.Size = New System.Drawing.Size(103, 27)
        Me.TableLayoutPanel_Head_Body_Left_UserName.TabIndex = 0
        '
        'Label_UserLoginOut
        '
        Me.Label_UserLoginOut.AutoSize = True
        Me.Label_UserLoginOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_UserLoginOut.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Underline)
        Me.Label_UserLoginOut.ForeColor = System.Drawing.Color.Blue
        Me.Label_UserLoginOut.Location = New System.Drawing.Point(54, 3)
        Me.Label_UserLoginOut.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_UserLoginOut.Name = "Label_UserLoginOut"
        Me.Label_UserLoginOut.Size = New System.Drawing.Size(46, 21)
        Me.Label_UserLoginOut.TabIndex = 5
        Me.Label_UserLoginOut.Text = "Label1"
        Me.Label_UserLoginOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_UserNameValue
        '
        Me.Label_UserNameValue.AutoSize = True
        Me.Label_UserNameValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_UserNameValue.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.Label_UserNameValue.Location = New System.Drawing.Point(3, 3)
        Me.Label_UserNameValue.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_UserNameValue.Name = "Label_UserNameValue"
        Me.Label_UserNameValue.Size = New System.Drawing.Size(45, 21)
        Me.Label_UserNameValue.TabIndex = 4
        Me.Label_UserNameValue.Text = "Label1"
        Me.Label_UserNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label_Cell
        '
        Me.Label_Cell.AutoSize = True
        Me.Label_Cell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Cell.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_Cell.Location = New System.Drawing.Point(3, 3)
        Me.Label_Cell.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Cell.Name = "Label_Cell"
        Me.Label_Cell.Size = New System.Drawing.Size(45, 21)
        Me.Label_Cell.TabIndex = 1
        Me.Label_Cell.Text = "Label1"
        Me.Label_Cell.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel_Body_Head_Logo
        '
        Me.TableLayoutPanel_Body_Head_Logo.ColumnCount = 1
        Me.TableLayoutPanel_Body_Head_Logo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Head_Logo.Controls.Add(Me.Label_Time, 0, 1)
        Me.TableLayoutPanel_Body_Head_Logo.Controls.Add(Me.PictureBox_Log, 0, 0)
        Me.TableLayoutPanel_Body_Head_Logo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head_Logo.Location = New System.Drawing.Point(649, 0)
        Me.TableLayoutPanel_Body_Head_Logo.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head_Logo.Name = "TableLayoutPanel_Body_Head_Logo"
        Me.TableLayoutPanel_Body_Head_Logo.RowCount = 2
        Me.TableLayoutPanel_Body_Head_Logo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Head_Logo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head_Logo.Size = New System.Drawing.Size(125, 81)
        Me.TableLayoutPanel_Body_Head_Logo.TabIndex = 3
        '
        'Label_Time
        '
        Me.Label_Time.AutoSize = True
        Me.Label_Time.BackColor = System.Drawing.Color.Transparent
        Me.Label_Time.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Time.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Time.ForeColor = System.Drawing.Color.Black
        Me.Label_Time.Location = New System.Drawing.Point(1, 65)
        Me.Label_Time.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Time.Name = "Label_Time"
        Me.Label_Time.Size = New System.Drawing.Size(123, 15)
        Me.Label_Time.TabIndex = 2
        Me.Label_Time.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PictureBox_Log
        '
        Me.PictureBox_Log.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_Log.Image = Global.Kochi.HMI.MainControl.My.Resources.Resources.logo_screen_145px2
        Me.PictureBox_Log.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox_Log.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox_Log.Name = "PictureBox_Log"
        Me.PictureBox_Log.Size = New System.Drawing.Size(125, 64)
        Me.PictureBox_Log.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_Log.TabIndex = 1
        Me.PictureBox_Log.TabStop = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 638)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Controls.Add(Me.StatusForm)
        Me.Controls.Add(Me.TableLayoutPanel_Head)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HMI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Head.ResumeLayout(False)
        Me.Panel_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Body.PerformLayout()
        Me.Panel_Head_Body_Left.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Head_Body_Left.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Head_Body_Left.PerformLayout()
        Me.TableLayoutPanel_Head_Body_Left_UserName.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Body_Left_UserName.PerformLayout()
        Me.TableLayoutPanel_Body_Head_Logo.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head_Logo.PerformLayout()
        CType(Me.PictureBox_Log, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusForm As System.Windows.Forms.StatusStrip
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Left As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Left_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Timer_Logo As System.Windows.Forms.Timer
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Head As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Head_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label_Titile As System.Windows.Forms.Label
    Friend WithEvents Panel_Head_Body_Left As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Head_Body_Left As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_UserLevelValue As System.Windows.Forms.Label
    Friend WithEvents Label_UserLevel As System.Windows.Forms.Label
    Friend WithEvents Label_UserName As System.Windows.Forms.Label
    Friend WithEvents Label_CellValue As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Head_Body_Left_UserName As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_UserLoginOut As System.Windows.Forms.Label
    Friend WithEvents Label_UserNameValue As System.Windows.Forms.Label
    Friend WithEvents Label_Cell As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Body_Head_Logo As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label_Time As System.Windows.Forms.Label
    Friend WithEvents PictureBox_Log As System.Windows.Forms.PictureBox

End Class
