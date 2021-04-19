Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics
Imports Kostal.Las.Base

Public Interface IScannerBase
    ReadOnly Property Running As Boolean
    Property ScanResult As String
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property InterfaceConfig As InterfaceConfig
    ReadOnly Property InterfaceDevice As IInterface
    ReadOnly Property StatusDescription As String
    Function TrigON() As Boolean
    Function TrigOFF() As Boolean
    Function Beep() As Boolean
    Function SendAndReadCommand(ByVal iTimeOut As Integer, ByVal strSendCommand As String, ByVal strReadCommnd As String) As Boolean
    Function SendCommand(ByVal strSendCommand As String) As Boolean
    Function Scan(ByVal iTimeOut As Integer, ByVal strTrigOn As String, ByVal strTrigOff As String) As String
    Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
    Sub Dispose()
End Interface

Public Class Scanner
    Implements IScanner

    Protected _iScanner As IScannerBase
    Protected _DeviceType As New DeviceType
    Public Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String) Implements IScanner.DataReceived

    ReadOnly Property Running As Boolean Implements IScanner.Running
        Get
            If IsNothing(_iScanner) Then Return False
            Return _iScanner.Running
        End Get
    End Property

    Property ScanResult As String Implements IScanner.ScanResult

        Get
            If IsNothing(_iScanner) Then Return ""
            Return _iScanner.ScanResult
        End Get
        Set(ByVal value As String)
            _iScanner.ScanResult = value
        End Set
    End Property

    ReadOnly Property Status As enumDevice_ErrorCodes Implements IScanner.Status
        Get
            If IsNothing(_iScanner) Then Return enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            Return _iScanner.Status
        End Get
    End Property

    ReadOnly Property StatusDescription As String Implements IScanner.StatusDescription
        Get
            If IsNothing(_iScanner) Then Return ""
            Return _iScanner.StatusDescription
        End Get
    End Property

    Public ReadOnly Property InterfaceConfig As InterfaceConfig Implements IScanner.InterfaceConfig
        Get
            If IsNothing(_iScanner) Then Return Nothing
            Return _iScanner.InterfaceConfig
        End Get
    End Property

    Public ReadOnly Property InterfaceDevice As IInterface Implements IScanner.InterfaceDevice
        Get
            If IsNothing(_iScanner) Then Return Nothing
            Return _iScanner.InterfaceDevice
        End Get
    End Property

    Public Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IScanner.Init
        If mType = LasDeviceType.Keyence_LAN Then
            _iScanner = New Keyence_Scanner()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Keyence_RS232 Then
            _iScanner = New Keyence_Scanner()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.PSD_IT_4820_RS232 Then
            _iScanner = New PSD_IT_4820()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.Sympol_MS447_RS232 Then
            _iScanner = New Sympol_MS447()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.DataLogic_210_LAN Then
            _iScanner = New DataLogic_210()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.DataLogic_210_RS232 Then
            _iScanner = New DataLogic_210()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.DataLogic_210N_LAN Then
            _iScanner = New DataLogic_210N()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.PD9530_RS232 Then
            _iScanner = New PD9530()
            _DeviceType = DeviceType.RS232
        Else
            Return False
        End If
        If IsNothing(_iScanner) Then Return False
        If Not _iScanner.Init(_DeviceType, mConfig, MyStation, _AppSettings, MyLanguage) Then Return False
        AddHandler _iScanner.DataReceived, AddressOf ScannerDataReceived
        Return True
    End Function

    Public Function Scan(ByVal iTimeOut As Integer, ByVal strTrigOn As String, ByVal strTrigOff As String) As String Implements IScanner.Scan
        If IsNothing(_iScanner) Then Return "No Define"
        Return _iScanner.Scan(iTimeOut, strTrigOn, strTrigOff)
    End Function

    Public Function TrigOFF() As Boolean Implements IScanner.TrigOFF
        If IsNothing(_iScanner) Then Return False
        If Not _iScanner.TrigOFF() Then Return False
        Return True
    End Function

    Public Function TrigON() As Boolean Implements IScanner.TrigON
        If IsNothing(_iScanner) Then Return False
        If Not _iScanner.TrigON() Then Return False
        Return True
    End Function

    Public Function Beep() As Boolean Implements IScanner.Beep
        If IsNothing(_iScanner) Then Return False
        If Not _iScanner.Beep() Then Return False
        Return True
    End Function

    Public Sub Dispose() Implements IScanner.Dispose
        If IsNothing(_iScanner) Then Return
        _iScanner.Dispose()
    End Sub

    Protected Sub ScannerDataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        RaiseEvent DataReceived(Pass, Result, ErrorMsg)
    End Sub

    Public Function SendAndReadCommand(ByVal iTimeOut As Integer, ByVal strSendCommand As String, ByVal strReadCommnd As String) As Boolean Implements IScanner.SendAndReadCommand
        If IsNothing(_iScanner) Then Return False
        Return _iScanner.SendAndReadCommand(iTimeOut, strSendCommand, strReadCommnd)
    End Function

    Public Function SendCommand(strSendCommand As String) As Boolean Implements IScanner.SendCommand
        If IsNothing(_iScanner) Then Return False
        Return _iScanner.SendCommand(strSendCommand)
    End Function
