Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
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
    Private cVariantManager As clsVariantManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Private mMainForm As IMainUI
    Private cInSpection As clsInSpection
    Private OldStructInSpection As New StructInSpection
    Private TempStructInSpection As New StructInSpection
    Public Const FormName As String = "InSpectionControlUI"

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Set(ByVal value As Object)
            cInSpection = value
        End Set
        Get
            Return cInSpection
        End Get
    End Property
    Public ReadOnly Property UI As Panel Implements IShortcutUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
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

        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Result.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Result")
        HmiLabel_Result.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Pro.TextBoxReadOnly = True
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructInSpection), New StructInSpection)
                        iStep = iStep + 1

                    Case 4
                        TempStructInSpection = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructInSpection.fdHMIProg <> OldStructInSpection.fdHMIProg Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.TextBox.Text = TempStructInSpection.fdHMIProg
                                                   End Sub)
                        End If
                        OldStructInSpection.fdHMIProg = TempStructInSpection.fdHMIProg
                        iStep = iStep + 1
                    Case 5
                        TempStructInSpection = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructInSpection.bulPLCResult <> OldStructInSpection.bulPLCResult Then
                            mMainForm.InvokeAction(Sub()
                                                       Panel_Indicate_Result.SetIndicateBackColor(TempStructInSpection.bulPLCResult)
                                                   End Sub)
                        End If
                        OldStructInSpection.bulPLCResult = TempStructInSpection.bulPLCResult
                        iStep = 4

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function



    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.StartRefresh
        bExit = False
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function

End Class
