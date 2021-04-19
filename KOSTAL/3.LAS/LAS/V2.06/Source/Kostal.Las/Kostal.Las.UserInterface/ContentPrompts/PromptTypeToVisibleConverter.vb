'Imports Kostal.Testman.Framework.Base.Prompts

Public Class PromptTypeToVisibleConverter
    Implements System.Windows.Data.IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If (value).GetType() IsNot GetType(Prompts.PromptTypes) Then
            Throw New ArgumentException("Value is not of type Prompts.PromptTypes")
        End If
        If (parameter).GetType() IsNot GetType(String) Then
            Throw New ArgumentException("parameter is not of type string")
        End If
        Dim promptType As Prompts.PromptTypes = DirectCast(value, Prompts.PromptTypes)
        Dim param As String = DirectCast(parameter, String)

        Select Case param.ToUpperInvariant()
            Case "ALARM"
                If promptType = Prompts.PromptTypes.Alarm Then Return System.Windows.Visibility.Visible
            Case "PROBLEM"
                If promptType = Prompts.PromptTypes.Problem Then Return System.Windows.Visibility.Visible
            Case "WARNING"
                If promptType = Prompts.PromptTypes.Warning Then Return System.Windows.Visibility.Visible
            Case "INFORMATION"
                If promptType = Prompts.PromptTypes.Information Then Return System.Windows.Visibility.Visible
            Case "QUESTION"
                If promptType = Prompts.PromptTypes.Question Then Return System.Windows.Visibility.Visible
        End Select

        Return System.Windows.Visibility.Collapsed
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
