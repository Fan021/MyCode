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
<Assembly: AssemblyCompany("KOSTAL MEXCO")>
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
            _SerialNoGenerator = New prjSerialNoGenerator.clsSerialGenerator
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
                Const VPPS_CODE As String = "VPPS_CODE"
                Const DUNS_CODE As String = "DUNS_CODE"

                Select Case i.Name

                    Case "SR752_01"
                        '"[)>" & ChrW(30) & "06" & ChrW(29) & "Y7002030000000Y" & ChrW(29) & "P26211242" & ChrW(29) & "12V421274883" & ChrW(29) & "T1A18115A1Z0N001F" & ChrW(30) & ChrW(4)
                        If mScannerResult.Length < 65 Then
                            _ErrorMsg = i.Name + "  SN Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        If Not mScannerResult.Contains(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data) _
                            Or Not mScannerResult.Contains(_LocalArticle.ArticleElements(VPPS_CODE).Data) _
                            Or Not mScannerResult.Contains(_LocalArticle.ArticleElements(DUNS_CODE).Data) _
                             Then
                            'Or Not mScannerResult.Contains(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                            _ErrorMsg = i.Name + " Barcode content is Wrong. Scanned Content:" + mScannerResult
                            Return False

                        End If

                        Return True

                    Case "SR1000_11"
                        If mScannerResult.Length <> 24 Then
                            _ErrorMsg = i.Name + " SN Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If
                        Dim cBarcode() As String = mScannerResult.Split(CChar("/"))
                        If cBarcode.Length < 2 Then
                            _ErrorMsg = i.Name + " SN Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If
                        _PCBANumber = Convert.ToString(CInt(_LocalArticle.ArticleElements("LK_article_no_PCB_0").Data), 16).PadLeft(2, CChar("0")) &
                                      Convert.ToString(CInt(_LocalArticle.ArticleElements("LK_article_no_PCB_1").Data), 16).PadLeft(2, CChar("0")) &
                                      Convert.ToString(CInt(_LocalArticle.ArticleElements("LK_article_no_PCB_2").Data), 16).PadLeft(2, CChar("0")) &
                                      Convert.ToString(CInt(_LocalArticle.ArticleElements("LK_article_no_PCB_3").Data), 16).PadLeft(2, CChar("0"))

                        If cBarcode(0) <> _PCBANumber Then
                            _ErrorMsg = i.Name + " PCBA Article Match is Wrong. Barcode Article: " + cBarcode(0) + " PLC Article:" + _PCBANumber
                            Return False
                        End If
                        _SN = cBarcode(1).Substring(2)

                        _LineControl = CType(Stations("SCREW"), LineControlStation)
                        _LineControl.ManualReadStructRequestAction.stuPlcArticleSet.strKostalNr = cBarcode(0)
                        _LineControl.ManualReadStructRequestAction.stuPlcArticleSet.strSerialNr = _SN
                        _LineControl.ManualReadStructRequestAction.bulDoPositiveAction = True

                        Do While Not _LineControl.ManualWriteStructResponseAction.bulPartReceived
                        Loop

                        If _LineControl.ManualWriteStructResponseAction.bulActionIsFail Then
                            _LineControl.ManualReadStructRequestAction.bulDoPositiveAction = False
                            _LineControl.ManualReadStructRequestAction.bulDoNegativeAction = False
                            _LineControl.ManualReadStructRequestAction.bulRunning = False

                            _LineControl.ManualWriteStructResponseAction.bulActionIsFail = False
                            _LineControl.ManualWriteStructResponseAction.bulActionIsPass = False
                            _LineControl.ManualWriteStructResponseAction.bulPartReceived = False
                            _ErrorMsg = i.Name + "  Screw Linecontrol is Failure. Error Message: " + _LineControl.ManualWriteStructResponseAction.strActionResultText
                            Return False
                        End If

                        _LineControl.ManualReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.ManualReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ManualReadStructRequestAction.bulRunning = False

                        _LineControl.ManualWriteStructResponseAction.bulActionIsFail = False
                        _LineControl.ManualWriteStructResponseAction.bulActionIsPass = False
                        _LineControl.ManualWriteStructResponseAction.bulPartReceived = False


                        _TC = CType(Devices("PLC1"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuDataStore") Then
                            _TC.AddAdsVariable(".ADS_stuDataStore")
                        End If

                        _PLC_stuDataStore1 = New StructRequestAction
                        _PLC_stuDataStore1.stuPlcArticleSet.strSerialNr = _SN
                        _PLC_stuDataStore1.stuPlcArticleSet.strKostalNr = cBarcode(0)
                        If Not _TC.WriteAny(".ADS_stuDataStore", _PLC_stuDataStore1) Then
                            _ErrorMsg = _TC.StatusDescription
                            Return False
                        End If

                        Return True

                    Case "SR710_01"

                        _SN = String.Empty
                        Dim _Settings As Settings = CType(Devices(Settings.Name), Settings)
                        _FileHandler.WriteIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN", _SN)
                        'Sample value as below  // added by wang65 20180602
                        '"CDH# 4K1 941 501 L 6PS#1#542963759#/3OS10341591-00/SN91110RC90000X09031811**="

                        If mScannerResult.Length < 77 Then
                            _ErrorMsg = i.Name + "  SN Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data.Length <> 8 Then
                            _ErrorMsg = i.Name + " Article Number Length is Wrong"
                            Return False
                        End If

                        Dim cBarcode() As String = mScannerResult.Split(CChar("#"))
                        If cBarcode.Length < 5 Then
                            _ErrorMsg = i.Name + "  Barcode Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If
                        Dim subBacode() As String = cBarcode(4).Split(CChar("/"))
                        If subBacode.Length < 3 Then
                            _ErrorMsg = i.Name + "  Barcode Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If
                        '=======================解析===============================

                        _Article = subBacode(1).Substring(3, 8)
                        _SN = subBacode(2).Substring(2, 13)

                        '=======================Check Article======================
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article Then
                            _ErrorMsg = i.Name + "  Article Match is Wrong. Label Article: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN

                        _FileHandler.WriteIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN", _SN)

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

            If SerialNoTracer.SerialNoManager.SM_SaveSerialNo(strSN, "") <> 1 Then
                _ErrorMsg = "SerialNoTracer.SerialNoManager.SM_SaveSerialNo Failure"
                Return False
            End If
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

                    Case "Zebra_1"

                        '=======================设置Print Field:注意Fields大小====================
                        ReDim Fields(4)
                        Const VPPS_CODE As String = "VPPS_CODE"
                        Const DUNS_CODE As String = "DUNS_CODE"

                        Const RS As String = "_1E" '"~d030"
                        Const GS As String = "_1D" '"~d029"
                        Const EOT As String = "_04" '"~d004"

                        Dim Barcode As String = String.Empty
                        '==========================================================================
                        '^XA
                        '^PQ1
                        '^XF10336891
                        '^FN1^FD7002030000000Y^FS
                        '^FN2^FD421274883^FS
                        '^FN3^FD10336891^FS
                        '^FN4^FDLS17128A2B4C6000^FS
                        '^BY3,3.0^FO17,12^BXN,3,200^FR^FH^FN5^FD[)>_1E06_1DY7002030000000Y_1DPxxxxxxxx_1D12V421274883_1DT1116361@905330N1_1D1P10103377_1E_04^FS
                        '^BY3,3.0^FO17,12^BXN,3,200^FR^FH^FN5^FD[)>_1E06_1DY7002030000000Y_1DPxxxxxxxx_1D12V421274883_1DT1116361@905330N1_1D1P10103377_1E_04^FS
                        '^XZ
                        '==========================================================================
                        Barcode = "[)>" &
                            RS & "06" &
                            GS & "Y" & LocalArticle.ArticleElements(VPPS_CODE).Data &
                            GS & "P" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data &
                            GS & "12V" & LocalArticle.ArticleElements(DUNS_CODE).Data &
                            GS & "T" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data &
                            RS &
                            EOT

                        Fields(0) = LocalArticle.ArticleElements(VPPS_CODE).Data
                        Fields(1) = LocalArticle.ArticleElements(DUNS_CODE).Data
                        Fields(2) = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                        Fields(3) = LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        Fields(4) = Barcode

                        Return True

                    Case "Zebra_2"
                        '=======================设置Print Field:注意Fields大小====================
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

        Public Function GetSeqentialCommands(ByVal i As Station, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef mCmd As String) As Boolean Implements IFlashDefine.GetSeqentialCommands
            Try
                _ErrorMsg = ""
                Select Case i.Name
                    Case "Flash"
                        '=======================Command==================================
                        mCmd = "RUN R5F109BC.FRS"
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

    Public Class ManualScanDefine
        Implements IManualScanDefine

        Private _SN As String
        Private _Customer As String
        Private _Article As String
        Private _ArticleIndex As String
        Private _Index As String
        Private _AppArticle As Article
        Private _ErrorMsg As String
        Private _ErrorCode As String
        Private _PCBANumber As String
        Private _SW As String
        Private _HW As String
        Private articlesw As String
        Private articlehw As String
        Private _Color As String
        Private _FileHandler As New FileHandler

        Public ReadOnly Property ErrorMsg As String Implements IManualScanDefine.ErrorMsg
            Get
                Return _ErrorMsg
            End Get
        End Property

        Public ReadOnly Property ErrorCode As String Implements IManualScanDefine.ErrorCode
            Get
                Return _ErrorCode
            End Get
        End Property

        Public Function GetBarcode(ByVal i As Station, ByVal mScannerResult As String, ByRef _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean Implements IManualScanDefine.GetBarcode
            Try
                Dim articleindex As String
                _ErrorMsg = ""
                _ErrorCode = ""
                _SN = String.Empty
                Dim _Settings As Settings = CType(Devices(Settings.Name), Settings)
                _FileHandler.WriteIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN", _SN)

                Select Case i.Name

                    Case "Reference" '样件Barcode解析

                        If mScannerResult.Length < 65 Then
                            _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                            Return False
                        End If
                        _SN = mScannerResult.Substring(47, 16)



                        '====================================================================
                        ' to be deleted!
                        'Dim cBarcode() As String = mScannerResult.Split(CChar("/"))
                        'If cBarcode.Length < 7 Then
                        '    _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                        '    Return False
                        'End If

                        '"[)>" & ChrW(30) & "06" & ChrW(29) & "Y7002030000000Y" & ChrW(29) & "P26211242" & ChrW(29) & "12V421274883" & ChrW(29) & "T1A18115A1Z0N0014" & ChrW(30) & ChrW(4)
                        '=======================解析===============================
                        '_Customer = mScannerResult.Substring(47, 16)
                        '_Article = cBarcode(3).Substring(3, 8)
                        '_ArticleIndex = cBarcode(3).Substring(11)
                        '_Customer = "/" + cBarcode(1)
                        '_SW = "/" + cBarcode(4)
                        '_HW = "/" + cBarcode(5)
                        '_Color = "/" + cBarcode(2)

                        '=======================Check Article======================
                        If Not mScannerResult.Contains(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data) Then
                            _ErrorMsg = "Expected Customer Nr:" + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data &
                                        vbCrLf + " Scanned Context:" + mScannerResult
                            Return False
                        End If

                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN

                        _FileHandler.WriteIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN", _SN)

                        Return True

                    Case "ReTest" 'ReTest Barcode解析

                        If mScannerResult.Length < 68 Then
                            _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                            Return False
                        End If

                        Dim cBarcode() As String = mScannerResult.Split(CChar("/"))
                        If cBarcode.Length < 7 Then
                            _ErrorMsg = "Incorrect SN Length" + vbCrLf + "Scanned Content: " + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================


                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data.Length <> 2 Then
                            _ErrorMsg = "Incorrect Article Index Length"
                            Return False
                        End If

                        ArticleIndex = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data.Length <> 12 Then
                            _ErrorMsg = "Incorrect SW Length"
                            Return False
                        End If
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data.IndexOf("/") < 0 Then
                            _ErrorMsg = "Incorrect SW Format"
                            Return False
                        End If
                        articlesw = LK_SN_CONSTANTS.DELIMITER + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data.Replace("/", "").Replace(".", "").Replace(" ", "")

                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data.Length <> 12 Then
                            _ErrorMsg = "Incorrect HW Length"
                            Return False
                        End If
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data.IndexOf("/") < 0 Then
                            _ErrorMsg = "Incorrect HW Format"
                            Return False
                        End If
                        articlehw = LK_SN_CONSTANTS.DELIMITER + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data.Replace("/", "").Replace(".", "").Replace(" ", "")
                        _Index = Convert.ToString(CInt(ArticleIndex), 16).ToUpper.PadLeft(2, CChar("0"))

                        _SN = cBarcode(6).Substring(2)
                        _Article = cBarcode(3).Substring(3, 8)
                        _ArticleIndex = cBarcode(3).Substring(11)
                        _Customer = cBarcode(1).Replace(" ", "")
                        _Color = "/" + cBarcode(2)
                        _SW = "/" + cBarcode(4)
                        _HW = "/" + cBarcode(5)

                        '=======================Check Article======================
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _Article Then
                            _ErrorMsg = " Scanned Article: " + _Article + vbCrLf + " Selected Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                            Return False
                        End If

                        '=======================Check ArticleIndex======================
                        If _Index <> _ArticleIndex Then
                            _ErrorMsg = "Scanned ArticleIndex: " + _ArticleIndex + vbCrLf + " Selected ArticleIndex: " + _Index
                            Return False
                        End If

                        '=======================Check Color======================
                        If _LocalArticle.ArticleElements("LabelDaiLuCol").Data <> _Color Then
                            _ErrorMsg = "Scanned Color: " + _Color + vbCrLf + " Selected Color: " + _LocalArticle.ArticleElements("LabelDaiLuCol").Data
                            Return False
                        End If

                        '=======================Check Customer======================
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data.Replace(" ", "") <> _Customer Then
                            _ErrorMsg = "Scanned Customer: " + _Customer + " Selected Customer: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data.Replace(" ", "")
                            Return False
                        End If

                        '=======================Check SW======================
                        If articlesw <> _SW Then
                            _ErrorMsg = "Scanned SW: " + _SW + vbCrLf + " Selected SW: " + articlesw
                            Return False
                        End If

                        '=======================Check HW======================
                        If articlehw <> _HW Then
                            _ErrorMsg = "Scanned HW: " + _HW + vbCrLf + " Selected HW: " + articlehw
                            Return False
                        End If

                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN

                        Return True
                    Case Else 'Don't Change.
                        _ErrorMsg = "User Define:ManualScanDefine don't Support:" + i.Name
                        Return False
                End Select

                '=======================Don't Change==========================
                _ErrorMsg = "User Define:ManualScanDefine don't Support:" + i.Name
                Return False
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
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
                Static _LastUsedSN As String
                _SN = String.Empty
                Dim _Settings As Settings = CType(Devices(Settings.Name), Settings)

                Select Case _i.Name
                    Case "NewPart"

                        '=======================Write Variant Information to PLC===============================================
                        variantInfo.strKostalNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                        variantInfo.strKostalArticleName = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
                        variantInfo.strCustomerNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        variantInfo.strProductFamily = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data
                        '=======================Write PLC Need Information ====================================================
                        variantInfo.strUserDefine = ""

                        _SN = _FileHandler.ReadIniFile(_Settings.LogFolder, "SN.ini", "UserDefined", "SN")
                        If _SN.Length <> 13 Then Return False   'Or _LastUsedSN = _SN

                        variantInfo.strSerialNr = _SN 'LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        _LastUsedSN = variantInfo.strSerialNr

                        Return True

                    Case "Article"

                        variantInfo.strKostalNr = LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
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

                    Case "QGW01"
                        _Listchild.Add("1", New ChildElement("SOFTWARE", LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data))
                        _Listchild.Add("2", New ChildElement("HARDWARE", LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data))
                    Case "QGW11"
                        _TC = CType(Devices("PLC1"), TwinCatAds)
                        If Not _TC.PLCVairablesHandles.ContainsKey(".ADS_stuDataStore") Then
                            _TC.AddAdsVariable(".ADS_stuDataStore")
                        End If
                        _PLC_stuDataStore1 = CType(_TC.ReadAny(".ADS_stuDataStore", GetType(StructRequestAction)), StructRequestAction)
                        _Listchild.Add("1", New ChildElement(_PLC_stuDataStore1.stuPlcArticleSet.strSerialNr, _PLC_stuDataStore1.stuPlcArticleSet.strKostalNr))

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

        Private _ErrorMsg As String
        Private _ErrorCode As String
        Public Function CheckTrigInfoAndSelectLocalArticle(ByVal _i As Station, ByVal LocalArticle As Article, ByVal TrigSignal As Dictionary(Of String, Object), ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, Base.IStationTypeBase)) As SelectLocalArticleType Implements Base.ICheckTrigInfo.CheckTrigInfoAndSelectLocalArticle
            Try
                Select Case _i.Name

                    Case "SR710_01"
                        If CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If

                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr Then
                            LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = ""
                            For Each element As ArticleListElement In LocalArticle.ArticleListElement.Values
                                If element.ID.IndexOf(CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr) >= 0 Then
                                    If Not LocalArticle.GetArticle_FromID(element.ID) Then
                                        _ErrorMsg = _i.Name + "  LocalArticle.GetArticle_FromID Failure"
                                        Return SelectLocalArticleType.SelectFail
                                    End If
                                    Exit For
                                End If
                            Next
                        End If

                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = "" Then
                            Throw New Exception("LocalArticle.GetArticle_FromID Failure")
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change


                    Case "ICT"
                        If CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If
                        If CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr = "" Then
                            _ErrorMsg = _i.Name + "  strSerialNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If

                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        Return SelectLocalArticleType.UserDefineSelect
                    Case "SCREW"
                        If CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If
                        If CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr = "" Then
                            _ErrorMsg = _i.Name + "  strSerialNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = CType(TrigSignal("_ManualReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change

                    Case "SR752_23"
                        If CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If

                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr Then
                            LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = ""
                            For Each element As ArticleListElement In LocalArticle.ArticleListElement.Values
                                If element.ID.IndexOf(CType(TrigSignal("_ReadStructDeviceInteraction"), StructDeviceInteraction).stuPlcArticleSet.strKostalNr) >= 0 Then
                                    If Not LocalArticle.GetArticle_FromID(element.ID) Then
                                        _ErrorMsg = _i.Name + "  LocalArticle.GetArticle_FromID Failure"
                                        Return SelectLocalArticleType.SelectFail
                                    End If
                                    Exit For
                                End If
                            Next
                        End If

                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = "" Then
                            Throw New Exception("LocalArticle.GetArticle_FromID Failure")
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change

                    Case "QGW23"
                        If CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr = "" Then
                            _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If

                        If CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr = "" Then
                            _ErrorMsg = _i.Name + "  strSerialNr is Null"
                            Return SelectLocalArticleType.SelectFail
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strSerialNr
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal("_ReadStructRequestAction"), StructRequestAction).stuPlcArticleSet.strKostalNr
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
                               ' strMsg = "请选择是否手动补打螺丝. 不良螺丝数为:" + PLC_OUT_WT.PartFailValue
                            Case 5
                                '   strMsg = "请选择手动补打螺丝是否成功?"
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
                Select Case _i.Name
                    Case "ManualReTest"
                        _ReTestStation = CType(Stations("ReTest"), ReTestStation)
                        _ReTestStation.ManualVariantInfo = CType(Stations("ManualReTest"), ManualReTestStation).ReadStructDeviceInteraction.stuPlcArticleSet
                        _ScheduleManager = CType(Stations(ScheduleManager.Name), ScheduleManager)
                        _ScheduleManager.InsertChangeIndicatedName("RetestMode12")
                        While _ScheduleManager.GetChangeIndicatedStatus("RetestMode12") <> enumChangeResult.PASS
                            System.Threading.Thread.Sleep(10)
                        End While
                End Select
                Return True
            Catch ex As Exception
                _ErrorMsg = ex.Message
                Return False
            End Try
        End Function

    End Class

End Module




