
'TwinCat Communication
'Author Frank Dümpelmann, added Module of LAS_PLC_Interface by wang65
'
' V2.0.0.0 Build 2012_02_02_00
' Add Second Sub New
'
'TC means TwinCat

' A Transfer Area is a logical Block for a information group, like Articledata, WT Data, LineLogger Data,
'	Status Data, ...

' A Transfer Area contains x Numbers of Section
'	A Section is a targetaddress for each User/Station. For Example : Station 01 / Station 02 / WT Info / ...

' If a WT with more than one Part, a Section can contains div. Partitions for each part on the WT.
'	For Example : in "Transfer Area Status"  in "Section 1" there is a WT with eight Partitions/Parts
'	So the NewPart Information in	190.0 is for Partition 1
'							...
'							until	190.7 is for Partition 8
'====================================================================================================================

Imports System.Runtime.InteropServices

Imports System.Reflection
Imports Kostal.Las.Article
Imports Kostal.Las.ArticleProvider
Imports Kostal.Las.Common
'========================================================================
'
'(***============================================================ ***)
'(***=====LAS PLC INTERFACE INTERNAL VERSION0.07 2015.07.18=====***)
'(***============================================================ ***)
'
'========================================================================
#Region "LAS PLC INTERFACE IV0.07"

Public Module LAS_PLC_Interface

#Region "LAS_PLC_BaseConstants"
    '====================================================================
    '====================================================================
    '-----------------FORM PLC,VERY IMPORTANT---------------------------
    Public Const CON_MAXIMUM_SCHEDULES As UInteger = 50             'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_TOTAL_CARRIERS As UInteger = 100       'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_REAL_CARRIERS As UInteger = 50         'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_TOTAL_STATIONS As UInteger = 100       'DON'T CHANGE ME!!    

    '====================================================================
    '====================================================================
    '----------------------------WT: CARRIER-----------------------------
    Public Const CON_MAXIMUM_ASSEMBLY_STATIONS As UInteger = 20     'Unused
    Public Const CON_MAXIMUM_REAL_STATIONS As UInteger = 20         'You can change here due to actual readers in your line
    Public Const CON_MAXIMUM_WT_RUNNING_CYCLES As UInteger = 1      'You can change here due to how many cycles for WT to carry a DUT from the begining to the end.
    Public Const CON_MAXIMUM_AVAIABLE_STATIONS As UInteger = CON_MAXIMUM_REAL_STATIONS * CON_MAXIMUM_WT_RUNNING_CYCLES       'DON'T CHANGE ME!!

    '====================================================================
    '====================================================================
    '-----------------------STRING LENGTH RELATED------------------------
    Public Const CON_STRING_LENGTH_21 As Integer = 21               'DON'T CHANGE ME!!
    Public Const CON_STRING_LENGTH_256 As Integer = 256             'DON'T CHANGE ME!!
    Public Const CON_STRING_LENGTH_241 As Integer = 241             'DON'T CHANGE ME!!
    Public Const CON_SERILANUMBER_LENGTH_51 As Integer = 51         'DON'T CHANGE ME!!
    Public Const CON_DOT As Char = "."c                             'DON'T CHANGE ME!!
    '====================================================================
    '====================================================================
#End Region


#Region "LAS_PLC_BaseAdsClass"

    Public Class KostalAdsVariables

        '================================================================
        'Important - First Ads Loading - change if necessary
        '================================================================
        'you can expand any amount of ads variables here.

        'written by PC only
        <AutomationVariable()> Public Const PC_stuVariantInfo As String = CON_DOT & "PC_stuCurrentVariantInfo"
        <AutomationVariable()> Public Const PC_bytScheduleNr As String = CON_DOT & "PC_bytCurrentScheduleNr"
        <AutomationVariable()> Public Const PC_arrScheduleList As String = CON_DOT & "PC_arrScheduleList"
        <AutomationVariable()> Public Const PC_bulNewPartAvailable As String = CON_DOT & "PC_bulNewPartAvailable"
        '<AutomationVariable()> Public Const PC_bulWriteResultIsPass As String = CON_DOT & "PC_bulWriteResultIsPass"
        '<AutomationVariable()> Public Const PC_bulWriteResultIsFail As String = CON_DOT & "PC_bulWriteResultIsFail"


        'written by PLC only
        '<AutomationVariable()> Public Const PLC_bulTestPass As String = CON_DOT & "PLC_bulTestPass"
        '<AutomationVariable()> Public Const PLC_bulTestFail As String = CON_DOT & "PLC_bulTestFail"
        <AutomationVariable()> Public Const PLC_bulGetNewPart As String = CON_DOT & "PLC_bulGetNewPart"
        <AutomationVariable()> Public Const PLC_stuAssmDataRequest As String = CON_DOT & "PLC_stuAssmDataRequest"
        <AutomationVariable()> Public Const PLC_stuFinishedPartRequest As String = CON_DOT & "PLC_stuFinishedPartRequest"


        'to be written by anyone is permitted.
        <AutomationVariable()> Public Const ADS_stuScannerSt01 As String = CON_DOT & "ADS_stuScannerSt01"
        <AutomationVariable()> Public Const ADS_stuScannerSt11 As String = CON_DOT & "ADS_stuScannerSt11"
        <AutomationVariable()> Public Const ADS_stuScannerSt12 As String = CON_DOT & "ADS_stuScannerSt12"
        <AutomationVariable()> Public Const ADS_stuPrinterSt14 As String = CON_DOT & "ADS_stuPrinterSt14"
        <AutomationVariable()> Public Const ADS_stuPrinterSt09 As String = CON_DOT & "ADS_stuPrinterSt09"
        <AutomationVariable()> Public Const ADS_stuAssmDataResponse As String = CON_DOT & "ADS_stuAssmDataResponse"
        <AutomationVariable()> Public Const ADS_stuFinishedPartResponse As String = CON_DOT & "ADS_stuFinishedPartResponse"

        '================================================================
        'Important Functions as below, DO NOT change them!!
        '================================================================
        Private Shared _variablelist As List(Of String)

        Shared Sub New()

            _variablelist = New List(Of String)
            ScanVariablesOnce()

        End Sub

        Public Shared Function GetAllAdsVariables() As IEnumerable(Of String)

            Return _variablelist

        End Function


        Private Shared Sub ScanVariablesOnce()

            Dim t As Type = GetType(KostalAdsVariables)

            Dim sp As FieldInfo() = t.GetFields()

            For Each f As FieldInfo In sp

                If f.IsDefined(GetType(AutomationVariableAttribute), True) Then
                    Try

                        Dim o As Object = f.GetValue(Nothing)
                        _variablelist.Add(o.ToString())

                    Catch ex As Exception
                        Dim strErr As String = ex.Message

                    End Try
                End If
            Next

        End Sub

        Private Class AutomationVariableAttribute : Inherits Attribute

            Public Sub New()
                'do nothing
            End Sub

        End Class

    End Class

#End Region


