Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent

Public Class ControlUI
    Implements IControlUI

    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private lListPoint As New Dictionary(Of String, clsPointCfg)
    Private lListIO As New Dictionary(Of String, clsMFunctionCfg)
    Private lListWeightUI As New List(Of WeightUI)
    Private cVariantManager As clsVariantManager
    Protected cLanguageManager As clsLanguageManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Private mMainForm As IMainUI
    Private cIniHandler As clsIniHandler
    Private cGapFiller As clsGapFiller
    Private cSystemManager As clsSystemManager
    Private OldStructGapFiller As New StructGapFiller
    Private TempStructGapFiller As New StructGapFiller
    Private cNCI As NCI
    Private cGFile As GFile
    Private cParameter As Parameter
    Private cBDTronic As BDTronic
    Public Const FormName As String = "GapFillerControlUI"
    Private lListGFile As New Dictionary(Of String, clsGFilePathCfg)

    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cGapFiller = value
        End Set
        Get
            Return cGapFiller
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            InitForm()
            InitControlText()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean

        cNCI = New NCI
        cNCI.ObjectSource = cGapFiller
        cNCI.FontSize = 10
        cNCI.ReadOnly = False
        cNCI.Init(cLocalElement, cSystemElement)
        TabPage1.Controls.Clear()
        TabPage1.Controls.Add(cNCI.Panel_UI)

        cGFile = New GFile
        cGFile.ObjectSource = cGapFiller
        cGFile.FontSize = 10
        cGFile.ReadOnly = False
        cGFile.Init(cLocalElement, cSystemElement)
        TabPage2.Controls.Clear()
        TabPage2.Controls.Add(cGFile.Panel_UI)

        cParameter = New Parameter
        cParameter.ObjectSource = cGapFiller
        cParameter.FontSize = 10
        cParameter.ReadOnly = False
        cParameter.Init(cLocalElement, cSystemElement)
        TabPage3.Controls.Clear()
        TabPage3.Controls.Add(cParameter.Panel_UI)


        cBDTronic = cGapFiller.cBDTronic
        cBDTronic.ObjectSource = cGapFiller
        cBDTronic.FontSize = 10
        cBDTronic.ReadOnly = False
        cBDTronic.Init(cLocalElement, cSystemElement)
        TabPage4.Controls.Clear()
        TabPage4.Controls.Add(cBDTronic.Panel_UI)

        Dim cWeightUI As WeightUI = cGapFiller.lListWeightUI(0)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.A
        cWeightUI.Index = 1
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = False
        cWeightUI.ObjectSource = cGapFiller
        cWeightUI.Init(cLocalElement, cSystemElement)
        TableLayoutPanel_Body4.Controls.Clear()
        TableLayoutPanel_Body4.Controls.Add(cWeightUI.Panel_Body, 0, 1)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = cGapFiller.lListWeightUI(1)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.B
        cWeightUI.Index = 2
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = False
        cWeightUI.ObjectSource = cGapFiller
        cWeightUI.Init(cLocalElement, cSystemElement)
        TableLayoutPanel_Body5.Controls.Clear()
        TableLayoutPanel_Body5.Controls.Add(cWeightUI.Panel_Body, 0, 1)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = cGapFiller.lListWeightUI(2)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.AB
        cWeightUI.Index = 3
        cWeightUI.FontSize = 10
        cWeightUI.ReadOnly = False
        cWeightUI.ObjectSource = cGapFiller
        cWeightUI.Init(cLocalElement, cSystemElement)
        TableLayoutPanel_Body6.Controls.Clear()
        TableLayoutPanel_Body6.Controls.Add(cWeightUI.Panel_Body, 0, 1)
        lListWeightUI.Add(cWeightUI)

        TabPage1.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage2")
        TabPage3.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage3")
        TabPage4.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage4")
        TabPage5.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage5")
        TabPage6.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage6")
        TabPage7.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage7")
        Return True
    End Function



    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        If Not IsNothing(cNCI) Then cNCI.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        If Not IsNothing(cGFile) Then cGFile.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        If Not IsNothing(cParameter) Then cParameter.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        If Not IsNothing(cBDTronic) Then cBDTronic.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        For Each element As WeightUI In lListWeightUI
            element.SetParameter(cLocalElement, cSystemElement, lListInitParameter, lListControlParameter)
        Next
        Return True
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(ControlUI.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "9"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "10", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructGapFiller), New StructGapFiller)
                        iStep = iStep + 1

                    Case 4
                        If Not IsNothing(cNCI) Then cNCI.RefreshUI()
                        If Not IsNothing(cGFile) Then cGFile.RefreshUI()
                        If Not IsNothing(cParameter) Then cParameter.RefreshUI()
                        If Not IsNothing(cBDTronic) Then cBDTronic.RefreshUI()
                        For Each element As WeightUI In lListWeightUI
                            element.RefreshUI()
                        Next
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        cGapFiller.StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function


    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StartRefresh
        bExit = False
        If Not IsNothing(cNCI) Then cNCI.StartRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cGFile) Then cGFile.StartRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cParameter) Then cParameter.StartRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cBDTronic) Then cBDTronic.StartRefresh(cLocalElement, cSystemElement)
        For Each element As WeightUI In lListWeightUI
            element.StartReflesh()
        Next
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StopRefresh
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
        If Not IsNothing(cNCI) Then cNCI.StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cGFile) Then cGFile.StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cParameter) Then cParameter.StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cBDTronic) Then cBDTronic.StopRefresh(cLocalElement, cSystemElement)
        For Each element As WeightUI In lListWeightUI
            element.StopReflesh()
        Next
        ' If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then

        'If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        '  End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
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
            TempStructGapFiller.bulHMIRRead = False
            TempStructGapFiller.bulHMIRWrite = False
            TempStructGapFiller.fdHMICurrentGFilePath = ""
            TempStructGapFiller.bulHMIStartBlindShot = False
            TempStructGapFiller.bulPLCStartBlindShot = False
            TempStructGapFiller.bulHMIStartClean = False
            TempStructGapFiller.bulPLCStartClean = False

            TempStructGapFiller.bulPLCB2000Ready = False
            TempStructGapFiller.bulPLCB2000Busy = False
            TempStructGapFiller.bulPLCB2000System_OK = False
            TempStructGapFiller.bulPLCB2000ProcessCycle_OK = False
            TempStructGapFiller.bulPLCB2000ProcessCycle_NOK = False
            TempStructGapFiller.bulPLCB2000Handshake_active = False

            TempStructGapFiller.bulPLCB2000actOP_Mode = 0
            TempStructGapFiller.bulPLCB2000requestPostiton = 0
            TempStructGapFiller.bulPLCB2000actRecipe = 0
            TempStructGapFiller.bulPLCB2000actUserLevel = 0

            TempStructGapFiller.bulPLCB2000FillingLevel1 = 0
            TempStructGapFiller.bulPLCB2000FillingLevel2 = 0

            TempStructGapFiller.bulHMIB2000Start = False
            TempStructGapFiller.bulHMIB2000Filling = False
            TempStructGapFiller.bulHMIB2000Cleaning = False
            TempStructGapFiller.bulHMIB2000Material_OK = False
            ' TempStructGapFiller.bulHMIB2000Position = 0
            ' TempStructGapFiller.bulHMIB2000OP_Mode = 0
            ' TempStructGapFiller.bulHMIB2000RecipeNumber = 0

            TempStructGapFiller.bulPLCPPSHandShake_active = False
            TempStructGapFiller.bulPLCPPSactOP_Mode = 0
            TempStructGapFiller.bulPLCPPSactUser_Level = 0
            TempStructGapFiller.bulPLCPPSFillingLevelP1 = 0
            TempStructGapFiller.bulPLCPPSFillingLevelP2 = 0
            TempStructGapFiller.bulPLCPPSSupplyPressureP1 = 0
            TempStructGapFiller.bulPLCPPSSupplyPressureP2 = 0
            TempStructGapFiller.bulPLCPPSPressureP1Outlet = 0
            TempStructGapFiller.bulPLCPPSPressureP2Outlet = 0
            TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 0
            ' TempStructGapFiller.bulHMIPPSOP_Mode = 0
            TempStructGapFiller.bulHMIPPSHS_MESok = 0

            TempStructGapFiller.bulPLCPPSstrPartNoA = ""
            TempStructGapFiller.bulPLCPPSstrVolumeA = ""
            TempStructGapFiller.bulPLCPPSstrExpiryDateA = ""
            TempStructGapFiller.bulPLCPPSstrBatchNoA = ""
            TempStructGapFiller.bulPLCPPSstrSupplierNoA = ""
            TempStructGapFiller.bulPLCPPSstrPackagingNoA = ""
            TempStructGapFiller.bulPLCPPSstrPartNoB = ""
            TempStructGapFiller.bulPLCPPSstrVolumeB = ""
            TempStructGapFiller.bulPLCPPSstrExpiryDateB = ""
            TempStructGapFiller.bulPLCPPSstrBatchNoB = ""
            TempStructGapFiller.bulPLCPPSstrSupplierNoB = ""
            TempStructGapFiller.bulPLCPPSstrPackagingNoB = ""
            TempStructGapFiller.fdPLCNCErrorCode = 0
            TempStructGapFiller.bulHMIStartTimeFilling = False
            TempStructGapFiller.PLC_Weight(0).fdHMIStartWeight = False
            TempStructGapFiller.PLC_Weight(1).fdHMIStartWeight = False
            TempStructGapFiller.PLC_Weight(2).fdHMIStartWeight = False

            cHMIPLC.WriteAny(lListInitParameter(0), TempStructGapFiller)
            If Not IsNothing(cGapFiller.cBDTronic) Then
                cGapFiller.cBDTronic.HmiTextBox_PPSOP_Mode.TextBox.Text = TempStructGapFiller.bulHMIPPSOP_Mode
                cGapFiller.cBDTronic.HmiTextBox_B2000Position.TextBox.Text = TempStructGapFiller.bulHMIB2000Position
                cGapFiller.cBDTronic.HmiTextBox_B2000OP_Mode.TextBox.Text = TempStructGapFiller.bulHMIB2000OP_Mode
                cGapFiller.cBDTronic.HmiTextBox_B2000RecipeNumber.TextBox.Text = TempStructGapFiller.bulHMIB2000RecipeNumber
            End If
        End If
        Return True
    End Function
End Class

Public Class clsPointCfg
    Public X As Single
    Public Y As Single
    Public Z As Single
End Class

Public Class clsGFilePathCfg
    Public Name As String
    Public Path As String
End Class

Public Class clsMFunctionCfg
    Protected strText As String
    Protected bReserve As Boolean
    Protected iMFunction As MFunction
    Private _Object As New Object
    Protected iIndex As Integer

    Public Property Index As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iIndex
            End SyncLock
        End Get
    End Property

    Public Property Text As String
        Set(ByVal value As String)
            SyncLock _Object
                strText = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strText
            End SyncLock
        End Get
    End Property

    Public Property Reserve As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bReserve = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bReserve
            End SyncLock
        End Get
    End Property

    Public Property MFunction As MFunction
        Set(ByVal value As MFunction)
            SyncLock _Object
                iMFunction = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iMFunction
            End SyncLock
        End Get
    End Property


    Sub New()

    End Sub
    Sub New(ByVal strText As String, ByVal iMFunction As MFunction)
        Me.strText = strText
        Me.iMFunction = MFunction
    End Sub
End Class


