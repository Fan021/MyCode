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
    Protected cLanguageManager As clsLanguageManager
    Private mMainForm As IMainUI
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cScanner As clsScanner
    Private bTrigOn As Boolean = False
    Private bTrigOff As Boolean = False
    Private bTrig As Boolean = False
    Private strOldMessage As String = String.Empty
    Public Const FormName As String = "KeyenceScannerShortcutUI"

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Set(ByVal value As Object)
            cScanner = value
        End Set
        Get
            Return cScanner
        End Get
    End Property

    Public ReadOnly Property UI As Panel Implements IShortcutUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_On.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "HmiButton_On")
        HmiButton_Off.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "HmiButton_Off")
        HmiButton_Off.Button.Enabled = False
        TextBox_Result.Text = ""
        AddHandler HmiButton_On.Button.Click, AddressOf MainButton_Click
        AddHandler HmiButton_Off.Button.Click, AddressOf MainButton_Click
        Return True
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)

    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_On"
                    bTrigOn = True
                Case "HmiButton_Off"
                    bTrigOff = True
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If bTrig Then
            cScanner.StopReceive()
            cScanner.TrigOff()
            cScanner.Close()
            cScanner.Running = False
            bTrig = False
        End If
        Me.Dispose()

        Return True
    End Function

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
                        If bTrigOn Then
                            iStep = 10
                            Continue While
                        End If
                        If bTrigOff Then
                            iStep = 20
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 2
                        If strOldMessage <> cScanner.ScanResult And cScanner.ScanResult <> "" Then
                            mMainForm.InvokeAction(Sub()
                                                       TextBox_Result.Text = cScanner.ScanResult
                                                   End Sub)
                            strOldMessage = cScanner.ScanResult
                            bTrigOff = True
                        End If
                        iStep = 1

                    Case 10
                        strOldMessage = ""
                        mMainForm.InvokeAction(Sub()
                                                   TextBox_Result.Text = ""
                                                   HmiButton_On.Button.Enabled = False
                                               End Sub)
                        iStep = iStep + 1

                    Case 11
                        If Not cScanner.Running Then
                            bTrig = True
                            cScanner.Running = True
                            iStep = iStep + 1
                        End If

                    Case 12
                        cScanner.Connect()
                        iStep = iStep + 1

                    Case 13
                        cScanner.TrigOn()
                        iStep = iStep + 1

                    Case 14
                        cScanner.StartReceive()
                        iStep = iStep + 1

                    Case 15
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Off.Button.Enabled = True
                                               End Sub)
                        bTrigOn = False
                        iStep = 1

                    Case 20
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Off.Button.Enabled = False
                                               End Sub)
                        iStep = iStep + 1

                    Case 21
                        cScanner.StopReceive()
                        iStep = iStep + 1

                    Case 22
                        cScanner.TrigOff()
                        iStep = iStep + 1

                    Case 23

                        cScanner.Close()
                        iStep = iStep + 1

                    Case 24
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_On.Button.Enabled = True
                                               End Sub)
                        cScanner.Running = False
                        bTrigOff = False
                        bTrig = False
                        iStep = 1

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While
    End Sub

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
        If bTrigOn Then
            cScanner.StopReceive()
            cScanner.TrigOff()
            cScanner.Close()
        End If
        If Not IsNothing(cThread) Then cThread.Abort()
        Return True
    End Function
End Class