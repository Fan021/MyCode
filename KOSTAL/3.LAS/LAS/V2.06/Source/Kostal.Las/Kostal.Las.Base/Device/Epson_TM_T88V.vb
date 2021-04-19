Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing

Public Interface IFailPrinterBase
    ReadOnly Property Running As Boolean
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function Printer(ByVal Lines As Collection) As Boolean
    Function Cut() As Boolean
    Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Sub Dispose()
End Interface

Public Class FailPrinter
    Implements IFailPrinter
    Protected _iFailPrinter As IFailPrinterBase
    Protected _DeviceType As New DeviceType

    ReadOnly Property Running As Boolean Implements IFailPrinter.Running
        Get
            If IsNothing(_iFailPrinter) Then Return False
            Return _iFailPrinter.Running
        End Get
    End Property


    ReadOnly Property Status As enumDevice_ErrorCodes Implements IFailPrinter.Status
        Get
            If IsNothing(_iFailPrinter) Then Return enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            Return _iFailPrinter.Status
        End Get
    End Property

    ReadOnly Property StatusDescription As String Implements IFailPrinter.StatusDescription
        Get
            If IsNothing(_iFailPrinter) Then Return ""
            Return _iFailPrinter.StatusDescription
        End Get
    End Property


    Public Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IFailPrinter.Init
        If mType = LasDeviceType.Epson_TM_T88V_LAN Then
            _iFailPrinter = New Epson_TM_T88V()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Epson_TM_T88V_RS232 Then
            _iFailPrinter = New Epson_TM_T88V()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.Epson_TM_T88V_USB Then
            _iFailPrinter = New Epson_TM_T88V()
            _DeviceType = DeviceType.USB
        Else
            Return False
        End If
        If IsNothing(_iFailPrinter) Then Return False
        If Not _iFailPrinter.Init(_DeviceType, mConfig, MyStation, _AppSettings, MyLanguage) Then Return False
        Return True
    End Function

    Public Function Printer(ByVal Lines As Collection) As Boolean Implements IFailPrinter.Printer
        If IsNothing(_iFailPrinter) Then Return False
        Return _iFailPrinter.Printer(Lines)
    End Function


    Public Function Cut() As Boolean Implements IFailPrinter.Cut
        If IsNothing(_iFailPrinter) Then Return False
        Return _iFailPrinter.Cut()
    End Function

    Public Sub Dispose() Implements IFailPrinter.Dispose
        If IsNothing(_iFailPrinter) Then Return
        _iFailPrinter.Dispose()
    End Sub

End Class

Public Class Epson_TM_T88V
    Implements IFailPrinterBase

#Region "Declarations"
    Protected IsDisposed As Boolean
    Public _Interface As IInterface
    Protected _InterfaceConfig As New InterfaceConfig
    Protected _USBContent As String = ""
    Protected _Status As enumDevice_ErrorCodes
    Protected _StatusDescription As String = ""
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

    Public ReadOnly Property Running() As Boolean Implements IFailPrinterBase.Running
        Get
            Return _Interface.Running
        End Get
    End Property


    Public ReadOnly Property Status() As enumDevice_ErrorCodes Implements IFailPrinterBase.Status
        Get
            If Not IsNothing(_Interface) Then
                'If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return _Interface.Status
                'End If
            End If
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements IFailPrinterBase.StatusDescription
        Get
            If Not IsNothing(_Interface) Then
                '   If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return _Interface.StatusDescription
                '   End If
            End If
            Return _StatusDescription
        End Get
    End Property

#End Region

    Public Overridable Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IFailPrinterBase.Init
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        If mConfig.Split(CChar(",")).Length <> 2 Then
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

        If mType = DeviceType.USB Then
            _Interface = New USBInterface_Printer
        End If

        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

    Public Overridable Function Printer(ByVal Lines As Collection) As Boolean Implements IFailPrinterBase.Printer
        Dim _StrPrinter As String = ""
        For i = 1 To Lines.Count
            _StrPrinter = _StrPrinter + Lines(i).ToString + vbCrLf
        Next
        Try
            _Interface.Send(_StrPrinter)
        Catch ex As Exception
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_EPSON_EEROR, ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function Cut() As Boolean Implements IFailPrinterBase.Cut
        Try
            _Interface.Send(Encoding.ASCII.GetString(New Byte() {&H1B, &H40, &H1B, &H69, &H1}))
        Catch ex As Exception
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_EPSON_EEROR, ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Sub Dispose() Implements IFailPrinterBase.Dispose
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