#Region "LAS_PLC_BaseStructs"
    '================================================================
    'Important - Structs    
    '================================================================

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructCarrierInfo
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytDestinationStation As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleModeNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytCycleNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytAssemblyCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTestResult As Boolean = False
        Public stuVariantInfoSet As StructVariantInfo
        Public stuFailedPartInfo As StructFailedPartInfo
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructFailedPartInfo
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailKostalNr As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_SERILANUMBER_LENGTH_51)> Public strFailSerialNr As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailScheduleName As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailTestStatus As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailCarrierNr As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailStationNr As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailTestStep As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailCode As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strFailText As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailValue As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailLowerLimit As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailUpperLimit As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailUnit As String = ""
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructScheduleMode
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleNr As Byte             '(* Kostal number without Index, like 10110201*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String          '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_SERILANUMBER_LENGTH_51)> Public strScheduleDescription As String       '(* Product family name  *)
        <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intSecurityChecksum As Short           '(* Security Checksum  *)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_STATIONS * 2)> Public arrScheduleData(0 To CON_MAXIMUM_TOTAL_STATIONS - 1, 0 To 1) As Byte            '(* Kostal serial number. like 13 digitals *)
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructVariantInfo
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalNr As String = ""            '(* Kostal number with/without Index, like 10110201(-01)*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalArticleName As String = ""   '(* Kostal article name, like B9_SCM*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strCustomerNr As String = ""         '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strProductFamily As String = ""      '(* Product family name  *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_SERILANUMBER_LENGTH_51)> Public strSerialNr As String = ""           '(* Kostal serial number. like 13 digitals *)
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructDeviceInteraction
        Public stuPlcArticleSet As StructVariantInfo
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strAdsActionValue As String = ""
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructRequestAction
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulRunning As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoPositiveAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoNegativeAction As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strActionScheduleName As String = ""
        Public stuPlcArticleSet As StructVariantInfo
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructResponseAction
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPartReceived As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strActionResultText As String = ""
    End Class

#End Region


#Region "StructAdsIndicator"

    Public Structure StructAdsNotification
        Public tlpContainer As TableLayoutPanel
        Public uiRowCount As UInteger
        Public uiColumnCount As UInteger
        Public dicAdsNames As Dictionary(Of String, String)
    End Structure

#End Region


End Module

#End Region


'======================================================
'
'TwinCatInterface
'
'======================================================
Public Class TwinCatInterface

    Private _Barcode As New Barcode_LK


#Region "DBE ADS Constants"

    Private Const TwinCatTransferId As String = "TwinCat"

    '========================================================================================
    'TransferArea LineLogger
    '========================================================================================

    Private Const TC_LineLogger_TransferArea_Offset_Read As Integer = 0
    Private Const TC_LineLogger_Section_Offset_Read As Integer = 80
    Private Const TC_LineLogger_TransferArea_Lenght_Read As Integer = 400

    Private Const TC_LineLogger_TransferArea_Offset_Write As Integer = 0
    Private Const TC_LineLogger_Section_Offset_Write As Integer = 0
    Private Const TC_LineLogger_TransferArea_Lenght_Write As Integer = 0

    '========================================================================================
    'TransferArea Article
    '========================================================================================

    Private Const TC_Article_TransferArea_Offset_Read As Integer = 400
    Private Const TC_Article_Section_Offset_Read As Integer = 0
    Private Const TC_Article_TransferArea_Lenght_Read As Integer = 0

    Private Const TC_Article_TransferArea_Offset_Write As Integer = 0
    Private Const TC_Article_Section_Offset_Write As Integer = 350
    Private Const TC_Article_TransferArea_Lenght_Write As Integer = 350


    '========================================================================================
    'TransferArea Status (binary Informations)
    '========================================================================================

    Private Const TC_Status_TransferArea_Offset_Read As Integer = 400
    Private Const TC_Status_TransferArea_Section_Offset_Read As Integer = 20
    Private Const TC_Status_TransferArea_Lenght_Read As Integer = 300


    Private Const TC_Status_TransferArea_Offset_Write As Integer = 350
    Private Const TC_Status_TransferArea_Section_Offset_Write As Integer = 20
    Private Const TC_Status_TransferArea_Lenght_Write As Integer = 300

    '========================================================================================
    'TransferArea Axis Data
    '========================================================================================

    Private Const TC_AxisData_TransferArea_Offset_Read As Integer = 700
    Private Const TC_AxisData_TransferArea_Section_Offset_Read As Integer = 16
    Private Const TC_AxisData_TransferArea_Lenght_Read As Integer = 32


    Private Const TC_AxisData_TransferArea_Offset_Write As Integer = 650
    Private Const TC_AxisData_TransferArea_Section_Offset_Write As Integer = 417
    Private Const TC_AxisData_TransferArea_Lenght_Write As Integer = 834


    '========================================================================================
    'TransferArea String Data
    '========================================================================================

    Private Const TC_StringData_TransferArea_Offset_Read As Integer = 732
    Private Const TC_StringData_TransferArea_Section_Offset_Read As Integer = 80
    Private Const TC_StringData_TransferArea_Lenght_Read As Integer = 1200

    Private Const TC_StringData_TransferArea_Offset_Write As Integer = 1484
    Private Const TC_StringData_TransferArea_Section_Offset_Write As Integer = 80
    Private Const TC_StringData_TransferArea_Lenght_Write As Integer = 1200


    '========================================================================================
    'TransferArea WT Info
    '========================================================================================

    Private Const TC_WT_Infos_TransferArea_Offset_Read As Integer = 1932
    Private Const TC_WT_Infos_TransferArea_Section_Offset_Read As Integer = 250
    Private Const TC_WT_Infos_TransferArea_Lenght_Read As Integer = 750

    Private Const TC_WT_Infos_TransferArea_Offset_Write As Integer = 2684
    Private Const TC_WT_Infos_TransferArea_Section_Offset_Write As Integer = 250
    Private Const TC_WT_Infos_TransferArea_Lenght_Write As Integer = 750


    '========================================================================================
    'TransferArea Central Info
    '========================================================================================

    Private Const TC_CentralInfo_Offset_Read As Integer = 2682
    Private Const TC_CentralInfo_Section_Offset_Read As Integer = 0
    Private Const TC_CentralInfo_Lenght_Read As Integer = 0

    Private Const TC_CentralInfo_Offset_Write As Integer = 3434
    Private Const TC_CentralInfo_Section_Offset_Write As Integer = 100
    Private Const TC_CentralInfo_Lenght_Write As Integer = 100


    '========================================================================================
    '========================================================================================
    'Public Data direct to other linked users
    '========================================================================================

    Private Const TC_Station_13_Scan_Write_Address As Integer = 5001
    Private Const TC_Station_13_Scan_Lenght As Integer = 101

    '========================================================================================

#End Region

    Public TC As TwinCatAdsCommnicator

    Private _Logger As Logger

    Private _StateInfo As String = String.Empty


#Region "Properties"

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return TC.IsConnected
        End Get
    End Property

#End Region


    Public Sub New(ByVal MySettings As Settings, ByVal lstAdsNotifications As IEnumerable(Of StructAdsNotification))


        _Logger = New Logger(MySettings)

        TC = New TwinCatAdsCommnicator(TwinCatTransferId, MySettings, KostalAdsVariables.GetAllAdsVariables, lstAdsNotifications)

        If Not TC.IsConnected Then
            _StateInfo = TC.StateInfo
        End If

    End Sub

    Public Sub Dispose()

        TC.Dispose()

    End Sub

    Public Function Read() As Boolean

        'If Not TC.Read() Then
        '    _StateInfo = TC.StateInfo
        '    Return False
        'End If

        TC_Common_Read()



        Return True

    End Function


    Public Function Write() As Boolean

        'If ST01_NewPart.WriteScheduleDataToTwinCat Then
        '    If Not TC_ScheduleList_Write() Then Return False
        '    If Not ReadWrite() Then Return False


        'End If

        'If ST01_NewPart.WriteArticleDataToTwinCat Then
        '    ''Note: not to write VariantList but an elment of it.
        '    If Not TC_VariantInfo_Write() Then Return False

        'End If

        'If Not TC_Common_Write() Then
        '    _StateInfo = TC.StateInfo
        '    Return False
        'End If
        'If Not ReadWrite() Then
        '    _StateInfo = TC.StateInfo
        '    Return False
        'End If
        'Return True

    End Function


    Public Function ReadWrite() As Boolean
        'Dim bulRes As Boolean = True

        'If TC.IsDisabled Then Return bulRes
        ''=======================================================================================================
        ''ST08_Scanner
        ''=======================================================================================================
        'bulRes = ReadAndWriteSTxxScanner(KostalAdsVariables.ADS_stuScannerSt01, ST01_Scanner)
        'If Not bulRes Then Return False

        ' ''=======================================================================================================
        ' ''ST11_Scanner
        ' ''=======================================================================================================
        ''bulRes = ReadAndWriteSTxxScanner(KostalAdsVariables.ADS_stuScannerSt11, ST11_Scanner)
        ''If Not bulRes Then Return False

        ' ''=======================================================================================================
        ' ''ST12_Scanner
        ' ''=======================================================================================================
        ''bulRes = ReadAndWriteSTxxScanner(KostalAdsVariables.ADS_stuScannerSt12, ST12_Scanner)
        ''If Not bulRes Then Return False

        ''=======================================================================================================
        ''ST14_Laser
        ''=======================================================================================================
        'bulRes = ReadAndWriteSTxxPrinter(KostalAdsVariables.ADS_stuPrinterSt14, ST14_Laser)
        'If Not bulRes Then Return False

        ''=======================================================================================================
        ''ST09_Laser
        ''=======================================================================================================
        'bulRes = ReadAndWriteSTxxPrinter(KostalAdsVariables.ADS_stuPrinterSt09, ST09_Laser)
        'If Not bulRes Then Return False

        ''Dim deviceSet As StructDeviceInteraction
        ''With ST20_Scanner
        ''    deviceSet = TC.ReadDeviceInteractionSet(KostalAdsVariables.ADS_stuScannerSt08)
        ''    .PLC_OUT_DoScan = True 'deviceSet.bulPlcDoAction
        ''    .PLC_OUT_WtArticle = "10128543-01" 'deviceSet.stuPlcArticleSet.strKostalNr
        ''    .PLC_OUT_WtSerialNumber = "5JT-KLO24.07.1500000040" 'deviceSet.stuPlcArticleSet.strSerialNr
        ''    If .WriteActionDataToTwinCat Then
        ''        deviceSet.strAdsActionValue = .PLC_IN_ScanResult
        ''        deviceSet.bulAdsActionIsPass = .PLC_IN_ScanIsPass
        ''        deviceSet.bulAdsActionIsFail = .PLC_IN_ScanIsFail
        ''        bulRes = TC_DeviceInteraction_Write(KostalAdsVariables.ADS_stuScannerSt08, deviceSet)
        ''        If Not bulRes Then Return False
        ''        .WriteActionDataToTwinCat = False
        ''    End If
        ''    deviceSet = Nothing
        ''End With

        'Return bulRes

    End Function

    'Private Function ReadAndWriteSTxxScanner(ByVal handleName As String, ByRef stationScanner As StationTyp_ScanAndVerifyViaKeyenceScanner) As Boolean
    '    Dim bulRes As Boolean = True
    '    Dim deviceSet As StructDeviceInteraction

    '    If stationScanner Is Nothing Then Return False
    '    '=======================================================================================================
    '    'STXX_Scanner
    '    '=======================================================================================================
    '    With stationScanner
    '        deviceSet = TC.ReadDeviceInteractionSet(handleName)
    '        If deviceSet Is Nothing Then Return False
    '        .PLC_OUT_DoScan = deviceSet.bulPlcDoAction
    '        .PLC_OUT_WtArticle = deviceSet.stuPlcArticleSet.strKostalNr
    '        .PLC_OUT_WtSerialNumber = deviceSet.stuPlcArticleSet.strSerialNr

    '        If .WriteActionDataToTwinCat Then
    '            deviceSet.strAdsActionValue = .PLC_IN_ScanResult
    '            deviceSet.bulAdsActionIsPass = .PLC_IN_ScanIsPass
    '            deviceSet.bulAdsActionIsFail = .PLC_IN_ScanIsFail
    '            bulRes = TC_DeviceInteraction_Write(handleName, deviceSet)
    '            If Not bulRes Then Return False
    '            .WriteActionDataToTwinCat = False
    '        End If
    '    End With
    '    deviceSet = Nothing

    '    Return bulRes

    'End Function

    'Private Function ReadAndWriteSTxxPrinter(ByVal handleName As String, ByRef stationPrinter As StationTyp_SetTemplateAndVariablesForLaser) As Boolean
    '    Dim bulRes As Boolean = True
    '    Dim deviceSet As StructDeviceInteraction

    '    If stationPrinter Is Nothing Then Return False
    '    '=======================================================================================================
    '    'STXX_Printer
    '    '=======================================================================================================
    '    With stationPrinter
    '        deviceSet = TC.ReadDeviceInteractionSet(handleName)
    '        If deviceSet Is Nothing Then Return False
    '        '.PLC_OUT_DoPrint = True
    '        '.PLC_OUT_WtArticle = "10128574-01"
    '        '.PLC_OUT_WtSerialNumber = "123654"
    '        .PLC_OUT_SettingLaserRequired = deviceSet.bulPlcDoAction
    '        .PLC_OUT_WtArticle = deviceSet.stuPlcArticleSet.strKostalNr
    '        .PLC_OUT_WtSerialNumber = deviceSet.stuPlcArticleSet.strSerialNr
    '        .PLC_OUT_WtCustomerNr = deviceSet.stuPlcArticleSet.strCustomerNr
    '        If .WriteResultDataToTwinCat Then
    '            'deviceSet.strAdsActionValue = .PLC_IN_PrintResult
    '            deviceSet.bulAdsActionIsPass = .PLC_IN_SettingLaserIsPass
    '            deviceSet.bulAdsActionIsFail = .PLC_IN_SettingLaserIsFail
    '            bulRes = TC_DeviceInteraction_Write(handleName, deviceSet)
    '            If Not bulRes Then Return False
    '            .WriteResultDataToTwinCat = False
    '        End If
    '    End With
    '    deviceSet = Nothing

    '    Return bulRes

    'End Function

#Region "Station 13 Scan"


    'Private Sub TC_Station_13_Scan_Write()

    '      TC.WriteAs_STRING(Station_13.LastCode(0), TC_Station_13_Scan_Write_Address, TC_Station_13_Scan_Write_Address + TC_Station_13_Scan_Lenght)

    'End Sub


#End Region


#Region "TC_LineLogger"


    Private Sub TC_LineLogger_Read()

        Dim Offset As Integer

        On Error Resume Next

        '=======================================================================================================
        'LineLogger Module 1
        '=======================================================================================================
        Offset = 0 * TC_LineLogger_Section_Offset_Read + TC_LineLogger_TransferArea_Offset_Read


        '=======================================================================================================
        'LineLogger Module 2
        '=======================================================================================================
        Offset = 1 * TC_LineLogger_Section_Offset_Read + TC_LineLogger_TransferArea_Offset_Read

     
        '=======================================================================================================
        'LineLogger Module 3
        '=======================================================================================================
        Offset = 2 * TC_LineLogger_Section_Offset_Read + TC_LineLogger_TransferArea_Offset_Read


        '=======================================================================================================
        'LineLogger Module 4
        '=======================================================================================================
        Offset = 3 * TC_LineLogger_Section_Offset_Read + TC_LineLogger_TransferArea_Offset_Read



        On Error GoTo 0
    End Sub


    Private Sub TC_LineLogger_Write()

        On Error Resume Next


        On Error GoTo 0

    End Sub


#End Region


#Region "TC_Article"


    Private Sub TC_Article_Read()

        On Error Resume Next

        On Error GoTo 0

    End Sub

    Private Function TC_ScheduleList_Write() As Boolean

        Dim bulRes As Boolean

        bulRes = TC.WriteScheduleMode(KostalAdsVariables.PC_arrScheduleList, LAS_arrScheduleList)

        '    If bulRes Then ST01_NewPart.WriteScheduleDataToTwinCat = False

        Return bulRes

    End Function

    Private Function TC_VariantInfo_Write() As Boolean

        Dim bulRes As Boolean
        Dim variantInfo As New StructVariantInfo

        'variantInfo.strKostalNr = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_ID).Data
        'variantInfo.strKostalArticleName = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_ARTICLE_NAME).Data
        'variantInfo.strCustomerNr = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
        ''variantInfo.strTag = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_TAG).Data
        '' variantInfo.strWheelPos = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data
        'variantInfo.strSerialNr = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
        'variantInfo.strProductFamily = ST01_NewPart.LocalArticle.Compoment(KostalArticleKeys.KEY_ARTICLE_INFO).Data

        'bulRes = TC.WriteVariantInfo(KostalAdsVariables.PC_stuVariantInfo, variantInfo)

        'If bulRes Then ST01_NewPart.WriteArticleDataToTwinCat = False

        Return bulRes
    End Function


    Private Function TC_DeviceInteraction_Write(ByVal handleName As String, ByVal stuDeviceSet As StructDeviceInteraction) As Boolean

        Dim bulRes As Boolean

        bulRes = TC.WriteDeviceInteraction(handleName, stuDeviceSet)

        Return bulRes

    End Function


    'Private Sub TC_Article_Write()

    '    Dim Offset As Integer

    '    On Error Resume Next

    '    '=======================================================================================================
    '    'Station 01
    '    '=======================================================================================================
    '    Offset = 0 * TC_Article_Section_Offset_Write + TC_Article_TransferArea_Offset_Write

    '    TC.WriteAs_STRING(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_ID).Data, Offset + 0, 19)
    '    TC.WriteAs_STRING(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SERIALNUMBER).Data, Offset + 20, 39)

    '    If Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHEDULE).Data.Contains(ScheduleInterface.AlternateScheduleString) Then
    '        TC.WriteAs_STRING(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHEDULE).Data.Replace(ScheduleInterface.AlternateScheduleString, ""), Offset + 40, 59)
    '    Else
    '        TC.WriteAs_STRING("Normal Production", Offset + 40, 59)   'changed String.Empty to ""  by wang65 20150130
    '    End If



    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S12).Data) <> "", Offset + 100, 0)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S13).Data) <> "", Offset + 100, 1)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S14).Data) <> "", Offset + 100, 2)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S15).Data) <> "", Offset + 100, 3)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S16).Data) <> "", Offset + 100, 4)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S17).Data) <> "", Offset + 100, 5)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S18).Data) <> "", Offset + 100, 6)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S19).Data) <> "", Offset + 100, 7)

    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S20).Data) <> "", Offset + 101, 0)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S21).Data) <> "", Offset + 101, 1)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S22).Data) <> "", Offset + 101, 2)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S23).Data) <> "", Offset + 101, 3)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S24).Data) <> "", Offset + 101, 4)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S25).Data) <> "", Offset + 101, 5)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S26).Data) <> "", Offset + 101, 6)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S27).Data) <> "", Offset + 101, 7)

    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S28).Data) <> "", Offset + 102, 0)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S29).Data) <> "", Offset + 102, 1)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S30).Data) <> "", Offset + 102, 2)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S31).Data) <> "", Offset + 102, 3)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S32).Data) <> "", Offset + 102, 4)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S33).Data) <> "", Offset + 102, 5)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S34).Data) <> "", Offset + 102, 6)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S35).Data) <> "", Offset + 102, 7)

    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S36).Data) <> "", Offset + 103, 0)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S37).Data) <> "", Offset + 103, 1)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S38).Data) <> "", Offset + 103, 2)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S39).Data) <> "", Offset + 103, 3)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S40).Data) <> "", Offset + 103, 4)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S41).Data) <> "", Offset + 103, 5)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S42).Data) <> "", Offset + 103, 6)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S43).Data) <> "", Offset + 103, 7)


    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_CABLE_LAYING).Data) <> "", Offset + 104, 0)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_OPTICLA_FIBRE).Data) <> "", Offset + 104, 1)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_CASE_TAXI).Data) <> "", Offset + 104, 2)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_FLOCKED).Data) <> "", Offset + 104, 3)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_COLOR_SHD).Data) <> "", Offset + 104, 4)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_VARIANT_SHD).Data) <> "", Offset + 104, 5)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_03_REPAIR_FAIL_SWITCH_MAT).Data) <> "", Offset + 104, 6)
    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_HOUSING_TAXIMETER).Data) <> "", Offset + 104, 7)


    '    TC.WriteAs_BOOL( _
    '     (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_HIGH_BLACK).Data) <> "") _
    '     Or (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_HIGH_GREIGE).Data) <> "") _
    '     Or (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_HIGH_PORCELAIN).Data) <> "") _
    '     Or (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_LOW_BLACK).Data) <> "") _
    '     Or (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_LOW_GREIGE).Data) <> "") _
    '     Or (_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_BUTTON_CARRIER_Teleaid_LOW_PORCELAIN).Data) <> "") _
    '     , Offset + 105, 0)

    '    TC.WriteAs_BOOL(_Logger.Remove(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S44).Data) <> "", Offset + 105, 1)


    '    If IsDisabledControl.WriteDataSet Then

    '        Dim _Byte As Integer = 0, _Bit As Integer = 0

    '        For Each dc As DisabledComponent In IsDisabledControl.DisabledComponents

    '            TC.WriteAs_BOOL(dc.IsDisabled, Offset + 110 + _Byte, _Bit)

    '            _Bit += 1

    '            If _Bit > 7 Then
    '                _Bit = 0
    '                _Byte += 1
    '            End If

    '        Next

    '        IsDisabledControl.WriteDataSet = False

    '    End If


    '    TC.WriteAs_BYTE(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_05_MASK).Data, Offset + 180)
    '    TC.WriteAs_BYTE(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_STATION_06_MASK).Data, Offset + 181)
    '    TC.WriteAs_BYTE(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_3_3S36_BANK).Data, Offset + 182)
    '    TC.WriteAs_BYTE(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_CONNECTOR).Data, Offset + 183)

    '    '---------------------------------------------------------------------------------------------------------------------------------------------------
    '    'Schedule ------------------------------------------------------------------------------------------------------------------------------------------

    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_01).Data, Offset + 300)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_01).Data, Offset + 301)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_02).Data, Offset + 302)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_02).Data, Offset + 303)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_03).Data, Offset + 304)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_03).Data, Offset + 305)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_04).Data, Offset + 306)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_04).Data, Offset + 307)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_05).Data, Offset + 308)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_05).Data, Offset + 309)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_06).Data, Offset + 310)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_06).Data, Offset + 311)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_07).Data, Offset + 312)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_07).Data, Offset + 313)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_08).Data, Offset + 314)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_08).Data, Offset + 315)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_09).Data, Offset + 316)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_09).Data, Offset + 317)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_10).Data, Offset + 318)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_10).Data, Offset + 319)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_11).Data, Offset + 320)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_11).Data, Offset + 321)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_12).Data, Offset + 322)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_12).Data, Offset + 323)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_13).Data, Offset + 324)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_13).Data, Offset + 325)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_PASS_STAT_14).Data, Offset + 326)
    '    TC.WriteAs_USINT(Station_01.LocalArticle.Compoment(Station_01.LocalArticle.Keys.KEY_SCHED_FAIL_STAT_14).Data, Offset + 327)

    '    Station_01.WriteArticleDataToTwinCat = False

    '    On Error GoTo 0
    'End Sub


