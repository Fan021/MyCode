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
        Public Shared DoQuery As String = "DoQuery"
        Public Shared SR752_01 As String = "SR752_01"
        Public Shared SR752_06 As String = "SR752_06"
        Private FileHander As New FileHandler
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
                    Case ScannerDefine.SR752_01
                        '  Return True
                        If mScannerResult.Length < 20 Then
                            _ErrorMsg = i.Name + "  SN Length is Wrong. Scanned Content:" + mScannerResult
                            Return False
                        End If

                        '=======================解析===============================

                        _Article = mScannerResult.Substring(0, 8)
                        _SN = mScannerResult.Substring(mScannerResult.IndexOf("/SN") + 3, 13)

                        '=======================选择是否检查其他信息======================
                        '获取SN
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data.IndexOf(ScannerDefine.DoQuery) >= 0 Then
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SN
                            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data <> _Article Then
                                _ErrorMsg = i.Name + "  Article Match is Wrong. Label Article: " + _Article + " PLC Article: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                                Return False
                            End If
                        Else
                            '比较SN
                            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data <> _SN Then
                                _ErrorMsg = i.Name + "  SN Match is Wrong. Label SN: " + _SN + " PLC SN: " + _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                                Return False
                            End If
                        End If

                        '赋值给系统

                        Return True


                    Case ScannerDefine.SR752_06
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

                    Case "Zebra_1"
                        '=======================设置Print Field:注意Fields大小====================
                        ReDim Fields(8)

                        Dim Barcode As String = String.Empty

                        Barcode = "/P" + LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data.Replace("-", "") _
                                  + "/M10.00" _
                                  + "/" + "" _
                                  + "/A004" _
                                  + "/HW" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data _
                                  + "/SW" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data _
                                  + "/SN" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data

                        Fields(0) = LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        Fields(1) = "M10.00"
                        Fields(2) = ""
                        Fields(3) = "A004"
                        Fields(4) = "HW" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data
                        Fields(5) = "SW" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data
                        Fields(6) = "SN" & LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                        Fields(7) = Format(Date.Now, "yyyy.MM.dd")
                        Fields(8) = Barcode

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
                    Case "LaserEzd"
                        Dim strCustomer As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                        Dim strColor_No As String = _LocalArticle.ArticleElements("Color_No").Data
                        Dim Trademark As String = _LocalArticle.ArticleElements("Trademark").Data
                        Dim strHW As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data
                        Dim strSW As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data
                        Dim StrArticle_NO As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
                        Dim StrSN As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data.Trim
                        Dim Production_Date As String = Format(Date.Now, "ddMMyy")
                        Dim StrIndex As String = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
                        Dim Barcode_2D As String = "3#" + strCustomer + "  " + strColor_No + "#H" + strHW + "S" + strSW + "#542963759#" + Production_Date + "=/3OS" + StrArticle_NO + "-" + StrIndex + "/SN" + StrSN

                        'Replace(strCustomer, ".", "")
                        mCmd = String.Format("01,{0};02,{1};03,{2};04,{3};05,{4};06,{5};07,{6};08,{7};09,{8}", strCustomer, strColor_No, Trademark, "HW:" + strHW, "SW:" + strSW, StrArticle_NO.Substring(0, 4), StrArticle_NO.Substring(4, 4), StrSN, Barcode_2D)

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
                        '    mCmd = "RUN R5F109BC.FRS"
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

    Public Class GetMulitSNDefine
        Implements IGetMulitSNDefine
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

        Public Function GetAllFieldsOfMulitSN(_i As Station, LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds() As String) As Boolean Implements IGetMulitSNDefine.GetAllFieldsOfMulitSN
            ReDim fileds(5)

            fileds(0) = "AAAAAAAAA" + "01"
            fileds(1) = "AAAAAAAAA" + "01"
            fileds(2) = "AAAAAAAAA" + "01"
            fileds(3) = "AAAAAAAAA" + "01"
            fileds(4) = "AAAAAAAAA" + "01"
            fileds(5) = "AAAAAAAAA" + "01"
            Return True
        End Function
    End Class

    Public Class RunMulitSNDefine
        Implements IRunMulitSNDefine
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

        Public Function RunAllFieldsOfMulitSN(_i As Station, LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), fileds() As String) As Boolean Implements IRunMulitSNDefine.RunAllFieldsOfMulitSN
            Return True
        End Function
    End Class

    Public Class ReprintMulitDefine
        Implements IReprintMulitDefine
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

        Public Function ReprintFields(_i As Station, LocalArticle As Article, Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), SN As String) As Boolean Implements IReprintMulitDefine.ReprintFields

            Return True
        End Function
    End Class

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

                    Case "Article"
                        Dim A As String = "中午"
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
                    Case "UserDefine", "SR752_01", "Printer01"
                        AppArticle = CType(Devices(Article.Name), Article)
                        If CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = "" Then
                            CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                            ' _ErrorMsg = _i.Name + "  strKostalNr is Null"
                            ' Return SelectLocalArticleType.SelectFail
                        End If
                        LocalArticle.GetArticle_FromID(CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strKostalNr)
                        If LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = "" Then
                            Throw New Exception("LocalArticle.GetArticle_FromID Failure")
                        End If
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strUserDefine
                        LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal(enumDeviceTrigerName._ReadStructDeviceInteraction.ToString), StructDeviceInteraction).stuPlcArticleSet.strSerialNr
                        Return SelectLocalArticleType.UserDefineSelect  'Don't Change

                        'Case "QGW11" '适用于 PLC 传递的变种不再变种List里面 例如PCBA变种
                        '    If CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr = "" Then
                        '        _ErrorMsg = _i.Name + "  strKostalNr is Null"
                        '        Return SelectLocalArticleType.SelectFail
                        '    End If

                        '    If CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strSerialNr = "" Then
                        '        _ErrorMsg = _i.Name + "  strSerialNr is Null"
                        '        Return SelectLocalArticleType.SelectFail
                        '    End If
                        '    LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strSerialNr
                        '    LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr
                        '    LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strKostalNr
                        '    LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = CType(TrigSignal(enumRequestTrigerName._ReadStructRequestAction.ToString), StructRequestAction).stuPlcArticleSet.strUserDefine
                        '    Return SelectLocalArticleType.UserDefineSelect  'Don't Change

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
                    Case "ManualScaner01"
                        strMsg = "扫描总成条码"
                        Return True
                    Case "ManualScaner02"
                        strMsg = "请扫描EV标签"
                        Return True
                    Case "ManualScaner03"
                        strMsg = "请扫描OS标签"
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
                    Case "SR752_01"
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




