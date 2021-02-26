<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.lblLine = New System.Windows.Forms.Label()
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusForm = New System.Windows.Forms.StatusStrip()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.ShowDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowArticleDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowLineControlConfigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SMTNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BitNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.timCycle = New System.Windows.Forms.Timer(Me.components)
        Me.Ui_Right = New Kostal.Mes.Milling.UI()
        Me.Ui_Left = New Kostal.Mes.Milling.UI()
        Me.PLCNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLine
        '
        Me.lblLine.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.Location = New System.Drawing.Point(4, 108)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(1255, 2)
        Me.lblLine.TabIndex = 17
        Me.lblLine.Text = "Label5"
        '
        'Label_Title
        '
        Me.Label_Title.AutoSize = True
        Me.Label_Title.Font = New System.Drawing.Font("Calibri", 45.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Title.Location = New System.Drawing.Point(103, 22)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(988, 74)
        Me.Label_Title.TabIndex = 18
        Me.Label_Title.Text = "Kostal MES Milling InterLock--Aurotek"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(633, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(2, 809)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Label5"
        '
        'StatusForm
        '
        Me.StatusForm.BackColor = System.Drawing.Color.LightCyan
        Me.StatusForm.Location = New System.Drawing.Point(0, 910)
        Me.StatusForm.Name = "StatusForm"
        Me.StatusForm.Size = New System.Drawing.Size(1264, 22)
        Me.StatusForm.TabIndex = 20
        Me.StatusForm.Text = "StatusStrip1"
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDataToolStripMenuItem, Me.ShowArticleDataToolStripMenuItem, Me.ShowLineControlConfigToolStripMenuItem, Me.SMTNumberToolStripMenuItem, Me.BitNumberToolStripMenuItem, Me.PLCNumberToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(1264, 25)
        Me.MainMenu.TabIndex = 23
        Me.MainMenu.Text = "MainMenu"
        '
        'ShowDataToolStripMenuItem
        '
        Me.ShowDataToolStripMenuItem.Name = "ShowDataToolStripMenuItem"
        Me.ShowDataToolStripMenuItem.Size = New System.Drawing.Size(94, 21)
        Me.ShowDataToolStripMenuItem.Text = "Article Count"
        '
        'ShowArticleDataToolStripMenuItem
        '
        Me.ShowArticleDataToolStripMenuItem.Name = "ShowArticleDataToolStripMenuItem"
        Me.ShowArticleDataToolStripMenuItem.Size = New System.Drawing.Size(60, 21)
        Me.ShowArticleDataToolStripMenuItem.Text = "Article "
        '
        'ShowLineControlConfigToolStripMenuItem
        '
        Me.ShowLineControlConfigToolStripMenuItem.Name = "ShowLineControlConfigToolStripMenuItem"
        Me.ShowLineControlConfigToolStripMenuItem.Size = New System.Drawing.Size(124, 21)
        Me.ShowLineControlConfigToolStripMenuItem.Text = "LineControlConfig"
        '
        'SMTNumberToolStripMenuItem
        '
        Me.SMTNumberToolStripMenuItem.Name = "SMTNumberToolStripMenuItem"
        Me.SMTNumberToolStripMenuItem.Size = New System.Drawing.Size(94, 21)
        Me.SMTNumberToolStripMenuItem.Text = "SMTNumber"
        '
        'BitNumberToolStripMenuItem
        '
        Me.BitNumberToolStripMenuItem.Name = "BitNumberToolStripMenuItem"
        Me.BitNumberToolStripMenuItem.Size = New System.Drawing.Size(83, 21)
        Me.BitNumberToolStripMenuItem.Text = "BitNumber"
        '
        'timCycle
        '
        '
        'Ui_Right
        '
        Me.Ui_Right.Location = New System.Drawing.Point(638, 115)
        Me.Ui_Right.Name = "Ui_Right"
        Me.Ui_Right.Size = New System.Drawing.Size(627, 800)
        Me.Ui_Right.TabIndex = 22
        '
        'Ui_Left
        '
        Me.Ui_Left.Location = New System.Drawing.Point(0, 115)
        Me.Ui_Left.Name = "Ui_Left"
        Me.Ui_Left.Size = New System.Drawing.Size(627, 800)
        Me.Ui_Left.TabIndex = 21
        '
        'PLCNumberToolStripMenuItem
        '
        Me.PLCNumberToolStripMenuItem.Name = "PLCNumberToolStripMenuItem"
        Me.PLCNumberToolStripMenuItem.Size = New System.Drawing.Size(89, 21)
        Me.PLCNumberToolStripMenuItem.Text = "PLCNumber"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1264, 932)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.Ui_Right)
        Me.Controls.Add(Me.Ui_Left)
        Me.Controls.Add(Me.StatusForm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label_Title)
        Me.Controls.Add(Me.lblLine)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "kostal.Mes.Milling"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents Label_Title As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents StatusForm As System.Windows.Forms.StatusStrip
    Friend WithEvents Ui_Right As Kostal.Mes.Milling.UI
    Public WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Public WithEvents ShowDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timCycle As System.Windows.Forms.Timer
    Friend WithEvents Ui_Left As Kostal.Mes.Milling.UI
    Public WithEvents ShowArticleDataToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowLineControlConfigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SMTNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BitNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PLCNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
