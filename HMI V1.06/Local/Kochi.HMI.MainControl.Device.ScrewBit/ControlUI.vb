Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class ControlUI
    Implements IControlUI

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
    Private cPKP As clsScrewBit
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructScrewBit As New StructScrewBit
    Private TempStructScrewBit As New StructScrewBit
    Private isCancel As Boolean = True
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Public Const FormName As String = "ScrewBitControlCutUI"


    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Get
            Return cPKP
        End Get
        Set(ByVal value As Object)
            cPKP = ObjectSource
        End Set
    End Property

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
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
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result1.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result1")
        HmiLabel_Result1.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result2.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result2")
        HmiLabel_Result2.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result3.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result3")
        HmiLabel_Result3.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result4.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result4")
        HmiLabel_Result4.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result5.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result5")
        HmiLabel_Result5.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result6.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result6")
        HmiLabel_Result6.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result7.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result7")
        HmiLabel_Result7.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Result8.Label.Text = cLanguageManager.GetUserTextLine("ScrewBit", "HmiLabel_Result8")
        HmiLabel_Result8.Label.Font = New System.Drawing.Font("Calibri", 12.0!)


        HmiTextBox_Pro.Text = OldStructScrewBit.fdHMIProg
        HmiTextBox_Pro.TextBoxReadOnly = True

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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewBit", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewBit", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewBit), New StructScrewBit)
                        iStep = iStep + 1

                    Case 4
                        TempStructScrewBit = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructScrewBit.fdHMIProg <> OldStructScrewBit.fdHMIProg Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.TextBox.Text = TempStructScrewBit.fdHMIProg.ToString
                                                   End Sub)
                        End If
                        If TempStructScrewBit.bulPLCResult1 <> OldStructScrewBit.bulPLCResult1 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result1.SetIndicateBackColor(TempStructScrewBit.bulPLCResult1)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult2 <> OldStructScrewBit.bulPLCResult2 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result2.SetIndicateBackColor(TempStructScrewBit.bulPLCResult2)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult3 <> OldStructScrewBit.bulPLCResult3 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result3.SetIndicateBackColor(TempStructScrewBit.bulPLCResult3)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult4 <> OldStructScrewBit.bulPLCResult4 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result4.SetIndicateBackColor(TempStructScrewBit.bulPLCResult4)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult5 <> OldStructScrewBit.bulPLCResult5 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result5.SetIndicateBackColor(TempStructScrewBit.bulPLCResult5)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult6 <> OldStructScrewBit.bulPLCResult6 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result6.SetIndicateBackColor(TempStructScrewBit.bulPLCResult6)
                                                   End Sub)
                        End If

                        If TempStructScrewBit.bulPLCResult7 <> OldStructScrewBit.bulPLCResult7 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result7.SetIndicateBackColor(TempStructScrewBit.bulPLCResult7)
                                                   End Sub)
                        End If


                        If TempStructScrewBit.bulPLCResult8 <> OldStructScrewBit.bulPLCResult8 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Result8.SetIndicateBackColor(TempStructScrewBit.bulPLCResult8)
                                                   End Sub)
                        End If
                        OldStructScrewBit.fdHMIProg = TempStructScrewBit.fdHMIProg
                        OldStructScrewBit.bulPLCResult1 = TempStructScrewBit.bulPLCResult1
                        OldStructScrewBit.bulPLCResult2 = TempStructScrewBit.bulPLCResult2
                        OldStructScrewBit.bulPLCResult3 = TempStructScrewBit.bulPLCResult3
                        OldStructScrewBit.bulPLCResult4 = TempStructScrewBit.bulPLCResult4
                        OldStructScrewBit.bulPLCResult5 = TempStructScrewBit.bulPLCResult5
                        OldStructScrewBit.bulPLCResult6 = TempStructScrewBit.bulPLCResult6
                        OldStructScrewBit.bulPLCResult7 = TempStructScrewBit.bulPLCResult7
                        OldStructScrewBit.bulPLCResult8 = TempStructScrewBit.bulPLCResult8
                        iStep = 4
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
               Me.Dispose()
        Return True
    End Function


    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        Return True
    End Function

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StartRefresh
        bExit = False
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempStructScrewBit As New StructScrewBit
        If lListInitParameter.Count >= 1 Then cHMIPLC.WriteAny(lListInitParameter(0), TempStructScrewBit)

        Return True
    End Function
End Class