#End Region


#Region "TC_WT_Info"


    Private Sub TC_WT_Info_Read()

        'Dim Offset As Integer

        'On Error Resume Next

        ''=======================================================================================================
        ''Station_01_QGW
        ''=======================================================================================================
        'Offset = 0 * TC_WT_Infos_TransferArea_Section_Offset_Read + TC_WT_Infos_TransferArea_Offset_Read

        'ST01_QGW.PLC_OUT_WT.Number = TC.ReadFrom_BYTE(Offset + 0)

        'ST01_QGW.PLC_OUT_WT.ArticleNumber = TC.ReadFrom_STRING(Offset + 10, 20)
        'ST01_QGW.PLC_OUT_WT.SerialNumber = TC.ReadFrom_STRING(Offset + 30, 20)
        'ST01_QGW.PLC_OUT_WT.Schedule = TC.ReadFrom_STRING(Offset + 50, 20)
        'ST01_QGW.PLC_OUT_WT.Status = TC.ReadFrom_STRING(Offset + 70, 20)
        'ST01_QGW.PLC_OUT_WT.Target = TC.ReadFrom_STRING(Offset + 90, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailLocation = TC.ReadFrom_STRING(Offset + 110, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailTestStep = TC.ReadFrom_STRING(Offset + 130, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailCode = TC.ReadFrom_STRING(Offset + 150, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailText = TC.ReadFrom_STRING(Offset + 170, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailValue = TC.ReadFrom_STRING(Offset + 190, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailLowerLimit = TC.ReadFrom_STRING(Offset + 210, 20)
        'ST01_QGW.PLC_OUT_WT.PartFailUpperLimit = TC.ReadFrom_STRING(Offset + 230, 20)



        ''=======================================================================================================
        ''Station 15 QGW
        ''=======================================================================================================
        'Offset = 1 * TC_WT_Infos_TransferArea_Section_Offset_Read + TC_WT_Infos_TransferArea_Offset_Read

        'ST20_QGW.PLC_OUT_WT.Number = TC.ReadFrom_BYTE(Offset + 0)

        'ST20_QGW.PLC_OUT_WT.ArticleNumber = TC.ReadFrom_STRING(Offset + 10, 20)
        'ST20_QGW.PLC_OUT_WT.SerialNumber = TC.ReadFrom_STRING(Offset + 30, 20)
        'ST20_QGW.PLC_OUT_WT.Schedule = TC.ReadFrom_STRING(Offset + 50, 20)
        'ST20_QGW.PLC_OUT_WT.Status = TC.ReadFrom_STRING(Offset + 70, 20)
        'ST20_QGW.PLC_OUT_WT.Target = TC.ReadFrom_STRING(Offset + 90, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailLocation = TC.ReadFrom_STRING(Offset + 110, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailTestStep = TC.ReadFrom_STRING(Offset + 130, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailCode = TC.ReadFrom_STRING(Offset + 150, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailText = TC.ReadFrom_STRING(Offset + 170, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailValue = TC.ReadFrom_STRING(Offset + 190, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailLowerLimit = TC.ReadFrom_STRING(Offset + 210, 20)
        'ST20_QGW.PLC_OUT_WT.PartFailUpperLimit = TC.ReadFrom_STRING(Offset + 230, 20)


        ''=======================================================================================================
        ''WT Info
        ''=======================================================================================================
        'Offset = 2 * TC_WT_Infos_TransferArea_Section_Offset_Read + TC_WT_Infos_TransferArea_Offset_Read

        'WatchWT.WtInfo.Number = TC.ReadFrom_BYTE(Offset + 0)

        'WatchWT.WtInfo.ArticleNumber = TC.ReadFrom_STRING(Offset + 10, 20)
        'WatchWT.WtInfo.SerialNumber = TC.ReadFrom_STRING(Offset + 30, 20)
        'WatchWT.WtInfo.Schedule = TC.ReadFrom_STRING(Offset + 50, 20)
        'WatchWT.WtInfo.Status = TC.ReadFrom_STRING(Offset + 70, 20)
        'WatchWT.WtInfo.Target = TC.ReadFrom_STRING(Offset + 90, 20)
        'WatchWT.WtInfo.PartFailLocation = TC.ReadFrom_STRING(Offset + 110, 20)
        'WatchWT.WtInfo.PartFailTestStep = TC.ReadFrom_STRING(Offset + 130, 20)
        'WatchWT.WtInfo.PartFailCode = TC.ReadFrom_STRING(Offset + 150, 20)
        'WatchWT.WtInfo.PartFailText = TC.ReadFrom_STRING(Offset + 170, 20)
        'WatchWT.WtInfo.PartFailValue = TC.ReadFrom_STRING(Offset + 190, 20)
        'WatchWT.WtInfo.PartFailLowerLimit = TC.ReadFrom_STRING(Offset + 210, 20)
        'WatchWT.WtInfo.PartFailUpperLimit = TC.ReadFrom_STRING(Offset + 230, 20)

        On Error GoTo 0
    End Sub



    Private Sub TC_WT_Info_Write()

        Dim Offset As Integer

        '=======================================================================================================
        'WT Info
        '=======================================================================================================
        Offset = 2 * TC_WT_Infos_TransferArea_Section_Offset_Write + TC_WT_Infos_TransferArea_Offset_Write

        On Error Resume Next

        TC.WriteAs_BYTE(WatchWT.WtNumberRequest, Offset + 0)


        On Error GoTo 0
    End Sub


