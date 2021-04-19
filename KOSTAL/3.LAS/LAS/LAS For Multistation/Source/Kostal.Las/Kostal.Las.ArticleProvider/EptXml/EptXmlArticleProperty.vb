Namespace Ept
    Public Class EptXmlArticleProperty

        Private _name As String
        Private _value As String

        Public Sub New(ByVal name As String, ByVal value As String)
            _name = name
            _value = value
        End Sub

        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

    End Class
End Namespace
