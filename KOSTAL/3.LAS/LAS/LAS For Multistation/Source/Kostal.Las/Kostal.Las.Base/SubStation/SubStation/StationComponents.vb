Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

Public Enum ProductionMode
    ProductionMode
    RetestMode
    SelfResistance
    MasterPart
    AssemblyOnly
    ClearMode
    UserDefined
    StandAlone
End Enum


Public Interface IStationPanel
    ReadOnly Property Panel As Panel
    ReadOnly Property StepID As Label
    ReadOnly Property Msg As Label

End Interface

Public Interface IScheduleUI
    Inherits IStationPanel
    ReadOnly Property ScheduleList As ComboBox
    ReadOnly Property ScheduleData As DataGridView
    ReadOnly Property OKButton As Button
    ReadOnly Property ResetButton As Button
    ReadOnly Property ScheduleTitle As Label
    ReadOnly Property ScheduleName As Label

    Event ScheduleChangeTo(ByVal IndicatedName As String, ByVal IgnorePassword As Boolean)

    Event AbortScheduleChange()

    Event ComboxScheduleChangeTo(ByVal ID As String)

End Interface

Public Interface IScannerUI
    Inherits IStationPanel
    ReadOnly Property TrigON As Button
    ReadOnly Property TrigOFF As Button
    ReadOnly Property Result As TextBox
    ReadOnly Property DataList As DataGridView

End Interface

Public Interface IPrinterUI
    Inherits IStationPanel
    ReadOnly Property Print As Button
    ReadOnly Property Count As TextBox
    ReadOnly Property DataList As DataGridView

End Interface


Public Interface INewPartUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface


Public Interface IMesUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface ILineControlUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IUpdateReferenceUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IManualScannerUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface
Public Interface IForcamUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IUserDefineUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface ILaserUI
    Inherits IStationPanel
    ReadOnly Property OK As Button
    ReadOnly Property Start As Button
    ReadOnly Property Cmd As ComboBox
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IFlashUI
    Inherits IStationPanel
    ReadOnly Property OK As Button
    ReadOnly Property Cmd As ComboBox
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IPackmanUI
    Inherits IStationPanel

End Interface

Public Interface IFailPrinterUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
    ReadOnly Property TextCmd As TextBox
    ReadOnly Property OK As Button
End Interface

Public Interface ICAQUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView

End Interface

Public Interface IPLCAlarmUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView

End Interface

Public Interface IManualUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IShowPicUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IMulitPrinterUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface ICounterUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface ISaveProductionUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface


Public Interface IReferencesUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IReTestUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IManualReTestUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IArticleUI
    Inherits IStationPanel

    ReadOnly Property DataList As DataGridView
End Interface

Public Interface ISNUI
    Inherits IStationPanel
    ReadOnly Property DataList As DataGridView
End Interface

Public Interface IUserDefine
    ReadOnly Property ErrorMsg As String
    ReadOnly Property ErrorCode As String
End Interface

Public Interface IScannerDefine
    Inherits IUserDefine
    Function CheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface IScannerDeviceDefine
    Inherits IUserDefine
    Function GetScannerName(ByVal _i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef strDeviceName As String, ByRef ScannerType As enumScanType) As enumScannerDeviceType
End Interface

Public Interface IUserStationDefine
    Inherits IUserDefine
    Function GetStationName(ByVal _i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef strStationName As String) As Boolean
End Interface
Public Enum enumScannerDeviceType
    AutoSelect = 1
    ManualSelect
End Enum

Public Interface IPrinterDefine
    Inherits IUserDefine
    Function GetAllFieldsOfPrintFile(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
End Interface

Public Interface IGetMulitSNDefine
    Inherits IUserDefine
    Function GetAllFieldsOfMulitSN(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
End Interface

Public Interface IRunMulitSNDefine
    Inherits IUserDefine
    Function RunAllFieldsOfMulitSN(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal fileds As String()) As Boolean
End Interface

Public Interface IReprintMulitDefine
    Inherits IUserDefine
    Function ReprintFields(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal SN As String) As Boolean
End Interface

Public Interface ISerialNoGeneratorDefine
    Inherits IUserDefine
    Function CreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
End Interface

Public Interface ILaserDefine
    Inherits IUserDefine
    Function GetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef mCmd As String) As Boolean
End Interface

Public Interface IFlashDefine
    Inherits IUserDefine
    Function GetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fildeds As String()) As Boolean
End Interface

