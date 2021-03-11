Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Concurrent

Public Class clsFileHandler
    <DllImport("kernel32", EntryPoint:="GetPrivateProfileStringA", CharSet:=CharSet.Ansi)> _
    Protected Shared Function GetPrivateProfileStringKey( _
                                                        ByVal s_Section As String, _
                                                        ByVal s_KeyWord As String, _
                                                        ByVal s_Default As String, _
                                                        ByVal sb_Result As StringBuilder, _
                                                        ByVal l_MaxChar As Int32, _
                                                        ByVal s_FileName As String _
                                                        ) As Int32

    End Function

    <DllImport("kernel32", EntryPoint:="WritePrivateProfileStringA", CharSet:=CharSet.Ansi)> _
    Protected Shared Function WritePrivateProfileString( _
                                                        ByVal s_Section As String, _
                                                        ByVal s_KeyWord As String, _
                                                        ByVal s_Entry As String, _
                                                        ByVal s_FileName As String _
                                                        ) As Int32

    End Function


    Protected Shared objGetPrivateProfileString As New Object
    Protected Shared objWritePrivateProfileString As New Object

    Protected Shared objWriteLogFile As New Object

    Protected Const i_MAX_CHAR As Integer = 1024
    Public Const s_DEFAULT As String = "#ERROR#"
    Public Const s_Null As String = ""
    Private _Object As New Object


    Public ReadOnly Property ErrorString() As String
        Get
            SyncLock _Object
                Return s_DEFAULT
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property MaxChar() As Integer
        Get
            SyncLock _Object
                Return i_MAX_CHAR
            End SyncLock
        End Get
    End Property


    Public Function FileExist(ByVal CompleteFileName As String) As Boolean
        SyncLock _Object
            If Dir(CompleteFileName) <> "" Then
                Return True
            Else
                Return False
            End If
        End SyncLock
    End Function


    Public Function DelectLogByDay(ByVal iDay As Integer, ByVal folderPath As String, ByVal extension As String) As Boolean
        SyncLock _Object
            Try

                If extension.IndexOf(".") <> 0 Then
                    Throw New Exception("Invalid extension:" + extension)
                End If

                Dim directroyInfo As DirectoryInfo = New DirectoryInfo(folderPath)
                Dim t1 As DateTime
                Dim t2 As DateTime
                t2 = DateTime.Parse(Date.Now.AddDays(-iDay).ToString)
                For Each tempFile In directroyInfo.GetFiles
                    t1 = DateTime.Parse(tempFile.CreationTime.ToString)
                    If DateTime.Compare(t1, t2) <= 0 And tempFile.Extension = extension Then
                        File.Delete(tempFile.FullName)
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DelectFile(ByVal strFile As String) As Boolean
        SyncLock _Object
            Try
                If Not File.Exists(strFile) Then
                    Throw New Exception("File :" + strFile + " not existed")
                End If
                File.Delete(strFile)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Overloads Function FileMove(ByVal strSrcPath As String, ByVal strDestPath As String, ByVal strFileName As String, Optional ByVal bOverwrite As Boolean = True) As Boolean
        SyncLock _Object
            Try
                If strFileName.IndexOf(".") < 0 Then
                    Throw New Exception("Invalid FileName:" + strFileName)
                End If

                If Not File.Exists(Path.Combine(strSrcPath, strFileName)) Then
                    Throw New Exception("File :" + Path.Combine(strSrcPath, strFileName) + " not existed")
                End If

                If Not Directory.Exists(strDestPath) Then
                    Directory.CreateDirectory(strDestPath)
                End If

                If File.Exists(Path.Combine(strDestPath, strFileName)) Then
                    If bOverwrite Then
                        File.Delete(Path.Combine(strDestPath, strFileName))
                    Else
                        Throw New Exception("File :" + Path.Combine(strDestPath, strFileName) + " have existed")
                    End If
                End If

                File.Move(Path.Combine(strSrcPath, strFileName), Path.Combine(strDestPath, strFileName))
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Overloads Function FileMove(ByVal strSrcFile As String, ByVal strDestFile As String, Optional ByVal bOverwrite As Boolean = True) As Boolean
        SyncLock _Object
            Try
                If strSrcFile.IndexOf(".") < 0 Then
                    Throw New Exception("Invalid strSrcFile:" + strSrcFile)
                End If

                If strDestFile.IndexOf(".") < 0 Then
                    Throw New Exception("Invalid strDestFile:" + strDestFile)
                End If

                If Not File.Exists(strSrcFile) Then
                    Throw New Exception("File :" + strSrcFile + " not existed")
                End If

                If Not Directory.Exists(strDestFile.Replace(Path.GetFileName(strDestFile), "")) Then
                    Directory.CreateDirectory(strDestFile.Replace(Path.GetFileName(strDestFile), ""))
                End If

                If File.Exists(strDestFile) Then
                    If bOverwrite Then
                        File.Delete(strDestFile)
                    Else
                        Throw New Exception("File :" + strDestFile + " have existed")
                    End If
                End If

                File.Move(strSrcFile, strDestFile)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckFolder(ByVal strFolderName As String) As String
        SyncLock _Object
            Try
                Dim strResult As String
                strResult = strFolderName
                If Not My.Computer.FileSystem.DirectoryExists(strResult) Then
                    My.Computer.FileSystem.CreateDirectory(strResult)
                End If
                Return strResult
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return ""
            End Try
        End SyncLock
    End Function

    Public Function ListDll(ByVal strFolderName As String) As List(Of String)
        SyncLock _Object
            Dim lListDll As New List(Of String)
            If Not Directory.Exists(strFolderName) Then Return lListDll

            For Each f As String In Directory.GetFiles(strFolderName)
                If Path.GetExtension(f).ToUpper <> ".DLL" Then Continue For
                lListDll.Add(f)
            Next
            Return lListDll
        End SyncLock
    End Function

    Public Function ListIni(ByVal strFolderName As String) As List(Of String)
        SyncLock _Object
            Dim lListIni As New List(Of String)
            If Not Directory.Exists(strFolderName) Then Return lListIni

            For Each f As String In Directory.GetFiles(strFolderName)
                If Path.GetExtension(f).ToUpper <> ".INI" Then Continue For
                lListIni.Add(f)
            Next
            Return lListIni
        End SyncLock
    End Function
End Class
