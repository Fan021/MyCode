
'TwinCat Communication
'Author: wang65
'
'V1.0.0.0 Build 2015_07_11_00

Imports System.Threading
Imports TwinCAT.Ads
Imports Kostal.Las.ArticleProvider
Imports System.Reflection
Imports System.Runtime.InteropServices

'========================================================================
'
'(***============================================================ ***)
'(***=====LAS PLC INTERFACE INTERNAL VERSION0.07 2015.07.18=====***)
'(***============================================================ ***)
'
'========================================================================
#Region "LAS PLC INTERFACE"

Public Module LAS_PLC_Interface

#Region "LAS_PLC_BaseConstants"
    '====================================================================
    '====================================================================
    '-----------------FORM PLC,VERY IMPORTANT---------------------------
    Public Const CON_MAXIMUM_SCHEDULES As UInteger = 50             'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_TOTAL_CARRIERS As UInteger = 30       'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_REAL_CARRIERS As UInteger = 50         'DON'T CHANGE ME!!
    Public Const CON_MAXIMUM_TOTAL_STATIONS As UInteger = 100       'DON'T CHANGE ME!!    

    '====================================================================
    '====================================================================
    '----------------------------WT: CARRIER-----------------------------
    Public Const CON_MAXIMUM_ASSEMBLY_STATIONS As UInteger = 20     'Unused
    Public Const CON_MAXIMUM_REAL_STATIONS As UInteger = 20         'You can change here due to actual readers in your line
    Public Const CON_MAXIMUM_WT_RUNNING_CYCLES As UInteger = 1      'You can change here due to how many cycles for WT to carry a DUT from the begining to the end.
    Public Const CON_MAXIMUM_AVAIABLE_STATIONS As UInteger = CON_MAXIMUM_REAL_STATIONS * CON_MAXIMUM_WT_RUNNING_CYCLES      'DON'T CHANGE ME!!

    '====================================================================
    '====================================================================
    '-----------------------STRING LENGTH RELATED------------------------
    Public Const CON_STRING_LENGTH_21 As Integer = 21               'DON'T CHANGE ME!!
    Public Const CON_STRING_LENGTH_256 As Integer = 256             'DON'T CHANGE ME!!
    Public Const CON_STRING_LENGTH_241 As Integer = 241             'DON'T CHANGE ME!!
    Public Const CON_STRING_LENGTH_51 As Integer = 51         'DON'T CHANGE ME!!
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
        <AutomationVariable()> Public Const PC_arrScheduleList As String = CON_DOT & "PC_arrScheduleList"
        <AutomationVariable()> Public Const PC_stuVariantInfo As String = CON_DOT & "PC_stuCurrentVariantInfo"
        <AutomationVariable()> Public Const PC_bytScheduleNr As String = CON_DOT & "PC_bytCurrentScheduleNr"
        <AutomationVariable()> Public Const PC_bulNewPartAvailable As String = CON_DOT & "PC_bulNewPartAvailable"
        <AutomationVariable()> Public Const PC_bulWriteCarrierInfoRequest As String = CON_DOT & "PC_bulWriteCarrierInfoRequest"
        <AutomationVariable()> Public Const PC_bulWriteCarrierInfoFinished As String = CON_DOT & "PC_bulWriteCarrierInfoFinished"
        <AutomationVariable()> Public Const PC_uinTargetCarrierNr As String = CON_DOT & "PC_uinTargetCarrierNr"
        <AutomationVariable()> Public Const PC_uinTargetCarrierStNr As String = CON_DOT & "PC_uinTargetCarrierStNr"
        <AutomationVariable()> Public Const PC_bulScanPartRequest As String = CON_DOT & "PC_bulScanPartRequest"
        <AutomationVariable()> Public Const PC_bulScannedPartResult As String = CON_DOT & "PC_bulScannedPartResult"
        <AutomationVariable()> Public Const PC_bulSwitchOnOff As String = CON_DOT & "PC_bulSwitchOnOff"
        <AutomationVariable()> Public Const PC_bulResetError As String = CON_DOT & "PC_bulResetError"

        <AutomationVariable()> Public Const HMI_EL1008 As String = CON_DOT & "HMI_Debug.arrDI_EL1008"
        <AutomationVariable()> Public Const HMI_EP1008 As String = CON_DOT & "HMI_Debug.arrDI_EP1008"
        <AutomationVariable()> Public Const HMI_EL2008 As String = CON_DOT & "HMI_Debug.arrDO_EL2008"
        <AutomationVariable()> Public Const HMI_Cylinder As String = CON_DOT & "HMI_Debug.arrCylinder"
        <AutomationVariable()> Public Const HMI_ShortcutButton As String = CON_DOT & "HMI_ShortcutButton"


        'written by PLC only        
        <AutomationVariable()> Public Const PLC_arrOverviewInfo As String = CON_DOT & "PLC_arrOverviewInfo"
        <AutomationVariable()> Public Const PLC_stuErrorMessage As String = CON_DOT & "PLC_stuErrorMessage"
        <AutomationVariable()> Public Const PLC_arrCarrierInfo As String = CON_DOT & "PLC_arrCarrierInfo"
        <AutomationVariable()> Public Const PLC_stuFailedPartInfo As String = CON_DOT & "PLC_stuFailedPartInfo"
        <AutomationVariable()> Public Const PLC_bulGetNewPart As String = CON_DOT & "PLC_bulGetNewPart"
        <AutomationVariable()> Public Const PLC_bulSetCarrierInfoReady As String = CON_DOT & "PLC_bulSetCarrierInfoReady"
        <AutomationVariable()> Public Const PLC_stuAssmDataRequest As String = CON_DOT & "PLC_stuAssmDataRequest"
        <AutomationVariable()> Public Const PLC_stuFinishedPartRequest As String = CON_DOT & "PLC_stuFinishedPartRequest"

        ' <AutomationVariable()> Public Const PLC_bulRedboxStatus As String = CON_DOT & "PLC_Sensor_S1_Red_Box_Big_Lock"

        'to be written by anyone is permitted.
        <AutomationVariable()> Public Const ADS_stuAssmDataResponse As String = CON_DOT & "ADS_stuAssmDataResponse"
        <AutomationVariable()> Public Const ADS_stuFinishedPartResponse As String = CON_DOT & "ADS_stuFinishedPartResponse"
        <AutomationVariable()> Public Const ADS_stuOperateCarrierInfo As String = CON_DOT & "ADS_stuOperateCarrierInfo"

        <AutomationVariable()> Public Const ADS_bulRedboxLock As String = CON_DOT & "ADS_Cylinder_S1_Red_Box_Big_Lock"
        <AutomationVariable()> Public Const HMI_DebugMode As String = CON_DOT & "HMI_DebugMode"

        Protected Shared _variablelist As List(Of String)

        Shared Sub New()

            _variablelist = New List(Of String)
            ScanVariablesOnce()

        End Sub

        Public Shared Function GetAllAdsVariables() As IEnumerable(Of String)

            Return _variablelist

        End Function


        Protected Shared Sub ScanVariablesOnce()

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

        Protected Class AutomationVariableAttribute : Inherits Attribute

            Public Sub New()
                'do nothing
            End Sub

        End Class

    End Class

#End Region

#Region "LAS_PLC_BaseStructs V2.00"
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructDebugCylinder
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDOA As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDOB As Boolean = False
    End Class
    '(*=========================*)
    '(*========= V2.0.0.0  ========*)
    '(*========2018-07-24========*)
    '(*=========================*)
    'TYPE StructCarrierInfo :
    'STRUCT
    '		bytDestinationStation: Byte :=0;			(*to indicate destination station number which carrier should go by*)
    '		bytScheduleModeNr: Byte :=0;			(*to save  test mode number that carrier has got from LAS *)
    '		bytCycleNr: Byte :=0;			(*to save the test circle number which DUT Is running in *)
    '		bytAssemblyCounter: Byte :=0;			(*to count how many assembly actions have been executed successfully*)
    '		bytScrewPassCounter: Byte :=0;			(*to indicate how many times of Screwing have been carried out *)
    '		bytTestRepeatCounter: Byte :=0;			(*to indicate how many test times have been carried out *)
    '		bulReferencePart: BOOL :=FALSE;		(*to indicate that current DUT Is a reference part Or Not, True seen as yes *)
    '		bulTestResult: BOOL :=TRUE;		(*to indicate that current DUT Is test passed Or Not, True seen as passed *)
    '		strScheduleName: String(20):='';
    '		stuVariantInfoSet: StructVariantInfo;		(*to save variant data that carrier has got from LAS *)
    '		stuFailedPartInfo: StructFailedPartInfo;	(*to save failed infomation while DUT test failed *)
    '		(*arrAdditionalMessage				: ARRAY[1..10] OF STRING(50) :='';*)
    'END_STRUCT
    'END_TYPE

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructCarrierInfo_V2_00
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytDestinationStation As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleModeNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytCycleNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytAssemblyCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScrewPassCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytTestRepeatCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReferencePart As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTestResult As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String
        Public stuVariantInfoSet As New StructVariantInfo
        Public stuFailedPartInfo As New StructFailedPartInfo

        Public Sub Clear()
            bytDestinationStation = 0
            bytScheduleModeNr = 0
            bytCycleNr = 0
            bytAssemblyCounter = 0
            bytScrewPassCounter = 0
            bulTestResult = False
            strScheduleName = ""
            stuVariantInfoSet.Clear()
            stuFailedPartInfo.Clear()
        End Sub
    End Class


    '(*=========================*)
    '(*========= V2.0.0.0  ========*)
    '(*========2018-07-24========*)
    '(*=========================*)
    'TYPE StructScheduleMode:
    'STRUCT
    '		bytScheduleNr: Byte:=0;						(* Number of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
    '		strScheduleName: String(20):='';					(* Name of  Schedule Test Mode. e.g Normal Test Mode, SRT Test Mode   *)
    '		strScheduleDescription: String(50):='';					(* Description about the schedule mode   *)
    '		bulReferenceSchedule: BOOL :=FALSE;					(* to indicate the schedule Is used for reference verification Or Not *)
    '		intSecurityChecksum: INT :=0;							(* Checksum of arrScheduleData[x] *)
    '		arrScheduleData: ARRAY[1..CON_MAXIMUM_TOTAL_STATIONS,0..1] OF BYTE:=0;		(*  Array of destination of carriers, 1...50 ==>Station Number, 0...1==> Test Result*)
    'END_STRUCT
    'END_TYPE

    '<StructLayout(LayoutKind.Sequential, Pack:=1)>
    'Public Class StructDestinationStation
    '    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> Public arrDestinationStationData(0 To 9) As Byte
    '    Public Sub Clear()
    '        For i As Integer = 0 To 9
    '            arrDestinationStationData(i) = 0
    '        Next
    '    End Sub
    'End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure StructDestinationStation
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> Public arrDestinationStationData() As Byte
        Public Sub Clear()
            ReDim arrDestinationStationData(9)
            For i As Integer = 0 To 9
                If IsNothing(arrDestinationStationData(i)) Then
                    arrDestinationStationData(i) = New Byte
                End If
                arrDestinationStationData(i) = 0
            Next
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructScheduleMode_V2_00
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleNr As Byte             '(* Kostal number without Index, like 10110201*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String          '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strScheduleDescription As String       '(* Product family name  *)
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReferenceSchedule As Boolean = False
        <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intSecurityChecksum As Int16           '(* Security Checksum  *)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_STATIONS * 2, ArraySubType:=UnmanagedType.Struct)> Public arrScheduleData(0 To CON_MAXIMUM_TOTAL_STATIONS - 1, 0 To 1) As StructDestinationStation
        Public Sub Clear()
            bytScheduleNr = 0
            strScheduleName = ""
            strScheduleDescription = ""
            intSecurityChecksum = 0
            For i As Integer = 0 To CON_MAXIMUM_TOTAL_STATIONS - 1
                arrScheduleData(i, 0) = New StructDestinationStation
                arrScheduleData(i, 0).Clear()
                arrScheduleData(i, 1) = New StructDestinationStation
                arrScheduleData(i, 1).Clear()
            Next
        End Sub
    End Class

#End Region


