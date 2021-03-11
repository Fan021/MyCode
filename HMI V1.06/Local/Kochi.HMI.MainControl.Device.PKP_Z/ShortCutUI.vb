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
    Private cPKP_Z As clsPKP_Z
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructPKP_Z As New StructPKP_Z
    Private TempStructPKP_Z As New StructPKP_Z
    Private isCancal As Boolean = True

    Public Const FormName As String = "PKP_ZShortCutUI"

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Get
            Return cPKP_Z
        End Get
        Set(ByVal value As Object)
            cPKP_Z = ObjectSource
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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveZ")
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceY")
        HmiLabel_ToleranceY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceZ")
        HmiLabel_ToleranceZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorX")
        HmiLabel_SensorX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorY")
        HmiLabel_SensorY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorZ")
        HmiLabel_SensorZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        Label_X.Text = OldStructPKP_Z.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructPKP_Z.fdPLCYPosition.ToString("0.00")
        Label_Z.Text = OldStructPKP_Z.fdPLCZPosition.ToString("0.00")
        HmiTextBox_MoveX.Text = OldStructPKP_Z.fdHMIMoveXPosition.ToString("0.00")
        HmiTextBox_MoveY.Text = OldStructPKP_Z.fdHMIMoveYPosition.ToString("0.00")
        HmiTextBox_MoveZ.Text = OldStructPKP_Z.fdHMIMoveZPosition.ToString("0.00")
        HmiTextBox_ToleranceX.Text = OldStructPKP_Z.fdHMIMoveXTolerance.ToString("0.00")
        HmiTextBox_ToleranceY.Text = OldStructPKP_Z.fdHMIMoveYTolerance.ToString("0.00")
        HmiTextBox_ToleranceZ.Text = OldStructPKP_Z.fdHMIMoveZTolerance.ToString("0.00")
        HmiTextBox_Pro.Text = OldStructPKP_Z.fdHMIProg
        HmiTextBox_AST.Text = OldStructPKP_Z.strHMIAST

        HmiTextBox_AST.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_MoveZ.TextBoxReadOnly = True
        HmiTextBox_ToleranceX.TextBoxReadOnly = True
        HmiTextBox_ToleranceY.TextBoxReadOnly = True
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructPKP_Z), New StructPKP_Z)
                        iStep = iStep + 1

                    Case 4
                        TempStructPKP_Z = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructPKP_Z.fdPLCXPosition <> OldStructPKP_Z.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructPKP_Z.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCYPosition <> OldStructPKP_Z.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Y.Text = TempStructPKP_Z.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCZPosition <> OldStructPKP_Z.fdPLCZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Z.Text = TempStructPKP_Z.fdPLCZPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveXPosition <> OldStructPKP_Z.fdHMIMoveXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveX.Text = TempStructPKP_Z.fdHMIMoveXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveYPosition <> OldStructPKP_Z.fdHMIMoveYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveY.Text = TempStructPKP_Z.fdHMIMoveYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveZPosition <> OldStructPKP_Z.fdHMIMoveZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveZ.Text = TempStructPKP_Z.fdHMIMoveZPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveXTolerance <> OldStructPKP_Z.fdHMIMoveXTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceX.Text = TempStructPKP_Z.fdHMIMoveXTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveYTolerance <> OldStructPKP_Z.fdHMIMoveYTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceY.Text = TempStructPKP_Z.fdHMIMoveYTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIMoveZTolerance <> OldStructPKP_Z.fdHMIMoveZTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceZ.Text = TempStructPKP_Z.fdHMIMoveZTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.strHMIAST <> OldStructPKP_Z.strHMIAST Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_AST.Text = TempStructPKP_Z.strHMIAST
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdHMIProg <> OldStructPKP_Z.fdHMIProg Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.Text = TempStructPKP_Z.fdHMIProg
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCXOriginDone <> OldStructPKP_Z.fdPLCXOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_X.SetIndicateBackColor(TempStructPKP_Z.fdPLCXOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCYOriginDone <> OldStructPKP_Z.fdPLCYOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Y.SetIndicateBackColor(TempStructPKP_Z.fdPLCYOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCZOriginDone <> OldStructPKP_Z.fdPLCZOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Z.SetIndicateBackColor(TempStructPKP_Z.fdPLCZOriginDone)
                                                   End Sub)
                        End If
                        OldStructPKP_Z.fdPLCXPosition = TempStructPKP_Z.fdPLCXPosition
                        OldStructPKP_Z.fdPLCYPosition = TempStructPKP_Z.fdPLCYPosition
                        OldStructPKP_Z.fdPLCZPosition = TempStructPKP_Z.fdPLCZPosition
                        OldStructPKP_Z.fdHMIMoveXPosition = TempStructPKP_Z.fdHMIMoveXPosition
                        OldStructPKP_Z.fdHMIMoveYPosition = TempStructPKP_Z.fdHMIMoveYPosition
                        OldStructPKP_Z.fdHMIMoveZPosition = TempStructPKP_Z.fdHMIMoveZPosition
                        OldStructPKP_Z.fdHMIMoveXTolerance = TempStructPKP_Z.fdHMIMoveXTolerance
                        OldStructPKP_Z.fdHMIMoveYTolerance = TempStructPKP_Z.fdHMIMoveYTolerance
                        OldStructPKP_Z.fdHMIMoveZTolerance = TempStructPKP_Z.fdHMIMoveZTolerance
                        OldStructPKP_Z.strHMIAST = TempStructPKP_Z.strHMIAST
                        OldStructPKP_Z.fdHMIProg = TempStructPKP_Z.fdHMIProg
                        OldStructPKP_Z.fdPLCXOriginDone = TempStructPKP_Z.fdPLCXOriginDone
                        OldStructPKP_Z.fdPLCYOriginDone = TempStructPKP_Z.fdPLCYOriginDone
                        OldStructPKP_Z.fdPLCZOriginDone = TempStructPKP_Z.fdPLCZOriginDone
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