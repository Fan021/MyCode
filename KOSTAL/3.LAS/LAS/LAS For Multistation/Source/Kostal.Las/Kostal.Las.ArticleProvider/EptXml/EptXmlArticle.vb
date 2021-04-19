Namespace Ept
    Public Class EptXmlArticle

        Private _id As String
        Private _properties As List(Of EptXmlArticleProperty)

        Public Sub New(ByVal id As String)
            _id = id
            _properties = New List(Of EptXmlArticleProperty)
        End Sub

        Public ReadOnly Property Id As String
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Properties As IList(Of EptXmlArticleProperty)
            Get
                Return _properties.ToArray
            End Get
        End Property

        Public Sub AddProperty(ByVal prop As EptXmlArticleProperty)
            For Each p As EptXmlArticleProperty In _properties
                If p.Name.Equals(prop.Name) Then
                    Exit Sub
                End If
            Next p

            _properties.Add(prop)
        End Sub

        Public ReadOnly Property PropertyNames As IList(Of String)
            Get
                Return (From p In _properties Select p.Name).ToArray
            End Get
        End Property

        Public ReadOnly Property PropertyValues As IList(Of String)
            Get
                Return (From p In _properties Select p.Value).ToArray
            End Get
        End Property

    End Class
End Namespace