#Region "LAS_PLC_BaseStructs V1.08"
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructCarrierInfo_V1_08
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytDestinationStation As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleModeNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytCycleNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytAssemblyCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScrewPassCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTestResult As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String
        Public stuVariantInfoSet As New StructVariantInfo
        Public stuFailedPartInfo As New StructFailedPartInfo

        Public Sub Clear()
            bytDestinationStation = 0
            bytScheduleModeNr = 0
            bytCycleNr = 0
            bytAssemblyCounter = 0
            bytScrewPassCounter = 0
            bulTestResult = False
            strScheduleName = ""
            stuVariantInfoSet.Clear()
            stuFailedPartInfo.Clear()
        End Sub
    End Class


#End Region

#Region "LAS_PLC_BaseStructs V0.08"
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
        Public stuVariantInfoSet As New StructVariantInfo
        Public stuFailedPartInfo As New StructFailedPartInfo

        Public Sub Clear()
            bytDestinationStation = 0
            bytScheduleModeNr = 0
            bytCycleNr = 0
            bytAssemblyCounter = 0
            bulTestResult = False
            stuVariantInfoSet.Clear()
            stuFailedPartInfo.Clear()
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructFailedPartInfo
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strFailKostalNr As String = ""
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strFailSerialNr As String = ""
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
        Public Sub Clear()
            strFailKostalNr = ""
            strFailSerialNr = ""
            strFailScheduleName = ""
            strFailTestStatus = ""
            strFailCarrierNr = ""
            strFailStationNr = ""
            strFailTestStep = ""
            strFailCode = ""
            strFailText = ""
            strFailValue = ""
            strFailLowerLimit = ""
            strFailUpperLimit = ""
            strFailUnit = ""
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructScheduleMode
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleNr As Byte             '(* Kostal number without Index, like 10110201*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String          '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strScheduleDescription As String       '(* Product family name  *)
        <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intSecurityChecksum As Short           '(* Security Checksum  *)
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_STATIONS * 2)> Public arrScheduleData(0 To CON_MAXIMUM_TOTAL_STATIONS - 1, 0 To 1) As Byte            '(* Kostal serial number. like 13 digitals *)
        Public Sub Clear()
            bytScheduleNr = 0
            strScheduleName = ""
            strScheduleDescription = ""
            intSecurityChecksum = 0
            For i As Integer = 0 To CON_MAXIMUM_TOTAL_STATIONS - 1
                arrScheduleData(i, 0) = 0
                arrScheduleData(i, 1) = 0
            Next
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructVariantInfo
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalNr As String = ""            '(* Kostal number with/without Index, like 10110201(-01)*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalArticleName As String = ""   '(* Kostal article name, like B9_SCM*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strCustomerNr As String = ""         '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strProductFamily As String = ""      '(* Product family name  *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strSerialNr As String = ""           '(* Kostal serial number. like 13 digitals *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strUserDefine As String = ""    '(* PLC Need Information  *)
        Public Sub Clear()
            strCustomerNr = ""
            strKostalArticleName = ""
            strKostalNr = ""
            strProductFamily = ""
            strSerialNr = ""
            strUserDefine = ""
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructDeviceInteraction
        Public stuPlcArticleSet As New StructVariantInfo
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strAdsActionValue As String = ""

        Public Sub Clear()
            bulPlcDoAction = False
            bulAdsActionIsPass = False
            bulAdsActionIsFail = False
            strAdsActionValue = ""
            stuPlcArticleSet.Clear()
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructRequestAction
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulRunning As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoPositiveAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoNegativeAction As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strActionScheduleName As String = ""
        Public stuPlcArticleSet As New StructVariantInfo
        Public Sub Clear()
            bulRunning = False
            bulDoPositiveAction = False
            bulDoNegativeAction = False
            strActionScheduleName = ""
            stuPlcArticleSet.Clear()
        End Sub

        Public Sub ClearAction()
            bulRunning = False
            bulDoPositiveAction = False
            bulDoNegativeAction = False
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructResponseAction
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPartReceived As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strActionResultText As String = ""
        Public Sub Clear()
            bulPartReceived = False
            bulActionIsPass = False
            bulActionIsFail = False
            strActionResultText = ""
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructConfirmSignal
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsContinue As Boolean = False
    End Class

#End Region

#Region "LAS_PLC_BaseStructs V0.07"
    '================================================================
    'Important - Structs    
    '================================================================
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructCarrierInfo_NoUserDefine
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytDestinationStation As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytScheduleModeNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytCycleNr As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytAssemblyCounter As Byte = 0
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTestResult As Boolean = False
        Public stuVariantInfoSet As New StructVariantInfo_NoUserDefine
        Public stuFailedPartInfo As New StructFailedPartInfo

        Public Sub Clear()
            bytDestinationStation = 0
            bytScheduleModeNr = 0
            bytCycleNr = 0
            bulTestResult = False
            bytAssemblyCounter = 0
            stuVariantInfoSet.Clear()
            stuFailedPartInfo.Clear()
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructVariantInfo_NoUserDefine
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalNr As String = ""            '(* Kostal number with/without Index, like 10110201(-01)*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strKostalArticleName As String = ""   '(* Kostal article name, like B9_SCM*)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strCustomerNr As String = ""         '(* Customer number   *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strProductFamily As String = ""      '(* Product family name  *)
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strSerialNr As String = ""           '(* Kostal serial number. like 13 digitals *)
        Public Sub Clear()
            strCustomerNr = ""
            strKostalArticleName = ""
            strKostalNr = ""
            strProductFamily = ""
            strSerialNr = ""
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructDeviceInteraction_NoUserDefine
        Public stuPlcArticleSet As New StructVariantInfo_NoUserDefine
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsPass As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAdsActionIsFail As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strAdsActionValue As String = ""

        Public Sub Clear()
            bulPlcDoAction = False
            bulAdsActionIsPass = False
            bulAdsActionIsFail = False
            strAdsActionValue = ""
            stuPlcArticleSet.Clear()
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Class StructRequestAction_NoUserDefine
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulRunning As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoPositiveAction As Boolean = False
        <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoNegativeAction As Boolean = False
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strActionScheduleName As String = ""
        Public stuPlcArticleSet As New StructVariantInfo_NoUserDefine
        Public Sub Clear()
            bulRunning = False
            bulDoPositiveAction = False
            bulDoNegativeAction = False
            strActionScheduleName = ""
            stuPlcArticleSet.Clear()
        End Sub
    End Class


#End Region

End Module

#End Region

#Region "Extendied Struct of PLC"
' added for Multi-station tester.

'(*=========================*)
'(*========= V1.0.0.2  ========*)
'(*=========================*)
'TYPE structStationOverviewInfo 
'STRUCT
'iKeyUser: INT:=0;
'	iCarrierNumber: INT:=-1;
'	iDestinationStation: INT:=0;
'	iActualStepNumber: INT:=-1;
'	iRequestedStepNumber: INT:=-1;
'	iTestmanPassCounter: DINT:=0;
'	iTestmanFailCounter: DINT:=0;
'	xTestResult: BOOL;
'	xPromptError: BOOL;
'	xPromptMessage: BOOL;
'	strStationName: String(20):='INVALID STATION';
'	strStationStatus: String(20):='';
'	strAutoManual: String(20):='';
'	strTestmanStatus: String(20):='';
'	strTestmanPercent: String(20):='';
'	strTestmanDppm: String(20):='';
'	strScheduleName: String(20):='';
'	strArticleNumber: String(20):='';
'	strProcessTime: String(20):='';
'	strSerialNumber: String(50) :='';
'	strStepLog: String(255) :='';
'END_STRUCT
'END_TYPE

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class structStationOverviewInfo_V1_00
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strStationName As String = "INVALID STATION"
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strArticleNumber As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strSerialNumber As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iCarrierNumber As Short = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iDestinationStation As Short = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iStepNumber As Short = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAuto As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulManual As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAlarm As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iAlarmNumber As Short = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iMessageNumber As Short = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strProcessTime As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iPassNumber As Short = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iFailNumber As Short = 0

    Public Sub Clear()

    End Sub

End Class

<StructLayout(LayoutKind.Sequential, Pack:=1）>
Public Class structStationOverviewInfo
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iKeyUser As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iCarrierNumber As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iDestinationStation As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iActualStepNumber As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iRequestedStepNumber As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iTestmanPassCounter As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public iTestmanFailCounter As Int16 = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public xTestResult As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public xPromptError As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public xPromptMessage As Boolean = False
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strStationName As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strStationStatus As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strAutoManual As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strTestmanStatus As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strTestmanPercent As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strTestmanDppm As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strScheduleName As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strArticleNumber As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strProcessTime As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strSerialNumber As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strStepLog As String = ""

End Class



'(*=========================*)
'(*========= V1.0.0.0  ========*)
'(*=========================*)
'TYPE structErrorMessageSet 
'STRUCT
'   iKeyUser: INT:=0;
'	iErrorCode: INT:=0;
'	strErrorType: String(20):='';
'	strErrorSource: String(20):='';
'	strErrorValue: String(20):='';
'	strRaisedTime: String(20):='';
'	strErrorTitle: String(50):='';
'	strErrorMessage: String(255):='';
'END_STRUCT
'END_TYPE

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class structErrorMessageSet
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iKeyUser As Short = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=2)> Public iErrorCode As Short = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strErrorType As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strErrorSource As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strErrorValue As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_21)> Public strRaisedTime As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_51)> Public strErrorTitle As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_STRING_LENGTH_256)> Public strErrorMessage As String = ""

    Public Sub Clear()
        iKeyUser = 0
        iErrorCode = 0
        strErrorType = ""
        strErrorSource = ""
        strErrorValue = ""
        strRaisedTime = ""
        strErrorTitle = ""
        strErrorMessage = ""
    End Sub

    Public Shared Operator =(ByVal org As structErrorMessageSet, ByVal tar As structErrorMessageSet) As Boolean

        If org.iKeyUser <> tar.iKeyUser Then Return False
        If org.iErrorCode <> tar.iErrorCode Then Return False
        If org.strErrorMessage <> tar.strErrorMessage Then Return False
        If org.strErrorSource <> tar.strErrorSource Then Return False
        If org.strErrorTitle <> tar.strErrorTitle Then Return False
        If org.strErrorType <> tar.strErrorType Then Return False
        If org.strErrorValue <> tar.strErrorValue Then Return False
        If org.strRaisedTime <> tar.strRaisedTime Then Return False

        Return True
    End Operator

    'Functions'
    Public Shared Operator <>(ByVal org As structErrorMessageSet, ByVal tar As structErrorMessageSet) As Boolean
        Return Not (org = tar)
    End Operator

End Class


#End Region

Public Class TwinCatAds

    Implements IDisposable

    Protected IsDisposed As Boolean = False

    Protected _AmsNetId As String = String.Empty
    Protected _Port As Integer = 0
    Protected mStateInfo As String = String.Empty
    Protected mIsConnected As Boolean = False
    Protected _IsDisabled As Boolean = False
    Protected AppSettings As Settings
    Protected MyLanguage As Language
    Protected _i As Station
    Protected _Devices As Dictionary(Of String, Object)
    Protected _Stations As Dictionary(Of String, IStationTypeBase)
    Protected WithEvents TcAds As TcAdsClient
    Protected _xmlHandler As New XmlHandler
    Protected sResult As String = String.Empty
    Protected LAS_arrScheduleList2(CON_MAXIMUM_SCHEDULES - 1) As StructScheduleMode_V2_00
    Protected LAS_arrScheduleList(CON_MAXIMUM_SCHEDULES - 1) As StructScheduleMode
    Protected _dicOverviewInfo As New Dictionary(Of Integer, structStationOverviewInfo)

    Protected _Section_ID As String = "TwinCat"
    Protected _StatusDescription As String
    'Protected AppSettings As Settings
    Protected lPLCVairablesHandles As New Dictionary(Of String, Integer)
    Protected _notificationHandles As New ArrayList
    Protected lDeviceNotificationEx As New Dictionary(Of String, Object)
    Protected _Object As Object = ""
    Protected _PLCConfig As PLCConfig
    Protected _TwicatRun As ITwicatRun
    Protected lAdsCfg As New Dictionary(Of String, clsAdsCfg)
    Private _Object2 As New Object
    Private _Object3 As New Object
    Private cThread As Thread
    Private bExit As Boolean = False
    Private bMaxCarrier As Integer = 0
    Private bMaxStation As Integer = 0
