Public Class SinglePromptControl
    Dim _vm As SinglePromptModel

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler Me.DataContextChanged, AddressOf View_DataContextChanged
    End Sub

    Private Sub View_DataContextChanged(sender As Object, e As System.Windows.DependencyPropertyChangedEventArgs)
        _vm = TryCast(Me.DataContext, SinglePromptModel)
    End Sub

    Private Sub ComboboxResponse_PreviewKeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles ComboboxResponse.PreviewKeyDown
        CheckForEnterKey(sender, e)
    End Sub

    Private Sub TextboxResponse_PreviewKeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles TextboxResponse.PreviewKeyDown
        CheckForEnterKey(sender, e)
    End Sub

    Private Sub CheckForEnterKey(sender As Object, e As System.Windows.Input.KeyEventArgs)
        If e.Key = System.Windows.Input.Key.Enter Then
            If _vm.Commands.Any() Then
                If _vm.Commands(0).Command IsNot Nothing Then
                    ' Load value from control, because the binding is LostFocus, but when ENTER is pressed the Focus is not changed
                    Dim txtbox As System.Windows.Controls.TextBox = TryCast(sender, System.Windows.Controls.TextBox)
                    If txtbox IsNot Nothing Then
                        _vm.ResponseText = txtbox.Text
                    Else
                        Dim cbobox As System.Windows.Controls.ComboBox = TryCast(sender, System.Windows.Controls.ComboBox)
                        If cbobox IsNot Nothing Then
                            _vm.ResponseText = cbobox.Text
                        End If
                    End If
                End If
                _vm.Commands(0).Command.Execute(Nothing)
            End If
        End If
    End Sub
End Class
