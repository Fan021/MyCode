Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Drawing

Public Class TreeViewWidthCheckBox
    Inherits TreeView
    Dim m_DragTargetNode As clsCheckTreeNodes = Nothing
    Dim cnodeFont As Font
    Dim focusBounds As Rectangle
    Dim cParantNode As clsCheckTreeNodes = Nothing
    Dim cSelectNode As clsCheckTreeNodes = Nothing
    Dim cChildrenNode As Integer = 0
    Dim _Object As New Object
    Public bDisable As Boolean = False
    Public ReadOnly Property GetSelectIndex As Integer
        Get
            SyncLock _Object
                Return Me.SelectedNode.Index
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetSelectParentIndex As Integer
        Get
            SyncLock _Object

                Return Me.SelectedNode.Parent.Index
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetSelectParentParentIndex As Integer
        Get
            SyncLock _Object
                Return Me.SelectedNode.Parent.Parent.Index
            End SyncLock
        End Get

    End Property

    Public ReadOnly Property GetVirtualSelectIndex As Integer
        Get
            SyncLock _Object
                Dim iCnt As Integer = 0
                If Me.SelectedNode.Level = 1 Then
                    iCnt = Me.SelectedNode.Index + 1
                    Return iCnt - 1
                End If

                iCnt = 0
                If Me.SelectedNode.Level = 2 Then
                    'Action1 
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Nodes
                        If element.Index > SelectedNode.Index Then Continue For
                        'SuAction1 
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            'PreSubAction
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                If element.Index = SelectedNode.Index Then
                                    If subelement.Index = 0 Then
                                        iCnt = iCnt + 1
                                    End If
                                Else
                                    iCnt = iCnt + 1
                                End If
                            Next
                        Next
                        iCnt = iCnt + 1
                    Next
                    Return iCnt - 1
                End If

                iCnt = 0
                If Me.SelectedNode.Level = 3 Then
                    Return SelectedNode.Index
                End If

                iCnt = 0
                If Me.SelectedNode.Level = 4 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Parent.Parent.Nodes
                        If element.Index > SelectedNode.Parent.Parent.Index Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                If subsubelement.Parent.Index > SelectedNode.Parent.Index And subsubelement.Parent.Parent.Index = SelectedNode.Parent.Parent.Index Then Continue For
                                If subsubelement.Index > SelectedNode.Index And subsubelement.Parent.Index = SelectedNode.Parent.Index And subsubelement.Parent.Parent.Index = SelectedNode.Parent.Parent.Index Then Continue For
                                iCnt = iCnt + 1
                            Next
                        Next
                        '添加SubAction计数
                        If SelectedNode.Parent.Index = 0 Then
                            If element.Index < SelectedNode.Parent.Parent.Index Then iCnt = iCnt + 1
                        Else
                            iCnt = iCnt + 1
                        End If

                    Next
                    Return iCnt - 1
                End If
                Return SelectedNode.Index
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property GetVirtualInsertIndex As Integer
        Get
            SyncLock _Object
                Dim iCnt As Integer = 0
                iCnt = 0
                '获取所有
                If Me.SelectedNode.Level = 1 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Nodes
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                iCnt = iCnt + 1
                            Next
                        Next
                        iCnt = iCnt + 1
                    Next
                    Return iCnt - 1
                End If

                '获取当前截止
                iCnt = 0
                If Me.SelectedNode.Level = 2 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Nodes
                        '获取当前截止
                        If element.Index > SelectedNode.Index Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                iCnt = iCnt + 1
                            Next
                        Next
                        iCnt = iCnt + 1
                    Next
                    Return iCnt - 1
                End If

                iCnt = 0
                If Me.SelectedNode.Level = 3 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Parent.Nodes
                        '获取当前截止
                        If element.Index > SelectedNode.Parent.Index Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes

                            If subelement.Index > SelectedNode.Index And subelement.Parent.Index = SelectedNode.Parent.Index Then Continue For
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                iCnt = iCnt + 1
                            Next
                        Next
                        '若添加SubAction计数
                        If SelectedNode.Index = 0 Then
                            If element.Index <> SelectedNode.Parent.Index Then iCnt = iCnt + 1
                        Else
                            '若不为Presuaction添加SubAction计数
                            iCnt = iCnt + 1
                        End If

                    Next
                    Return iCnt - 1
                End If

                iCnt = 0
                If Me.SelectedNode.Level = 4 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Parent.Parent.Nodes
                        '获取当前截止
                        If element.Index > SelectedNode.Parent.Parent.Index Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                If subsubelement.Parent.Index > SelectedNode.Parent.Index And subsubelement.Parent.Parent.Index = SelectedNode.Parent.Parent.Index Then Continue For
                                If subsubelement.Index > SelectedNode.Index And subsubelement.Parent.Index = SelectedNode.Parent.Index And subsubelement.Parent.Parent.Index = SelectedNode.Parent.Parent.Index Then Continue For
                                iCnt = iCnt + 1
                            Next

                        Next
                        '添加SubAction计数
                        If SelectedNode.Parent.Index = 0 Then
                            If element.Index <> SelectedNode.Parent.Parent.Index Then iCnt = iCnt + 1
                        Else
                            iCnt = iCnt + 1
                        End If

                    Next
                    Return iCnt - 1
                End If

                Return SelectedNode.Index - 1
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualUpSelectIndex As Integer
        Get
            SyncLock _Object
                Dim iCnt As Integer = 0
                iCnt = 0
                If Me.SelectedNode.Level = 2 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Nodes
                        If element.Index <> SelectedNode.Index - 1 Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                iCnt = iCnt + 1
                            Next
                        Next
                        iCnt = iCnt + 1
                    Next
                    Return iCnt
                End If
                Return SelectedNode.Index
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property GetVirtualDownSelectIndex As Integer
        Get
            SyncLock _Object
                Dim iCnt As Integer = 0
                iCnt = 0
                If Me.SelectedNode.Level = 2 Then
                    For Each element As clsCheckTreeNodes In Me.SelectedNode.Parent.Nodes
                        If element.Index <> SelectedNode.Index + 1 Then Continue For
                        For Each subelement As clsCheckTreeNodes In element.Nodes
                            For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                                iCnt = iCnt + 1
                            Next
                        Next
                        iCnt = iCnt + 1
                    Next
                    Return iCnt
                End If
                Return SelectedNode.Index
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualSelectParentIndex As Integer
        Get
            SyncLock _Object
                If Me.SelectedNode.Level = 1 Then
                    Return Me.SelectedNode.Index
                End If
                If Me.SelectedNode.Level = 2 Then
                    Return Me.SelectedNode.Parent.Index
                End If
                If Me.SelectedNode.Level = 3 Then
                    Return Me.SelectedNode.Parent.Parent.Index
                End If
                If Me.SelectedNode.Level = 4 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Index
                End If
                Return Me.SelectedNode.Parent.Index
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualSelectParentParentIndex As Integer
        Get
            SyncLock _Object
                If Me.SelectedNode.Level = 1 Then
                    Return Me.SelectedNode.Parent.Index
                End If
                If Me.SelectedNode.Level = 2 Then
                    Return Me.SelectedNode.Parent.Parent.Index
                End If
                If Me.SelectedNode.Level = 3 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Index
                End If
                If Me.SelectedNode.Level = 4 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Parent.Index
                End If
                Return Me.SelectedNode.Parent.Parent.Index
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualSelectParentParentText As String
        Get
            SyncLock _Object
                If Me.SelectedNode.Level = 1 Then
                    Return Me.SelectedNode.Parent.Text
                End If
                If Me.SelectedNode.Level = 2 Then
                    Return Me.SelectedNode.Parent.Parent.Text
                End If
                If Me.SelectedNode.Level = 3 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Text
                End If
                If Me.SelectedNode.Level = 4 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Parent.Text
                End If
                Return Me.SelectedNode.Parent.Parent.Text
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualSelectParentText As String
        Get
            SyncLock _Object
                If Me.SelectedNode.Level = 1 Then
                    Return Me.SelectedNode.Text
                End If
                If Me.SelectedNode.Level = 2 Then
                    Return Me.SelectedNode.Parent.Text
                End If
                If Me.SelectedNode.Level = 3 Then
                    Return Me.SelectedNode.Parent.Parent.Text
                End If
                If Me.SelectedNode.Level = 4 Then
                    Return Me.SelectedNode.Parent.Parent.Parent.Text
                End If
                Return Me.SelectedNode.Parent.Text
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetVirtualSelectText As String
        Get
            SyncLock _Object
                If Me.SelectedNode.Level = 1 Then
                    Return Me.SelectedNode.Text
                End If
                If Me.SelectedNode.Level = 2 Then
                    Return Me.SelectedNode.Text
                End If
                If Me.SelectedNode.Level = 3 Then
                    Return Me.SelectedNode.Text
                End If
                If Me.SelectedNode.Level = 4 Then
                    Return Me.SelectedNode.Text
                End If
                Return Me.SelectedNode.Text
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetSelectText As String
        Get
            SyncLock _Object
                Return Me.SelectedNode.Text
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetSelectParentText As String
        Get
            SyncLock _Object
                Return Me.SelectedNode.Parent.Text
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property GetSelectParentParentText As String
        Get
            SyncLock _Object
                Return Me.SelectedNode.Parent.Parent.Text
            End SyncLock
        End Get

    End Property
    Public Function AddRootNode(ByVal strName As String) As Boolean
        Try
            SyncLock _Object
                Me.Nodes.Add(strName)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddFirstNode(ByVal iRootIndex As Integer, ByVal strName As String) As Boolean
        Try
            SyncLock _Object
                Me.Nodes(iRootIndex).Nodes.Add(strName)
                Me.Nodes(iRootIndex).Expand()
            End SyncLock
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddFirstNode(ByVal strName As String) As Boolean
        Try
            SyncLock _Object
                Dim t As New clsCheckTreeNodes
                t.CheckState = CheckState.Checked
                t.Name = strName
                t.Text = strName
                Me.SelectedNode.Nodes.Add(t)
                Me.SelectedNode.Expand()

                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function AddSecondNode(ByVal strName As String, ByVal iInsertIndex As Integer, ByVal strSubActionType As String, ByVal bCheck As Boolean, ByVal bCreate As Boolean) As Boolean
        Try
            SyncLock _Object
                If bCreate Then

                    Dim t As New clsCheckTreeNodes
                    t.CheckState = CheckState.Checked
                    t.Name = strName
                    t.Text = strName

                    Dim s As New clsCheckTreeNodes
                    s.Name = "PreSubAction"
                    s.Text = "PreSubAction"
                    t.Nodes.Add(s)
                    s = New clsCheckTreeNodes
                    s.Name = "SubActionPass"
                    s.Text = "SubActionPass"
                    t.Nodes.Add(s)
                    s = New clsCheckTreeNodes
                    s.Name = "SubActionFailure"
                    s.Text = "SubActionFailure"
                    t.Nodes.Add(s)
                    If Me.SelectedNode.Level = 2 Or Me.SelectedNode.Level = 4 Then
                        Me.SelectedNode.Parent.Nodes.Insert(iInsertIndex + 1, t)
                    Else
                        Me.SelectedNode.Nodes.Insert(iInsertIndex + 1, t)
                        Me.SelectedNode.Expand()
                    End If

                Else
                    Dim t As New clsCheckTreeNodes
                    t.CheckState = IIf(bCheck, CheckState.Checked, CheckState.Unchecked)
                    t.Name = strName
                    t.Text = strName
                    If Me.SelectedNode.Level = 2 Or Me.SelectedNode.Level = 4 Then
                        Me.SelectedNode.Parent.Nodes.Insert(iInsertIndex + 1, t)
                    Else
                        Me.SelectedNode.Nodes.Insert(iInsertIndex + 1, t)
                        Me.SelectedNode.Expand()
                    End If
                    '  Me.Nodes(Me.SelectedNode.Parent.Parent.Index).Nodes(Me.SelectedNode.Parent.Index).ExpandAll()

                End If
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function


    Public Function UpSelectedNode() As Boolean
        Try
            SyncLock _Object
                Dim SelectActionNode As TreeNode = Me.SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                cParantNode = Me.SelectedNode.Parent
                cChildrenNode = Me.SelectedNode.Index
                ' If nSelIndex <= 0 Then Return True
                Me.SelectedNode.Parent.Nodes.Insert(nSelIndex - 1, Me.SelectedNode.Clone())
                Me.SelectedNode.Parent.Nodes.RemoveAt(nSelIndex + 1)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function DownSelectedNode() As Boolean
        Try
            SyncLock _Object
                Dim SelectActionNode As TreeNode = Me.SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                cParantNode = Me.SelectedNode.Parent
                cChildrenNode = Me.SelectedNode.Index
                'If nSelIndex <= 0 Then Return True
                Me.SelectedNode.Parent.Nodes.Insert(nSelIndex + 2, Me.SelectedNode.Clone())
                Me.SelectedNode.Parent.Nodes.RemoveAt(nSelIndex)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function PastSelectedNode(ByVal oViewDate As clsCheckTreeNodes) As Boolean
        Try
            SyncLock _Object
                If SelectedNode.Level = 2 Then
                    Me.SelectedNode.Parent.Nodes.Insert(Me.SelectedNode.Index + 1, oViewDate)
                ElseIf SelectedNode.Level = 4 Then
                    Me.SelectedNode.Parent.Nodes.Insert(Me.SelectedNode.Index + 1, oViewDate)
                Else
                    Me.SelectedNode.Nodes.Add(oViewDate)
                    Me.SelectedNode.Expand()
                End If
                cParantNode = Me.SelectedNode.Parent
                cChildrenNode = Me.SelectedNode.Index
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function PastPreSelectedNode(ByVal oViewDate As clsCheckTreeNodes) As Boolean
        Try
            SyncLock _Object
                For Each c As clsCheckTreeNodes In oViewDate.Nodes
                    Me.SelectedNode.Nodes.Add(c)
                Next
                cParantNode = Me.SelectedNode.Parent
                cChildrenNode = Me.SelectedNode.Index
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function RemoveRootNode() As Boolean
        Try
            SyncLock _Object
                Me.Nodes.RemoveAt(Me.SelectedNode.Index)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function RemoveSelectNode() As Boolean
        Try
            SyncLock _Object
                cParantNode = Me.SelectedNode.Parent
                cChildrenNode = Me.SelectedNode.Index
                Me.SelectedNode.Remove()
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function ChooseSelectedNode() As Boolean
        Try
            SyncLock _Object
                If cParantNode.Nodes.Count > 0 Then
                    If cChildrenNode <= cParantNode.Nodes.Count - 1 Then
                        Me.SelectedNode = cParantNode.Nodes(cChildrenNode)
                    Else
                        Me.SelectedNode = cParantNode.Nodes(cChildrenNode - 1)
                    End If
                Else
                    Me.SelectedNode = cParantNode
                End If
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function ChooseSelectedUpNode() As Boolean
        Try
            SyncLock _Object
                Me.SelectedNode = cParantNode.Nodes(cChildrenNode - 1)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function ChooseSelectedDownNode() As Boolean
        Try
            SyncLock _Object
                Me.SelectedNode = cParantNode.Nodes(cChildrenNode + 1)
                Return True
            End SyncLock
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Function ChooseSelectedCurrentNode() As Boolean
        Try
            SyncLock _Object
                If IsNothing(cParantNode) Then
                    Me.SelectedNode = Me.Nodes(cChildrenNode)
                Else
                    Me.SelectedNode = cParantNode.Nodes(cChildrenNode)
                End If
                Return True
            End SyncLock
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

        Dim s As CheckBoxState = CheckBoxState.CheckedDisabled
        Dim b As New SolidBrush(Color.Black)
        If e.Node.GetType.Name = "clsCheckTreeNodes" Then
            Select Case CType(e.Node, clsCheckTreeNodes).CheckState
                Case CheckState.Checked
                    s = CheckBoxState.CheckedNormal
                Case CheckState.Indeterminate
                    s = CheckBoxState.MixedNormal
                Case CheckState.Unchecked
                    s = CheckBoxState.UncheckedNormal
            End Select

        Else
            If e.Node.Checked Then
                s = CheckBoxState.CheckedNormal
            Else
                s = CheckBoxState.UncheckedNormal
            End If
        End If

        Dim size As Size

        If e.Node.Level = 0 Or e.Node.Level = 3 Then
            size = CheckBoxRenderer.GetGlyphSize(e.Graphics, CheckBoxState.CheckedDisabled)
            CheckBoxRenderer.DrawCheckBox(e.Graphics, New Point(e.Node.Bounds.Left - size.Width - 1, e.Bounds.Bottom - size.Height * 1.8 + 2), CheckBoxState.CheckedDisabled)

        Else
            size = CheckBoxRenderer.GetGlyphSize(e.Graphics, s)
            CheckBoxRenderer.DrawCheckBox(e.Graphics, New Point(e.Node.Bounds.Left - size.Width - size.Width / 10, e.Bounds.Bottom - size.Height * 1.8 + 2), s)
        End If

        If e.Node Is Me.m_DragTargetNode Then
            e.Graphics.FillRectangle(Brushes.Beige, e.Bounds)
        End If
        'e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
        'e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
        'e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel
        If e.Bounds.X > 0 Then e.Graphics.DrawString(e.Node.Text, Me.Font, b, e.Bounds.X, e.Bounds.Y)
        b.Dispose()

        e.DrawDefault = True
        Return

        If ((e.State And TreeNodeStates.Selected) <> 0) Then
            e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds)
            cnodeFont = e.Node.NodeFont
            If IsNothing(cnodeFont) Then
                cnodeFont = CType(Me, TreeView).Font
            End If

            e.Graphics.DrawString(e.Node.Text, cnodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0))
        Else
            e.DrawDefault = False
        End If

        If ((e.State And TreeNodeStates.Focused) <> 0) Then
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

    Public Function Clone() As TreeViewWidthCheckBox
        Dim t As New TreeViewWidthCheckBox
        For Each elementTreeNode1 As clsCheckTreeNodes In Me.Nodes
            Dim TreeNode1 As New clsCheckTreeNodes
            TreeNode1.Name = elementTreeNode1.Name
            TreeNode1.Text = elementTreeNode1.Text
            t.Nodes.Add(TreeNode1)
            For Each elementTreeNode2 As clsCheckTreeNodes In elementTreeNode1.Nodes
                Dim TreeNode2 As New clsCheckTreeNodes
                TreeNode2.Name = elementTreeNode2.Name
                TreeNode2.Text = elementTreeNode2.Text
                TreeNode1.Nodes.Add(TreeNode2)
                For Each elementTreeNode3 As clsCheckTreeNodes In elementTreeNode2.Nodes
                    Dim TreeNode3 As New clsCheckTreeNodes
                    TreeNode3.Name = elementTreeNode3.Name
                    TreeNode3.Text = elementTreeNode3.Text
                    TreeNode2.Nodes.Add(TreeNode3)
                    For Each elementTreeNode4 As clsCheckTreeNodes In elementTreeNode3.Nodes
                        Dim TreeNode4 As New clsCheckTreeNodes
                        TreeNode4 = elementTreeNode4.Clone
                        TreeNode3.Nodes.Add(TreeNode4)
                        If elementTreeNode4.IsSelected Then
                            t.SelectedNode = TreeNode4
                        End If
                    Next
                Next
            Next
        Next
        Return t
    End Function
End Class

