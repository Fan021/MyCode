<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Debug
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
    'Required by the Windows Form Designer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DesignPanel = New System.Windows.Forms.Panel()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.picArticle = New System.Windows.Forms.PictureBox()
        Me.TabControl_Debug = New System.Windows.Forms.TabControl()
        Me.KeyPanel = New System.Windows.Forms.Panel()
        Me.DesignPanel.SuspendLayout()
        Me.grpPicture.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KeyPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'DesignPanel
        '
        Me.DesignPanel.Controls.Add(Me.grpPicture)
        Me.DesignPanel.Location = New System.Drawing.Point(2, 12)
        Me.DesignPanel.Name = "DesignPanel"
        Me.DesignPanel.Size = New System.Drawing.Size(629, 553)
        Me.DesignPanel.TabIndex = 0
        '
        'grpPicture
        '
        Me.grpPicture.BackColor = System.Drawing.Color.Silver
        Me.grpPicture.Controls.Add(Me.TableLayoutPanel1)
        Me.grpPicture.Controls.Add(Me.picArticle)
        Me.grpPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPicture.Location = New System.Drawing.Point(0, 0)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Padding = New System.Windows.Forms.Padding(2)
        Me.grpPicture.Size = New System.Drawing.Size(629, 553)
        Me.grpPicture.TabIndex = 24
        Me.grpPicture.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.KeyPanel, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.853712!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.32881!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.817481!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(625, 535)
        Me.TableLayoutPanel1.TabIndex = 31
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.lblMessage)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(34, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(556, 41)
        Me.Panel1.TabIndex = 6
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancel.Location = New System.Drawing.Point(406, -10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(150, 51)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.BackColor = System.Drawing.Color.Silver
        Me.lblMessage.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(3, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(397, 41)
        Me.lblMessage.TabIndex = 5
        Me.lblMessage.Text = "Name"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picArticle
        '
        Me.picArticle.BackColor = System.Drawing.Color.White
        Me.picArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picArticle.Location = New System.Drawing.Point(2, 16)
        Me.picArticle.Name = "picArticle"
        Me.picArticle.Size = New System.Drawing.Size(625, 535)
        Me.picArticle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picArticle.TabIndex = 20
        Me.picArticle.TabStop = False
        '
        'TabControl_Debug
        '
        Me.TabControl_Debug.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Debug.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_Debug.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Debug.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Debug.Name = "TabControl_Debug"
        Me.TabControl_Debug.Padding = New System.Drawing.Point(0, 0)
        Me.TabControl_Debug.SelectedIndex = 0
        Me.TabControl_Debug.Size = New System.Drawing.Size(556, 445)
        Me.TabControl_Debug.TabIndex = 31
        Me.TabControl_Debug.Visible = False
        '
        'KeyPanel
        '
        Me.KeyPanel.Controls.Add(Me.TabControl_Debug)
        Me.KeyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KeyPanel.Location = New System.Drawing.Point(34, 50)
        Me.KeyPanel.Name = "KeyPanel"
        Me.KeyPanel.Size = New System.Drawing.Size(556, 445)
        Me.KeyPanel.TabIndex = 1
        '
        'Debug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 653)
        Me.Controls.Add(Me.DesignPanel)
        Me.Name = "Debug"
        Me.Text = "Form1"
        Me.DesignPanel.ResumeLayout(False)
        Me.grpPicture.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KeyPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DesignPanel As Panel
    Public WithEvents grpPicture As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Public WithEvents picArticle As PictureBox
    Friend WithEvents Panel1 As Panel
    Public WithEvents btnCancel As Button
    Friend WithEvents lblMessage As Label
    Friend WithEvents KeyPanel As Panel
    Public WithEvents TabControl_Debug As TabControl
End Class
