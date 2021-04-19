Imports System.IO
Imports System.IO.Ports
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics
Imports Kostal.Las.Base

Public Interface IPrinterBase
    Property ClearMaskFile As Boolean
    ReadOnly Property PrintMode As enumZebra_PrintModes
    ReadOnly Property Running As Boolean
    ReadOnly Property Status() As enumZebra_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Function GetLabelStatus() As Boolean
    Function Calibration() As Boolean
    Function ChangePrintModeTo(ByVal printMode As enumZebra_PrintModes) As Boolean
    Function SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
    Sub Dispose()
End Interface

Public Class Printer
    Implements IPrinter
    Protected _iPrinter As IPrinterBase
    Protected _DeviceType As New DeviceType
    Public Property ClearMaskFile As Boolean Implements IPrinter.ClearMaskFile
        Get
            If IsNothing(_iPrinter) Then Return False
            Return _iPrinter.ClearMaskFile
        End Get
        Set(ByVal value As Boolean)
            _iPrinter.ClearMaskFile = value
        End Set
    End Property

    Public ReadOnly Property PrintMode As enumZebra_PrintModes Implements IPrinter.PrintMode
        Get
            If IsNothing(_iPrinter) Then Return enumZebra_PrintModes.ZEBRA_PRINT_MODE_UNKNOWN
            Return _iPrinter.PrintMode
        End Get
    End Property

    Public ReadOnly Property Running As Boolean Implements IPrinter.Running
        Get
            If IsNothing(_iPrinter) Then Return False
            Return _iPrinter.Running
        End Get
    End Property

    Public ReadOnly Property Status As enumZebra_ErrorCodes Implements IPrinter.Status
        Get
            If IsNothing(_iPrinter) Then Return enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            Return _iPrinter.Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String Implements IPrinter.StatusDescription
        Get
            If IsNothing(_iPrinter) Then Return ""
            Return _iPrinter.StatusDescription
        End Get
    End Property


    Public Function GetLabelStatus() As Boolean Implements IPrinter.GetLabelStatus
        If IsNothing(_iPrinter) Then Return False
        Return _iPrinter.GetLabelStatus
    End Function

    Public Function Calibration() As Boolean Implements IPrinter.Calibration
        If IsNothing(_iPrinter) Then Return False
        Return _iPrinter.Calibration
    End Function


    Public Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IPrinter.Init

        If mType = LasDeviceType.Zebra_LAN Then
            _iPrinter = New ZebraPrinter()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Zebra_RS232 Then
            _iPrinter = New ZebraPrinter()
            _DeviceType = DeviceType.RS232
        ElseIf mType = LasDeviceType.Zebra_ZT610_LAN Then
            _iPrinter = New ZebraPrinter_ZT610()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Zebra_ZT610_RS232 Then
            _iPrinter = New ZebraPrinter_ZT610()
            _DeviceType = DeviceType.RS232

        ElseIf mType = LasDeviceType.Zebra_MACH2_LAN Then
            _iPrinter = New ZebraPrinter_MACH2()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Zebra_MACH2_RS232 Then
            _iPrinter = New ZebraPrinter_MACH2()
            _DeviceType = DeviceType.RS232


        ElseIf mType = LasDeviceType.Zebra_MACH4_LAN Then
            _iPrinter = New ZebraPrinter_MACH4()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.Zebra_MACH4_RS232 Then
            _iPrinter = New ZebraPrinter_MACH4()
            _DeviceType = DeviceType.RS232

        Else
            Return False
        End If
        If IsNothing(_iPrinter) Then Return False
        Return _iPrinter.Init(_DeviceType, mConfig, MyStation, _AppSettings, MyLanguage)
    End Function

    Public Function SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean Implements IPrinter.SendData
        If IsNothing(_iPrinter) Then Return False
        Return _iPrinter.SendData(Fields, MaskPath, MaskFile, MaskNameWithOutExtension)
    End Function

    Public Function ChangePrintModeTo(ByVal printMode As enumZebra_PrintModes) As Boolean Implements IPrinter.ChangePrintModeTo
        If IsNothing(_iPrinter) Then Return False
        Return _iPrinter.ChangePrintModeTo(printMode)
    End Function

    Public Sub Dispose() Implements IPrinter.Dispose
        If IsNothing(_iPrinter) Then Return
        _iPrinter.Dispose()
    End Sub

End Class

Public Enum enumZebra_Interface
    ZEBRA_INTERFACE_RS232 = 0
    ZEBRA_INTERFACE_TCP_IP = 1
End Enum

Public Enum enumZebra_PrintModes
    ZEBRA_PRINT_MODE_TEAR_OFF = 0
    ZEBRA_PRINT_MODE_PEEL_OFF
    ZEBRA_PRINT_MODE_REWIND
    ZEBRA_PRINT_MODE_CUTTER
    ZEBRA_PRINT_MODE_DELAYED_CUT
    ZEBRA_PRINT_MODE_APPLICATOR
    ZEBRA_PRINT_MODE_UNKNOWN
End Enum

Public Enum enumZebra_Languages
    ZEBRA_LANGUAGE_ENGLISH = 1
    ZEBRA_LANGUAGE_SPANISH
    ZEBRA_LANGUAGE_FRENCH
    ZEBRA_LANGUAGE_GERMAN
    ZEBRA_LANGUAGE_ITALIAN
    ZEBRA_LANGUAGE_NORWEGIAN
    ZEBRA_LANGUAGE_PORTUGUESE
    ZEBRA_LANGUAGE_SWEDISH
    ZEBRA_LANGUAGE_DANISH
    ZEBRA_LANGUAGE_SPANISH2
    ZEBRA_LANGUAGE_DUTCH
    ZEBRA_LANGUAGE_FINNISH
    ZEBRA_LANGUAGE_JAPANESE
End Enum