Public Interface IFailPrinterDefine
    Inherits IUserDefine
    Function GetFailCollection(ByVal _i As Station, ByVal WT As WT, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Collection
End Interface

Public Interface IManualScanDefine
    Inherits IUserDefine
    Function GetBarcode(ByVal _i As Station, ByVal mScannerResult As String, ByRef _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface IShowPicDefine
    Inherits IUserDefine
    Function GetAllFieldsOfFileName(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
End Interface

Public Interface IVariantInfoDefine
    Inherits IUserDefine
    Function GetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean
End Interface


Public Interface IReTestDefine
    Inherits IUserDefine
    Function ReTest(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface IManualReTestMsgDefine
    Inherits IUserDefine
    Function GetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal PLC_OUT_WT As WT, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface IManualScannerMsgDefine
    Inherits IUserDefine
    Function GetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface


Public Interface IScannerCommandDefine
    Inherits IUserDefine
    Function GetCommand(ByVal _i As Station, ByRef strTrigOnCmd As String, ByRef strTrigOffCmd As String, ByRef iTimeOut As Integer, ByVal iRepeat As Integer, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface ILineControlStationDefine
    Inherits IUserDefine
    Function GetStation(ByVal _i As Station, ByRef strPreviousStation As String, ByRef strCurrentStation As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface
Public Interface IManualReTestChangeScheduleDefine
    Inherits IUserDefine
    Function ChangeSchedule(ByVal _i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface ILineControlDefine
    Inherits IUserDefine
    Function LineControlDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef _Listchild As Dictionary(Of String, ChildElement)) As Boolean
End Interface

Public Interface IBeforeStepDefine
    Inherits IUserDefine
    Function StepDefine(ByVal _i As Station, ByVal Logger As Logger, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface IAfterStepDefine
    Inherits IUserDefine
    Function StepDefine(ByVal _i As Station, ByVal Logger As Logger, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Interface ICheckTrigInfo
    Inherits IUserDefine
    Function CheckTrigInfoAndSelectLocalArticle(ByVal _i As Station, ByVal LocalArticle As Article, ByVal TrigSignal As Dictionary(Of String, Object), ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As SelectLocalArticleType
End Interface

Public Interface ITwicatRun
    Inherits IUserDefine
    Function TwicatRun(ByVal _i As Station, ByVal TC As TwinCatAds, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
End Interface

Public Class SubStationCfg
    Public Station As String = String.Empty
    Public Name As String = String.Empty
    Public Inteface As StationType
    Public Enable As Boolean = False
    Public Type As LasDeviceType = LasDeviceType.NONE
    Public TypeName As String = String.Empty
    Public Config As String = String.Empty
    Public Repeat As Integer = 0
    Public MainDevice As String = String.Empty
    Public PLCName As String = String.Empty
    Public Scanner As String = String.Empty
    Public ScannerType As enumScanType = enumScanType.Manual
    Public Printer As String = String.Empty
    Public FailPrinter As String = String.Empty
    Public Laser As String = String.Empty
    Public LineControl As String = String.Empty
    Public CAQ As String = String.Empty
    Public SaveProduction As String = String.Empty
    Public Counter As String = String.Empty
    Public SN As String = String.Empty
    Public AdsInput As New List(Of String)
    Public AdsOutput As New List(Of String)
    Public Packman As String = String.Empty
End Class

Public Enum enumScanType
    Manual
    Auto
End Enum

Public Enum StationType
    Schedule = 0
    Reference
    NewPart
    Article
    Scanner
    ManualScanner
    Printer
    Laser
    Flash
    QGW
    QGW_ASSM
    QGW_Finish
    FailPrinter
    Manual
    ShowPic
    Counter
    ReTest
    ManualReTest
    UserDefine
    UpdateReference
    SaveProduction
    SN
    CAQ
    PLCAlarm
    MulitPrinter
    ForCam
    UserDefineQGW
    MES
    Packman
    NONE
End Enum

Public Class StationElement
    Protected _Name As String = String.Empty
    Protected _SubStation As New Dictionary(Of String, SubStationCfg)
    Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Property SubStation As Dictionary(Of String, SubStationCfg)
        Get
            Return _SubStation
        End Get
        Set(ByVal value As Dictionary(Of String, SubStationCfg))
            _SubStation = value
        End Set
    End Property

    Public Sub Clear()
        _Name = ""
        _SubStation.Clear()
    End Sub
End Class

Public Class StationCfg
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _FileHandler As FileHandler
    Protected _XmlHandler As XmlHandler
    Protected mSupportType As String = String.Empty
    Protected _StationListElement As New Dictionary(Of String, StationElement)
    Public Const Name As String = "_mStationCfg"
    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language)
        AppSettings = _AppSettings
        _Language = MyLanguage
        _FileHandler = New FileHandler
        _XmlHandler = New XmlHandler
    End Sub

    Property StationListElement As Dictionary(Of String, StationElement)
        Get
            Return _StationListElement
        End Get
        Set(ByVal value As Dictionary(Of String, StationElement))
            _StationListElement = value
        End Set
    End Property

    Public Function Init() As Boolean
        If Not LoadStationConfig() Then Return False
        If Not CheckStationCfg() Then Return False
        Return True
    End Function

    Private Function LoadStationConfig() As Boolean
        Dim _MainDevice As String = ""
        Dim _cMainDevice() As String
        _StationListElement = _XmlHandler.GetSubStationCfgs(AppSettings.ConfigFolder, AppSettings.ConfigName)

        For Each _StationElement As StationElement In _StationListElement.Values
            For Each _SubStionElement As SubStationCfg In _StationElement.SubStation.Values
                If AppSettings.PLCConfig.Count = 1 Then
                    If _SubStionElement.PLCName = "" Then
                        _SubStionElement.PLCName = AppSettings.PLCConfig(AppSettings.PLCConfig.Keys(0)).Name
                    End If
                End If
                If _SubStionElement.MainDevice <> "" Then
                    _MainDevice = _SubStionElement.MainDevice
                    _cMainDevice = _MainDevice.Split(CChar(","))
                    For Each _cMainDeviceElement As String In _cMainDevice
                        SetMainDevice(_SubStionElement, _cMainDeviceElement)
                    Next
                End If
            Next
        Next
        Return True
    End Function

    Private Function SetMainDevice(ByVal SubStationCfg As SubStationCfg, ByVal MainDevice As String) As Boolean
        For Each _StationElement As StationElement In _StationListElement.Values
            For Each _SubStionElement As SubStationCfg In _StationElement.SubStation.Values
                If _SubStionElement.Name = MainDevice Then
                    Select Case SubStationCfg.Inteface
                        Case StationType.Scanner, StationType.UserDefine
                            If SubStationCfg.Type = LasDeviceType.Keyence_LAN Or SubStationCfg.Type = LasDeviceType.Keyence_RS232 Then
                                _SubStionElement.ScannerType = enumScanType.Auto
                            Else
                                _SubStionElement.ScannerType = enumScanType.Manual
                            End If
                            _SubStionElement.Scanner = SubStationCfg.Name
                            Return True
                        Case StationType.Printer
                            _SubStionElement.Printer = SubStationCfg.Name
                            Return True
                        Case StationType.QGW, StationType.QGW_ASSM, StationType.QGW_Finish, StationType.UserDefineQGW
                            _SubStionElement.LineControl = SubStationCfg.Name
                            Return True
                        Case StationType.FailPrinter
                            _SubStionElement.FailPrinter = SubStationCfg.Name
                            Return True
                        Case StationType.Counter
                            _SubStionElement.Counter = SubStationCfg.Name
                            Return True
                        Case StationType.Packman
                            _SubStionElement.Packman = SubStationCfg.Name
                            Return True
                        Case StationType.SN
                            _SubStionElement.SN = SubStationCfg.Name
                            Return True
                        Case StationType.CAQ
                            _SubStionElement.CAQ = SubStationCfg.Name
                            Return True
                        Case StationType.SaveProduction
                            _SubStionElement.SaveProduction = SubStationCfg.Name
                            Return True
                        Case Else
                            Throw New Exception("Not support Owner:" + MainDevice)
                            Return False
                    End Select
                End If
            Next
        Next
        Throw New Exception("Not support Owner:" + MainDevice)
        Return False
    End Function

    Public Function HasStation(ByVal eStationType As StationType) As Boolean
        For Each _StationElement As StationElement In _StationListElement.Values
            For Each _SubStationElement As SubStationCfg In _StationElement.SubStation.Values
                If _SubStationElement.Inteface = eStationType Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
    Private Function CheckStationCfg() As Boolean
        For Each _StationElement As StationElement In _StationListElement.Values
            For Each _SubStationElement As SubStationCfg In _StationElement.SubStation.Values
                If (_SubStationElement.AdsInput.Count > 0 Or _SubStationElement.AdsOutput.Count > 0) And _SubStationElement.PLCName = "" Then
                    Throw New Exception("SubStation:" + _SubStationElement.Name + " PLC Name is Null. Please Add PLC Name")
                    Return False
                End If

                If _SubStationElement.PLCName <> "" And Not AppSettings.PLCConfig.ContainsKey(_SubStationElement.PLCName) Then
                    Throw New Exception("SubStation:" + _SubStationElement.Name + " Not Support PLC Name:" + _SubStationElement.PLCName)
                    Return False
                End If
                If _SubStationElement.Type = LasDeviceType.NONE Then
                    Throw New Exception("Not Support Las Device Type: " + _SubStationElement.TypeName + " Name: " + _SubStationElement.Name + vbCrLf + "Support Device Type is:" + GetSupprotLasType())
                    Return False
                End If

                If _SubStationElement.Inteface = StationType.NONE Then
                    Throw New Exception("Not Support Interface: " + _SubStationElement.Inteface.ToString + vbCrLf + " Support Type is:" + GetSupprotLasType())
                    Return False
                End If
                'If _SubStationElement.MainDevice = "" And _SubStationElement.AdsInput.Count = 0 And _SubStationElement.AdsOutput.Count = 0 And _SubStationElement.Inteface = StationType.ShowPic And _SubStationElement.Inteface = StationType.Scanner And _SubStationElement.Inteface = StationType.Printer And _SubStationElement.Inteface = StationType.FailPrinter And _SubStationElement.Inteface = StationType.QGW And _SubStationElement.Inteface = StationType.SN And _SubStationElement.Inteface = StationType.Laser And _SubStationElement.Inteface = StationType.Counter Then
                '    Throw New Exception("Not Trig Ads or Other Station." + _SubStationElement.Name)
                '    Return False
                'End If

            Next
        Next
        Return True
    End Function

    Public Shared Function ChangeStringToLasType(ByVal Type As String) As LasDeviceType
        For Each TypeElemet As LasDeviceType In [Enum].GetValues(GetType(LasDeviceType))
            If [Enum].GetName(GetType(LasDeviceType), TypeElemet) = Type Then
                Return TypeElemet
            End If
        Next
        Return LasDeviceType.NONE
    End Function

    Public Shared Function ChangeStringToStationType(ByVal Type As String) As StationType
        For Each TypeElemet As StationType In [Enum].GetValues(GetType(StationType))
            If [Enum].GetName(GetType(StationType), TypeElemet) = Type Then
                Return TypeElemet
            End If
        Next
        Return StationType.NONE
    End Function

    Public Function GetSupprotLasType() As String
        mSupportType = ""
        For Each TypeElemet In [Enum].GetValues(GetType(LasDeviceType))
            mSupportType = mSupportType + vbCrLf + "    " + [Enum].GetName(GetType(LasDeviceType), TypeElemet)
        Next
        Return mSupportType
    End Function

    Public Function GetInteface() As String
        mSupportType = ""
        For Each TypeElemet In [Enum].GetValues(GetType(StationType))
            mSupportType = mSupportType + vbCrLf + "    " + [Enum].GetName(GetType(StationType), TypeElemet)
        Next
        Return mSupportType
    End Function

    Public Shared Function ChangeTypeToInface(ByVal Type As LasDeviceType) As StationType

        Select Case Type
            Case 0 To CType(19, LasDeviceType)
                Return StationType.Scanner
            Case CType(20, LasDeviceType) To CType(29, LasDeviceType)
                Return StationType.Printer
            Case CType(30, LasDeviceType) To CType(34, LasDeviceType)
                Return StationType.Laser
            Case CType(35, LasDeviceType) To CType(39, LasDeviceType)
                Return StationType.Flash
            Case CType(40, LasDeviceType) To CType(49, LasDeviceType)
                Return StationType.FailPrinter

            Case CType(50, LasDeviceType) To CType(100, LasDeviceType)
                Return ChangeStringToStationType(Type.ToString)

            Case Else
                Return StationType.NONE

        End Select
        Return StationType.NONE
    End Function

End Class

Public Enum ShowMsgType
    ShowSN = 0
    ShowWaiting
    ShowDefine
    ReTestDefine
    ShowLineControl
    ShowSNFail
    ShowFail
    ShowPass
    TakeFail
    TakeDefineFail
    Scaner
    ShowWaiting2
End Enum

Public Enum SelectLocalArticleType
    AutoSelect
    UserDefineSelect
    SelectFail
End Enum

Public Interface IStationTypeBase
    ReadOnly Property IsMasterError As Boolean
    ReadOnly Property UI As Panel
    ReadOnly Property SubStationCfg As SubStationCfg
    ReadOnly Property LocalArticle As Article
    ReadOnly Property LockArticle As Boolean
    ReadOnly Property AppArticle As Article
    Property ManualRun As Boolean
    Function Init() As Boolean
    Function ReLoadLanguage() As Boolean
    Sub Run()
    Sub Dispose()

End Interface
Public MustInherit Class StationTypeBase

    Implements IStationTypeBase

    Protected IsDisposed As Boolean

    Protected _i As Station
    Protected _UI As IStationPanel
    Protected _Logger As Logger
    Protected _Messager As New Messager
    Protected AppSettings As New Settings
    Protected _SubStationCfg As SubStationCfg
    Protected _Language As Language
    Protected _LocalArticle As Article
    Protected _AppArticle As Article
    Protected _FileHandler As New FileHandler
    Protected _xmlHandler As New XmlHandler
    Protected _FirstPulse As Boolean
    Protected _FirstFlag As Boolean
    Protected _ManualOffPulse As Boolean
    Protected _ManualFlag As Boolean
    Protected _ManualMode As Boolean
    Protected _ManualRefresh As Boolean
    Protected _InternPass As Boolean
    Protected _InternFail As Boolean
    Protected _InternMsg As String
    Protected _isRun As Boolean = False
    Protected _Current As String = ""
    Protected _Last As String = ""
    Protected _StepMsg As String = ""
    Protected _Devices As New Dictionary(Of String, Object)
    Protected _Stations As New Dictionary(Of String, IStationTypeBase)
    Protected _TrigSignal As New Dictionary(Of String, Object)
    Protected _StepInfo As New Dictionary(Of Integer, Object)
    Protected _object As Object = ""
    Protected _StationMode As Integer
    Protected _ReadStructRequestAction As StructRequestAction
    Protected _WriteStructResponseAction As StructResponseAction
    Protected _ManualReadStructRequestAction As StructRequestAction
    Protected _ManualWriteStructResponseAction As StructResponseAction
    Protected _ManualRun As Boolean
    Protected _ReadStructDeviceInteraction As StructDeviceInteraction
    Protected _ManualReadStructDeviceInteraction As StructDeviceInteraction
    Protected _isCallBackRunning As Boolean
    Protected _isCallBackResult As Boolean
    Protected _StartCallBack As Boolean
    Protected _LockArticle As Boolean
    Protected _BeforStepLine As IBeforeStepDefine
    Protected _AfterStepLine As IAfterStepDefine
    Protected _CheckTrigInfo As ICheckTrigInfo
    Protected _isBeforStepLineRunning As Boolean
    Protected _StartBeforStepLineCallBack As Boolean
    Protected Delegate Function dBeforeStepDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pBeforeStepDefine As New dBeforeStepDefine(AddressOf _BeforeStepDefine)
    Protected pBeforeStepDefineCB As AsyncCallback = New AsyncCallback(AddressOf _BeforeStepDefineCB)
    Protected _isAfterStepDefineRunning As Boolean
    Protected _StartAfterStepDefineCallBack As Boolean
    Protected _isAfterStepDefineResult As Boolean
    Protected _isBeforStepDefineResult As Boolean
    Protected _isAfterStepDefineStart As Boolean
    Protected _isBeforStepDefineStart As Boolean
    Protected Delegate Function dAfterStepDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pAfterStepDefine As New dAfterStepDefine(AddressOf _AfterStepDefine)
    Protected pAfterStepDefineCB As AsyncCallback = New AsyncCallback(AddressOf _AfterStepDefineCB)
    Protected _isCheckTrigInfoDefineRunning As Boolean
    Protected _StartCheckTrigInfoDefineCallBack As Boolean
    Protected _LastVariantInfo As StructVariantInfo
    Protected _StartCheckTrigInfoDefineResult As SelectLocalArticleType
    Protected Delegate Function dCheckTrigInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal TrigSignal As Dictionary(Of String, Object), ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As SelectLocalArticleType
    Protected pCheckTrigInfoDefine As New dCheckTrigInfo(AddressOf _CheckTrigInfoDefine)
    Protected pCheckTrigInfoDefineCB As AsyncCallback = New AsyncCallback(AddressOf _CheckTrigInfoDefineCB)


#Region "Properties"

    Public Property ReadStructRequestAction As StructRequestAction
        Set(ByVal value As StructRequestAction)
            _ReadStructRequestAction = value
        End Set
        Get
            Return _ReadStructRequestAction
        End Get
    End Property

    Public ReadOnly Property WriteStructResponseAction As StructResponseAction
        Get
            Return _WriteStructResponseAction
        End Get
    End Property

    Public Property ManualReadStructRequestAction As StructRequestAction
        Set(ByVal value As StructRequestAction)
            _ManualReadStructRequestAction = value
        End Set
        Get
            Return _ManualReadStructRequestAction
        End Get
    End Property

    Public ReadOnly Property ManualWriteStructResponseAction As StructResponseAction
        Get
            Return _ManualWriteStructResponseAction
        End Get
    End Property

    Public Property ReadStructDeviceInteraction As StructDeviceInteraction
        Set(ByVal value As StructDeviceInteraction)
            _ReadStructDeviceInteraction = value
        End Set
        Get
            Return _ReadStructDeviceInteraction
        End Get
    End Property

    Public Property ManualReadStructDeviceInteraction As StructDeviceInteraction
        Set(ByVal value As StructDeviceInteraction)
            _ManualReadStructDeviceInteraction = value
        End Set
        Get
            Return _ManualReadStructDeviceInteraction
        End Get
    End Property

    Public ReadOnly Property UI As Panel Implements IStationTypeBase.UI
        Get
            Return _UI.Panel
        End Get
    End Property

    Public Property isRun As Boolean

        Set(ByVal value As Boolean)
            SyncLock _object
                _isRun = value
            End SyncLock
        End Set
        Get
            SyncLock _object
                Return _isRun
            End SyncLock
        End Get

    End Property

    Public ReadOnly Property IsMasterError As Boolean Implements IStationTypeBase.IsMasterError
        Get
            Return _i.IsMasterError
        End Get
    End Property

    Public ReadOnly Property SubStationCfg As SubStationCfg Implements IStationTypeBase.SubStationCfg
        Get
            Return _SubStationCfg
        End Get
    End Property

    Public ReadOnly Property LocalArticle As Article Implements IStationTypeBase.LocalArticle
        Get
            Return _LocalArticle
        End Get
    End Property

    Public ReadOnly Property AppArticle As Article Implements IStationTypeBase.AppArticle
        Get
            Return _AppArticle
        End Get
    End Property

    Public Property ManualRun As Boolean Implements IStationTypeBase.ManualRun
        Set(ByVal value As Boolean)
            _ManualRun = value
        End Set
        Get
            Return _ManualRun
        End Get
    End Property

    Public ReadOnly Property LockArticle As Boolean Implements IStationTypeBase.LockArticle
        Get
            Return _LockArticle
        End Get
    End Property

#End Region


    Public Sub New(ByVal StationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal BeforStepLine As IBeforeStepDefine, ByVal AfterStepLine As IAfterStepDefine)
        _Devices = Devices
        _Stations = Stations
        AppSettings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)
        _AppArticle = CType(Devices(Article.Name), Article)
        _SubStationCfg = StationCfg
        _i = New Station(_SubStationCfg.Name)
        _Logger = New Logger(AppSettings)
        _ReadStructRequestAction = New StructRequestAction
        _WriteStructResponseAction = New StructResponseAction
        _ManualReadStructRequestAction = New StructRequestAction
        _ManualWriteStructResponseAction = New StructResponseAction
        _ReadStructDeviceInteraction = New StructDeviceInteraction
        _ManualReadStructDeviceInteraction = New StructDeviceInteraction
        _BeforStepLine = BeforStepLine
        _AfterStepLine = AfterStepLine
        _LocalArticle = New Article(_i, AppSettings, _Language)
        _LocalArticle.Init()
        _i.BeforeStepNumber = 0
        _i.AfterInputNumber = 0

    End Sub



    Public MustOverride Sub Run() Implements IStationTypeBase.Run
    Public MustOverride Function Init() As Boolean Implements IStationTypeBase.Init
    Public MustOverride Function ReLoadLanguage() As Boolean Implements IStationTypeBase.ReLoadLanguage
    Public MustOverride Sub Dispose() Implements IStationTypeBase.Dispose

    Protected Function CheckStepLine() As Boolean
        If _isBeforStepLineRunning Or _isAfterStepDefineRunning Then
            Return False
        End If

        If Not _isBeforStepDefineResult And _isBeforStepDefineStart Then
            _isBeforStepDefineStart = False
            _Logger.ThrowerNoStation(_i, _Messager, "BeforStepDefine Fail. Message:" + _BeforStepLine.ErrorMsg)
        End If
        If Not _isAfterStepDefineResult And _isAfterStepDefineStart Then
            _isAfterStepDefineStart = False
            _Logger.ThrowerNoStation(_i, _Messager, "AfterStepLine Fail. Message:" + _AfterStepLine.ErrorMsg)
        End If
        Return True
    End Function
    Protected Function UpdateMsg(ByVal strStation As String) As Boolean
        _i.Toggle = _i.StepOutputNumber <> _i.StepInputNumber
        _i.StepOutputNumber = _i.StepInputNumber
        If _LocalArticle IsNot Nothing Then
            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> "" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data <> "" Then
                _i.StepTextLine = "[" & _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data & "]" + "[" & _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data & "]Step " & CStr(_i.StepOutputNumber)
            Else
                _i.StepTextLine = "Step " & CStr(_i.StepOutputNumber)
            End If
        End If

        If _UI.StepID IsNot Nothing Then _UI.StepID.Text = _i.StepOutputNumber.ToString

        If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
            _StepMsg = _Language.Read(strStation, _i.StepOutputNumber.ToString)
            If _StepMsg <> FileHandler.s_DEFAULT And _StepMsg <> FileHandler.s_Null Then
                _Logger.LoggerNoStepTextLine(_i, _Messager, _StepMsg)
            End If
        End If
        Return True
    End Function

    Protected Function BeforeLine() As Boolean
        If _i.BeforeStepNumber <> _i.StepOutputNumber Then
            If Not IsNothing(_BeforStepLine) Then
                _isBeforStepLineRunning = True
                _isBeforStepDefineStart = True
                _i.BeforeStepNumber = _i.StepOutputNumber
                pBeforeStepDefine.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, pBeforeStepDefineCB, Nothing)
                Return False
            End If
        End If
        Return True
    End Function

    Protected Function AfterLine() As Boolean
        If _i.AfterInputNumber <> _i.StepOutputNumber Then
            If Not IsNothing(_AfterStepLine) Then
                _isAfterStepDefineRunning = True
                _isAfterStepDefineStart = True
                _i.AfterInputNumber = _i.StepOutputNumber
                pAfterStepDefine.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, pAfterStepDefineCB, Nothing)
                Return False
            End If
        End If
        Return True
    End Function

#Region "CheckPLCInfo"
    Protected Overridable Function CheckStructRequestActionPLCInfo() As Boolean
        '执行委托
        If Not _StartCheckTrigInfoDefineCallBack Then
            If Not IsNothing(_CheckTrigInfo) Then
                _isCheckTrigInfoDefineRunning = True
                _StartCheckTrigInfoDefineCallBack = True
                pCheckTrigInfoDefine.BeginInvoke(_i, _LocalArticle, _TrigSignal, _Devices, _Stations, pCheckTrigInfoDefineCB, Nothing)
            Else
                _StartCheckTrigInfoDefineResult = SelectLocalArticleType.AutoSelect
                _isCheckTrigInfoDefineRunning = False
                _StartCheckTrigInfoDefineCallBack = True
            End If
        End If

        If _StartCheckTrigInfoDefineCallBack And Not _isCheckTrigInfoDefineRunning Then
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.AutoSelect Then
                If _StationMode = 1 Then
                    If CheckStructRequestActionPLCInfoMode1() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
                If _StationMode = 2 Then
                    If CheckStructRequestActionPLCInfoMode2() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
                If _StationMode = 3 Then
                    If CheckStructRequestActionPLCInfoMode3() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
            End If
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.UserDefineSelect Then
                _i.StepInputNumber = _i.StepOutputNumber + 1
            End If
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.SelectFail Then
                _Logger.Logger(_i, _Messager, "CheckTrigInfo.CheckTrigInfoAndSelectLocalArticle Fail. Message:" + _CheckTrigInfo.ErrorMsg)
                _i.StepInputNumber = _i.Address_Fail
            End If
        End If
        Return True
    End Function

    Protected Overridable Function CheckStructDeviceInteractionPLCInfo() As Boolean
        '执行委托
        If Not _StartCheckTrigInfoDefineCallBack Then
            If Not IsNothing(_CheckTrigInfo) Then
                _isCheckTrigInfoDefineRunning = True
                _StartCheckTrigInfoDefineCallBack = True
                pCheckTrigInfoDefine.BeginInvoke(_i, _LocalArticle, _TrigSignal, _Devices, _Stations, pCheckTrigInfoDefineCB, Nothing)
            Else
                _StartCheckTrigInfoDefineResult = SelectLocalArticleType.AutoSelect
                _isCheckTrigInfoDefineRunning = False
                _StartCheckTrigInfoDefineCallBack = True
            End If
        End If

        '等待委托
        If _StartCheckTrigInfoDefineCallBack And Not _isCheckTrigInfoDefineRunning Then
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.AutoSelect Then
                If _StationMode = 1 Then
                    If CheckStructDeviceInteractionPLCInfoMode1() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
                If _StationMode = 2 Then
                    If CheckStructDeviceInteractionPLCInfoMode2() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
                If _StationMode = 3 Then
                    If CheckStructDeviceInteractionPLCInfoMode3() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If

                If _StationMode = 4 Then
                    If CheckStructDeviceInteractionPLCInfoMode4() Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                End If
            End If
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.UserDefineSelect Then
                _i.StepInputNumber = _i.StepOutputNumber + 1
            End If
            If _StartCheckTrigInfoDefineResult = SelectLocalArticleType.SelectFail Then
                _InternMsg = "CheckTrigInfo.CheckTrigInfoAndSelectLocalArticle Fail. Message:" + _CheckTrigInfo.ErrorMsg
                _Logger.Logger(_i, _Messager, "CheckTrigInfo.CheckTrigInfoAndSelectLocalArticle Fail. Message:" + _CheckTrigInfo.ErrorMsg)
                _i.StepInputNumber = _i.Address_Fail
            End If
        End If
        Return True
    End Function

    Protected Overridable Function CheckStructRequestActionPLCInfoMode1() As Boolean
        If _ReadStructRequestAction.stuPlcArticleSet.strSerialNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ReadStructRequestAction.stuPlcArticleSet.strSerialNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ReadStructRequestAction.stuPlcArticleSet.strSerialNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If
        _Logger.Logger(_i, _Messager, "PLC SN:" + _ReadStructRequestAction.stuPlcArticleSet.strSerialNr)

        If _ReadStructRequestAction.stuPlcArticleSet.strKostalNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ReadStructRequestAction.stuPlcArticleSet.strKostalNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ReadStructRequestAction.stuPlcArticleSet.strKostalNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If

        If _ReadStructRequestAction.bulDoNegativeAction And _ReadStructRequestAction.bulDoPositiveAction Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SIGNAL))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SIGNAL)
            _i.StepInputNumber = _i.Address_Fail
        End If

        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _ReadStructRequestAction.stuPlcArticleSet.strKostalNr Then
            If Not _LocalArticle.GetArticle_FromID(_ReadStructRequestAction.stuPlcArticleSet.strKostalNr) Then
                _i.StepInputNumber = _i.Address_Fail
                Return False
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ReadStructRequestAction.stuPlcArticleSet.strSerialNr
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = _ReadStructRequestAction.stuPlcArticleSet.strUserDefine
        _LockArticle = True
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_CHECK, "Successful"))
        Return True
    End Function

    Protected Overridable Function CheckStructRequestActionPLCInfoMode2() As Boolean
        If _ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If
        _Logger.Logger(_i, _Messager, "Manual SN:" + _ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr)

        If _ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If

        If _ManualReadStructRequestAction.bulDoNegativeAction And _ManualReadStructRequestAction.bulDoPositiveAction Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SIGNAL))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SIGNAL)
            _i.StepInputNumber = _i.Address_Fail
        End If

        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr Then
            If Not _LocalArticle.GetArticle_FromID(_ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr) Then
                _i.StepInputNumber = _i.Address_Fail
                Return False
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = _ManualReadStructRequestAction.stuPlcArticleSet.strUserDefine
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_CHECK, "Successful"))
        Return True
    End Function


    Protected Overridable Function CheckStructRequestActionPLCInfoMode3() As Boolean
        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
            If Not _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data) Then
                _i.StepInputNumber = _i.Address_Fail
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
        Return True
    End Function

    Protected Overridable Function CheckStructDeviceInteractionPLCInfoMode1() As Boolean
        If _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If
        _Logger.Logger(_i, _Messager, "PLC SN:" + _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr)

        If _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If

        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr Then
            If Not _LocalArticle.GetArticle_FromID(_ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr) Then
                _i.StepInputNumber = _i.Address_Fail
                Return False
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine

        _LockArticle = True
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_CHECK, "Successful"))
        Return True
    End Function

    Protected Overridable Function CheckStructDeviceInteractionPLCInfoMode2() As Boolean
        If _ManualReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ManualReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_SERIAL, _ManualReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If
        _Logger.Logger(_i, _Messager, "Manual SN:" + _ManualReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr)

        If _ManualReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = "" Then
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ManualReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr))
            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_ARTICLE, _ManualReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr)
            _i.StepInputNumber = _i.Address_Fail
            Return False
        End If

        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _ManualReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr Then
            If Not _LocalArticle.GetArticle_FromID(_ManualReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr) Then
                _i.StepInputNumber = _i.Address_Fail
                Return False
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ManualReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = _ManualReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PLC_CHECK, "Successful"))
        Return True
    End Function


    Protected Overridable Function CheckStructDeviceInteractionPLCInfoMode3() As Boolean
        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
            If Not _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data) Then
                _i.StepInputNumber = _i.Address_Fail
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
        Return True
    End Function

    Protected Overridable Function CheckStructDeviceInteractionPLCInfoMode4() As Boolean
        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _LastVariantInfo.strKostalNr Then
            If Not _LocalArticle.GetArticle_FromID(_LastVariantInfo.strKostalNr) Then
                _i.StepInputNumber = _i.Address_Fail
            End If
        End If
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _LastVariantInfo.strSerialNr
        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = _LastVariantInfo.strUserDefine
        Return True
    End Function