End Class


Public Enum enumKeyence_ScannerCmds
    LON = 1
    LOFF = 2
    _LDON = 3
    _LDOFF = 4
    BCLR
    SSET
    RESET
End Enum

Public Enum enumDataFrame
    STX = &H2
    EXT = &H3
End Enum


#Region "Keyence all-types"

Public Class Keyence_Scanner
    Implements IScannerBase

#Region "Declarations"

    Protected _Interface As IInterface
    Protected _InterfaceConfig As New InterfaceConfig
    Public Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String) Implements IScannerBase.DataReceived
    Protected IsDisposed As Boolean
    Public Const CON_sERROR As String = "ERROR"
    Public Const CON_sTIMEOUT As String = "TIMEOUT"
    Public Const CON_sNULL As String = ""
    Protected _Status As enumDevice_ErrorCodes
    Protected _StatusDescription As String = String.Empty
    Protected _STX As String = String.Empty
    Protected _EXT As String = String.Empty
    Protected _TrigOn As String = String.Empty
    Protected _TrigOff As String = String.Empty
    Protected _ConfigFile As String = String.Empty
    Protected _FileHander As New FileHandler
    Protected _XmlHander As New XmlHandler
    Protected _sResult As String = String.Empty
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station
#End Region

#Region "Properties"

    Public Property DeviceInterface() As IInterface
        Set(ByVal value As IInterface)
            _Interface = value
        End Set
        Get
            Return _Interface
        End Get
    End Property

    Public Property Result() As String Implements IScannerBase.ScanResult
        Get
            Return _Interface.ScanResult
        End Get
        Set(ByVal value As String)
            _Interface.ScanResult = value
        End Set
    End Property

    Public ReadOnly Property Running() As Boolean Implements IScannerBase.Running
        Get
            Return _Interface.Running
        End Get
    End Property


    Public ReadOnly Property Status() As enumDevice_ErrorCodes Implements IScannerBase.Status
        Get
            If Not IsNothing(_Interface) Then
                'If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return _Interface.Status
                ' End If
            End If
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements IScannerBase.StatusDescription
        Get
            If Not IsNothing(_Interface) Then
                'If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return _Interface.StatusDescription
                ' End If
            End If
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property InterfaceConfig As InterfaceConfig Implements IScannerBase.InterfaceConfig
        Get
            Return _InterfaceConfig
        End Get
    End Property

    Public ReadOnly Property InterfaceDevice As IInterface Implements IScannerBase.InterfaceDevice
        Get
            Return _Interface
        End Get
    End Property