Public Enum enumZebra_ErrorCodes
    ZEBRA_ERROR_WINDOWS_ERROR = -99
    ZEBRA_ERROR_NO_ERROR = 0
    ZEBRA_ERROR_PORT_IS_ALWAYS_OPEN
    ZEBRA_ERROR_PORT_CANNOT_OPEN
    ZEBRA_ERROR_INTERFACE_IS_NOT_MSCOMM
    ZEBRA_ERROR_FILE_NOT_FOUND
    ZEBRA_ERROR_NO_RESPONSE
    ZEBRA_ERROR_INVALID_IP_ADDRESS
    ZEBRA_ERROR_INVALID_IP_PORT
    ZEBRA_ERROR_CHECK_STATUS
End Enum

Public Structure ZebraPrinterStatus
    Dim strCommSettings As String
    Dim bIsPaperOut As Boolean
    Dim bIsPaused As Boolean
    Dim nLabelLength As Integer
    Dim nNumberOfFormats As Integer
    Dim bIsBufferFulled As Boolean
    Dim bIsDiagnosticMode As Boolean
    Dim bIsPartialFormat As Boolean
    Dim strUnused As String
    Dim bIsRamCorrupt As Boolean
    Dim bIsUnderTemperature As Boolean
    Dim bIsOverTemperature As Boolean

    Dim strFunctionSettings As String
    Dim bUnused As Boolean
    Dim bIsHeadUp As Boolean
    Dim bIsRibbonOut As Boolean
    Dim bIsThermelTransferMode As Boolean
    Dim PrintMode As enumZebra_PrintModes
    Dim PrintWidthMode As Byte
    Dim bIsLabelWaitting As Boolean
    Dim nLabelsRemaining As Integer
    Dim bFormatPrinting As Boolean
    Dim nNumberOfGraphicImages As Integer

    Dim Password As String
    Dim bIsRAMInstalled As Boolean
End Structure

Public Class ZebraPrinter
    Implements IPrinterBase

    Protected IsDisposed As Boolean

#Region "Declaration"

    Protected _InterfaceMode As enumZebra_Interface
    Protected _Interface As IInterface
    Protected _InterfaceConfig As New InterfaceConfig
    Protected _InterfaceSync As New Object

    Protected _IpInterface As Socket
    Protected _IpPort As Int32 = 9100
    Protected _IpEndPoint As IPEndPoint
    Protected _IpAddress As IPAddress
    Protected _IP As String = ""
    Protected _IpValid As Boolean

    Protected _RS232_Baud As Integer
    Protected _RS232_Port As String

    Protected _ReceivedString As String = ""

    Protected TimerCB As New TimerCallback(AddressOf _TimerCB)
    Protected _Timer As New System.Threading.Timer(TimerCB, Nothing, Timeout.Infinite, Timeout.Infinite)
    Protected _TimerTick As Boolean

    Protected Const _Delimiter As String = ";"

    Protected _Status As enumZebra_ErrorCodes
    Protected _StatusDescription As String = ""


    Protected mGET_MASK_FILE As String = ""
    Protected mGET_FONT_FILE As String = ""
    Protected mGET_LABEL_STATUS As String = ""
    Protected mCLEAN_FILE As String = ""
    Protected mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR As String = ""
    Protected mPRINT_MODES(0 To 4) As String


    Protected Const mCONTROL_CODE_START_OF_TEXT As String = "^"
    Protected Const mCONTROL_CODE_START_TRANSFER As String = "^XA"
    Protected Const mCONTROL_CODE_START_EMPTYLABLE As String = "~PH"
    Protected Const mCONTROL_CODE_END_TRANSFER As String = "^XZ"

    Protected Const mCONTROL_CODE_VARIABLE_NUMBER As String = "^FN"
    Protected Const mCONTROL_CODE_START_OF_VARIABLE As String = "^FD"
    Protected Const mCONTROL_CODE_END_OF_VARIABLE_LINE As String = "^FS"

    Protected Const mCONTROL_CODE_NUMBER_OF_PRINTS As String = "^PQ"
    Protected Const mCONTROL_CODE_MASK_NAME As String = "^XF"
    Protected Const mCONTROL_CODE_MASK_NAME_OF_PRINTFILE As String = "^DF"

    Protected Const mCONTROL_CODE_FORM_FEED As String = "^PH"

    Protected Const mLABEL_WAITING_FLAG_POSITION As Long = 18

    Protected Const mCONTROL_CODE_DEFINE_LANGUAGE As String = "^KL"

    Protected Const mCONTROL_CODE_TEAR_OFF As String = "~TA"
    Protected Const mTEAR_OFF_LIMIT As Long = 120

    Protected Const mCONTROL_CODE_PRINT_MODE As String = "^MM"

    Protected Const mCONTROL_CODE_LABEL_TOP As String = "^LT"
    Protected Const mLABEL_TOP_LIMIT As Long = 120

    Protected Const mCONTROL_CODE_LABEL_SHIFT As String = "^LS"
    Protected Const mLABEL_SHIFT_LIMIT As Long = 9999

    Protected Const mCONTROL_CODE_SET_DARKNESS As String = "~SD"
    Protected Const mSET_DARKNESS_LIMIT_MIN As Long = 0
    Protected Const mSET_DARKNESS_LIMIT_MAX As Long = 30
    Protected Const mCONTROL_CODE_CALIBRATION As String = "~JC"

    Protected Const ETX As Long = 3

    Protected mNumberOfLabel As Long
    Protected mMaskFile As String = ""

    Protected mIsEnabledExtensionInterface As Boolean
    Protected mDotsPerInch As Long

    Protected mLanguage As enumZebra_Languages
    Protected mTearOffEdge As Long
    Protected mPrintMode As enumZebra_PrintModes
    Protected mLabelTop As Single   ' mm
    Protected mLabelLeft As Single   ' mm
    Protected mDarkness As Long
    Protected mMaxVariableNumber As Long
    Protected mEmptyLable As Boolean
    Protected mClearMaskFile As Boolean
    Protected mBaudRate As Integer
    Protected mOldMask As String = ""

    Protected _StopThread As Boolean = False

    Protected Delegate Function DelegateSendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
    Protected pSendDataRoutine As New DelegateSendData(AddressOf _SendData)
    Protected pPrinterCallBack As AsyncCallback = New AsyncCallback(AddressOf PrinterCallBack)

    Public Event SendDataComplete(ByVal Pass As Boolean)
    Protected _SendDataRun As Boolean
    Protected _Pass As New Boolean
    Protected _FileHandler As New FileHandler
    Private _XmlHandler As New XmlHandler
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

    Public ReadOnly Property SendDataRun() As Boolean Implements IPrinterBase.Running
        Get
            Return _SendDataRun Or _Interface.Running
        End Get
    End Property

    Public ReadOnly Property Pass() As Boolean
        Get
            Return _Pass
        End Get
    End Property

    Public Property ClearMaskFile() As Boolean Implements IPrinterBase.ClearMaskFile
        Get
            Return mClearMaskFile
        End Get
        Set(ByVal value As Boolean)
            mClearMaskFile = value
        End Set
    End Property

    Public ReadOnly Property Status() As enumZebra_ErrorCodes Implements IPrinterBase.Status
        Get
            If Not IsNothing(_Interface) Then
                '  If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return CType(_Interface.Status, enumZebra_ErrorCodes)
                ' End If
            End If
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements IPrinterBase.StatusDescription
        Get
            If Not IsNothing(_Interface) Then
                'If _Interface.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                Return _Interface.StatusDescription
                ' End If
            End If
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property MaskFile() As String
        Get
            Return mMaskFile
        End Get
    End Property

    Public Property DotsPerInch() As Long
        Get
            Return mDotsPerInch
        End Get
        Set(ByVal value As Long)
            mDotsPerInch = value
        End Set
    End Property

    Public Property NumberOfLabel() As Long
        Get
            Return mNumberOfLabel
        End Get
        Set(ByVal value As Long)
            mNumberOfLabel = value
        End Set
    End Property

    Public ReadOnly Property MaxVariableNumber() As Long
        Get
            Return mMaxVariableNumber
        End Get
    End Property

    Public ReadOnly Property Darkness() As Long
        Get
            Return mDarkness
        End Get
    End Property

    Public ReadOnly Property LabelLeft() As Single
        Get
            Return mLabelLeft
        End Get
    End Property

    Public ReadOnly Property LabelTop() As Single
        Get
            Return mLabelTop
        End Get
    End Property

    Public ReadOnly Property PrintMode() As enumZebra_PrintModes Implements IPrinterBase.PrintMode
        Get
            Return mPrintMode
        End Get
    End Property

    Public ReadOnly Property TearOffEdge() As Long
        Get
            Return mTearOffEdge
        End Get
    End Property

    Public ReadOnly Property Language() As enumZebra_Languages
        Get
            Return mLanguage
        End Get
    End Property

    Public ReadOnly Property IsEnabledExtensionInterface() As Boolean
        Get
            Return mIsEnabledExtensionInterface
        End Get
    End Property

    Public ReadOnly Property BaudRate() As Integer
        Get
            Return mBaudRate
        End Get
    End Property

