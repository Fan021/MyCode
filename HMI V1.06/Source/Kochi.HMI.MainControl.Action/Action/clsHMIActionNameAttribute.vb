Public Class clsHMIActionNameAttribute
    Inherits Attribute
    Private strName As String
    Private eHMIActionType As enumHMIActionType
    Private eHMISubActionType As enumHMISubActionType
    Sub New(ByVal strName As String, ByVal eHMIActionType As enumHMIActionType, ByVal eHMISubActionType As enumHMISubActionType)
        Me.strName = strName
        Me.eHMIActionType = eHMIActionType
        Me.eHMISubActionType = eHMISubActionType
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return strName
        End Get
    End Property
    Public ReadOnly Property HMIActionType As enumHMIActionType
        Get
            Return eHMIActionType
        End Get
    End Property
    Public ReadOnly Property HMISubActionType As enumHMISubActionType
        Get
            Return eHMISubActionType
        End Get
    End Property
End Class

Public Enum enumHMIActionType
    Null
    Manual
    Auto
    ManualAuto
    [Global]
End Enum

Public Enum enumHMISubActionType
    SubAction
    SubSubAction
    All
End Enum