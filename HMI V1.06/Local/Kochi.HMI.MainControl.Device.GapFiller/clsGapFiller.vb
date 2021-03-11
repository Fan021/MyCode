Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Threading
Imports TwinCAT.Ads
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("GapFiller", "GapFiller")>
Public Class clsGapFiller
    Inherits clsHMIGapFiller
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private lListPoint As New Dictionary(Of String, clsPointCfg)
    Private bExit As Boolean = False
    Private cThread As Thread
    Public cBDTronic As BDTronic
    Public lListWeightUI As New List(Of WeightUI)
    Private cErrorMessageManager As clsErrorMessageManager
    Public Const FormName As String = "GapFillerControlUI"
    Private lListGFile As New Dictionary(Of String, clsGFilePathCfg)
    Public WithEvents TcAds As TcAdsClient
    Public strLastProgram As String
    Private strLastValue As String = ""
    Private strPPSstrPartNoA As String = ""
    Private strPPSstrVolumeA As String = ""
    Private strPPSstrExpiryDateA As String = ""
    Private strPPSstrBatchNoA As String = ""
    Private strPPSstrSupplierNoA As String = ""
    Private strPPSstrPackagingNoA As String = ""
    Private strPPSstrPartNoB As String = ""
    Private strPPSstrVolumeB As String = ""
    Private strPPSstrExpiryDateB As String = ""
    Private strPPSstrBatchNoB As String = ""
    Private strPPSstrSupplierNoB As String = ""
    Private strPPSstrPackagingNoB As String = ""
    Private strLastTemp As String = String.Empty
    Private strNowLine1 As String = String.Empty
    Private strNowLine2 As String = String.Empty
    Private strNowLine3 As String = String.Empty
    Private strLastLine1 As String = String.Empty
    Private strLastLine2 As String = String.Empty
    Private strLastLine3 As String = String.Empty
    Public Overrides Property PPSstrPartNoA As String
        Get
            SyncLock _Object
                Return strPPSstrPartNoA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrPartNoA = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrVolumeA As String
        Get
            SyncLock _Object
                Return strPPSstrVolumeA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrVolumeA = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrExpiryDateA As String
        Get
            SyncLock _Object
                Return strPPSstrExpiryDateA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrExpiryDateA = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrBatchNoA As String
        Get
            SyncLock _Object
                Return strPPSstrBatchNoA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrBatchNoA = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrSupplierNoA As String
        Get
            SyncLock _Object
                Return strPPSstrSupplierNoA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrSupplierNoA = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrPackagingNoA As String
        Get
            SyncLock _Object
                Return strPPSstrPackagingNoA
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrPackagingNoA = value
            End SyncLock
        End Set
    End Property



    Public Overrides Property PPSstrPartNoB As String
        Get
            SyncLock _Object
                Return strPPSstrPartNoB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrPartNoB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrVolumeB As String
        Get
            SyncLock _Object
                Return strPPSstrVolumeB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrVolumeB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrExpiryDateB As String
        Get
            SyncLock _Object
                Return strPPSstrExpiryDateB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrExpiryDateB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrBatchNoB As String
        Get
            SyncLock _Object
                Return strPPSstrBatchNoB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrBatchNoB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrSupplierNoB As String
        Get
            SyncLock _Object
                Return strPPSstrSupplierNoB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrSupplierNoB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property PPSstrPackagingNoB As String
        Get
            SyncLock _Object
                Return strPPSstrPackagingNoB
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strPPSstrPackagingNoB = value
            End SyncLock
        End Set
    End Property

    Public Overrides Property LastProgram As String
        Get
            SyncLock _Object
                Return strLastProgram
            End SyncLock
        End Get
        Set(ByVal value As String)
            SyncLock _Object
                strLastProgram = value
            End SyncLock
        End Set
    End Property

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)

        Me.lListInitParameter = lListInitParameter
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "3"), enumExceptionType.Crash)
            Return False
        End If
        Dim cPlcDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cHMIPLC.Name)
        TcAds = New TcAdsClient
        Dim listValue As List(Of String) = clsParameter.ToList(cPlcDeviceCfg.InitParameter)
        TcAds.Connect(listValue(0), CInt(500))
        'TcAds.Connect("169.254.157.20.1.1", CInt(500))
        'TcAds.Connect(listValue(0), CInt(801))
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)

        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        lListPoint.Clear()
        lListPoint.Add("Service Position", New clsPointCfg)
        lListPoint.Add("Blindshot Position", New clsPointCfg)
        lListPoint.Add("Purging Position", New clsPointCfg)
        lListPoint.Add("Weighing Position", New clsPointCfg)
        lListPoint.Add("TwinSafe Position", New clsPointCfg)
        lListPoint.Add("NeedleCheck Position", New clsPointCfg)
        Dim mTempValue As String = String.Empty
        Dim cPoint(9) As StructPoint
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            cPoint(i) = New StructPoint
            If i <= lListPoint.Count - 1 Then
                cPoint(i).strHMIName = lListPoint.Keys(i)
                cPoint(i).fdXPosition = lListPoint(lListPoint.Keys(i)).X
                cPoint(i).fdYPosition = lListPoint(lListPoint.Keys(i)).Y
                cPoint(i).fdZPosition = lListPoint(lListPoint.Keys(i)).Z
            End If
        Next
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)


        lListGFile.Clear()
        mTempValue = String.Empty
        Dim cGFile(19) As StructGFile
        For i = 0 To 19
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Name")
            Dim cGFilePathCfg As New clsGFilePathCfg
            cGFilePathCfg.Name = mTempValue
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Path")
            cGFilePathCfg.Path = mTempValue
            lListGFile.Add(i.ToString, cGFilePathCfg)
            cGFile(i) = New StructGFile
            cGFile(i).strName = lListGFile(i).Name
            cGFile(i).strPath = lListGFile(i).Path
        Next
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_SystemGFile", cGFile)

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed")
        If mTempValue = "" Then
            mTempValue = "10"
        End If
        Dim fNewValue As Int16 = CInt(mTempValue)
        If fNewValue <= 0 Then fNewValue = 10
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", Int16.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Needle")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMINeedleDiameter", Single.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_MAX")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMAXOffsetXY", Single.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Automatic")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutomaticCheck", Int16.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_HS_Confirm")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        ' cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIHshakeNo", Int16.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_PotLife")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIPotLiftTime", Single.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_BlindTime")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindShotTime", Single.Parse(mTempValue))


        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_BlindNo")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindNo", Int16.Parse(mTempValue))

        For i = 1 To 3
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Max" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIWeightMax", Single.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Min" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIWeightMin", Single.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_PreShot" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIPrepShots", Int16.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_PreShotTime" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIPrepShot", Single.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Pause" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIPrepPause", Single.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_PerCycle" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIShotsPreCycle", Int16.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_DispensingTime" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIDispensing", Single.Parse(mTempValue))


            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_DispensingPause" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMIDispensingPause", Single.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "CheckBox_AutoWeight" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "True"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMAutoWeight", Boolean.Parse(mTempValue))

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_AutoWeightNo" + i.ToString)
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + (i).ToString + "].fdHMAutoWeightNr", Int16.Parse(mTempValue))
        Next


        Dim cDefaultValue() As Double = Enumerable.Repeat(New Double, 100).ToArray()
        For j = 0 To 9
            Dim cRCfg As New clsRCfg
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "R" + j.ToString, "Index")
            If mTempValue = "" Then
                cRCfg.iIndex = j
            Else
                cRCfg.iIndex = CInt(mTempValue)
            End If

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "R" + j.ToString, "Value")
            If mTempValue = "" Then
                mTempValue = "0"
            End If
            cDefaultValue(cRCfg.iIndex) = Double.Parse(mTempValue)
        Next

        cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_RFucntion", cDefaultValue)

        If Not cLocalElement.ContainsKey(clsErrorMessageManager.Name) Then
            cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
        End If
        cBDTronic = New BDTronic
        cBDTronic.ObjectSource = Me
        cBDTronic.FontSize = 10
        cBDTronic.ReadOnly = True
        cBDTronic.Init(cLocalElement, cSystemElement)

        Dim cWeightUI As New WeightUI
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.A
        cWeightUI.Index = 1
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = True
        cWeightUI.ObjectSource = Me
        cWeightUI.Init(cLocalElement, cSystemElement)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = New WeightUI
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.B
        cWeightUI.Index = 2
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = True
        cWeightUI.ObjectSource = Me
        cWeightUI.Init(cLocalElement, cSystemElement)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = New WeightUI
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.AB
        cWeightUI.Index = 3
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = True
        cWeightUI.ObjectSource = Me
        cWeightUI.Init(cLocalElement, cSystemElement)
        lListWeightUI.Add(cWeightUI)
        If Not IsNothing(cBDTronic) Then cBDTronic.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        For Each element As WeightUI In lListWeightUI
            element.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        Next
        StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function


    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iControlUI) Then
                iControlUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iParameterUI) Then
                iParameterUI.Quit(cLocalElement, cSystemElement)
            End If
            StopRefresh(cLocalElement, cSystemElement)
            If Not IsNothing(cBDTronic) Then cBDTronic.StopRefresh(cLocalElement, cSystemElement)
            For Each element As WeightUI In lListWeightUI
                element.StopReflesh()
            Next
            If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
                If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
            End If
            Dim TempStructGapFiller As New StructGapFiller
            If lListInitParameter.Count >= 1 Then
                TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                TempStructGapFiller.bulHMIAutoRefer = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisXHome = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisXReset = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisYHome = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisYReset = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisZHome = New StructGapFillerButton
                TempStructGapFiller.bulHMIAxisZReset = New StructGapFillerButton
                TempStructGapFiller.bulHMIBuild3D = New StructGapFillerButton
                TempStructGapFiller.bulHMICheckNeedle = New StructGapFillerButton
                TempStructGapFiller.bulHMIContinueEnable = False
                TempStructGapFiller.bulHMIFilling = New StructGapFillerButton
                TempStructGapFiller.bulHMIHSConfirm = False
                TempStructGapFiller.bulHMI3DReset = False
                TempStructGapFiller.bulHMILoadGFile = New StructGapFillerButton
                TempStructGapFiller.bulHMIMotorEnable = False
                TempStructGapFiller.bulHMIRelease3D = New StructGapFillerButton
                TempStructGapFiller.bulHMIStart = False
                TempStructGapFiller.bulHMIStop = False
                TempStructGapFiller.bulHMIXBackward = False
                TempStructGapFiller.bulHMIXForward = False
                TempStructGapFiller.bulHMIYBackward = False
                TempStructGapFiller.bulHMIYForward = False
                TempStructGapFiller.bulHMIZBackward = False
                TempStructGapFiller.bulHMIZForward = False
                TempStructGapFiller.fdHMIMove = New StructGapFillerButton
                TempStructGapFiller.fdHMIMoveXPosition = 0
                TempStructGapFiller.fdHMIMoveYPosition = 0
                TempStructGapFiller.fdHMIMoveZPosition = 0
                cHMIPLC.WriteAny(lListInitParameter(0), TempStructGapFiller)
            End If

            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then
            RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        StopRefresh(cLocalElement, cSystemElement)
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        MyBase.CreateShortcutUI(cLocalElement, cSystemElement)
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        StopRefresh(cLocalElement, cSystemElement)
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then
            RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iInitUI.Quit(cLocalElement, cSystemElement)
        End If
        iInitUI = New InitUI
        AddHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iParameterUI) Then
            RemoveHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iParameterUI.Quit(cLocalElement, cSystemElement)
        End If
        iParameterUI = New ParameterUI
        AddHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function
    Private Sub Read()
        SyncLock _Object
            Dim stream As New TwinCAT.Ads.AdsStream(2048)
            Dim nByte As Integer = 0
            Dim nRet As Integer = TcAds.Read(&H2300 + 2, &H20000001, stream)
            Dim S As New AdsBinaryReader(stream)
            ' Dim strTemp As String = S.ReadPlcString(2048, System.Text.Encoding.ASCII)
            Dim strTemp As String = S.ReadPlcAnsiString(2048)
            S.Close()
            stream.Close()
            stream.Dispose()

            'll.Add(strTemp)

            If strTemp.Split(vbCrLf).Length >= 3 And strTemp <> strLastTemp Then
                strLastValue = strLastValue + strTemp
                strLastTemp = strTemp
            End If

            Dim cValue() As String = strLastValue.Split(vbLf)
            If cValue.Length >= 2 Then
                strLastValue = ""
                strNowLine1 = cValue(0).Replace(vbCr, "")
                If cValue.Length >= 2 Then
                    strNowLine2 = cValue(1).Replace(vbCr, "")
                Else
                    strNowLine2 = ""
                End If

                If cValue.Length >= 3 Then
                    strNowLine3 = cValue(2).Replace(vbCr, "")
                Else
                    strNowLine3 = ""
                End If

                If strNowLine1 <> strLastLine1 And strNowLine1 <> strLastLine2 And strNowLine1 <> strLastLine3 Then
                    strLastProgram = strNowLine1
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine1
                    For i = 1 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
                If strNowLine2 <> strLastLine1 And strNowLine2 <> strLastLine2 And strNowLine2 <> strLastLine3 And strNowLine2 <> "" Then
                    strLastProgram = strNowLine2
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine2
                    For i = 2 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
                If strNowLine3 <> strLastLine1 And strNowLine3 <> strLastLine2 And strNowLine3 <> strLastLine3 And strNowLine3 <> "" Then
                    strLastProgram = strNowLine3
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine3
                    For i = 3 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
            End If

            If strTemp = "" Then
                strLastValue = ""
                strNowLine1 = ""
                strNowLine2 = ""
                strNowLine3 = ""
                strLastLine1 = ""
                strLastLine2 = ""
                strLastLine3 = ""
            End If
        End SyncLock
    End Sub
    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(clsGapFiller.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "9"), enumExceptionType.Alarm, clsGapFiller.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "10", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, clsGapFiller.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructGapFiller), New StructGapFiller)
                        iStep = iStep + 1

                    Case 4
                        If Not IsNothing(cBDTronic) Then cBDTronic.RefreshUI()
                        For Each element As WeightUI In lListWeightUI
                            element.RefreshUI()
                        Next
                        Read()
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, clsGapFiller.FormName))
            End Try


        End While

    End Sub
    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        If Not IsNothing(cBDTronic) Then cBDTronic.StopRefresh(cLocalElement, cSystemElement)
        For Each element As WeightUI In lListWeightUI
            element.StopReflesh()
        Next
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
        Dim iCnt As Integer = 100
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
        If Not IsNothing(cThread) Then cThread.Abort()
        'If Not IsNothing(cParameter) Then cParameter.StopRefresh(cLocalElement, cSystemElement)
        'For Each element As WeightUI In lListWeightUI
        '    element.StopReflesh()
        'Next
        'If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
        '    If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        'End If
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructGapFiller
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIZForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIZBackward As Boolean = False

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveZPosition As Single = 0

    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMISpeed As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIStep As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIContinueEnable As Boolean = True
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIMotorEnable As Boolean = False
    Public fdHMIMove As StructGapFillerButton
    Public bulHMICheckNeedle As StructGapFillerButton
    Public bulHMIAutoRefer As StructGapFillerButton
    Public bulHMIFilling As StructGapFillerButton
    Public bulHMIAxisXHome As StructGapFillerButton
    Public bulHMIAxisYHome As StructGapFillerButton
    Public bulHMIAxisZHome As StructGapFillerButton
    Public bulHMIAxisXReset As StructGapFillerButton
    Public bulHMIAxisYReset As StructGapFillerButton
    Public bulHMIAxisZReset As StructGapFillerButton
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMINeedleDiameter As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMAXOffsetXY As Single = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIAutomaticCheck As Int16 = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIStart As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIStop As Boolean = False
    Public bulHMIBuild3D As StructGapFillerButton
    Public bulHMIRelease3D As StructGapFillerButton
    Public bulHMILoadGFile As StructGapFillerButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIHSConfirm As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMI3DReset As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIHshakeNo As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIPotLiftTime As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIBlindShotTime As Single = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIBlindNo As Int16 = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIRRead As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIRWrite As Boolean = False
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=101)> Public fdHMICurrentGFilePath As String
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIStartBlindShot As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCStartBlindShot As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIStartClean As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCStartClean As Boolean = False
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMITimeFillingTime As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIStartTimeFilling As Boolean = False

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCPPSHandShake_active As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCPPSactOP_Mode As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCPPSactUser_Level As Byte = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSFillingLevelP1 As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSFillingLevelP2 As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSSupplyPressureP1 As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSSupplyPressureP2 As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSPressureP1Outlet As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCPPSPressureP2Outlet As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCPPSscanProcessReadyMES As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIPPSOP_Mode As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIPPSHS_MESok As Byte = 0

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrPartNoA As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrVolumeA As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrExpiryDateA As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrBatchNoA As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrSupplierNoA As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrPackagingNoA As String = ""

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrPartNoB As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrVolumeB As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrExpiryDateB As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrBatchNoB As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrSupplierNoB As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public bulPLCPPSstrPackagingNoB As String = ""


    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000Ready As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000Busy As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000System_OK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000ProcessCycle_OK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000ProcessCycle_NOK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000Handshake_active As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000actOP_Mode As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000requestPostiton As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000actRecipe As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCB2000actUserLevel As Byte = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCB2000FillingLevel1 As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public bulPLCB2000FillingLevel2 As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000Start As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000Filling As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000Cleaning As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000Material_OK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000Position As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000OP_Mode As Byte = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIB2000RecipeNumber As Byte = 0

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCZPosition As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisXHome As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisYHome As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisZHome As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisXReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisYReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCAxisZReset As Boolean = False
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCActualOffsetX As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCActualOffsetY As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCActualOffsetZ As Single = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPLCErrorCode As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCBlindShotTime As Single = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPLCBlindNo As Int16 = 0
    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdPLCNCErrorCode As Int32 = 0

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10, arraysubtype:=UnmanagedType.Struct)> Public cPoint(0 To 9) As StructPoint
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20, arraysubtype:=UnmanagedType.Struct)> Public cGFile(0 To 19) As StructGFile
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100, arraysubtype:=UnmanagedType.I1)> Public PLC_MFucntion(0 To 99) As Boolean
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100, arraysubtype:=UnmanagedType.R8)> Public PLC_RFucntion(0 To 99) As Double
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3, arraysubtype:=UnmanagedType.Struct)> Public PLC_Weight(0 To 2) As StructWeight
End Class


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructPoint
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strHMIName As String
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdXPosition As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdYPosition As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdZPosition As Single
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructWeightPoint
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCCurrentWeight As Single
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoaction As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCIsPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCIsFail As Boolean
End Structure


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructWeight
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIWeightMax As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIWeightMin As Single
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIPrepShots As Int16
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIPrepShot As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIPrepPause As Single
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIShotsPreCycle As Int16
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIDispensing As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIDispensingPause As Single
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdHMAutoWeight As Boolean
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMAutoWeightNr As Int16
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdHMIStartWeight As Boolean

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCCupPresent As Boolean
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCCurrentWeight As Single
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPLCShotsPreCycle As Int16
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPLCShotsInCycle As Int16
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCWeightResult As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdPLCStartWeight As Boolean
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPLCCurrentIndex As Int16
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=100, arraysubtype:=UnmanagedType.Struct)> Public PLC_WeightPoint() As StructWeightPoint
End Structure


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructGapFillerButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoAction As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean
End Structure


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructGFile
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strName As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=101)> Public strPath As String
End Structure