#Region "Properties"

    Public Property TcAdsClient() As TcAdsClient
        Set(ByVal value As TcAdsClient)
            TcAds = value
        End Set
        Get
            Return TcAds
        End Get
    End Property

    Public ReadOnly Property StationOverviewInfo() As Dictionary(Of Integer, structStationOverviewInfo)
        Get
            Return _dicOverviewInfo
        End Get
    End Property

    Public ReadOnly Property IsDisabled() As Boolean
        Get
            Return _IsDisabled
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property Section_ID() As String
        Get
            Return _Section_ID
        End Get
    End Property

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return mIsConnected
        End Get
    End Property

    Public ReadOnly Property StateInfo() As String
        Get
            If _Section_ID = String.Empty Then
                If IsNothing(TcAds) Then
                    Return "nothing"
                Else
                    Return TcAds.ReadState.AdsState.ToString
                End If
            Else
                If IsNothing(TcAds) Then
                    Return "nothing"
                Else
                    Return _Section_ID + ";" + TcAds.ReadState.AdsState.ToString
                End If
            End If
        End Get
    End Property

    Public Property AmsNetId() As String
        Set(ByVal value As String)
            _AmsNetId = value
        End Set
        Get
            Return _AmsNetId
        End Get
    End Property

    Public Property Port() As Integer
        Set(ByVal value As Integer)
            _Port = value
        End Set
        Get
            Return _Port
        End Get
    End Property

    Public Property PLCVairablesHandles As Dictionary(Of String, Integer)
        Set(ByVal value As Dictionary(Of String, Integer))
            lPLCVairablesHandles = value
        End Set
        Get
            Return lPLCVairablesHandles
        End Get
    End Property

    Public Property ListDeviceNotification As Dictionary(Of String, Object)
        Set(ByVal value As Dictionary(Of String, Object))
            lDeviceNotificationEx = value
        End Set
        Get
            Return lDeviceNotificationEx
        End Get
    End Property


