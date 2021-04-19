<System.Windows.Data.ValueConversion(GetType(Global.Kostal.Testman.Framework.Base.UserLevel), GetType(Boolean))> _
<System.Windows.Data.ValueConversion(GetType(Kostal.Testman.UserInterface.Design.Visibilities), GetType(Boolean))> _
Public Class UserLevelToBooleanConverter
    Implements System.Windows.Data.IMultiValueConverter

    Public Function Convert(values As Object(), targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Return BaseForUserLevelToConverter.Check(values, parameter)
    End Function

    ''' <summary>
    ''' Not Implemented
    ''' </summary>
    ''' <returns>NotImplementedException</returns>
    Public Function ConvertBack(value As Object, targetTypes As System.Type(), parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New System.NotImplementedException()
    End Function

End Class