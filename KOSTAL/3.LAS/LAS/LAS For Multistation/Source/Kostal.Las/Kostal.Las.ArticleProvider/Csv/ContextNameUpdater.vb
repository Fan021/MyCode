Namespace Csv
    Public Class ContextNameValidator

        Private Shared regex_ContextName As New Text.RegularExpressions.Regex("^[()0-9A-Za-z_-]+$")


        Public Shared Function ValidateAndUpdateContextName(ByVal name As String) As String

            If regex_ContextName.IsMatch(name) Then Return name

            name = name.Replace(".", "_")
            name = name.Replace(" ", "_")
            name = name.Replace(",", "_")
            name = name.Replace(";", "_")

            name = name.Replace("ö", "oe")
            name = name.Replace("ä", "ae")
            name = name.Replace("ü", "ue")

            name = name.Replace("Ö", "Oe")
            name = name.Replace("Ä", "Ae")
            name = name.Replace("Ü", "Ue")

            If regex_ContextName.IsMatch(name) Then Return name

            Throw New ArgumentException(String.Format("The name '{0}' is not a valid context key.", name))

        End Function


    End Class
End Namespace
