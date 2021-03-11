<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HMIDateTime
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HMIDateTime))
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.Panel_Date = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Date = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Picture = New System.Windows.Forms.Button()
        Me.TextBoxValue = New System.Windows.Forms.TextBox()
        Me.Panel_Body.SuspendLayout()
        Me.Panel_Date.SuspendLayout()
        Me.TableLayoutPanel_Date.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.Panel_Date)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.TabIndex = 1
        '
        'Panel_Date
        '
        Me.Panel_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Date.Controls.Add(Me.TableLayoutPanel_Date)
        Me.Panel_Date.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Panel_Date.Location = New System.Drawing.Point(3, 3)
        Me.Panel_Date.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Date.Name = "Panel_Date"
        Me.Panel_Date.Size = New System.Drawing.Size(100, 27)
        Me.Panel_Date.TabIndex = 0
        '
        'TableLayoutPanel_Date
        '
        Me.TableLayoutPanel_Date.ColumnCount = 2
        Me.TableLayoutPanel_Date.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Date.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Date.Controls.Add(Me.Button_Picture, 1, 0)
        Me.TableLayoutPanel_Date.Controls.Add(Me.TextBoxValue, 0, 0)
        Me.TableLayoutPanel_Date.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Date.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Date.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Date.Name = "TableLayoutPanel_Date"
        Me.TableLayoutPanel_Date.RowCount = 1
        Me.TableLayoutPanel_Date.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Date.Size = New System.Drawing.Size(98, 25)
        Me.TableLayoutPanel_Date.TabIndex = 0
        '
        'Button_Picture
        '
        Me.Button_Picture.BackgroundImage = CType(resources.GetObject("Button_Picture.BackgroundImage"), System.Drawing.Image)
        Me.Button_Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Picture.FlatAppearance.BorderSize = 0
        Me.Button_Picture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Picture.Location = New System.Drawing.Point(78, 0)
        Me.Button_Picture.Margin = New System.Windows.Forms.Padding(0)
        Me.Button_Picture.Name = "Button_Picture"
        Me.Button_Picture.Size = New System.Drawing.Size(20, 25)
        Me.Button_Picture.TabIndex = 2
        Me.Button_Picture.UseVisualStyleBackColor = True
        '
        'TextBoxValue
        '
        Me.TextBoxValue.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxValue.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.TextBoxValue.Location = New System.Drawing.Point(0, 5)
        Me.TextBoxValue.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxValue.Name = "TextBoxValue"
        Me.TextBoxValue.ReadOnly = True
        Me.TextBoxValue.Size = New System.Drawing.Size(78, 14)
        Me.TextBoxValue.TabIndex = 1
        '
        'HMIDateTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel_Body)
        Me.Name = "HMIDateTime"
        Me.Size = New System.Drawing.Size(106, 33)
        Me.Panel_Body.ResumeLayout(False)
        Me.Panel_Date.ResumeLayout(False)
        Me.TableLayoutPanel_Date.ResumeLayout(False)
        Me.TableLayoutPanel_Date.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents Panel_Date As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Date As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBoxValue As System.Windows.Forms.TextBox
    Friend WithEvents Button_Picture As System.Windows.Forms.Button

End Class
