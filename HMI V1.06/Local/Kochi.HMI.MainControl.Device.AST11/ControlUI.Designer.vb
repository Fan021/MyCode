<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlUI
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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Boby_Left = New System.Windows.Forms.Panel()
        Me.Panel_Boby_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.MainRightButton_Web = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.MainRightButton_Data = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.MainRightButton_Back = New Kochi.HMI.MainControl.UI.MainRightButton()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Boby_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(623, 530)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Boby_Left, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Boby_Right, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Panel_Boby_Left
        '
        Me.Panel_Boby_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Boby_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Boby_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Boby_Left.Name = "Panel_Boby_Left"
        Me.Panel_Boby_Left.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Boby_Left.TabIndex = 0
        '
        'Panel_Boby_Right
        '
        Me.Panel_Boby_Right.Controls.Add(Me.TableLayoutPanel_Body_Top_Right)
        Me.Panel_Boby_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Boby_Right.Location = New System.Drawing.Point(498, 0)
        Me.Panel_Boby_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Boby_Right.Name = "Panel_Boby_Right"
        Me.Panel_Boby_Right.Size = New System.Drawing.Size(125, 530)
        Me.Panel_Boby_Right.TabIndex = 1
        '
        'TableLayoutPanel_Body_Top_Right
        '
        Me.TableLayoutPanel_Body_Top_Right.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Body_Top_Right.ColumnCount = 1
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Back, 0, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Web, 0, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.MainRightButton_Data, 0, 2)
        Me.TableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top_Right.Name = "TableLayoutPanel_Body_Top_Right"
        Me.TableLayoutPanel_Body_Top_Right.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.35849!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.54717!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.54717!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.54717!))
        Me.TableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(125, 530)
        Me.TableLayoutPanel_Body_Top_Right.TabIndex = 3
        '
        'MainRightButton_Web
        '
        Me.MainRightButton_Web.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Web.Location = New System.Drawing.Point(3, 412)
        Me.MainRightButton_Web.Name = "MainRightButton_Web"
        Me.MainRightButton_Web.Size = New System.Drawing.Size(119, 33)
        Me.MainRightButton_Web.TabIndex = 2
        '
        'MainRightButton_Data
        '
        Me.MainRightButton_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Data.Location = New System.Drawing.Point(3, 451)
        Me.MainRightButton_Data.Name = "MainRightButton_Data"
        Me.MainRightButton_Data.Size = New System.Drawing.Size(119, 33)
        Me.MainRightButton_Data.TabIndex = 3
        '
        'MainRightButton_Back
        '
        Me.MainRightButton_Back.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainRightButton_Back.Location = New System.Drawing.Point(3, 490)
        Me.MainRightButton_Back.Name = "MainRightButton_Back"
        Me.MainRightButton_Back.Size = New System.Drawing.Size(119, 37)
        Me.MainRightButton_Back.TabIndex = 4
        '
        'ControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ControlUI"
        Me.Text = "ControlUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Boby_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Boby_Left As System.Windows.Forms.Panel
    Friend WithEvents Panel_Boby_Right As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MainRightButton_Web As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents MainRightButton_Data As Kochi.HMI.MainControl.UI.MainRightButton
    Friend WithEvents MainRightButton_Back As Kochi.HMI.MainControl.UI.MainRightButton
End Class