#End Region

    Public Sub New()


    End Sub


    Public Overridable Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IPrinterBase.Init

        mGET_MASK_FILE = "^XA^HWR:*.*^XZ" & vbCrLf
        mGET_FONT_FILE = "^XA^HWE:*.FNT^XZ" & vbCrLf
        mGET_LABEL_STATUS = "~HS" & vbCrLf
        mCLEAN_FILE = "~JA" & vbCrLf
        'mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR = "^XA^EF^XZ" & vbCrLf & "^XA^EG^XZ" & vbCrLf
        mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR = "^XA" & vbCrLf & "^IDR:*.ZPL^FS" & vbCrLf & "^XZ" & vbCrLf

        mPRINT_MODES(0) = "T"   'TearOff
        mPRINT_MODES(1) = "P"   'PeelOff
        mPRINT_MODES(2) = "R"   'Rewind
        mPRINT_MODES(3) = "C"   'Cutter
        mPRINT_MODES(4) = "D"   'DelayedCut
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation

        mDotsPerInch = 300      'Default'
        If Not GetConfig(mConfig) Then Return False

        If mType = DeviceType.LAN Then
            _InterfaceConfig.IP = _IP
            _InterfaceConfig.Port = _IpPort
            _InterfaceConfig.DataFrameSTX = ""
            _InterfaceConfig.DataFrameEXT = Chr(&H3)
            _Interface = New TCPInterface
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = _RS232_Port
            _InterfaceConfig.BaudRate = _RS232_Baud
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _InterfaceConfig.DataFrameSTX = ""
            _InterfaceConfig.DataFrameEXT = ""
            _Interface = New RS232Interface
        End If

        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        If Not _Interface.Interface_Transmit(mCLEAN_FILE) Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

    Public Function GetConfig(ByVal mConfig As String) As Boolean
        Dim sResult As String = ""
        Dim ErrorExtensionInterface As Boolean = False

        sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "PrinterIP")
        If sResult = _FileHandler.ErrorString Then
            Return False
        Else
            _IP = sResult
        End If

        sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "Port")
        If sResult = _FileHandler.ErrorString Then
            Return False
        Else
            _RS232_Port = sResult
            If _IP <> "" Then _IpPort = CInt(sResult)
        End If

        sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "Baud")
        If sResult = _FileHandler.ErrorString Then
            Return False
        Else
            _RS232_Baud = CInt(sResult)
        End If

        Try
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "EmptyLable")
        Catch ex As Exception
            sResult = "False"
        End Try

        If Trim(sResult.ToUpper) = "TRUE" Then
            mEmptyLable = True
        Else
            mEmptyLable = False
        End If

        sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "ExternalInterface")
        If Trim(sResult.ToUpper) = "TRUE" Then
            mIsEnabledExtensionInterface = True
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "Language")
            Try
                mLanguage = CType(sResult, enumZebra_Languages)
            Catch ex As Exception
                Return True
            End Try
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "PrintMode")
            Try
                mPrintMode = CType(sResult, enumZebra_PrintModes)
            Catch ex As Exception
                Return False
            End Try
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "TearOffEdge")
            Try
                mTearOffEdge = CLng(sResult)
            Catch ex As Exception
                Return False
            End Try
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "LabelTop")
            Try
                mLabelTop = CSng(sResult)
            Catch ex As Exception
                Return False
            End Try
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "LabelLeft")
            Try
                mLabelLeft = CSng(sResult)
            Catch ex As Exception
                Return False
            End Try
        End If
        If Not ErrorExtensionInterface Then
            sResult = _XmlHandler.GetSectionInformation(mConfig, "GeneralInformation", "Darkness")
            Try
                mDarkness = CLng(sResult)
            Catch ex As Exception
                ErrorExtensionInterface = True
                Return False
            End Try
        End If
        Return True
    End Function

    Public Sub Dispose() Implements IPrinterBase.Dispose
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next
        _StopThread = True
        _Interface.Interface_Close()
        IsDisposed = True
        MyBase.Finalize()
        _Interface = Nothing

    End Sub

    Public Overridable Function SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean Implements IPrinterBase.SendData
        SyncLock _InterfaceSync
            _SendDataRun = True
            _ReceivedString = ""
            _Interface.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
            _Interface.StatusDescription = _Interface.Status.ToString
            _StatusDescription = _Status.ToString
            If Not IsNothing(Fields) Then
                For i = 0 To Fields.Length - 1
                    If IsNothing(Fields(i)) Then
                        _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
                        _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR1, i.ToString)
                        Return False
                    End If
                Next
            End If
            pSendDataRoutine.BeginInvoke(Fields, MaskPath, MaskFile, MaskNameWithOutExtension, pPrinterCallBack, Nothing)

        End SyncLock
        Return True
    End Function

    Protected Sub PrinterCallBack(ByVal Result As IAsyncResult)
        _Pass = pSendDataRoutine.EndInvoke(Result)
        _Interface.Interface_Abort()
        RaiseEvent SendDataComplete(_Pass)
        _SendDataRun = False

    End Sub

    Public Function EnableExtensionInterface(ByVal Language As enumZebra_Languages, ByVal TearOffEdge As Long, ByVal PrintMode As enumZebra_PrintModes, ByVal LabelTop As Single, ByVal LabelLeft As Single, ByVal Darkness As Long) As Boolean
        mLanguage = Language
        mTearOffEdge = TearOffEdge
        mPrintMode = PrintMode
        mLabelTop = LabelTop
        mLabelLeft = LabelLeft
        mDarkness = Darkness
        mIsEnabledExtensionInterface = True
        Return True
    End Function


    Public Function DisableExtensionInterface() As Boolean
        mIsEnabledExtensionInterface = False
        Return True
    End Function


    Protected Function MM_TO_DOT(ByVal DotsPerInch As Long, ByVal Millimeter As Single) As Long
        '1 Inch ^ 25.4mm
        Return CType(CSng(DotsPerInch) / 25.4 * Millimeter, Long)
    End Function

    Protected Sub _TimerCB(ByVal state As Object)
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        _TimerTick = True
    End Sub

    Public Function IsMaskFileAvailable(ByVal MaskNameWithOutExtension As String) As Boolean

        Dim Result As Boolean

        SyncLock _InterfaceSync

            If _SendDataRun Then
                Result = False
            Else

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
                _StatusDescription = Status.ToString

                _ReceivedString = ""

                Result = _Interface.Interface_Connect()

                If Result Then
                    Result = _IsMaskFileAvailable(MaskNameWithOutExtension)
                End If

                _Interface.Interface_Abort()

            End If

        End SyncLock

        Return Result And _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR

    End Function

    Public Function IsFontAvailable(ByVal FontNameWithOutExtension As String) As Boolean

        Dim Result As Boolean

        SyncLock _InterfaceSync

            If _SendDataRun Then
                Result = False
            Else

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
                _StatusDescription = Status.ToString

                _ReceivedString = ""

                Result = _Interface.Interface_Connect()

                If Result Then
                    Result = _IsFontAvailable_IP(FontNameWithOutExtension)
                End If

                _Interface.Interface_Abort()

            End If

        End SyncLock

        Return Result And _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR


    End Function

    Public Overridable Function GetLabelStatus() As Boolean Implements IPrinterBase.GetLabelStatus

        Dim Result As Boolean

        SyncLock _InterfaceSync

            If _SendDataRun Then
                Result = False
            Else
                _SendDataRun = True
                _Interface.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
                _Interface.StatusDescription = _Interface.Status.ToString
                _StatusDescription = Status.ToString

                _ReceivedString = ""

                Result = _Interface.Interface_Connect()

                If Result Then

                    Result = _GetLabelStatus()

                    _Interface.Interface_Abort()

                End If

            End If

        End SyncLock
        _SendDataRun = False
        Return Result And _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR

    End Function

    Public Overridable Function Calibration() As Boolean Implements IPrinterBase.Calibration
        Dim Result As Boolean

        SyncLock _InterfaceSync

            If _SendDataRun Then
                Result = False
            Else
                _SendDataRun = True
                _Interface.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
                _Interface.StatusDescription = _Interface.Status.ToString
                _StatusDescription = Status.ToString
                _ReceivedString = ""

                Result = _Interface.Interface_Connect()

                If Result Then
                    Result = _Interface.Interface_Transmit(mCONTROL_CODE_CALIBRATION & vbCrLf)
                    '   _Interface.Interface_Abort()

                End If

            End If

        End SyncLock
        _SendDataRun = False
        Return Result And _Interface.Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
    End Function
    Public Overridable Function ChangePrintModeTo(ByVal printMode As enumZebra_PrintModes) As Boolean Implements IPrinterBase.ChangePrintModeTo

        Dim Result As Boolean

        SyncLock _InterfaceSync

            If _SendDataRun Then
                Result = False
            Else
                _SendDataRun = True
                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR
                _Interface.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
                _Interface.StatusDescription = _Interface.Status.ToString
                _StatusDescription = Status.ToString
                _ReceivedString = ""

                Result = _Interface.Interface_Connect()

                If Result Then
                    Result = _Interface.Interface_Transmit(mCONTROL_CODE_START_TRANSFER & mCONTROL_CODE_PRINT_MODE & mPRINT_MODES(enumZebra_PrintModes.ZEBRA_PRINT_MODE_TEAR_OFF) & mCONTROL_CODE_END_TRANSFER & vbCrLf)
                    _Interface.Interface_Abort()

                End If

            End If

        End SyncLock
        _SendDataRun = False
        Return Result And _Interface.Status = enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR

    End Function

