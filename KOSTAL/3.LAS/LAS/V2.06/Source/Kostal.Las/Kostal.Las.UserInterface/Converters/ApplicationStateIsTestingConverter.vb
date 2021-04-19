Namespace Converters

    <System.Windows.Data.ValueConversion(GetType(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates), GetType(Boolean))>
    Public Class ApplicationStateIsTestingConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return System.Windows.DependencyProperty.UnsetValue

            Dim enumValue As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates

            If Not [Enum].TryParse(value.ToString(), enumValue) Then Return System.Windows.DependencyProperty.UnsetValue

            Select Case enumValue
                Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Test
                    'ISystemStateManager.SystemStates.TurningFromRunToTest,
                    'ISystemStateManager.SystemStates.TurningFromTestToRun,
                    'ISystemStateManager.SystemStates.DiffusBetweenRunAndTest
                    Return True
            End Select

            Return False
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotImplementedException
        End Function

    End Class

End Namespace