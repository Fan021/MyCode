Imports Kostal.Las.ArticleProvider.Base
Namespace Csv
    Public Class Csv
        Implements IArticlesProvider

        Private _ReadFile As String
        Private _csvDirectory As String
        Private _MappingFile As String
        Private _articleCollection As ArticleCollection


        ''' <summary>
        ''' Creates a new instance of the <see cref="CsvArticleProvider"/>
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub


        Public Property CsvDirectory() As String
            Get
                Return _csvDirectory
            End Get
            Set(ByVal value As String)
                _csvDirectory = value
            End Set
        End Property

        Public Property ReadFile() As String Implements IArticlesProvider.ReadFile
            Get
                Return _ReadFile
            End Get
            Set(ByVal value As String)
                _ReadFile = value
            End Set
        End Property

        Public Property MappingFile() As String Implements IArticlesProvider.MappingFile
            Get
                Return _MappingFile
            End Get
            Set(ByVal value As String)
                _MappingFile = value
            End Set
        End Property

        Public Function GetArticles() As ArticleCollection Implements IArticlesProvider.GetArticles
            If _articleCollection Is Nothing Then
                Dim configBuilder As New XmlConfigurationBuilder(ReadFile, MappingFile)
                Dim parser As New CsvParser(configBuilder.GetConfig())

                _articleCollection = parser.Read()
            End If

            Return _articleCollection

        End Function
    End Class
End Namespace