#End Region


#Region "TC_StringData"


    'Private Sub TC_StringData_Read()

    '    Dim Offset As Integer

    '    On Error Resume Next

    '    '=======================================================================================================
    '    'Station_01
    '    '=======================================================================================================

    '    Offset = 0 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_01_ReadButtonSet.PLC_OUT_ArticleModul = AppArticle.Compoment(Main.Artikel_Keys.KEY_ID).Data
    '    Station_01_ReadButtonSet.PLC_OUT_ArticleComponent = AppArticle.Compoment(Main.Artikel_Keys.KEY_BUTTON_TRAY_ARTICLE).Data + "-" + AppArticle.Compoment(Main.Artikel_Keys.KEY_BUTTON_TRAY_INDEX).Data

    '    '=======================================================================================================
    '    'Station_02
    '    '=======================================================================================================

    '    Offset = 1 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    '=======================================================================================================
    '    'Station_03
    '    '=======================================================================================================

    '    Offset = 2 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_03.PLC_OUT_WtArticle = TC.ReadFrom_STRING(Offset + 0, 20)
    '    Station_03.PLC_OUT_WtSerialNumber = TC.ReadFrom_STRING(Offset + 20, 20)

    '    '=======================================================================================================
    '    'Station_04
    '    '=======================================================================================================

    '    Offset = 3 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_04.PLC_OUT_ArticleModul = TC.ReadFrom_STRING(Offset + 0, 20)
    '    Station_04.PLC_OUT_ArticleComponent = Station_04.LocalArticle.Components(Artikel_Keys.KEY_BG_NR_PCB).Data

    '    '=======================================================================================================
    '    'Station_05
    '    '=======================================================================================================

    '    Offset = 4 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_05.PLC_OUT_Article = TC.ReadFrom_STRING(Offset + 0, TC_StringData_TransferArea_Section_Offset_Read)

    '    '=======================================================================================================
    '    'Station_06
    '    '=======================================================================================================

    '    Offset = 5 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_06.PLC_OUT_Article = TC.ReadFrom_STRING(Offset + 0, TC_StringData_TransferArea_Section_Offset_Read)

    '    '=======================================================================================================
    '    'Station_07
    '    '=======================================================================================================

    '    Offset = 6 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    '=======================================================================================================
    '    'Station_13
    '    '=======================================================================================================
    '    Offset = 12 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read

    '    Station_13.PLC_OUT_WtArticle = TC.ReadFrom_STRING(Offset + 0, 20)
    '    Station_13.PLC_OUT_WtSerialNumber = TC.ReadFrom_STRING(Offset + 20, 20)

    '    '=======================================================================================================
    '    'Station_16  (Location at Station_09 added by wang65 2015.01.27)
    '    '=======================================================================================================
    '    Offset = 8 * TC_StringData_TransferArea_Section_Offset_Read + TC_StringData_TransferArea_Offset_Read  'changed by wang65 20141111 from 11 to 8

    '    Station_16.PLC_OUT_WtArticle = TC.ReadFrom_STRING(Offset + 0, 20)
    '    Station_16.PLC_OUT_WtSerialNumber = TC.ReadFrom_STRING(Offset + 20, 20)
    '    Station_16.PLC_OUT_WtSchedule = TC.ReadFrom_STRING(Offset + 40, 20)


    '    On Error GoTo 0
    'End Sub



    Private Sub TC_StringData_Write()

        Dim Offset As Integer

        On Error Resume Next

        '=======================================================================================================
        'Station 01
        '=======================================================================================================
        Offset = 0 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 02
        '=======================================================================================================
        Offset = 1 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 03
        '=======================================================================================================
        Offset = 2 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 04
        '=======================================================================================================
        Offset = 3 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        'TC.WriteAs_STRING(Station_04.PLC_IN_ComponentSerialNumber, Offset + 40, Offset + 59)

        '=======================================================================================================
        'Station 05
        '=======================================================================================================
        Offset = 4 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 06
        '=======================================================================================================
        Offset = 5 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 07 
        '=======================================================================================================
        Offset = 6 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 08
        '=======================================================================================================
        Offset = 7 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 09
        '=======================================================================================================
        Offset = 8 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 10
        '=======================================================================================================
        Offset = 9 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 11
        '=======================================================================================================
        Offset = 10 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 12
        '=======================================================================================================
        Offset = 11 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 13
        '=======================================================================================================
        Offset = 12 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        '=======================================================================================================
        'Station 14
        '=======================================================================================================
        Offset = 13 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read


        '=======================================================================================================
        'WT Info
        '=======================================================================================================
        Offset = 14 * TC_StringData_TransferArea_Section_Offset_Write + TC_StringData_TransferArea_Offset_Read

        TC.WriteAs_BYTE(WatchWT.WtNumberRequest, Offset + 0)


        On Error GoTo 0
    End Sub