#End Region

    Public Overridable Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IScannerBase.Init
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation

        If mConfig.Split(CChar(",")).Length <> 2 And mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_CONFIG
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        If mType = DeviceType.LAN Then
            _InterfaceConfig.IP = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.Port = Integer.Parse(mConfig.Split(CChar(","))(1))
            _Interface = New TCPInterface
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.BaudRate = Integer.Parse(mConfig.Split(CChar(","))(1))
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _Interface = New RS232Interface
        End If

        If mConfig.Split(CChar(",")).Length >= 3 Then '此时从ini读取触发命令
            _ConfigFile = mConfig.Split(CChar(","))(2)
            GetConfigCommand()
        Else
            _InterfaceConfig.DataFrameSTX = Chr(enumDataFrame.STX)
            _InterfaceConfig.DataFrameEXT = Chr(enumDataFrame.EXT)
            _InterfaceConfig.TrigOn = _InterfaceConfig.DataFrameSTX + enumKeyence_ScannerCmds.LON.ToString + _InterfaceConfig.DataFrameEXT
            _InterfaceConfig.TrigOff = _InterfaceConfig.DataFrameSTX + enumKeyence_ScannerCmds.LOFF.ToString + _InterfaceConfig.DataFrameEXT
            _InterfaceConfig.Beep = ""
        End If
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

    Public Overridable Function TrigON() As Boolean Implements IScannerBase.TrigON
        Try
            If _InterfaceConfig.TrigOn <> "" Then _Interface.SendRead(_InterfaceConfig.TrigOn, 3000)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function TrigOFF() As Boolean Implements IScannerBase.TrigOFF
        Try
            If _InterfaceConfig.TrigOff <> "" Then _Interface.Send(_InterfaceConfig.TrigOff)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function Beep() As Boolean Implements IScannerBase.Beep
        If _InterfaceConfig.Beep <> "" Then _Interface.Send(_InterfaceConfig.Beep)
        Return True
    End Function

    Public Overridable Function Scan(ByVal iTimeOut As Integer, ByVal strTrigOn As String, ByVal strTrigOff As String) As String Implements IScannerBase.Scan
        If strTrigOn = "" Then
            strTrigOn = _InterfaceConfig.TrigOn
        Else
            strTrigOn = strTrigOn
        End If
        If strTrigOff = "" Then
            strTrigOff = _InterfaceConfig.TrigOff
        Else
            strTrigOff = strTrigOff
        End If
        _Interface.SendReadScan(strTrigOn, strTrigOff, iTimeOut)
        Return ""
    End Function

    Public Sub InterfaceDataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        RaiseEvent DataReceived(Pass, Result, ErrorMsg)
    End Sub


    Protected Sub GetConfigCommand()
        If Not _FileHander.FileExist(_ConfigFile) Then
            Throw New Exception("No Find file:" + _ConfigFile)
        End If
        _InterfaceConfig.DataFrameSTX = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "STX"))
        _InterfaceConfig.DataFrameEXT = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "EXT"))
        _InterfaceConfig.TrigOn = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "TrigOn"))
        _InterfaceConfig.TrigOff = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "TrigOff"))
        _InterfaceConfig.Beep = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "Beep"))
        Try
            _InterfaceConfig.RemoveString = ChangeDECtoString(_XmlHander.GetSectionInformation(_ConfigFile, "GeneralInformation", "RemoveString"))
        Catch ex As Exception
            _InterfaceConfig.RemoveString = ""
        End Try
    End Sub

    Protected Function ChangeDECtoString(ByVal mStr As String) As String
        Dim mTemp As String = String.Empty
        Try

            Dim cCmd() As String = mStr.Split(CChar(" "))
            For Each Element As String In cCmd
                If Element = "" Then Continue For
                mTemp = mTemp & Chr(CInt(Convert.ToInt32(Element, 16)))
            Next
        Catch ex As Exception
            Throw New Exception("ChangeDECtoString:" + ex.Message.ToString)
        End Try

        Return mTemp
    End Function

    Public Sub Dispose() Implements IScannerBase.Dispose
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
    Protected Overrides Sub Finalize()
        On Error Resume Next
        _Interface.Interface_Close()
        IsDisposed = True
        MyBase.Finalize()
        _Interface = Nothing
    End Sub

    Public Function SendAndReadCommand(iTimeOut As Integer, strSendCommand As String, strReadCommnd As String) As Boolean Implements IScannerBase.SendAndReadCommand
        _Interface.SendCommand(strSendCommand, strReadCommnd, iTimeOut)
        Return True
    End Function

    Public Function SendCommand(ByVal strSendCommand As String) As Boolean Implements IScannerBase.SendCommand
        _Interface.Send(strSendCommand)
        Return True
    End Function
End Class

#End Region
