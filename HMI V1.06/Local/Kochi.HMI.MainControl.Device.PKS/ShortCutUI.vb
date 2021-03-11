Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Windows.Forms

Public Class ShortCutUI
    Implements IShortCutUI
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
    Private cPKS As clsPKS
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructPKS As New StructPKS
    Private TempStructPKS As New StructPKS
    Private isCancal As Boolean = True

    Public Const FormName As String = "PKSShortCutUI"

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Get
            Return cPKS
        End Get
        Set(ByVal value As Object)
            cPKS = ObjectSource
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
            cActionManager.Init(cLocalElement, cSystemElement)
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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_R.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_R")
        HmiLabel_R.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveR.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_MoveR")
        HmiLabel_MoveR.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveZ.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_MoveZ")
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceR.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_ToleranceR")
        HmiLabel_ToleranceR.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceZ.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_ToleranceZ")
        HmiLabel_ToleranceZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)


        HmiLabel_SensorX.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_SensorX")
        HmiLabel_SensorX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorR.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_SensorR")
        HmiLabel_SensorR.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorZ.Label.Text = cLanguageManager.GetUserTextLine("PKS", "HmiLabel_SensorZ")
        HmiLabel_SensorZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        Label_X.Text = OldStructPKS.fdPLCXPosition.ToString("0.00")
        Label_R.Text = OldStructPKS.fdPLCRPosition.ToString("0.00")
        Label_Z.Text = OldStructPKS.fdPLCZPosition.ToString("0.00")
        HmiTextBox_MoveX.Text = OldStructPKS.fdHMIMoveXPosition.ToString("0.00")
        HmiTextBox_MoveR.Text = OldStructPKS.fdHMIMoveRPosition.ToString("0.00")
        HmiTextBox_MoveZ.Text = OldStructPKS.fdHMIMoveZPosition.ToString("0.00")
        HmiTextBox_ToleranceX.Text = OldStructPKS.fdHMIMoveXTolerance.ToString("0.00")
        HmiTextBox_ToleranceR.Text = OldStructPKS.fdHMIMoveRTolerance.ToString("0.00")
        HmiTextBox_ToleranceZ.Text = OldStructPKS.fdHMIMoveZTolerance.ToString("0.00")

        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveR.TextBoxReadOnly = True
        HmiTextBox_MoveZ.TextBoxReadOnly = True
        HmiTextBox_ToleranceX.TextBoxReadOnly = True
        HmiTextBox_ToleranceR.TextBoxReadOnly = True
        HmiTextBox_ToleranceZ.TextBoxReadOnly = True

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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKS", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKS", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructPKS), New StructPKS)
                        iStep = iStep + 1

                    Case 4
                        TempStructPKS = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructPKS.fdPLCXPosition <> OldStructPKS.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructPKS.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdPLCRPosition <> OldStructPKS.fdPLCRPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_R.Text = TempStructPKS.fdPLCRPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdPLCZPosition <> OldStructPKS.fdPLCZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Z.Text = TempStructPKS.fdPLCZPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveXPosition <> OldStructPKS.fdHMIMoveXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveX.Text = TempStructPKS.fdHMIMoveXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveRPosition <> OldStructPKS.fdHMIMoveRPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveR.Text = TempStructPKS.fdHMIMoveRPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveZPosition <> OldStructPKS.fdHMIMoveZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveZ.Text = TempStructPKS.fdHMIMoveZPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveXTolerance <> OldStructPKS.fdHMIMoveXTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceX.Text = TempStructPKS.fdHMIMoveXTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveRTolerance <> OldStructPKS.fdHMIMoveRTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceR.Text = TempStructPKS.fdHMIMoveRTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKS.fdHMIMoveZTolerance <> OldStructPKS.fdHMIMoveZTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceZ.Text = TempStructPKS.fdHMIMoveZTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                      
                        If TempStructPKS.fdPLCXOriginDone <> OldStructPKS.fdPLCXOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_X.SetIndicateBackColor(TempStructPKS.fdPLCXOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKS.fdPLCROriginDone <> OldStructPKS.fdPLCROriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_R.SetIndicateBackColor(TempStructPKS.fdPLCROriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKS.fdPLCZOriginDone <> OldStructPKS.fdPLCZOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Z.SetIndicateBackColor(TempStructPKS.fdPLCZOriginDone)
                                                   End Sub)
                        End If
                        OldStructPKS.fdPLCXPosition = TempStructPKS.fdPLCXPosition
                        OldStructPKS.fdPLCRPosition = TempStructPKS.fdPLCRPosition
                        OldStructPKS.fdPLCZPosition = TempStructPKS.fdPLCZPosition
                        OldStructPKS.fdHMIMoveXPosition = TempStructPKS.fdHMIMoveXPosition
                        OldStructPKS.fdHMIMoveRPosition = TempStructPKS.fdHMIMoveRPosition
                        OldStructPKS.fdHMIMoveZPosition = TempStructPKS.fdHMIMoveZPosition
                        OldStructPKS.fdHMIMoveXTolerance = TempStructPKS.fdHMIMoveXTolerance
                        OldStructPKS.fdHMIMoveRTolerance = TempStructPKS.fdHMIMoveRTolerance
                        OldStructPKS.fdHMIMoveZTolerance = TempStructPKS.fdHMIMoveZTolerance
                        OldStructPKS.fdPLCXOriginDone = TempStructPKS.fdPLCXOriginDone
                        OldStructPKS.fdPLCROriginDone = TempStructPKS.fdPLCROriginDone
                        OldStructPKS.fdPLCZOriginDone = TempStructPKS.fdPLCZOriginDone
                        iStep = 4

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