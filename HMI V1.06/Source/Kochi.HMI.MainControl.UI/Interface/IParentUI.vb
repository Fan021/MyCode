Imports System.Windows.Forms
Imports System.Collections.Concurrent

Public Interface IParentUI
    ReadOnly Property UI As Panel
    Property ButtonName As String
    ReadOnly Property LocalElement As Dictionary(Of String, Object)
    Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface


