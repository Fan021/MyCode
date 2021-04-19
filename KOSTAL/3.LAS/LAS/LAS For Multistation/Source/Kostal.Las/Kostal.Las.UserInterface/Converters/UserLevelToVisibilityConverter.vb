<System.Windows.Data.ValueConversion(GetType(Global.Kostal.Testman.Framework.Base.UserLevel), GetType(System.Windows.Visibility))> _
<System.Windows.Data.ValueConversion(GetType(Kostal.Testman.UserInterface.Design.Visibilities), GetType(System.Windows.Visibility))> _
Public Class UserLevelToVisibilityConverter
    Implements System.Windows.Data.IMultiValueConverter

    Public Function Convert(values As Object(), targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Return If(BaseForUserLevelToConverter.Check(values, parameter), System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed)
    End Function

    ''' <summary>
    ''' Not Implemented
    ''' </summary>
    ''' <returns>NotImplementedException</returns>
    Public Function ConvertBack(value As Object, targetTypes As System.Type(), parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New System.NotImplementedException()
    End Function

End Class