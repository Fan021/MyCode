''' <summary>
''' Singleton for all Converters 
''' </summary>
Public NotInheritable Class KostalCoreConverter

    ''' <summary>
    ''' Construktor for the Singleton for all Converter
    ''' </summary>
    Shared Sub New()

    End Sub

    ''' <summary>
    ''' Prevents a default instance of the <see cref="KostalCoreConverter"/> class from being created.
    ''' </summary>
    Private Sub New()

    End Sub

    <ThreadStatic>
    Private Shared _boolToVisibilityOrHiddenConverter As Kostal.Windows.Presentation.BoolToValueConverter

    ''' <summary>
    ''' Property for <see cref="BoolToVisibilityOrHiddenConverter"/>
    ''' </summary>
    Public Shared ReadOnly Property BoolToVisibilityOrHiddenConverter As Kostal.Windows.Presentation.BoolToValueConverter
        Get
            If _boolToVisibilityOrHiddenConverter Is Nothing Then
                _boolToVisibilityOrHiddenConverter = New Kostal.Windows.Presentation.BoolToValueConverter() _
                    With {.TrueValue = System.Windows.Visibility.Visible,
                          .FalseValue = System.Windows.Visibility.Hidden}
                _boolToVisibilityOrHiddenConverter.Freeze()
            End If

            Return _boolToVisibilityOrHiddenConverter
        End Get
    End Property

    <ThreadStatic>
    Private Shared _horizontalNoneToCollapsedElseVisibleConverter As EqualsCompareConverter
    ''' <summary>
    ''' Property for <see cref="HorizontalNoneToCollapsedElseVisibleConverter"/>
    ''' </summary>
    Public Shared ReadOnly Property HorizontalNoneToCollapsedElseVisibleConverter As EqualsCompareConverter
        Get
            If _horizontalNoneToCollapsedElseVisibleConverter Is Nothing Then
                _horizontalNoneToCollapsedElseVisibleConverter = New EqualsCompareConverter() _
                    With {.EqualsValue = Kostal.Testman.UserInterface.Design.HorizontalPositions.None,
                          .ResultValue = System.Windows.Visibility.Collapsed,
                          .OtherwiseResultValue = System.Windows.Visibility.Visible}
                _horizontalNoneToCollapsedElseVisibleConverter.Freeze()
            End If

            Return _horizontalNoneToCollapsedElseVisibleConverter
        End Get
    End Property

    <ThreadStatic>
    Private Shared _horizontalRightToRightElseLeftConverter As EqualsCompareConverter
    ''' <summary>
    ''' Property for <see cref="HorizontalRightToRightElseLeftConverter"/>
    ''' </summary>
    Public Shared ReadOnly Property HorizontalRightToRightElseLeftConverter As EqualsCompareConverter
        Get
            If _horizontalRightToRightElseLeftConverter Is Nothing Then
                _horizontalRightToRightElseLeftConverter = New EqualsCompareConverter() _
                    With {.EqualsValue = Kostal.Testman.UserInterface.Design.HorizontalPositions.Right,
                          .ResultValue = System.Windows.Controls.Dock.Right,
                          .OtherwiseResultValue = System.Windows.Controls.Dock.Left}
                _horizontalRightToRightElseLeftConverter.Freeze()
            End If

            Return _horizontalRightToRightElseLeftConverter
        End Get
    End Property

End Class