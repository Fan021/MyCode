Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Windows.Forms

Public Class ShortCutUI
    Implements IShortcutUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cVariantManager As clsVariantManager
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Protected cChangePage As clsChangePage
    Private mMainForm As IMainUI
    Private cIAI As clsIAI
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructIAI As New StructIAI
    Private TempStructIAI As New StructIAI
    Private isCancel As Boolean = True

    Public Const FormName As String = "IAIShortCutUI"


    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Get
            Return cIAI
        End Get
        Set(ByVal value As Object)
            cIAI = ObjectSource
        End Set
    End Property

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
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
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiPassFailButton1.Text = cLanguageManager.GetUserTextLine("IAI", "HmiPassFailButton1")
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiPassFailButton3.Text = cLanguageManager.GetUserTextLine("IAI", "HmiPassFailButton3")
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_STPEnable.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_STPEnable")
        HmiButton_STPEnable.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Ready.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Ready")
        HmiLabel_Ready.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Alarm.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Alarm")
        HmiLabel_Alarm.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiButton_MotorEnable.Enabled = False
        HmiButton_STPEnable.Enabled = False
        HmiPassFailButton1.Enabled = False
        HmiPassFailButton3.Enabled = False
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "8"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "9", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructIAI), New StructIAI)
                        iStep = iStep + 1

                    Case 4
                        TempStructIAI = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructIAI))
                        iStep = iStep + 1

                    Case 5

                        TempStructIAI = cHMIPLC.GetValue(lListInitParameter(0))


                        If TempStructIAI.bulPLCIAIReady <> OldStructIAI.bulPLCIAIReady Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Ready.SetIndicateBackColor(TempStructIAI.bulPLCIAIReady)
                                                   End Sub)
                        End If
                        If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Alarm.SetIndicateErrorBackColor(TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                        End If
                        If TempStructIAI.bulHMIMotorEnable <> OldStructIAI.bulHMIMotorEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_MotorEnable.SetIndicateBackColor(TempStructIAI.bulHMIMotorEnable)
                                                   End Sub)
                        End If


                        If TempStructIAI.bytHMIPosition <> OldStructIAI.bytHMIPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.TextBox.Text = TempStructIAI.bytHMIPosition.ToString
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMISTP <> OldStructIAI.bulHMISTP Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_STPEnable.SetIndicateBackColor(TempStructIAI.bulHMISTP)
                                                   End Sub)

                        End If


                        'X Home
                        If TempStructIAI.bulHMIAxisXHome.bulHMIDoAction <> OldStructIAI.bulHMIAxisXHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructIAI.bulHMIAxisXHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass <> OldStructIAI.bulHMIAxisXHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass, TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail)
                                                   End Sub)
                            'If TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass Then
                            '    Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructIAIButton))
                            '    Dim dNewValue As New StructIAIButton
                            '    dNewValue.bulHMIDoAction = False
                            '    dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                            '    dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                            '    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
                            'End If
                        End If



                        'X Reset
                        If TempStructIAI.bulHMIAxisXReset.bulHMIDoAction <> OldStructIAI.bulHMIAxisXReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructIAI.bulHMIAxisXReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass, TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                            'If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass Then
                            '    Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructIAIButton))
                            '    Dim dNewValue As New StructIAIButton
                            '    dNewValue.bulHMIDoAction = False
                            '    dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                            '    dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                            '    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
                            'End If
                        End If
                        OldStructIAI.bulPLCIAIReady = TempStructIAI.bulPLCIAIReady
                        OldStructIAI.bytHMIPosition = TempStructIAI.bytHMIPosition
                        OldStructIAI.bulHMIMotorEnable = TempStructIAI.bulHMIMotorEnable
                        OldStructIAI.bulHMISTP = TempStructIAI.bulHMISTP

                        OldStructIAI.bulHMIAxisXHome.bulHMIDoAction = TempStructIAI.bulHMIAxisXHome.bulHMIDoAction
                        OldStructIAI.bulHMIAxisXHome.bulPlcActionIsPass = TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass
                        OldStructIAI.bulHMIAxisXHome.bulPlcActionIsFail = TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail

                        OldStructIAI.bulHMIAxisXReset.bulHMIDoAction = TempStructIAI.bulHMIAxisXReset.bulHMIDoAction
                        OldStructIAI.bulHMIAxisXReset.bulPlcActionIsPass = TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass
                        OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail = TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail


                        OldStructIAI.bulHMIMove.bulHMIDoAction = TempStructIAI.bulHMIMove.bulHMIDoAction
                        OldStructIAI.bulHMIMove.bulPlcActionIsPass = TempStructIAI.bulHMIMove.bulPlcActionIsPass
                        OldStructIAI.bulHMIMove.bulPlcActionIsFail = TempStructIAI.bulHMIMove.bulPlcActionIsFail
                        iStep = 5
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function


    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IShortcutUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
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