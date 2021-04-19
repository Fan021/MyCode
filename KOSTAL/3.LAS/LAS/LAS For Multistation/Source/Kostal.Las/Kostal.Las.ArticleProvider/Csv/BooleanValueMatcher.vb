Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv
    Public Class BooleanValueMatcher

        ''' <summary>
        ''' Checks weather a string is matching a booleanValueDescription or not
        ''' </summary>
        ''' <param name="value">the string to be checked</param>
        ''' <param name="valueDescription">the description to check against</param>
        ''' <returns>true if value matches the description, false in case of no match</returns>
        ''' <remarks></remarks>
        Public Shared Function IsMatch(ByVal value As String, ByVal valueDescription As BooleanValueDescription) As Boolean

            If String.IsNullOrEmpty(value) AndAlso valueDescription.AcceptsEmptyString Then
                Return True
            End If

            Dim modifiedString = StringModifier.Modify(value, valueDescription.Strategy)

            Return modifiedString.Equals(valueDescription.Characters, valueDescription.ComparisonOption)


        End Function

        ''' <summary>
        ''' Checks wether a value is matching either the true or false description.
        ''' </summary>
        ''' <param name="value">value to be checked</param>
        ''' <param name="trueValueDescription">true description to check against</param>
        ''' <param name="falseValueDescription">false description to check against</param>
        ''' <returns>true in case of match with true description, false in case of match with false description</returns>
        ''' <exception cref="ArgumentException">If value matches non of the provided descriptions.</exception>
        ''' <remarks></remarks>
        Public Shared Function GetValue(ByVal value As String, ByVal trueValueDescription As BooleanValueDescription, ByVal falseValueDescription As BooleanValueDescription) As Boolean

            If IsMatch(value, trueValueDescription) Then Return True
            If IsMatch(value, falseValueDescription) Then Return False

            Throw New ArgumentException(String.Format("The value '{0}' couldn't be parsed to boolean value.", value))

        End Function


    End Class
End Namespace
