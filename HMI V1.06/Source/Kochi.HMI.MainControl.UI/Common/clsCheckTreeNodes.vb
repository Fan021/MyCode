Imports System.Windows.Forms

Public Class clsCheckTreeNodes
    Inherits TreeNode

    Dim m_State As CheckState = Windows.Forms.CheckState.Unchecked

    Property CheckState() As CheckState
        Get
            Return m_State
        End Get
        Set(ByVal value As CheckState)
            m_State = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Dim t As clsCheckTreeNodes = MyBase.Clone()
        t.CheckState = Me.CheckState
        Return t
    End Function
End Class
