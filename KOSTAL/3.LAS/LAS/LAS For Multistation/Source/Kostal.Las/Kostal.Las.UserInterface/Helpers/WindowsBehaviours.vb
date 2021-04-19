Imports System.Windows

Namespace Helpers

    Public Module WindowBehaviours

        Public Sub SetClose(target As DependencyObject, value As Boolean)
            target.SetValue(CloseProperty, value)
        End Sub

        Public ReadOnly CloseProperty As DependencyProperty = DependencyProperty.RegisterAttached("Close", GetType(Boolean), GetType(WindowBehaviours), New UIPropertyMetadata(False, AddressOf OnClose))

        Private Sub OnClose(sender As DependencyObject, e As DependencyPropertyChangedEventArgs)
            If TypeOf e.NewValue Is Boolean AndAlso DirectCast(e.NewValue, Boolean) Then

                Dim window As Window = GetWindow(sender)

                If window IsNot Nothing Then
                    window.Close()
                End If
            End If
        End Sub

        Private Function GetWindow(sender As DependencyObject) As Window

            Dim window As Window = Nothing

            Dim tempWindow As Window = TryCast(sender, Window)
            If tempWindow IsNot Nothing Then
                window = tempWindow
            End If

            If window Is Nothing Then
                window = window.GetWindow(sender)
            End If

            Return window
        End Function

    End Module
End NameSpace