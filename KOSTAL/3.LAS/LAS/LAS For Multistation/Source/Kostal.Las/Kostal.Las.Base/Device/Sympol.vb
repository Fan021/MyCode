Imports System.Text
Public Class Sympol_MS447
    Inherits Keyence_Scanner
    Const MIN_FRAME_LEN As Byte = 4
    Const MAX_FRAME_LEN As Byte = 255
    Enum CMD_TYPE As Byte
        Abort_Macro_Pdf = &H11
        Aim_Off = &HC4
        Aim_On = &HC5
        Para_Default = &HC8
        Para_Send = &HC6
        Para_Request = &HC7
        Start_Session = &HE4
        End_Session = &HE5
        Decode_Data = &HF3
        Cmd_Ack = &HD0
        Cmd_Nak = &HD1
        Enable = &HE9
        Disable = &HEA
    End Enum

    Enum MessSrc As Byte
        Decode = 0
        Host = 4
    End Enum



    Public Overrides Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        If mConfig.Split(CChar(",")).Length <> 2 And mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_CONFIG
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.BaudRate = Integer.Parse(mConfig.Split(CChar(","))(1))
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _InterfaceConfig.DataFrameSTX = Chr(&H16)
            _InterfaceConfig.DataFrameEXT = Chr(&HD)
            _Interface = New RS232Interface
        End If
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

    Public Overrides Function TrigON() As Boolean
        Try
            Dim dtData As Byte() = MakePackage(Nothing, CMD_TYPE.Start_Session)
            Wakeup()
            _Interface.SendCommandAndRead(Encoding.ASCII.GetString(dtData), "111", 3000)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Overrides Function TrigOFF() As Boolean
        Dim dtData As Byte() = MakePackage(Nothing, CMD_TYPE.End_Session)
        Wakeup()
        _Interface.Send(Encoding.ASCII.GetString(dtData))
        Return True
    End Function

    Public Overrides Function Beep() As Boolean
        Dim cmd(3) As Byte
        cmd(0) = &H42
        cmd(1) = &H45
        cmd(2) = &H4C
        _Interface.Send(Encoding.ASCII.GetString(cmd))
        Return True
    End Function

    Public Overrides Function Scan(ByVal iTimeOut As Integer, ByVal strTrigOn As String, ByVal strTrigOff As String) As String
        Return ""
    End Function


    ''' <summary>
    ''' Make the send-date to available format for host transmit only.
    ''' </summary>
    ''' <param name="dtSendData">Byte-data want to send.</param>
    ''' <param name="cmdType">Command types(Abort_Macro_Pdf;Aim_Off;Aim_On;Para_Default;Para_Send;_
    ''' Para_Request;Start_Session;End_Session;Decode_Data;Cmd_Ack;Cmd_Nak;Enable;Disable).</param>
    ''' <returns>The packaged data.</returns>
    Private Function MakePackage(ByVal dtSendData As Byte(), ByVal cmdType As CMD_TYPE) As Byte()
        ' Length Opcode Message Source Status Data.... Checksum
        Dim nDataLen As Byte = 0

        If dtSendData Is Nothing Then
            nDataLen = 0
        Else
            nDataLen = CByte(dtSendData.Length)
        End If

        Dim DataToSend(nDataLen + 5) As Byte
        Dim nCheckSum As UInt32 = 0
        Dim nIndex As Int32 = 0

        DataToSend(nIndex) = CByte(nDataLen + 4) : nIndex += 1
        DataToSend(nIndex) = cmdType : nIndex += 1
        DataToSend(nIndex) = MessSrc.Host : nIndex += 1
        DataToSend(nIndex) = 8 : nIndex += 1 'Performance changed

        If nDataLen <> 0 Then
            Array.Copy(dtSendData, 0, DataToSend, nIndex, nDataLen)
            : nIndex += dtSendData.Length
        End If

        For i As Byte = 0 To CByte(nDataLen + 3)
            nCheckSum += DataToSend(i)
        Next

        nCheckSum = CUInt(&H10000 - nCheckSum)
        'check sum 
        DataToSend(nIndex) = CByte((nCheckSum >> 8) And &HFF) : nIndex += 1
        DataToSend(nIndex) = CByte(nCheckSum And &HFF)

        Return DataToSend
    End Function

    ''' <summary>
    ''' This sub make the scanner wakeup.
    ''' </summary>
    Public Overridable Sub Wakeup()
        Dim cmd(1) As Byte
        cmd(0) = &H0
        _Interface.Send(Encoding.ASCII.GetString(cmd))
        Threading.Thread.Sleep(100)
    End Sub

    ''' <summary>
    ''' Check the package for the received data.
    ''' </summary>
    ''' <param name=" RecvData">Data array to check.</param>
    ''' <param name="cmdType">Command types(Abort_Macro_Pdf;Aim_Off;Aim_On;Para_Default;Para_Send;_
    ''' Para_Request;Start_Session;End_Session;Decode_Data;Cmd_Ack;Cmd_Nak;Enable;Disable).</param>
    ''' <param name=" FrameData">Data array of this Frame.</param>
    ''' <returns>True for success, false for fail.</returns>
    Private Function CheckPackage(ByVal RecvData() As Byte, ByVal cmdType As CMD_TYPE, ByRef FrameData() As Byte) As Boolean

        If RecvData Is Nothing Then Return False

        Dim bRet As Boolean = False
        Dim nStartPos As Int32 = 0

        Do Until nStartPos >= RecvData.Length

            'check nLen
            If RecvData.Length - nStartPos < (RecvData(nStartPos) + 2) Then
                nStartPos += 1 'next position
                Continue Do
            End If

            If RecvData(nStartPos) < MIN_FRAME_LEN Then
                nStartPos += 1 'next position
                Continue Do
            End If

            'Check command Type
            If RecvData(nStartPos + 1) <> cmdType Then
                nStartPos += 1
                Continue Do
            End If

            'check sum
            Dim nLen As Byte = RecvData(nStartPos)
            Dim nSum As Int32 = 0
            For i As Byte = CByte(nStartPos) To CByte(nLen - 1)
                nSum += RecvData(i)
            Next

            nSum = &H10000 - nSum

            bRet = (CByte(nSum >> 8 And &HFF) = RecvData(nStartPos + nLen) And _
                   CByte(nSum >> 8 Mod &HFF) = RecvData(nStartPos + nLen))

            'Copy data of this frame
            If bRet Then
                If Not FrameData Is Nothing Then
                    Array.Resize(FrameData, nLen + 2)
                    Array.Copy(RecvData, nStartPos, FrameData, 0, nLen + 2)
                End If
                Exit Do
            End If

            'next pos 
            nStartPos += 1
        Loop

        Return bRet

    End Function
End Class