#Region "Common"

    Protected Overridable Function _SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
        Dim l As Integer, CompleteMaskFile As String

        If Not _Interface.Interface_Connect() Then
            Return False
        End If

        If mEmptyLable Then
            If Not _Interface.Interface_Transmit(mCONTROL_CODE_START_EMPTYLABLE & vbCrLf) Then Return False
            Return True
        End If
        If Right(MaskPath, 1) = "\" Then
            CompleteMaskFile = MaskPath & MaskFile
        Else
            CompleteMaskFile = MaskPath & "\" & MaskFile
        End If

        If Not _IsMaskFileAvailable(MaskNameWithOutExtension) Then
            If _Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                Return False
            End If

            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        ElseIf mClearMaskFile Then
            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        End If
        If Not _Interface.Interface_Transmit(mCONTROL_CODE_START_TRANSFER & vbCrLf) Then Return False


        If mNumberOfLabel = 0 Then mNumberOfLabel = 1
        If Not _Interface.Interface_Transmit(mCONTROL_CODE_NUMBER_OF_PRINTS & CStr(mNumberOfLabel) & vbCrLf) Then Return False
        If Not _Interface.Interface_Transmit(mCONTROL_CODE_MASK_NAME & UCase(MaskNameWithOutExtension) & vbCrLf) Then Return False

        If Not IsNothing(Fields) Then

            For l = Fields.GetLowerBound(0) To Fields.GetUpperBound(0)

                If Fields(l).Contains(mCONTROL_CODE_VARIABLE_NUMBER) AndAlso
                   Fields(l).Contains(mCONTROL_CODE_START_OF_VARIABLE) AndAlso
                   Fields(l).Contains(mCONTROL_CODE_END_OF_VARIABLE_LINE) Then
                    If Not _Interface.Interface_Transmit(Fields(l) & vbCrLf) Then Return False
                Else
                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_VARIABLE_NUMBER & CStr(l - Fields.GetLowerBound(0) + 1) & mCONTROL_CODE_START_OF_VARIABLE & Fields(l) & mCONTROL_CODE_END_OF_VARIABLE_LINE & vbCrLf) Then Return False
                End If

            Next l
        End If

        If Not _Interface.Interface_Transmit(mCONTROL_CODE_END_TRANSFER & vbCrLf) Then Return False

        Return True

    End Function

    Protected Overridable Function _SendMask(ByVal MaskNameWithPath As String) As Boolean
        Dim _Stream As System.IO.StreamReader, mLine As String, l As Integer, mTearOffset As String
        Dim StartSignFound As Boolean, mResults() As String, mResult As String, mLableDots As Long, ExtensionInterfaceSent As Boolean
        Dim Buffer(0) As Byte

        ExtensionInterfaceSent = False

        Try

            If Not My.Computer.FileSystem.FileExists(MaskNameWithPath) Then

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_FILE_NOT_FOUND
                _StatusDescription = _Status.ToString

                Return False
            End If

            StartSignFound = False
            mMaxVariableNumber = 0

            If mClearMaskFile Then
                ' Buffer = Encoding.ASCII.GetBytes(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                _Interface.Interface_Transmit(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _IpInterface.Send(Buffer, Buffer.Length, SocketFlags.None)
                mClearMaskFile = False
            End If

            _Stream = My.Computer.FileSystem.OpenTextFileReader(MaskNameWithPath)

            Do Until _Stream.EndOfStream
                mLine = _Stream.ReadLine
                If InStr(mLine, mCONTROL_CODE_START_TRANSFER) <> 0 Then StartSignFound = True
                If StartSignFound Then
                    If InStr(mLine, mCONTROL_CODE_MASK_NAME_OF_PRINTFILE) <> 0 Then
                        mResults = Split(mLine, "^")
                        For l = mResults.GetLowerBound(0) To mResults.GetUpperBound(0)
                            If Left(mResults(l), 2) = "DF" Then
                                mMaskFile = Replace(mResults(l), "DF", "")
                            End If
                        Next l
                    End If

                    If InStr(mLine, mCONTROL_CODE_VARIABLE_NUMBER) <> 0 Then
                        mResults = Split(mLine, "^")
                        For l = mResults.GetLowerBound(0) To mResults.GetUpperBound(0)
                            If Left(mResults(l), 2) = "FN" Then
                                mResult = Replace(mResults(l), "FN", "")
                                If IsNumeric(mResult) Then
                                    If CLng(mResult) > mMaxVariableNumber Then mMaxVariableNumber = CLng(mResult)
                                End If
                            End If
                        Next l
                    End If
                End If

                If mIsEnabledExtensionInterface And InStr(mLine, mCONTROL_CODE_END_TRANSFER) <> 0 And Not ExtensionInterfaceSent Then
                    ExtensionInterfaceSent = True

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_DEFINE_LANGUAGE & CStr(mLanguage) & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If

                    If mTearOffEdge < -mTEAR_OFF_LIMIT Then mTearOffEdge = mTEAR_OFF_LIMIT
                    If mTearOffEdge > mTEAR_OFF_LIMIT Then mTearOffEdge = mTEAR_OFF_LIMIT
                    If mTearOffEdge < 0 Then
                        mTearOffset = Format(-mTearOffEdge, "-000")
                    Else
                        mTearOffset = Format(mTearOffEdge, "000")
                    End If

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_TEAR_OFF & mTearOffset & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_PRINT_MODE & mPRINT_MODES(mPrintMode) & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If


                    mLableDots = MM_TO_DOT(mDotsPerInch, mLabelTop)
                    If mLableDots < -mLABEL_TOP_LIMIT Then mLableDots = mLABEL_TOP_LIMIT
                    If mLableDots > mLABEL_TOP_LIMIT Then mLableDots = mLABEL_TOP_LIMIT

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_LABEL_TOP & CStr(mLableDots) & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If

                    mLableDots = MM_TO_DOT(mDotsPerInch, -mLabelLeft)
                    If mLableDots < -mLABEL_SHIFT_LIMIT Then mLableDots = mLABEL_SHIFT_LIMIT
                    If mLableDots > mLABEL_SHIFT_LIMIT Then mLableDots = mLABEL_SHIFT_LIMIT

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_LABEL_SHIFT & CStr(mLableDots) & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If

                    If mDarkness < mSET_DARKNESS_LIMIT_MIN Then mDarkness = mSET_DARKNESS_LIMIT_MIN
                    If mDarkness > mSET_DARKNESS_LIMIT_MAX Then mDarkness = mSET_DARKNESS_LIMIT_MAX

                    If Not _Interface.Interface_Transmit(mCONTROL_CODE_SET_DARKNESS & CStr(mDarkness) & vbCrLf) Then
                        _Stream.Close()
                        Return False
                    End If
                End If

                If Not _Interface.Interface_Transmit(mLine & vbCrLf) Then
                    _Stream.Close()
                    Return False
                End If

            Loop

            _Stream.Close()

            Return True

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR2, _Status.ToString, ex.Message)

            Return False

        End Try

    End Function

    Protected Function _IsMaskFileAvailable(ByVal MaskNameWithOutExtension As String) As Boolean
        If mOldMask <> MaskNameWithOutExtension Then
            Return False
        End If
        Return True
        'If Not p_Interface_Transmit(mGET_MASK_FILE & vbCrLf) Then
        '    Return False
        'End If

        'If Not p_Interface_Receive() Then
        '    Return False
        'End If

        'Return InStr(UCase(_ReceivedString), UCase(MaskNameWithOutExtension) & ".ZPL") <> 0

    End Function

    Protected Function _IsFontAvailable_IP(ByVal FontNameWithOutExtension As String) As Boolean

        If Not _Interface.Interface_Transmit(mGET_FONT_FILE & vbCrLf) Then
            Return False
        End If

        If Not _Interface.Interface_Receive() Then
            Return False
        End If

        Try
            Return InStr(UCase(_ReceivedString), UCase(FontNameWithOutExtension) & ".FNT") <> 0

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR3, _Status.ToString, ex.Message)

            Return False

        End Try

    End Function


    Protected Function CheckLabelStatus() As Boolean
        Dim dResult As Boolean = False
        Dim Results() As String     ',SplitString() As String
        Dim Status As New ZebraPrinterStatus

        If PrintMode <> enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF Then
            Return True
        End If

        Try
            While True
                'Get three entries
                If Not _Interface.Interface_Transmit(mGET_LABEL_STATUS & vbCrLf) Then
                    Return False
                End If
                Do
                    If Not _Interface.Interface_Receive() Then
                        Return False
                    End If

                    Results = Split(_ReceivedString, vbCrLf)
                Loop Until Results.GetLength(0) >= 3
                If GetZebraPrinterStatus(Results, Status) Then
                    Exit While
                End If
            End While

            dResult = Status.bIsLabelWaitting

            If Status.bIsLabelWaitting Then
                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_CHECK_STATUS
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR4, _Status.ToString)
                Return False
            End If

            If Status.nNumberOfFormats <> 0 Then
                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_CHECK_STATUS
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR4, _Status.ToString)
                Return False
            End If
            Return True

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_CHECK_STATUS
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR5, _Status.ToString, ex.Message)

            Return False

        End Try

    End Function
    Protected Function _GetLabelStatus() As Boolean
        Dim dResult As Boolean = False
        Dim Results() As String     ',SplitString() As String
        Dim Status As New ZebraPrinterStatus
        Dim sw As New Stopwatch
        Try
            While True
                'Get three entries
                sw.Reset()
                sw.Start()
                If Not _Interface.Interface_Transmit(mGET_LABEL_STATUS & vbCrLf) Then
                    Return False
                End If
                Do
                    If Not _Interface.Interface_Receive(3000, "", "") Then
                        Return False
                    End If

                    Results = Split(_Interface.ReceivedString, vbCrLf)
                    If sw.ElapsedMilliseconds > 3500 Then
                        Throw New Exception(" _GetLabelStatus Time Out")
                    End If
                Loop Until Results.GetLength(0) >= 3
                sw.Stop()
                If GetZebraPrinterStatus(Results, Status) Then
                    Exit While
                End If
            End While

            dResult = Status.bIsLabelWaitting
            Return dResult
            ' Return Status.bIsLabelWaitting
            'SplitString = Split(Results(1), ",")

            'If UBound(SplitString) > 7 Then
            '    Return SplitString(7) = "1"

            'Else
            '    Return False

            'End If

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_CHECK_STATUS
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR5, _Status.ToString, ex.Message)

            Return False

        End Try

    End Function

    Protected Function GetZebraPrinterStatus(ByVal results() As String, ByRef Status As ZebraPrinterStatus) As Boolean
        ' Dim Status As New ZebraPrinterStatus
        Dim items() As String
        If results.GetLength(0) < 3 Then
            Return False
        End If

        'First Frame
        items = Split(results(0), ",")
        If items.Length < 12 Then
            Return False
        End If

        Status.strCommSettings = items(0)
        Status.bIsPaperOut = (items(1) = "1")
        Status.bIsPaused = (items(2) = "1")
        Status.nLabelLength = Integer.Parse(items(3))
        Status.nNumberOfFormats = Integer.Parse(items(4))
        Status.bIsBufferFulled = (items(5) = "1")
        Status.bIsDiagnosticMode = (items(6) = "1")
        Status.bIsPartialFormat = (items(7) = "1")
        Status.strUnused = items(8)
        Status.bIsRamCorrupt = (items(9) = "1")
        Status.bIsUnderTemperature = (items(10) = "1")
        Status.bIsOverTemperature = (items(11) = "1")
        'Second Frame
        items = Split(results(1), ",")
        If items.Length < 11 Then
            Return False
        End If

        Status.strFunctionSettings = items(0)
        Status.bUnused = (items(1) = "1")
        Status.bIsHeadUp = (items(2) = "1")
        Status.bIsRibbonOut = (items(3) = "1")
        Status.bIsThermelTransferMode = (items(4) = "1")
        Select Case Integer.Parse(items(5))
            Case 0  'rewind
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_REWIND
            Case 1  'Peel-off
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF
            Case 2  'Tear-off
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_TEAR_OFF
            Case 3  'Cutter
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_CUTTER
            Case 4  'Applicator
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_APPLICATOR
            Case Else   'Unknown
                Status.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_UNKNOWN
        End Select
        Status.PrintWidthMode = Byte.Parse(items(6))
        Status.bIsLabelWaitting = (items(7) = "1")
        Status.nLabelsRemaining = Integer.Parse(items(8))
        Status.bFormatPrinting = (items(9) = "1")
        Status.nNumberOfGraphicImages = Integer.Parse(Left(items(10), 3))
        'Third(Frame)
        'items = Split(results(2), ",")
        'If items.Length < 2 Then
        '    Return False
        'End If
        'Status.Password = items(0)
        'Status.bIsRAMInstalled = (items(1) = "1")

        Return True

    End Function

