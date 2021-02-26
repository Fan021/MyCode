Public Class clsAlarm
    Public Const Name As String = "Alarm"
    Public strLastTime As String = String.Empty
    Protected _FileHandler As New FileHandler
    Protected _Settings As Settings
    Public strLastArticle As String = String.Empty

    Public Sub Init(ByVal _Settings As Settings)
        Me._Settings = _Settings
        Dim strResult As String = _FileHandler.ReadIniFile(_Settings.LogFolder + "Alarm.ini", "Time", "Shift")
        If strResult <> FileHandler.s_DEFAULT And strResult <> FileHandler.s_Null Then
            strLastTime = strResult
        Else
            strLastTime = 1
        End If

        strResult = _FileHandler.ReadIniFile(_Settings.LogFolder + "Alarm.ini", "Time", "Article")
        If strResult <> FileHandler.s_DEFAULT And strResult <> FileHandler.s_Null Then
            strLastArticle = strResult
        Else
            strLastArticle = ""
        End If

    End Sub
    Public Sub Save()
        _FileHandler.WriteIniFile(_Settings.LogFolder + "Alarm.ini", "Time", "Shift", strLastTime)
    End Sub
    Public Sub Save2()
        _FileHandler.WriteIniFile(_Settings.LogFolder + "Alarm.ini", "Time", "Article", strLastArticle)
    End Sub

End Class
