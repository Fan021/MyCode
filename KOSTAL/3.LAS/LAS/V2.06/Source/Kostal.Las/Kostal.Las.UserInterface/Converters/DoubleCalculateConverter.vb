''' <summary>
''' Takes a double in the value do a numeric add with the parameter that is converted to double
''' </summary>
''' <remarks>The parameter will be converted with InvariantCulture.</remarks>
<System.Windows.Data.ValueConversion(GetType(Double), GetType(Double))>
Public Class DoubleCalculateConverter
    Implements System.Windows.Data.IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim calcValue As Double
        Dim paramValue As Double

        If (value).GetType() IsNot GetType(Double) Then
            Throw New ArgumentException("DoubleCalculateConverter: Value is not of type double.")
        End If
        If targetType IsNot GetType(Double) Then
            Throw New ArgumentException("DoubleCalculateConverter: targetType is not of type double.")
        End If

        calcValue = DirectCast(value, Double)

        If Not System.Double.TryParse(CStr(parameter), System.Globalization.NumberStyles.Float Or System.Globalization.NumberStyles.AllowThousands, System.Globalization.NumberFormatInfo.InvariantInfo, paramValue) Then
            Throw New ArgumentException("DoubleCalculateConverter: Parameter is not a double.")
        End If

        Return calcValue + paramValue
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException
    End Function

End Class