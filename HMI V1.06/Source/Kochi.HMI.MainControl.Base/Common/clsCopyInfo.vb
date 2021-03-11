Imports System
Imports System.Collections.Concurrent

<Serializable()> _
Public Class clsCopyInfo
    Inherits clsXmlStr
    Dim m_Data As New Dictionary(Of String, Object)

    ReadOnly Property Data() As Dictionary(Of String, Object)
        Get
            Return m_Data
        End Get
    End Property

    Public Overrides Function IsStep() As Boolean
        If Me.Type = ListType.MainStep Or _
            Me.Type = ListType.SubStep Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

<Serializable()> _
Public Class clsXmlStr
    Dim m_Type As ListType
    Dim m_StepData As Object
    Dim m_ViewData As Object
    Shared lListObject As New Dictionary(Of String, Object)
    Property Type() As ListType
        Get
            Return m_Type
        End Get
        Set(ByVal value As ListType)
            m_Type = value
        End Set
    End Property

    Property StepData() As Object
        Get
            Return m_StepData
        End Get
        Set(ByVal value As Object)
            m_StepData = value
        End Set
    End Property

    Property ViewData() As Object
        Get
            Return m_ViewData
        End Get
        Set(ByVal value As Object)
            m_ViewData = value
        End Set
    End Property

    Public Overridable Function IsStep() As Boolean
        Return False
    End Function

    Public Shared Function CopyToClipboard(ByRef info As clsCopyInfo) As Boolean
        If lListObject.ContainsKey("HMICopyInfo") Then
            lListObject("HMICopyInfo") = info
        Else
            lListObject.Add("HMICopyInfo", info)
        End If
        Return True
    End Function

    Public Shared Function GetClipboardContent() As clsCopyInfo

        Try
            If lListObject.ContainsKey("HMICopyInfo") Then
                Return lListObject("HMICopyInfo")
            Else
                Return Nothing
            End If

        Catch ex As Exception
        End Try
        Return Nothing
    End Function
End Class

Public Enum ListType As Byte
    MainStep
    SubStep
    SubSubStep
    PreSub
    SubPass
    SubFail
End Enum
