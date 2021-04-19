Public Class ApplicationButtonsControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        AddHandler Me.DataContextChanged, AddressOf Me_DataContextChanged
    End Sub

    Private Sub Me_DataContextChanged(sender As Object, e As System.Windows.DependencyPropertyChangedEventArgs)
        'Dim modelStation As StationModel = TryCast(Me.DataContext, StationModel)
        'If modelStation IsNot Nothing Then
        '    'Me.StationStateCntrol.DataContext = modelStation.StationStateModel
        'End If
    End Sub

End Class
