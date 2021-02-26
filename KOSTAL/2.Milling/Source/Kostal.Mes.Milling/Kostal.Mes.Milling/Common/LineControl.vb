Imports Linecontroller
Public Class LineControlCfg
    Public bEnable As Boolean
    Public strDefaultLR As String
    Public strTraceId As String
    Public strPreviousTest As String
    Public strCurrentTest As String
    Public strDefalutSection As String
End Class

Public Class LineControlElement

    Public Sub New(ByVal mSN As String, ByVal mArticle As String, ByVal mIndex As String, ByVal mPreviousTest As String, ByVal ScrapResult As Boolean)
        strSN = mSN
        strArticle = mArticle
        strIndex = mIndex
        strPreviousTest = mPreviousTest
        bScrapResult = ScrapResult
    End Sub

    Public Sub New(ByVal mSN As String, ByVal mArticle As String, ByVal mIndex As String, ByVal mCurrentTest As String, ByVal WriteResult As Boolean, ByVal ScrapResult As Boolean)
        strSN = mSN
        strArticle = mArticle
        strIndex = mIndex
        strCurrentTest = mCurrentTest
        bWriteResult = WriteResult
        bScrapResult = ScrapResult
    End Sub
    Public strSN As String = ""
    Public strArticle As String = ""
    Public strIndex As String = ""
    Public strPreviousTest As String = ""
    Public strCurrentTest As String = ""
    Public bWriteResult As Boolean = False
    Public bTestResult As Boolean = False
    Public bScrapResult As Boolean = False
    Public strErrorMsg As String = ""
End Class

