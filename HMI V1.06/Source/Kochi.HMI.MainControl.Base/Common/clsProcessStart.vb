Imports System.IO
Public Class clsProcessStart
    Private process As New Process
    Public Function Start(ByVal strWorkingDirectory As String, ByVal strFileName As String, Optional ByVal bShow As Boolean = True) As Boolean
        If File.Exists(strWorkingDirectory + "\" + strFileName) Then
            process = New Process()
            process.StartInfo.WorkingDirectory = strWorkingDirectory
            process.StartInfo.FileName = strWorkingDirectory + "\" + strFileName
            If Not bShow Then
                process.StartInfo.UseShellExecute = False
                process.StartInfo.RedirectStandardError = True
                process.StartInfo.RedirectStandardInput = True
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.CreateNoWindow = True
            End If
            process.Start()
            Return False
        End If
        Return True
    End Function

    Public Function [Stop]() As Boolean
        If Not process.HasExited Then
            ' process.Kill()
        End If

        Return True
    End Function
End Class
