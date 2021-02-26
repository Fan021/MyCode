'Logger - 
'Author		Frank Dümpelmann
'Version	1.0.0.0 Build 2009_04_16	- First Build
Public Enum enmLogType
    ErrorLog = 1
    AllLog = 2
End Enum

Public Class Logger

    Protected mFileHandler As New FileHandler
    Protected mSettings As New Settings
    Protected _Object As Object = ""
    Protected _ExceptionMsg As ExceptionMsg

    Public Sub New(ByVal MySettings As Settings)
        mSettings = MySettings
    End Sub

    Public Function Remove(ByVal sString As String, Optional ByVal ToRemove As String = " ") As String
        Remove = Replace(sString, ToRemove, "", 1, -1, CompareMethod.Binary)
    End Function

    Public Function OneLineText(ByVal sString As String) As String
        OneLineText = sString
        Do
            OneLineText = Replace(OneLineText, Chr(13), " ")
            OneLineText = Replace(OneLineText, Chr(10), "")
        Loop Until InStr(OneLineText, Chr(10)) = 0 And InStr(OneLineText, Chr(13)) = 0
    End Function


    '==================================================================================================================
    '==================================================================================================================
    'Loggings
    '==================================================================================================================
    '==================================================================================================================
    'Write Error in Logfile and send message to User
    Public Sub PcError(ByVal i As Station, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing)
        SyncLock _Object
            If i.IsMasterError Or i.IsError Then Exit Sub

            Logger(i, MessageBox, Messager)
            i.IsError = True
        End SyncLock

    End Sub

    'Write Master Error in Logfile, send message to User and terminate the program
    Public Sub PcMasterError(ByVal i As Station, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing)
        SyncLock _Object
            If i.IsMasterError Then Exit Sub

            Logger(i, MessageBox, Messager)
            i.IsError = False
            i.IsMasterError = True
        End SyncLock

    End Sub

    'Write Master Error in Logfile, send message to User and terminate the program
    Public Sub StackError(ByVal i As Station, ByVal logLevel As enmLogType, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing)
        SyncLock _Object
            LoggerOnlyTextLine(i, logLevel, MessageBox, Messager)
            _ExceptionMsg = New ExceptionMsg()
            _ExceptionMsg.TextBox_Msg.Text = i.Text
            _ExceptionMsg.TextBox_Stack.Text = i.Stack
            _ExceptionMsg.ShowDialog()
            _ExceptionMsg.Dispose()
            i.IsError = False
        End SyncLock

    End Sub

    Public Overloads Sub Logger(ByVal i As Station)
        SaveLogger(i, enmLogType.AllLog)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal strText As String)
        SaveLogger(i, enmLogType.AllLog, , , strText)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal strText As String, ByVal strTextLine As String)
        SaveLogger(i, enmLogType.AllLog, , , strText, strTextLine)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal Messager As Messager)
        SaveLogger(i, enmLogType.AllLog, , Messager)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String)
        SaveLogger(i, enmLogType.AllLog, , Messager, strText)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal logLevel As enmLogType, ByVal Messager As Messager, ByVal strText As String)
        SaveLogger(i, logLevel, , Messager, strText)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveLogger(i, enmLogType.AllLog, , Messager, strText, strTextLine)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean)
        SaveLogger(i, MessageBox)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String)
        SaveLogger(i, enmLogType.AllLog, MessageBox, , strText)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String, ByVal strTextLine As String)
        SaveLogger(i, enmLogType.AllLog, MessageBox, , strText, strTextLine)
    End Sub


    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager)
        SaveLogger(i, enmLogType.AllLog, MessageBox, Messager)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String)
        SaveLogger(i, enmLogType.AllLog, MessageBox, Messager, strText)
    End Sub

    Public Overloads Sub Logger(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveLogger(i, enmLogType.AllLog, MessageBox, Messager, strText, strTextLine)
    End Sub

    Public Sub SaveLogger(ByVal i As Station, ByVal LogLevel As enmLogType, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing, Optional ByVal strText As String = "", Optional ByVal strTextLine As String = "")
        SyncLock _Object
            Dim _Text As String
            Dim _TextWriteLog As String
            If strText <> "" Then i.Text = strText
            If strTextLine <> "" Then
                _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.IdString & mSettings.Separator & strTextLine & mSettings.Separator & i.Text
            Else
                _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.IdString & mSettings.Separator & i.StepTextLine & mSettings.Separator & i.Text
            End If
            _TextWriteLog = _Text
            If mSettings.LogCfg.LogEnable Then
                If LogLevel <= mSettings.LogCfg.LogLevel Then
                    If mSettings.LogCfg.LogPath = "" Then
                        mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.LogName, _TextWriteLog)
                    Else
                        mFileHandler.WriteLogFile(mSettings.LogCfg.LogPath, mSettings.LogName, _TextWriteLog)
                    End If
                End If
            End If

            If Not IsNothing(mSettings.LogBox) Then
                mSettings.LogBox.ShowMessage(_Text)   'list messages,  from appSetting
            End If
            If Not Messager Is Nothing Then
                Messager.ShowMessage(_Text)     'added to lblMessage or 
            End If

            If MessageBox Then
                MsgBox(i.Text, vbCritical, i.IdString & mSettings.Separator & i.StepTextLine)       'show Msgbox
            End If
        End SyncLock
    End Sub

    Public Sub LoggerNoStepTextLine(ByVal i As Station, ByVal Messager As Messager, Optional ByVal strText As String = "", Optional ByVal strTextLine As String = "")
        SyncLock _Object
            Dim _Text As String
            If strText <> "" Then i.Text = strText
            If strTextLine <> "" Then i.StepTextLine = strTextLine
            _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.IdString & mSettings.Separator & i.StepTextLine & mSettings.Separator & i.Text
            mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.LogName, _Text)
            If Not IsNothing(mSettings.LogBox) Then
                mSettings.LogBox.ShowMessage(_Text)   'list messages,  from appSetting
            End If

            If Not Messager Is Nothing Then
                Messager.ShowMessage(i.Text)     'added to lblMessage or 
            End If
        End SyncLock
    End Sub
    Public Sub LoggerOnlyTextLine(ByVal i As Station, ByVal logLevel As enmLogType, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing)
        SyncLock _Object
            Dim _Text, _TextWriteLog As String
            _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.Text
            _TextWriteLog = _Text
            _TextWriteLog = _TextWriteLog.Replace(vbCrLf, ";")
            If mSettings.LogCfg.LogEnable Then
                If logLevel <= mSettings.LogCfg.LogLevel Then
                    If mSettings.LogCfg.LogPath = "" Then
                        mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.LogName, _TextWriteLog)
                    Else
                        mFileHandler.WriteLogFile(mSettings.LogCfg.LogPath, mSettings.LogName, _TextWriteLog)
                    End If
                End If
            End If

            If Not IsNothing(mSettings.LogBox) Then
                mSettings.LogBox.ShowMessage(_Text)   'list messages,  from appSetting
            End If

            If Not Messager Is Nothing Then
                Messager.ShowMessage(i.Text)     'added to lblMessage or 
            End If

            If MessageBox Then
                MsgBox(i.Text, vbCritical, i.StepTextLine)       'show Msgbox
            End If
        End SyncLock
    End Sub


    Public Overloads Sub Thrower(ByVal i As Station)
        SaveThrower(i)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal strText As String)
        SaveThrower(i, , , strText)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal strText As String, ByVal strTextLine As String)
        SaveThrower(i, , , strText, strTextLine)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal Messager As Messager)
        SaveThrower(i, , Messager)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String)
        SaveThrower(i, , Messager, strText)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveThrower(i, , Messager, strText, strTextLine)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean)
        SaveThrower(i, MessageBox)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String)
        SaveThrower(i, MessageBox, , strText)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String, ByVal strTextLine As String)
        SaveThrower(i, MessageBox, , strText, strTextLine)
    End Sub


    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager)
        SaveThrower(i, MessageBox, Messager)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String)
        SaveThrower(i, MessageBox, Messager, strText)
    End Sub

    Public Overloads Sub Thrower(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveThrower(i, MessageBox, Messager, strText, strTextLine)
    End Sub

    Public Sub SaveThrower(ByVal i As Station, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing, Optional ByVal strText As String = "", Optional ByVal strTextLine As String = "")
        SyncLock _Object
            Dim _Text As String
            If strText <> "" Then i.Text = strText
            If strTextLine <> "" Then i.StepTextLine = strTextLine
            _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.IdString & mSettings.Separator & i.StepTextLine & mSettings.Separator & i.Text
            mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ApplicationName, _Text)
            If Not IsNothing(mSettings.LogBox) Then
                mSettings.LogBox.ShowMessage(_Text)   'list messages,  from appSetting
            End If

            If Not Messager Is Nothing Then
                Messager.ShowMessage(i.StepTextLine & mSettings.Separator & i.Text)     'added to lblMessage or 
            End If

            If MessageBox Then
                MsgBox(i.Text, vbCritical, i.IdString & mSettings.Separator & i.StepTextLine)       'show Msgbox
            End If
            Throw New Exception("Staion:" & i.IdString + vbCrLf + "Function:" & i.StepTextLine + vbCrLf + "Message:" & i.Text)
        End SyncLock
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station)
        SaveThrowerNoStation(i)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal strText As String)
        SaveThrowerNoStation(i, , , strText)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal strText As String, ByVal strTextLine As String)
        SaveThrowerNoStation(i, , , strText, strTextLine)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal Messager As Messager)
        SaveThrowerNoStation(i, , Messager)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String)
        SaveThrowerNoStation(i, , Messager, strText)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveThrowerNoStation(i, , Messager, strText, strTextLine)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal MessageBox As Boolean)
        SaveThrowerNoStation(i, MessageBox)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String)
        SaveThrowerNoStation(i, MessageBox, , strText)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal MessageBox As Boolean, ByVal strText As String, ByVal strTextLine As String)
        SaveThrowerNoStation(i, MessageBox, , strText, strTextLine)
    End Sub


    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager)
        SaveThrowerNoStation(i, MessageBox, Messager)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal logLevel As enmLogType, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String)
        SaveThrowerNoStation(i, MessageBox, Messager, strText)
    End Sub

    Public Overloads Sub ThrowerNoStation(ByVal i As Station, ByVal MessageBox As Boolean, ByVal Messager As Messager, ByVal strText As String, ByVal strTextLine As String)
        SaveThrowerNoStation(i, MessageBox, Messager, strText, strTextLine)
    End Sub

    Public Overloads Sub SaveThrowerNoStation(ByVal i As Station, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing, Optional ByVal strText As String = "", Optional ByVal strTextLine As String = "")
        SyncLock _Object
            Dim _Text As String
            If strText <> "" Then i.Text = strText
            If strTextLine <> "" Then i.StepTextLine = strTextLine
            _Text = Format(Date.Now, "yyyy.MM.dd") & mSettings.Separator & Format(Date.Now, "HH:mm:ss") & mSettings.Separator & i.IdString & mSettings.Separator & i.StepTextLine & mSettings.Separator & i.Text
            mFileHandler.WriteLogFile(mSettings.LogFolder, mSettings.ApplicationName, _Text)
            If Not IsNothing(mSettings.LogBox) Then
                mSettings.LogBox.ShowMessage(_Text)   'list messages,  from appSetting
            End If

            If Not Messager Is Nothing Then
                Messager.ShowMessage(i.StepTextLine & mSettings.Separator & i.Text)     'added to lblMessage or 
            End If

            If MessageBox Then
                MsgBox(i.Text, vbCritical, i.IdString & mSettings.Separator & i.StepTextLine)       'show Msgbox
            End If
            If strTextLine <> "" Then
                Throw New Exception("Function:" + i.StepTextLine & mSettings.Separator & i.Text)
            Else
                Throw New Exception(i.Text)
            End If
        End SyncLock
    End Sub


    Public Sub Viewer(ByVal i As Station, Optional ByVal MessageBox As Boolean = False, Optional ByVal Messager As Messager = Nothing)
        If Not Messager Is Nothing Then
            Messager.ShowMessage(i.StepTextLine & mSettings.Separator & i.Text)
        End If
        If MessageBox Then
            MsgBox(i.Text, vbCritical, i.IdString & mSettings.Separator & i.StepTextLine)
        End If
    End Sub


    '==================================================================================================================
    '==================================================================================================================
    'End Loggings
    '==================================================================================================================
    '==================================================================================================================

End Class



