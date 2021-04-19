Imports ForcamClient

Public Enum enumForcamStatus
    WindowsError = -99
    FailWhileStart = -2
    FailWhileComplete = -1
    NotInitialized = 0
    Initialized = 1
    Disabled = 2
    InStartMode = 3
    InCompleteMode = 3
End Enum
Public Class Forcam
    Private ForcamClient As New ForcamClient
    Private _Status As enumForcamStatus
    Private _StatusDescription As String = String.Empty
    Protected _FileHandler As New FileHandler
    Protected AppSettings As Settings
    Protected broker As String = String.Empty
    Protected user As String = String.Empty
    Protected pwd As String = String.Empty
    Protected Qos As String = String.Empty
    Protected TTL As String = String.Empty
    Protected controller As String = String.Empty
    Protected operation As String = String.Empty
    Protected topic As String = String.Empty
    Protected resource As String = String.Empty
    Protected Delegate Function dStart() As Boolean
    Protected pStart As New dStart(AddressOf _Start)
    Protected pStartCB As AsyncCallback = New AsyncCallback(AddressOf _StartCB)
    Protected _Start_RUN As Boolean
    Protected _Pass As Boolean
    Protected Delegate Function dComplete(ByVal iPass As Integer, ByVal iFail As Integer) As Boolean
    Protected pComplete As New dComplete(AddressOf _Complete)
    Protected pCompleteCB As AsyncCallback = New AsyncCallback(AddressOf _CompleteCB)
    Protected _Complete_RUN As Boolean

    Public ReadOnly Property Start_RUN() As Boolean
        Get
            Return _Start_RUN
        End Get
    End Property


    Public ReadOnly Property Complete_RUN() As Boolean
        Get
            Return _Complete_RUN
        End Get
    End Property

    Public ReadOnly Property Status() As enumForcamStatus
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            If _Status < enumForcamStatus.NotInitialized Then
                Return _Status.ToString & ";" & _StatusDescription
            Else
                Return _Status.ToString
            End If
        End Get
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal i As Station, ByVal mSettings As Settings, ByVal _IniFile As String)
        AppSettings = mSettings
        broker = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "broker"))
        user = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "user"))
        pwd = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "pwd"))
        Qos = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "Qos"))
        TTL = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "TTL"))
        controller = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "controller"))
        operation = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "operation"))
        topic = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "topic"))
        resource = Trim(_FileHandler.ReadIniFile(AppSettings.ConfigFolder, _IniFile, "Setting", "resource"))
        If ForcamClient.Init(broker, user, pwd, Qos, TTL, controller, operation, topic, resource) Then
            _Status = enumForcamStatus.Initialized
        End If
    End Sub
    Public Function Init(broker As String, user As String, pwd As String, Qos As Byte, TTL As Integer, controller As String, operation As String, topic As String, resource As String)
        Try
            ForcamClient.Init(broker, user, pwd, Qos, TTL, controller, operation, topic, resource)
            Return True
        Catch ex As Exception
            _Status = enumForcamStatus.NotInitialized
            _StatusDescription = "ForcamClient.Init. Message:" + ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Function Start() As Boolean
        _Start_RUN = True
        _Status = enumForcamStatus.Initialized
        pStart.BeginInvoke(pStartCB, Nothing)
        Return True
    End Function

    Private Function _Start() As Boolean
        Try
            Return ForcamClient.start()
        Catch ex As Exception
            _Status = enumForcamStatus.FailWhileStart
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Private Sub _StartCB(ByVal Result As IAsyncResult)
        _Pass = pStart.EndInvoke(Result)
        If Not _Pass Then
            _Status = enumForcamStatus.FailWhileStart
        End If
        _Start_RUN = False
    End Sub

    Public Function Complete(ByVal iPass As Integer, ByVal iFail As Integer) As Boolean
        _Complete_RUN = True
        _Status = enumForcamStatus.Initialized
        pComplete.BeginInvoke(iPass, iFail, pCompleteCB, Nothing)
        Return True
    End Function
    Private Function _Complete(ByVal iPass As Integer, ByVal iFail As Integer) As Boolean
        Try
            Return ForcamClient.completeByCount(iPass, iFail, 0)
        Catch ex As Exception
            _Status = enumForcamStatus.FailWhileStart
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Private Sub _CompleteCB(ByVal Result As IAsyncResult)
        _Pass = pComplete.EndInvoke(Result)
        If Not _Pass Then
            _Status = enumForcamStatus.FailWhileComplete
        End If
        _Complete_RUN = False
    End Sub
    Public Sub Quit()
        ForcamClient.Quit()
    End Sub

End Class
