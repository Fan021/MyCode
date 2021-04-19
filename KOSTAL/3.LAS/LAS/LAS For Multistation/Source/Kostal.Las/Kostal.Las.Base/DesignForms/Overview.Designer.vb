<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Overview
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.picBoxSystem = New System.Windows.Forms.PictureBox()
        Me.picArticle = New System.Windows.Forms.PictureBox()
        Me.DesignPanel.SuspendLayout()
        Me.grpPicture.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.picBoxSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.grpPicture)
        Me.DesignPanel.Location = New System.Drawing.Point(12, 12)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(768, 570)
        Me.DesignPanel.TabIndex = 1
        '
        'grpPicture
        '
        Me.grpPicture.BackColor = System.Drawing.Color.Transparent
        Me.grpPicture.Controls.Add(Me.Panel2)
        Me.grpPicture.Controls.Add(Me.Panel1)
        Me.grpPicture.Controls.Add(Me.picArticle)
        Me.grpPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPicture.Location = New System.Drawing.Point(0, 0)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Padding = New System.Windows.Forms.Padding(2)
        Me.grpPicture.Size = New System.Drawing.Size(768, 570)
        Me.grpPicture.TabIndex = 24
        Me.grpPicture.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.dgView)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(2, 399)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(764, 169)
        Me.Panel2.TabIndex = 22
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(46, 150)
        Me.Panel3.TabIndex = 1
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.AllowUserToResizeColumns = False
        Me.dgView.AllowUserToResizeRows = False
        Me.dgView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgView.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgView.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgView.Location = New System.Drawing.Point(82, 4)
        Me.dgView.MultiSelect = False
        Me.dgView.Name = "dgView"
        Me.dgView.RowTemplate.Height = 23
        Me.dgView.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgView.Size = New System.Drawing.Size(641, 171)
        Me.dgView.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.picBoxSystem)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(2, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(764, 377)
        Me.Panel1.TabIndex = 21
        '
        'picBoxSystem
        '
        Me.picBoxSystem.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.overview
        Me.picBoxSystem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picBoxSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBoxSystem.Location = New System.Drawing.Point(0, 0)
        Me.picBoxSystem.Name = "picBoxSystem"
        Me.picBoxSystem.Size = New System.Drawing.Size(764, 377)
        Me.picBoxSystem.TabIndex = 0
        Me.picBoxSystem.TabStop = False
        '
        'picArticle
        '
        Me.picArticle.BackColor = System.Drawing.Color.White
        Me.picArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picArticle.Location = New System.Drawing.Point(2, 16)
        Me.picArticle.Name = "picArticle"
        Me.picArticle.Size = New System.Drawing.Size(764, 552)
        Me.picArticle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picArticle.TabIndex = 20
        Me.picArticle.TabStop = False
        '
        'Overview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 611)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "Overview"
        Me.Text = "Overview"
        Me.DesignPanel.ResumeLayout(False)
        Me.grpPicture.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.picBoxSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Public WithEvents grpPicture As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents picBoxSystem As PictureBox
    Public WithEvents picArticle As PictureBox
    Friend WithEvents dgView As DataGridView
    Friend WithEvents Panel3 As Panel
End Class