#End Region
End Class

Public Class ZebraPrinter_ZT610
    Inherits ZebraPrinter
    Protected ListData As New List(Of String)
    Protected Overrides Function _SendMask(ByVal MaskNameWithPath As String) As Boolean
        Dim _Stream As System.IO.StreamReader, mLine As String
        Dim Buffer(0) As Byte

        Try

            If Not My.Computer.FileSystem.FileExists(MaskNameWithPath) Then

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_FILE_NOT_FOUND
                _StatusDescription = _Status.ToString

                Return False
            End If

            mMaxVariableNumber = 0

            If mClearMaskFile Then
                ' Buffer = Encoding.ASCII.GetBytes(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _Interface.Interface_Transmit(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _IpInterface.Send(Buffer, Buffer.Length, SocketFlags.None)
                mClearMaskFile = False
            End If

            _Stream = My.Computer.FileSystem.OpenTextFileReader(MaskNameWithPath)
            ListData.Clear()
            Do Until _Stream.EndOfStream
                mLine = _Stream.ReadLine
                ListData.Add(mLine)
            Loop

            _Stream.Close()

            Return True

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR2, _Status.ToString, ex.Message)

            Return False

        End Try
    End Function

    Protected Overrides Function _SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
        Dim l As Integer, CompleteMaskFile As String

        If Not _Interface.Interface_Connect() Then
            Return False
        End If

        If mEmptyLable Then
            Return True
        End If
        If Right(MaskPath, 1) = "\" Then
            CompleteMaskFile = MaskPath & MaskFile
        Else
            CompleteMaskFile = MaskPath & "\" & MaskFile
        End If

        If Not _IsMaskFileAvailable(MaskNameWithOutExtension) Then
            If _Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                Return False
            End If

            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        ElseIf mClearMaskFile Then
            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        End If
        If mNumberOfLabel = 0 Then mNumberOfLabel = 1
        If Not IsNothing(Fields) Then
            For l = 0 To Fields.Count - 1
                ChangeFiled(l + 1, Fields(l))
            Next l
        End If

        For i = 0 To ListData.Count - 1
            If Not _Interface.Interface_Transmit(ListData(i) & vbCrLf) Then Return False
        Next

        Return True
    End Function


    Public Sub ChangeFiled(ByVal iIndex As Integer, ByVal strValue As String)
        For i = iIndex To ListData.Count - 1
            Dim mTitle As String = "^FN" + iIndex.ToString
            If ListData(i).Contains(mTitle) And ListData(i).Contains("^FS") Then
                If strValue.IndexOf("^FN") >= 0 Then
                    ListData(i) = ListData(i).Substring(0, ListData(i).IndexOf(mTitle) + mTitle.Length) + strValue + "^FS"
                Else
                    ListData(i) = ListData(i).Substring(0, ListData(i).IndexOf(mTitle)) + strValue + "^FS"
                End If

            End If
        Next
    End Sub
