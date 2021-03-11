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
    Private cPKP As clsPKP
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructPKP As New StructPKP
    Private TempStructPKP As New StructPKP
    Private isCancel As Boolean = True

    Public Const FormName As String = "PKPShortCutUI"


    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
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
        HmiButton_PKP_X_Home.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_PKP_X_Home")
        HmiButton_PKP_X_Home.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_PKP_Y_Home.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_PKP_Y_Home")
        HmiButton_PKP_Y_Home.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_ToleranceY")
        HmiLabel_ToleranceY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_SensorX")
        HmiLabel_SensorX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_SensorY")
        HmiLabel_SensorY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        Label_X.Text = OldStructPKP.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructPKP.fdPLCYPosition.ToString("0.00")
        HmiTextBox_MoveX.Text = OldStructPKP.fdHMIMoveXPosition.ToString("0.00")
        HmiTextBox_MoveY.Text = OldStructPKP.fdHMIMoveYPosition.ToString("0.00")
        HmiTextBox_ToleranceX.Text = OldStructPKP.fdHMIMoveXTolerance.ToString("0.00")
        HmiTextBox_ToleranceY.Text = OldStructPKP.fdHMIMoveYTolerance.ToString("0.00")

        HmiTextBox_Pro.Text = OldStructPKP.fdHMIProg
        HmiTextBox_AST.Text = OldStructPKP.strHMIAST

        HmiTextBox_AST.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_ToleranceX.TextBoxReadOnly = True
        HmiTextBox_ToleranceY.TextBoxReadOnly = True

        AddHandler HmiButton_PKP_X_Home.Button.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_PKP_X_Home.Button.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_PKP_Y_Home.Button.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_PKP_Y_Home.Button.MouseUp, AddressOf Button_MouseUp
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructPKP), New StructPKP)
                        iStep = iStep + 1

                    Case 4
                        TempStructPKP = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructPKP.fdPLCXPosition <> OldStructPKP.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructPKP.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdPLCYPosition <> OldStructPKP.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Y.Text = TempStructPKP.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdHMIMoveXPosition <> OldStructPKP.fdHMIMoveXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveX.Text = TempStructPKP.fdHMIMoveXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdHMIMoveYPosition <> OldStructPKP.fdHMIMoveYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveY.Text = TempStructPKP.fdHMIMoveYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdHMIMoveXTolerance <> OldStructPKP.fdHMIMoveXTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceX.Text = TempStructPKP.fdHMIMoveXTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdHMIMoveYTolerance <> OldStructPKP.fdHMIMoveYTolerance Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_ToleranceY.Text = TempStructPKP.fdHMIMoveYTolerance.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.strHMIAST <> OldStructPKP.strHMIAST Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_AST.Text = TempStructPKP.strHMIAST
                                                   End Sub)
                        End If
                        If TempStructPKP.fdHMIProg <> OldStructPKP.fdHMIProg Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.Text = TempStructPKP.fdHMIProg
                                                   End Sub)
                        End If
                        If TempStructPKP.fdPLCXOriginDone <> OldStructPKP.fdPLCXOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_X.SetIndicateBackColor(TempStructPKP.fdPLCXOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP.fdPLCYOriginDone <> OldStructPKP.fdPLCYOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Y.SetIndicateBackColor(TempStructPKP.fdPLCYOriginDone)
                                                   End Sub)
                        End If
                        OldStructPKP.fdPLCXPosition = TempStructPKP.fdPLCXPosition
                        OldStructPKP.fdPLCYPosition = TempStructPKP.fdPLCYPosition
                        OldStructPKP.fdHMIMoveXPosition = TempStructPKP.fdHMIMoveXPosition
                        OldStructPKP.fdHMIMoveYPosition = TempStructPKP.fdHMIMoveYPosition
                        OldStructPKP.fdHMIMoveXTolerance = TempStructPKP.fdHMIMoveXTolerance
                        OldStructPKP.fdHMIMoveYTolerance = TempStructPKP.fdHMIMoveYTolerance
                        OldStructPKP.strHMIAST = TempStructPKP.strHMIAST
                        OldStructPKP.fdHMIProg = TempStructPKP.fdHMIProg
                        OldStructPKP.fdPLCXOriginDone = TempStructPKP.fdPLCXOriginDone
                        OldStructPKP.fdPLCYOriginDone = TempStructPKP.fdPLCYOriginDone
                        iStep = 4

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub
    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_PKP_X_Home"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXHome", dNewValue)
            Case "HmiButton_PKP_Y_Home"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYHome", dNewValue)
        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_PKP_X_Home"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXHome", dNewValue)
            Case "HmiButton_PKP_Y_Home"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYHome", dNewValue)
        End Select
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