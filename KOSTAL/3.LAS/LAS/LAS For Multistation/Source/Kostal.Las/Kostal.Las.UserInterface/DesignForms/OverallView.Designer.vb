<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OverallView
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.picBoxMain = New System.Windows.Forms.PictureBox()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.lblMainTip = New System.Windows.Forms.Label()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picBoxMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DesignPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.picBoxMain, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.dgView, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMainTip, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1028, 736)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'picBoxMain
        '
        Me.picBoxMain.BackColor = System.Drawing.Color.Transparent
        Me.picBoxMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picBoxMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBoxMain.Location = New System.Drawing.Point(20, 36)
        Me.picBoxMain.Margin = New System.Windows.Forms.Padding(0)
        Me.picBoxMain.Name = "picBoxMain"
        Me.picBoxMain.Size = New System.Drawing.Size(986, 390)
        Me.picBoxMain.TabIndex = 2
        Me.picBoxMain.TabStop = False
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.AllowUserToResizeColumns = False
        Me.dgView.AllowUserToResizeRows = False
        Me.dgView.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgView.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgView.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgView.Location = New System.Drawing.Point(20, 530)
        Me.dgView.Margin = New System.Windows.Forms.Padding(0)
        Me.dgView.MultiSelect = False
        Me.dgView.Name = "dgView"
        Me.dgView.RowTemplate.Height = 23
        Me.dgView.Size = New System.Drawing.Size(986, 100)
        Me.dgView.TabIndex = 1
        '
        'lblMainTip
        '
        Me.lblMainTip.AutoSize = True
        Me.lblMainTip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMainTip.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblMainTip.ForeColor = System.Drawing.Color.Navy
        Me.lblMainTip.Location = New System.Drawing.Point(23, 0)
        Me.lblMainTip.Name = "lblMainTip"
        Me.lblMainTip.Size = New System.Drawing.Size(980, 36)
        Me.lblMainTip.TabIndex = 3
        Me.lblMainTip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.DesignPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignPanel.Location = New System.Drawing.Point(0, 0)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(1028, 736)
        Me.DesignPanel.TabIndex = 4
        '
        'OverallView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 736)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "OverallView"
        Me.Text = "OverallView"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.picBoxMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DesignPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents dgView As DataGridView
    Friend WithEvents DesignPanel As Panel
    Friend WithEvents lblMainTip As Label
    Friend WithEvents picBoxMain As PictureBox
End Class
