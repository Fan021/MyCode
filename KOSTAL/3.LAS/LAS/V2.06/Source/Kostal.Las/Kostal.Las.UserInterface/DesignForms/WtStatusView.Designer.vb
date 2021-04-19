<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WtStatusView
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
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.btnWtAbort = New System.Windows.Forms.Button()
        Me.lblWtName = New System.Windows.Forms.Label()
        Me.DG_WT_Data = New System.Windows.Forms.DataGridView()
        Me.RowName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RowValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblWtNumber = New System.Windows.Forms.Label()
        Me.btnWtReset = New System.Windows.Forms.Button()
        Me.cmbWT = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.rbSource_WT = New System.Windows.Forms.RadioButton()
        Me.rbSource_Station = New System.Windows.Forms.RadioButton()
        Me.DesignPanel.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        CType(Me.DG_WT_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Head.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.TableLayoutPanel_Body)
        Me.DesignPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignPanel.Location = New System.Drawing.Point(0, 0)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(920, 641)
        Me.DesignPanel.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 7
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.btnWtAbort, 3, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.lblWtName, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.DG_WT_Data, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.lblWtNumber, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.btnWtReset, 4, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.cmbWT, 5, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head, 6, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(920, 641)
        Me.TableLayoutPanel_Body.TabIndex = 16
        '
        'btnWtAbort
        '
        Me.btnWtAbort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnWtAbort.Location = New System.Drawing.Point(396, 3)
        Me.btnWtAbort.Name = "btnWtAbort"
        Me.btnWtAbort.Size = New System.Drawing.Size(125, 58)
        Me.btnWtAbort.TabIndex = 17
        Me.btnWtAbort.Text = "btnWtAbort"
        Me.btnWtAbort.UseVisualStyleBackColor = True
        '
        'lblWtName
        '
        Me.lblWtName.AutoSize = True
        Me.lblWtName.BackColor = System.Drawing.Color.Transparent
        Me.lblWtName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWtName.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.lblWtName.Location = New System.Drawing.Point(3, 0)
        Me.lblWtName.Name = "lblWtName"
        Me.lblWtName.Size = New System.Drawing.Size(125, 64)
        Me.lblWtName.TabIndex = 11
        Me.lblWtName.Text = "Number:"
        Me.lblWtName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DG_WT_Data
        '
        Me.DG_WT_Data.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.DG_WT_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DG_WT_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_WT_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_WT_Data.ColumnHeadersVisible = False
        Me.DG_WT_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RowName, Me.RowValue})
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.DG_WT_Data, 6)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_WT_Data.DefaultCellStyle = DataGridViewCellStyle1
        Me.DG_WT_Data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_WT_Data.Location = New System.Drawing.Point(3, 67)
        Me.DG_WT_Data.Name = "DG_WT_Data"
        Me.DG_WT_Data.RowHeadersVisible = False
        Me.DG_WT_Data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_WT_Data.RowTemplate.Height = 44
        Me.DG_WT_Data.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_WT_Data.Size = New System.Drawing.Size(780, 571)
        Me.DG_WT_Data.TabIndex = 10
        '
        'RowName
        '
        Me.RowName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.RowName.HeaderText = ""
        Me.RowName.Name = "RowName"
        Me.RowName.ReadOnly = True
        Me.RowName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RowName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RowValue
        '
        Me.RowValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.RowValue.HeaderText = ""
        Me.RowValue.Name = "RowValue"
        Me.RowValue.ReadOnly = True
        Me.RowValue.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RowValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'lblWtNumber
        '
        Me.lblWtNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblWtNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWtNumber.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.lblWtNumber.Location = New System.Drawing.Point(134, 0)
        Me.lblWtNumber.Name = "lblWtNumber"
        Me.lblWtNumber.Size = New System.Drawing.Size(125, 64)
        Me.lblWtNumber.TabIndex = 9
        Me.lblWtNumber.Text = "000"
        Me.lblWtNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnWtReset
        '
        Me.btnWtReset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnWtReset.Location = New System.Drawing.Point(527, 3)
        Me.btnWtReset.Name = "btnWtReset"
        Me.btnWtReset.Size = New System.Drawing.Size(125, 58)
        Me.btnWtReset.TabIndex = 15
        Me.btnWtReset.Text = "btnWtReset"
        Me.btnWtReset.UseVisualStyleBackColor = True
        '
        'cmbWT
        '
        Me.cmbWT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbWT.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.cmbWT.FormattingEnabled = True
        Me.cmbWT.Location = New System.Drawing.Point(658, 6)
        Me.cmbWT.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.cmbWT.Name = "cmbWT"
        Me.cmbWT.Size = New System.Drawing.Size(125, 37)
        Me.cmbWT.TabIndex = 12
        '
        'TableLayoutPanel_Head
        '
        Me.TableLayoutPanel_Head.ColumnCount = 1
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Head.Controls.Add(Me.rbSource_WT, 0, 0)
        Me.TableLayoutPanel_Head.Controls.Add(Me.rbSource_Station, 0, 1)
        Me.TableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head.Location = New System.Drawing.Point(786, 0)
        Me.TableLayoutPanel_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head.Name = "TableLayoutPanel_Head"
        Me.TableLayoutPanel_Head.RowCount = 2
        Me.TableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head.Size = New System.Drawing.Size(134, 64)
        Me.TableLayoutPanel_Head.TabIndex = 16
        '
        'rbSource_WT
        '
        Me.rbSource_WT.AutoSize = True
        Me.rbSource_WT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbSource_WT.Location = New System.Drawing.Point(3, 3)
        Me.rbSource_WT.Name = "rbSource_WT"
        Me.rbSource_WT.Size = New System.Drawing.Size(128, 26)
        Me.rbSource_WT.TabIndex = 13
        Me.rbSource_WT.Text = "rbSource_WT"
        Me.rbSource_WT.UseVisualStyleBackColor = True
        '
        'rbSource_Station
        '
        Me.rbSource_Station.AutoSize = True
        Me.rbSource_Station.Checked = True
        Me.rbSource_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rbSource_Station.Location = New System.Drawing.Point(3, 35)
        Me.rbSource_Station.Name = "rbSource_Station"
        Me.rbSource_Station.Size = New System.Drawing.Size(128, 26)
        Me.rbSource_Station.TabIndex = 14
        Me.rbSource_Station.TabStop = True
        Me.rbSource_Station.Text = "rbSource_Station"
        Me.rbSource_Station.UseVisualStyleBackColor = True
        '
        'WtStatusView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 641)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "WtStatusView"
        Me.Text = "WtStatusView"
        Me.DesignPanel.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        CType(Me.DG_WT_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Head.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Friend WithEvents DG_WT_Data As DataGridView
    Friend WithEvents RowName As DataGridViewTextBoxColumn
    Friend WithEvents RowValue As DataGridViewTextBoxColumn
    Friend WithEvents lblWtNumber As Label
    Friend WithEvents btnWtReset As Button
    Friend WithEvents rbSource_Station As RadioButton
    Friend WithEvents rbSource_WT As RadioButton
    Friend WithEvents cmbWT As ComboBox
    Friend WithEvents lblWtName As Label
    Friend WithEvents TableLayoutPanel_Body As TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Head As TableLayoutPanel
    Friend WithEvents btnWtAbort As Button
End Class
