﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiTextBox_Port = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Port = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_IP = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_IP = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.FolderBrowserDialog_Path = New System.Windows.Forms.FolderBrowserDialog()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Port, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Port, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_IP, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_IP, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_Port
        '
        Me.HmiTextBox_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Port.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_Port.Name = "HmiTextBox_Port"
        Me.HmiTextBox_Port.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Port.TabIndex = 6
        Me.HmiTextBox_Port.TextBoxReadOnly = False
        '
        'HmiLabel_Port
        '
        Me.HmiLabel_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Port.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Port.Name = "HmiLabel_Port"
        Me.HmiLabel_Port.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Port.TabIndex = 5
        '
        'HmiTextBox_IP
        '
        Me.HmiTextBox_IP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_IP.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_IP.Name = "HmiTextBox_IP"
        Me.HmiTextBox_IP.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_IP.TabIndex = 1
        Me.HmiTextBox_IP.TextBoxReadOnly = False
        '
        'HmiLabel_IP
        '
        Me.HmiLabel_IP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_IP.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_IP.Name = "HmiLabel_IP"
        Me.HmiLabel_IP.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_IP.TabIndex = 4
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
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_IP As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_IP As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Port As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Port As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents FolderBrowserDialog_Path As System.Windows.Forms.FolderBrowserDialog
End Class
