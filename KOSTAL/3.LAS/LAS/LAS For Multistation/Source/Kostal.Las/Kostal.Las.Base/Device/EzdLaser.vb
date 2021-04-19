Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Kostal.Las.Base
Imports LaserEzd

Public Class EzdLaser
    Implements ILaserBase

    Protected Enum EnumRequestActionType
        Idle = 0
        IsInitializing = 2
        IsLoadingTemplate = 4
        IsChangingVariable = 6
        IsGettingVariable = 7
        IsMarking = 8
    End Enum

    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station

    Protected _statusDescription As String
    Protected _status As Alltec_StatusCode
    Protected _currentActionType As EnumRequestActionType
    Property _changeTextDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

#Region "Private"
    Public ReadOnly Property LastResponse As String Implements ILaserBase.LastResponse
        Get
            Return ""
        End Get
    End Property

    Public ReadOnly Property ReadyToWrite As Boolean Implements ILaserBase.ReadyToWrite
        Get
            Return _currentActionType = EnumRequestActionType.Idle
        End Get
    End Property

    Public ReadOnly Property Status As Alltec_StatusCode Implements ILaserBase.Status
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String Implements ILaserBase.StatusDescription
        Get
            Return _statusDescription
        End Get
    End Property

#End Region

    Public Function GetGetTemplate() As Boolean Implements ILaserBase.GetGetTemplate
        Return True
    End Function

    Public Function GetGetTemplateReady() As Boolean Implements ILaserBase.GetGetTemplateReady
        Return True
    End Function

    Public Function GetStatus() As Boolean Implements ILaserBase.GetStatus
        Return True
    End Function

    Public Function GetStatusReady(mTemplateName As String) As Boolean Implements ILaserBase.GetStatusReady
        Return True
    End Function

    Public Function GetVar() As Boolean Implements ILaserBase.GetVar
        Try
            Dim result As String = ""

            _currentActionType = EnumRequestActionType.IsGettingVariable

            For Each kv As KeyValuePair(Of String, String) In _changeTextDic

                Dim r As Integer = LMCDriver.lmc1_GetTextByName(kv.Key, result)

                If (r <> 0) Then
                    _statusDescription = "获取变量内容出错，错误代码：" + r.ToString + "; 错误信息:" + EzCadErrorMessage.ErrorMessage(r) + "; Key:" + kv.Key + "; Value:" + kv.Value
                    Throw New Exception(_statusDescription)
                    Return False
                End If

            Next

        Catch ex As Exception
            Throw ex
        End Try

        _currentActionType = EnumRequestActionType.Idle

        Return True
    End Function

    Public Function GetVarReady() As Boolean Implements ILaserBase.GetVarReady
        Return True
    End Function

    Public Function Init(mType As DeviceType, mConfig As String, MyStation As Station, _AppSettings As Settings, MyLanguage As Language) As Boolean Implements ILaserBase.Init

        LMCDriver.lmc1_Close()

        _i = MyStation
        AppSettings = _AppSettings
        _Language = MyLanguage
        Dim path As String = AppSettings.LibFolder + "Ezd"

        Try
            _currentActionType = EnumRequestActionType.IsInitializing
            Dim r As Integer = LMCDriver.lmc1_Initial(path, False, _AppSettings.mForm.Handle)

            If (r <> 0) Then
                _statusDescription = "初始化光刻设备出错，错误代码：" + r.ToString + "；错误信息:" + EzCadErrorMessage.ErrorMessage(r)
                Throw New Exception(_statusDescription)
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

        _currentActionType = EnumRequestActionType.Idle
        Return True

    End Function

    Public Function SetAndGetTemplate(name As String) As Boolean Implements ILaserBase.SetAndGetTemplate

        _currentActionType = EnumRequestActionType.IsLoadingTemplate

        Dim r As Integer = LMCDriver.lmc1_LoadEzdFile(name)

        If (r <> 0) Then
            _statusDescription = "加载打印模板出错，错误代码：" + r.ToString + "；错误信息:" + EzCadErrorMessage.ErrorMessage(r)
            Throw New Exception(_statusDescription)
            Return False
        End If

        _currentActionType = EnumRequestActionType.Idle
        Return True

    End Function

    Public Function SetAnyCommand(cmd As String) As Boolean Implements ILaserBase.SetAnyCommand

        Try
            Dim tempStr As String() = cmd.Split(";")
            _changeTextDic.Clear()

            For Each str As String In tempStr
                Dim k As String = str.Split(",")(0).Trim
                Dim v As String = str.Split(",")(1).Trim
                _changeTextDic.Add(k, v)
            Next

            _currentActionType = EnumRequestActionType.IsChangingVariable

            For Each kv As KeyValuePair(Of String, String) In _changeTextDic

                Dim r As Integer = LMCDriver.lmc1_ChangeTextByName(kv.Key, kv.Value)

                If (r <> 0) Then
                    _statusDescription = "设置变量内容出错，错误代码：" + r.ToString + "; 错误信息:" + EzCadErrorMessage.ErrorMessage(r) + "; Key:" + kv.Key + "; Value:" + kv.Value
                    Throw New Exception(_statusDescription)
                    Return False
                End If

            Next

            _currentActionType = EnumRequestActionType.Idle

        Catch ex As Exception
            Throw ex
        End Try

        Return True

    End Function

    Public Function SetAnyCommandReady(cmd As String) As Boolean Implements ILaserBase.SetAnyCommandReady
        Return True
    End Function

    Public Function SetTemplateReady(name As String) As Boolean Implements ILaserBase.SetTemplateReady
        Return True
    End Function

    Public Function Start() As Boolean Implements ILaserBase.Start

        _currentActionType = EnumRequestActionType.IsMarking

        Dim r As Integer = LMCDriver.lmc1_Mark(False)

        If (r <> 0) Then
            Throw New Exception("光刻出错，错误代码：" + r.ToString + "; 错误信息:" + EzCadErrorMessage.ErrorMessage(r))
            Return False
        End If

        _currentActionType = EnumRequestActionType.Idle

        Return True

    End Function

    Public Function StartReady() As Boolean Implements ILaserBase.StartReady
        Return True
    End Function

    Public Sub Dispose() Implements ILaserBase.Dispose
        LMCDriver.lmc1_Close()

    End Sub

    Public Sub ResetLastResponse() Implements ILaserBase.ResetLastResponse

    End Sub

End Class
