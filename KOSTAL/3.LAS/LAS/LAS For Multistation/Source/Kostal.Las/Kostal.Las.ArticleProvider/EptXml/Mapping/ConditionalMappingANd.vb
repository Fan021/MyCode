Namespace Ept.Mapping

    ''' <summary>
    ''' Represents a conditional value mapping of type AND.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConditionalMappingAnd
        Inherits Mapping

        Private _info As String
        Private _conditions As IEnumerable(Of Condition)
        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates new instance of <see cref="ConditionalMappingAnd"/>
        ''' </summary>
        ''' <param name="info">Info to be mapped.</param>
        ''' <param name="conditions">Conditions to be AND-Associated.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal info As String, ByVal ParamArray conditions() As Condition)
            Me.Info = info
            Me.Conditions = conditions
        End Sub

        Public Property Info() As String
            Get
                Return _info
            End Get
            Set(ByVal value As String)
                _info = value
            End Set
        End Property

        Public Property Conditions() As IEnumerable(Of Condition)
            Get
                Return _conditions
            End Get
            Set(ByVal value As IEnumerable(Of Condition))
                _conditions = value
            End Set
        End Property

        Friend Overrides Property EmptyEptPropertyOption() As OnEmptyEptPropertyOption
            Get
                Return _emptyEptPropertyOption
            End Get
            Set(ByVal value As OnEmptyEptPropertyOption)
                _emptyEptPropertyOption = value
            End Set
        End Property

        Friend Overrides Property ShowInArticleSelector() As Boolean
            Get
                Return _showInArticleSelector
            End Get
            Set(ByVal value As Boolean)
                _showInArticleSelector = value
            End Set
        End Property

        ''' <summary>
        ''' Returns a value depending on specified conditions.
        ''' </summary>
        ''' <param name="xmlPropertyValues">All values of all single variant properties</param>
        ''' <returns>A property value if all conditions produce a TRUE, otherwise NOTHING.</returns>
        ''' <remarks></remarks>
        Friend Overrides Function GetValue(ByVal xmlPropertyValues As IEnumerable(Of String)) As MappedValue
            Dim isMatch As Boolean = True

            For Each condition As Condition In Conditions
                isMatch = isMatch And condition.IsMatch(xmlPropertyValues.ToArray)
            Next

            If isMatch Then
                Return New MappedValue(Info, True)
            Else
                Return New MappedValue(Nothing, False)
            End If

        End Function

        Friend Overrides Sub SetupPropertyNames(ByVal xmlPropertyNames As IEnumerable(Of String))
            For Each condition As Condition In Conditions
                condition.Mapping.SetupPropertyNames(xmlPropertyNames.ToArray)
            Next
        End Sub

    End Class

End Namespace
