<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAdsVariable = New System.Windows.Forms.Label()
        Me.picbox = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblAdsVariable, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.picbox, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(200, 53)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'lblAdsVariable
        '
        Me.lblAdsVariable.BackColor = System.Drawing.Color.Transparent
        Me.lblAdsVariable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAdsVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAdsVariable.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdsVariable.ForeColor = System.Drawing.Color.Black
        Me.lblAdsVariable.Location = New System.Drawing.Point(3, 0)
        Me.lblAdsVariable.Name = "lblAdsVariable"
        Me.lblAdsVariable.Size = New System.Drawing.Size(144, 53)
        Me.lblAdsVariable.TabIndex = 0
        Me.lblAdsVariable.Text = "Station"
        Me.lblAdsVariable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picbox
        '
        Me.picbox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.picbox.BackColor = System.Drawing.Color.Transparent
        Me.picbox.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.gray
        Me.picbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picbox.Location = New System.Drawing.Point(153, 3)
        Me.picbox.Name = "picbox"
        Me.picbox.Size = New System.Drawing.Size(44, 47)
        Me.picbox.TabIndex = 1
        Me.picbox.TabStop = False
        '
        'IOControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "IOControl"
        Me.Size = New System.Drawing.Size(200, 53)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblAdsVariable As Label
    Friend WithEvents picbox As PictureBox
End Class
