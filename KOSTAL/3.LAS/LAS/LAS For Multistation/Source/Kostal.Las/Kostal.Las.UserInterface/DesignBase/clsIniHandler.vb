Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Concurrent

Public Class clsIniHandler
    Private _Object As New Object
    Public Const Name As String = "IniHandler"
    Public Const DefaultValue As String = ""
    Public lListFileValue As New List(Of String)
    Public lListSectionValue As New List(Of String)
    '<DllImport("kernel32", EntryPoint:="GetPrivateProfileStringA")> _
    'Protected Shared Function GetPrivateProfileStringKey( _
    '                                                    ByVal strSection As String, _
    '                                                    ByVal strKeyWord As String, _
    '                                                    ByVal strDefault As String, _
    '                                                   <MarshalAs(UnmanagedType.LPArray)> ByVal lpBuffer() As Byte, _
    '                                                    ByVal iMaxChar As Integer, _
    '                                                    ByVal strFileName As String _
    '                                                    ) As Integer

    'End Function



    '<DllImport("kernel32", EntryPoint:="WritePrivateProfileStringA", CharSet:=CharSet.Auto)> _
    'Protected Shared Function WritePrivateProfileString( _
    '                                                    ByVal strSection As String, _
    '                                                    ByVal strKeyWord As String, _
    '                                                    ByVal strEntry As String, _
    '                                                    ByVal strFileName As String _
    '                                                    ) As Integer

    'End Function

    '<DllImport("kernel32", EntryPoint:="GetPrivateProfileSectionNamesA", CharSet:=CharSet.Ansi)> _
    'Protected Shared Function GetPrivateProfileSectionNames( _
    '                                                    ByVal buffer() As Byte, _
    '                                                    ByVal iMaxChar As Integer, _
    '                                                    ByVal strFileName As String _
    '                                                    ) As Integer

    'End Function

    Public Function CleanSection(ByVal strFileFullPath As String, ByVal strSection As String) As Boolean
        SyncLock _Object
            Try
                If Not File.Exists(strFileFullPath) Then
                    Dim iFile2 As New StreamWriter(strFileFullPath)
                    iFile2.Close()
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim strLastSection As String = String.Empty
                Dim mTemp As String = iFile.ReadLine
                Dim lListRead As New List(Of String)
                Do While Not IsNothing(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        strLastSection = mTemp
                    End If
                    If strLastSection = "[" + strSection + "]" Then
                    Else
                        lListRead.Add(mTemp)
                    End If

                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()
                Dim iFile1 As New StreamWriter(strFileFullPath)
                For Each element As String In lListRead
                    iFile1.WriteLine(element)
                Next
                iFile1.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function WriteIniFile(ByVal strFileFullPath As String, ByVal strSection As String, ByVal strKeyWord As Object, Optional ByVal strValue As Object = Nothing) As Boolean
        SyncLock _Object
            Try
                If Not File.Exists(strFileFullPath) Then
                    Dim iFile2 As New StreamWriter(strFileFullPath)
                    iFile2.Close()
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim strLastSection As String = String.Empty
                Dim mTemp As String = iFile.ReadLine
                Dim lListRead As New List(Of String)
                Dim bFind As Boolean = False
                Dim bKeyFind As Boolean = False
                Do While Not IsNothing(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        strLastSection = mTemp
                        If bFind Then
                            If Not bKeyFind Then
                                lListRead.Add(strKeyWord + "=" + strValue.ToString)
                                bKeyFind = True
                            End If
                        End If
                    End If
                    If strLastSection = "[" + strSection + "]" Then
                        bFind = True
                        If IsNothing(strKeyWord) Then
                        Else
                            If mTemp.IndexOf("=") >= 0 Then
                                Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                                Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                                If strKeyWord = mTempKey Then
                                    mTemp = strKeyWord + "=" + strValue.ToString
                                    bKeyFind = True
                                End If
                            End If
                            lListRead.Add(mTemp)
                        End If
                    Else
                        lListRead.Add(mTemp)
                    End If

                    mTemp = iFile.ReadLine
                Loop
                If Not bFind Then
                    mTemp = "[" + strSection + "]"
                    lListRead.Add(mTemp)
                    mTemp = strKeyWord + "=" + strValue.ToString
                    lListRead.Add(mTemp)
                End If
                If bFind Then
                    If Not bKeyFind Then
                        mTemp = strKeyWord + "=" + strValue.ToString
                        lListRead.Add(mTemp)
                    End If
                End If
                iFile.Close()


                Dim iFile1 As New StreamWriter(strFileFullPath)
                For Each element As String In lListRead
                    iFile1.WriteLine(element)
                Next
                iFile1.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function OpenIniFile(ByVal strFileFullPath As String)
        SyncLock _Object
            Try
                lListFileValue.Clear()
                lListSectionValue.Clear()
                If Not File.Exists(strFileFullPath) Then
                    Return True
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim mTemp As String = iFile.ReadLine
                Do While Not IsNothing(mTemp)
                    lListFileValue.Add(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        lListSectionValue.Add(mTemp.Replace("[", "").Replace("]", ""))
                    End If
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveIniFile(ByVal strFileFullPath As String, ByVal lListValue As List(Of String))
        SyncLock _Object
            Try
                Dim iFile1 As New StreamWriter(strFileFullPath, False)
                For Each element As String In lListValue
                    iFile1.WriteLine(element)
                Next
                iFile1.Close()
                lListValue.Clear()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function WriteIniFileOnOpen(ByVal strFileFullPath As String, ByVal strSection As String, ByVal strKeyWord As Object, Optional ByVal strValue As Object = Nothing) As Boolean
        SyncLock _Object
            Try
                Dim strLastSection As String = String.Empty
                Dim lListRead As New List(Of String)
                Dim bFind As Boolean = False
                Dim bKeyFind As Boolean = False
                Dim mTemp As String = ""
                For Each mTemp In lListFileValue
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        strLastSection = mTemp
                        If bFind Then
                            If Not bKeyFind Then
                                lListRead.Add(strKeyWord + "=" + strValue.ToString)
                                bKeyFind = True
                            End If
                        End If
                    End If
                    If strLastSection = "[" + strSection + "]" Then
                        bFind = True
                        If IsNothing(strKeyWord) Then
                        Else
                            If mTemp.IndexOf("=") >= 0 Then
                                Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                                Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                                If strKeyWord = mTempKey Then
                                    mTemp = strKeyWord + "=" + strValue.ToString
                                    bKeyFind = True
                                End If
                            End If
                            lListRead.Add(mTemp)
                        End If
                    Else
                        lListRead.Add(mTemp)
                    End If

                Next

                If Not bFind Then
                    mTemp = "[" + strSection + "]"
                    lListRead.Add(mTemp)
                    mTemp = strKeyWord + "=" + strValue.ToString
                    lListRead.Add(mTemp)
                End If
                If bFind Then
                    If Not bKeyFind Then
                        mTemp = strKeyWord + "=" + strValue.ToString
                        lListRead.Add(mTemp)
                    End If
                End If
                lListFileValue.Clear()
                For Each element As String In lListRead
                    lListFileValue.Add(element)
                Next

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function




    Public Function ReadIniFile(ByVal strFileFullPath As String, ByVal strSection As String, ByVal strKeyWord As String) As String
        SyncLock _Object
            Try
                If Not File.Exists(strFileFullPath) Then
                    Return ""
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim strLastSection As String = String.Empty
                Dim mTemp As String = iFile.ReadLine
                Do While Not IsNothing(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        strLastSection = mTemp
                    End If
                    If strLastSection = "[" + strSection + "]" Then
                        If mTemp.IndexOf("=") >= 0 Then
                            Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                            mTempKey = RTrim(mTempKey)
                            Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                            Dim i As Integer = strKeyWord.Length
                            Dim j As Integer = mTempKey.Length
                            If strKeyWord = mTempKey Then
                                iFile.Close()
                                Return mTempValue
                            End If
                        End If
                    End If
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()
                Return ""
            Catch ex As Exception
                Throw ex
                Return ""
            End Try
        End SyncLock
    End Function

    Public Function ReadIniFileFromOpenIni(ByVal strFileFullPath As String, ByVal strSection As String, ByVal strKeyWord As String) As String
        SyncLock _Object
            Try
                If Not File.Exists(strFileFullPath) Then
                    Return ""
                End If
                Dim iCnt As Integer = 0
                Dim strLastSection As String = String.Empty
                For Each mTemp In lListFileValue
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        strLastSection = mTemp
                    End If
                    If strLastSection = "[" + strSection + "]" Then
                        If mTemp.IndexOf("=") >= 0 Then
                            Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                            mTempKey = RTrim(mTempKey)
                            Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                            If strKeyWord = mTempKey Then
                                lListFileValue.RemoveAt(iCnt)
                                Return mTempValue
                            End If
                        End If
                    End If
                    iCnt = iCnt + 1
                Next
                Return ""
            Catch ex As Exception
                Throw ex
                Return ""
            End Try
        End SyncLock
    End Function
    Public Function CheckSectionName(ByVal strFileFullPath As String, ByVal strSection As String) As Boolean
        SyncLock _Object
            Try

                Dim lArrayList As New List(Of String)
                If Not File.Exists(strFileFullPath) Then
                    Return False
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim mTemp As String = iFile.ReadLine
                Dim lListRead As New List(Of String)
                Do While Not IsNothing(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        lArrayList.Add(mTemp.Replace("[", "").Replace("]", ""))
                    End If
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()

                If lArrayList.Contains(strSection) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckSectionNameFromOpenIni(ByVal strFileFullPath As String, ByVal strSection As String) As Boolean
        SyncLock _Object
            Try
                If lListSectionValue.Contains(strSection) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetSectionName(ByVal strFileFullPath As String, ByVal strSection As String) As List(Of String)
        SyncLock _Object
            Try

                Dim lArrayList As New List(Of String)
                If Not File.Exists(strFileFullPath) Then
                    Return lArrayList
                End If
                Dim iFile As New StreamReader(strFileFullPath)
                Dim mTemp As String = iFile.ReadLine
                Dim lListRead As New List(Of String)
                Do While Not IsNothing(mTemp)
                    If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                        lArrayList.Add(mTemp.Replace("[", "").Replace("]", ""))
                    End If
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()

                Return lArrayList

            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveAllSection(ByVal strFileFullPath As String, ByVal strSection As String) As Boolean
        SyncLock _Object
            Try

                Dim iCnt As Integer = 1
                Dim mTempSection As String = String.Empty

                mTempSection = strSection + iCnt.ToString
                Do While CheckSectionName(strFileFullPath, mTempSection)
                    CleanSection(strFileFullPath, mTempSection)
                    iCnt = iCnt + 1
                    mTempSection = strSection + iCnt.ToString
                Loop

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RemoveSection(ByVal strFileFullPath As String, ByVal strSection As String) As Boolean
        SyncLock _Object
            Try
                CleanSection(strFileFullPath, strSection)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetAnyListFromIni(ByVal strFileFullPath As String, ByVal strSection As String, ByVal cTagName() As String) As List(Of Object)
        SyncLock _Object
            Try
                Dim lListDictionary As New List(Of Object)
                Dim lTagValue As New Dictionary(Of String, Object)
                Dim iCnt As Integer = 1
                Dim mTempSection As String = String.Empty
                Dim mTempValue As String = String.Empty

                mTempSection = strSection + iCnt.ToString
                Do While CheckSectionName(strFileFullPath, mTempSection)
                    lTagValue = New Dictionary(Of String, Object)
                    For Each element As String In cTagName
                        mTempValue = ReadIniFile(strFileFullPath, mTempSection, element)
                        If mTempValue = DefaultValue Then
                            mTempValue = ""
                        End If
                        lTagValue.Add(element, mTempValue)
                    Next
                    lListDictionary.Add(lTagValue)
                    iCnt = iCnt + 1
                    mTempSection = strSection + iCnt.ToString
                Loop
                Return lListDictionary

            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetAnyListFromOpenIni(ByVal strFileFullPath As String, ByVal strSection As String, ByVal cTagName() As String) As List(Of Dictionary(Of String, String))
        SyncLock _Object
            Try

                Dim lListDictionary As New List(Of Dictionary(Of String, String))
                Dim lTagValue As New Dictionary(Of String, String)
                Dim iCnt As Integer = 1
                Dim mTempSection As String = String.Empty
                Dim mTempValue As String = String.Empty

                mTempSection = strSection + iCnt.ToString
                Do While CheckSectionNameFromOpenIni(strFileFullPath, mTempSection)
                    lTagValue = New Dictionary(Of String, String)
                    For Each element As String In cTagName
                        mTempValue = ReadIniFileFromOpenIni(strFileFullPath, mTempSection, element)
                        If mTempValue = DefaultValue Then
                            mTempValue = ""
                        End If
                        lTagValue.Add(element, mTempValue)
                    Next
                    lListDictionary.Add(lTagValue)
                    iCnt = iCnt + 1
                    mTempSection = strSection + iCnt.ToString
                Loop
                Return lListDictionary

            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function SetAnyListToIni(ByVal strFileFullPath As String, ByVal strSection As String, ByVal cTagName() As String, ByVal cTagValue() As String) As Boolean
        SyncLock _Object
            Try

                Dim iCnt As Integer = 1

                For i = 0 To cTagName.Length - 1
                    If Not WriteIniFile(strFileFullPath, strSection, cTagName.GetValue(i).ToString, cTagValue.GetValue(i).ToString) Then
                        Throw New Exception("WriteIniFile Fail")
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
End Class
