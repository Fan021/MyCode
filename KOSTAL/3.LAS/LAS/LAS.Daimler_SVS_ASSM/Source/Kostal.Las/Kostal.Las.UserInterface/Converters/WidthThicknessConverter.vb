<System.Windows.Data.ValueConversion(GetType(Double), GetType(Double))>
<System.Windows.Data.ValueConversion(GetType(System.Windows.Thickness), GetType(Double))>
Public Class WidthThicknessConverter
    Implements System.Windows.Data.IMultiValueConverter

    Public Function Convert(values() As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Try
            If values.Any(Function(x As Object)
                              OutputDebugStringConditionalDebug("{0}", x.ToString())
                              Return x Is System.Windows.DependencyProperty.UnsetValue
                          End Function) Then
                System.Diagnostics.Trace.WriteLine(String.Format("WidthPaddingConverter.Check() values.Any(Function(x As Object) x Is DependencyProperty.UnsetValue"))
                Return True
            End If

            If values.Count() <> 2 Then
                Throw New ArgumentException("Only 2 binding values are supported.")
            End If

            Dim actualWidth As Double
            If (values(0)).GetType() IsNot GetType(Double) Then
                Throw New ArgumentException("First value is not of type Double")
            End If
            actualWidth = DirectCast(values(0), Double)

            Dim padding As System.Windows.Thickness
            If (values(1)).GetType() IsNot GetType(System.Windows.Thickness) Then
                Throw New ArgumentException("Second value is not of type System.Windows.Thickness")
            End If
            padding = DirectCast(values(1), System.Windows.Thickness)

            Dim width As Double = actualWidth - padding.Left - padding.Right
            If width < 0 Then Return 0
            Return width
        Catch ex As Exception
            OutputDebugString("WidthThicknessConverter {0}", ex.ToString())
        End Try
        Return 0
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotImplementedException
    End Function

End Class