#End Region


#Region "TC_Common_READ"

    Public Sub TC_Common_Read()

        On Error Resume Next

        ''=======================================================================================================
        ''Station_01
        ''=======================================================================================================
        'ST01_NewPart.PLC_OUT_GetNewPart = TC.ReadBoolean(KostalAdsVariables.PLC_bulGetNewPart)
        ''=======================================================================================================


        ''=======================================================================================================
        ''Station_20 AASM QGW ASSM
        ''=======================================================================================================
        'Dim stuAssmRequestAction As StructRequestAction = TC.ReadRequestAction(KostalAdsVariables.PLC_stuAssmDataRequest)
        'If stuAssmRequestAction IsNot Nothing Then
        '    ST20_QGW_ASSM.PLC_OUT_WritePass = stuAssmRequestAction.bulDoPositiveAction
        '    ST20_QGW_ASSM.PLC_OUT_WriteFail = stuAssmRequestAction.bulDoNegativeAction
        '    ST20_QGW_ASSM.PLC_OUT_WT.Schedule = stuAssmRequestAction.strActionScheduleName
        '    ST20_QGW_ASSM.PLC_OUT_WT.ArticleNumber = stuAssmRequestAction.stuPlcArticleSet.strKostalNr
        '    'ST20_QGW_ASSM.PLC_OUT_WT.Tag = stuAssmRequestAction.stuPlcArticleSet.strTag
        '    '  ST20_QGW_ASSM.PLC_OUT_WT.WheelPos = stuAssmRequestAction.stuPlcArticleSet.strWheelPos
        '    ST20_QGW_ASSM.PLC_OUT_WT.SerialNumber = stuAssmRequestAction.stuPlcArticleSet.strSerialNr
        '    'ST20_QGW_ASSM.PLC_OUT_WritePass = True
        '    'ST20_QGW_ASSM.PLC_OUT_WriteFail = False  'stuAssmRequestAction.bulDoNegativeAction
        '    'ST20_QGW_ASSM.PLC_OUT_WT.Schedule = stuAssmRequestAction.strActionScheduleName
        '    'ST20_QGW_ASSM.PLC_OUT_WT.ArticleNumber = "10128535" 'stuAssmRequestAction.stuPlcArticleSet.strKostalNr
        '    'ST20_QGW_ASSM.PLC_OUT_WT.SerialNumber = "5JT-KL008.07.1500000024" 'stuAssmRequestAction.stuPlcArticleSet.strSerialNr
        '    stuAssmRequestAction = Nothing
        'End If

        ''=======================================================================================================
        ''Station_20 QGW PARTS
        ''=======================================================================================================
        ''ST20_QGW.PLC_OUT_WritePass = TC.ReadBoolean(KostalAdsVariables.PLC_bulTestPass)
        ''ST20_QGW.PLC_OUT_WriteFail = TC.ReadBoolean(KostalAdsVariables.PLC_bulTestFail)
        'Dim stuRequestAction As StructRequestAction = TC.ReadRequestAction(KostalAdsVariables.PLC_stuFinishedPartRequest)
        'If stuRequestAction IsNot Nothing Then
        '    ST20_QGW.PLC_OUT_WritePass = stuRequestAction.bulDoPositiveAction
        '    ST20_QGW.PLC_OUT_WriteFail = stuRequestAction.bulDoNegativeAction
        '    ST20_QGW.PLC_OUT_WT.Schedule = stuRequestAction.strActionScheduleName
        '    ST20_QGW.PLC_OUT_WT.ArticleNumber = stuRequestAction.stuPlcArticleSet.strKostalNr
        '    '  ST20_QGW.PLC_OUT_WT.Tag = stuRequestAction.stuPlcArticleSet.strTag
        '    '  ST20_QGW.PLC_OUT_WT.WheelPos = stuRequestAction.stuPlcArticleSet.strWheelPos
        '    ST20_QGW.PLC_OUT_WT.SerialNumber = stuRequestAction.stuPlcArticleSet.strSerialNr
        '    'ST20_QGW_ASSM.PLC_OUT_WritePass = True
        '    'ST20_QGW_ASSM.PLC_OUT_WriteFail = False  'stuAssmRequestAction.bulDoNegativeAction
        '    'ST20_QGW_ASSM.PLC_OUT_WT.Schedule = stuAssmRequestAction.strActionScheduleName
        '    'ST20_QGW_ASSM.PLC_OUT_WT.ArticleNumber = "10128535" 'stuAssmRequestAction.stuPlcArticleSet.strKostalNr
        '    'ST20_QGW_ASSM.PLC_OUT_WT.SerialNumber = "5JT-KL008.07.1500000024" 'stuAssmRequestAction.stuPlcArticleSet.strSerialNr
        '    stuRequestAction = Nothing
        'End If

    End Sub

    Public Function TC_Common_Write() As Boolean
        Dim stuAssmResponse As StructResponseAction

        On Error Resume Next

        '=======================================================================================================
        'Station_01
        ''=======================================================================================================
        'If Not TC.WriteAny(KostalAdsVariables.PC_bulNewPartAvailable, ST01_NewPart.PLC_IN_NewPartAvailable) Then Return False
        'If Not TC.WriteAny(KostalAdsVariables.PC_bytScheduleNr, _currentScheduleMode.bytScheduleNr) Then Return False


        ''=======================================================================================================
        ''Station_20 QGW ASSM
        ''=======================================================================================================
        'If ST20_QGW_ASSM.PLC_OUT_WritePass Or ST20_QGW_ASSM.PLC_OUT_WriteFail Then
        '    stuAssmResponse = New StructResponseAction
        '    stuAssmResponse.bulActionIsPass = ST20_QGW_ASSM.PLC_IN_WriteIsPass
        '    stuAssmResponse.bulActionIsFail = ST20_QGW_ASSM.PLC_IN_WriteIsFail
        '    stuAssmResponse.strActionResultText = ST20_QGW_ASSM.PLC_IN_ActionResultText
        '    If Not TC.WriteResponseAction(KostalAdsVariables.ADS_stuAssmDataResponse, stuAssmResponse) Then stuAssmResponse = Nothing : Return False
        '    stuAssmResponse = Nothing
        'End If

        ''=======================================================================================================
        ''Station_20 QGW PARTS
        ''=======================================================================================================
        'If ST20_QGW.PLC_OUT_WritePass Or ST20_QGW.PLC_OUT_WriteFail Then
        '    stuAssmResponse = New StructResponseAction
        '    stuAssmResponse.bulActionIsPass = ST20_QGW.PLC_IN_WriteIsPass
        '    stuAssmResponse.bulActionIsFail = ST20_QGW.PLC_IN_WriteIsFail
        '    stuAssmResponse.strActionResultText = ST20_QGW.PLC_IN_ActionResultText
        '    If Not TC.WriteResponseAction(KostalAdsVariables.ADS_stuFinishedPartResponse, stuAssmResponse) Then stuAssmResponse = Nothing : Return False
        '    stuAssmResponse = Nothing
        'End If

        'Return True
    End Function

