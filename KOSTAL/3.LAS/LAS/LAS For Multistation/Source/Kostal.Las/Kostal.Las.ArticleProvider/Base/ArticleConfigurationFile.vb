Namespace Base
    ''' <summary>
    ''' References a flow file located on local harddrive
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArticleConfigurationFile

        Private _hibernateId As Integer
        Private ReadOnly _parent As ArticleConfigurationSet
        Private ReadOnly _fileName As String

        ''' <summary>
        ''' Initializes a new instance of the ArticleConfigurationFile class.
        ''' </summary>
        ''' <param name="parent">connection to the article the file referres to.</param>
        ''' <param name="fileName">name and path referencing the file.</param>
        Public Sub New(ByVal parent As ArticleConfigurationSet, ByVal fileName As String)
            _parent = parent
            _fileName = fileName
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the ArticleConfigurationFile class.
        ''' The properties Parent and FileName as still null
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Gets the value representing the article this configuration element is attached to.
        ''' </summary>
        ''' <value>Parent article as ArticleConfigurationSet</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Parent() As ArticleConfigurationSet
            Get
                Return _parent
            End Get
        End Property

        ''' <summary>
        ''' Gets the value representing the reference to flow file on local harddisk
        ''' </summary>
        ''' <value></value>
        ''' <returns>Filename and path (relative or absolut) to the flow file represented by this class.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileName() As String
            Get
                Return _fileName
            End Get
        End Property


        Public Overloads Function Equals(ByVal other As ArticleConfigurationFile) As Boolean
            If ReferenceEquals(Nothing, other) Then Return False
            If ReferenceEquals(Me, other) Then Return True
            Return Equals(other._fileName, _fileName)
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If Not Equals(obj.GetType(), GetType(ArticleConfigurationFile)) Then Return False
            Return Equals(DirectCast(obj, ArticleConfigurationFile))
        End Function

        Public Overrides Function GetHashCode() As Integer
            If _fileName IsNot Nothing Then Return _fileName.GetHashCode()
            Return 0
        End Function
    End Class
End Namespace


