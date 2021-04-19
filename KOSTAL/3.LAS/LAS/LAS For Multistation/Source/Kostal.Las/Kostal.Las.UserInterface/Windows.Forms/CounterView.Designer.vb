<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CounterView
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.easyPanel = New System.Windows.Forms.Panel()
        Me.lblIO = New System.Windows.Forms.Button()
        Me.lblPercent = New System.Windows.Forms.Button()
        Me.lblNIO = New System.Windows.Forms.Button()
        Me.lblPpm = New System.Windows.Forms.Button()
        Me.lblCycleTime = New System.Windows.Forms.Button()
        Me.CounterTable = New System.Windows.Forms.TableLayoutPanel()
        Me.easyPanel.SuspendLayout()
        Me.CounterTable.SuspendLayout()
        Me.SuspendLayout()
        '
        'easyPanel
        '
        Me.easyPanel.Controls.Add(Me.CounterTable)
        Me.easyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.easyPanel.Location = New System.Drawing.Point(0, 0)
        Me.easyPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.easyPanel.Name = "easyPanel"
        Me.easyPanel.Size = New System.Drawing.Size(1020, 57)
        Me.easyPanel.TabIndex = 0
        '
        'lblIO
        '
        Me.lblIO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIO.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen
        Me.lblIO.FlatAppearance.BorderSize = 3
        Me.lblIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblIO.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIO.Location = New System.Drawing.Point(211, 6)
        Me.lblIO.Margin = New System.Windows.Forms.Padding(7, 6, 12, 6)
        Me.lblIO.Name = "lblIO"
        Me.lblIO.Size = New System.Drawing.Size(185, 45)
        Me.lblIO.TabIndex = 8
        Me.lblIO.TabStop = False
        Me.lblIO.Text = "0"
        Me.lblIO.UseVisualStyleBackColor = False
        '
        'lblPercent
        '
        Me.lblPercent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPercent.FlatAppearance.BorderSize = 3
        Me.lblPercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPercent.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercent.Location = New System.Drawing.Point(624, 6)
        Me.lblPercent.Margin = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(180, 45)
        Me.lblPercent.TabIndex = 11
        Me.lblPercent.TabStop = False
        Me.lblPercent.Text = "0 %"
        Me.lblPercent.UseVisualStyleBackColor = False
        '
        'lblNIO
        '
        Me.lblNIO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNIO.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick
        Me.lblNIO.FlatAppearance.BorderSize = 3
        Me.lblNIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblNIO.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNIO.Location = New System.Drawing.Point(420, 6)
        Me.lblNIO.Margin = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.lblNIO.Name = "lblNIO"
        Me.lblNIO.Size = New System.Drawing.Size(180, 45)
        Me.lblNIO.TabIndex = 9
        Me.lblNIO.TabStop = False
        Me.lblNIO.Text = "0"
        Me.lblNIO.UseVisualStyleBackColor = False
        '
        'lblPpm
        '
        Me.lblPpm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPpm.FlatAppearance.BorderSize = 3
        Me.lblPpm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPpm.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPpm.Location = New System.Drawing.Point(828, 6)
        Me.lblPpm.Margin = New System.Windows.Forms.Padding(12, 6, 7, 6)
        Me.lblPpm.Name = "lblPpm"
        Me.lblPpm.Size = New System.Drawing.Size(185, 45)
        Me.lblPpm.TabIndex = 10
        Me.lblPpm.TabStop = False
        Me.lblPpm.Text = "0 ppm"
        Me.lblPpm.UseVisualStyleBackColor = False
        '
        'lblCycleTime
        '
        Me.lblCycleTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCycleTime.FlatAppearance.BorderSize = 3
        Me.lblCycleTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCycleTime.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCycleTime.Location = New System.Drawing.Point(12, 6)
        Me.lblCycleTime.Margin = New System.Windows.Forms.Padding(12, 6, 7, 6)
        Me.lblCycleTime.Name = "lblCycleTime"
        Me.lblCycleTime.Size = New System.Drawing.Size(185, 45)
        Me.lblCycleTime.TabIndex = 12
        Me.lblCycleTime.TabStop = False
        Me.lblCycleTime.Text = "0.0 mS"
        Me.lblCycleTime.UseVisualStyleBackColor = False
        '
        'CounterTable
        '
        Me.CounterTable.ColumnCount = 7
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.CounterTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.CounterTable.Controls.Add(Me.lblCycleTime, 2, 0)
        Me.CounterTable.Controls.Add(Me.lblPpm, 6, 0)
        Me.CounterTable.Controls.Add(Me.lblNIO, 4, 0)
        Me.CounterTable.Controls.Add(Me.lblPercent, 5, 0)
        Me.CounterTable.Controls.Add(Me.lblIO, 3, 0)
        Me.CounterTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CounterTable.Location = New System.Drawing.Point(0, 0)
        Me.CounterTable.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.CounterTable.Name = "CounterTable"
        Me.CounterTable.RowCount = 1
        Me.CounterTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.CounterTable.Size = New System.Drawing.Size(1020, 57)
        Me.CounterTable.TabIndex = 0
        '
        'CounterView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 57)
        Me.Controls.Add(Me.easyPanel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "CounterView"
        Me.Text = "EasyGui"
        Me.easyPanel.ResumeLayout(False)
        Me.CounterTable.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents easyPanel As System.Windows.Forms.Panel
    Friend WithEvents CounterTable As TableLayoutPanel
    Friend WithEvents lblCycleTime As Button
    Friend WithEvents lblPpm As Button
    Friend WithEvents lblNIO As Button
    Friend WithEvents lblPercent As Button
    Friend WithEvents lblIO As Button
End Class
