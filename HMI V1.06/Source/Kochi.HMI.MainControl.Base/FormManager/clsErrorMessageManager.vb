Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsErrorMessageManager
    Implements IDisposable
    Public Const Name As String = "ErrorMessageManager"
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cTableLayoutPanel As TableLayoutPanel
    Protected cTableLayoutPanel_Tips As TableLayoutPanel
    Protected iOldSize As Single = 0
    Protected Button_Abort As New Button
    Protected Button_Save As New Button
    Protected Label_Tips As New Label
    Protected WithEvents Panel_Tips As New Panel
    Protected lListHMIException As New List(Of clsHMIException)
    Protected lListStationError As New Dictionary(Of String, enumErrorMessageManagerState)
    Protected eErrorMessageManagerState As enumErrorMessageManagerState
    Private cLanguageManager As clsLanguageManager
    Protected cLogHandler As clsLogHandler
    Protected cSystemManager As clsSystemManager
    Public Delegate Sub SaveFunction()
    Protected dSaveFunction As SaveFunction
    Public Delegate Sub AbortFunction()
    Protected dAbortFunction As AbortFunction
    Public Delegate Sub ResetFunction()
    Protected dResetFunction As ResetFunction
    Protected _Object As New Object
    Protected cCurrentHMIException As clsHMIException
    Protected mMainForm As IMainUI
    Protected iMainForm As IMainUI
    Protected dOldFontSize As Single
    Protected Delegate Sub DTransferHMIException()
    Protected cTransferHMIException As DTransferHMIException

    Public ReadOnly Property CurrentHMIException As clsHMIException
        Get
            SyncLock _Object
                Return cCurrentHMIException
            End SyncLock
        End Get
    End Property


    Public ReadOnly Property ErrorMessageManagerState As enumErrorMessageManagerState
        Get
            SyncLock _Object
                Return eErrorMessageManagerState
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                iMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), IMainUI)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cTransferHMIException = New DTransferHMIException(AddressOf TransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cTableLayoutPanel As TableLayoutPanel) As Boolean
        SyncLock _Object
            Try
                Me.cTableLayoutPanel = cTableLayoutPanel
                cTableLayoutPanel.RowCount = cTableLayoutPanel.RowCount + 1
                cTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
                CreateTab()
                AddHandler Button_Save.Click, AddressOf Button_Click
                AddHandler Button_Abort.Click, AddressOf Button_Click
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterSaveFunction(ByVal dSaveFunction As SaveFunction) As Boolean
        SyncLock _Object
            Me.dSaveFunction = dSaveFunction
            Return True
        End SyncLock
    End Function

    Public Function RegisterAbortFunction(ByVal dAbortFunction As AbortFunction) As Boolean
        SyncLock _Object
            Me.dAbortFunction = dAbortFunction
            Return True
        End SyncLock
    End Function

    Public Function RegisterResetFunction(ByVal dResetFunction As ResetFunction) As Boolean
        SyncLock _Object
            Me.dResetFunction = dResetFunction
            Return True
        End SyncLock
    End Function

    Public Function DisposeSaveFunction() As Boolean
        SyncLock _Object
            Me.dSaveFunction = Nothing
            Return True
        End SyncLock
    End Function

    Public Function DisposeAbortFunction() As Boolean
        SyncLock _Object
            Me.dAbortFunction = Nothing
            Return True
        End SyncLock
    End Function

    Public Function DisposeResetFunction() As Boolean
        SyncLock _Object
            Me.dResetFunction = Nothing
            Return True
        End SyncLock
    End Function

    Public Function AddHMIException(ByVal cHMIException As clsHMIException) As Boolean
        SyncLock _Object
            Try
                If lListHMIException.Any(Function(e) cHMIException.Name = "PLC" And e.Name = cHMIException.Name) Then
                    lListHMIException = lListHMIException.Where(Function(e) e.Name <> "PLC").ToList
                    eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                    lListHMIException.Add(cHMIException)
                ElseIf Not lListHMIException.Any(Function(e) e.Message = cHMIException.Message And e.Name = cHMIException.Name) Then
                    lListHMIException.Add(cHMIException)
                Else
                    Return True
                End If
                If cHMIException.Name <> "" And (cHMIException.ExceptionType <> enumExceptionType.Normal And cHMIException.ExceptionType <> enumExceptionType.PLC) Then
                    If Not lListStationError.ContainsKey(cHMIException.Name) Then
                        lListStationError.Add(cHMIException.Name, enumErrorMessageManagerState.Alarm)
                    Else
                        lListStationError(cHMIException.Name) = enumErrorMessageManagerState.Alarm
                    End If
                End If
                cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, cHMIException)
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddHMIException(ByVal cException As Exception, Optional ByVal eExceptionType As enumExceptionType = enumExceptionType.Alarm) As Boolean
        SyncLock _Object
            Try
                Dim cHMIException As clsHMIException
                If TypeOf cException Is clsHMIException Then
                    AddHMIException(CType(cException, clsHMIException))
                    Return True
                End If

                cHMIException = New clsHMIException(cException, eExceptionType)
                If Not lListHMIException.Any(Function(e) cHMIException.Name = "PLC" And e.Name = cHMIException.Name) Then
                    lListHMIException = lListHMIException.Where(Function(e) e.Name <> "PLC").ToList
                    eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                    lListHMIException.Add(cHMIException)
                ElseIf Not lListHMIException.Any(Function(e) e.Message = cHMIException.Message And e.Name = cHMIException.Name) Then
                    lListHMIException.Add(cHMIException)
                Else
                    Return True
                End If
                cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, cHMIException)
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    'Public Function AddHMIException(ByVal strID As String, ByVal cException As Exception, Optional ByVal eExceptionType As enumExceptionType = enumExceptionType.Alarm) As Boolean
    '    SyncLock _Object
    '        Try
    '            Dim cHMIException As clsHMIException
    '            'If TypeOf cException Is clsHMIException Then
    '            '    AddHMIException(CType(cException, clsHMIException))
    '            '    Return True
    '            'End If

    '            cHMIException = New clsHMIException(cLanguageManager.GetTextLine("ErrorMessageManager", "1", strID, cException.Message), eExceptionType, strID)
    '            If Not lListHMIException.Any(Function(e) e.Message = cHMIException.Message And e.Name = cHMIException.Name) Then
    '                lListHMIException.Add(cHMIException)
    '            Else
    '                Return True
    '            End If

    '            If cHMIException.Name <> "" Then
    '                If Not lListStationError.ContainsKey(cHMIException.Name) Then
    '                    lListStationError.Add(cHMIException.Name, enumErrorMessageManagerState.Alarm)
    '                Else
    '                    lListStationError(cHMIException.Name) = enumErrorMessageManagerState.Alarm
    '                End If
    '            End If
    '            cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, cHMIException)
    '            TransferHMIException()
    '            Return True
    '        Catch ex As Exception
    '            Throw New clsHMIException(ex, enumExceptionType.Crash)
    '            Return False
    '        End Try
    '    End SyncLock
    'End Function

    Public Sub TransferHMIException()
        SyncLock _Object
            If Not IsNothing(cCurrentHMIException) AndAlso cCurrentHMIException.Name <> "" Then
                If lListStationError.ContainsKey(cCurrentHMIException.Name) Then
                    If Not lListHMIException.Any(Function(e1) e1.Name = cCurrentHMIException.Name) Then
                        lListStationError(cCurrentHMIException.Name) = enumErrorMessageManagerState.Normal
                    End If
                End If
            End If
            If lListHMIException.Count > 0 And eErrorMessageManagerState = enumErrorMessageManagerState.Normal Then
                eErrorMessageManagerState = enumErrorMessageManagerState.Alarm
                If dOldFontSize > 0 Then Label_Tips.Font = New System.Drawing.Font(Label_Tips.Font.Name, Single.Parse(dOldFontSize), Label_Tips.Font.Style)
                Dim lListPLCHMIException As List(Of clsHMIException) = lListHMIException.Where(Function(e) e.Name = "PLC").ToList
                If lListPLCHMIException.Count > 0 Then
                    cCurrentHMIException = lListPLCHMIException(0)
                Else
                    cCurrentHMIException = lListHMIException(0)
                End If
                Select Case cCurrentHMIException.ExceptionType
                    Case enumExceptionType.Crash
                        SetLabel_Msg(cCurrentHMIException)
                        iMainForm.RaiseHMIExceptionEvent(cCurrentHMIException)
                    Case enumExceptionType.Confirm
                        SetLabel_Msg(cCurrentHMIException)
                        ShowButton()
                    Case enumExceptionType.Normal
                        SetLabel_Msg(cCurrentHMIException)
                    Case Else
                        SetLabel_Msg(cCurrentHMIException)
                End Select
            ElseIf lListHMIException.Count = 0 Then
                eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                cCurrentHMIException = Nothing
                ResetText()
                HiddenButton()
                HiddenError()
            End If
            Application.DoEvents()
        End SyncLock
    End Sub

    Private Sub SetLabel_Msg(ByVal cHMIException As clsHMIException)
        Dim strFontName As String = ""
        dOldFontSize = Label_Tips.Font.Size
        If cLanguageManager.CurrentLanguageCfg.Name = "Chinese" Then
            strFontName = "Arial"
        Else
            strFontName = Label_Tips.Font.Name
        End If
        If cHMIException.Message.Length >= 60 And cHMIException.Message.Length <= 100 Then
            Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.8), Label_Tips.Font.Style)
        End If
        If cHMIException.Message.Length > 100 And cHMIException.Message.Length <= 200 Then
            Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.6), Label_Tips.Font.Style)
        End If

        If cHMIException.Message.Length > 200 Then
            Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.4), Label_Tips.Font.Style)
        End If
        Dim mTemp As String = cHMIException.Message
        Label_Tips.Text = mTemp.Replace("\r", vbCr).Replace("\n", vbLf)
        Select Case cHMIException.ExceptionType
            Case enumExceptionType.Crash
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            Case enumExceptionType.Confirm
                Label_Tips.ForeColor = System.Drawing.Color.Blue()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 70)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
            Case enumExceptionType.Normal
                Label_Tips.ForeColor = System.Drawing.Color.Blue()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            Case Else
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
        End Select

        If cTableLayoutPanel.RowCount = 2 Then
            cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 84)
            cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
        End If
        If cTableLayoutPanel.RowCount = 3 Then
            cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 68)
            cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
            cTableLayoutPanel.RowStyles(2) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
        End If
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Reset()
            If CType(sender, Button).Name = "Button_Save" Then
                dSaveFunction.Invoke()
                HiddenButton()
            End If
            If CType(sender, Button).Name = "Button_Abort" Then
                dAbortFunction.Invoke()
                HiddenButton()
            End If
        End SyncLock
    End Sub

    Public Function GetListStationErrorKey() As List(Of String)
        SyncLock _Object
            Try
                Return lListStationError.Keys.ToList()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetStationManagerStateFromKey(ByVal strKey As String) As enumErrorMessageManagerState
        SyncLock _Object
            Try
                If lListStationError.ContainsKey(strKey) Then
                    Return lListStationError(strKey)
                Else
                    Return enumErrorMessageManagerState.Normal
                End If

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function Reset() As Boolean
        SyncLock _Object
            Try
                If IsNothing(cCurrentHMIException) Then
                    Return True
                End If
                If cCurrentHMIException.Name = "PLC" Then
                    Return True
                Else
                    If lListHMIException.Count > 0 Then
                        eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                        lListHMIException.RemoveAt(0)
                    End If
                    mMainForm.InvokeAction(cTransferHMIException)
                    If Not IsNothing(dResetFunction) Then dResetFunction.Invoke()
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function Clean(ByVal strStationName As String) As Boolean
        SyncLock _Object
            Try
                lListHMIException = lListHMIException.Where(Function(e) e.Name <> strStationName).ToList
                eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                mMainForm.InvokeAction(cTransferHMIException)
                System.Threading.Thread.Sleep(100)
                Application.DoEvents()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function Clean(ByVal strStationName As String, ByVal cHMIException As clsHMIException) As Boolean
        SyncLock _Object
            Try
                lListHMIException = lListHMIException.Where(Function(e) (e.Message <> cHMIException.Message) Or (e.Message = cHMIException.Message And e.Name <> strStationName)).ToList
                eErrorMessageManagerState = enumErrorMessageManagerState.Normal
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Sub CreateTab()
        SyncLock _Object '
            'Panel_Tips
            '
            Me.Panel_Tips.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel_Tips.Location = New System.Drawing.Point(0, 360)
            Me.Panel_Tips.Margin = New System.Windows.Forms.Padding(0)
            Me.Panel_Tips.Name = "Panel_Tips"
            Me.Panel_Tips.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)

            cTableLayoutPanel_Tips = New TableLayoutPanel
            cTableLayoutPanel_Tips.ColumnCount = 3
            cTableLayoutPanel_Tips.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
            cTableLayoutPanel_Tips.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
            cTableLayoutPanel_Tips.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
            cTableLayoutPanel_Tips.Dock = System.Windows.Forms.DockStyle.Fill
            cTableLayoutPanel_Tips.Location = New System.Drawing.Point(0, 0)
            cTableLayoutPanel_Tips.Margin = New System.Windows.Forms.Padding(0)
            cTableLayoutPanel_Tips.Name = "TableLayoutPanel_Tips"
            cTableLayoutPanel_Tips.RowCount = 1
            cTableLayoutPanel_Tips.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.Panel_Tips.Controls.Add(cTableLayoutPanel_Tips)

            '
            'Label_Tips
            '
            Label_Tips.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Label_Tips.AutoSize = True
            Label_Tips.Dock = System.Windows.Forms.DockStyle.Fill
            Label_Tips.BackColor = System.Drawing.Color.LightGray
            Label_Tips.Font = New System.Drawing.Font("Calibri", 24.0!, System.Drawing.FontStyle.Bold)
            Label_Tips.ForeColor = System.Drawing.Color.Blue
            Label_Tips.Location = New System.Drawing.Point(0, 0)
            Label_Tips.Margin = New System.Windows.Forms.Padding(0)
            Label_Tips.Name = "Label_Tips"
            Label_Tips.Text = ""
            Label_Tips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            cTableLayoutPanel_Tips.Controls.Add(Label_Tips, 0, 0)
            '
            'Button_Save
            ' 
            Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
            Button_Save.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
            Button_Save.Location = New System.Drawing.Point(439, 3)
            Button_Save.Name = "Button_Save"
            Button_Save.Text = cLanguageManager.GetTextLine("ErrorMessageManager", "Button_Save")
            Button_Save.UseVisualStyleBackColor = True
            cTableLayoutPanel_Tips.Controls.Add(Button_Save, 1, 0)


            Button_Abort.Dock = System.Windows.Forms.DockStyle.Fill
            Button_Abort.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
            Button_Abort.Location = New System.Drawing.Point(532, 3)
            Button_Abort.Name = "Button_Abort"
            Button_Abort.Text = cLanguageManager.GetTextLine("ErrorMessageManager", "Button_Abort")
            Button_Abort.UseVisualStyleBackColor = True
            cTableLayoutPanel_Tips.Controls.Add(Button_Abort, 2, 0)

            cTableLayoutPanel.Controls.Add(Panel_Tips, 0, cTableLayoutPanel.RowCount - 1)
            cTableLayoutPanel.SetColumnSpan(Panel_Tips, cTableLayoutPanel.ColumnCount)

            cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
            cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            If cTableLayoutPanel.RowCount = 2 Then
                cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 0)
            End If
            If cTableLayoutPanel.RowCount = 3 Then
                cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 84)
                cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
                cTableLayoutPanel.RowStyles(2) = New RowStyle(System.Windows.Forms.SizeType.Percent, 0)
            End If
        End SyncLock
    End Sub


    Public Sub ResetText()
        SyncLock _Object
            mMainForm.InvokeAction(Sub()
                                       If Label_Tips.Text <> "" Then
                                           Label_Tips.Font = New System.Drawing.Font(Label_Tips.Font.Name, Single.Parse(dOldFontSize), Label_Tips.Font.Style)
                                           Label_Tips.Text = ""
                                       End If
                                   End Sub)
        End SyncLock
    End Sub

    Public Sub HiddenError()
        SyncLock _Object
            mMainForm.InvokeAction(Sub()
                                       If cTableLayoutPanel.RowCount = 2 Then
                                           cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 100)
                                           cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 0)
                                       End If
                                       If cTableLayoutPanel.RowCount = 3 Then
                                           cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 84)
                                           cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
                                           cTableLayoutPanel.RowStyles(2) = New RowStyle(System.Windows.Forms.SizeType.Percent, 0)
                                       End If
                                   End Sub)
        End SyncLock
    End Sub

    Public Sub ShowButton()
        SyncLock _Object
            mMainForm.InvokeAction(Sub()
                                       cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 70)
                                       cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                                       cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                                   End Sub)
        End SyncLock
    End Sub

    Public Sub HiddenButton()
        SyncLock _Object
            mMainForm.InvokeAction(Sub()
                                       cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                                       cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                                       cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                                   End Sub)
        End SyncLock
    End Sub

    Private Sub Panel_Tips_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Tips.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                RemoveHandler Button_Save.Click, AddressOf Button_Click
                RemoveHandler Button_Abort.Click, AddressOf Button_Click
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next
        MyBase.Finalize()
    End Sub
#End Region

End Class

Public Enum enumErrorMessageManagerState
    Alarm = 0
    Normal
End Enum