End Class


Public Class ZebraPrinter_MACH2
    Inherits ZebraPrinter
    Protected ListData As New List(Of String)
    Protected Overrides Function _SendMask(ByVal MaskNameWithPath As String) As Boolean
        Dim _Stream As System.IO.StreamReader, mLine As String
        Dim Buffer(0) As Byte

        Try

            If Not My.Computer.FileSystem.FileExists(MaskNameWithPath) Then

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_FILE_NOT_FOUND
                _StatusDescription = _Status.ToString

                Return False
            End If

            mMaxVariableNumber = 0

            If mClearMaskFile Then
                ' Buffer = Encoding.ASCII.GetBytes(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _Interface.Interface_Transmit(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _IpInterface.Send(Buffer, Buffer.Length, SocketFlags.None)
                mClearMaskFile = False
            End If

            _Stream = My.Computer.FileSystem.OpenTextFileReader(MaskNameWithPath)
            ListData.Clear()
            Do Until _Stream.EndOfStream
                mLine = _Stream.ReadLine
                ListData.Add(mLine)
            Loop

            _Stream.Close()

            Return True

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR2, _Status.ToString, ex.Message)

            Return False

        End Try
    End Function

    Protected Overrides Function _SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
        Dim l As Integer, CompleteMaskFile As String

        If Not _Interface.Interface_Connect() Then
            Return False
        End If

        If mEmptyLable Then
            Return True
        End If
        If Right(MaskPath, 1) = "\" Then
            CompleteMaskFile = MaskPath & MaskFile
        Else
            CompleteMaskFile = MaskPath & "\" & MaskFile
        End If

        If Not _SendMask(CompleteMaskFile) Then
            Return False
        End If
        If mNumberOfLabel = 0 Then mNumberOfLabel = 1
        If Not IsNothing(Fields) Then
            For l = 0 To Fields.Count - 1
                ChangeFiled(l + 1, Fields(l))
            Next l
        End If

        For i = 0 To ListData.Count - 1
            If Not _Interface.Interface_Transmit(ListData(i) & vbCrLf) Then Return False
        Next

        Return True
    End Function


    Public Sub ChangeFiled(ByVal iIndex As Integer, ByVal strValue As String)
        For i = iIndex To ListData.Count - 1 Step 1
            If ListData(i).Contains("CONTENT" + iIndex.ToString) Then
                ListData(i) = ListData(i).Replace("CONTENT" + iIndex.ToString, strValue)
            End If
        Next

    End Sub

    Public Overrides Function GetLabelStatus() As Boolean
        Return False
    End Function
    Public Overrides Function ChangePrintModeTo(printMode As enumZebra_PrintModes) As Boolean
        Return True
    End Function
    Public Overrides Function Calibration() As Boolean
        Return True
    End Function
