<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NoArticleCount
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.bdnInfo = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtCurrentPage = New System.Windows.Forms.ToolStripTextBox()
        Me.lblPageCount = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.bdsInfo = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox_Check = New System.Windows.Forms.GroupBox()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Button_Check = New System.Windows.Forms.Button()
        Me.TextBox_Article = New System.Windows.Forms.TextBox()
        Me.Label_Article = New System.Windows.Forms.Label()
        Me.TextBox_ID = New System.Windows.Forms.TextBox()
        Me.Label_ID = New System.Windows.Forms.Label()
        Me.TextBox_Edit_Id = New System.Windows.Forms.TextBox()
        Me.Label_Edit_Id = New System.Windows.Forms.Label()
        Me.Label_Edit_Article = New System.Windows.Forms.Label()
        Me.TextBox_Edit_Article = New System.Windows.Forms.TextBox()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.Button_Delete = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.bdnInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bdnInfo.SuspendLayout()
        CType(Me.bdsInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Check.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 60)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(654, 486)
        Me.DataGridView1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.bdnInfo)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 552)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(654, 53)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'bdnInfo
        '
        Me.bdnInfo.AddNewItem = Nothing
        Me.bdnInfo.CountItem = Me.BindingNavigatorCountItem
        Me.bdnInfo.DeleteItem = Nothing
        Me.bdnInfo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.ToolStripLabel1, Me.ToolStripSeparator1, Me.txtCurrentPage, Me.lblPageCount, Me.ToolStripSeparator2, Me.ToolStripLabel2})
        Me.bdnInfo.Location = New System.Drawing.Point(3, 17)
        Me.bdnInfo.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bdnInfo.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bdnInfo.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bdnInfo.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bdnInfo.Name = "bdnInfo"
        Me.bdnInfo.PositionItem = Me.BindingNavigatorPositionItem
        Me.bdnInfo.Size = New System.Drawing.Size(648, 25)
        Me.bdnInfo.TabIndex = 0
        Me.bdnInfo.Text = "BindingNavigator1"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(39, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(90, 22)
        Me.ToolStripLabel1.Text = "Previous Page"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'txtCurrentPage
        '
        Me.txtCurrentPage.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtCurrentPage.Name = "txtCurrentPage"
        Me.txtCurrentPage.ReadOnly = True
        Me.txtCurrentPage.Size = New System.Drawing.Size(50, 25)
        '
        'lblPageCount
        '
        Me.lblPageCount.Name = "lblPageCount"
        Me.lblPageCount.Size = New System.Drawing.Size(20, 22)
        Me.lblPageCount.Text = "/5"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(68, 22)
        Me.ToolStripLabel2.Text = "Next Page"
        '
        'GroupBox_Check
        '
        Me.GroupBox_Check.Controls.Add(Me.Button_Reset)
        Me.GroupBox_Check.Controls.Add(Me.Button_Check)
        Me.GroupBox_Check.Controls.Add(Me.TextBox_Article)
        Me.GroupBox_Check.Controls.Add(Me.Label_Article)
        Me.GroupBox_Check.Controls.Add(Me.TextBox_ID)
        Me.GroupBox_Check.Controls.Add(Me.Label_ID)
        Me.GroupBox_Check.Location = New System.Drawing.Point(12, 0)
        Me.GroupBox_Check.Name = "GroupBox_Check"
        Me.GroupBox_Check.Size = New System.Drawing.Size(654, 54)
        Me.GroupBox_Check.TabIndex = 2
        Me.GroupBox_Check.TabStop = False
        Me.GroupBox_Check.Text = "Query:"
        '
        'Button_Reset
        '
        Me.Button_Reset.Location = New System.Drawing.Point(554, 21)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(75, 23)
        Me.Button_Reset.TabIndex = 5
        Me.Button_Reset.Text = "Reset"
        Me.Button_Reset.UseVisualStyleBackColor = True
        '
        'Button_Check
        '
        Me.Button_Check.Location = New System.Drawing.Point(462, 21)
        Me.Button_Check.Name = "Button_Check"
        Me.Button_Check.Size = New System.Drawing.Size(75, 23)
        Me.Button_Check.TabIndex = 4
        Me.Button_Check.Text = "Query"
        Me.Button_Check.UseVisualStyleBackColor = True
        '
        'TextBox_Article
        '
        Me.TextBox_Article.Location = New System.Drawing.Point(288, 22)
        Me.TextBox_Article.Name = "TextBox_Article"
        Me.TextBox_Article.Size = New System.Drawing.Size(140, 21)
        Me.TextBox_Article.TabIndex = 3
        '
        'Label_Article
        '
        Me.Label_Article.AutoSize = True
        Me.Label_Article.Location = New System.Drawing.Point(231, 26)
        Me.Label_Article.Name = "Label_Article"
        Me.Label_Article.Size = New System.Drawing.Size(53, 12)
        Me.Label_Article.TabIndex = 2
        Me.Label_Article.Text = "Article:"
        '
        'TextBox_ID
        '
        Me.TextBox_ID.Location = New System.Drawing.Point(65, 23)
        Me.TextBox_ID.Name = "TextBox_ID"
        Me.TextBox_ID.Size = New System.Drawing.Size(140, 21)
        Me.TextBox_ID.TabIndex = 1
        '
        'Label_ID
        '
        Me.Label_ID.AutoSize = True
        Me.Label_ID.Location = New System.Drawing.Point(17, 26)
        Me.Label_ID.Name = "Label_ID"
        Me.Label_ID.Size = New System.Drawing.Size(23, 12)
        Me.Label_ID.TabIndex = 0
        Me.Label_ID.Text = "ID:"
        '
        'TextBox_Edit_Id
        '
        Me.TextBox_Edit_Id.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TextBox_Edit_Id.Location = New System.Drawing.Point(678, 91)
        Me.TextBox_Edit_Id.Name = "TextBox_Edit_Id"
        Me.TextBox_Edit_Id.ReadOnly = True
        Me.TextBox_Edit_Id.Size = New System.Drawing.Size(83, 21)
        Me.TextBox_Edit_Id.TabIndex = 3
        '
        'Label_Edit_Id
        '
        Me.Label_Edit_Id.AutoSize = True
        Me.Label_Edit_Id.Location = New System.Drawing.Point(682, 60)
        Me.Label_Edit_Id.Name = "Label_Edit_Id"
        Me.Label_Edit_Id.Size = New System.Drawing.Size(23, 12)
        Me.Label_Edit_Id.TabIndex = 4
        Me.Label_Edit_Id.Text = "ID:"
        '
        'Label_Edit_Article
        '
        Me.Label_Edit_Article.AutoSize = True
        Me.Label_Edit_Article.Location = New System.Drawing.Point(682, 129)
        Me.Label_Edit_Article.Name = "Label_Edit_Article"
        Me.Label_Edit_Article.Size = New System.Drawing.Size(53, 12)
        Me.Label_Edit_Article.TabIndex = 5
        Me.Label_Edit_Article.Text = "Article:"
        '
        'TextBox_Edit_Article
        '
        Me.TextBox_Edit_Article.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TextBox_Edit_Article.Location = New System.Drawing.Point(678, 160)
        Me.TextBox_Edit_Article.Name = "TextBox_Edit_Article"
        Me.TextBox_Edit_Article.Size = New System.Drawing.Size(83, 21)
        Me.TextBox_Edit_Article.TabIndex = 6
        '
        'Button_Add
        '
        Me.Button_Add.Location = New System.Drawing.Point(678, 204)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(75, 23)
        Me.Button_Add.TabIndex = 6
        Me.Button_Add.Text = "Add"
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'Button_Delete
        '
        Me.Button_Delete.Location = New System.Drawing.Point(678, 243)
        Me.Button_Delete.Name = "Button_Delete"
        Me.Button_Delete.Size = New System.Drawing.Size(75, 23)
        Me.Button_Delete.TabIndex = 10
        Me.Button_Delete.Text = "Delete"
        Me.Button_Delete.UseVisualStyleBackColor = True
        '
        'NoArticleCount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 608)
        Me.Controls.Add(Me.Button_Delete)
        Me.Controls.Add(Me.Button_Add)
        Me.Controls.Add(Me.TextBox_Edit_Article)
        Me.Controls.Add(Me.Label_Edit_Article)
        Me.Controls.Add(Me.Label_Edit_Id)
        Me.Controls.Add(Me.TextBox_Edit_Id)
        Me.Controls.Add(Me.GroupBox_Check)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "NoArticleCount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ArticleCount"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.bdnInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bdnInfo.ResumeLayout(False)
        Me.bdnInfo.PerformLayout()
        CType(Me.bdsInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Check.ResumeLayout(False)
        Me.GroupBox_Check.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents bdnInfo As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtCurrentPage As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents lblPageCount As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents bdsInfo As System.Windows.Forms.BindingSource
    Friend WithEvents GroupBox_Check As System.Windows.Forms.GroupBox
    Friend WithEvents Button_Reset As System.Windows.Forms.Button
    Friend WithEvents Button_Check As System.Windows.Forms.Button
    Friend WithEvents TextBox_Article As System.Windows.Forms.TextBox
    Friend WithEvents Label_Article As System.Windows.Forms.Label
    Friend WithEvents TextBox_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_Edit_Id As System.Windows.Forms.TextBox
    Friend WithEvents Label_Edit_Id As System.Windows.Forms.Label
    Friend WithEvents Label_Edit_Article As System.Windows.Forms.Label
    Friend WithEvents TextBox_Edit_Article As System.Windows.Forms.TextBox
    Friend WithEvents Button_Add As System.Windows.Forms.Button
    Friend WithEvents Button_Delete As System.Windows.Forms.Button
End Class
