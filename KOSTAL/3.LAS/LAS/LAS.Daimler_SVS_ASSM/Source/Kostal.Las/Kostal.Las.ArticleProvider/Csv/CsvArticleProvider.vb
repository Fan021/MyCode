Imports Kostal.Las.ArticleProvider.Base
Namespace Csv
    ''' <summary>
    ''' Article provider that can be used by Testman to create a <see cref="ArticleCollection"/> out of a csv file.
    ''' The provider must have a strict configuration mapping csv columns to article properties.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvArticleProvider
        Implements IArticlesProvider
        Private _configuration As ReaderConfiguration
        Private _ReadFile As String
        Private _MappingFile As String

        ''' <summary>
        ''' Creates a new instance of the <see cref="CsvArticleProvider"/>
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Gets or sets the <see cref="ReaderConfiguration"/> used by the <see cref="CsvArticleProvider"/>.
        ''' </summary>
        ''' <value>the <see cref="ReaderConfiguration"/> used to load articles from csv file.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Configuration() As ReaderConfiguration
            Get
                Return _configuration
            End Get
            Set(ByVal value As ReaderConfiguration)
                _configuration = value
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
        Protected Overridable Function GetArticles() As ArticleCollection Implements IArticlesProvider.GetArticles

            If Configuration Is Nothing Then
                Throw New ArgumentException(String.Format("Missing configuration for csv article provider."))
            End If

            If Not IO.File.Exists(Configuration.Filename) Then
                Throw New ArgumentException(String.Format("File '{0}' containing article information is not existing.", Configuration.Filename))
            End If

            Dim parser As New CsvParser(Configuration)

            Return parser.Read()

        End Function

    End Class
End Namespace
