Namespace Csv.Mapping

    ''' <summary>
    ''' Represents a conditional value mapping of type OR.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConditionalMappingOr
        Inherits Mapping

        Private _info As String
        Private _conditions As IEnumerable(Of Condition)
        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
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

        Friend Overrides Property EmptyColumnOption() As OnEmptyColumnOption
            Get
                Return _emptyColumnOption
            End Get
            Set(ByVal value As OnEmptyColumnOption)
                _emptyColumnOption = value
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
        ''' <param name="csvRow">A row of the csv the value has to be mapped from.</param>
        ''' <returns>A row value if all AND-Associated conditions produce a TRUE, otherwise NOTHING.</returns>
        ''' <remarks></remarks>
        Friend Overrides Function GetValue(ByVal csvRow() As String) As MappingValue
            Dim isMatch As Boolean
            For Each condition In Conditions
                If condition.IsMatch(csvRow) Then
                    isMatch = True
                    Exit For
                End If
            Next


            If isMatch Then
                Return New MappingValue(Info, True)
            Else
                Return New MappingValue(Nothing, False)
            End If

        End Function

        Friend Overrides Sub SetupHeaders(ByVal headers() As String)
            For Each condition In Conditions
                condition.Mapping.SetupHeaders(headers)
            Next
        End Sub

    End Class

End Namespace
