Imports System.IO
Public Class clsLogHandler
    Private _Object As New Object
    Public Const Name As String = "LogHandler"
    Private cSystemManager As clsSystemManager
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try

                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                DeleteFile()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub DeleteFile()
        If Directory.Exists(cSystemManager.Settings.LogFolder) Then
            Dim directroyInfo As DirectoryInfo = New DirectoryInfo(cSystemManager.Settings.LogFolder)
            Dim t1 As DateTime
            Dim t2 As DateTime
            t2 = DateTime.Parse(Date.Now.AddDays(-60).ToString)
            For Each tempFile As FileInfo In directroyInfo.GetFiles
                t1 = DateTime.Parse(tempFile.CreationTime.ToString)
                If DateTime.Compare(t1, t2) <= 0 Then
                    File.Delete(tempFile.FullName)
                End If
            Next
        End If
    End Sub

    Public Sub SaveLogger(ByVal strFilePath As String, ByVal strMessage As String)
        SyncLock _Object
            Dim strText As String
            Dim strFileFullPath = strFilePath & "\" & Format(Date.Now, "yyyy_MM_dd") & ".log"
            Dim cFile As StreamWriter
            strText = Format(Date.Now, "yyyy.MM.dd") & " " & Format(Date.Now, "HH:mm:ss ffff") & " | " & strMessage
            cFile = New StreamWriter(strFileFullPath, True)
            cFile.WriteLine(strText)
            cFile.Close()
        End SyncLock
    End Sub

    Public Sub SaveLogger(ByVal strFilePath As String, ByVal cException As Exception)
        SyncLock _Object
            Dim strText As String
            Dim strFileFullPath = strFilePath & "\" & Format(Date.Now, "yyyy_MM_dd") & ".log"
            Dim cFile As StreamWriter

            strText = Format(Date.Now, "yyyy.MM.dd") & " " & Format(Date.Now, "HH:mm:ss ffff")
            If TypeOf cException Is clsHMIException Then
                Select Case CType(cException, clsHMIException).ExceptionType
                    Case enumExceptionType.Crash, enumExceptionType.Alarm
                        If IsNothing(CType(cException, clsHMIException).InnerException) Then
                            strText = strText & "|" & CType(cException, clsHMIException).Name.ToString & "|" & CType(cException, clsHMIException).ExceptionType.ToString & "|" & CType(cException, clsHMIException).Message & "|" & CType(cException, clsHMIException).ToString
                        Else
                            strText = strText & "|" & CType(cException, clsHMIException).Name.ToString & "|" & CType(cException, clsHMIException).ExceptionType.ToString & "|" & CType(cException, clsHMIException).Message & "|" & CType(cException, clsHMIException).ToString & "|" & CType(cException, clsHMIException).InnerException.StackTrace.ToString
                        End If

                    Case enumExceptionType.Confirm
                        Return
                    Case Else
                        strText = strText & "|" & CType(cException, clsHMIException).ExceptionType.ToString & "|" & CType(cException, clsHMIException).Message
                End Select
            Else
                strText = strText & "|" & enumExceptionType.Crash.ToString & "|" & cException.Message & "|" & cException.ToString
            End If
            cFile = New StreamWriter(strFileFullPath, True)
            cFile.WriteLine(strText)
            cFile.Close()
        End SyncLock
    End Sub

    Public Sub SaveLogger(ByVal strFilePath As String, ByVal cMainTipsManagerCfg As clsMainTipsManagerCfg)
        SyncLock _Object
            Dim strText As String
            Dim strFileFullPath = strFilePath & "\" & Format(Date.Now, "yyyy_MM_dd") & ".log"
            Dim cFile As StreamWriter

            strText = Format(Date.Now, "yyyy.MM.dd") & " " & Format(Date.Now, "HH:mm:ss ffff")

            Select Case cMainTipsManagerCfg.MainTipsManagerType
                Case enumMainTipsManagerType.Normal
                    strText = strText & "|" & enumMainTipsManagerType.Normal.ToString & "|" & cMainTipsManagerCfg.Text
                Case enumMainTipsManagerType.Confirm
                    strText = strText & "|" & enumMainTipsManagerType.Normal.ToString & "|" & cMainTipsManagerCfg.Text
                    Return
                Case Else
                    strText = strText & "|" & enumMainTipsManagerType.Normal.ToString & "|" & cMainTipsManagerCfg.Text
            End Select
            cFile = New StreamWriter(strFileFullPath, True)
            cFile.WriteLine(strText)
            cFile.Close()
        End SyncLock
    End Sub
End Class
