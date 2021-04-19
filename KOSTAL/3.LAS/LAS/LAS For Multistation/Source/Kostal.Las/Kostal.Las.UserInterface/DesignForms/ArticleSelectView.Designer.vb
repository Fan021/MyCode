<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ArticleSelectView
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.KeyPanel = New System.Windows.Forms.Panel()
        Me.lstMatchBox = New System.Windows.Forms.ListBox()
        Me.DG_Article = New System.Windows.Forms.DataGridView()
        Me.DG_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.CBArticle = New System.Windows.Forms.ComboBox()
        Me.DesignPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.KeyPanel.SuspendLayout()
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.BackColor = System.Drawing.Color.White
        Me.DesignPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.DesignPanel.Location = New System.Drawing.Point(62, 62)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(633, 352)
        Me.DesignPanel.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.KeyPanel, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.405406!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.08108!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.51351!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(633, 352)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'KeyPanel
        '
        Me.KeyPanel.Controls.Add(Me.lstMatchBox)
        Me.KeyPanel.Controls.Add(Me.DG_Article)
        Me.KeyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KeyPanel.Location = New System.Drawing.Point(66, 109)
        Me.KeyPanel.Name = "KeyPanel"
        Me.KeyPanel.Size = New System.Drawing.Size(500, 204)
        Me.KeyPanel.TabIndex = 1
        '
        'lstMatchBox
        '
        Me.lstMatchBox.FormattingEnabled = True
        Me.lstMatchBox.ItemHeight = 12
        Me.lstMatchBox.Location = New System.Drawing.Point(0, 0)
        Me.lstMatchBox.Name = "lstMatchBox"
        Me.lstMatchBox.Size = New System.Drawing.Size(194, 112)
        Me.lstMatchBox.TabIndex = 68
        Me.lstMatchBox.Visible = False
        '
        'DG_Article
        '
        Me.DG_Article.AllowUserToAddRows = False
        Me.DG_Article.AllowUserToDeleteRows = False
        Me.DG_Article.AllowUserToResizeColumns = False
        Me.DG_Article.AllowUserToResizeRows = False
        Me.DG_Article.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DG_Article.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Article.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_Article.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_Name, Me.DG_Value})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Article.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Article.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Article.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Article.Location = New System.Drawing.Point(0, 0)
        Me.DG_Article.Name = "DG_Article"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Article.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Article.RowTemplate.Height = 23
        Me.DG_Article.Size = New System.Drawing.Size(500, 204)
        Me.DG_Article.TabIndex = 67
        '
        'DG_Name
        '
        Me.DG_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DG_Name.HeaderText = "Name"
        Me.DG_Name.Name = "DG_Name"
        Me.DG_Name.ReadOnly = True
        Me.DG_Name.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DG_Value
        '
        Me.DG_Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DG_Value.HeaderText = "Value"
        Me.DG_Value.Name = "DG_Value"
        Me.DG_Value.ReadOnly = True
        Me.DG_Value.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Font = New System.Drawing.Font("SimSun", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(66, 17)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(3)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(500, 40)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "PLEASE SELECT AN ARTICLE:"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOK, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CBArticle, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(66, 63)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(500, 40)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(433, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 34)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnOK.Location = New System.Drawing.Point(353, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 34)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'CBArticle
        '
        Me.CBArticle.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.CBArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBArticle.Font = New System.Drawing.Font("Consolas", 20.0!)
        Me.CBArticle.FormattingEnabled = True
        Me.CBArticle.Location = New System.Drawing.Point(3, 3)
        Me.CBArticle.MaxDropDownItems = 20
        Me.CBArticle.Name = "CBArticle"
        Me.CBArticle.Size = New System.Drawing.Size(344, 40)
        Me.CBArticle.TabIndex = 4
        '
        'ArticleSelectView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 706)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "ArticleSelectView"
        Me.Text = "ArticleView"
        Me.DesignPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.KeyPanel.ResumeLayout(False)
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents KeyPanel As Panel
    Public WithEvents lstMatchBox As ListBox
    Friend WithEvents lblMessage As Label
    Public WithEvents DG_Article As DataGridView
    Public WithEvents DG_Name As DataGridViewTextBoxColumn
    Public WithEvents DG_Value As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Public WithEvents btnCancel As Button
    Public WithEvents btnOK As Button
    Public WithEvents CBArticle As ComboBox
End Class
