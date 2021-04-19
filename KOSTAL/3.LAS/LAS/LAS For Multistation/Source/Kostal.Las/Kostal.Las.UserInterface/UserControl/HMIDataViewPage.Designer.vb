<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HMIDataViewPage
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component List.
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_TotalPage = New System.Windows.Forms.Label()
        Me.Button_Go = New System.Windows.Forms.Button()
        Me.Button_DownLast = New System.Windows.Forms.Button()
        Me.Button_Down = New System.Windows.Forms.Button()
        Me.Button_Up = New System.Windows.Forms.Button()
        Me.Button_UpFirst = New System.Windows.Forms.Button()
        Me.Label_Total = New System.Windows.Forms.Label()
        Me.HmiTextBox_Page = New HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(373, 37)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 8
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_TotalPage, 4, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_Go, 5, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_DownLast, 7, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_Down, 6, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_Up, 2, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Button_UpFirst, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Total, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Page, 3, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(373, 37)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'Label_TotalPage
        '
        Me.Label_TotalPage.AutoSize = True
        Me.Label_TotalPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_TotalPage.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_TotalPage.Location = New System.Drawing.Point(225, 3)
        Me.Label_TotalPage.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_TotalPage.Name = "Label_TotalPage"
        Me.Label_TotalPage.Size = New System.Drawing.Size(31, 31)
        Me.Label_TotalPage.TabIndex = 7
        Me.Label_TotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button_Go
        '
        Me.Button_Go.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_Go.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Go.FlatAppearance.BorderSize = 0
        Me.Button_Go.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Go.Location = New System.Drawing.Point(262, 3)
        Me.Button_Go.Name = "Button_Go"
        Me.Button_Go.Size = New System.Drawing.Size(31, 31)
        Me.Button_Go.TabIndex = 5
        Me.Button_Go.UseVisualStyleBackColor = True
        '
        'Button_DownLast
        '
        Me.Button_DownLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_DownLast.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_DownLast.FlatAppearance.BorderSize = 0
        Me.Button_DownLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_DownLast.Location = New System.Drawing.Point(336, 3)
        Me.Button_DownLast.Name = "Button_DownLast"
        Me.Button_DownLast.Size = New System.Drawing.Size(34, 31)
        Me.Button_DownLast.TabIndex = 3
        Me.Button_DownLast.UseVisualStyleBackColor = True
        '
        'Button_Down
        '
        Me.Button_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_Down.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Down.FlatAppearance.BorderSize = 0
        Me.Button_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Down.Location = New System.Drawing.Point(299, 3)
        Me.Button_Down.Name = "Button_Down"
        Me.Button_Down.Size = New System.Drawing.Size(31, 31)
        Me.Button_Down.TabIndex = 2
        Me.Button_Down.UseVisualStyleBackColor = True
        '
        'Button_Up
        '
        Me.Button_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_Up.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Up.FlatAppearance.BorderSize = 0
        Me.Button_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Up.Location = New System.Drawing.Point(114, 3)
        Me.Button_Up.Name = "Button_Up"
        Me.Button_Up.Size = New System.Drawing.Size(31, 31)
        Me.Button_Up.TabIndex = 1
        Me.Button_Up.UseVisualStyleBackColor = True
        '
        'Button_UpFirst
        '
        Me.Button_UpFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_UpFirst.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_UpFirst.FlatAppearance.BorderSize = 0
        Me.Button_UpFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_UpFirst.Location = New System.Drawing.Point(77, 3)
        Me.Button_UpFirst.Name = "Button_UpFirst"
        Me.Button_UpFirst.Size = New System.Drawing.Size(31, 31)
        Me.Button_UpFirst.TabIndex = 0
        Me.Button_UpFirst.UseVisualStyleBackColor = True
        '
        'Label_Total
        '
        Me.Label_Total.AutoSize = True
        Me.Label_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Total.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Total.Location = New System.Drawing.Point(3, 3)
        Me.Label_Total.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Total.Name = "Label_Total"
        Me.Label_Total.Size = New System.Drawing.Size(68, 31)
        Me.Label_Total.TabIndex = 4
        Me.Label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_Page
        '
        Me.HmiTextBox_Page.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Page.Location = New System.Drawing.Point(151, 3)
        Me.HmiTextBox_Page.Name = "HmiTextBox_Page"
        Me.HmiTextBox_Page.Size = New System.Drawing.Size(68, 31)
        Me.HmiTextBox_Page.TabIndex = 6
        '
        'HMIDataViewPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Body)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "HMIDataViewPage"
        Me.Size = New System.Drawing.Size(373, 37)
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel_Body As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Public WithEvents Label_TotalPage As System.Windows.Forms.Label
    Public WithEvents Button_Go As System.Windows.Forms.Button
    Public WithEvents Button_DownLast As System.Windows.Forms.Button
    Public WithEvents Button_Down As System.Windows.Forms.Button
    Public WithEvents Button_Up As System.Windows.Forms.Button
    Public WithEvents Button_UpFirst As System.Windows.Forms.Button
    Public WithEvents Label_Total As System.Windows.Forms.Label
    Public WithEvents HmiTextBox_Page As HMITextBox

End Class