#End Region

#Region "StructDeviceInteraction"
    Protected Overridable Function UpdateStructDeviceInteractionPassStep1() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructDeviceInteractionPassStep1Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructDeviceInteractionPassStep1Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionPassStep1Mode1() As Boolean
        _ReadStructDeviceInteraction.bulAdsActionIsPass = True
        _ReadStructDeviceInteraction.bulAdsActionIsFail = False
        _ReadStructDeviceInteraction.strAdsActionValue = ""
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionPassStep1Mode2() As Boolean
        _ManualReadStructDeviceInteraction.bulAdsActionIsPass = True
        _ManualReadStructDeviceInteraction.bulAdsActionIsFail = False
        _ManualReadStructDeviceInteraction.strAdsActionValue = ""
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function


    Protected Overridable Function UpdateStructDeviceInteractionPassStep2() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructDeviceInteractionPassStep2Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructDeviceInteractionPassStep2Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionPassStep2Mode1() As Boolean
        If _ReadStructDeviceInteraction.bulAdsActionIsPass = False And _ReadStructDeviceInteraction.bulAdsActionIsFail = False And _ReadStructDeviceInteraction.bulPlcDoAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _LockArticle = False
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionPassStep2Mode2() As Boolean
        If _ManualReadStructDeviceInteraction.bulAdsActionIsPass = False And _ManualReadStructDeviceInteraction.bulAdsActionIsFail = False And _ManualReadStructDeviceInteraction.bulPlcDoAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function


    Protected Overridable Function UpdateStructDeviceInteractionFailStep1() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructDeviceInteractionFailStep1Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructDeviceInteractionFailStep1Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionFailStep1Mode1() As Boolean
        _ReadStructDeviceInteraction.bulAdsActionIsPass = False
        _ReadStructDeviceInteraction.bulAdsActionIsFail = True
        _ReadStructDeviceInteraction.strAdsActionValue = _InternMsg
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionFailStep1Mode2() As Boolean
        _ManualReadStructDeviceInteraction.bulAdsActionIsPass = False
        _ManualReadStructDeviceInteraction.bulAdsActionIsFail = True
        _ManualReadStructDeviceInteraction.strAdsActionValue = _InternMsg
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function


    Protected Overridable Function UpdateStructDeviceInteractionFailStep2() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructDeviceInteractionFailStep2Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructDeviceInteractionFailStep2Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionFailStep2Mode1() As Boolean
        If _ReadStructDeviceInteraction.bulAdsActionIsPass = False And _ReadStructDeviceInteraction.bulAdsActionIsFail = False And _ReadStructDeviceInteraction.bulPlcDoAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _LockArticle = False
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

    Protected Overridable Function UpdateStructDeviceInteractionFailStep2Mode2() As Boolean
        If _ManualReadStructDeviceInteraction.bulAdsActionIsPass = False And _ManualReadStructDeviceInteraction.bulAdsActionIsFail = False And _ManualReadStructDeviceInteraction.bulPlcDoAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