#End Region

    Public Sub New(ByVal PLCConfig As PLCConfig, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal TwicatRun As ITwicatRun = Nothing)
        _PLCConfig = PLCConfig
        _Devices = Devices
        _Stations = Stations
        _i = New Station(PLCConfig.Name)
        _TwicatRun = TwicatRun
        AppSettings = CType(_Devices(Settings.Name), Settings)
        MyLanguage = CType(_Devices(Language.Name), Language)
    End Sub

    ''' <summary>
    ''' 运行SubStation ADS请求.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Run() As Boolean
        Dim mTempstation As IStationTypeBase = Nothing
        Try
            If _IsDisabled Then Return True
            If Not IsNothing(_TwicatRun) Then
                If Not _TwicatRun.TwicatRun(_i, Me, _Devices, _Stations) Then
                    _StatusDescription = _TwicatRun.ErrorMsg
                    Return False
                End If
            End If

            For Each substation As IStationTypeBase In _Stations.Values
                If substation.SubStationCfg.PLCName = "" Or substation.SubStationCfg.PLCName <> _PLCConfig.Name Or substation.ManualRun Then
                    Continue For
                End If

                mTempstation = substation

                'ScheduleStation ADS请求
                If TypeOf substation Is ReferenceStation Then
                    Dim station As ReferenceStation = CType(substation, ReferenceStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    station.PLC_Reference_Sensor = CType(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0)), Boolean)
                    If station.SubStationCfg.AdsOutput.Count > 0 AndAlso CBool(GetDeviceNotificationEx(station.SubStationCfg.AdsOutput(0))) <> station.LAS_Reference_Fail Then
                        If Not WriteAny(station.SubStationCfg.AdsOutput(0), station.LAS_Reference_Fail) Then Return False
                    End If
                End If

                'ScheduleStation ADS请求
                If TypeOf substation Is ReTestStation Then
                    Dim station As ReTestStation = CType(substation, ReTestStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    station.PLC_Reference_Sensor = CType(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0)), Boolean)
                    If station.SubStationCfg.AdsOutput.Count > 0 AndAlso CBool(GetDeviceNotificationEx(station.SubStationCfg.AdsOutput(0))) <> station.LAS_Reference_Fail Then
                        If Not WriteAny(station.SubStationCfg.AdsOutput(0), station.LAS_Reference_Fail) Then Return False
                    End If
                End If

                'Schedule ADS请求
                If TypeOf substation Is ScheduleStation Then
                    Dim station As ScheduleStation = CType(substation, ScheduleStation)
                    If station.WriteSchedule Then
                        station.WriteSchedule = False
                        If Not InitSchedule(station) Then Return False
                    End If
                End If

                'Alarm ADS请求
                If TypeOf substation Is PLCAlarmStation Then
                    Dim station As PLCAlarmStation = CType(substation, PLCAlarmStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    station.ErrorCode = CType(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0)), Int16)
                End If

                If TypeOf substation Is ArticleStation Then
                    Dim station As ArticleStation = CType(substation, ArticleStation)
                    If station.WriteArticle Then
                        station.WriteArticle = False
                        If Not WriteAny(station.SubStationCfg.AdsOutput(0), WriteChangePLCVersion(station.VariantInfo)) Then Return False
                    End If
                End If


                'UserStationDefine ADS请求
                If TypeOf substation Is UserDefineStation Then
                    Dim station As UserDefineStation = CType(substation, UserDefineStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not station.LockArticle Then
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If station.ReadStructDeviceInteraction.bulAdsActionIsPass Or station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = station.ReadStructDeviceInteraction.strAdsActionValue
                        'If Not _PLCConfig.BoschLine Then '手工线回写SN
                        '    deciceSet.stuPlcArticleSet.strKostalNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strKostalNr
                        '    deciceSet.stuPlcArticleSet.strSerialNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                        'End If
                        If Not WriteAny(station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'Scanner ADS请求
                If TypeOf substation Is ScannerStation Then
                    Dim station As ScannerStation = CType(substation, ScannerStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not station.LockArticle Then
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If station.ReadStructDeviceInteraction.bulAdsActionIsPass Or station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = station.ReadStructDeviceInteraction.strAdsActionValue
                        'If Not _PLCConfig.BoschLine Then '手工线回写SN
                        '    deciceSet.stuPlcArticleSet.strKostalNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strKostalNr
                        '    deciceSet.stuPlcArticleSet.strSerialNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                        'End If
                        If Not WriteAny(station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'ManualRetest ADS请求
                If TypeOf substation Is ManualReTestStation Then
                    Dim station As ManualReTestStation = CType(substation, ManualReTestStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    station.PLC_OUT_WT = ReadFailedPartInfo(station.SubStationCfg.AdsInput(1))
                    station.PLCConfirmSignal = CType(ReadChangePLCVersion(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(2))), StructConfirmSignal)
                    If Not station.LockArticle Then
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If station.ReadStructDeviceInteraction.bulAdsActionIsPass Or station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'Manual Scanner ADS请求
                If TypeOf substation Is ManualScannerStation Then
                    Dim station As ManualScannerStation = CType(substation, ManualScannerStation)
                    If station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not station.LockArticle Then
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If station.ReadStructDeviceInteraction.bulAdsActionIsPass Or station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        'deciceSet.strAdsActionValue = station.ReadStructDeviceInteraction.strAdsActionValue
                        'If Not _PLCConfig.BoschLine Then '手工线回写SN
                        '    deciceSet.stuPlcArticleSet.strKostalNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strKostalNr
                        '    deciceSet.stuPlcArticleSet.strSerialNr = station.OutStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                        'End If
                        If Not WriteAny(station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'ShowPicture ADS请求
                If TypeOf substation Is ShowPicStation Then
                    Dim Station As ShowPicStation = CType(substation, ShowPicStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If Station.SubStationCfg.AdsInput.Count > 1 Then
                        Station.PLCErrorCode = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(1))), String)
                    End If

                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not Station.LockArticle Then
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If Station.ReadStructDeviceInteraction.bulAdsActionIsPass Or Station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = Station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = Station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = Station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(Station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'Laser ADS请求
                If TypeOf substation Is LaserStation Then
                    Dim Station As LaserStation = CType(substation, LaserStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not Station.LockArticle Then
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If Station.ReadStructDeviceInteraction.bulAdsActionIsPass Or Station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = Station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = Station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = Station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(Station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'Flash ADS请求
                If TypeOf substation Is FlashStation Then
                    Dim Station As FlashStation = CType(substation, FlashStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not Station.LockArticle Then
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If Station.ReadStructDeviceInteraction.bulAdsActionIsPass Or Station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = Station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = Station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = Station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(Station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

                'Counter ADS请求
                If TypeOf substation Is CounterStation Then
                    Dim Station As CounterStation = CType(substation, CounterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructRequestAction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructRequestAction)
                    If Not Station.LockArticle Then
                        Station.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructRequestAction.bulRunning = deciceSet.bulRunning
                    Station.ReadStructRequestAction.bulDoNegativeAction = deciceSet.bulDoNegativeAction
                    Station.ReadStructRequestAction.bulDoPositiveAction = deciceSet.bulDoPositiveAction
                    If Station.WriteStructResponseAction.bulPartReceived Then
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), WriteChangePLCVersion(Station.WriteStructResponseAction)) Then Return False
                        Station.WriteStructResponseAction.bulActionIsPass = False
                        Station.WriteStructResponseAction.bulActionIsFail = False
                        Station.WriteStructResponseAction.bulPartReceived = False
                    End If
                End If

                'SaveProduction ADS请求
                If TypeOf substation Is SaveProductionStation Then
                    Dim Station As SaveProductionStation = CType(substation, SaveProductionStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructRequestAction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructRequestAction)
                    If Station.SubStationCfg.AdsInput.Count > 1 And (deciceSet.bulDoNegativeAction Or deciceSet.bulDoPositiveAction) Then
                        Station.PLC_OUT_WT = ReadFailedPartInfo(Station.SubStationCfg.AdsInput(1))
                    End If
                    If Not Station.LockArticle Then
                        Station.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructRequestAction.bulRunning = deciceSet.bulRunning
                    Station.ReadStructRequestAction.bulDoNegativeAction = deciceSet.bulDoNegativeAction
                    Station.ReadStructRequestAction.bulDoPositiveAction = deciceSet.bulDoPositiveAction
                    If Station.WriteStructResponseAction.bulPartReceived Then
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), WriteChangePLCVersion(Station.WriteStructResponseAction)) Then Return False
                        Station.WriteStructResponseAction.bulActionIsPass = False
                        Station.WriteStructResponseAction.bulActionIsFail = False
                        Station.WriteStructResponseAction.bulPartReceived = False
                    End If
                End If
                'Printer ADS请求
                If TypeOf substation Is PrinterStation Then
                    Dim Station As PrinterStation = CType(substation, PrinterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Not Station.LockArticle Then
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If Station.ReadStructDeviceInteraction.bulAdsActionIsPass Or Station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = Station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = Station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = Station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(Station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If
                'NewPart ADS请求
                If TypeOf substation Is NewPartStation Then
                    Dim Station As NewPartStation = CType(substation, NewPartStation)
                    Dim deciceSet As Boolean = CBool(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))))
                    Station.ReadGetNewPart = deciceSet
                    If _PLCConfig.PLCVersion >= 1.0 Then
                        If CBool(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(1))) <> Station.PC_bulScanPartRequest Then
                            If Not WriteAny(Station.SubStationCfg.AdsInput(1), Station.PC_bulScanPartRequest) Then Return False
                        End If
                        If CBool(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(2))) <> Station.PC_bulScannedPartResult Then
                            If Not WriteAny(Station.SubStationCfg.AdsInput(2), Station.PC_bulScannedPartResult) Then Return False
                        End If
                    End If

                    If Station.WritebulChangedArticleInfo Then '写变种信息
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(1), WriteChangePLCVersion(Station.VariantInfo)) Then Return False
                        Dim readVariantInfo As New StructVariantInfo
                        If _PLCConfig.UserDefine Then
                            readVariantInfo = CType(ReadChangePLCVersion(ReadAny(Station.SubStationCfg.AdsOutput(1), GetType(StructVariantInfo))), StructVariantInfo)
                        Else
                            readVariantInfo = CType(ReadChangePLCVersion(ReadAny(Station.SubStationCfg.AdsOutput(1), GetType(StructVariantInfo_NoUserDefine))), StructVariantInfo)
                        End If

                        If readVariantInfo.strKostalNr = "" Then
                            _StatusDescription = "readVariantInfo.strKostalNr is Null"
                            Return False
                        End If
                        If readVariantInfo.strKostalNr <> Station.VariantInfo.strKostalNr Then
                            _StatusDescription = "readVariantInfo.strKostalNr is Fail"
                            Return False
                        End If
                        Station.WritebulChangedArticleInfo = False
                    End If

                    If Station.WritebulArticleInfo Then '写变种信息
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), Station.WritebytCurrentScheduleNr) Then Return False
                        If Station.VariantInfo.strKostalNr = "" Then
                            _StatusDescription = "VariantInfo.strKostalNr is Null"
                            Return False
                        End If
                        If Station.VariantInfo.strSerialNr = "" And Not Station.WriteArticleOnly Then
                            _StatusDescription = "VariantInfo.strSerialNr is Null"
                            Return False
                        End If
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(1), WriteChangePLCVersion(Station.VariantInfo)) Then Return False
                        Dim readVariantInfo As New StructVariantInfo
                        If _PLCConfig.UserDefine Then
                            readVariantInfo = CType(ReadChangePLCVersion(ReadAny(Station.SubStationCfg.AdsOutput(1), GetType(StructVariantInfo))), StructVariantInfo)
                        Else
                            readVariantInfo = CType(ReadChangePLCVersion(ReadAny(Station.SubStationCfg.AdsOutput(1), GetType(StructVariantInfo_NoUserDefine))), StructVariantInfo)
                        End If

                        If readVariantInfo.strKostalNr = "" Then
                            _StatusDescription = "readVariantInfo.strKostalNr is Null"
                            Return False
                        End If
                        If readVariantInfo.strSerialNr = "" And Not Station.WriteArticleOnly Then
                            _StatusDescription = "readVariantInfo.strSerialNr is Null"
                            Return False
                        End If
                        If readVariantInfo.strKostalNr <> Station.VariantInfo.strKostalNr Then
                            _StatusDescription = "readVariantInfo.strKostalNr is Fail"
                            Return False
                        End If
                        If readVariantInfo.strSerialNr <> Station.VariantInfo.strSerialNr Then
                            _StatusDescription = "readVariantInfo.strSerialNr is Fail"
                            Return False
                        End If
                        Station.WritebulArticleInfo = False
                    End If

                    If Station.WritebulScheduleInfo Then '写生产模式
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), Station.WritebytCurrentScheduleNr) Then Return False
                        Station.WritebulScheduleInfo = False
                    End If

                    If CBool(GetDeviceNotificationEx(Station.SubStationCfg.AdsOutput(2))) <> Station.WritebulNewPartAvailable Then
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(2), Station.WritebulNewPartAvailable) Then Return False
                    End If

                End If
                'LineControl ADS请求
                If TypeOf substation Is LineControlStation Then
                    Dim Station As LineControlStation = CType(substation, LineControlStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructRequestAction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructRequestAction)
                    If Station.SubStationCfg.AdsInput.Count > 1 And (deciceSet.bulDoNegativeAction Or deciceSet.bulDoPositiveAction) Then
                        Station.PLC_OUT_WT = ReadFailedPartInfo(Station.SubStationCfg.AdsInput(1))
                    End If
                    If Not Station.LockArticle Then
                        Station.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructRequestAction.bulRunning = deciceSet.bulRunning
                    Station.ReadStructRequestAction.bulDoNegativeAction = deciceSet.bulDoNegativeAction
                    Station.ReadStructRequestAction.bulDoPositiveAction = deciceSet.bulDoPositiveAction
                    Station.ReadStructRequestAction.strActionScheduleName = deciceSet.strActionScheduleName

                    If Station.WriteStructResponseAction.bulPartReceived Then
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), WriteChangePLCVersion(Station.WriteStructResponseAction)) Then Return False
                        Station.WriteStructResponseAction.bulActionIsPass = False
                        Station.WriteStructResponseAction.bulActionIsFail = False
                        Station.WriteStructResponseAction.bulPartReceived = False
                    End If
                End If


                'UpdateReference ADS请求
                If TypeOf substation Is UpdateReferenceStation Then
                    Dim Station As UpdateReferenceStation = CType(substation, UpdateReferenceStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructRequestAction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructRequestAction)
                    If Not Station.LockArticle Then
                        Station.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    'Station.ReadStructRequestAction = deciceSet
                    Station.ReadStructRequestAction.bulRunning = deciceSet.bulRunning
                    Station.ReadStructRequestAction.bulDoNegativeAction = deciceSet.bulDoNegativeAction
                    Station.ReadStructRequestAction.bulDoPositiveAction = deciceSet.bulDoPositiveAction
                    Station.ReadStructRequestAction.strActionScheduleName = deciceSet.strActionScheduleName
                    If Station.WriteStructResponseAction.bulPartReceived Then
                        If Not WriteAny(Station.SubStationCfg.AdsOutput(0), WriteChangePLCVersion(Station.WriteStructResponseAction)) Then Return False
                        Station.WriteStructResponseAction.bulActionIsPass = False
                        Station.WriteStructResponseAction.bulActionIsFail = False
                        Station.WriteStructResponseAction.bulPartReceived = False
                    End If
                End If

                'FailPrinter ADS请求
                If TypeOf substation Is FailPrinterStation Then
                    Dim Station As FailPrinterStation = CType(substation, FailPrinterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim deciceSet As StructDeviceInteraction = CType(ReadChangePLCVersion(GetDeviceNotificationEx(Station.SubStationCfg.AdsInput(0))), StructDeviceInteraction)
                    If Station.SubStationCfg.AdsInput.Count > 1 And deciceSet.bulPlcDoAction Then
                        Station.PLC_OUT_WT = ReadFailedPartInfo(Station.SubStationCfg.AdsInput(1))
                    End If
                    If Not Station.LockArticle Then
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = deciceSet.stuPlcArticleSet.strKostalNr
                        Station.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = deciceSet.stuPlcArticleSet.strSerialNr
                        Station.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = deciceSet.stuPlcArticleSet.strUserDefine
                    End If
                    Station.ReadStructDeviceInteraction.bulPlcDoAction = deciceSet.bulPlcDoAction
                    If Station.ReadStructDeviceInteraction.bulAdsActionIsPass Or Station.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        deciceSet.bulAdsActionIsPass = Station.ReadStructDeviceInteraction.bulAdsActionIsPass
                        deciceSet.bulAdsActionIsFail = Station.ReadStructDeviceInteraction.bulAdsActionIsFail
                        deciceSet.strAdsActionValue = Station.ReadStructDeviceInteraction.strAdsActionValue
                        If Not WriteAny(Station.SubStationCfg.AdsInput(0), WriteChangePLCVersion(deciceSet)) Then Return False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        Station.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    End If
                End If

            Next

            If AppSettings.LineType = enumLineType.MultiLine Then
                For i As Integer = 1 To bMaxStation + 3

                    Dim overviewInfo As structStationOverviewInfo = CType(GetDeviceNotificationEx(KostalAdsVariables.PLC_arrOverviewInfo + "[" + i.ToString + "]"), structStationOverviewInfo)

                    If _dicOverviewInfo.Keys.Contains(i) Then
                        _dicOverviewInfo(i) = overviewInfo
                    Else
                        _dicOverviewInfo.Add(i, overviewInfo)
                    End If

                Next
            End If


        Catch ex As Exception
            If mTempstation IsNot Nothing Then
                _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR1, mTempstation.SubStationCfg.Name, ex.Message.ToString+ ex.StackTrace)
            Else
                _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString+ ex.StackTrace)
            End If
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 初始化.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Init(Optional ByVal InitStationAds As Boolean = True) As Boolean
        Try
            mIsConnected = False
            If Not _PLCConfig.TwinCatEnable Then
                _IsDisabled = True
                mIsConnected = True
                Return True
            End If
            _AmsNetId = _PLCConfig.TwinCatAmsNetId
            If _AmsNetId = String.Empty Or _AmsNetId = XmlHandler.s_DEFAULT Or _AmsNetId = XmlHandler.s_Null Then
                _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR3, _AmsNetId)
                Return False
            End If

            If AppSettings.LineType > 0 Then
                bMaxCarrier = CInt(_xmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "WtStatus"))
                bMaxStation = CInt(_xmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "MaxStation"))
            Else
                bMaxCarrier = 0
            End If

            _Port = _PLCConfig.TwinCatPort
            TcAds = New TcAdsClient
            TcAds.Connect(AmsNetId, Port)
            mIsConnected = True
            If _Port >= 300 AndAlso _Port <= 399 Then   'Ports 300-399 SystemManager do not support this Info
                _StatusDescription = "Connected"
            Else
                _StatusDescription = TcAds.ReadState.AdsState.ToString
                mStateInfo = TcAds.ReadState.AdsState.ToString
            End If
            If InitStationAds Then
                If Not AddLocationAds() Then Return False
                If Not AddSystemAds() Then Return False
            End If
            If Not AddStationAds() Then Return False
            If Not AddDeviceNotificationEx() Then Return False
            cThread = New Thread(AddressOf Refresh)
            cThread.IsBackground = True
            cThread.Priority = ThreadPriority.Normal
            cThread.Start()
            Return True
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            mIsConnected = False
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 写Schedule List至PLC.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitSchedule(ByVal mStation As ScheduleStation) As Boolean
        Try
            Dim i As Integer = 0
            '    LAS_arrScheduleList = New StructScheduleMode
            For Each ScheduleElement In mStation.LocalSchedule.ArticleListElement.Values
                mStation.LocalSchedule.GetArticle_FromID(ScheduleElement.ID)
                i = Byte.Parse(mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_INDEX).Data) - 1
                LAS_arrScheduleList2(i) = New StructScheduleMode_V2_00
                LAS_arrScheduleList2(i).bytScheduleNr = Byte.Parse(mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_INDEX).Data)
                LAS_arrScheduleList2(i).strScheduleName = mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data
                LAS_arrScheduleList2(i).strScheduleDescription = mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_DESCRIPTION).Data
                If mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_REFERENCE_SCHEDULE).Data.ToUpper = True.ToString.ToUpper Then
                    LAS_arrScheduleList2(i).bulReferenceSchedule = True
                Else
                    LAS_arrScheduleList2(i).bulReferenceSchedule = False
                End If
                If mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SECURITY_CHECKSUM).Data <> "" Then
                    LAS_arrScheduleList2(i).intSecurityChecksum = Short.Parse(mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SECURITY_CHECKSUM).Data)
                Else
                    LAS_arrScheduleList2(i).intSecurityChecksum = 0
                End If

                For j As Integer = 1 To CON_MAXIMUM_TOTAL_STATIONS
                    If mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_FAIL_STATION(CUInt(j))).Data <> "" Then
                        Dim cValue() As String = mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_FAIL_STATION(CUInt(j))).Data.Split(CChar(","))
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 0) = New StructDestinationStation
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 0).Clear()
                        For k = 0 To cValue.Length - 1
                            If cValue(k) = "" Then cValue(k) = "0"
                            If k > 9 Then Exit For
                            LAS_arrScheduleList2(i).arrScheduleData(j - 1, 0).arrDestinationStationData(k) = Byte.Parse(cValue(k))
                        Next
                    Else
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 0) = New StructDestinationStation
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 0).Clear()
                    End If

                    If mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_PASS_STATION(CUInt(j))).Data <> "" Then
                        Dim cValue() As String = mStation.LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_PASS_STATION(CUInt(j))).Data.Split(CChar(","))
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 1) = New StructDestinationStation
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 1).Clear()
                        For k = 0 To cValue.Length - 1
                            If cValue(k) = "" Then cValue(k) = "0"
                            If k > 9 Then Exit For
                            LAS_arrScheduleList2(i).arrScheduleData(j - 1, 1).arrDestinationStationData(k) = Byte.Parse(cValue(k))
                        Next
                    Else
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 1) = New StructDestinationStation
                        LAS_arrScheduleList2(i).arrScheduleData(j - 1, 1).Clear()
                    End If
                Next

            Next

            For i = 0 To CON_MAXIMUM_SCHEDULES - 1
                If LAS_arrScheduleList2(i) Is Nothing Then
                    LAS_arrScheduleList2(i) = New StructScheduleMode_V2_00
                    LAS_arrScheduleList2(i).Clear()
                End If
            Next

            For i = 0 To CON_MAXIMUM_SCHEDULES - 1
                LAS_arrScheduleList(i) = CType(WriteChangePLCVersion(LAS_arrScheduleList2(i)), StructScheduleMode)
            Next

            If _PLCConfig.PLCVersion > 1.0 Then
                If Not WriteAny(mStation.SubStationCfg.AdsOutput(0), LAS_arrScheduleList2) Then
                    Return False
                End If
            Else
                If Not WriteAny(mStation.SubStationCfg.AdsOutput(0), LAS_arrScheduleList) Then
                    Return False
                End If
            End If
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 添加系统级别Notification.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function AddDeviceNotificationEx() As Boolean
        Try
            For Each substation As IStationTypeBase In _Stations.Values
                If substation.SubStationCfg.PLCName = "" Or substation.SubStationCfg.PLCName <> _PLCConfig.Name Then
                    Continue For
                End If
                If TypeOf substation Is ScannerStation Then
                    Dim Station As ScannerStation = CType(substation, ScannerStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim t As Single = 0
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is UserDefineStation Then
                    Dim Station As UserDefineStation = CType(substation, UserDefineStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim t As Single = 0
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is ManualReTestStation Then
                    Dim Station As ManualReTestStation = CType(substation, ManualReTestStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim t As Single = 0
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                    AddNotificationEx(Station.SubStationCfg.AdsInput(2), New StructConfirmSignal)
                End If

                If TypeOf substation Is ManualScannerStation Then
                    Dim Station As ManualScannerStation = CType(substation, ManualScannerStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    Dim t As Single = 0
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is ShowPicStation Then
                    Dim Station As ShowPicStation = CType(substation, ShowPicStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                    If Station.SubStationCfg.AdsInput.Count > 1 Then
                        AddNotificationEx(Station.SubStationCfg.AdsInput(1), New Integer)
                    End If
                End If

                If TypeOf substation Is LaserStation Then
                    Dim Station As LaserStation = CType(substation, LaserStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is FlashStation Then
                    Dim Station As FlashStation = CType(substation, FlashStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is PrinterStation Then
                    Dim Station As PrinterStation = CType(substation, PrinterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is NewPartStation Then
                    Dim Station As NewPartStation = CType(substation, NewPartStation)
                    AddNotificationEx(Station.SubStationCfg.AdsInput(0), New Boolean)
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(2), New Boolean)
                    If _PLCConfig.PLCVersion >= 1.0 Then
                        AddNotificationEx(Station.SubStationCfg.AdsInput(1), New Boolean)
                        AddNotificationEx(Station.SubStationCfg.AdsInput(2), New Boolean)
                    End If

                End If

                If TypeOf substation Is LineControlStation Then
                    Dim Station As LineControlStation = CType(substation, LineControlStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    'For i = 0 To Station.SubStationCfg.AdsInput.Count - 1
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction_NoUserDefine)
                    End If
                    'Next
                    'For i = 0 To Station.SubStationCfg.AdsOutput.Count - 1
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New StructResponseAction)
                    '  Next
                    'If Station.SubStationCfg.AdsInput.Count > 1 Then
                    '    AddNotificationEx(Station.SubStationCfg.AdsInput(1), New StructFailedPartInfo)
                    'End If
                End If

                If TypeOf substation Is CounterStation Then
                    Dim Station As CounterStation = CType(substation, CounterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    'For i = 0 To Station.SubStationCfg.AdsInput.Count - 1
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction_NoUserDefine)
                    End If
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New StructResponseAction)
                End If

                If TypeOf substation Is SaveProductionStation Then
                    Dim Station As SaveProductionStation = CType(substation, SaveProductionStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    'For i = 0 To Station.SubStationCfg.AdsInput.Count - 1
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction_NoUserDefine)
                    End If
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New StructResponseAction)
                End If

                If TypeOf substation Is UpdateReferenceStation Then
                    Dim Station As UpdateReferenceStation = CType(substation, UpdateReferenceStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    'For i = 0 To Station.SubStationCfg.AdsInput.Count - 1
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructRequestAction_NoUserDefine)
                    End If
                    ' Next
                    '  For i = 0 To Station.SubStationCfg.AdsOutput.Count - 1
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New StructResponseAction)
                    '  Next
                End If

                If TypeOf substation Is FailPrinterStation Then
                    Dim Station As FailPrinterStation = CType(substation, FailPrinterStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    If _PLCConfig.UserDefine Then 'PLC版本管控
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction)
                    Else
                        AddNotificationEx(Station.SubStationCfg.AdsInput(0), New StructDeviceInteraction_NoUserDefine)
                    End If
                End If

                If TypeOf substation Is ReferenceStation Then
                    Dim Station As ReferenceStation = CType(substation, ReferenceStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    AddNotificationEx(Station.SubStationCfg.AdsInput(0), New Boolean)
                    If Station.SubStationCfg.AdsOutput.Count < 1 Then Continue For
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New Boolean)
                End If
                If TypeOf substation Is ReTestStation Then
                    Dim Station As ReTestStation = CType(substation, ReTestStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    AddNotificationEx(Station.SubStationCfg.AdsInput(0), New Boolean)
                    If Station.SubStationCfg.AdsOutput.Count < 1 Then Continue For
                    AddNotificationEx(Station.SubStationCfg.AdsOutput(0), New Boolean)
                End If

                If TypeOf substation Is PLCAlarmStation Then
                    Dim Station As PLCAlarmStation = CType(substation, PLCAlarmStation)
                    If Station.SubStationCfg.AdsInput.Count < 1 Then Continue For
                    AddNotificationEx(Station.SubStationCfg.AdsInput(0), New Int16)
                End If
            Next


            If _PLCConfig.LineType > 0 Then

                For i As Integer = 1 To bMaxCarrier
                    If _PLCConfig.PLCVersion >= 2.0 Then
                        AddNotificationEx(".PLC_arrCarrierInfo[" + i.ToString + "]", New StructCarrierInfo_V2_00)
                    ElseIf _PLCConfig.PLCVersion >= 1.0 Then
                        AddNotificationEx(".PLC_arrCarrierInfo[" + i.ToString + "]", New StructCarrierInfo)
                    Else
                        If _PLCConfig.UserDefine Then
                            AddNotificationEx(".PLC_arrCarrierInfo[" + i.ToString + "]", New StructCarrierInfo)
                        Else
                            AddNotificationEx(".PLC_arrCarrierInfo[" + i.ToString + "]", New StructCarrierInfo)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' 手动添加Notification.
    ''' </summary>
    ''' <param name="VariableName"></param>
    ''' <remarks></remarks>
    Public Function AddNotificationEx(ByVal VariableName As String, ByVal ObjectValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean

        ' Dim iHandel As Integer
        If VariableName = "" Then
            Return True
        End If
        'If strName.Trim.IndexOf(".") <> 0 Then
        '    strName = "." + strName.Trim
        'End If
        'If Not notificationHandles.ContainsKey(strName) Then
        '    iHandel = TcAds.AddDeviceNotificationEx(strName, AdsTransMode.OnChange, 100, 10, strName, ObjectType, args)
        '    notificationHandles.Add(strName, iHandel)
        '    lDeviceNotificationEx.Add(strName, ObjectDefauleValue)
        'End If
        VariableName = ChangeAdsName(VariableName)
        If Not lAdsCfg.ContainsKey(VariableName) Then
            If IsNothing(args) Then
                lAdsCfg.Add(VariableName, New clsAdsCfg(VariableName, ObjectValue.GetType(), 0, ObjectValue))
            Else
                lAdsCfg.Add(VariableName, New clsAdsCfg(VariableName, ObjectValue.GetType(), args(0), ObjectValue))
            End If
            lDeviceNotificationEx.Add(VariableName, ObjectValue)
        End If
        Return True
    End Function

    Public Function RemoveNotificationEx(ByVal strName As String) As Boolean
        SyncLock _Object3
            Try
                'If strName.Trim.IndexOf(".") <> 0 Then
                '    strName = "." + strName.Trim
                'End If
                'If notificationHandles.ContainsKey(strName) Then
                '    _TcAds.DeleteDeviceNotification(notificationHandles(strName))
                '    notificationHandles.Remove(strName)
                '    lDeviceNotificationEx.Remove(strName)
                'End If
                strName = ChangeAdsName(strName)
                If Not lAdsCfg.ContainsKey(strName) Then
                    Return True
                End If

                If lAdsCfg.ContainsKey(strName) Then
                    lAdsCfg.Remove(strName)
                    lDeviceNotificationEx.Remove(strName)
                    If lPLCVairablesHandles.ContainsKey(strName) Then
                        _TcAds.DeleteVariableHandle(lPLCVairablesHandles(strName))
                        lPLCVairablesHandles.Remove(strName)
                    End If
                End If

                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End SyncLock
    End Function

    ''' <summary>
    ''' Notification回调函数.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub adsClient_AdsNotificationEx(ByVal sender As Object, ByVal e As TwinCAT.Ads.AdsNotificationExEventArgs) Handles TcAds.AdsNotificationEx
        SyncLock _Object
            Dim ObjectValue As New Object
            If lDeviceNotificationEx.ContainsKey(CType(e.UserData, String)) Then
                lDeviceNotificationEx(CType(e.UserData, String)) = e.Value
            End If
        End SyncLock
    End Sub

    ' Private Sub adsClient_AdsStateChanged(ByVal sender As Object, ByVal e As TwinCAT.Ads.AdsStateChangedEventArgs) Handles TcAds.AdsStateChanged
    ' If e.State.AdsState <> AdsState.Run Then
    ' Throw New Exception("PLC :" + _PLCConfig.Name + " State is " + e.State.AdsState.ToString)
    ' End If
    ' End Sub

    ''' <summary>
    ''' 获取Notification值.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDeviceNotificationEx(ByVal Name As String) As Object
        SyncLock _Object
            SyncLock _Object2
                Try
                    'If strName.Trim.IndexOf(".") <> 0 Then
                    '    strName = "." + strName.Trim
                    'End If
                    Name = ChangeAdsName(Name)
                    If lDeviceNotificationEx.ContainsKey(Name) Then
                        Return lDeviceNotificationEx(Name)
                    Else
                        Throw New Exception("lDeviceNotificationEx not ContainsKey:" + Name + "22222222222")
                    End If
                Catch ex As Exception
                    Throw New Exception("lDeviceNotificationEx not ContainsKey:" + "1111111111111")
                    'Return Nothing
                End Try
                Return Nothing
            End SyncLock
        End SyncLock
    End Function



    ''' <summary>
    ''' 获取SetDeviceNotificationEx值.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function SetDeviceNotificationEx(ByVal Name As String, ByVal oValue As Object) As Boolean
        'SyncLock _Object
        SyncLock _Object2
            Try


                'If strName.Trim.IndexOf(".") <> 0 Then
                '    strName = "." + strName.Trim
                'End If
                Name = ChangeAdsName(Name)
                If lDeviceNotificationEx.ContainsKey(Name) Then
                    lDeviceNotificationEx(Name) = oValue
                Else
                    Throw New Exception("lDeviceNotificationEx not ContainsKey:" + Name + "444444444444444")
                End If
            Catch ex As Exception
                Throw New Exception("lDeviceNotificationEx not ContainsKey:" + Name + "333333333333")
                Return True
            End Try

        End SyncLock
        ' End SyncLock
        Return True
    End Function
    Protected Function AddStationAds() As Boolean
        Try
            For Each substation As IStationTypeBase In _Stations.Values
                If substation.SubStationCfg.PLCName = "" Or substation.SubStationCfg.PLCName <> _PLCConfig.Name Then
                    Continue For
                End If
                For Each mValues As String In substation.SubStationCfg.AdsInput
                    If Not AddAdsVariable(substation.SubStationCfg.Name, mValues) Then Return False
                Next
                For Each mValues As String In substation.SubStationCfg.AdsOutput
                    If Not AddAdsVariable(substation.SubStationCfg.Name, mValues) Then Return False
                Next
            Next
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            Return False
        End Try
        Return True
    End Function


    Private Sub Refresh()
        While Not bExit
            Try
                ' System.Windows.Forms.Application.DoEvents()
                SyncLock _Object3
                    For Each element As clsAdsCfg In lAdsCfg.Values
                        If bExit Then
                            Return
                        End If
                        If IsNothing(element.args) Then
                            Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType)
                            SyncLock _Object2
                                SetDeviceNotificationEx(element.Name, ObjectValue)
                            End SyncLock
                        End If

                        If Not IsNothing(element.args) Then
                            If element.args = 0 Then
                                Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType)
                                SyncLock _Object2
                                    SetDeviceNotificationEx(element.Name, ObjectValue)
                                End SyncLock
                            Else

                                Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType, New Integer() {element.args})
                                SyncLock _Object2
                                    SetDeviceNotificationEx(element.Name, ObjectValue)
                                End SyncLock
                            End If
                        End If
                    Next
                End SyncLock
                Thread.Sleep(20)
            Catch ex As Exception
                If bExit Then
                    Return
                End If
                Thread.Sleep(20)
            End Try
        End While

    End Sub

    Protected Function AddSystemAds() As Boolean
        Try


            If AppSettings.LineType <> enumLineType.MultiLine Then
                Return True
            End If
            'If Not AddAdsVariable("System", KostalAdsVariables.ADS_bulRedboxLock) Then Return False

            ' If Not AddAdsVariable("System", KostalAdsVariables.PC_bulSwitchOnOff) Then Return False

            If Not AddAdsVariable("System", KostalAdsVariables.PC_bulResetError) Then Return False

            If Not AddAdsVariable("System", KostalAdsVariables.PLC_arrOverviewInfo) Then Return False

            If Not AddAdsVariable("System", KostalAdsVariables.PLC_stuErrorMessage) Then Return False

            ' If Not AddAdsVariable("System", KostalAdsVariables.PLC_bulRedboxStatus) Then Return False

            '  AddNotificationEx(KostalAdsVariables.PLC_bulRedboxStatus, New Boolean)

            AddNotificationEx(KostalAdsVariables.PLC_stuErrorMessage, New structErrorMessageSet)

            For i As Integer = 1 To bMaxStation + 3

                AddNotificationEx(KostalAdsVariables.PLC_arrOverviewInfo + "[" + i.ToString + "]", New structStationOverviewInfo)

            Next

        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' 添加系统级别ADS变量.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function AddLocationAds() As Boolean
        Try
            Dim strAddPLCName As String = String.Empty
            For Each substation As IStationTypeBase In _Stations.Values
                If substation.SubStationCfg.PLCName = "" Or substation.SubStationCfg.PLCName <> _PLCConfig.Name Then
                    Continue For
                End If

                If TypeOf substation Is ScheduleStation Then
                    Dim station As ScheduleStation = CType(substation, ScheduleStation)
                    station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PLC_arrCarrierInfo)
                    station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.PC_arrScheduleList)
                End If

                If TypeOf substation Is ArticleStation Then
                    Dim Station As ArticleStation = CType(substation, ArticleStation)
                    Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.PC_stuVariantInfo)
                End If

                If TypeOf substation Is NewPartStation Then
                    Dim Station As NewPartStation = CType(substation, NewPartStation)
                    Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PLC_bulGetNewPart)
                    Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.PC_bytScheduleNr)
                    Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.PC_stuVariantInfo)
                    Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.PC_bulNewPartAvailable)
                    If _PLCConfig.PLCVersion >= 1.0 Then
                        Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PC_bulScanPartRequest)
                        Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PC_bulScannedPartResult)
                    End If
                End If

                If TypeOf substation Is LineControlStation Then
                    Dim Station As LineControlStation = CType(substation, LineControlStation)
                    If Station.SubStationCfg.Inteface = StationType.QGW_ASSM Then
                        Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PLC_stuAssmDataRequest)
                        Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.ADS_stuAssmDataResponse)
                    End If
                    If Station.SubStationCfg.Inteface = StationType.QGW_Finish Then
                        Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PLC_stuFinishedPartRequest)
                        Station.SubStationCfg.AdsInput.Add(KostalAdsVariables.PLC_stuFailedPartInfo)
                        Station.SubStationCfg.AdsOutput.Add(KostalAdsVariables.ADS_stuFinishedPartResponse)
                    End If
                End If
            Next
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR2, ex.Message.ToString + ex.StackTrace)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 手动添加ADS变量.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function AddAdsVariable(ByVal VariableName As String) As Boolean
        Try
            Dim hVariable As Integer = -1
            If VariableName.IndexOf(CON_DOT) < 0 Then
                VariableName = CON_DOT + VariableName
            End If
            VariableName = ChangeAdsName(VariableName)
            If lPLCVairablesHandles.ContainsKey(VariableName.Trim) Then
                _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR4, VariableName.Trim)
                Return True
            End If
            hVariable = TcAds.CreateVariableHandle(VariableName.Trim)
            lPLCVairablesHandles.Add(VariableName.Trim, hVariable)
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR1, VariableName.Trim, ex.Message)
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function AddAdsVariable(ByVal mStationName As String, ByVal VariableName As String) As Boolean
        Try
            Dim hVariable As Integer = -1
            If VariableName.IndexOf(CON_DOT) < 0 Then
                VariableName = CON_DOT + VariableName
            End If
            VariableName = ChangeAdsName(VariableName)
            If lPLCVairablesHandles.ContainsKey(VariableName.Trim) Then
                _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR5, mStationName, VariableName)
                Return True
            End If
            hVariable = TcAds.CreateVariableHandle(VariableName.Trim)
            lPLCVairablesHandles.Add(VariableName.Trim, hVariable)
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR6, mStationName, VariableName, ex.Message)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 读取ADS变量.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadAny(ByVal name As String, ByVal type As Type, Optional ByVal args() As Integer = Nothing) As Object
        Try
            'SyncLock _Object
            Dim objResult As Object = Nothing

                If _IsDisabled Then Return False

                If TcAds Is Nothing Then
                    _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR7)
                    Return Nothing
                End If

                If Not TcAds.IsConnected Then Return Nothing
                name = ChangeAdsName(name)

                If Not lPLCVairablesHandles.ContainsKey(name) Then
                    AddAdsVariable(name)
                    ' _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR8, name)
                    ' Return Nothing
                End If
                Try
                    objResult = TcAds.ReadAny(lPLCVairablesHandles(name), type, args)
                    Return objResult
                Catch ex As Exception
                    _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR9, name, ex.Message)
                    Return Nothing
                End Try
            'End SyncLock
        Catch ex As Exception

            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR9, name, ex.Message)
            Throw New Exception(_StatusDescription)
            Return ""
        End Try

    End Function

    Private Function ChangeAdsName(ByVal strName As String) As String
        If _PLCConfig.PLCVersion >= 3.0 Then
            If strName.IndexOf(".") = 0 Then
                strName = strName.Substring(1)
            End If
            If strName.IndexOf(".") < 0 Then
                strName = "LAS." + strName
            End If
            If (strName.IndexOf("HMI_Debug") >= 0 Or strName.IndexOf("HMI_ShortcutButton") >= 0) And strName.IndexOf("LAS") < 0 Then
                strName = "LAS." + strName
            End If
        End If
        Return strName
    End Function

    ''' <summary>
    ''' 读取Bool ADS变量.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadBoolean(ByVal name As String) As Boolean

        Return CBool(ReadAny(name, GetType(Boolean)))

    End Function

    ''' <summary>
    ''' 读取String ADS变量.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadString(ByVal name As String, ByVal iLength As Integer) As String

        Dim data(iLength) As Byte
        Dim sRet As String = ""

        Dim Results() As Byte = CType(ReadAny(name, GetType(Byte()), New Integer() {iLength}), Byte())

        For i As Integer = 0 To iLength - 1
            If Results(i) = 0 Then
                Exit For
            End If
            sRet += Chr(Results(i))
        Next

        Return sRet

    End Function

    ''' <summary>
    ''' 读取WT信息.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadFailedPartInfo(ByVal name As String) As WT
        Dim _wt As New WT
        Dim _Failed As StructFailedPartInfo = CType(ReadAny(name, GetType(StructFailedPartInfo)), StructFailedPartInfo)
        If _Failed.strFailCarrierNr <> "" Then
            Try
                _wt.Number = Byte.Parse(_Failed.strFailCarrierNr.Replace("WT", ""))
            Catch ex As Exception
                _wt.Number = 0
            End Try
        End If
        _wt.PartFailLocation = _Failed.strFailStationNr
        _wt.PartFailCode = _Failed.strFailCode
        _wt.ArticleNumber = _Failed.strFailKostalNr
        _wt.PartFailLowerLimit = _Failed.strFailLowerLimit
        _wt.PartFailUpperLimit = _Failed.strFailUpperLimit
        _wt.Schedule = _Failed.strFailScheduleName
        _wt.SerialNumber = _Failed.strFailSerialNr
        _wt.PartFailTestStep = _Failed.strFailTestStep
        _wt.PartFailText = _Failed.strFailText
        _wt.PartFailValue = _Failed.strFailValue
        Return _wt
    End Function

    ''' <summary>
    ''' 读取托盘信息.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ReadCarrierInfo(ByVal mID As Integer) As WT
        Dim iCarrierID As Integer = 0
        Dim bFind As Boolean = False
        If AppSettings.LineType = enumLineType.MultiLine Then
            For Each item As structStationOverviewInfo In _dicOverviewInfo.Values
                If item.strStationName.ToUpper = "STATION" + mID.ToString("D02") Then
                    iCarrierID = item.iCarrierNumber
                    bFind = True
                    Exit For
                End If
            Next
            If Not bFind Then
                iCarrierID = mID
            End If
        Else
            iCarrierID = mID
        End If

        If iCarrierID <= 0 Then
            Return New WT
        End If
        mID = iCarrierID
        Dim _wt As New WT
        Dim _Failed As StructFailedPartInfo

        If _PLCConfig.PLCVersion >= 2.0 Then
            Dim _StructCarrierInfo As StructCarrierInfo_V2_00
            _StructCarrierInfo = CType(ReadChangePLCVersion(GetDeviceNotificationEx(".PLC_arrCarrierInfo[" + mID.ToString + "]")), StructCarrierInfo_V2_00)
            _Failed = _StructCarrierInfo.stuFailedPartInfo
            _wt.Number = Byte.Parse(mID.ToString)
            _wt.PartFailLocation = _Failed.strFailStationNr
            _wt.PartFailCode = _Failed.strFailCode
            _wt.ArticleNumber = _Failed.strFailKostalNr
            _wt.PartFailLowerLimit = _Failed.strFailLowerLimit
            _wt.PartFailUpperLimit = _Failed.strFailUpperLimit
            _wt.Schedule = _Failed.strFailScheduleName
            _wt.SerialNumber = _Failed.strFailSerialNr
            _wt.PartFailTestStep = _Failed.strFailTestStep
            _wt.PartFailText = _Failed.strFailText
            _wt.PartFailValue = _Failed.strFailValue
            _wt.Target = _StructCarrierInfo.bytDestinationStation.ToString
            _wt.SerialNumber = _StructCarrierInfo.stuVariantInfoSet.strSerialNr
            _wt.ArticleNumber = _StructCarrierInfo.stuVariantInfoSet.strKostalNr
            _wt.ReferencePart = _StructCarrierInfo.bulReferencePart
            _wt.Status = IIf(_StructCarrierInfo.bulTestResult, "PASS", "FAIL").ToString
        Else
            Dim _StructCarrierInfo As StructCarrierInfo
            _StructCarrierInfo = CType(ReadChangePLCVersion(GetDeviceNotificationEx(".PLC_arrCarrierInfo[" + mID.ToString + "]")), StructCarrierInfo)
            _Failed = _StructCarrierInfo.stuFailedPartInfo
            _wt.Number = Byte.Parse(mID.ToString)
            _wt.PartFailLocation = _Failed.strFailStationNr
            _wt.PartFailCode = _Failed.strFailCode
            _wt.ArticleNumber = _Failed.strFailKostalNr
            _wt.PartFailLowerLimit = _Failed.strFailLowerLimit
            _wt.PartFailUpperLimit = _Failed.strFailUpperLimit
            _wt.Schedule = _Failed.strFailScheduleName
            _wt.SerialNumber = _Failed.strFailSerialNr
            _wt.PartFailTestStep = _Failed.strFailTestStep
            _wt.PartFailText = _Failed.strFailText
            _wt.PartFailValue = _Failed.strFailValue
            _wt.ReferencePart = False
            _wt.Target = _StructCarrierInfo.bytDestinationStation.ToString
            _wt.SerialNumber = _StructCarrierInfo.stuVariantInfoSet.strSerialNr
            _wt.ArticleNumber = _StructCarrierInfo.stuVariantInfoSet.strKostalNr
            _wt.Status = IIf(_StructCarrierInfo.bulTestResult, "PASS", "FAIL").ToString
        End If


        Return _wt
    End Function

    ''' <summary>
    ''' Reset托盘信息.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ResetCarrierInfo(ByVal mID As Integer) As Boolean
        Dim iCarrierID As Integer = 0
        Dim bFind As Boolean = False
        If AppSettings.LineType = enumLineType.MultiLine Then
            For Each item As structStationOverviewInfo In _dicOverviewInfo.Values
                If item.strStationName.ToUpper = "STATION" + mID.ToString("D02") Then
                    iCarrierID = item.iCarrierNumber
                    bFind = True
                    Exit For
                End If
            Next
            If Not bFind Then
                iCarrierID = mID
            End If
        Else
            iCarrierID = mID
        End If

        If iCarrierID <= 0 Then
            Return True
        End If
        mID = iCarrierID

        Dim wtRead As WT = ReadCarrierInfo(mID)
        ' If wtRead.ReferencePart Then Return True

        Dim _wt As New WT
        If Not lPLCVairablesHandles.ContainsKey(".PLC_arrCarrierInfo[" + mID.ToString + "]") Then
            AddAdsVariable(".PLC_arrCarrierInfo[" + mID.ToString + "]")
        End If
        If _PLCConfig.PLCVersion >= 2.0 Then
            Dim _NewCarrierInfo As New StructCarrierInfo_V2_00
            _NewCarrierInfo.bulTestResult = True
            WriteAny(".PLC_arrCarrierInfo[" + mID.ToString + "]", _NewCarrierInfo)
        Else
            Dim _NewCarrierInfo As New StructCarrierInfo
            _NewCarrierInfo.bulTestResult = True
            WriteAny(".PLC_arrCarrierInfo[" + mID.ToString + "]", _NewCarrierInfo)
        End If
        Return True
    End Function


    Public Function AbortCarrierInfo(ByVal mID As Integer) As Boolean
        Dim iCarrierID As Integer = 0
        Dim bFind As Boolean = False
        If AppSettings.LineType = enumLineType.MultiLine Then
            For Each item As structStationOverviewInfo In _dicOverviewInfo.Values
                If item.strStationName.ToUpper = "STATION" + mID.ToString("D02") Then
                    iCarrierID = item.iCarrierNumber
                    bFind = True
                    Exit For
                End If
            Next
            If Not bFind Then
                iCarrierID = mID
            End If
        Else
            iCarrierID = mID
        End If


        If iCarrierID <= 0 Then
            Return True
        End If
        mID = iCarrierID

        Dim wtRead As WT = ReadCarrierInfo(mID)
        If wtRead.ReferencePart Then Return True

        Dim _wt As New WT
        If Not lPLCVairablesHandles.ContainsKey(".PLC_arrCarrierInfo[" + mID.ToString + "]") Then
            AddAdsVariable(".PLC_arrCarrierInfo[" + mID.ToString + "]")
        End If
        If _PLCConfig.PLCVersion >= 2.0 Then
            Dim _NewCarrierInfo As StructCarrierInfo_V2_00 = CType(ReadChangePLCVersion(GetDeviceNotificationEx(".PLC_arrCarrierInfo[" + mID.ToString + "]")), StructCarrierInfo_V2_00)
            _NewCarrierInfo.bulTestResult = False
            _NewCarrierInfo.bytDestinationStation = 1
            _NewCarrierInfo.stuFailedPartInfo.strFailText = "Manual Abort"
            WriteAny(".PLC_arrCarrierInfo[" + mID.ToString + "]", _NewCarrierInfo)
        Else
            Dim _NewCarrierInfo As StructCarrierInfo = CType(ReadChangePLCVersion(GetDeviceNotificationEx(".PLC_arrCarrierInfo[" + mID.ToString + "]")), StructCarrierInfo)
            _NewCarrierInfo.bulTestResult = False
            _NewCarrierInfo.bytDestinationStation = 1
            _NewCarrierInfo.stuFailedPartInfo.strFailText = "Manual Abort"
            WriteAny(".PLC_arrCarrierInfo[" + mID.ToString + "]", _NewCarrierInfo)
        End If

        Return True
    End Function

    ''' <summary>
    ''' 写入ADS信息.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function WriteAny(ByVal name As String, ByVal value As Object, Optional ByVal args() As Integer = Nothing) As Boolean
        Try
            SyncLock _Object
                If _IsDisabled Then Return True
                name = ChangeAdsName(name)
                If Not lPLCVairablesHandles.ContainsKey(name) Then
                    AddAdsVariable(name)
                End If
                Try
                    TcAds.WriteAny(lPLCVairablesHandles(name), value, args)
                    'mStateInfo = TcAds.ReadState.AdsState.ToString
                    Return True
                Catch ex As Exception
                    _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR11, name, ex.Message.ToString + ex.StackTrace)
                    Return False
                End Try
            End SyncLock
        Catch ex As Exception
            _StatusDescription = MyLanguage.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TWINCAT_ERROR11, name, ex.Message.ToString+ ex.StackTrace)
            Throw New Exception(_StatusDescription)
            Return False
        End Try

    End Function

    Public Function ReadChangePLCVersion(ByVal ObjectValue As Object) As Object
        If TypeOf ObjectValue Is StructCarrierInfo_NoUserDefine Then
            Dim ObjectReturnValue As StructCarrierInfo = New StructCarrierInfo
            ObjectReturnValue.bulTestResult = CType(ObjectValue, StructCarrierInfo_NoUserDefine).bulTestResult
            ObjectReturnValue.bytAssemblyCounter = CType(ObjectValue, StructCarrierInfo_NoUserDefine).bytAssemblyCounter
            ObjectReturnValue.bytCycleNr = CType(ObjectValue, StructCarrierInfo_NoUserDefine).bytCycleNr
            ObjectReturnValue.bytDestinationStation = CType(ObjectValue, StructCarrierInfo_NoUserDefine).bytDestinationStation
            ObjectReturnValue.bytScheduleModeNr = CType(ObjectValue, StructCarrierInfo_NoUserDefine).bytScheduleModeNr
            ObjectReturnValue.stuFailedPartInfo = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuFailedPartInfo
            ObjectReturnValue.stuVariantInfoSet.strCustomerNr = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuVariantInfoSet.strCustomerNr
            ObjectReturnValue.stuVariantInfoSet.strKostalArticleName = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuVariantInfoSet.strKostalArticleName
            ObjectReturnValue.stuVariantInfoSet.strKostalNr = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuVariantInfoSet.strKostalNr
            ObjectReturnValue.stuVariantInfoSet.strProductFamily = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuVariantInfoSet.strProductFamily
            ObjectReturnValue.stuVariantInfoSet.strSerialNr = CType(ObjectValue, StructCarrierInfo_NoUserDefine).stuVariantInfoSet.strSerialNr
            ObjectReturnValue.stuVariantInfoSet.strUserDefine = ""
            Return ObjectReturnValue
        End If

        If TypeOf ObjectValue Is StructCarrierInfo_V1_08 Then
            Dim ObjectReturnValue As StructCarrierInfo = New StructCarrierInfo
            ObjectReturnValue.bulTestResult = CType(ObjectValue, StructCarrierInfo_V1_08).bulTestResult
            ObjectReturnValue.bytAssemblyCounter = CType(ObjectValue, StructCarrierInfo_V1_08).bytAssemblyCounter
            ObjectReturnValue.bytCycleNr = CType(ObjectValue, StructCarrierInfo_V1_08).bytCycleNr
            ObjectReturnValue.bytDestinationStation = CType(ObjectValue, StructCarrierInfo_V1_08).bytDestinationStation
            ObjectReturnValue.bytScheduleModeNr = CType(ObjectValue, StructCarrierInfo_V1_08).bytScheduleModeNr
            ObjectReturnValue.stuFailedPartInfo = CType(ObjectValue, StructCarrierInfo_V1_08).stuFailedPartInfo
            ObjectReturnValue.stuVariantInfoSet.strCustomerNr = CType(ObjectValue, StructCarrierInfo_V1_08).stuVariantInfoSet.strCustomerNr
            ObjectReturnValue.stuVariantInfoSet.strKostalArticleName = CType(ObjectValue, StructCarrierInfo_V1_08).stuVariantInfoSet.strKostalArticleName
            ObjectReturnValue.stuVariantInfoSet.strKostalNr = CType(ObjectValue, StructCarrierInfo_V1_08).stuVariantInfoSet.strKostalNr
            ObjectReturnValue.stuVariantInfoSet.strProductFamily = CType(ObjectValue, StructCarrierInfo_V1_08).stuVariantInfoSet.strProductFamily
            ObjectReturnValue.stuVariantInfoSet.strSerialNr = CType(ObjectValue, StructCarrierInfo_V1_08).stuVariantInfoSet.strSerialNr
            ObjectReturnValue.stuVariantInfoSet.strUserDefine = ""
            Return ObjectReturnValue
        End If

        If TypeOf ObjectValue Is StructCarrierInfo_V2_00 Then
            'Dim ObjectReturnValue As StructCarrierInfo = New StructCarrierInfo
            'ObjectReturnValue.bulTestResult = CType(ObjectValue, StructCarrierInfo_V2_00).bulTestResult
            'ObjectReturnValue.bytAssemblyCounter = CType(ObjectValue, StructCarrierInfo_V2_00).bytAssemblyCounter
            'ObjectReturnValue.bytCycleNr = CType(ObjectValue, StructCarrierInfo_V2_00).bytCycleNr
            'ObjectReturnValue.bytDestinationStation = CType(ObjectValue, StructCarrierInfo_V2_00).bytDestinationStation
            'ObjectReturnValue.bytScheduleModeNr = CType(ObjectValue, StructCarrierInfo_V2_00).bytScheduleModeNr
            'ObjectReturnValue.stuFailedPartInfo = CType(ObjectValue, StructCarrierInfo_V2_00).stuFailedPartInfo
            'ObjectReturnValue.stuVariantInfoSet.strCustomerNr = CType(ObjectValue, StructCarrierInfo_V2_00).stuVariantInfoSet.strCustomerNr
            'ObjectReturnValue.stuVariantInfoSet.strKostalArticleName = CType(ObjectValue, StructCarrierInfo_V2_00).stuVariantInfoSet.strKostalArticleName
            'ObjectReturnValue.stuVariantInfoSet.strKostalNr = CType(ObjectValue, StructCarrierInfo_V2_00).stuVariantInfoSet.strKostalNr
            'ObjectReturnValue.stuVariantInfoSet.strProductFamily = CType(ObjectValue, StructCarrierInfo_V2_00).stuVariantInfoSet.strProductFamily
            'ObjectReturnValue.stuVariantInfoSet.strSerialNr = CType(ObjectValue, StructCarrierInfo_V2_00).stuVariantInfoSet.strSerialNr
            'ObjectReturnValue.stuVariantInfoSet.strUserDefine = ""
            Return ObjectValue
        End If

        If TypeOf ObjectValue Is StructDeviceInteraction_NoUserDefine Then
            Dim ObjectReturnValue As StructDeviceInteraction = New StructDeviceInteraction
            ObjectReturnValue.bulAdsActionIsFail = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).bulAdsActionIsFail
            ObjectReturnValue.bulAdsActionIsPass = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).bulAdsActionIsPass
            ObjectReturnValue.bulPlcDoAction = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).bulPlcDoAction
            ObjectReturnValue.strAdsActionValue = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).strAdsActionValue
            ObjectReturnValue.stuPlcArticleSet.strCustomerNr = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).stuPlcArticleSet.strCustomerNr
            ObjectReturnValue.stuPlcArticleSet.strKostalArticleName = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).stuPlcArticleSet.strKostalArticleName
            ObjectReturnValue.stuPlcArticleSet.strKostalNr = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).stuPlcArticleSet.strKostalNr
            ObjectReturnValue.stuPlcArticleSet.strProductFamily = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).stuPlcArticleSet.strProductFamily
            ObjectReturnValue.stuPlcArticleSet.strSerialNr = CType(ObjectValue, StructDeviceInteraction_NoUserDefine).stuPlcArticleSet.strSerialNr
            ObjectReturnValue.stuPlcArticleSet.strUserDefine = ""
            Return ObjectReturnValue
        End If

        If TypeOf ObjectValue Is StructRequestAction_NoUserDefine Then
            Dim ObjectReturnValue As StructRequestAction = New StructRequestAction
            ObjectReturnValue.bulDoNegativeAction = CType(ObjectValue, StructRequestAction_NoUserDefine).bulDoNegativeAction
            ObjectReturnValue.bulDoPositiveAction = CType(ObjectValue, StructRequestAction_NoUserDefine).bulDoPositiveAction
            ObjectReturnValue.bulRunning = CType(ObjectValue, StructRequestAction_NoUserDefine).bulRunning
            ObjectReturnValue.strActionScheduleName = CType(ObjectValue, StructRequestAction_NoUserDefine).strActionScheduleName
            ObjectReturnValue.stuPlcArticleSet.strCustomerNr = CType(ObjectValue, StructRequestAction_NoUserDefine).stuPlcArticleSet.strCustomerNr
            ObjectReturnValue.stuPlcArticleSet.strKostalArticleName = CType(ObjectValue, StructRequestAction_NoUserDefine).stuPlcArticleSet.strKostalArticleName
            ObjectReturnValue.stuPlcArticleSet.strKostalNr = CType(ObjectValue, StructRequestAction_NoUserDefine).stuPlcArticleSet.strKostalNr
            ObjectReturnValue.stuPlcArticleSet.strProductFamily = CType(ObjectValue, StructRequestAction_NoUserDefine).stuPlcArticleSet.strProductFamily
            ObjectReturnValue.stuPlcArticleSet.strSerialNr = CType(ObjectValue, StructRequestAction_NoUserDefine).stuPlcArticleSet.strSerialNr
            ObjectReturnValue.stuPlcArticleSet.strUserDefine = ""
            Return ObjectReturnValue
        End If

        If TypeOf ObjectValue Is StructVariantInfo_NoUserDefine Then
            Dim ObjectReturnValue As StructVariantInfo = New StructVariantInfo
            ObjectReturnValue.strCustomerNr = CType(ObjectValue, StructVariantInfo_NoUserDefine).strCustomerNr
            ObjectReturnValue.strKostalArticleName = CType(ObjectValue, StructVariantInfo_NoUserDefine).strKostalArticleName
            ObjectReturnValue.strKostalNr = CType(ObjectValue, StructVariantInfo_NoUserDefine).strKostalNr
            ObjectReturnValue.strProductFamily = CType(ObjectValue, StructVariantInfo_NoUserDefine).strProductFamily
            ObjectReturnValue.strSerialNr = CType(ObjectValue, StructVariantInfo_NoUserDefine).strSerialNr
            ObjectReturnValue.strUserDefine = ""
            Return ObjectReturnValue
        End If
        Return ObjectValue
    End Function


    Public Function WriteChangePLCVersion(ByVal ObjectValue As Object) As Object
        If TypeOf ObjectValue Is StructScheduleMode_V2_00 Then
            Dim ObjectReturnValue As StructScheduleMode = New StructScheduleMode
            For i As Integer = 0 To CON_MAXIMUM_TOTAL_STATIONS - 1
                ObjectReturnValue.arrScheduleData(i, 0) = CType(ObjectValue, StructScheduleMode_V2_00).arrScheduleData(i, 0).arrDestinationStationData(0)
                ObjectReturnValue.arrScheduleData(i, 1) = CType(ObjectValue, StructScheduleMode_V2_00).arrScheduleData(i, 1).arrDestinationStationData(0)
            Next
            ObjectReturnValue.bytScheduleNr = CType(ObjectValue, StructScheduleMode_V2_00).bytScheduleNr
            ObjectReturnValue.intSecurityChecksum = CType(ObjectValue, StructScheduleMode_V2_00).intSecurityChecksum
            ObjectReturnValue.strScheduleDescription = CType(ObjectValue, StructScheduleMode_V2_00).strScheduleDescription
            ObjectReturnValue.strScheduleName = CType(ObjectValue, StructScheduleMode_V2_00).strScheduleName
            Return ObjectReturnValue
        End If

        If Not _PLCConfig.UserDefine Then
            If TypeOf ObjectValue Is StructCarrierInfo Then
                Dim ObjectReturnValue As StructCarrierInfo_NoUserDefine = New StructCarrierInfo_NoUserDefine
                ObjectReturnValue.bulTestResult = CType(ObjectValue, StructCarrierInfo).bulTestResult
                ObjectReturnValue.bytAssemblyCounter = CType(ObjectValue, StructCarrierInfo).bytAssemblyCounter
                ObjectReturnValue.bytCycleNr = CType(ObjectValue, StructCarrierInfo).bytCycleNr
                ObjectReturnValue.bytDestinationStation = CType(ObjectValue, StructCarrierInfo).bytDestinationStation
                ObjectReturnValue.bytScheduleModeNr = CType(ObjectValue, StructCarrierInfo).bytScheduleModeNr
                ObjectReturnValue.stuFailedPartInfo = CType(ObjectValue, StructCarrierInfo).stuFailedPartInfo
                ObjectReturnValue.stuVariantInfoSet.strCustomerNr = CType(ObjectValue, StructCarrierInfo).stuVariantInfoSet.strCustomerNr
                ObjectReturnValue.stuVariantInfoSet.strKostalArticleName = CType(ObjectValue, StructCarrierInfo).stuVariantInfoSet.strKostalArticleName
                ObjectReturnValue.stuVariantInfoSet.strKostalNr = CType(ObjectValue, StructCarrierInfo).stuVariantInfoSet.strKostalNr
                ObjectReturnValue.stuVariantInfoSet.strProductFamily = CType(ObjectValue, StructCarrierInfo).stuVariantInfoSet.strProductFamily
                ObjectReturnValue.stuVariantInfoSet.strSerialNr = CType(ObjectValue, StructCarrierInfo).stuVariantInfoSet.strSerialNr
                Return ObjectReturnValue
            End If

            If TypeOf ObjectValue Is StructDeviceInteraction Then
                Dim ObjectReturnValue As StructDeviceInteraction_NoUserDefine = New StructDeviceInteraction_NoUserDefine
                ObjectReturnValue.bulAdsActionIsFail = CType(ObjectValue, StructDeviceInteraction).bulAdsActionIsFail
                ObjectReturnValue.bulAdsActionIsPass = CType(ObjectValue, StructDeviceInteraction).bulAdsActionIsPass
                ObjectReturnValue.bulPlcDoAction = CType(ObjectValue, StructDeviceInteraction).bulPlcDoAction
                ObjectReturnValue.strAdsActionValue = CType(ObjectValue, StructDeviceInteraction).strAdsActionValue
                ObjectReturnValue.stuPlcArticleSet.strCustomerNr = CType(ObjectValue, StructDeviceInteraction).stuPlcArticleSet.strCustomerNr
                ObjectReturnValue.stuPlcArticleSet.strKostalArticleName = CType(ObjectValue, StructDeviceInteraction).stuPlcArticleSet.strKostalArticleName
                ObjectReturnValue.stuPlcArticleSet.strKostalNr = CType(ObjectValue, StructDeviceInteraction).stuPlcArticleSet.strKostalNr
                ObjectReturnValue.stuPlcArticleSet.strProductFamily = CType(ObjectValue, StructDeviceInteraction).stuPlcArticleSet.strProductFamily
                ObjectReturnValue.stuPlcArticleSet.strSerialNr = CType(ObjectValue, StructDeviceInteraction).stuPlcArticleSet.strSerialNr
                Return ObjectReturnValue
            End If

            If TypeOf ObjectValue Is StructRequestAction Then
                Dim ObjectReturnValue As StructRequestAction_NoUserDefine = New StructRequestAction_NoUserDefine
                ObjectReturnValue.bulDoNegativeAction = CType(ObjectValue, StructRequestAction).bulDoNegativeAction
                ObjectReturnValue.bulDoPositiveAction = CType(ObjectValue, StructRequestAction).bulDoPositiveAction
                ObjectReturnValue.bulRunning = CType(ObjectValue, StructRequestAction).bulRunning
                ObjectReturnValue.strActionScheduleName = CType(ObjectValue, StructRequestAction).strActionScheduleName
                ObjectReturnValue.stuPlcArticleSet.strCustomerNr = CType(ObjectValue, StructRequestAction).stuPlcArticleSet.strCustomerNr
                ObjectReturnValue.stuPlcArticleSet.strKostalArticleName = CType(ObjectValue, StructRequestAction).stuPlcArticleSet.strKostalArticleName
                ObjectReturnValue.stuPlcArticleSet.strKostalNr = CType(ObjectValue, StructRequestAction).stuPlcArticleSet.strKostalNr
                ObjectReturnValue.stuPlcArticleSet.strProductFamily = CType(ObjectValue, StructRequestAction).stuPlcArticleSet.strProductFamily
                ObjectReturnValue.stuPlcArticleSet.strSerialNr = CType(ObjectValue, StructRequestAction).stuPlcArticleSet.strSerialNr
                Return ObjectReturnValue
            End If
            If TypeOf ObjectValue Is StructVariantInfo Then
                Dim ObjectReturnValue As StructVariantInfo_NoUserDefine = New StructVariantInfo_NoUserDefine
                ObjectReturnValue.strCustomerNr = CType(ObjectValue, StructVariantInfo).strCustomerNr
                ObjectReturnValue.strKostalArticleName = CType(ObjectValue, StructVariantInfo).strKostalArticleName
                ObjectReturnValue.strKostalNr = CType(ObjectValue, StructVariantInfo).strKostalNr
                ObjectReturnValue.strProductFamily = CType(ObjectValue, StructVariantInfo).strProductFamily
                ObjectReturnValue.strSerialNr = CType(ObjectValue, StructVariantInfo).strSerialNr
                Return ObjectReturnValue
            End If
        End If
        Return ObjectValue
    End Function

    Public Function Readfield(ByVal o As Object, ByVal field As String) As Object
        Dim res As Object = Nothing
        Dim currName As String = ""
        Dim strArr As String() = o.ToString.Split(CChar(";"))
        Dim str As String = ""

        Dim fields As New List(Of String)
        Try


            If o IsNot Nothing Then
                For Each str In strArr
                    If str.Contains(field) Then
                        strArr = str.Split(CChar("="))
                        str = strArr(1)
                        Return str
                    End If
                Next

                fields.AddRange(field.Split(CChar(".")))
            End If

            If fields.Count = 1 Then

                If o.ToString.Contains(field) Then

                End If

                Return o.GetType.GetField(fields(0)).GetValue(o)
            Else
                'Dim currObj As Object = o.GetType.GetField(fields(0)).GetValue(o)
                fields.RemoveAt(0)
                Return Readfield(o, field)
            End If

            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 释放.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose

        bExit = True
        Dim iCnt As Integer = 50000
        Do While iCnt > 0
            If IsNothing(cThread) Then
                Exit Do
            End If
            If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                Exit Do
            End If
            iCnt = iCnt - 1
            System.Threading.Thread.Sleep(1)
        Loop

        If Not IsDisposed Then
            'delete registered notifications.
            Try
                ' Dim iHandle As Integer
                For Each element As String In lDeviceNotificationEx.Keys
                    If lPLCVairablesHandles.ContainsKey(element) Then
                        _TcAds.DeleteVariableHandle(lPLCVairablesHandles(element))
                        lPLCVairablesHandles.Remove(element)
                    End If
                Next

            Catch ex As Exception
                Dim strErr As String = ex.Message
            End Try
            lPLCVairablesHandles.Clear()
            lPLCVairablesHandles = Nothing

            GC.SuppressFinalize(Me)
            Finalize()
        End If

    End Sub

    Protected Overrides Sub Finalize()
        Try
            TcAds.Dispose()
            TcAds = Nothing
        Catch ex As Exception
            '
        End Try
        IsDisposed = True
        MyBase.Finalize()
    End Sub
End Class

Public Class clsAdsCfg
    Private strName As String
    Private cObjectType As Type
    Private cargs As Integer
    Public cObjectValue As Object

    Public Property Name As String
        Set(ByVal value As String)
            strName = value
        End Set
        Get
            Return strName
        End Get
    End Property
    Public Property ObjectType As Type
        Set(ByVal value As Type)
            cObjectType = value
        End Set
        Get
            Return cObjectType
        End Get
    End Property
    Public Property args As Integer
        Set(ByVal value As Integer)
            cargs = value
        End Set
        Get
            Return cargs
        End Get
    End Property
    Sub New(ByVal strName As String, ByVal cObjectType As Type, ByVal cargs As Integer, ByVal cObjectValue As Object)
        Me.strName = strName
        Me.cObjectType = cObjectType
        Me.cargs = cargs
        Me.cObjectValue = cObjectValue
    End Sub
End Class




