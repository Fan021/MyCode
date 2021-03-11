Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent

Public Class ShortCutUI
    Implements IShortcutUI
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
    Public Const FormName As String = "GapFillerControlUI"
    Private cBDTronic As BDTronic

    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
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
        cNCI.FontSize = 8
        cNCI.ReadOnly = True
        cNCI.Init(cLocalElement, cSystemElement)
        TabPage1.Controls.Clear()
        TabPage1.Controls.Add(cNCI.Panel_UI)

        cGFile = New GFile
        cGFile.ObjectSource = cGapFiller
        cGFile.FontSize = 8
        cGFile.ReadOnly = True
        cGFile.Init(cLocalElement, cSystemElement)
        TabPage2.Controls.Clear()
        TabPage2.Controls.Add(cGFile.Panel_UI)

        cParameter = New Parameter
        cParameter.ObjectSource = cGapFiller
        cParameter.FontSize = 8
        cParameter.ReadOnly = True
        cParameter.Init(cLocalElement, cSystemElement)
        TabPage3.Controls.Clear()
        TabPage3.Controls.Add(cParameter.Panel_UI)

        cBDTronic = cGapFiller.cBDTronic
        cBDTronic.ObjectSource = cGapFiller
        cBDTronic.FontSize = 8
        cBDTronic.ReadOnly = True
        cBDTronic.Init(cLocalElement, cSystemElement)
        TabPage4.Controls.Clear()
        TabPage4.Controls.Add(cBDTronic.Panel_UI)

        Dim cWeightUI As WeightUI = cGapFiller.lListWeightUI(0)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.A
        cWeightUI.Index = 1
        cWeightUI.FontSize = 8
        cWeightUI.ReadOnly = True
        cWeightUI.ObjectSource = cGapFiller
        cWeightUI.Init(cLocalElement, cSystemElement)
        TableLayoutPanel_Body4.Controls.Clear()
        TableLayoutPanel_Body4.Controls.Add(cWeightUI.Panel_Body, 0, 1)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = cGapFiller.lListWeightUI(1)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.B
        cWeightUI.Index = 2
        cWeightUI.FontSize = 8
        cWeightUI.ReadOnly = True
        cWeightUI.ObjectSource = cGapFiller
        cWeightUI.Init(cLocalElement, cSystemElement)
        TableLayoutPanel_Body5.Controls.Clear()
        TableLayoutPanel_Body5.Controls.Add(cWeightUI.Panel_Body, 0, 1)
        lListWeightUI.Add(cWeightUI)

        cWeightUI = cGapFiller.lListWeightUI(2)
        cWeightUI.TopLevel = False
        cWeightUI.PageType = enumPageType.AB
        cWeightUI.Index = 3
        cWeightUI.FontSize = 8
        cWeightUI.ReadOnly = True
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

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IShortcutUI.SetParameter
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
        Dim TempStructGapFiller As New StructGapFiller
        '  If lListInitParameter.Count > 1 Then cHMIPLC.WriteAny(lListInitParameter(0), TempStructGapFiller)
        Me.Dispose()
        cGapFiller.StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function


    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.StartRefresh
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

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.StopRefresh
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
        ' If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        ' End If
        Return True
    End Function
End Class

