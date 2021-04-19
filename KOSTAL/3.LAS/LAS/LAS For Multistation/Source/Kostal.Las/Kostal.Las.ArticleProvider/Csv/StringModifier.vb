Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv
    ''' <summary>
    ''' Contains routines to modify a <see cref="System.String"/>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StringModifier

        ''' <summary>
        ''' Mofidies a <see cref="System.String"/> based on the strategy provided by <see cref="StringModification"/>
        ''' </summary>
        ''' <param name="value">the <see cref="System.String"/> to be modified</param>
        ''' <param name="strategy">the strategy applied to the <see cref="System.String"/></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Modify(ByVal value As String, ByVal strategy As StringModification) As String

            Select Case strategy
                Case StringModification.Unmodified
                    Return value
                Case StringModification.Trim
                    Return value.Trim()
                Case StringModification.RemoveWhitespaces
                    Return value.Replace(" "c, "")
            End Select

            Throw New ArgumentException("The selected strategy could not be applied.")

        End Function

    End Class
End Namespace