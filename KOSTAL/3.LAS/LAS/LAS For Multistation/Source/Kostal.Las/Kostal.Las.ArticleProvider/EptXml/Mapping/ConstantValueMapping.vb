Imports System.Globalization

Namespace Ept.Mapping

    ''' <summary>
    ''' Maps to a constant value that is inserted in constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConstantValueMapping : Inherits Mapping

        Private ReadOnly _value As String
        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="ConstantValueMapping"/>
        ''' </summary>
        ''' <param name="constantString">the constant value that will be replied by this mapping as <see cref="System.String"/></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal constantString As String)
            _value = constantString
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="ConstantValueMapping"/>
        ''' </summary>
        ''' <param name="constant">the constant value that will be replied by this mapping as Integer</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal constant As Integer)
            Me.New(constant.ToString(CultureInfo.InvariantCulture))
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="ConstantValueMapping"/>
        ''' </summary>
        ''' <param name="constant">the constant value that will be replied by this mapping as <see cref="Double"/></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal constant As Double)
            Me.New(constant.ToString(CultureInfo.InvariantCulture))
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="ConstantValueMapping"/>
        ''' </summary>
        ''' <param name="constant">the constant value that will be replied by this mapping as <see cref="Boolean"/></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal constant As Boolean)
            Me.New(constant.ToString(CultureInfo.InvariantCulture))
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="ConstantValueMapping"/>
        ''' </summary>
        ''' <param name="constant">the constant value that will be replied by this mapping as <see cref="DateTime"/></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal constant As DateTime)
            Me.New(constant.ToShortDateString())
        End Sub

        ''' <summary>
        ''' The constant value replied by this mapping
        ''' </summary>
        ''' <returns>the replied value as <see cref="System.String"/></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Value() As String
            Get
                Return _value
            End Get
        End Property

        Friend Overrides Property EmptyEptPropertyOption() As OnEmptyEptPropertyOption
            Get
                Return _emptyEptPropertyOption
            End Get
            Set(ByVal newValue As OnEmptyEptPropertyOption)
                _emptyEptPropertyOption = newValue
            End Set
        End Property

        Friend Overrides Property ShowInArticleSelector() As Boolean
            Get
                Return _showInArticleSelector
            End Get
            Set(ByVal newValue As Boolean)
                _showInArticleSelector = newValue
            End Set
        End Property

        Friend Overrides Function GetValue(ByVal xmlPropertyValues As IEnumerable(Of String)) As MappedValue
            Return New MappedValue(Value)
        End Function

        Friend Overrides Sub SetupPropertyNames(ByVal xmlPropertyNames As IEnumerable(Of String))

        End Sub

    End Class
End Namespace