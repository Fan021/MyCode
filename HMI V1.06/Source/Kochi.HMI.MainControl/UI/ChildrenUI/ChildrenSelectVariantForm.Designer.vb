Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenSelectVariantForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component List.
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Mid = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Confirm = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.Panel_ComBox = New System.Windows.Forms.Panel()
        Me.lstVariant = New System.Windows.Forms.ListBox()
        Me.ComboBox_Variant = New System.Windows.Forms.ComboBox()
        Me.HmiLabel_Title = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.Panel_ComBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Mid, 1, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(461, 524)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Panel_Mid
        '
        Me.Panel_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.Panel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Mid.Location = New System.Drawing.Point(92, 10)
        Me.Panel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Mid.Name = "Panel_Mid"
        Me.Panel_Mid.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel_Mid.Size = New System.Drawing.Size(276, 504)
        Me.Panel_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 2
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiButton_Cancel, 0, 2)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiButton_Confirm, 1, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.Panel_ComBox, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiLabel_Title, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 4
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(272, 500)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(193, 93)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(76, 39)
        Me.HmiButton_Cancel.TabIndex = 3
        '
        'HmiButton_Confirm
        '
        Me.HmiButton_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Confirm.Location = New System.Drawing.Point(193, 48)
        Me.HmiButton_Confirm.MarginHeight = 6
        Me.HmiButton_Confirm.Name = "HmiButton_Confirm"
        Me.HmiButton_Confirm.Size = New System.Drawing.Size(76, 39)
        Me.HmiButton_Confirm.TabIndex = 0
        '
        'Panel_ComBox
        '
        Me.Panel_ComBox.Controls.Add(Me.lstVariant)
        Me.Panel_ComBox.Controls.Add(Me.ComboBox_Variant)
        Me.Panel_ComBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_ComBox.Location = New System.Drawing.Point(0, 45)
        Me.Panel_ComBox.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_ComBox.Name = "Panel_ComBox"
        Me.TableLayoutPanel_Body_Mid.SetRowSpan(Me.Panel_ComBox, 3)
        Me.Panel_ComBox.Size = New System.Drawing.Size(190, 455)
        Me.Panel_ComBox.TabIndex = 1
        '
        'lstVariant
        '
        Me.lstVariant.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.lstVariant.FormattingEnabled = True
        Me.lstVariant.ItemHeight = 29
        Me.lstVariant.Location = New System.Drawing.Point(3, 45)
        Me.lstVariant.Name = "lstVariant"
        Me.lstVariant.Size = New System.Drawing.Size(178, 410)
        Me.lstVariant.TabIndex = 67
        Me.lstVariant.Visible = False
        '
        'ComboBox_Variant
        '
        Me.ComboBox_Variant.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.ComboBox_Variant.FormattingEnabled = True
        Me.ComboBox_Variant.Location = New System.Drawing.Point(3, 6)
        Me.ComboBox_Variant.Name = "ComboBox_Variant"
        Me.ComboBox_Variant.Size = New System.Drawing.Size(178, 37)
        Me.ComboBox_Variant.TabIndex = 0
        '
        'HmiLabel_Title
        '
        Me.HmiLabel_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Title.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Title.Name = "HmiLabel_Title"
        Me.HmiLabel_Title.Size = New System.Drawing.Size(184, 39)
        Me.HmiLabel_Title.TabIndex = 2
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'ChildrenSelectVariantForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenSelectVariantForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectVariantForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.Panel_ComBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Mid As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Mid As HMITableLayoutPanel
    Friend WithEvents HmiButton_Confirm As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents Panel_ComBox As System.Windows.Forms.Panel
    Friend WithEvents ComboBox_Variant As System.Windows.Forms.ComboBox
    Public WithEvents lstVariant As System.Windows.Forms.ListBox
    Friend WithEvents HmiLabel_Title As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
