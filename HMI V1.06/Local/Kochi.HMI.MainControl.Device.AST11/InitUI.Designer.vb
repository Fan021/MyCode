﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_ASTIP = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PLCAds = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ASTIP = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_PLCAds = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 246)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ASTIP, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_PLCAds, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ASTIP, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PLCAds, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_ASTIP
        '
        Me.HmiTextBox_ASTIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ASTIP.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_ASTIP.Name = "HmiTextBox_ASTIP"
        Me.HmiTextBox_ASTIP.Number = 0
        Me.HmiTextBox_ASTIP.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_ASTIP.TabIndex = 0
        Me.HmiTextBox_ASTIP.TextBoxReadOnly = False
        Me.HmiTextBox_ASTIP.ValueType = GetType(String)
        '
        'HmiTextBox_PLCAds
        '
        Me.HmiTextBox_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PLCAds.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_PLCAds.Name = "HmiTextBox_PLCAds"
        Me.HmiTextBox_PLCAds.Number = 0
        Me.HmiTextBox_PLCAds.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_PLCAds.TabIndex = 1
        Me.HmiTextBox_PLCAds.TextBoxReadOnly = False
        Me.HmiTextBox_PLCAds.ValueType = GetType(String)
        '
        'HmiLabel_ASTIP
        '
        Me.HmiLabel_ASTIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ASTIP.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_ASTIP.Name = "HmiLabel_ASTIP"
        Me.HmiLabel_ASTIP.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_ASTIP.TabIndex = 3
        '
        'HmiLabel_PLCAds
        '
        Me.HmiLabel_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PLCAds.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_PLCAds.Name = "HmiLabel_PLCAds"
        Me.HmiLabel_PLCAds.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_PLCAds.TabIndex = 4
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 246)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InitUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents HmiTextBox_ASTIP As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PLCAds As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ASTIP As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_PLCAds As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
End Class
