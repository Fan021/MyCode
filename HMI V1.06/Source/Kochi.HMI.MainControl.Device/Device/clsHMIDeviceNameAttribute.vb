Public Class clsHMIDeviceNameAttribute
    Inherits Attribute
    Private strName As String
    Private strType As String
    Sub New(ByVal strName As String, ByVal strType As String)
        Me.strName = strName
        Me.strType = strType
    End Sub

    Public ReadOnly Property Name
        Get
            Return strName
        End Get
    End Property

    Public ReadOnly Property Type
        Get
            Return strType
        End Get
    End Property

End Class
