Public Class clsHMIActionNameAttribute
    Inherits Attribute
    Private strName As String
    Sub New(ByVal strName As String)
        Me.strName = strName
    End Sub

    Public ReadOnly Property Name
        Get
            Return strName
        End Get
    End Property
End Class
