Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics

Public Interface IScannerBase
    ReadOnly Property Running As Boolean
    Property ScanResult As String
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function TrigON() As Boolean
    Function TrigOFF() As Boolean
    Function Beep() As Boolean
    Function Scan(ByVal iTimeOut As Integer) As String
    Sub ContinueScan()
    Function Init(ByVal mConfig As InterfaceConfig, ByVal MySettings As Settings) As Boolean
    Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
    Sub Dispose()
End Interface

Public Class Scanner
    Implements IScanner
    Protected _iScanner As IScannerBase
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


    Public Function Init(ByVal mConfig As InterfaceConfig, ByVal MySettings As Settings) As Boolean Implements IScanner.Init
        If mConfig.Type = InterfaceType.LAN Then
            _iScanner = New Datalogic210N_Scanner()
        ElseIf mConfig.Type = InterfaceType.RS232 Then
            _iScanner = New Datalogic210N_Scanner()
        Else
            Return False
        End If
        If IsNothing(_iScanner) Then Return False
        If Not _iScanner.Init(mConfig, MySettings) Then Return False
        AddHandler _iScanner.DataReceived, AddressOf ScannerDataReceived
        Return True
    End Function

    Sub ContinueScan() Implements IScanner.ContinueScan
        If IsNothing(_iScanner) Then Return
        _iScanner.ContinueScan()
    End Sub
    Public Function Scan(ByVal iTimeOut As Integer) As String Implements IScanner.Scan
        If IsNothing(_iScanner) Then Return "No Define"
        Return _iScanner.Scan(iTimeOut)
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


Public Class Datalogic210N_Scanner
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
    Protected _sResult As String = String.Empty
    Protected _Settings As Settings
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
                If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                    Return _Interface.Status
                End If
            End If
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements IScannerBase.StatusDescription
        Get
            If Not IsNothing(_Interface) Then
                If _Interface.StatusDescription <> "" Then
                    Return _Interface.StatusDescription
                End If
            End If
            Return _StatusDescription
        End Get
    End Property

#End Region

    Public Overridable Function Init(ByVal mConfig As InterfaceConfig, ByVal MySettings As Settings) As Boolean Implements IScannerBase.Init
        _Settings = MySettings
        _InterfaceConfig = mConfig
        If _InterfaceConfig.Type = InterfaceType.LAN Then
            _Interface = New TCPInterface
            '_InterfaceConfig.EventDataReceived = True
        End If
        If _InterfaceConfig.Type = InterfaceType.RS232 Then
            _Interface = New RS232Interface
            _InterfaceConfig.EventDataReceived = True
        End If
        '    _InterfaceConfig.DataFrameSTX = Chr(enumDataFrame.STX)
        '    _InterfaceConfig.DataFrameEXT = Chr(enumDataFrame.EXT
        ' _InterfaceConfig.DataFrameSTX = ""
        '  _InterfaceConfig.DataFrameEXT = "."
        '  _InterfaceConfig.TrigOn = "T"
        '  _InterfaceConfig.TrigOff = "S"
        ' _InterfaceConfig.Beep = ""
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _Settings) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        ' _Interface.Interface_Abort()
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

    Public Overridable Sub ContinueScan() Implements IScannerBase.ContinueScan
        _Interface.ContinueRead()
    End Sub
    Public Overridable Function Scan(ByVal iTimeOut As Integer) As String Implements IScannerBase.Scan
        _Interface.SendReadScan(_InterfaceConfig.TrigOn, _InterfaceConfig.TrigOff, iTimeOut)
        Return ""
    End Function

    Public Sub InterfaceDataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        RaiseEvent DataReceived(Pass, Result, ErrorMsg)
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

End Class

