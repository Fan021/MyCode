Imports System.Collections.Concurrent

Public Class clsParameter
    Private Shared _Object As New Object
    Public Shared Shadows Function ToString(ByVal lParameter As List(Of String)) As String
        SyncLock _Object
            Dim mTemp As String = String.Empty
            Dim bFirst As Boolean = False
            For Each element As String In lParameter
                If Not bFirst Then
                    mTemp = element
                    bFirst = True
                Else
                    mTemp = mTemp + "|" + element
                End If

            Next
            Return mTemp
        End SyncLock
    End Function

    Public Shared Function ToList(ByVal strParameter As String) As List(Of String)
        SyncLock _Object
            Dim mTemp As New List(Of String)
            If strParameter = "" Then Return mTemp
            For Each element As String In strParameter.Split("|")
                mTemp.Add(element)
            Next
            Return mTemp
        End SyncLock
    End Function
End Class