#End Region


#Region "TC_Status"


    Public Sub TC_Status_Read()

        '    Dim Offset As Integer, Partition As Integer = 0

        '    On Error Resume Next

        '    '=======================================================================================================
        '    'Station_01
        '    '=======================================================================================================

        '    Offset = 0 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_01.PLC_OUT_GetNewPart = TC.ReadFrom_BOOL(Offset + 0, Partition)
        '    Station_01.PLC_OUT_DummyResponse = TC.ReadFrom_BOOL(Offset + 1, Partition)
        '    Station_01_QGW.PLC_OUT_WritePass = TC.ReadFrom_BOOL(Offset + 2, Partition)
        '    Station_01_QGW.PLC_OUT_WriteFail = TC.ReadFrom_BOOL(Offset + 3, Partition)
        '    Station_01_ReadButtonSet.PLC_OUT_DoScan = TC.ReadFrom_BOOL(Offset + 4, Partition)

        '    Station_01.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_01.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_01.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_02
        '    '=======================================================================================================

        '    Offset = 1 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_02.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_02.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_02.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_03
        '    '=======================================================================================================

        '    Offset = 2 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0


        '    'Station_03.PLC_OUT_DoScan = TC.ReadFrom_BOOL(Offset + 4, Partition)

        '    'Station_03.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_03.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_03.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_04
        '    '=======================================================================================================

        '    Offset = 3 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    'Station_04.PLC_OUT_DoScan = TC.ReadFrom_BOOL(Offset + 4, Partition)

        '    'Station_04.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_04.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_04.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_05
        '    '=======================================================================================================

        '    Offset = 4 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    'Station_05.PLC_OUT_DoTeach = TC.ReadFrom_BOOL(Offset + 5, Partition)

        '    'Station_05.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_05.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_05.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    'Station_05.PLC_OUT_AllPositionsRequest = TC.ReadFrom_BOOL(Offset + 10, Partition)
        '    'Station_05.PLC_OUT_AllPositionsRequest_Org = TC.ReadFrom_BOOL(Offset + 11, Partition)


        '    '=======================================================================================================
        '    'Station_06
        '    '=======================================================================================================

        '    Offset = 5 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    'Station_06.PLC_OUT_DoTeach = TC.ReadFrom_BOOL(Offset + 5, Partition)

        '    'Station_06.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_06.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_06.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    'Station_06.PLC_OUT_AllPositionsRequest = TC.ReadFrom_BOOL(Offset + 10, Partition)
        '    'Station_06.PLC_OUT_AllPositionsRequest_Org = TC.ReadFrom_BOOL(Offset + 11, Partition)

        '    '=======================================================================================================
        '    'Station_07
        '    '=======================================================================================================

        '    Offset = 6 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    'Station_07.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_07.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_07.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_08
        '    '=======================================================================================================

        '    Offset = 7 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    'Station_08.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    'Station_08.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    'Station_08.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_09
        '    '=======================================================================================================

        '    Offset = 8 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_09.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_09.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_09.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    'Added by wang65 2015.01.27 
        '    Station_16.PLC_OUT_DoScan = TC.ReadFrom_BOOL(Offset + 4, Partition)

        '    '=======================================================================================================
        '    'Station_10
        '    '=======================================================================================================

        '    Offset = 9 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_10.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_10.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_10.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_11
        '    '=======================================================================================================

        '    Offset = 10 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_11.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_11.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_11.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_12
        '    '=======================================================================================================

        '    Offset = 11 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_12.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_12.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_12.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_13
        '    '=======================================================================================================

        '    Offset = 12 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_13.PLC_OUT_DoScan = TC.ReadFrom_BOOL(Offset + 4, Partition)

        '    Station_13.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_13.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_13.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Station_14
        '    '=======================================================================================================

        '    Offset = 13 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    Station_14_QGW.PLC_OUT_WritePass = TC.ReadFrom_BOOL(Offset + 2, Partition)
        '    Station_14_QGW.PLC_OUT_WriteFail = TC.ReadFrom_BOOL(Offset + 3, Partition)

        '    Station_14_QGW.PLC_OUT_StatusRun = TC.ReadFrom_BOOL(Offset + 6, Partition)
        '    Station_14_QGW.PLC_OUT_StatusAttention = TC.ReadFrom_BOOL(Offset + 7, Partition)
        '    Station_14_QGW.PLC_OUT_StatusFail = TC.ReadFrom_BOOL(Offset + 8, Partition)

        '    '=======================================================================================================
        '    'Watch WT
        '    '=======================================================================================================

        '    Offset = 14 * TC_Status_TransferArea_Section_Offset_Read + TC_Status_TransferArea_Offset_Read
        '    Partition = 0

        '    WatchWT.IsReset_WT = TC.ReadFrom_BOOL(Offset + 19, 0)

        '    On Error GoTo 0
    End Sub


    Public Sub TC_Status_Write()

        '    Dim Offset As Integer = 0, Partition As Integer = 0

        '    On Error Resume Next

        '    '=======================================================================================================
        '    'Station_01
        '    '=======================================================================================================

        '    Offset = 0 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_01.PLC_IN_NewPartAvailable, Offset + 0, Partition)


        '    TC.WriteAs_BOOL(Station_01.PLC_IN_RepairPartScanned, Offset + 1, Partition)
        '    TC.WriteAs_BOOL(Station_01.PLC_IN_DummyRequest, Offset + 2, Partition)

        '    TC.WriteAs_BOOL(Station_01_QGW.PLC_IN_WriteIsPass, Offset + 4, Partition)
        '    TC.WriteAs_BOOL(Station_01_QGW.PLC_IN_WriteIsFail, Offset + 5, Partition)

        '    TC.WriteAs_BOOL(Station_01_ReadButtonSet.PLC_IN_ScanIsPass, Offset + 6, Partition)
        '    TC.WriteAs_BOOL(Station_01_ReadButtonSet.PLC_IN_ScanIsFail, Offset + 7, Partition)

        '    TC.WriteAs_BOOL(Station_01.PLC_IN_ScheduleUpperTwo, Offset + 15, Partition)

        '    TC.WriteAs_BYTE(Station_01_ReadButtonSet.PLC_IN_FailCode, Offset + 18)

        '    '=======================================================================================================
        '    'Station 02
        '    '=======================================================================================================

        '    Offset = 1 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 03
        '    '=======================================================================================================

        '    Offset = 2 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    'TC.WriteAs_BYTE(Station_03.PLC_IN_FailCode, Offset + 0)

        '    TC.WriteAs_BOOL(Station_03.PLC_IN_ScanIsPass, Offset + 6, Partition)
        '    TC.WriteAs_BOOL(Station_03.PLC_IN_ScanIsFail, Offset + 7, Partition)

        '    '=======================================================================================================
        '    'Station 04
        '    '=======================================================================================================

        '    Offset = 3 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_04.PLC_IN_ScanIsPass, Offset + 6, Partition)
        '    TC.WriteAs_BOOL(Station_04.PLC_IN_ScanIsFail, Offset + 7, Partition)

        '    '=======================================================================================================
        '    'Station 05
        '    '=======================================================================================================

        '    Offset = 4 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_05.PLC_IN_IsTeached, Offset + 8, Partition)
        '    TC.WriteAs_BOOL(Station_05.PLC_IN_PositionPass, Offset + 9, Partition)
        '    TC.WriteAs_BOOL(Station_05.PLC_IN_PositionFail, Offset + 10, Partition)

        '    TC.WriteAs_BOOL(Station_05.PLC_IN_AllPositionsSet, Offset + 13, Partition)
        '    TC.WriteAs_BOOL(Station_05.PLC_IN_AllPositionsSet_Org, Offset + 14, Partition)

        '    '=======================================================================================================
        '    'Station 06
        '    '=======================================================================================================

        '    Offset = 5 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_06.PLC_IN_IsTeached, Offset + 8, Partition)
        '    TC.WriteAs_BOOL(Station_06.PLC_IN_PositionPass, Offset + 9, Partition)
        '    TC.WriteAs_BOOL(Station_06.PLC_IN_PositionFail, Offset + 10, Partition)

        '    TC.WriteAs_BOOL(Station_06.PLC_IN_AllPositionsSet, Offset + 13, Partition)
        '    TC.WriteAs_BOOL(Station_06.PLC_IN_AllPositionsSet_Org, Offset + 14, Partition)

        '    '=======================================================================================================
        '    'Station 07
        '    '=======================================================================================================

        '    Offset = 6 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 08
        '    '=======================================================================================================

        '    Offset = 7 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 09
        '    '=======================================================================================================

        '    Offset = 8 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0
        '    'added by wang65 2015.01.27
        '    TC.WriteAs_BOOL(Station_16.PLC_IN_ScanIsPass, Offset + 6, Partition)
        '    TC.WriteAs_BOOL(Station_16.PLC_IN_ScanIsFail, Offset + 7, Partition)

        '    '=======================================================================================================
        '    'Station 10
        '    '=======================================================================================================

        '    Offset = 9 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 11
        '    '=======================================================================================================

        '    Offset = 10 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 12
        '    '=======================================================================================================

        '    Offset = 11 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    '=======================================================================================================
        '    'Station 13
        '    '=======================================================================================================

        '    Offset = 12 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_13.PLC_IN_ScanIsPass, Offset + 6, Partition)
        '    TC.WriteAs_BOOL(Station_13.PLC_IN_ScanIsFail, Offset + 7, Partition)

        '    '=======================================================================================================
        '    'Station 14 - QGW
        '    '=======================================================================================================

        '    Offset = 13 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0

        '    TC.WriteAs_BOOL(Station_14_QGW.PLC_IN_WriteIsPass, Offset + 4, Partition)
        '    TC.WriteAs_BOOL(Station_14_QGW.PLC_IN_WriteIsFail, Offset + 5, Partition)


        '    '=======================================================================================================
        '    'Watch WT
        '    '=======================================================================================================

        '    Offset = 14 * TC_Status_TransferArea_Section_Offset_Write + TC_Status_TransferArea_Offset_Write
        '    Partition = 0


        '    TC.WriteAs_BOOL(WatchWT.DoReset_WT, Offset + 19, 0)

        '    On Error GoTo 0

    End Sub


