Public Class clsSystemPath
    Private Shared _Object As New Object
    Public Shared Shadows Function ToSystemPath(ByVal strPath As String) As String
        SyncLock _Object
            Dim mTemp As String = String.Empty
            If strPath = "" Then
                Return ""
            End If
            If strPath.IndexOf("[") >= 0 And strPath.IndexOf("]") >= 0 And strPath.IndexOf(":") < 0 Then
                Return strPath
            End If
            If strPath.IndexOf(":") >= 0 Then
                Return strPath
            End If
            strPath = My.Application.Info.DirectoryPath + strPath
            Return strPath
        End SyncLock
    End Function
    Public Shared Shadows Function ToIniPath(ByVal strPath As String) As String
        SyncLock _Object
            Dim mTemp As String = String.Empty
            If strPath = "" Then
                Return ""
            End If
            If strPath.IndexOf("[") >= 0 And strPath.IndexOf("]") >= 0 And strPath.IndexOf(":") < 0 Then
                Return strPath
            End If
            If strPath.IndexOf(":") >= 0 Then
                If strPath.IndexOf(My.Application.Info.DirectoryPath) >= 0 Then
                    strPath = strPath.Replace(My.Application.Info.DirectoryPath, "")
                End If
            End If
            Return strPath
        End SyncLock
    End Function
End Class
