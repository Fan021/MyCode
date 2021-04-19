<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LanguageSelectView
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Language = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.CBLanguage = New System.Windows.Forms.ComboBox()
        Me.DesignPanel.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel_Language.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
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
        Me.TableLayoutPanel1.Controls.Add(Me.lblMessage, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel_Language, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(633, 352)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMessage.Font = New System.Drawing.Font("SimSun", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(66, 13)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(500, 46)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "PLEASE SELECT AN Language:"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'TableLayoutPanel_Language
        '
        Me.TableLayoutPanel_Language.ColumnCount = 4
        Me.TableLayoutPanel_Language.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Language.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel_Language.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        Me.TableLayoutPanel_Language.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel_Language.Controls.Add(Me.btnCancel, 3, 0)
        Me.TableLayoutPanel_Language.Controls.Add(Me.btnOK, 1, 0)
        Me.TableLayoutPanel_Language.Controls.Add(Me.CBLanguage, 0, 0)
        Me.TableLayoutPanel_Language.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Language.Location = New System.Drawing.Point(63, 59)
        Me.TableLayoutPanel_Language.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Language.Name = "TableLayoutPanel_Language"
        Me.TableLayoutPanel_Language.RowCount = 1
        Me.TableLayoutPanel_Language.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Language.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Language.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Language.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Language.Size = New System.Drawing.Size(506, 46)
        Me.TableLayoutPanel_Language.TabIndex = 5
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(437, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(66, 40)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Font = New System.Drawing.Font("Cambria", 16.0!)
        Me.btnOK.Location = New System.Drawing.Point(357, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 40)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'CBLanguage
        '
        Me.CBLanguage.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.CBLanguage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBLanguage.Font = New System.Drawing.Font("Consolas", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLanguage.FormattingEnabled = True
        Me.CBLanguage.Location = New System.Drawing.Point(3, 3)
        Me.CBLanguage.MaxDropDownItems = 20
        Me.CBLanguage.Name = "CBLanguage"
        Me.CBLanguage.Size = New System.Drawing.Size(348, 45)
        Me.CBLanguage.TabIndex = 4
        '
        'LanguageSelectView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 706)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "LanguageSelectView"
        Me.Text = "LanguageView"
        Me.DesignPanel.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel_Language.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblMessage As Label
    Friend WithEvents TableLayoutPanel_Language As TableLayoutPanel
    Public WithEvents btnCancel As Button
    Public WithEvents btnOK As Button
    Public WithEvents CBLanguage As ComboBox
End Class