#End Region


#Region "TC_AxisData"


    'Public Sub TC_AxisData_Read()

    '    Dim Offset As Integer

    '    On Error Resume Next

    '    '=======================================================================================================
    '    'Station_05
    '    '=======================================================================================================
    '    Offset = 0 * TC_AxisData_TransferArea_Section_Offset_Read + TC_AxisData_TransferArea_Offset_Read

    '    Station_05.PLC_OUT_Position = TC.ReadFrom_DWORD(Offset + 0)
    '    Station_05.PLC_OUT_X = TC.ReadFrom_INT(Offset + 4)
    '    Station_05.PLC_OUT_Y = TC.ReadFrom_INT(Offset + 6)
    '    Station_05.PLC_OUT_Z = TC.ReadFrom_INT(Offset + 8)
    '    Station_05.PLC_OUT_Type = TC.ReadFrom_INT(Offset + 10)
    '    Station_05.PLC_OUT_Owner = TC.ReadFrom_INT(Offset + 12)
    '    Station_05.PLC_OUT_ProgramNumber = TC.ReadFrom_INT(Offset + 14)


    '    '=======================================================================================================
    '    'Station_06
    '    '=======================================================================================================
    '    Offset = 1 * TC_AxisData_TransferArea_Section_Offset_Read + TC_AxisData_TransferArea_Offset_Read

    '    Station_06.PLC_OUT_Position = TC.ReadFrom_DWORD(Offset + 0)
    '    Station_06.PLC_OUT_X = TC.ReadFrom_INT(Offset + 4)
    '    Station_06.PLC_OUT_Y = TC.ReadFrom_INT(Offset + 6)
    '    Station_06.PLC_OUT_Z = TC.ReadFrom_INT(Offset + 8)
    '    Station_06.PLC_OUT_Type = TC.ReadFrom_INT(Offset + 10)
    '    Station_06.PLC_OUT_Owner = TC.ReadFrom_INT(Offset + 12)
    '    Station_05.PLC_OUT_ProgramNumber = TC.ReadFrom_INT(Offset + 14)


    '    On Error GoTo 0

    'End Sub



    'Public Sub TC_AxisData_Write()

    '    Const MaxPosition As Integer = 25

    '    Const MaskOffset As Integer = 16
    '    Dim Offset As Integer
    '    Dim Index As Integer

    '    On Error Resume Next

    '    '=======================================================================================================
    '    'Station_05
    '    '=======================================================================================================
    '    Offset = 0 * TC_AxisData_TransferArea_Section_Offset_Write + TC_AxisData_TransferArea_Offset_Write

    '    If Station_05.WriteAllPositions Then

    '        For Index = 1 To MaxPosition
    '            TC.WriteAs_DWORD(CUInt(Index), Offset + (Index - 1) * MaskOffset + 0)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).X), Offset + (Index - 1) * MaskOffset + 4)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).Y), Offset + (Index - 1) * MaskOffset + 6)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).Z), Offset + (Index - 1) * MaskOffset + 8)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).Type), Offset + (Index - 1) * MaskOffset + 10)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).Owner), Offset + (Index - 1) * MaskOffset + 12)
    '            TC.WriteAs_INT(CShort(Station_05.Mask.Positions(Index.ToString).ProgramNumber), Offset + (Index - 1) * MaskOffset + 14)
    '        Next

    '        Station_05.WriteAllPositions = False

    '    End If


    '    If Station_05.WriteAllPositions_Org Then

    '        For Index = 1 To MaxPosition
    '            TC.WriteAs_DWORD(CUInt(Index), Offset + (Index - 1) * MaskOffset + 0)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).X), Offset + (Index - 1) * MaskOffset + 4)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).Y), Offset + (Index - 1) * MaskOffset + 6)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).Z), Offset + (Index - 1) * MaskOffset + 8)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).Type), Offset + (Index - 1) * MaskOffset + 10)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).Owner), Offset + (Index - 1) * MaskOffset + 12)
    '            TC.WriteAs_INT(CShort(Station_05.MaskOrg.Positions(Index.ToString).ProgramNumber), Offset + (Index - 1) * MaskOffset + 14)
    '        Next

    '        Station_05.WriteAllPositions_Org = False

    '    End If


    '    TC.WriteAs_BYTE(Mode_05_06.User(Key_Station_05).State, Offset + 400)

    '    TC.WriteAs_DWORD(CUInt(Station_05.PLC_IN_Position), Offset + 401)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_X), Offset + 405)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_Y), Offset + 407)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_Z), Offset + 409)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_Owner), Offset + 411)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_Type), Offset + 413)
    '    TC.WriteAs_INT(CShort(Station_05.PLC_IN_ProgramNumber), Offset + 415)


    '    '=======================================================================================================
    '    'Station_06
    '    '=======================================================================================================
    '    Offset = 1 * TC_AxisData_TransferArea_Section_Offset_Write + TC_AxisData_TransferArea_Offset_Write

    '    If Station_06.WriteAllPositions Then

    '        Station_06.WriteAllPositions = False

    '        For Index = 1 To MaxPosition
    '            TC.WriteAs_DWORD(CUInt(Index), Offset + (Index - 1) * MaskOffset + 0)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).X), Offset + (Index - 1) * MaskOffset + 4)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).Y), Offset + (Index - 1) * MaskOffset + 6)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).Z), Offset + (Index - 1) * MaskOffset + 8)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).Type), Offset + (Index - 1) * MaskOffset + 10)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).Owner), Offset + (Index - 1) * MaskOffset + 12)
    '            TC.WriteAs_INT(CShort(Station_06.Mask.Positions(Index.ToString).ProgramNumber), Offset + (Index - 1) * MaskOffset + 14)
    '        Next

    '    End If


    '    If Station_06.WriteAllPositions_Org Then

    '        For Index = 1 To MaxPosition
    '            TC.WriteAs_DWORD(CUInt(Index), Offset + (Index - 1) * MaskOffset + 0)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).X), Offset + (Index - 1) * MaskOffset + 4)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).Y), Offset + (Index - 1) * MaskOffset + 6)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).Z), Offset + (Index - 1) * MaskOffset + 8)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).Type), Offset + (Index - 1) * MaskOffset + 10)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).Owner), Offset + (Index - 1) * MaskOffset + 12)
    '            TC.WriteAs_INT(CShort(Station_06.MaskOrg.Positions(Index.ToString).ProgramNumber), Offset + (Index - 1) * MaskOffset + 14)
    '        Next

    '        Station_06.WriteAllPositions_Org = False

    '    End If


    '    TC.WriteAs_BYTE(Mode_05_06.User(Key_Station_06).State, Offset + 400)

    '    TC.WriteAs_DWORD(CUInt(Station_06.PLC_IN_Position), Offset + 401)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_X), Offset + 405)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_Y), Offset + 407)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_Z), Offset + 409)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_Owner), Offset + 411)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_Type), Offset + 413)
    '    TC.WriteAs_INT(CShort(Station_06.PLC_IN_ProgramNumber), Offset + 415)

    '    On Error GoTo 0

    'End Sub

#End Region


#Region "TC_CentralInfo"


    Private Sub TC_Central_Info_Read()

        On Error Resume Next

        On Error GoTo 0

    End Sub


    Private Sub TC_Central_Info_Write()

        Dim Offset As Integer

        On Error Resume Next

        '=======================================================================================================
        'Section 01
        '=======================================================================================================
        Offset = 0 * TC_CentralInfo_Section_Offset_Write + TC_CentralInfo_Offset_Write

        '   TC.WriteAs_STRING(Machine_Identifier.TraceId, Offset + 0, Offset + 19)
        ' TC.WriteAs_STRING(Machine_Identifier.ProjectId, Offset + 20, Offset + 39)
        ' TC.WriteAs_STRING(Machine_Identifier.DrawingId, Offset + 40, Offset + 59)

        On Error GoTo 0

    End Sub


#End Region


End Class

