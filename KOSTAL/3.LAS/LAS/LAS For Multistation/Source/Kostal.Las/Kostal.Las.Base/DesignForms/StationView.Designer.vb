Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StationView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.KeyPanel = New System.Windows.Forms.Panel()
        Me.TabControlSystem = New System.Windows.Forms.TabControl()
        Me.TabControlStations = New System.Windows.Forms.TabControl()
        Me.picArticle = New System.Windows.Forms.PictureBox()
        Me.HmiButton_Cancal = New Kostal.Las.Base.HMIButton()
        Me.DesignPanel.SuspendLayout()
        Me.grpPicture.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.KeyPanel.SuspendLayout()
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.grpPicture)
        Me.DesignPanel.Location = New System.Drawing.Point(2, 12)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(629, 553)
        Me.DesignPanel.TabIndex = 0
        '
        'grpPicture
        '
        Me.grpPicture.BackColor = System.Drawing.Color.Silver
        Me.grpPicture.Controls.Add(Me.TableLayoutPanel1)
        Me.grpPicture.Controls.Add(Me.picArticle)
        Me.grpPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPicture.Location = New System.Drawing.Point(0, 0)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Padding = New System.Windows.Forms.Padding(2)
        Me.grpPicture.Size = New System.Drawing.Size(629, 553)
        Me.grpPicture.TabIndex = 24
        Me.grpPicture.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.KeyPanel, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(625, 535)
        Me.TableLayoutPanel1.TabIndex = 31
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblMessage, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.HmiButton_Cancal, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(31, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(562, 30)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Silver
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Font = New System.Drawing.Font("Calibri", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(3, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(443, 30)
        Me.lblMessage.TabIndex = 5
        Me.lblMessage.Text = "Name"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'KeyPanel
        '
        Me.KeyPanel.Controls.Add(Me.TabControlSystem)
        Me.KeyPanel.Controls.Add(Me.TabControlStations)
        Me.KeyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KeyPanel.Location = New System.Drawing.Point(34, 33)
        Me.KeyPanel.Name = "KeyPanel"
        Me.KeyPanel.Size = New System.Drawing.Size(556, 448)
        Me.KeyPanel.TabIndex = 1
        '
        'TabControlSystem
        '
        Me.TabControlSystem.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlSystem.Location = New System.Drawing.Point(216, 317)
        Me.TabControlSystem.Name = "TabControlSystem"
        Me.TabControlSystem.SelectedIndex = 0
        Me.TabControlSystem.Size = New System.Drawing.Size(200, 100)
        Me.TabControlSystem.TabIndex = 32
        '
        'TabControlStations
        '
        Me.TabControlStations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlStations.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlStations.Location = New System.Drawing.Point(0, 0)
        Me.TabControlStations.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControlStations.Name = "TabControlStations"
        Me.TabControlStations.Padding = New System.Drawing.Point(0, 0)
        Me.TabControlStations.SelectedIndex = 0
        Me.TabControlStations.Size = New System.Drawing.Size(556, 448)
        Me.TabControlStations.TabIndex = 31
        Me.TabControlStations.Visible = False
        '
        'picArticle
        '
        Me.picArticle.BackColor = System.Drawing.Color.White
        Me.picArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picArticle.Location = New System.Drawing.Point(2, 16)
        Me.picArticle.Name = "picArticle"
        Me.picArticle.Size = New System.Drawing.Size(625, 535)
        Me.picArticle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picArticle.TabIndex = 20
        Me.picArticle.TabStop = False
        '
        'HmiButton_Cancal
        '
        Me.HmiButton_Cancal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancal.Location = New System.Drawing.Point(449, 0)
        Me.HmiButton_Cancal.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Cancal.MarginHeight = 0
        Me.HmiButton_Cancal.Name = "HmiButton_Cancal"
        Me.HmiButton_Cancal.Size = New System.Drawing.Size(113, 30)
        Me.HmiButton_Cancal.TabIndex = 6
        '
        'StationView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 653)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "StationView"
        Me.Text = "Form1"
        Me.DesignPanel.ResumeLayout(False)
        Me.grpPicture.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.KeyPanel.ResumeLayout(False)
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents DesignPanel As Panel
    Public WithEvents grpPicture As GroupBox
    Public WithEvents picArticle As PictureBox
    Public WithEvents TableLayoutPanel1 As TableLayoutPanel
    Public WithEvents TableLayoutPanel2 As TableLayoutPanel
    Public WithEvents lblMessage As Label
    Public WithEvents HmiButton_Cancal As HMIButton
    Public WithEvents KeyPanel As Panel
    Public WithEvents TabControlSystem As TabControl
    Public WithEvents TabControlStations As TabControl
End Class
