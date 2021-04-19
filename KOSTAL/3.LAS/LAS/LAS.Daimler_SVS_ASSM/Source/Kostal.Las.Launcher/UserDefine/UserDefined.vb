'======================================================
'
'"User_Article_Interface_V0"  Module
'
'Date:2015.07.03
'
'======================================================
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices

Imports prjSerialNoGenerator
Imports Kostal.Las.Base
Imports Kostal.Las.ArticleProvider

<Assembly: AssemblyTitle("LAS OF Daimler SVS ")>
<Assembly: AssemblyDescription("LAS OF Daimler SVS")>
<Assembly: AssemblyCompany("KOSTAL CHINA")>
<Assembly: AssemblyProduct("Daimler SVS")>
<Assembly: AssemblyCopyright("Copyright ? KOSTAL 2017")>
<Assembly: AssemblyTrademark("")>

Public Module User_Defined_Interface
    '====================================================================================================
    'User-defined Area
    'DO NOT change the class name(CustomSerialNoGenerator) but you can change its context.
    'The class is just working with StationTyp_NewPart
    'Please change it according to the situations of your project.
    '====================================================================================================
    Public Structure LK_SN_CONSTANTS
        Public Const DELIMITER As Char = "/"c
        Public Const PREFIX_LK_NUMBER As String = "/3OS"
        Public Const PREFIX_SERIAL_NUMBER As String = "/SN"
        Public Const PREFIX_CUSTOMER_NUMBER As String = "/P"
        Public Const PREFIX_HW_INDEX As String = "/HW"                    'Harware Index
        Public Const PREFIX_SW_INDEX As String = "/SW"                    'Software Index
        Public Const PREFIX_AS_INDEX As String = "/AS"
        Public Const PREFIX_AI_INDEX As String = "/AI"
        Public Const PREFIX_ZI_INDEX As String = "/ZI"                    'Drawing Index
        Public Const PREFIX_QIE As String = "/KS"                         'Quality Index
        Public Const PREFIX_ME_INDEX As String = "/ME"                    'Mechanic Index
        Public Const PREFIX_CUSTOMER_COLOR_CODE As String = "/CC"
        Public Const PREFIX_CUSTOMER_SERIAL_NUMBER As String = "/CSN"
        Public Const PREFIX_NGS As String = "/Y"
    End Structure

    Public Interface ISerialNoGeneratorBase
        ReadOnly Property ErrorMsg As String
        ReadOnly Property ErrorCode As String
        Function CreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
    End Interface

    Public Enum enumSerialNoProvider
        LK_STANDARD_SN = 1
        CUSTOMER_DEFINED_SN = 2
    End Enum

    Public Class SerialNoGeneratorDefine
        Implements ISerialNoGeneratorDefine

        Private _ISerialNoGeneratorBase As ISerialNoGeneratorBase

        Sub New()
            _ISerialNoGeneratorBase = New LK_STANDARD_SN
            '_ISerialNoGeneratorBase = New CustomSerialNoGenerator
        End Sub

        Public Function CreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String Implements ISerialNoGeneratorDefine.CreateSerialNo
            If IsNothing(_ISerialNoGeneratorBase) Then Return ""
            Return _ISerialNoGeneratorBase.CreateSerialNo(_i, Settings, LocalArticle, Devices, Stations)
        End Function

        Public ReadOnly Property ErrorMsg As String Implements ISerialNoGeneratorDefine.ErrorMsg
            Get
                If IsNothing(_ISerialNoGeneratorBase) Then Return ""
                Return _ISerialNoGeneratorBase.ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements ISerialNoGeneratorDefine.ErrorCode
            Get
                If IsNothing(_ISerialNoGeneratorBase) Then Return ""
                Return _ISerialNoGeneratorBase.ErrorCode
            End Get
        End Property

    End Class



    Public Class LK_STANDARD_SN
        Implements ISerialNoGeneratorBase

        Private _SerialNoGenerator As clsSerialGenerator
        Private _ErrorMsg As String
        Private _ErrorCode As String
        Public ReadOnly Property ErrorMsg As String Implements ISerialNoGeneratorBase.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements ISerialNoGeneratorBase.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Sub New()
            _SerialNoGenerator = New prjSerialNoGenerator.clsSerialGenerator()
            _SerialNoGenerator.lSerialMode = prjSerialNoGenerator.eSerialMode.sg_36System_5_0
        End Sub

        Public Function CreateSerialNo(ByVal _i As Base.Station, ByVal Settings As Settings, ByVal LocalArticle As Base.Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String Implements ISerialNoGeneratorBase.CreateSerialNo
            '=======================产生SN===============================
            _SerialNoGenerator.sArticleNo = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
            _SerialNoGenerator.sTraceID = Settings.MachineIdentifier.TraceId
            _SerialNoGenerator.CreateSerialNo()
            Return _SerialNoGenerator.sSerialNo
        End Function


    End Class

    Public Class CustomSerialNoGenerator
        Implements ISerialNoGeneratorBase

        '=======================产生非标SN===============================
        Private _SerialNoGenerator As New clsSerialGenerator
        Private _enumGenerateMode As eSerialMode
        Private _nCounterLength As Short
        Private _sSerialNo As String
        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _LocalArticle As Article

        'Private _bUseLkStandardSn As Boolean
        Private _Setting As Settings

        Public ReadOnly Property GenerateSerialNumberMode As eSerialMode
            Get
                Return _enumGenerateMode
            End Get
        End Property

        Public ReadOnly Property CounterLength As Short
            Get
                Return _nCounterLength
            End Get
        End Property

        Public ReadOnly Property sSerialNo As String
            Get
                Return _sSerialNo
            End Get
        End Property
        Public ReadOnly Property ErrorMsg As String Implements ISerialNoGeneratorBase.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements ISerialNoGeneratorBase.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        'Optional ByVal eSerialMode As eSerialMode = eSerialMode.sg_36System_5_0, Optional ByVal nCounterLength As Short = 8
        Sub New()

            _enumGenerateMode = eSerialMode.sg_36System
            _nCounterLength = 8

            _SerialNoGenerator.lSerialMode = _enumGenerateMode
            '   _SerialNoGenerator.sArticleNo = AppArticle.Compoment(KostalArticleKeys.KEY_ID).Data
            '  _SerialNoGenerator.sTraceID = Machine_Identifier.TraceId
            '_SerialNoGenerator.nCounterLength = _nCounterLength

        End Sub

        Private Function GetPrefixOfSerialNumber() As String
            Dim strStarter As String = "" ' "T"
            Dim strLineMachineNr As String = Mid(_Setting.MachineIdentifier.LineId, 2, 1)
            Dim strShiftNr As String = "A"
            'Select Case ST01_NewPart.CurrentShift
            '    Case 1
            '        strShiftNr = "A"
            '    Case 2
            '        strShiftNr = "B"
            '    Case 3
            '        strShiftNr = "C"
            '    Case Else
            '        strShiftNr = "D"
            'End Select
            Dim strYYDDD As String = Format(Now, "yy").ToString + Now.DayOfYear.ToString("D03")
            Dim strBatchCode As String = "A" '"@"

            Return strStarter & strLineMachineNr & strShiftNr & strYYDDD & strBatchCode & ReturnHexTraceID(CInt(_Setting.MachineIdentifier.TraceId))


        End Function

        Private Function GetSuffixOfSerialNumber() As String

            Return String.Empty

        End Function

        Private Function Init() As Boolean
            _SerialNoGenerator.sArticleNo = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
            _SerialNoGenerator.sTraceID = _Setting.MachineIdentifier.TraceId
            Return True
        End Function

        Public Function CreateSerialNo(ByVal _i As Base.Station, ByVal Settings As Settings, ByVal LocalArticle As Base.Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String Implements ISerialNoGeneratorBase.CreateSerialNo
            _LocalArticle = LocalArticle
            _Setting = Settings
            Init()
            _SerialNoGenerator.CreateSerialNo()
            _sSerialNo = GetPrefixOfSerialNumber() & Right(_SerialNoGenerator.sSerialNo, 4) & GetSuffixOfSerialNumber()
            If _sSerialNo.Length <> 16 Then _sSerialNo = ""
            Return _sSerialNo
        End Function

        Private Function ReturnHexTraceID(ByVal intTraceID As Integer) As String
            ''生成十六进制TraceID
            Dim strRes As String = ""
            Dim thebit(0 To 3) As String
            For i As Integer = 3 To 0 Step -1
                Dim temp As Integer = CInt(Int(intTraceID / (Math.Pow(36, i))))
                If temp > 0 And temp <= 9 Then
                    thebit(i) = temp.ToString
                    intTraceID = CInt(intTraceID - Math.Pow(36, i) * temp)
                ElseIf temp > 9 Then
                    thebit(i) = Chr(temp + 55).ToString
                    intTraceID = CInt(intTraceID - Math.Pow(36, i) * temp)
                Else
                    thebit(i) = "0"
                End If
            Next

            For i As Integer = 3 To 0 Step -1
                strRes = strRes & thebit(i)
            Next
            Return strRes
        End Function

    End Class

    Public Class ScannerDefine
        Implements IScannerDefine
        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _SN As String
        Private _Customer As String
        Private _Article As String
        Private _ArticleIndex As String
        Private _Index As String
        Private _PCBANumber As String
        Private _SW As String
        Private _HW As String
        Private _Color As String
        Private articlesw As String
        Private articlehw As String
        Private articleindex As String
        Private _TC As TwinCatAds
        Private _PLC_stuDataStore1 As StructRequestAction
        Private _LineControl As LineControlStation
        Private _FileHandler As New FileHandler
        Public Shared DoQuery As String = "DoQuery"
        Public Shared SR751_01 As String = "SR751_01"
        Public Shared SR751_02 As String = "SR751_02"
        Public Shared SR751_03 As String = "SR751_03"
        Public Shared SR751_04 As String = "SR751_04"
        Public Shared SR1000_01 As String = "SR1000_01"
        Public Shared SR1000_02 As String = "SR1000_02"
        Public Shared SR1000_03 As String = "SR1000_03"
        Public Shared SR752_01 As String = "SR752_01"
        Public Shared SR752_02 As String = "SR752_02"
        Public Shared ManualScanner01 As String = "ManualScanner01"
        Public Shared ManualScanner02 As String = "ManualScanner02"
        Private FileHander As New FileHandler
        Private _ADS_stuStoreData As StructDeviceInteraction
        Private _mAppSettings As Settings
        Public ReadOnly Property ErrorMsg As String Implements IScannerDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IScannerDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function CheckScannerResult(ByVal i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements IScannerDefine.CheckScannerResult
            Try
                _ErrorMsg = ""
                Select Case i.Name
                    Case ScannerDefine.ManualScanner01
                        '等待确认条码更新程序
                        Return True

                    Case ScannerDefine.SR751_01, ScannerDefine.SR751_02
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        _TC = CType(Devices("PLC20"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN01") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN01")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN01", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If

                        Return True

                    Case ScannerDefine.SR751_03
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        _TC = CType(Devices("PLC30"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN02") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN02")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN02", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If

                        Return True

                    Case ScannerDefine.SR751_04
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        '查询及写LineControl
                        If _LocalArticle.ArticleElements("T_SEAT_ADJ_FUNC").Data.Substring(0, 2) <> "WO" Then
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                            _LineControl = CType(Stations("QGW03"), LineControlStation)
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                            _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                            Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                            Loop
                            If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                                _LineControl.ReadStructRequestAction.Clear()
                                _LineControl.WriteStructResponseAction.Clear()
                                _ErrorMsg = i.Name + "  QGW03 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                                Return False
                            End If
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                        End If

                        _TC = CType(Devices("PLC40"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN03") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN03")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN03", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If
                        Return True

                    Case ScannerDefine.SR1000_01
                        Dim PCBArticle As String = ""
                        Dim PCBSN As String = ""
                        If mScannerResult.Length <> 24 Then
                            _ErrorMsg = i.Name + "  Scan PCB SN Length FAIL. Scan Msg: " + mScannerResult
                            Return False
                        End If

                        Dim cBarcode() As String = mScannerResult.Split(CChar("/"))
                        If cBarcode.Length <> 2 Then
                            _ErrorMsg = i.Name + "  Scan PCB SN Length FAIL. Scan Msg: " + mScannerResult
                            Return False
                        End If

                        PCBArticle = cBarcode(0)
                        If PCBArticle = "" Then
                            _ErrorMsg = i.Name + "  Scan PCB Article FAIL. Scan Msg: " + mScannerResult
                            Return False
                        End If
                        PCBSN = cBarcode(1).Substring(2, 13)

                        If PCBArticle = "" Then
                            _ErrorMsg = i.Name + "  Scan PCB Article Length FAIL. Scan Msg: " + mScannerResult
                            Return False
                        End If
                        If PCBSN = "" Then
                            _ErrorMsg = i.Name + "  Wrong PCB SN Length FAIL. Scan Msg: " + mScannerResult
                            Return False
                        End If
                        If _LocalArticle.ArticleElements("PCB_Stuffed_MatNr").Data <> "" Then
                            If PCBArticle <> _LocalArticle.ArticleElements("PCB_Stuffed_MatNr").Data.Substring(0, 8) Then
                                _ErrorMsg = i.Name + " Wrong PCB Article FAIL. Scan Msg: " + mScannerResult
                                Return False
                            End If
                            '查询及写LineControl
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                            _LineControl = CType(Stations("QGWMB01"), LineControlStation)
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                            _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                            Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                            Loop
                            If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                                _LineControl.ReadStructRequestAction.Clear()
                                _LineControl.WriteStructResponseAction.Clear()
                                _ErrorMsg = i.Name + "  QGWMB01 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                                Return False
                            End If
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                            'SN及Article号继承在PLC上
                            _TC = CType(Devices("PLC40"), TwinCatAds)
                            If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData01") Then
                                _TC.AddAdsVariable(".ADS_stuStoreData01")
                            End If
                            _ADS_stuStoreData = New StructDeviceInteraction
                            _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = PCBSN
                            _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = PCBArticle
                            If Not _TC.WriteAny(".ADS_stuStoreData01", _ADS_stuStoreData) Then
                                _ErrorMsg = _TC.StatusDescription
                                Return False
                            End If
                        Else
                            If PCBArticle <> _LocalArticle.ArticleElements("PCB_Stuffed_MatNr").Data Then
                                _ErrorMsg = i.Name + " Wrong PCB Article FAIL. Scan Msg: " + mScannerResult
                                Return False
                            End If
                        End If

                        Return True

                    Case ScannerDefine.SR1000_02
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        '查询及写LineControl
                        If _LocalArticle.ArticleElements("PCB_Stuffed_MatNr").Data <> "" Then
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                            _LineControl = CType(Stations("QGW05"), LineControlStation)
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                            _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                            Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                            Loop
                            If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                                _LineControl.ReadStructRequestAction.Clear()
                                _LineControl.WriteStructResponseAction.Clear()
                                _ErrorMsg = i.Name + "  QGW05 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                                Return False
                            End If
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                        End If
                        _TC = CType(Devices("PLC50"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN04") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN04")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN04", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If
                        Return True

                    Case ScannerDefine.SR1000_03
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        '查询及写LineControl
                        If _LocalArticle.ArticleElements("PCB_Stuffed_MatNr").Data <> "" Then
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                            _LineControl = CType(Stations("QGW07"), LineControlStation)
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                            _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                            _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                            Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                            Loop
                            If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                                _LineControl.ReadStructRequestAction.Clear()
                                _LineControl.WriteStructResponseAction.Clear()
                                _ErrorMsg = i.Name + "  QGW07 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                                Return False
                            End If
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                        End If
                        _TC = CType(Devices("PLC50"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN05") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN05")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN05", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If
                        Return True
                    Case ScannerDefine.SR752_01
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        '查询及写LineControl
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                        _LineControl = CType(Stations("QGW09"), LineControlStation)
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                        Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                        Loop
                        If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                            _ErrorMsg = i.Name + "  QGW09 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                            Return False
                        End If
                        _LineControl.ReadStructRequestAction.Clear()
                        _LineControl.WriteStructResponseAction.Clear()

                        _TC = CType(Devices("PLC60"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN06") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN06")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN06", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If
                        Return True

                    Case ScannerDefine.SR752_02
                        '  Return True
                        If mScannerResult.Length < 53 Then
                            _ErrorMsg = i.Name + "  SN Length Is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================
                        _Article = mScannerResult.Substring(0, 13)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取Article
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article.Substring(0, 8) Then
                            _ErrorMsg = i.Name + "  Article Number Match is Wrong. Label Article Nr: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data <> _Article.Substring(9, 4) Then
                            _ErrorMsg = i.Name + "  Article Index Match is Wrong. Label Index Nr: " + _Article.Substring(9, 4) + " PLC Index: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                            Return False
                        End If

                        '查询及写LineControl
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                        _LineControl = CType(Stations("QGW11"), LineControlStation)
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _Article
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                        Do While Not _LineControl.WriteStructResponseAction.bulPartReceived
                        Loop
                        If _LineControl.WriteStructResponseAction.bulActionIsFail Then
                            _LineControl.ReadStructRequestAction.Clear()
                            _LineControl.WriteStructResponseAction.Clear()
                            _ErrorMsg = i.Name + "  QGW11 FAIL. Msg: " + _LineControl.WriteStructResponseAction.strActionResultText
                            Return False
                        End If
                        _LineControl.ReadStructRequestAction.Clear()
                        _LineControl.WriteStructResponseAction.Clear()

                        _TC = CType(Devices("PLC60"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData_SN07") Then
                            _TC.AddAdsVariable(".ADS_stuStoreData_SN07")
                        End If
                        _ADS_stuStoreData = New StructDeviceInteraction
                        _ADS_stuStoreData.stuPlcArticleSet.strKostalNr = _Article
                        _ADS_stuStoreData.stuPlcArticleSet.strSerialNr = _SN
                        If Not _TC.WriteAny(".ADS_stuStoreData_SN07", _ADS_stuStoreData) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If
                        Return True

                    Case ScannerDefine.ManualScanner01
                        If mScannerResult.Length <> 22 Then
                            If mScannerResult.Length <> 23 Then
                                _ErrorMsg = i.Name + "  Scan SN Length FAIL. Scan Msg: " + mScannerResult
                                Return False
                            End If
                        End If
                        Dim Cbarcode() As String = mScannerResult.Split(CChar("/"))

                        Dim StrLocation As String = Cbarcode(0)
                        Dim StrDate As String = Cbarcode(1)
                        Dim _StrLocation As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data.ToUpper()
                        If _StrLocation.Substring(0, 3) = "LUX" Then
                            If StrLocation <> _StrLocation.Substring(0, _StrLocation.Length - 1) Then
                                _ErrorMsg = i.Name + "  Scan BOX Location is error ,Scan Msg: " + StrLocation + ", PLC  Msg: " + _StrLocation
                                Return False
                            End If
                        Else
                            If StrLocation <> _StrLocation Then
                                _ErrorMsg = i.Name + "  Scan BOX Location is error ,Scan Msg: " + StrLocation + ", PLC  Msg: " + _StrLocation
                                Return False
                            End If
                        End If

                        _mAppSettings = CType(Devices("_mAppSettings"), Settings)

                        Dim _PlasmaInterval As String = _mAppSettings.MachineIdentifier.PlasmaInterval
                        Dim t As Date = CDate(StrDate)
                        Dim s As Long = DateAndTime.DateDiff(DateInterval.Second, t, Now)

                        If CInt(s / 3600) >= Convert.ToInt32(_PlasmaInterval.Substring(0, _PlasmaInterval.Length - 1)) Then
                            _ErrorMsg = i.Name + "  Scan BOX DateTime is error ,Scan Msg: " + StrDate + " overdue: " + CInt(s / 3600).ToString + "h," & CInt((s Mod 3600) / 60) & "m," & s Mod 60 & "s"
                            Return False
                        End If
                        Return True

                    Case ScannerDefine.ManualScanner02
                        Dim strFoilType As String = ""
                        Dim strFoilIndex As String = "05"
                        If _LocalArticle.ArticleElements("FoilType").Data = "3" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "LEFT" Then
                            strFoilType = "12025758"
                        ElseIf _LocalArticle.ArticleElements("FoilType").Data = "4" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "LEFT" Then
                            strFoilType = "10524141"
                        ElseIf _LocalArticle.ArticleElements("FoilType").Data = "7" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "LEFT" Then
                            strFoilType = "12026704"
                        ElseIf _LocalArticle.ArticleElements("FoilType").Data = "3" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "RIGHT" Then
                            strFoilType = "12026709"
                        ElseIf _LocalArticle.ArticleElements("FoilType").Data = "4" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "RIGHT" Then
                            strFoilType = "12026708"
                        ElseIf _LocalArticle.ArticleElements("FoilType").Data = "7" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = "RIGHT" Then
                            strFoilType = "12026706"
                        End If

                        If mScannerResult.Length <> 26 Then
                            If mScannerResult.Length <> 26 Then
                                _ErrorMsg = i.Name + "  Scan Foil SN Length FAIL. Scan Msg: " + mScannerResult
                                Return False
                            End If
                        End If
                        If mScannerResult.Substring(0, 11) <> strFoilType + "-" + strFoilIndex Then

                            _ErrorMsg = i.Name + "  Scan Foil Barcode Article/Index FAIL. Scan Msg: " + mScannerResult
                            Return False

                        End If
                        Return True

                    Case "Reference" '样件Barcode解析

                        If mScannerResult.Length < 65 Then
                            _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                            Return False
                        End If
                        _SN = mScannerResult.Substring(47, 16)


                        '=======================Check Article======================
                        If Not mScannerResult.Contains(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data) Then
                            _ErrorMsg = "Expected Customer Nr:" + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data &
                                        vbCrLf + " Scanned Context:" + mScannerResult
                            Return False
                        End If

                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN

                        Return True

                    Case "ReTest" 'ReTest Barcode解析

                        If mScannerResult.Length < 68 Then
                            _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================

                        _Article = mScannerResult.Substring(3, 8)
                        _SN = mScannerResult.Substring(2, 13)
                        '=======================选择是否检查其他信息======================
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article Then
                            _ErrorMsg = i.Name + "  Article Match is Wrong. Label Article: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN

                        Return True
                    Case Else 'Don't Change
                        _ErrorMsg = "User Define:PrinterDefine don't Support:" + i.Name
                        Return False
                End Select
                '=======================Don't Change==========================
                _ErrorMsg = "User Define:PrinterDefine don't Support:" + i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try

        End Function

        Public Function CheckSN(ByVal strSN As String) As Boolean
            If SerialNoTracer.SerialNoManager.SM_SetParameters("127.0.0.1", "DBCheckSN", "root", "apb34eol", "TableCheckSN", 3306) <> 1 Then
                _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_SetParameters Failure"
                Return False
            End If

            If SerialNoTracer.SerialNoManager.SM_CheckDatabase() <> 1 Then
                _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_CheckDatabase Failure"
                Return False
            End If

            If SerialNoTracer.SerialNoManager.SM_IsLabelSerialNoExist(strSN) <> 0 Then
                _ErrorMsg = "SN:" + strSN + " have existed"
                Return False
            End If

            'If SerialNoTracer.SerialNoManager.SM_SaveSerialNo(strSN, "") <> 1 Then
            '    _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_SaveSerialNo Failure"
            '    Return False
            'End If
            Return True
        End Function
    End Class

    Public Class PrinterDefine
        Implements IPrinterDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorMsg As String Implements IPrinterDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property
        Public ReadOnly Property ErrorCode As String Implements IPrinterDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetAllFieldsOfPrintFile(ByVal i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fields As String()) As Boolean Implements IPrinterDefine.GetAllFieldsOfPrintFile
            Try
                _ErrorMsg = ""


                Select Case i.Name
                    Case "Printer01"
                        '=======================设置Print Field:注意Fields大小====================
                        ReDim Fields(2)
                        Dim StrSN = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        Dim Barcode As String = String.Empty
                        Barcode = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data _
                                  + "-" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data _
                                  + "/SN" & StrSN _
                                  + "/HW:" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data _
                                  + "/SW:" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data

                        'Fields(0) = "^FN1^FD" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data &
                        '    "-" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data & "^FS"
                        'Fields(1) = "^FN2^FD" & StrSN & "^FS"
                        'Cancel show Article & SN
                        Fields(0) = "^FN1^FD" & "" & "^FS"
                        Fields(1) = "^FN2^FD" & "" & "^FS"

                        Fields(2) = "^FN3^FD" & Barcode & "^FS"

                        Return True

                    Case "Printer02"
                        Dim SN As String = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        Dim DT As String = Format(Date.Now, "yyyy-MM-dd HH:mm")
                        Dim Barcode As String = LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data.ToUpper() + "/" + DT
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_FILE).Data = "PlasmaLabel.txt"
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_NAME).Data = "PlasmaLabel"
                        ReDim Fields(1)
                        Fields(0) = DT
                        Fields(1) = Barcode
                        Return True


                    Case Else 'Don't Change.
                        _ErrorMsg = "User Define:PrinterDefine don't Support:" + i.Name
                        Return False
                End Select

                '=======================Don't Change==========================
                _ErrorMsg = "User Define:PrinterDefine don't Support:" + i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

        '=======================获取星期==================================
        Private Function GetWeek() As String
            Dim calidar As Globalization.Calendar = Globalization.CultureInfo.CurrentCulture.Calendar
            Return calidar.GetWeekOfYear(Date.Now, Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday).ToString("D02")
        End Function
    End Class

    Public Class LaserDefine
        Implements ILaserDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorMsg As String Implements ILaserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements ILaserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetSeqentialCommands(ByVal i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef mCmd As String) As Boolean Implements ILaserDefine.GetSeqentialCommands
            Try
                _ErrorMsg = ""
                Select Case i.Name
                    Case "Laser"
                        Return True
                    Case Else 'Don't Change.
                        mCmd = ""
                        _ErrorMsg = "User Define:GetSeqentialCommands don't Support:" + i.Name
                        Return False
                End Select
                '=======================Don't Change==========================
                mCmd = ""
                _ErrorMsg = "User Define:GetSeqentialCommands don't Support:" + i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function
    End Class

    Public Class FlashDefine
        Implements IFlashDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorMsg As String Implements IFlashDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IFlashDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetSeqentialCommands(ByVal i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fileds As String()) As Boolean Implements IFlashDefine.GetSeqentialCommands
            Try
                _ErrorMsg = ""
                Select Case i.Name
                    Case "Flash"
                        '=======================Command==================================
                        ReDim Fileds(0)

                        If _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAG1301" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAG1301.prj"
                            'System.Threading.Thread.Sleep(100)
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1301" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1301.prj"
                            'System.Threading.Thread.Sleep(100)
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAG1400" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAG1400.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1400" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1400.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAG1501" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAG1501.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1500" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1500.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1500-116" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1500-116.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAG1600" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAG1600.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1600" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1600.prj"
                        ElseIf _LocalArticle.ArticleElements("LK_SW_Version").Data = "AAK1600-116" Then
                            Fileds(0) = "#1*RUN DaimlerSVS-AAK1600-116.prj"
                        Else
                            If Not _LocalArticle.ArticleElements("LK_SW_Version").Data = "" Then
                                Fileds(0) = String.Format("#1*RUN DaimlerSVS-{0}.prj", _LocalArticle.ArticleElements("LK_SW_Version").Data) ' cut in from R9.4 
                            Else
                                _ErrorMsg = "User Define:GetSeqentialCommands don't Support:" + i.Name + " LK_SW_Version is none "
                                Return False
                            End If
                        End If

                            Return True
                    Case Else 'Don't Change.
                        ' mCmd = ""
                        _ErrorMsg = "User Define:GetSeqentialCommands don't Support:" + i.Name
                        Return False
                End Select
                '=======================Don't Change==========================
                _ErrorMsg = "User Define:GetSeqentialCommands don't Support:" + i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function
    End Class

    Public Class FailPrinterDefine
        Implements IFailPrinterDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorMsg As String Implements IFailPrinterDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IFailPrinterDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetFailCollection(ByVal i As Station, ByVal WT As WT, ByVal Devices As System.Collections.Generic.Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Microsoft.VisualBasic.Collection Implements IFailPrinterDefine.GetFailCollection
            Dim Line As New Collection
            Dim cStep As String = WT.PartFailTestStep
            Dim cLow As String = WT.PartFailLowerLimit
            Dim cUpper As String = WT.PartFailUpperLimit
            Dim cMeasureValue As String = WT.PartFailValue
            '=======================设置打印内容与格式==================================
            Line.Add("************************************************")
            Line.Add("")
            Line.Add("SN:" + WT.SerialNumber)
            Line.Add("Article:" + WT.ArticleNumber)
            Line.Add("Schedule:" + WT.Schedule)
            Line.Add("FailCarrierNr:" + WT.Number.ToString)
            Line.Add("PartFailLocation:" + WT.PartFailLocation)
            Line.Add("PartFailCode:" + WT.PartFailCode)
            Line.Add("PartFailTestStep:" + WT.PartFailTestStep)
            Line.Add("PartFailValue:" + WT.PartFailValue)
            Line.Add("PartFailLowerLimit:" + WT.PartFailLowerLimit)
            Line.Add("PartFailUpperLimit:" + WT.PartFailUpperLimit)
            Line.Add("PartFailUnit:" + WT.PartFailUnit)
            Line.Add("PartFailText:" + WT.PartFailText)
            Line.Add("Time:" + Date.Now.ToString)
            Line.Add("")
            Line.Add("************************************************")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Line.Add("")
            Return Line
        End Function
    End Class

    'Friend Class SnReadWriteHandler

    '    Public Shared Function Write(ByVal SN As String) As Boolean

    '        Dim _Settings As Settings = CType(Devices(Settings.Name), Settings)
    '        _FileHandler.WriteIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN", _SN)

    '        Return True
    '    End Function


    'End Class

    Public Class ShowPicDefine
        Implements IShowPicDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _FileHandler As New FileHandler
        Private _mAppSettings As Settings

        Public ReadOnly Property ErrorMsg As String Implements IShowPicDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IShowPicDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetAllFieldsOfFileName(ByVal i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fields As String()) As Boolean Implements IShowPicDefine.GetAllFieldsOfFileName
            Try
                '=======================根据日期删除文件===============================
                _mAppSettings = CType(Devices("_mAppSettings"), Settings)
                '=======================指定图片赋值:注意Fields大小====================
                ReDim Fields(0)
                Fields(0) = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data + "_2.png"
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

    Public Class VariantInfo
        Implements IVariantInfoDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _SN As String
        Private _FileHandler As New FileHandler


        Public ReadOnly Property ErrorMsg As String Implements IVariantInfoDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IVariantInfoDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean Implements IVariantInfoDefine.GetVariantInfo
            Try
                Select Case _i.Name
                    Case "NewPart"

                        '=======================Write Variant Information to PLC===============================================
                        variantInfo.strKostalNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                        variantInfo.strKostalArticleName = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
                        variantInfo.strCustomerNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        variantInfo.strSerialNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        variantInfo.strProductFamily = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data
                        '=======================Write PLC Need Information ====================================================
                        variantInfo.strUserDefine = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data

                        Return True

                    Case "Article05", "Article10", "Article15", "Article20", "Article30", "Article40", "Article50", "Article60"
                        Dim A As String = "中午"
                        variantInfo.strKostalNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                        variantInfo.strCustomerNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        variantInfo.strKostalArticleName = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
                        variantInfo.strCustomerNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        variantInfo.strSerialNr = ""
                        variantInfo.strProductFamily = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data

                        Return True

                    Case Else 'Don't Change.
                        _ErrorMsg = "User Define:VariantInfo don't Support:" + _i.Name
                        Return False

                End Select

                '=======================Don't Change==========================
                _ErrorMsg = "User Define:VariantInfo don't Support:" + _i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

    Public Class ReTestDefine
        Implements IReTestDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function ReTest(ByVal _i As Base.Station, ByVal LocalArticle As Base.Article, ByVal Devices As System.Collections.Generic.Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements Base.IReTestDefine.ReTest
            Try
                '=======================获取SN===============================================
                'Dim SN As String
                'SN = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data

                '=======================复位数据库============================================
                'If SerialNoTracer.SerialNoManager.SM_SetParameters("127.0.0.1", "Audi", "root", "apb34eol", "Audi", "3306") <> 0 Then
                '    _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_SetParameters Fail"
                '    Return False
                'End If

                'If SerialNoTracer.SerialNoManager.SM_CheckDatabase() <> 0 Then
                '    _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_CheckDatabase Fail"
                '    Return False
                'End If

                'If SerialNoTracer.SerialNoManager.SM_SetLabelRetest(SN) <> 0 Then
                '    _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_SetLabelRetest Fail"
                '    Return False
                'End If
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

    Public Class LineControlDefine
        Implements ILineControlDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _TC As TwinCatAds
        Private _PLC_stuDataStore1 As StructRequestAction

        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function LineControlDefine(ByVal _i As Base.Station, ByVal LocalArticle As Base.Article, ByVal Devices As System.Collections.Generic.Dictionary(Of String, Object), ByVal Stations As System.Collections.Generic.Dictionary(Of String, Base.IStationTypeBase), ByRef _Listchild As System.Collections.Generic.Dictionary(Of String, Base.ChildElement)) As Boolean Implements Base.ILineControlDefine.LineControlDefine
            Try
                Select Case _i.Name

                    Case "QGW04"
                        Dim _TC1 As TwinCatAds = CType(Devices("PLC40"), TwinCatAds)
                        If Not _TC1.PLCVairablesHandles.ContainsKey(".ADS_stuStoreData01") Then
                            _TC1.AddAdsVariable(".ADS_stuStoreData01")
                        End If
                        Dim _ADS_stuStoreData1 As StructDeviceInteraction = CType(_TC1.ReadAny(".ADS_stuStoreData01", GetType(StructDeviceInteraction)), StructDeviceInteraction)
                        If _ADS_stuStoreData1.stuPlcArticleSet.strKostalNr = "" Then Return True
                        _Listchild.Add("1", New ChildElement(_ADS_stuStoreData1.stuPlcArticleSet.strSerialNr, _ADS_stuStoreData1.stuPlcArticleSet.strKostalNr))

                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
            Return True
        End Function

    End Class

    Public Class BeforeStepLine
        Implements IBeforeStepDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public Function StepDefine(ByVal _i As Station, ByVal logger As Logger, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, Base.IStationTypeBase)) As Boolean Implements IBeforeStepDefine.StepDefine
            Try
                'Select Case _i.Name
                '    Case "SR710_1"
                '        Select Case _i.StepOutputNumber
                '            Case 0, 2001
                '                MessageBox.Show(_i.StepOutputNumber.ToString)
                '            Case 1
                '        End Select
                'End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property
    End Class

    Public Class AfterStepLine
        Implements IAfterStepDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _failPirnter As FailPrinter
        Private _FileHandler As New FileHandler
        Private _mAppSettings As Settings

        Public Function StepDefine(ByVal _i As Station, ByVal logger As Logger, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, Base.IStationTypeBase)) As Boolean Implements IAfterStepDefine.StepDefine
            Try
                Select Case _i.Name
                    Case "Fail Printer"
                        Select Case _i.StepOutputNumber
                            Case 1000, 2001
                                _failPirnter = CType(Devices("Fail Printer"), FailPrinter)
                                _failPirnter.Cut()
                        End Select

                    Case "ShowPic"
                        _mAppSettings = CType(Devices(Settings.Name), Settings)
                        Select Case _i.StepOutputNumber
                            Case 1000, 2000
                                _FileHandler.DelectLogByDay(30, _mAppSettings.PicFolder, ".png")
                                If _FileHandler.FileExist(_mAppSettings.PicFolder + "\" + LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data + "_2.png") Then
                                    _FileHandler.FileMove(_mAppSettings.PicFolder, "c:\DATA\Picture", LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data + "_2.png")
                                End If
                        End Select
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property
    End Class

    Public Class CheckTrigInfo
        Implements ICheckTrigInfo

        Public Enum enumDeviceTrigerName
            _ReadStructDeviceInteraction = 0 ' 自动触发使用 0  手动触发使用1
            _ManualReadStructRequestAction = 1
        End Enum

        Public Enum enumRequestTrigerName
            _ReadStructRequestAction = 0 ' 自动触发使用 0  手动触发使用1
            _ManualStructRequestAction = 1
        End Enum
        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private AppArticle As Article
        Public Function CheckTrigInfoAndSelectLocalArticle(ByVal _i As Station, ByVal LocalArticle As Article, ByVal TrigSignal As Dictionary(Of String, Object), ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, Base.IStationTypeBase)) As SelectLocalArticleType Implements Base.ICheckTrigInfo.CheckTrigInfoAndSelectLocalArticle
            Try
                Select Case _i.Name
                    Case "ManualScanner02", "Printer02", "ManualScanner01", "Printer01", "SR751_01", "SR751_02", "SR751_03", "SR751_04", "SR1000_01", "Flash", "SR1000_02", "SR1000_03", "SR752_01", "SR752_02"
                        AppArticle = CType(Devices(Article.Name), Article)
                        If CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = "" Then
                            CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                            ' _ErrorMsg = _i.Name + "  strKostalNr Is Null"
                            ' Return SelectLocalArticleType.SelectFail
                        End If
                        LocalArticle.GetArticle_FromID(CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr)
                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = "" Then
                            Throw New Exception("LocalArticle.GetArticle_FromID Failure")
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strUserDefine
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strSerialNr
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change

                    Case "QGWMB" '适用于 PLC 传递的变种不再变种List里面 例如PCBA变种
                        If CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr Is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If

                        If CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strSerialNr = "" Then
                            _ErrorMsg = _i.Name + "  strSerialNr Is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strSerialNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strUserDefine
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change
                    Case "QGWMB01"
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change
                End Select
                Return SelectLocalArticleType.AutoSelect
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return SelectLocalArticleType.SelectFail
            End Try
        End Function

        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property
    End Class


    Public Class ScannerDeviceDefine '适用于样件和复测时选择哪个扫描仪
        Implements IScannerDeviceDefine
        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Function GetScannerName(_i As Station, _LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), ByRef strDeviceName As String, ByRef ScannerType As enumScanType) As enumScannerDeviceType Implements IScannerDeviceDefine.GetScannerName
            strDeviceName = "SR752_01" '扫描仪名称 PSD_IT_4820 或者 SR752_01
            ScannerType = enumScanType.Auto '手持扫描仪时 enumScanType.Manual 自动时 enumScanType.Auto
            Return enumScannerDeviceType.ManualSelect '系统选择enumScannerDeviceType.AutoSelect Userdefine层定义使用enumScannerDeviceType.ManualSelect
        End Function
    End Class

    Public Class UserStationDefine '适用于Userdefine Station 选择触发哪一站
        Implements IUserStationDefine
        Public ReadOnly Property ErrorCode As String Implements IUserDefine.ErrorCode
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements IUserDefine.ErrorMsg
            Get
                Throw New NotImplementedException()
            End Get
        End Property
        Public Function GetStationName(_i As Station, _LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), ByRef strStationName As String) As Boolean Implements IUserStationDefine.GetStationName
            strStationName = "SR752_01" '写站别名称 特别注意手持扫描仪时不是Device名称 是站别名称
            Return True
        End Function
    End Class

    Public Class ManualScannerMsgDefine
        Implements IManualScannerMsgDefine
        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function GetMsg(_i As Station, ByRef strMsg As String, _LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements IManualScannerMsgDefine.GetMsg
            Try
                Select Case _i.Name
                    Case "ManualScanner01"
                        strMsg = "Scan Plasma Barcode"
                        Return True
                    Case "ManualScanner02"
                        strMsg = "Scan Foil Barcode"
                        Return True
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function
    End Class

    Public Class ScannerCommandDefine
        Implements IScannerCommandDefine
        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function GetCommand(ByVal _i As Station, ByRef strTrigOnCmd As String, ByRef strTrigOffCmd As String, ByRef iTimeOut As Integer, ByVal iRepeat As Integer, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements IScannerCommandDefine.GetCommand
            Try
                Select Case _i.Name
                    Case "SR751"
                        Dim _Scanner As Scanner = CType(Devices(_i.Name), Scanner)

                        '选择是否发送其它命令
                        'Do While _Scanner.Running
                        '    System.Threading.Thread.Sleep(10)
                        'Loop

                        '_Scanner.SendAndReadCommand(3000, _Scanner.InterfaceConfig.DataFrameSTX + "1111" + _Scanner.InterfaceConfig.DataFrameEXT, _Scanner.InterfaceConfig.DataFrameSTX + "1111" + _Scanner.InterfaceConfig.DataFrameEXT)
                        ''_Scanner.SendCommand("22222")
                        'Do While _Scanner.Running
                        '    System.Threading.Thread.Sleep(10)
                        'Loop


                        'If _Scanner.Status <> enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                        '    _ErrorMsg = "SendAndReadCommand Fail"
                        '    Return False
                        'End If
                        'strTrigOnCmd="" 时选择系统命令
                        'strTrigOffCmd="" 时选择系统命令

                        strTrigOnCmd = _Scanner.InterfaceConfig.DataFrameSTX + "LON" + _Scanner.InterfaceConfig.DataFrameEXT
                        strTrigOffCmd = _Scanner.InterfaceConfig.DataFrameSTX + "LOFF" + _Scanner.InterfaceConfig.DataFrameEXT
                        Return True
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function
    End Class

    Public Class LineControlStationDefine
        Implements ILineControlStationDefine
        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function GetStation(ByVal _i As Station, ByRef strPreviousStation As String, ByRef strCurrentStation As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements ILineControlStationDefine.GetStation
            Try
                Select Case _i.Name
                    Case "QGW02"
                        ' _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data
                        'strPreviousStation="" 时选择系统站别
                        'strCurrentStation="" 时选择系统站别
                        strPreviousStation = ""
                        strCurrentStation = ""
                        Return True
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function
    End Class

    Public Class TwicatRun
        Implements ITwicatRun

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function TwicatRun(ByVal _i As Station, ByVal TC As TwinCatAds, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, Base.IStationTypeBase)) As Boolean Implements ITwicatRun.TwicatRun
            Try
                'Select Case _i.Name
                '    Case "PLC1"
                '        If Not TC.ListDeviceNotificationEx.ContainsKey(".ADS_stuScannerSt17") Then
                '            TC.AddNotificationEx(".ADS_stuScannerSt17", New StructDeviceInteraction)
                '        End If
                '        Dim t As ScannerStation = CType(Stations("SR710_4"), ScannerStation)
                '        Dim tDeviceInteraction As New StructDeviceInteraction
                '        tDeviceInteraction = CType(TC.GetDeviceNotificationEx(".ADS_stuScannerSt17"), StructDeviceInteraction)
                '        t.ManualReadStructDeviceInteraction.stuPlcArticleSet = tDeviceInteraction.stuPlcArticleSet
                '        t.ManualReadStructDeviceInteraction.bulPlcDoAction = tDeviceInteraction.bulPlcDoAction
                '        If t.ManualReadStructDeviceInteraction.bulAdsActionIsPass Or t.ManualReadStructDeviceInteraction.bulAdsActionIsFail Then
                '            tDeviceInteraction.bulAdsActionIsPass = t.ManualReadStructDeviceInteraction.bulAdsActionIsPass
                '            tDeviceInteraction.bulAdsActionIsFail = t.ManualReadStructDeviceInteraction.bulAdsActionIsFail
                '            tDeviceInteraction.strAdsActionValue = t.ManualReadStructDeviceInteraction.strAdsActionValue
                '            TC.WriteAny(".ADS_stuScannerSt17", tDeviceInteraction)
                '        End If
                'End Select

                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

    Public Class ManualReTestMsgDefine
        Implements IManualReTestMsgDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function GetMsg(ByVal _i As Base.Station, ByRef strMsg As String, ByVal PLC_OUT_WT As Base.WT, ByVal _LocalArticle As Base.Article, ByVal Devices As System.Collections.Generic.Dictionary(Of String, Object), ByVal Stations As System.Collections.Generic.Dictionary(Of String, Base.IStationTypeBase)) As Boolean Implements Base.IManualReTestMsgDefine.GetMsg
            Try
                Select Case _i.Name
                    Case "ManualReTest"
                        Select Case _i.StepOutputNumber
                            Case 2

                            Case 5
                        End Select
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

    Public Class ManualReTestChangeScheduleDefine
        Implements IManualReTestChangeScheduleDefine

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _ScheduleManager As ScheduleManager
        Private _ReTestStation As ReTestStation

        Public ReadOnly Property ErrorCode As String Implements Base.IUserDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMsg As String Implements Base.IUserDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public Function ChangeSchedule(ByVal _i As Base.Station, ByVal _LocalArticle As Base.Article, ByVal Devices As System.Collections.Generic.Dictionary(Of String, Object), ByVal Stations As System.Collections.Generic.Dictionary(Of String, Base.IStationTypeBase)) As Boolean Implements Base.IManualReTestChangeScheduleDefine.ChangeSchedule
            Try
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

End Module




