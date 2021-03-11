Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

Public Class IOForm
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Public Const FormName As String = "IOForm"
    Public Const GWL_STYLE As Integer = -16
    Public Const WS_DISABLED As Integer = &H8000000
    Public TempStructAST As New StructAST
    Public OldStructAST As New StructAST
    Protected cLanguageManager As clsLanguageManager
    Protected cAST As clsAST11
    <DllImport("user32.dll ")>
    Protected Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal wndproc As Integer) As Integer

    End Function
    <DllImport("user32.dll ")>
    Protected Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer

    End Function

    Public Property AST As clsAST11
        Set(ByVal value As clsAST11)
            cAST = value
        End Set
        Get
            Return cAST
        End Get
    End Property

    Public Sub SetControlEnabled(ByVal c As Control, ByVal enabled As Boolean)
        SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE))
    End Sub


    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        InputIO1.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Start"), "Start")
        InputIO2.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Stop"), "Stop")
        '   SetControlEnabled(InputIO2, False)
        '  InputIO2.MainButton.ForeColor = Drawing.Color.Black
        InputIO3.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "AutoStop"), "AutoStop")
        InputIO4.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Reload"), "Reload")
        InputIO5.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Enable Start"), "Enable Start")
        InputIO6.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Pro.Input"), "Pro.Input")
        InputIO7.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Data Saved"), "Data Saved")

        OutputIO1.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "System OK"), "System OK")
        OutputIO2.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "AST11 Ready"), "AST11 Ready")
        OutputIO3.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "OK"), "OK")
        OutputIO4.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "NOK"), "NOK")
        OutputIO5.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Pro.Output"), "Pro.Output")
        OutputIO6.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "AST11 Alive"), "AST11 Alive")
        OutputIO7.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Data Available"), "Data Available")
        SetControlEnabled(OutputIO1, False)
        OutputIO1.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO2, False)
        OutputIO2.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO3, False)
        OutputIO3.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO4, False)
        OutputIO4.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO5, False)
        OutputIO5.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO6, False)
        OutputIO6.MainButton.ForeColor = Drawing.Color.Black
        SetControlEnabled(OutputIO7, False)
        OutputIO7.MainButton.ForeColor = Drawing.Color.Black
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBox.TextAlign = HorizontalAlignment.Center

        HmiLabel_SystemState.Label.Text = cLanguageManager.GetUserTextLine("AST11", "System State")
        HmiLabel_SystemState.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_ProgramNo.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Program No")
        HmiLabel_ProgramNo.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_State.Label.Text = cLanguageManager.GetUserTextLine("AST11", "State")
        HmiLabel_State.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_CycleTime.Label.Text = cLanguageManager.GetUserTextLine("AST11", "CycleTime")
        HmiLabel_CycleTime.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_Step.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Step")
        HmiLabel_Step.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_Torque.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Torque")
        HmiLabel_Torque.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_Angle.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Angle")
        HmiLabel_Angle.Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        HmiLabel_Step1.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Step1")
        HmiLabel_Step2.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Step2")
        HmiLabel_Step3.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Step3")
        HmiLabel_Step4.Label.Text = cLanguageManager.GetUserTextLine("AST11", "Step NOK")

        InputIO1.SetIndicateBackColor(OldStructAST.Start)
        InputIO2.SetIndicateBackColor(OldStructAST.Stop)
        InputIO3.SetIndicateBackColor(OldStructAST.AutoStop)
        InputIO4.SetIndicateBackColor(OldStructAST.Reload)
        InputIO5.SetIndicateBackColor(OldStructAST.StartRelease)
        InputIO6.SetIndicateBackColor(OldStructAST.ProgIn2)
        InputIO7.SetIndicateBackColor(OldStructAST.DataSaved)

        OutputIO1.SetIndicateBackColor(OldStructAST.SystemOK)
        OutputIO2.SetIndicateBackColor(OldStructAST.Ready)
        OutputIO3.SetIndicateBackColor(OldStructAST.OK)
        OutputIO4.SetIndicateBackColor(OldStructAST.NOK)
        OutputIO5.SetIndicateBackColor(OldStructAST.ProgOut8)
        OutputIO6.SetIndicateBackColor(OldStructAST.Heartbeat)
        OutputIO7.SetIndicateBackColor(OldStructAST.DataAvailable)
        HmiTextBox_Pro.TextBox.Text = OldStructAST.ProgramNo.ToString
        Label_SystemState.Text = OldStructAST.SysStatus.ToString
        Label_ProgramNo.Text = OldStructAST.fdProg.ToString
        Label_State.Text = OldStructAST.fdStatus.ToString
        Label_CycleTime.Text = OldStructAST.fdTime.ToString + " sec"
        Label_Step1_Step.Text = OldStructAST.fdStep1.ToString
        Label_Step1_Torque.Text = Round(OldStructAST.fdTorque1, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(OldStructAST.fdTorqueUnit)
        Label_Step1_Angle.Text = OldStructAST.fdAngle1.ToString + " °"
        Label_Step2_Step.Text = OldStructAST.fdStep2.ToString
        Label_Step2_Torque.Text = Round(OldStructAST.fdTorque2, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(OldStructAST.fdTorqueUnit)
        Label_Step2_Angle.Text = OldStructAST.fdAngle2.ToString + " °"
        Label_Step3_Step.Text = OldStructAST.fdStep3.ToString
        Label_Step3_Torque.Text = Round(OldStructAST.fdTorque3, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(OldStructAST.fdTorqueUnit)
        Label_Step3_Angle.Text = OldStructAST.fdAngle3.ToString + " °"
        Label_Step4_Step.Text = OldStructAST.fdStepNOk.ToString
        Label_Step4_Torque.Text = Round(OldStructAST.fdTorqueNOk, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(OldStructAST.fdTorqueUnit)
        Label_Step4_Angle.Text = OldStructAST.fdAngleNOk.ToString + " °"

        '   AddHandler InputIO1.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO2.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO3.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO4.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO5.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO6.MainButton.MouseUp, AddressOf Button_MouseUp
        AddHandler InputIO7.MainButton.MouseUp, AddressOf Button_MouseUp

        '    AddHandler InputIO1.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO2.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO3.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO4.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO5.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO6.MainButton.MouseDown, AddressOf Button_MouseDown
        AddHandler InputIO7.MainButton.MouseDown, AddressOf Button_MouseDown

        AddHandler InputIO1.MainButton.Click, AddressOf Button_Click
        AddHandler InputIO3.MainButton.Click, AddressOf Button_Click
        AddHandler InputIO5.MainButton.Click, AddressOf Button_Click
        AddHandler Button_Down.Click, AddressOf Button_Click
        AddHandler Button_Up.Click, AddressOf Button_Click
        Return True
    End Function

    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter

        Return True
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try

                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(IOForm.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException("PLC is Nothing, Please Add first", enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException("Device:" + cHMIPLC.Name + " Status:" + cHMIPLC.DeviceState.ToString, enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        cHMIPLC.AddNotificationEx(lListInitParameter(1), GetType(StructAST), New StructAST)
                        iStep = iStep + 1
                    Case 3
                        Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))

                        If TempStructAST.Start <> OldStructAST.Start Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO1.SetIndicateBackColor(TempStructAST.Start)
                                                   End Sub)
                        End If
                        If TempStructAST.Stop <> OldStructAST.Stop Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO2.SetIndicateBackColor(TempStructAST.Stop)
                                                   End Sub)
                        End If
                        If TempStructAST.AutoStop <> OldStructAST.AutoStop Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO3.SetIndicateBackColor(TempStructAST.AutoStop)
                                                   End Sub)
                        End If
                        If TempStructAST.Reload <> OldStructAST.Reload Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO4.SetIndicateBackColor(TempStructAST.Reload)
                                                   End Sub)
                        End If
                        If TempStructAST.StartRelease <> OldStructAST.StartRelease Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO5.SetIndicateBackColor(TempStructAST.StartRelease)
                                                   End Sub)
                        End If

                        If TempStructAST.ProgIn2 <> OldStructAST.ProgIn2 Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO6.SetIndicateBackColor(TempStructAST.ProgIn2)
                                                   End Sub)
                        End If

                        If TempStructAST.DataSaved <> OldStructAST.DataSaved Then
                            mMainForm.InvokeAction(Sub()
                                                       InputIO7.SetIndicateBackColor(TempStructAST.DataSaved)
                                                   End Sub)
                        End If

                        If TempStructAST.SystemOK <> OldStructAST.SystemOK Then
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO1.SetIndicateBackColor(TempStructAST.SystemOK)
                                                   End Sub)
                        End If

                        If TempStructAST.Ready <> OldStructAST.Ready Then
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO2.SetIndicateBackColor(TempStructAST.Ready)
                                                   End Sub)
                        End If

                        If TempStructAST.OK <> OldStructAST.OK Then
                            If TempStructAST.OK Then cHMIPLC.WriteAny(lListInitParameter(1) + ".Start", False)
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO3.SetIndicateBackColor(TempStructAST.OK)
                                                   End Sub)
                        End If


                        If TempStructAST.NOK <> OldStructAST.NOK Then
                            If TempStructAST.NOK Then cHMIPLC.WriteAny(lListInitParameter(1) + ".Start", False)
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO4.SetIndicateBackColor(TempStructAST.NOK)
                                                   End Sub)
                        End If

                        If TempStructAST.ProgOut8 <> OldStructAST.ProgOut8 Then
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO5.SetIndicateBackColor(TempStructAST.ProgOut8)
                                                   End Sub)
                        End If

                        If TempStructAST.Heartbeat <> OldStructAST.Heartbeat Then
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO6.SetIndicateBackColor(TempStructAST.Heartbeat)
                                                   End Sub)
                        End If

                        If TempStructAST.DataAvailable <> OldStructAST.DataAvailable Then
                            mMainForm.InvokeAction(Sub()
                                                       OutputIO7.SetIndicateBackColor(TempStructAST.DataAvailable)
                                                   End Sub)
                        End If


                        If TempStructAST.ProgramNo <> OldStructAST.ProgramNo Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.TextBox.Text = TempStructAST.ProgramNo.ToString
                                                   End Sub)
                        End If
                        If TempStructAST.SysStatus <> OldStructAST.SysStatus Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_SystemState.Text = TempStructAST.SysStatus.ToString
                                                   End Sub)
                        End If
                        If TempStructAST.fdProg <> OldStructAST.fdProg Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_ProgramNo.Text = TempStructAST.fdProg.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdStatus <> TempStructAST.fdStatus Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_State.Text = TempStructAST.fdStatus.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdTime <> TempStructAST.fdTime Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_CycleTime.Text = TempStructAST.fdTime.ToString + " sec"
                                                   End Sub)
                        End If
                        If OldStructAST.fdStep1 <> TempStructAST.fdStep1 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step1_Step.Text = TempStructAST.fdStep1.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdTorque1 <> TempStructAST.fdTorque1 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step1_Torque.Text = Round(TempStructAST.fdTorque1, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                   End Sub)
                        End If
                        If OldStructAST.fdAngle1 <> TempStructAST.fdAngle1 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step1_Angle.Text = Round(TempStructAST.fdAngle1, 2).ToString + " °"
                                                   End Sub)
                        End If

                        If OldStructAST.fdStep2 <> TempStructAST.fdStep2 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step2_Step.Text = TempStructAST.fdStep2.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdTorque2 <> TempStructAST.fdTorque2 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step2_Torque.Text = Round(TempStructAST.fdTorque2, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                   End Sub)
                        End If
                        If OldStructAST.fdAngle2 <> TempStructAST.fdAngle2 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step2_Angle.Text = Round(TempStructAST.fdAngle2, 2).ToString("0.00") + " °"
                                                   End Sub)
                        End If

                        If OldStructAST.fdStep3 <> TempStructAST.fdStep3 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step3_Step.Text = TempStructAST.fdStep3.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdTorque3 <> TempStructAST.fdTorque3 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step3_Torque.Text = Round(TempStructAST.fdTorque3, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                   End Sub)
                        End If
                        If OldStructAST.fdAngle3 <> TempStructAST.fdAngle3 Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step3_Angle.Text = Round(TempStructAST.fdAngle3, 2).ToString + " °"
                                                   End Sub)
                        End If

                        If OldStructAST.fdStepNOk <> TempStructAST.fdStepNOk Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step4_Step.Text = TempStructAST.fdStepNOk.ToString
                                                   End Sub)
                        End If
                        If OldStructAST.fdTorqueNOk <> TempStructAST.fdTorqueNOk Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step4_Torque.Text = Round(TempStructAST.fdTorqueNOk, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                   End Sub)
                        End If
                        If OldStructAST.fdAngleNOk <> TempStructAST.fdAngleNOk Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step4_Angle.Text = Round(TempStructAST.fdAngleNOk, 2).ToString + " °"
                                                   End Sub)
                        End If

                        If OldStructAST.fdTorqueUnit <> TempStructAST.fdTorqueUnit Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Step1_Torque.Text = Round(TempStructAST.fdTorque1, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                       Label_Step2_Torque.Text = Round(TempStructAST.fdTorque2, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                       Label_Step3_Torque.Text = Round(TempStructAST.fdTorque3, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                       Label_Step4_Torque.Text = Round(TempStructAST.fdTorqueNOk, 2).ToString("0.00") + " " + cAST.ChangeTorqueUnit(TempStructAST.fdTorqueUnit)
                                                   End Sub)
                        End If

                        OldStructAST.Start = TempStructAST.Start
                        OldStructAST.AutoStop = TempStructAST.AutoStop
                        OldStructAST.Stop = TempStructAST.Stop
                        OldStructAST.Reload = TempStructAST.Reload
                        OldStructAST.StartRelease = TempStructAST.StartRelease
                        OldStructAST.ProgIn2 = TempStructAST.ProgIn2
                        OldStructAST.DataSaved = TempStructAST.DataSaved
                        OldStructAST.SystemOK = TempStructAST.SystemOK
                        OldStructAST.Ready = TempStructAST.Ready
                        OldStructAST.OK = TempStructAST.OK
                        OldStructAST.NOK = TempStructAST.NOK
                        OldStructAST.ProgOut8 = TempStructAST.ProgOut8
                        OldStructAST.Heartbeat = TempStructAST.Heartbeat
                        OldStructAST.DataAvailable = TempStructAST.DataAvailable
                        OldStructAST.ProgramNo = TempStructAST.ProgramNo
                        OldStructAST.SysStatus = TempStructAST.SysStatus
                        OldStructAST.fdProg = TempStructAST.fdProg
                        OldStructAST.fdStatus = TempStructAST.fdStatus
                        OldStructAST.fdTime = TempStructAST.fdTime
                        OldStructAST.fdStep1 = TempStructAST.fdStep1
                        OldStructAST.fdTorque1 = TempStructAST.fdTorque1
                        OldStructAST.fdAngle1 = TempStructAST.fdAngle1

                        OldStructAST.fdStep2 = TempStructAST.fdStep2
                        OldStructAST.fdTorque2 = TempStructAST.fdTorque2
                        OldStructAST.fdAngle2 = TempStructAST.fdAngle2

                        OldStructAST.fdStep3 = TempStructAST.fdStep3
                        OldStructAST.fdTorque3 = TempStructAST.fdTorque3
                        OldStructAST.fdAngle3 = TempStructAST.fdAngle3

                        OldStructAST.fdStepNOk = TempStructAST.fdStepNOk
                        OldStructAST.fdTorqueNOk = TempStructAST.fdTorqueNOk
                        OldStructAST.fdAngleNOk = TempStructAST.fdAngleNOk
                        OldStructAST.fdTorqueUnit = TempStructAST.fdTorqueUnit
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, IOForm.FormName))
            End Try


        End While
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "Button_Down"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dOldValue As Int16 = TempStructAST.ProgramNo
                Dim dNewValue As Int16 = dOldValue - 1
                If dNewValue < 1 Then dNewValue = 16
                cHMIPLC.WriteAny(lListInitParameter(1) + ".ProgramNo", dNewValue)
            Case "Button_Up"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dOldValue As Int16 = TempStructAST.ProgramNo
                Dim dNewValue As Int16 = dOldValue + 1
                If dNewValue > 16 Then dNewValue = 1
                cHMIPLC.WriteAny(lListInitParameter(1) + ".ProgramNo", dNewValue)
            Case "Enable Start"
                Dim TempStructAST As StructAST = cHMIPLC.ReadAny(lListInitParameter(1), GetType(StructAST))
                Dim dOldValue As Boolean = TempStructAST.StartRelease
                Dim dNewValue As Boolean = Not dOldValue
                cHMIPLC.WriteAny(lListInitParameter(1) + ".StartRelease", dNewValue)
            Case "AutoStop"
                Dim TempStructAST As StructAST = cHMIPLC.ReadAny(lListInitParameter(1), GetType(StructAST))
                Dim dOldValue As Boolean = TempStructAST.AutoStop
                Dim dNewValue As Boolean = Not dOldValue
                cHMIPLC.WriteAny(lListInitParameter(1) + ".AutoStop", dNewValue)
            Case "Start"
                Dim TempStructAST As StructAST = cHMIPLC.ReadAny(lListInitParameter(1), GetType(StructAST))
                Dim dOldValue As Boolean = TempStructAST.Start
                Dim dNewValue As Boolean = Not dOldValue
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Start", dNewValue)
        End Select

    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "Start"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Start", dNewValue)
            Case "Stop"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Stop", dNewValue)
                'Case "AutoStop"
                '    Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                '    Dim dNewValue As Boolean = False
                '    cHMIPLC.WriteAny(lListInitParameter(1) + ".AutoStop", dNewValue)
            Case "Reload"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Reload", dNewValue)
                'Case "Enable Start"
                '    Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                '    Dim dNewValue As Boolean = False
                'cHMIPLC.WriteAny(lListInitParameter(1) + ".StartRelease", dNewValue)
            Case "Pro.Input"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(1) + ".ProgIn2", dNewValue)
            Case "Data Saved"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(1) + ".DataSaved", dNewValue)
        End Select
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "Start"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Start", dNewValue)
            Case "Stop"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Stop", dNewValue)
            Case "Reload"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(1) + ".Reload", dNewValue)
                'Case "AutoStop"
                '    Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                '    Dim dNewValue As Boolean = True
                '    cHMIPLC.WriteAny(lListInitParameter(1) + ".AutoStop", dNewValue)
                'Case "Enable Start"
                '    Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                '    Dim dNewValue As Boolean = True
                ' cHMIPLC.WriteAny(lListInitParameter(1) + ".StartRelease", dNewValue)
            Case "Pro.Input"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(1) + ".ProgIn2", dNewValue)
            Case "Data Saved"
                Dim TempStructAST As StructAST = cHMIPLC.GetValue(lListInitParameter(1))
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(1) + ".DataSaved", dNewValue)

        End Select
    End Sub

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count >= 1 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(1))
        End If
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

End Class