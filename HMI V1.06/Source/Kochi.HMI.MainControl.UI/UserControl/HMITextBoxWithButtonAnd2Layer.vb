Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Concurrent
Public Class HMITextBoxWithButtonAnd2Layer
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg
    Private ChanList As ToolStripMenuItem
    Private PopMenu As New ContextMenuStrip
    Private eInsertType As enumInsertType
    Private lListLine As New List(Of Integer)
    Private lListNameWitheTextLine As New List(Of Integer)
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

    Public Function RegisterButton(ByVal lListKey As Dictionary(Of String, Dictionary(Of String, String)), ByVal lListLine As List(Of Integer), ByVal lListNameWitheTextLine As List(Of Integer), Optional ByVal eInsertType As enumInsertType = enumInsertType.Replace, Optional ByVal bReadOnly As Boolean = False) As Boolean
        Me.lListLine = lListLine
        Me.lListNameWitheTextLine = lListNameWitheTextLine
        Me.eInsertType = eInsertType
        Dim mTempValue As String = ""
        PopMenu.Items.Clear()
        Dim iCnt As Integer = 1
        For Each key As String In lListKey.Keys
            ChanList = New ToolStripMenuItem(key)
            For Each SecondKey As String In lListKey(key).Keys
                Dim SecondChanList = New ToolStripMenuItem(SecondKey)
                If lListKey(key)(SecondKey).Length > 30 Then
                    mTempValue = lListKey(key)(SecondKey).Substring(0, 30) + "...."
                Else
                    mTempValue = lListKey(key)(SecondKey)
                End If
                Dim cToolStripMenuItem As New ToolStripMenuItem
                cToolStripMenuItem.Text = mTempValue
                cToolStripMenuItem.Tag = iCnt
                If lListNameWitheTextLine.Contains(iCnt) Then
                    cToolStripMenuItem.Name = mTempValue
                Else
                    cToolStripMenuItem.Name = key + "_" + SecondKey
                End If
                AddHandler cToolStripMenuItem.Click, AddressOf OnVarClick
                SecondChanList.DropDownItems.Add(cToolStripMenuItem)
                ChanList.DropDownItems.Add(SecondChanList)
            Next
            iCnt = iCnt + 1
            PopMenu.Items.Add(ChanList)
        Next
        If bReadOnly Then
            TextBoxValue.BackColor = Drawing.Color.WhiteSmoke
            TextBoxValue.ReadOnly = True
        End If
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
                Dim iLine As Integer = Integer.Parse(CType(sender, ToolStripMenuItem).Tag)
                Dim idx As Integer = TextBoxValue.SelectionStart

                Dim s As String = ""
                If lListLine.Contains(iLine) Then
                    s = CType(sender, ToolStripMenuItem).Name
                Else
                    s = "[" + CType(sender, ToolStripMenuItem).Name + "]"
                End If

                TextBoxValue.Text = TextBoxValue.Text.Insert(idx, s)
                TextBoxValue.SelectionStart = idx + s.Length
                TextBoxValue.Focus()
            Case enumInsertType.Replace
                Dim iLine As Integer = Integer.Parse(CType(sender, ToolStripMenuItem).Tag)
                Dim s As String = ""
                If lListLine.Contains(iLine) Then
                    s = CType(sender, ToolStripMenuItem).Name
                Else
                    s = "[" + CType(sender, ToolStripMenuItem).Name + "]"
                End If
                TextBoxValue.Text = s
        End Select
    End Sub

    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            TextBoxValue.Width = Panel_Body.Width - 18 - 6
            TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
            Button_List.Width = 18       
            Button_List.Height = TextBoxValue.Height
            Button_List.Location = New System.Drawing.Point(2 + TextBoxValue.Width, (Panel_Body.Height - TextBoxValue.Height) / 2)
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    'Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxValue.SizeChanged
    '    cSizeChangeSizeCfg.newH = Me.Panel_Body.Width
    '    If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
    '        TextBoxValue.Width = Panel_Body.Width - 18 - 6
    '        TextBoxValue.Location = New System.Drawing.Point(3, (Panel_Body.Height - TextBoxValue.Height) / 2)
    '        Button_List.Width = 18
    '        Button_List.Height = TextBoxValue.Height
    '        Button_List.Location = New System.Drawing.Point(2 + TextBoxValue.Width, (Panel_Body.Height - Button_List.Height) / 2)
    '    End If
    '    cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    'End Sub
End Class
