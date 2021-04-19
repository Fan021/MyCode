Imports System.IO.Ports

Public Class EpsonSerial

    Public Shared _com As SerialPort
    Private _comport As Integer = 10
    Private _defaultSetting As String = "9600,N,8,1"

    Public Function Init(ByVal iPort As Integer, ByVal settings As String) As Integer
        Dim setting() As String

        If settings = "" Then
            setting = _defaultSetting.Split(","c)
        Else
            setting = settings.Split(","c)
        End If

        Try
            If _com Is Nothing Then
                _com = New SerialPort
            End If

            With _com
                If Not .IsOpen Then
                    .PortName = "COM" + iPort.ToString
                    .BaudRate = CInt(setting(0))
                    Select Case setting(1).ToUpper
                        Case "N"
                            .Parity = Parity.None
                        Case "E"
                            .Parity = Parity.Even
                        Case "O"
                            .Parity = Parity.Odd
                        Case Else
                            Return -1
                    End Select
                    .DataBits = CInt(setting(2))
                    .StopBits = CInt(setting(3))
                    .Open()
                End If
            End With

            _comport = iPort

        Catch ex As Exception
            Throw
            Return -1
        End Try

        Return 0
    End Function

    Public Function Print(title As String, failInfo As TestInfo.TestInfo, ByVal sn As String) As Boolean
        Dim printText As String
        Try
            printText = setInfo(title, failInfo, sn)
            _com.WriteLine(printText)
            CutLabel()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Private Function setInfo(title As String, failInfo As TestInfo.TestInfo, ByVal sn As String) As String
        Dim resultStr As String = String.Empty
        Dim newLine As String = vbCrLf

        resultStr = title & newLine
        resultStr += "  Date: " & Format(Now, "yyyy/MM/dd HH:mm") & newLine
        resultStr += "  Article: " & failInfo.GetArticleNo & newLine
        resultStr += "  SerialNo: " & sn & newLine
        resultStr += "  Test Step: " & failInfo.GetFailStep & newLine
        resultStr += "  LLimit: " & failInfo.GetFailLoLimit & newLine
        resultStr += "  ULimit: " & failInfo.GetFailUpLimit & newLine
        resultStr += "  Nomal Value: " & failInfo.GetFailNomvalue & newLine
        resultStr += "  Actual Value: " & failInfo.GetMeasuredValue & newLine

        Return resultStr
    End Function

    Friend Function PrintT(title As String, ByVal nginfo As String, ByVal articleName As String) As String
        Dim resultstr As String = ""
        Dim newLine As String = vbCrLf
        Dim tempstr() As String
        Dim splitChar As String = "\"
        Dim labelTitle As String = "-----------SGM_358 Failure Part ----------" & vbCrLf
        tempstr = nginfo.Split(splitChar)
        If tempstr.Length < 5 Then
        Else
            resultstr = labelTitle & newLine & "Date: " & Format(Now, "yyyy/MM/dd HH:mm") & newLine
            resultstr += "Article: " & articleName & newLine
            resultstr += "Test Step: " & tempstr(0) & newLine
            resultstr += "Compare Mode: " & tempstr(1) & newLine
            resultstr += "LLimit: " & tempstr(2) & newLine
            resultstr += "ULimit: " & tempstr(3) & newLine
            resultstr += "Nomal Value: " & tempstr(4) & newLine
            resultstr += "Actual Value: " & tempstr(5) & newLine
            resultstr += "------------------------------------------" & vbCrLf & vbCrLf & vbCrLf & vbCrLf
        End If
        Return resultstr
    End Function

    Private Sub CutLabel()
        Dim ByteCut() As Byte = {&H1B, &H40, &H1B, &H69, &H1}
        _com.Write(ByteCut, 0, ByteCut.Length)
    End Sub

    Public Sub Quit()
        Try
            If _com IsNot Nothing AndAlso _com.IsOpen Then
                _com.Close()
                _com = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ShowDebugWindow()
        Dim mForm As New EpsonDebug
        mForm.Show()
    End Sub

End Class
