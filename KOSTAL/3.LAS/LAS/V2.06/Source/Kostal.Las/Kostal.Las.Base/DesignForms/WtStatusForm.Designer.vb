<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WtStatusForm
	Inherits System.Windows.Forms.Form

	'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

	'Wird vom Windows Form-Designer benötigt.
	Private components As System.ComponentModel.IContainer

	'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
	'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
	'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblWtNumber = New System.Windows.Forms.Label()
        Me.DG_WT_Data = New System.Windows.Forms.DataGridView()
        Me.RowName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RowValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblWtName = New System.Windows.Forms.Label()
        Me.cmbWT = New System.Windows.Forms.ComboBox()
        Me.rbSource_WT = New System.Windows.Forms.RadioButton()
        Me.rbSource_Station = New System.Windows.Forms.RadioButton()
        Me.btnWtReset = New System.Windows.Forms.Button()
        CType(Me.DG_WT_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblWtNumber
        '
        Me.lblWtNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblWtNumber.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWtNumber.Location = New System.Drawing.Point(210, 5)
        Me.lblWtNumber.Name = "lblWtNumber"
        Me.lblWtNumber.Size = New System.Drawing.Size(82, 53)
        Me.lblWtNumber.TabIndex = 1
        Me.lblWtNumber.Text = "000"
        Me.lblWtNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DG_WT_Data
        '
        Me.DG_WT_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DG_WT_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DG_WT_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_WT_Data.ColumnHeadersVisible = False
        Me.DG_WT_Data.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RowName, Me.RowValue})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_WT_Data.DefaultCellStyle = DataGridViewCellStyle5
        Me.DG_WT_Data.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_WT_Data.Location = New System.Drawing.Point(12, 57)
        Me.DG_WT_Data.Name = "DG_WT_Data"
        Me.DG_WT_Data.RowHeadersVisible = False
        Me.DG_WT_Data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_WT_Data.RowTemplate.Height = 44
        Me.DG_WT_Data.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_WT_Data.Size = New System.Drawing.Size(885, 559)
        Me.DG_WT_Data.TabIndex = 2
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
        'lblWtName
        '
        Me.lblWtName.AutoSize = True
        Me.lblWtName.BackColor = System.Drawing.Color.Transparent
        Me.lblWtName.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWtName.Location = New System.Drawing.Point(67, 3)
        Me.lblWtName.Name = "lblWtName"
        Me.lblWtName.Size = New System.Drawing.Size(153, 45)
        Me.lblWtName.TabIndex = 4
        Me.lblWtName.Text = "Number:"
        Me.lblWtName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbWT
        '
        Me.cmbWT.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbWT.FormattingEnabled = True
        Me.cmbWT.Location = New System.Drawing.Point(594, 13)
        Me.cmbWT.Name = "cmbWT"
        Me.cmbWT.Size = New System.Drawing.Size(106, 34)
        Me.cmbWT.TabIndex = 5
        '
        'rbSource_WT
        '
        Me.rbSource_WT.AutoSize = True
        Me.rbSource_WT.Location = New System.Drawing.Point(723, 12)
        Me.rbSource_WT.Name = "rbSource_WT"
        Me.rbSource_WT.Size = New System.Drawing.Size(89, 16)
        Me.rbSource_WT.TabIndex = 6
        Me.rbSource_WT.Text = "rbSource_WT"
        Me.rbSource_WT.UseVisualStyleBackColor = True
        '
        'rbSource_Station
        '
        Me.rbSource_Station.AutoSize = True
        Me.rbSource_Station.Checked = True
        Me.rbSource_Station.Location = New System.Drawing.Point(723, 32)
        Me.rbSource_Station.Name = "rbSource_Station"
        Me.rbSource_Station.Size = New System.Drawing.Size(119, 16)
        Me.rbSource_Station.TabIndex = 7
        Me.rbSource_Station.TabStop = True
        Me.rbSource_Station.Text = "rbSource_Station"
        Me.rbSource_Station.UseVisualStyleBackColor = True
        '
        'btnWtReset
        '
        Me.btnWtReset.Location = New System.Drawing.Point(469, 13)
        Me.btnWtReset.Name = "btnWtReset"
        Me.btnWtReset.Size = New System.Drawing.Size(119, 34)
        Me.btnWtReset.TabIndex = 8
        Me.btnWtReset.Text = "btnWtReset"
        Me.btnWtReset.UseVisualStyleBackColor = True
        '
        'WtStatusForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 628)
        Me.Controls.Add(Me.btnWtReset)
        Me.Controls.Add(Me.rbSource_Station)
        Me.Controls.Add(Me.rbSource_WT)
        Me.Controls.Add(Me.cmbWT)
        Me.Controls.Add(Me.lblWtName)
        Me.Controls.Add(Me.DG_WT_Data)
        Me.Controls.Add(Me.lblWtNumber)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WtStatusForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "WtStatus"
        CType(Me.DG_WT_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblWtNumber As System.Windows.Forms.Label
	Friend WithEvents DG_WT_Data As System.Windows.Forms.DataGridView
	Friend WithEvents Data As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents lblWtName As System.Windows.Forms.Label
	Friend WithEvents RowName As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents RowValue As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents cmbWT As System.Windows.Forms.ComboBox
	Friend WithEvents rbSource_WT As System.Windows.Forms.RadioButton
	Friend WithEvents rbSource_Station As System.Windows.Forms.RadioButton
	Friend WithEvents btnWtReset As System.Windows.Forms.Button
End Class
