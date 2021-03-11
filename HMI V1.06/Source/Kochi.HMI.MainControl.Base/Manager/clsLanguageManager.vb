Imports System.Reflection
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Action
Imports System.Collections.Concurrent
Imports System.IO

Public Class clsLanguageManager
    Public Const Name As String = "LanguageManager"
    Private cSystemManager As clsSystemManager
    Private cFileHandler As New clsFileHandler
    Private cMachineManager As clsMachineManager
    Private lListIni As New Dictionary(Of String, clsLanguageCfg)
    Private _Object As New Object
    Private cIniHandler As clsIniHandler
    Private cCurrentLanguageCfg As New clsLanguageCfg
    Private lListText As New List(Of String)
    Private lListUserText As New List(Of String)
    Private lListLocalText As New List(Of String)
    Private cSystemElement As New Dictionary(Of String, Object)
    Private strFirtLanguage As String
    Private strSecondLanguage As String
    Private bSecondLanguageEnable As Boolean = False
    Private bSecondLanguageActive As Boolean = False

    Public ReadOnly Property FirtLanguage As String
        Get
            Return strFirtLanguage
        End Get
    End Property

    Public ReadOnly Property SecondLanguage As String
        Get
            Return strSecondLanguage
        End Get
    End Property

    Public ReadOnly Property SecondLanguageEnable As Boolean
        Get
            Return bSecondLanguageEnable
        End Get
    End Property

    Public ReadOnly Property CurrentLanguageCfg As clsLanguageCfg
        Get
            Return cCurrentLanguageCfg
        End Get
    End Property

    Public ReadOnly Property SecondLanguageActive As Boolean
        Get
            Return bSecondLanguageActive
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                LoadIni()

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function LoadActiveLanguage() As Boolean
        Try
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            strFirtLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.FirstLanguage)
            If strFirtLanguage = "" Then
                strFirtLanguage = lListIni(lListIni.Keys(0)).Name
            End If
            strSecondLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.SecondLanguage)
            If strSecondLanguage = "" Then
                strSecondLanguage = "NONE"
            End If
            If strSecondLanguage = "NONE" Then
                bSecondLanguageEnable = False
            Else
                bSecondLanguageEnable = True
            End If

            If bSecondLanguageEnable Then
                If strSecondLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Language) Then
                    bSecondLanguageActive = True
                Else
                    bSecondLanguageActive = False
                End If
            Else
                bSecondLanguageActive = False
            End If

            If lListIni.ContainsKey(cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Language)) Then
                ChangeLanguage(cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Language))
            Else
                ChangeLanguage(lListIni(lListIni.Keys(0)).Name)
            End If
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Function GetTextLanguageKey() As List(Of String)
        Return lListIni.Keys.ToList
    End Function

    Public Function ChangeLanguage(ByVal strLanguageName As String) As Boolean
        SyncLock _Object
            Try
                cCurrentLanguageCfg = lListIni(strLanguageName)
                strFirtLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.FirstLanguage)
                If strFirtLanguage = "" Then
                    strFirtLanguage = lListIni(lListIni.Keys(0)).Name
                End If
                strSecondLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.SecondLanguage)
                If strSecondLanguage = "" Then
                    strSecondLanguage = "NONE"
                End If
                If strSecondLanguage = "NONE" Then
                    bSecondLanguageEnable = False
                Else
                    bSecondLanguageEnable = True
                End If
                If bSecondLanguageEnable Then
                    If strSecondLanguage = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Language) Then
                        bSecondLanguageActive = True
                    Else
                        bSecondLanguageActive = False
                    End If
                Else
                    bSecondLanguageActive = False
                End If
                lListText.Clear()
                If Not File.Exists(cCurrentLanguageCfg.Path) Then
                    Return ""
                End If
                Dim iFile As New StreamReader(cCurrentLanguageCfg.Path)
                Dim mTemp As String = iFile.ReadLine
                Do While Not IsNothing(mTemp)
                    lListText.Add(mTemp)
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()

                lListUserText.Clear()
                If Not File.Exists(cSystemManager.Settings.LanguageFolder + "\User_" + cCurrentLanguageCfg.Name + ".ini") Then
                    Return False
                End If
                iFile = New StreamReader(cSystemManager.Settings.LanguageFolder + "\User_" + cCurrentLanguageCfg.Name + ".ini")
                mTemp = iFile.ReadLine
                Do While Not IsNothing(mTemp)
                    lListUserText.Add(mTemp)
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()

                'Local
                lListLocalText.Clear()
                If Not File.Exists(cSystemManager.Settings.LanguageFolder + "\Local_" + cCurrentLanguageCfg.Name + ".ini") Then
                    Return False
                End If
                iFile = New StreamReader(cSystemManager.Settings.LanguageFolder + "\Local_" + cCurrentLanguageCfg.Name + ".ini")
                mTemp = iFile.ReadLine
                Do While Not IsNothing(mTemp)
                    lListLocalText.Add(mTemp)
                    mTemp = iFile.ReadLine
                Loop
                iFile.Close()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Function LoadIni() As Boolean
        SyncLock _Object
            Try
                For Each element As String In cFileHandler.ListIni(cSystemManager.Settings.LanguageFolder)
                    Dim fileName As String = element.Substring(element.LastIndexOf("\") + 1).Replace(".ini", "")
                    If fileName.IndexOf("_") >= 0 Then Continue For
                    lListIni.Add(fileName, New clsLanguageCfg(fileName, element))
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function GetTextLine(ByVal strSection As String, ByVal strKey As String, ByVal ParamArray strParameter() As String) As String
        Try
            Dim sResult As String = String.Empty
            Dim strLastSection As String = String.Empty
            For Each mTemp As String In lListText
                If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                    strLastSection = mTemp
                End If
                If strLastSection = "[" + strSection + "]" Then
                    If mTemp.IndexOf("=") >= 0 Then
                        Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                        mTempKey = RTrim(mTempKey)
                        Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                        If strKey = mTempKey Then
                            sResult = mTempValue
                            Exit For
                        End If
                    End If
                End If
            Next
            If sResult = "" Then
                sResult = strKey
            End If
            If (strParameter.Count > 0) Then
                Dim i As Integer = 0
                For i = 0 To strParameter.Count - 1
                    sResult = sResult.Replace("$" + (i + 1).ToString, strParameter(i).ToString)
                Next
            End If
            sResult = sResult.Replace("\r", vbCr)
            sResult = sResult.Replace("\n", vbLf)
            Return sResult
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetChangeTextLine(ByVal strFileName As String, ByVal strSection As String, ByVal strKey As String, ByVal ParamArray strParameter() As String) As String
        Dim sResult As String = String.Empty
        sResult = cIniHandler.ReadIniFile(cSystemManager.Settings.LanguageFolder + "\" + strFileName, strSection, strKey)
        If sResult = "" Then
            sResult = strKey
        End If
        If (strParameter.Count > 0) Then
            Dim i As Integer = 0
            For i = 0 To strParameter.Count - 1
                sResult = sResult.Replace("$" + (i + 1).ToString, strParameter(i).ToString)
            Next
        End If
        sResult = sResult.Replace("\r", vbCr)
        sResult = sResult.Replace("\n", vbLf)
        Return sResult
    End Function
    Public Function GetUserTextLine(ByVal strSection As String, ByVal strKey As String, ByVal ParamArray strParameter() As String) As String
        Try
            Dim sResult As String = String.Empty
            Dim strLastSection As String = String.Empty
            For Each mTemp As String In lListUserText
                If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                    strLastSection = mTemp
                End If
                If strLastSection = "[" + strSection + "]" Then
                    If mTemp.IndexOf("=") >= 0 Then
                        Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                        mTempKey = RTrim(mTempKey)
                        Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                        If strKey = mTempKey Then
                            sResult = mTempValue
                            Exit For
                        End If
                    End If
                End If
            Next
            If sResult = "" Then
                sResult = strKey
            End If
            If (strParameter.Count > 0) Then
                Dim i As Integer = 0
                For i = 0 To strParameter.Count - 1
                    sResult = sResult.Replace("$" + (i + 1).ToString, strParameter(i).ToString)
                Next
            End If
            sResult = sResult.Replace("\r", vbCr)
            sResult = sResult.Replace("\n", vbLf)
            Return sResult
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetLocalTextLine(ByVal strSection As String, ByVal strKey As String, ByVal ParamArray strParameter() As String) As String
        Try
            Dim sResult As String = String.Empty
            Dim strLastSection As String = String.Empty
            For Each mTemp As String In lListLocalText
                If mTemp.IndexOf("[") = 0 And mTemp.IndexOf("]") = mTemp.Length - 1 Then
                    strLastSection = mTemp
                End If
                If strLastSection = "[" + strSection + "]" Then
                    If mTemp.IndexOf("=") >= 0 Then
                        Dim mTempKey = mTemp.Substring(0, mTemp.IndexOf("="))
                        mTempKey = RTrim(mTempKey)
                        Dim mTempValue = mTemp.Substring(mTemp.IndexOf("=") + 1)
                        If strKey = mTempKey Then
                            sResult = mTempValue
                            Exit For
                        End If
                    End If
                End If
            Next
            If sResult = "" Then
                sResult = strKey
            End If
            If (strParameter.Count > 0) Then
                Dim i As Integer = 0
                For i = 0 To strParameter.Count - 1
                    sResult = sResult.Replace("$" + (i + 1).ToString, strParameter(i).ToString)
                Next
            End If
            sResult = sResult.Replace("\r", vbCr)
            sResult = sResult.Replace("\n", vbLf)
            Return sResult
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

Public Class clsLanguageCfg
    Private strPath As String = String.Empty
    Private strName As String = String.Empty
    Private _Object As New Object

    Public Property Path As String
        Set(ByVal value As String)
            SyncLock _Object
                strPath = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strPath
            End SyncLock
        End Get
    End Property

    Public Property Name As String
        Set(ByVal value As String)
            SyncLock _Object
                strName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strName
            End SyncLock
        End Get
    End Property
    Sub New(ByVal strName As String, ByVal strPath As String)
        Me.strName = strName
        Me.strPath = strPath
    End Sub
    Sub New()

    End Sub
End Class