#End Region

#Region "StructResponseAction"
    Protected Overridable Function UpdateStructResponseActionPassStep1() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructResponseActionPassStep1Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructResponseActionPassStep1Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructResponseActionPassStep1Mode1() As Boolean
        _WriteStructResponseAction.bulActionIsPass = True
        _WriteStructResponseAction.bulActionIsFail = False
        _WriteStructResponseAction.strActionResultText = ""
        _WriteStructResponseAction.bulPartReceived = True
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Overridable Function UpdateStructResponseActionPassStep1Mode2() As Boolean
        _ManualWriteStructResponseAction.bulActionIsPass = True
        _ManualWriteStructResponseAction.bulActionIsFail = False
        _ManualWriteStructResponseAction.strActionResultText = ""
        _ManualWriteStructResponseAction.bulPartReceived = True
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function


    Protected Overridable Function UpdateStructResponseActionPassStep2() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructResponseActionPassStep2Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructResponseActionPassStep2Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructResponseActionPassStep2Mode1() As Boolean
        If _WriteStructResponseAction.bulActionIsPass = False And _WriteStructResponseAction.bulActionIsFail = False And _WriteStructResponseAction.bulPartReceived = False And _ReadStructRequestAction.bulDoPositiveAction = False And _ReadStructRequestAction.bulDoNegativeAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _LockArticle = False
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

    Protected Overridable Function UpdateStructResponseActionPassStep2Mode2() As Boolean
        If _ManualWriteStructResponseAction.bulActionIsPass = False And _ManualWriteStructResponseAction.bulActionIsFail = False And _ManualWriteStructResponseAction.bulPartReceived = False And _ManualReadStructRequestAction.bulDoPositiveAction = False And _ManualReadStructRequestAction.bulDoNegativeAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function


    Protected Overridable Function UpdateStructResponseActionFailStep1() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructResponseActionFailStep1Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructResponseActionFailStep1Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructResponseActionFailStep1Mode1() As Boolean
        _WriteStructResponseAction.bulActionIsPass = False
        _WriteStructResponseAction.bulActionIsFail = True
        _WriteStructResponseAction.strActionResultText = _InternMsg
        _WriteStructResponseAction.bulPartReceived = True
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Overridable Function UpdateStructResponseActionFailStep1Mode2() As Boolean
        _ManualWriteStructResponseAction.bulActionIsPass = False
        _ManualWriteStructResponseAction.bulActionIsFail = True
        _ManualWriteStructResponseAction.strActionResultText = _InternMsg
        _ManualWriteStructResponseAction.bulPartReceived = True
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function


    Protected Overridable Function UpdateStructResponseActionFailStep2() As Boolean
        If _StationMode = 1 Then
            Return UpdateStructResponseActionFailStep2Mode1()
        End If
        If _StationMode = 2 Then
            Return UpdateStructResponseActionFailStep2Mode2()
        End If
        _Logger.ThrowerNoStation(_i, "Invalid StationMode")
        Return False
    End Function

    Protected Overridable Function UpdateStructResponseActionFailStep2Mode1() As Boolean
        If _WriteStructResponseAction.bulActionIsPass = False And _WriteStructResponseAction.bulActionIsFail = False And _WriteStructResponseAction.bulPartReceived = False And _ReadStructRequestAction.bulDoPositiveAction = False And _ReadStructRequestAction.bulDoNegativeAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _LockArticle = False
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

    Protected Overridable Function UpdateStructResponseActionFailStep2Mode2() As Boolean
        If _ManualWriteStructResponseAction.bulActionIsPass = False And _ManualWriteStructResponseAction.bulActionIsFail = False And _ManualWriteStructResponseAction.bulPartReceived = False And _ManualReadStructRequestAction.bulDoPositiveAction = False And _ManualReadStructRequestAction.bulDoNegativeAction = False Then
            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
            _i.StepInputNumber = _i.Address_Home
        End If
        Return True
    End Function

