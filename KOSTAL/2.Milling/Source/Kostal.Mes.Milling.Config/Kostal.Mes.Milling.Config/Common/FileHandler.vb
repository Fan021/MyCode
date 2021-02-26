Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

Public Class FileHandler

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

    Protected Const i_MAX_CHAR As Integer = 256
    Public Const s_DEFAULT As String = "#ERROR#"
    Public Const s_Null As String = ""


    Public ReadOnly Property ErrorString() As String
        Get
            Return s_DEFAULT
        End Get
    End Property

    Public ReadOnly Property MaxChar() As Integer
        Get
            Return i_MAX_CHAR
        End Get
    End Property



    Public Overloads Function Read(ByVal CompleteFileName As String, ByVal s_Section As String, ByVal s_KeyWord As String) As String
        Dim i_StringLenght As Integer, sb_Result As New StringBuilder(256), s_Result As String

        SyncLock objGetPrivateProfileString
            i_StringLenght = GetPrivateProfileStringKey(s_Section, s_KeyWord, s_DEFAULT, sb_Result, i_MAX_CHAR, CompleteFileName)
        End SyncLock

        s_Result = LSet(sb_Result.ToString, i_StringLenght)

        Return Trim(s_Result)
    End Function

    Public Overloads Function Read(ByVal PathName As String, ByVal FileName As String, ByVal s_Section As String, ByVal s_KeyWord As String) As String
        Dim CompleteFileName As String

        If Right(PathName, 1) = "\" Then
            CompleteFileName = PathName & FileName
        Else
            CompleteFileName = PathName & "\" & FileName
        End If

        If Not FileExist(CompleteFileName) Then
            Throw New Exception("File Not Find:" + CompleteFileName)
        End If

        Return Read(CompleteFileName, s_Section, s_KeyWord)

    End Function


    Public Function Write(ByVal CompleteFileName As String, ByVal s_Section As String, ByVal s_KeyWord As String, ByVal s_Entry As String) As String
        Dim l_Result As Long

        SyncLock objWritePrivateProfileString
            l_Result = WritePrivateProfileString(s_Section, s_KeyWord, " " & s_Entry, CompleteFileName)
        End SyncLock

        Return l_Result.ToString

    End Function


    Public Function FileExist(ByVal CompleteFileName As String) As Boolean

        If Dir(CompleteFileName) <> "" Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Function DelectLogByDay(ByVal iDay As Integer, ByVal folderPath As String, ByVal extension As String) As Boolean
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
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function

    Public Function DelectFile(ByVal strFile As String) As Boolean
        Try
            If Not File.Exists(strFile) Then
                Throw New Exception("File :" + strFile + " not existed")
            End If
            File.Delete(strFile)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Overloads Function FileMove(ByVal strSrcPath As String, ByVal strDestPath As String, ByVal strFileName As String, Optional ByVal bOverwrite As Boolean = True) As Boolean
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Overloads Function FileMove(ByVal strSrcFile As String, ByVal strDestFile As String, Optional ByVal bOverwrite As Boolean = True) As Boolean
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

End Class

