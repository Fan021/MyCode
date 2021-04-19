Imports Kostal.Las.ArticleProvider.Base
Namespace Ept
    ''' <summary>
    ''' Article provider that can be used by Testman to create a <see cref="ArticleCollection"/> out of a csv file.
    ''' The provider must have a strict configuration mapping csv columns to article properties.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EptXmlArticleProvider : Implements IArticlesProvider

        Private _eptXmlOutputFile As String
        Private _xmlDescriptionFile As String
        Private _articleCollection As ArticleCollection


        ''' <summary>
        ''' Creates a new instance of the <see cref="EptXmlArticleProvider"/>
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        Public Property ReadFile() As String Implements IArticlesProvider.ReadFile
            Get
                Return _eptXmlOutputFile
            End Get
            Set(ByVal value As String)
                _eptXmlOutputFile = value
            End Set
        End Property

        Public Property MappingFile() As String Implements IArticlesProvider.MappingFile
            Get
                Return _xmlDescriptionFile
            End Get
            Set(ByVal value As String)
                _xmlDescriptionFile = value
            End Set
        End Property

        Public Overridable Function GetArticles() As ArticleCollection Implements IArticlesProvider.GetArticles

            If _articleCollection Is Nothing Then
                Dim configBuilder As New EptXmlConfigurationBuilder(ReadFile, MappingFile)
                Dim parser As New EptXmlParser(configBuilder.GetConfig)

                _articleCollection = parser.Read()
            End If


            Return _articleCollection

        End Function

    End Class
End Namespace
