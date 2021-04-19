Imports System.Windows
Imports System.Windows.Controls

Namespace Helpers

    Public Class ContextMenuHelper

        Friend Shared Function CreateContextMenu(framewrkElement As FrameworkElement, dataContext As Object, dataContextPath As String, contextMenuItemStyleKey As String, contextMenuItemDataTemplateKey As String) As System.Windows.Controls.ContextMenu
            Dim o As Object = framewrkElement.TryFindResource(contextMenuItemStyleKey)
            Dim st As Style = Nothing
            If o IsNot Nothing Then
                st = TryCast(o, Style)
            End If
            If st Is Nothing Then
                st = New Style()
            End If

            Dim dtmpl As DataTemplate = Nothing
            o = framewrkElement.TryFindResource(contextMenuItemDataTemplateKey)
            If o IsNot Nothing Then
                dtmpl = TryCast(o, DataTemplate)
            End If
            If dtmpl Is Nothing Then
                dtmpl = New DataTemplate()
            End If

            Dim cm As ContextMenu
            cm = New ContextMenu() _
                With {
                    .DataContext = dataContext,
                    .ItemContainerStyle = st,
                    .ItemTemplate = dtmpl
                    }
            cm.SetBinding(ContextMenu.ItemsSourceProperty, New System.Windows.Data.Binding(dataContextPath))
            Return cm
        End Function

    End Class
End NameSpace