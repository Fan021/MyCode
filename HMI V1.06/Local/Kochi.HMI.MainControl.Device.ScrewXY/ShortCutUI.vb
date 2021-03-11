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
    Private cScrewXY As clsScrewXY
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructScrewXY As New StructScrewXY
    Private TempStructScrewXY As New StructScrewXY
    Private isCancel As Boolean = True

    Public Const FormName As String = "ScrewXYShortCutUI"


    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Get
            Return cScrewXY
        End Get
        Set(ByVal value As Object)
            cScrewXY = ObjectSource
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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Speed.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Speed")
        HmiLabel_Speed.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        Label_X.Text = OldStructScrewXY.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructScrewXY.fdPLCYPosition.ToString("0.00")
        HmiTextBox_MoveX.Text = OldStructScrewXY.fdHMIMoveXPosition.ToString("0.00")
        HmiTextBox_MoveY.Text = OldStructScrewXY.fdHMIMoveYPosition.ToString("0.00")
        HmiTextBox_Speed.Text = OldStructScrewXY.fdHMISpeed.ToString

        HmiTextBox_Pro.Text = OldStructScrewXY.fdHMIProg
        HmiTextBox_AST.Text = OldStructScrewXY.strHMIAST

        HmiTextBox_AST.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_Speed.TextBoxReadOnly = True
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "8"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "9", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewXY), New StructScrewXY)
                        iStep = iStep + 1

                    Case 4
                        TempStructScrewXY = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructScrewXY.fdPLCXPosition <> OldStructScrewXY.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructScrewXY.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdPLCYPosition <> OldStructScrewXY.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Y.Text = TempStructScrewXY.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMIMoveXPosition <> OldStructScrewXY.fdHMIMoveXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveX.Text = TempStructScrewXY.fdHMIMoveXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMIMoveYPosition <> OldStructScrewXY.fdHMIMoveYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_MoveY.Text = TempStructScrewXY.fdHMIMoveYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMIAutoSpeed <> OldStructScrewXY.fdHMIAutoSpeed Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Speed.Text = TempStructScrewXY.fdHMIAutoSpeed.ToString
                                                   End Sub)
                        End If
                        If TempStructScrewXY.strHMIAST <> OldStructScrewXY.strHMIAST Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_AST.Text = TempStructScrewXY.strHMIAST
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMIProg <> OldStructScrewXY.fdHMIProg Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Pro.Text = TempStructScrewXY.fdHMIProg
                                                   End Sub)
                        End If
                        OldStructScrewXY.fdPLCXPosition = TempStructScrewXY.fdPLCXPosition
                        OldStructScrewXY.fdPLCYPosition = TempStructScrewXY.fdPLCYPosition
                        OldStructScrewXY.fdHMIMoveXPosition = TempStructScrewXY.fdHMIMoveXPosition
                        OldStructScrewXY.fdHMIMoveYPosition = TempStructScrewXY.fdHMIMoveYPosition
                        OldStructScrewXY.fdHMIAutoSpeed = TempStructScrewXY.fdHMIAutoSpeed
                        OldStructScrewXY.strHMIAST = TempStructScrewXY.strHMIAST
                        OldStructScrewXY.fdHMIProg = TempStructScrewXY.fdHMIProg
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