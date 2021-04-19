Public Class EqualsCompareConverter
    Inherits System.Windows.Freezable
    Implements System.Windows.Data.IValueConverter

    ''' <summary>
    ''' <see cref="System.Windows.DependencyProperty"/> for TrueValue
    ''' </summary>
    Public Shared ReadOnly EqualsValueProperty As System.Windows.DependencyProperty

    Public Shared ReadOnly ResultValueProperty As System.Windows.DependencyProperty
    Public Shared ReadOnly OtherwiseResultValueProperty As System.Windows.DependencyProperty

    Shared Sub New()
        EqualsValueProperty = System.Windows.DependencyProperty.Register("EqualsValue", GetType(Object), GetType(EqualsCompareConverter))
        ResultValueProperty = System.Windows.DependencyProperty.Register("ResultValue", GetType(Object), GetType(EqualsCompareConverter))
        OtherwiseResultValueProperty = System.Windows.DependencyProperty.Register("OtherwiseResultValue", GetType(Object), GetType(EqualsCompareConverter))
    End Sub

    ''' <summary>
    ''' Get or set the value that is used in case of <c>True</c>
    ''' </summary>
    Public Property EqualsValue As Object
        Get
            Return GetValue(EqualsValueProperty)
        End Get
        Set(value As Object)
            SetValue(EqualsValueProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Get or set the value that is used in case of <c>False</c>
    ''' </summary>
    Public Property ResultValue As Object
        Get
            Return GetValue(ResultValueProperty)
        End Get
        Set(value As Object)
            SetValue(ResultValueProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Get or set the value that is used in case of <c>False</c>
    ''' </summary>
    Public Property OtherwiseResultValue As Object
        Get
            Return GetValue(OtherwiseResultValueProperty)
        End Get
        Set(value As Object)
            SetValue(OtherwiseResultValueProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Converts a value
    ''' </summary>
    ''' <returns></returns>
    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim invert As Boolean = False

        invert = Kostal.Windows.Presentation.Converter.Helper.ToBool(parameter)

        If value.Equals(EqualsValue) Then
            Return IIf(Not invert, ResultValue, OtherwiseResultValue)
        End If
        Return IIf(invert, ResultValue, OtherwiseResultValue)
    End Function

    ''' <summary>
    ''' Converts backs
    ''' </summary>
    ''' <returns></returns>
    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New System.NotImplementedException()
    End Function

    ''' <summary>
    ''' When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable"/> derived class. 
    ''' </summary>
    ''' <returns>
    ''' The new instance.
    ''' </returns>
    Protected Overrides Function CreateInstanceCore() As System.Windows.Freezable
        Return New EqualsCompareConverter()
    End Function

End Class