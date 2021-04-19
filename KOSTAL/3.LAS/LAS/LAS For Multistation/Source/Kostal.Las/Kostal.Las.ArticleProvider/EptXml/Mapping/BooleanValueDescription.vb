Namespace Ept.Mapping

    ''' <summary>
    ''' Class to describe the encoding of a boolean value.
    ''' Includes the character string representing value including allowed interpretations (e.g ignore case)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BooleanValueDescription

        Private ReadOnly _characters As String
        Private _strategy As StringModification
        Private _comparisonOption As StringComparison
        Private _acceptsEmptyString As Boolean

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String)
            Me.New(characters, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <param name="strategy">the strategy applied to the read value before it is checked against the character sequence</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String, ByVal strategy As StringModification)
            Me.New(characters, strategy, StringComparison.InvariantCulture)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <param name="comparisonOption">the options used when comparing the string from csv with the characters sequence</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String, ByVal comparisonOption As StringComparison)
            Me.New(characters, StringModification.Unmodified, comparisonOption)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <param name="strategy">the strategy applied to the read value before it is checked against the character sequence</param>
        ''' <param name="comparisonOption">the options used when comparing the string from csv with the characters sequence</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String, ByVal strategy As StringModification, ByVal comparisonOption As StringComparison)
            Me.New(characters, strategy, comparisonOption, False)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <param name="acceptsEmptyString">if true this identifier is accepting a empty or null value as a valid match, false is not accepting empty or null strings</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String, ByVal acceptsEmptyString As Boolean)
            Me.New(characters)
            _acceptsEmptyString = acceptsEmptyString
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="BooleanValueDescription"/>
        ''' </summary>
        ''' <param name="characters">the sequence of characters representing a boolean value</param>
        ''' <param name="strategy">the strategy applied to the read value before it is checked against the character sequence</param>
        ''' <param name="comparisonOption">the options used when comparing the string from csv with the characters sequence</param>
        ''' <param name="acceptsEmptyString">if true this identifier is accepting a empty or null value as a valid match, false is not accepting empty or null strings</param>
        ''' <remarks>this can be values like True,false,+,-,yes,no etc...</remarks>
        Public Sub New(ByVal characters As String, ByVal strategy As StringModification, ByVal comparisonOption As StringComparison, ByVal acceptsEmptyString As Boolean)
            _characters = characters
            _strategy = strategy
            _comparisonOption = comparisonOption
            _acceptsEmptyString = acceptsEmptyString
        End Sub

        ''' <summary>
        ''' The character sequence representing the boolean value
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Characters() As String
            Get
                Return _characters
            End Get
        End Property

        ''' <summary>
        ''' The strategy to apply to a value before it is compared to character sequence
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Strategy() As StringModification
            Get
                Return _strategy
            End Get
            Set(ByVal value As StringModification)
                _strategy = value
            End Set
        End Property

        ''' <summary>
        ''' The options used to compare the value to the character sequence
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ComparisonOption() As StringComparison
            Get
                Return _comparisonOption
            End Get
            Set(ByVal value As StringComparison)
                _comparisonOption = value
            End Set
        End Property


        ''' <summary>
        ''' If true a null value or empty string will be a match for this value indicator. If false the value will not be accepted.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AcceptsEmptyString() As Boolean
            Get
                Return _acceptsEmptyString
            End Get
            Set(ByVal value As Boolean)
                _acceptsEmptyString = value
            End Set
        End Property
    End Class
End Namespace