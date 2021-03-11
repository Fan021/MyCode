Imports System.Windows.Forms
Imports System.Collections.Concurrent

Public Interface IChildrenUI
    ReadOnly Property UI As Panel
    Property ButtonName As String
    Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface

Public Class clsChildrenUINameAttribute
    Inherits Attribute
    Private strDevice As Type
    Private strName As String
    Sub New(ByVal strName As String, ByVal strDevice As Type)
        Me.strDevice = strDevice
        Me.strName = strName
    End Sub

    Public ReadOnly Property DeviceType
        Get
            Return strDevice
        End Get
    End Property

    Public ReadOnly Property Name
        Get
            Return strName
        End Get
    End Property

End Class