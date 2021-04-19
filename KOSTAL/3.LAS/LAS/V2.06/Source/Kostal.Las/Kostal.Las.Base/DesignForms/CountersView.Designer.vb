Imports System.Windows.Forms
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ArticleCounter
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            Terminate()
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DG_Counter = New System.Windows.Forms.DataGridView()
        Me.btnReset = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.colArticle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInWork = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPass = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DesignPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DG_Counter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.TableLayoutPanel1)
        Me.DesignPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignPanel.Location = New System.Drawing.Point(0, 0)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(945, 346)
        Me.DesignPanel.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DG_Counter, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(945, 346)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'DG_Counter
        '
        Me.DG_Counter.AllowUserToAddRows = False
        Me.DG_Counter.AllowUserToDeleteRows = False
        Me.DG_Counter.AllowUserToResizeColumns = False
        Me.DG_Counter.AllowUserToResizeRows = False
        Me.DG_Counter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DG_Counter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Counter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Counter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_Counter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.btnReset, Me.colArticle, Me.colInWork, Me.colPass, Me.colFail})
        Me.DG_Counter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Counter.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Counter.Location = New System.Drawing.Point(3, 3)
        Me.DG_Counter.Name = "DG_Counter"
        Me.DG_Counter.ReadOnly = True
        Me.DG_Counter.RowHeadersVisible = False
        Me.DG_Counter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Counter.RowTemplate.Height = 23
        Me.DG_Counter.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Counter.Size = New System.Drawing.Size(939, 322)
        Me.DG_Counter.TabIndex = 4
        '
        'btnReset
        '
        Me.btnReset.HeaderText = "Reset"
        Me.btnReset.Name = "btnReset"
        Me.btnReset.ReadOnly = True
        Me.btnReset.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colArticle
        '
        Me.colArticle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colArticle.HeaderText = "Article"
        Me.colArticle.Name = "colArticle"
        Me.colArticle.ReadOnly = True
        Me.colArticle.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colArticle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colInWork
        '
        Me.colInWork.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colInWork.HeaderText = "InWork"
        Me.colInWork.Name = "colInWork"
        Me.colInWork.ReadOnly = True
        Me.colInWork.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colInWork.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colPass
        '
        Me.colPass.HeaderText = "PASS"
        Me.colPass.Name = "colPass"
        Me.colPass.ReadOnly = True
        Me.colPass.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colPass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colFail
        '
        Me.colFail.HeaderText = "FAIL"
        Me.colFail.Name = "colFail"
        Me.colFail.ReadOnly = True
        Me.colFail.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colFail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ArticleCounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 346)
        Me.Controls.Add(Me.DesignPanel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ArticleCounter"
        Me.Text = "Counter"
        Me.DesignPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DG_Counter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DesignPanel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DG_Counter As DataGridView
    Friend WithEvents btnReset As DataGridViewButtonColumn
    Friend WithEvents colArticle As DataGridViewTextBoxColumn
    Friend WithEvents colInWork As DataGridViewTextBoxColumn
    Friend WithEvents colPass As DataGridViewTextBoxColumn
    Friend WithEvents colFail As DataGridViewTextBoxColumn
End Class