Public Class LineControl
    Dim LR As clsDB_Linecontrolling
    Private _TraceID As String = ""
    Private _isRunning As Boolean
    Private _isResult As Boolean
    Private _StatusDescription As String
    Protected Delegate Function dWriteStamp(ByVal strList As List(Of LineControlElement)) As Boolean
    Protected pWriteStamp As New dWriteStamp(AddressOf _WriteStamp)
    Protected pWriteStampCB As AsyncCallback = New AsyncCallback(AddressOf _WriteStampCB)

    Protected Delegate Function dReadStamp(ByVal strList As List(Of LineControlElement)) As Boolean
    Protected pReadStamp As New dWriteStamp(AddressOf _ReadStamp)
    Protected pReadStampCB As AsyncCallback = New AsyncCallback(AddressOf _ReadStampCB)


    Public ReadOnly Property isResult As Boolean
        Get
            Return _isResult
        End Get
    End Property

    Public ReadOnly Property isRunning As Boolean
        Get
            Return _isRunning
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public Function Init(ByVal InitLRFilePath As String, ByVal INTSection As String, ByVal TraceID As String) As Boolean
        LR = New clsDB_Linecontrolling
        LR.INIFile = InitLRFilePath
        LR.INISection = INTSection
        LR.TraceID = TraceID
        Dim lResult As Integer = LR.Init
        If lResult <> 0 Then
            _StatusDescription = ErrorHandler_Init(lResult) + " Error Code:" + lResult.ToString
            Return False
        End If
        _TraceID = TraceID
        Return True
    End Function
    Protected Function ErrorHandler_Init(ByVal lErrorNo As Integer) As String
        Dim strErrorMessage As String = ""
        Select Case lErrorNo
            Case 0
                strErrorMessage = "Init - No Error"
            Case -1
                strErrorMessage = "Init - Database IP Address invalid"
            Case -2
                strErrorMessage = "Init - Parameter fail"
            Case -3
                strErrorMessage = "Init - Database cannot open"
            Case -4
                strErrorMessage = "Init - No previous Tests found"
            Case -5
                strErrorMessage = "Init - Fail entry in ini file"
            Case -6
                strErrorMessage = "Init - Configuration File nor found"
            Case -7
                strErrorMessage = "Init - Local path not available"
            Case -8
                strErrorMessage = "Init - Fail durind initialization of Loggin modul"
            Case -9
                strErrorMessage = "Init - Ini File not found"
            Case -10
                strErrorMessage = "Init - Invalid Servermode"

            Case Else
                strErrorMessage = "Init - UnknowError"
        End Select
        Return strErrorMessage
    End Function

    Public Sub WriteCurrentStamp(ByVal strList As List(Of LineControlElement))
        _isRunning = True
        _isResult = False
        pWriteStamp.BeginInvoke(strList, pWriteStampCB, Nothing)
    End Sub

    Protected Function _WriteStamp(ByVal strList As List(Of LineControlElement)) As Boolean
        Try
            Dim lResult As Integer
            For Each element In strList
                If element.bScrapResult Then
                    element.bTestResult = True
                    element.strErrorMsg = "Scrap"
                    Continue For
                End If
                If LR.CheckDBConnection() <> 0 Then
                    _StatusDescription = "CheckDBConnection Fail"
                    Return False
                End If


                LR.articleNo = element.strArticle
                LR.serialNo = element.strSN
                LR.CurrentTest = element.strCurrentTest
                LR.TestResult = element.bWriteResult
                LR.TraceID = _TraceID
                LR.AdditionalText(0) = ""
                LR.AdditionalText(1) = element.strIndex
                LR.AdditionalText(2) = ""
                LR.AdditionalText(3) = ""
                lResult = LR.WriteCurrentStamp()
                If lResult = 0 Then
                    element.bTestResult = True
                    element.strErrorMsg = "OK"
                Else
                    element.bTestResult = False
                    element.strErrorMsg = ErrorHandler_WriteCurrentStamp(lResult) + " Error Code:" + lResult.ToString + "."
                End If
            Next
            Return True
        Catch ex As Exception
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Protected Sub _WriteStampCB(ByVal Result As IAsyncResult)
        _isResult = pWriteStamp.EndInvoke(Result)
        _isRunning = False
    End Sub

    Protected Function ErrorHandler_WriteCurrentStamp(ByVal iErrorNo As Integer) As String
        Dim strErrorMessage As String = ""
        Select Case iErrorNo
            Case 0
                strErrorMessage = "WriteCurrentStamp - No Error"
            Case -1
                strErrorMessage = "WriteCurrentStamp - Parameter fail"
            Case -2
                strErrorMessage = "WriteCurrentStamp - DLL is not initialized"
            Case -3
                strErrorMessage = "WriteCurrentStamp - Record was not writed"
            Case -4
                strErrorMessage = "WriteCurrentStamp - Record was not writed"
            Case Else
                strErrorMessage = "WriteCurrentStamp - UnknowError"
        End Select
        Return strErrorMessage
    End Function


    Public Sub ReadPreviousStamp(ByVal strList As List(Of LineControlElement))
        _isRunning = True
        pReadStamp.BeginInvoke(strList, pReadStampCB, Nothing)
    End Sub

    Protected Function _ReadStamp(ByVal strList As List(Of LineControlElement)) As Boolean
        Try
            Dim lResult As Integer
            For Each element In strList
                If element.bScrapResult Then
                    element.bTestResult = True
                    element.strErrorMsg = "Scrap"
                    Continue For
                End If
                LR.articleNo = element.strArticle
                LR.serialNo = element.strSN
                LR.CurrentTest = element.strCurrentTest
                LR.TestResult = element.bWriteResult
                LR.TraceID = _TraceID
                LR.PreviousTest(0) = element.strPreviousTest
                ' LR.SearchMode = 0
                lResult = LR.ReadPreviousStamp()
                If lResult = 0 Then
                    If LR.AdditionalText(1) = element.strIndex Then
                        element.bTestResult = True
                        element.strErrorMsg = "OK"
                    Else
                        element.bTestResult = False
                        element.strErrorMsg = "Linecontrol Index:" + LR.AdditionalText(1) + " Scan Index:" + element.strIndex
                    End If
                Else
                    element.bTestResult = False
                    element.strErrorMsg = ErrorHandler_ReadPreviousStamp(lResult) + " Error Code:" + lResult.ToString + "."
                End If
            Next
            Return True
        Catch ex As Exception
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Protected Sub _ReadStampCB(ByVal Result As IAsyncResult)
        _isResult = pReadStamp.EndInvoke(Result)
        _isRunning = False
    End Sub

    Protected Function ErrorHandler_ReadPreviousStamp(ByVal iErrorNo As Integer) As String
        Dim strErrorMessage As String = ""
        Select Case iErrorNo
            Case 0
                strErrorMessage = "ReadPreviousStamp - No Error"
            Case -1
                strErrorMessage = "ReadPreviousStamp - No entry to article or serialnumber found"
            Case -2
                strErrorMessage = "ReadPreviousStamp - No Connection to database"
            Case -3
                strErrorMessage = "ReadPreviousStamp - Parameter fail"
            Case -4
                strErrorMessage = "ReadPreviousStamp - Bad Part detected"
            Case -5
                strErrorMessage = "ReadPreviousStamp - DLL is not initialized"
            Case -6
                strErrorMessage = "ReadPreviousStamp - No Entries in database to this article"
            Case -69
                strErrorMessage = "ReadPreviousStamp - Previous Station Error"
            Case Else
                _StatusDescription = "ReadPreviousStamp - UnknowError"
        End Select
        Return strErrorMessage
    End Function
End Class