#End Region
    Protected Sub CleanLocalArticleElement()
        For Each element As ArticleElement In _LocalArticle.ArticleElements.Values
            _LocalArticle.ArticleElements(element.Key).Data = ""
        Next
    End Sub

    Function _BeforeStepDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _BeforStepLine.StepDefine(_i, _Logger, LocalArticle, Devices, Stations)
    End Function

    Protected Sub _BeforeStepDefineCB(ByVal Result As IAsyncResult)
        _isBeforStepDefineResult = pBeforeStepDefine.EndInvoke(Result)
        _isBeforStepLineRunning = False
    End Sub

    Function _AfterStepDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _AfterStepLine.StepDefine(_i, _Logger, LocalArticle, Devices, Stations)
    End Function

    Protected Sub _AfterStepDefineCB(ByVal Result As IAsyncResult)
        _isAfterStepDefineResult = pAfterStepDefine.EndInvoke(Result)
        _isAfterStepDefineRunning = False
    End Sub

    Function _CheckTrigInfoDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal TrigSignal As Dictionary(Of String, Object), ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As SelectLocalArticleType
        Return _CheckTrigInfo.CheckTrigInfoAndSelectLocalArticle(_i, LocalArticle, TrigSignal, Devices, Stations)
    End Function

    Protected Sub _CheckTrigInfoDefineCB(ByVal Result As IAsyncResult)
        _StartCheckTrigInfoDefineResult = pCheckTrigInfoDefine.EndInvoke(Result)
        _isCheckTrigInfoDefineRunning = False
    End Sub
End Class
