Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Drawing

Public Class TreeViewWidthKeyDown
    Inherits TreeView
    Dim m_DragTargetNode As clsCheckTreeNodes = Nothing
    Dim cnodeFont As Font
    Dim focusBounds As Rectangle
    Public ReadOnly Property GetSelectIndex As Integer
        Get
            Return Me.SelectedNode.Index
        End Get
    End Property

    Public ReadOnly Property GetSelectParentIndex As Integer
        Get
            Return Me.SelectedNode.Parent.Index
        End Get
    End Property

    Public ReadOnly Property GetSelectParentParentIndex As Integer
        Get
            Return Me.SelectedNode.Parent.Parent.Index
        End Get

    End Property

    Public ReadOnly Property GetSelectText As String
        Get
            Return Me.SelectedNode.Text
        End Get
    End Property

    Public ReadOnly Property GetSelectParentText As String
        Get
            Return Me.SelectedNode.Parent.Text
        End Get
    End Property

    Public ReadOnly Property GetSelectParentParentText As String
        Get
            Return Me.SelectedNode.Parent.Parent.Text
        End Get

    End Property
    Public Function AddRootNode(ByVal strName As String) As Boolean
        Try
            Me.Nodes.Add(strName)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddFirstNode(ByVal iRootIndex As Integer, ByVal strName As String) As Boolean
        Try
            Me.Nodes(iRootIndex).Nodes.Add(strName)
            Me.Nodes(iRootIndex).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddFirstNode(ByVal strName As String) As Boolean
        Try
            Me.Nodes(Me.SelectedNode.Index).Nodes.Add(strName)
            Me.Nodes(Me.SelectedNode.Index).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddSecondNode(ByVal iRootIndex As Integer, ByVal iFirstIndex As Integer, ByVal strName As String) As Boolean
        Try
            Me.Nodes(iRootIndex).Nodes(iFirstIndex).Nodes.Add(strName)
            Me.Nodes(iRootIndex).Nodes(iFirstIndex).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function


    Public Function AddSecondNode(ByVal strName As String) As Boolean
        Try
            Me.Nodes(Me.SelectedNode.Parent.Index).Nodes(Me.SelectedNode.Index).Nodes.Add(strName)
            Me.Nodes(Me.SelectedNode.Parent.Index).Nodes(Me.SelectedNode.Index).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddSecondNode(ByVal iInsertIndex As Integer, ByVal strName As String) As Boolean
        Try
            Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Index).Nodes.Insert(iInsertIndex + 1, strName)
            Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Index).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddThirdNode(ByVal iRootIndex As Integer, ByVal iFirstIndex As Integer, ByVal iSecondIndex As Integer, ByVal strName As String) As Boolean
        Try
            Me.Nodes(iRootIndex).Nodes(iFirstIndex).Nodes(iSecondIndex).Nodes.Add(strName)
            Me.Nodes(iRootIndex).Nodes(iFirstIndex).Nodes(iSecondIndex).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddThirdNode(ByVal strName As String) As Boolean
        Try
            Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Index).Nodes.Add(strName)
            Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Index).ExpandAll()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function


    Public Function UpFirstNode() As Boolean
        Try
            Dim SelectActionNode As TreeNode = Me.SelectedNode
            Dim nSelIndex As Int32 = SelectActionNode.Index
            Dim nSelParentIndex As Integer = SelectActionNode.Parent.Index
            If nSelIndex <= 0 Then Return True
            Me.Nodes(nSelParentIndex).Nodes.Insert(nSelIndex - 1, Me.Nodes(nSelParentIndex).Nodes(nSelIndex).Clone())
            Me.Nodes(nSelParentIndex).Nodes.RemoveAt(nSelIndex + 1)
            ' Me.Nodes(nSelParentIndex).Nodes(nSelIndex - 1).Expand()
            Me.SelectedNode = Me.Nodes(nSelParentIndex).Nodes(nSelIndex - 1)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function UpSecondNode() As Boolean
        Try
            Dim SelectActionNode As TreeNode = Me.SelectedNode
            Dim nSelIndex As Int32 = SelectActionNode.Index
            Dim nSelParentIndex As Integer = SelectActionNode.Parent.Index
            Dim nSelParentParentIndex As Integer = SelectActionNode.Parent.Parent.Index
            If nSelIndex <= 0 Then Return True
            Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes.Insert(nSelIndex - 1, Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes(nSelIndex).Clone())
            Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes.RemoveAt(nSelIndex + 1)
            '   Me.SelectedNode = Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes(nSelIndex - 1)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function DownFirstNode() As Boolean
        Try
            Dim SelectActionNode As TreeNode = Me.SelectedNode
            Dim nSelIndex As Int32 = SelectActionNode.Index
            Dim nSelParentIndex As Integer = SelectActionNode.Parent.Index
            Me.Nodes(nSelParentIndex).Nodes.Insert(nSelIndex + 2, Me.Nodes(nSelParentIndex).Nodes(nSelIndex).Clone())
            '  Me.Nodes(nSelParentIndex).Nodes(nSelIndex + 2).Expand()
            Me.Nodes(nSelParentIndex).Nodes.RemoveAt(nSelIndex)
            Me.Nodes(nSelParentIndex).Nodes(nSelIndex + 1).Expand()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function DownSecondNode() As Boolean
        Try
            Dim SelectActionNode As TreeNode = Me.SelectedNode
            Dim nSelIndex As Int32 = SelectActionNode.Index
            Dim nSelParentIndex As Integer = SelectActionNode.Parent.Index
            Dim nSelParentParentIndex As Integer = SelectActionNode.Parent.Parent.Index
            Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes.Insert(nSelIndex + 2, Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes(nSelIndex).Clone())
            Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes.RemoveAt(nSelIndex)
            '   Me.SelectedNode = Me.Nodes(nSelParentParentIndex).Nodes(nSelParentIndex).Nodes(nSelIndex + 1)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function RemoveRootNode() As Boolean
        Try
            Me.Nodes.RemoveAt(Me.SelectedNode.Index)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function RemoveFirstNode() As Boolean
        Try

            Me.Nodes(Me.SelectedNode.Parent.Index).Nodes.RemoveAt(Me.SelectedNode.Index)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function RemoveSecondNode() As Boolean
        Try
            Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Index).Nodes.RemoveAt(Me.SelectedNode.Index)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
        If msg.Msg = &H100 Then
            If msg.WParam = 46 Then  'disable the delete key
                Return False
            End If
        End If

        Return MyBase.PreProcessMessage(msg)
    End Function


    Protected Overrides Sub OnDrawNode(ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs)
        e.DrawDefault = True
        Return
        If (e.State And TreeNodeStates.Selected) <> 0 Then
            e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds)
            cnodeFont = e.Node.NodeFont
            If IsNothing(cnodeFont) Then
                cnodeFont = CType(Me, TreeView).Font
            End If

            e.Graphics.DrawString(e.Node.Text, cnodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0))
        Else
            e.DrawDefault = False
        End If

        If (e.State And TreeNodeStates.Focused) <> 0 Then
            Using focusPen As New Pen(Color.Black)
                focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
                focusBounds = e.Node.Bounds
                focusBounds.Size = New Size(focusBounds.Width - 1, focusBounds.Height - 1)
                e.Graphics.DrawRectangle(focusPen, focusBounds)
            End Using
        End If

        MyBase.OnDrawNode(e)
    End Sub

    Private Sub TreeViewWidthKeyDown_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.AfterExpand
        Me.Refresh()
    End Sub



    Private Sub TreeViewWidthKeyDown_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
    End Sub

    Private Sub TreeViewWidthKeyDown_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles Me.ItemDrag
    End Sub
End Class
