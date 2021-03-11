Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Concurrent

Public Class HMITextBoxWithButton
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg
    Private ChanList As ToolStripMenuItem
    Private PopMenu As New ContextMenuStrip
    Private lListKey As Dictionary(Of String, String)
    Private eInsertType As enumInsertType

    Public ReadOnly Property TextBox As TextBox
        Get
            Return TextBoxValue
        End Get
    End Property

    Public Overrides Property Text As String
        Set(ByVal value As String)
            TextBoxValue.Text = value
        End Set
        Get
            Return TextBoxValue.Text
        End Get
    End Property

    Public Function RegisterButton(ByVal lListKey As Dictionary(Of String, String), Optional ByVal eInsertType As enumInsertType = enumInsertType.Replace) As Boolean
        Me.eInsertType = eInsertType
        Dim mTempValue As String = ""
        PopMenu.Items.Clear()
        For Each key As String In lListKey.Keys
            ChanList = New ToolStripMenuItem(key)
            If lListKey(key).Length > 30 Then
                mTempValue = lListKey(key).Substring(0, 30) + "...."
            Else
                mTempValue = lListKey(key)
            End If
            Dim cToolStripMenuItem As New ToolStripMenuItem
            cToolStripMenuItem.Text = mTempValue
            cToolStripMenuItem.Name = key
            AddHandler cToolStripMenuItem.Click, AddressOf OnVarClick
            ChanList.DropDownItems.Add(cToolStripMenuItem)
            PopMenu.Items.Add(ChanList)
        Next

        Return True
    End Function
    Private Sub HMITextBoxWithButton_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBoxValue.Name = Name
    End Sub

    Private Sub Button_List_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_List.Click
        Dim rc As Rectangle = New Rectangle(Me.Button_List.Location, Me.Button_List.Size)
        PopMenu.Show(Me, New Point(rc.Left, rc.Bottom), ToolStripDropDownDirection.Default)
    End Sub

    Private Sub OnVarClick(ByVal sender As System.Object, ByVal e As EventArgs)
        Select Case eInsertType
            Case enumInsertType.Insert
                Dim idx As Integer = TextBoxValue.SelectionStart
                Dim s As String = "[" + CType(sender, ToolStripMenuItem).Name + "]"
                TextBoxValue.Text = TextBoxValue.Text.Insert(idx, s)
                TextBoxValue.SelectionStart = idx + s.Length
                TextBoxValue.Focus()
            Case enumInsertType.Replace
                TextBoxValue.Text = "[" + CType(sender, ToolStripMenuItem).Name + "]"
        End Select
    End Sub

    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            TextBoxValue.Width = Panel_Body.Width - 18 - 6
            TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
            Button_List.Width = 18
            Button_List.Height = TextBoxValue.Height
            Button_List.Location = New System.Drawing.Point(2 + TextBoxValue.Width, (Panel_Body.Height - Button_List.Height) / 2)
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxValue.SizeChanged
        cSizeChangeSizeCfg.newH = Me.Panel_Body.Width
        If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
            TextBoxValue.Width = Panel_Body.Width - 18 - 6
            TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
            Button_List.Width = 18
            Button_List.Height = TextBoxValue.Height
            Button_List.Location = New System.Drawing.Point(2 + TextBoxValue.Width, (Panel_Body.Height - Button_List.Height) / 2)
        End If
        cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    End Sub

End Class

Public Enum enumInsertType
    Insert = 0
    Replace
End Enum