End Class



Public Class ZebraPrinter_MACH4
    Inherits ZebraPrinter
    Protected ListData As New List(Of String)
    Protected Overrides Function _SendMask(ByVal MaskNameWithPath As String) As Boolean
        Dim _Stream As System.IO.StreamReader, mLine As String
        Dim Buffer(0) As Byte

        Try

            If Not My.Computer.FileSystem.FileExists(MaskNameWithPath) Then

                _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_FILE_NOT_FOUND
                _StatusDescription = _Status.ToString

                Return False
            End If

            mMaxVariableNumber = 0

            If mClearMaskFile Then
                ' Buffer = Encoding.ASCII.GetBytes(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _Interface.Interface_Transmit(mCONTROL_CODE_SEND_MASK_FILE_WITH_CLEAR & vbCrLf)
                ' _IpInterface.Send(Buffer, Buffer.Length, SocketFlags.None)
                mClearMaskFile = False
            End If

            _Stream = My.Computer.FileSystem.OpenTextFileReader(MaskNameWithPath)
            ListData.Clear()
            Do Until _Stream.EndOfStream
                mLine = _Stream.ReadLine
                ListData.Add(mLine)
            Loop

            _Stream.Close()

            Return True

        Catch ex As Exception
            _Status = enumZebra_ErrorCodes.ZEBRA_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_ZEBRA_ERROR2, _Status.ToString, ex.Message)

            Return False

        End Try
    End Function

    Protected Overrides Function _SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
        Dim l As Integer, CompleteMaskFile As String

        If Not _Interface.Interface_Connect() Then
            Return False
        End If

        If mEmptyLable Then
            Return True
        End If
        If Right(MaskPath, 1) = "\" Then
            CompleteMaskFile = MaskPath & MaskFile
        Else
            CompleteMaskFile = MaskPath & "\" & MaskFile
        End If

        If Not _IsMaskFileAvailable(MaskNameWithOutExtension) Then
            If _Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                Return False
            End If

            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        ElseIf mClearMaskFile Then
            If Not _SendMask(CompleteMaskFile) Then
                Return False
            End If
            mOldMask = MaskNameWithOutExtension
        End If
        If mNumberOfLabel = 0 Then mNumberOfLabel = 1
        If Not IsNothing(Fields) Then
            For l = 0 To Fields.Count - 2
                ChangeFiled(l + 1, Fields(l))
            Next l
            ChangeBarcodeFiled(Fields.GetUpperBound(0), Fields(Fields.GetUpperBound(0)))
        End If

        For i = 0 To ListData.Count - 1
            If Not _Interface.Interface_Transmit(ListData(i) & vbCrLf) Then Return False
        Next

        Return True
    End Function

    Public Sub ChangeFiled(ByVal iIndex As Integer, ByVal strValue As String)
        For i = ListData.Count - 1 To 0 Step -1
            If ListData(i).IndexOf("Text" + iIndex.ToString + ";") > 0 Then
                Dim cData() As String = ListData(i).Split(CChar(";"))
                cData(2) = strValue
                ListData(i) = cData(0) + ";" + cData(1) + ";" + cData(2)
            End If
        Next
    End Sub

    Public Sub ChangeBarcodeFiled(ByVal iIndex As Integer, ByVal strValue As String)
        For i = ListData.Count - 1 To 0 Step -1
            If ListData(i).IndexOf("Barcode1;") > 0 Then
                Dim cData() As String = ListData(i).Split(CChar(";"))
                cData(2) = strValue
                ListData(i) = cData(0) + ";" + cData(1) + ";" + cData(2)
            End If
        Next
    End Sub

    Public Overrides Function GetLabelStatus() As Boolean
        Return False
    End Function
    Public Overrides Function ChangePrintModeTo(printMode As enumZebra_PrintModes) As Boolean
        Return True
    End Function
    Public Overrides Function Calibration() As Boolean
        Return True
    End Function
End Class
