Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsMainTipsManager
    Implements IDisposable
    Public Const Name As String = "MainTipsManager"
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cTableLayoutPanel As TableLayoutPanel
    Protected cTableLayoutPanel_Tips As TableLayoutPanel
    Protected cTabControl As TabControl
    Protected Button_Abort As New Button
    Protected Button_Continue As New Button
    Protected Label_Tips As New Label
    Protected WithEvents Panel_Tips As New Panel
    Protected cLogHandler As clsLogHandler
    Protected cSystemManager As clsSystemManager
    Protected mMainForm As IMainUI
    Private cLanguageManager As clsLanguageManager
    Protected cTextManager As clsTextManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected cCurrentMainTipsManagerCfg As clsMainTipsManagerCfg
    Protected lListMainTipsManagerCfg As New Dictionary(Of String, clsMainTipsManagerCfg)
    Protected lListCurrentMainTipsManagerCfg As New Dictionary(Of String, clsMainTipsManagerCfg)
    Protected lListStationMainTipsConfirmType As New Dictionary(Of String, enumMainTipsConfirmType)
    Protected _Object As New Object
    Protected dOldFontSize As Single
    Protected Delegate Sub DTransferHMIException()
    Protected cTransferHMIException As DTransferHMIException
    Protected Delegate Sub DAddErrorMessage(ByVal cMainTipsManagerCfg As clsMainTipsManagerCfg)
    Protected cAddErrorMessage As DAddErrorMessage
    Protected OldColor As Color
    Protected OldBackColor As Color



    Public ReadOnly Property CurrentMainTipsManagerCfg As clsMainTipsManagerCfg
        Get
            SyncLock _Object
                Return cCurrentMainTipsManagerCfg
            End SyncLock
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cTextManager = CType(cSystemElement(clsTextManager.Name), clsTextManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
                cTransferHMIException = New DTransferHMIException(AddressOf TransferHMIException)
                cAddErrorMessage = New DAddErrorMessage(AddressOf AddErrorMessage)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal cTableLayoutPanel As TableLayoutPanel, ByVal cTabControl As TabControl) As Boolean
        SyncLock _Object
            Try
                Me.cTableLayoutPanel = cTableLayoutPanel
                Me.cTabControl = cTabControl
                cTableLayoutPanel.RowCount = cTableLayoutPanel.RowCount + 1
                cTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
                CreateControl()
                AddHandler Button_Continue.Click, AddressOf Button_Click
                AddHandler Button_Abort.Click, AddressOf Button_Click
                AddHandler cTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function AddTips(ByVal cMainTipsManagerCfg As clsMainTipsManagerCfg) As Boolean
        SyncLock _Object
            Try

                If lListStationMainTipsConfirmType.ContainsKey(cMainTipsManagerCfg.ID) Then
                    lListStationMainTipsConfirmType(cMainTipsManagerCfg.ID) = enumMainTipsConfirmType.Normal
                Else
                    lListStationMainTipsConfirmType.Add(cMainTipsManagerCfg.ID, enumMainTipsConfirmType.Normal)
                End If

                If lListMainTipsManagerCfg.ContainsKey(cMainTipsManagerCfg.ID) Then
                    lListMainTipsManagerCfg(cMainTipsManagerCfg.ID) = cMainTipsManagerCfg
                Else
                    lListMainTipsManagerCfg.Add(cMainTipsManagerCfg.ID, cMainTipsManagerCfg)
                End If
                If lListCurrentMainTipsManagerCfg.ContainsKey(cMainTipsManagerCfg.ID) Then
                    lListCurrentMainTipsManagerCfg(cMainTipsManagerCfg.ID) = New clsMainTipsManagerCfg(cMainTipsManagerCfg.ID, cMainTipsManagerCfg.Text, cMainTipsManagerCfg.MainTipsManagerType)
                Else
                    lListCurrentMainTipsManagerCfg.Add(cMainTipsManagerCfg.ID, New clsMainTipsManagerCfg(cMainTipsManagerCfg.ID, cMainTipsManagerCfg.Text, cMainTipsManagerCfg.MainTipsManagerType))
                End If
                '   mMainForm.InvokeAction(cAddErrorMessage, cMainTipsManagerCfg)
                If cMainTipsManagerCfg.MainTipsManagerType <> enumMainTipsManagerType.NormalAndNoLog Then
                    cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, cMainTipsManagerCfg)
                End If

                mMainForm.InvokeAction(cTransferHMIException)
                Application.DoEvents()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub AddErrorMessage(ByVal cMainTipsManagerCfg As clsMainTipsManagerCfg)
        If Not IsNothing(cTabControl) Then
            If Not IsNothing(cTabControl.SelectedTab) Then
                If cMainTipsManagerCfg.ID <> "PLC" And cMainTipsManagerCfg.ID <> "" And cMainTipsManagerCfg.ID <> cTabControl.SelectedTab.Name And (cMainTipsManagerCfg.MainTipsManagerType = enumMainTipsManagerType.Alarm Or cMainTipsManagerCfg.MainTipsManagerType = enumMainTipsManagerType.Confirm) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cMainTipsManagerCfg.Text, enumExceptionType.Alarm, cMainTipsManagerCfg.ID))
                End If
            End If
        End If
    End Sub


    Public Function CleanStationTips(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                If lListMainTipsManagerCfg.ContainsKey(strStationID) Then
                    lListMainTipsManagerCfg.Remove(strStationID)
                End If

                If lListStationMainTipsConfirmType.ContainsKey(strStationID) Then
                    lListStationMainTipsConfirmType(strStationID) = enumMainTipsConfirmType.Normal
                End If
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CleanTips() As Boolean
        SyncLock _Object
            Try
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CleanPLCTips(ByVal strStationName As String) As Boolean
        SyncLock _Object
            Try
                cCurrentMainTipsManagerCfg = Nothing
                If lListMainTipsManagerCfg.ContainsKey(strStationName) Then
                    lListMainTipsManagerCfg.Remove(strStationName)
                End If
                mMainForm.InvokeAction(cTransferHMIException)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Sub TransferHMIException()
        SyncLock _Object
            Try
                If lListMainTipsManagerCfg.Count > 0 Then
                    If dOldFontSize > 0 Then Label_Tips.Font = New System.Drawing.Font(Label_Tips.Font.Name, Single.Parse(dOldFontSize), Label_Tips.Font.Style)
                    If lListMainTipsManagerCfg.ContainsKey("PLC") Then
                        cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg("PLC")
                    ElseIf lListMainTipsManagerCfg.ContainsKey("Mode") Then
                        cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg("Mode")
                    ElseIf lListMainTipsManagerCfg.ContainsKey("User") Then
                        cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg("User")
                    ElseIf lListMainTipsManagerCfg.ContainsKey("") Then
                        cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg("")
                        lListMainTipsManagerCfg.Remove("")
                    ElseIf Not IsNothing(cTabControl) AndAlso Not IsNothing(cTabControl.SelectedTab) Then
                        If lListMainTipsManagerCfg.ContainsKey("PLC" + cTabControl.SelectedTab.Name) Then
                            cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg("PLC" + cTabControl.SelectedTab.Name)
                        ElseIf lListMainTipsManagerCfg.ContainsKey(cTabControl.SelectedTab.Name) Then
                            cCurrentMainTipsManagerCfg = lListMainTipsManagerCfg(cTabControl.SelectedTab.Name)
                        Else
                            cCurrentMainTipsManagerCfg = New clsMainTipsManagerCfg("", enumMainTipsManagerType.Normal)
                        End If
                    End If

                    Select Case cCurrentMainTipsManagerCfg.MainTipsManagerType
                        Case enumMainTipsManagerType.Normal
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)
                        Case enumMainTipsManagerType.PLC, enumMainTipsManagerType.Mode
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)
                        Case enumMainTipsManagerType.Confirm
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)
                        Case enumMainTipsManagerType.ConfirmDisableContine
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)

                        Case enumMainTipsManagerType.Alarm
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)
                        Case Else
                            SetLabel_Msg(cCurrentMainTipsManagerCfg.MainTipsManagerType, cCurrentMainTipsManagerCfg.FontSize)
                    End Select

                Else
                    Label_Tips.ForeColor = OldColor
                    Label_Tips.BackColor = OldBackColor
                    HiddenButton()
                End If
                    Application.DoEvents()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Private Sub SetLabel_Msg(ByVal eMainTipsManagerType As enumMainTipsManagerType, ByVal iFontSize As Single)
        Dim strFontName As String = ""
        dOldFontSize = Label_Tips.Font.Size
        If cLanguageManager.CurrentLanguageCfg.Name = "Chinese" Then
            strFontName = "Arial"
        Else
            strFontName = "Arial"
        End If
        Dim iSize As Integer = 0
        If eMainTipsManagerType = enumMainTipsManagerType.Confirm Or eMainTipsManagerType = enumMainTipsManagerType.ConfirmDisableContine Then
            iSize = 20
        Else
            iSize = 40
        End If
        Label_Tips.ForeColor = OldColor
        Label_Tips.BackColor = OldBackColor
        If iFontSize > 0 Then
            Label_Tips.Font = New System.Drawing.Font(strFontName, iFontSize, Label_Tips.Font.Style)
        Else

            If cCurrentMainTipsManagerCfg.Text.Length >= iSize And cCurrentMainTipsManagerCfg.Text.Length <= iSize + 30 Then
                Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.8), Label_Tips.Font.Style)
            End If
            If cCurrentMainTipsManagerCfg.Text.Length >= iSize + 30 And cCurrentMainTipsManagerCfg.Text.Length < 150 Then
                Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.6), Label_Tips.Font.Style)
            End If

            If cCurrentMainTipsManagerCfg.Text.Length >= 150 Then
                Label_Tips.Font = New System.Drawing.Font(strFontName, Single.Parse(dOldFontSize * 0.4), Label_Tips.Font.Style)
            End If
        End If
       
        cCurrentMainTipsManagerCfg.Text = cCurrentMainTipsManagerCfg.Text.Replace("\r", vbCr)
        cCurrentMainTipsManagerCfg.Text = cCurrentMainTipsManagerCfg.Text.Replace("\n", vbLf)
        Label_Tips.Text = cCurrentMainTipsManagerCfg.Text
        Select Case eMainTipsManagerType
            Case enumMainTipsManagerType.Normal
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                Label_Tips.ForeColor = System.Drawing.Color.Blue()


            Case enumMainTipsManagerType.Confirm
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 70)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                Button_Continue.Enabled = True
            Case enumMainTipsManagerType.ConfirmDisableContine
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 70)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 15)
                Button_Continue.Enabled = False
            Case enumMainTipsManagerType.Alarm
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)

            Case enumMainTipsManagerType.PLCAlarm
                Label_Tips.ForeColor = System.Drawing.Color.Red()
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            Case Else
                cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
                cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
                ChangFontColor(Label_Tips.Text)
        End Select
    End Sub

    Private Sub ChangFontColor(ByVal strMessage As String)
        Try
            If strMessage.IndexOf("[BackColor_") >= 0 And strMessage.IndexOf("]") >= 0 Then
                Dim mColor As String = strMessage.Substring(strMessage.IndexOf("[BackColor_") + 11, strMessage.IndexOf("]") - strMessage.IndexOf("[BackColor_") - 11)
                Dim mReplace As String = "[BackColor_" + mColor + "]"
                strMessage = strMessage.Replace(mReplace, "")
                Label_Tips.BackColor = Color.FromName(mColor)
            End If
            If strMessage.IndexOf("[Color_") >= 0 And strMessage.IndexOf("]") >= 0 Then
                Dim mColor As String = strMessage.Substring(strMessage.IndexOf("[Color_") + 7, strMessage.IndexOf("]") - strMessage.IndexOf("[Color_") - 7)
                Dim mReplace As String = "[Color_" + mColor + "]"
                strMessage = strMessage.Replace(mReplace, "")
                Label_Tips.ForeColor = Color.FromName(mColor)
            End If
            Label_Tips.Text = strMessage
        Catch ex As Exception
            Label_Tips.BackColor = OldBackColor
            Label_Tips.ForeColor = System.Drawing.Color.Blue()
        End Try
    
    End Sub
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If Not IsNothing(cCurrentMainTipsManagerCfg) AndAlso cCurrentMainTipsManagerCfg.ID <> "" Then
                If sender.name = "Button_Continue" Then
                    If lListStationMainTipsConfirmType.ContainsKey(cCurrentMainTipsManagerCfg.ID) Then
                        lListStationMainTipsConfirmType(cCurrentMainTipsManagerCfg.ID) = enumMainTipsConfirmType.Continue
                    Else
                        lListStationMainTipsConfirmType.Add(cCurrentMainTipsManagerCfg.ID, enumMainTipsConfirmType.Continue)
                    End If
                    lListMainTipsManagerCfg.Remove(cCurrentMainTipsManagerCfg.ID)
                    If lListCurrentMainTipsManagerCfg.ContainsKey(cCurrentMainTipsManagerCfg.ID) Then
                        lListCurrentMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID) = New clsMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID, cCurrentMainTipsManagerCfg.Text, cCurrentMainTipsManagerCfg.MainTipsManagerType)
                    Else
                        lListCurrentMainTipsManagerCfg.Add(cCurrentMainTipsManagerCfg.ID, New clsMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID, cCurrentMainTipsManagerCfg.Text, cCurrentMainTipsManagerCfg.MainTipsManagerType))
                    End If
                End If
                If sender.name = "Button_Abort" Then
                    If lListStationMainTipsConfirmType.ContainsKey(cCurrentMainTipsManagerCfg.ID) Then
                        lListStationMainTipsConfirmType(cCurrentMainTipsManagerCfg.ID) = enumMainTipsConfirmType.Abort
                    Else
                        lListStationMainTipsConfirmType.Add(cCurrentMainTipsManagerCfg.ID, enumMainTipsConfirmType.Abort)
                    End If
                    lListMainTipsManagerCfg.Remove(cCurrentMainTipsManagerCfg.ID)
                    If lListCurrentMainTipsManagerCfg.ContainsKey(cCurrentMainTipsManagerCfg.ID) Then
                        lListCurrentMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID) = New clsMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID, cCurrentMainTipsManagerCfg.Text, cCurrentMainTipsManagerCfg.MainTipsManagerType)
                    Else
                        lListCurrentMainTipsManagerCfg.Add(cCurrentMainTipsManagerCfg.ID, New clsMainTipsManagerCfg(cCurrentMainTipsManagerCfg.ID, cCurrentMainTipsManagerCfg.Text, cCurrentMainTipsManagerCfg.MainTipsManagerType))
                    End If
                End If
            End If
            mMainForm.InvokeAction(cTransferHMIException)
        End SyncLock
    End Sub

    Public Sub Auto_Continue(ByVal strStationID As String)
        SyncLock _Object
            Try
                If IsNothing(lListMainTipsManagerCfg(strStationID)) Then
                    Return
                End If
            Catch ex As Exception
                Return
            End Try

            If lListMainTipsManagerCfg(strStationID).MainTipsManagerType <> enumMainTipsManagerType.Confirm Then
                Return
            End If
            If lListStationMainTipsConfirmType.ContainsKey(strStationID) Then
                lListStationMainTipsConfirmType(strStationID) = enumMainTipsConfirmType.Continue
            End If
            
            If lListCurrentMainTipsManagerCfg.ContainsKey(strStationID) Then
                lListCurrentMainTipsManagerCfg(strStationID) = New clsMainTipsManagerCfg(strStationID, lListMainTipsManagerCfg(strStationID).Text, lListMainTipsManagerCfg(strStationID).MainTipsManagerType)
            Else
                lListCurrentMainTipsManagerCfg.Add(strStationID, New clsMainTipsManagerCfg(strStationID, lListMainTipsManagerCfg(strStationID).Text, lListMainTipsManagerCfg(strStationID).MainTipsManagerType))
            End If
            If lListMainTipsManagerCfg.ContainsKey(strStationID) Then
                lListMainTipsManagerCfg.Remove(strStationID)
            End If
            mMainForm.InvokeAction(cTransferHMIException)
        End SyncLock
    End Sub

    Public Sub Auto_Abort(ByVal strStationID As String)
        SyncLock _Object
            Try
                If IsNothing(lListMainTipsManagerCfg(strStationID)) Then
                    Return
                End If
            Catch ex As Exception
                Return
            End Try
            
            If lListMainTipsManagerCfg(strStationID).MainTipsManagerType <> enumMainTipsManagerType.Confirm And lListMainTipsManagerCfg(strStationID).MainTipsManagerType <> enumMainTipsManagerType.ConfirmDisableContine Then
                Return
            End If
            If lListStationMainTipsConfirmType.ContainsKey(strStationID) Then
                lListStationMainTipsConfirmType(strStationID) = enumMainTipsConfirmType.Abort
            End If

            If lListCurrentMainTipsManagerCfg.ContainsKey(strStationID) Then
                lListCurrentMainTipsManagerCfg(strStationID) = New clsMainTipsManagerCfg(strStationID, lListMainTipsManagerCfg(strStationID).Text, lListMainTipsManagerCfg(strStationID).MainTipsManagerType)
            Else
                lListCurrentMainTipsManagerCfg.Add(strStationID, New clsMainTipsManagerCfg(strStationID, lListMainTipsManagerCfg(strStationID).Text, lListMainTipsManagerCfg(strStationID).MainTipsManagerType))
            End If
            If lListMainTipsManagerCfg.ContainsKey(strStationID) Then
                lListMainTipsManagerCfg.Remove(strStationID)
            End If
            mMainForm.InvokeAction(cTransferHMIException)
        End SyncLock
    End Sub

    Public Function GetStationMainTipsManagerCfgFromKey(ByVal strKey As String) As clsMainTipsManagerCfg
        SyncLock _Object
            Try
                Return lListMainTipsManagerCfg(strKey)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentStationMainTipsManagerCfgFromKey(ByVal strKey As String) As clsMainTipsManagerCfg
        SyncLock _Object
            Try
                Return lListCurrentMainTipsManagerCfg(strKey)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainTipsConfirmTypeFromKey(ByVal strKey As String) As enumMainTipsConfirmType
        SyncLock _Object
            Try
                Return lListStationMainTipsConfirmType(strKey)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainTipsCfgTypeFromKey(ByVal strKey As String) As clsMainTipsManagerCfg
        SyncLock _Object
            Try
                If lListMainTipsManagerCfg.ContainsKey(strKey) Then
                    Return lListMainTipsManagerCfg(strKey)
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            mMainForm.InvokeAction(cTransferHMIException)
        End SyncLock
    End Sub

    Public Sub CreateControl()
        '
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
        cTableLayoutPanel_Tips.BackColor = System.Drawing.Color.LightGray
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
        'Button_Continue
        ' 
        Button_Continue.Dock = System.Windows.Forms.DockStyle.Fill
        Button_Continue.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Button_Continue.Location = New System.Drawing.Point(439, 3)
        Button_Continue.Name = "Button_Continue"
        Button_Continue.Text = cLanguageManager.GetTextLine("MainTipsManager", "Button_Continue")
        Button_Continue.UseVisualStyleBackColor = True
        cTableLayoutPanel_Tips.Controls.Add(Button_Continue, 1, 0)


        Button_Abort.Dock = System.Windows.Forms.DockStyle.Fill
        Button_Abort.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Button_Abort.Location = New System.Drawing.Point(532, 3)
        Button_Abort.Name = "Button_Abort"
        Button_Abort.Text = cLanguageManager.GetTextLine("MainTipsManager", "Button_Abort")
        Button_Abort.UseVisualStyleBackColor = True
        cTableLayoutPanel_Tips.Controls.Add(Button_Abort, 2, 0)

        cTableLayoutPanel.Controls.Add(Panel_Tips, 0, cTableLayoutPanel.RowCount - 1)
        cTableLayoutPanel.SetColumnSpan(Panel_Tips, cTableLayoutPanel.ColumnCount)

        cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
        cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
        cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
        OldColor = Label_Tips.ForeColor
        OldBackColor = Label_Tips.BackColor
    End Sub
    Public Sub ShowTips()
        SyncLock _Object
            mMainForm.InvokeAction(Sub()
                                       If cTableLayoutPanel.RowCount = 2 Then
                                           cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 84)
                                           cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
                                       End If
                                       If cTableLayoutPanel.RowCount = 3 Then
                                           cTableLayoutPanel.RowStyles(0) = New RowStyle(System.Windows.Forms.SizeType.Percent, 68)
                                           cTableLayoutPanel.RowStyles(1) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
                                           cTableLayoutPanel.RowStyles(2) = New RowStyle(System.Windows.Forms.SizeType.Percent, 16)
                                       End If
                                   End Sub)
        End SyncLock
    End Sub

    Public Sub HiddenTips()
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
            Label_Tips.Text = ""
            cTableLayoutPanel_Tips.ColumnStyles(0) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 100)
            cTableLayoutPanel_Tips.ColumnStyles(1) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)
            cTableLayoutPanel_Tips.ColumnStyles(2) = New ColumnStyle(System.Windows.Forms.SizeType.Percent, 0)

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
                RemoveHandler Button_Continue.Click, AddressOf Button_Click
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


Public Class clsMainTipsManagerCfg
    Protected strID As String = ""
    Protected strText As String = ""
    Protected iFontSize As Single = 0
    Protected eMainTipsManagerType As enumMainTipsManagerType

    Public Property ID As String
        Set(ByVal value As String)
            strID = value
        End Set
        Get
            Return strID
        End Get
    End Property
    Public Property FontSize As Single
        Set(ByVal value As Single)
            iFontSize = value
        End Set
        Get
            Return iFontSize
        End Get
    End Property
    Public Property Text As String
        Set(ByVal value As String)
            strText = value
        End Set
        Get
            Return strText
        End Get
    End Property

    Public Property MainTipsManagerType As enumMainTipsManagerType
        Set(ByVal value As enumMainTipsManagerType)
            eMainTipsManagerType = value
        End Set
        Get
            Return eMainTipsManagerType
        End Get
    End Property

    Sub New(ByVal strText As String, Optional ByVal eMainTipsManager As enumMainTipsManagerType = enumMainTipsManagerType.Normal)
        Me.strText = strText
        Me.eMainTipsManagerType = eMainTipsManager
    End Sub


    Sub New(ByVal strID As String, ByVal strText As String, Optional ByVal eMainTipsManager As enumMainTipsManagerType = enumMainTipsManagerType.Normal)
        Me.strID = strID
        Me.strText = strText
        Me.eMainTipsManagerType = eMainTipsManager
        Me.iFontSize = iFontSize
    End Sub
    Sub New(ByVal strID As String, ByVal strText As String, ByVal eMainTipsManager As enumMainTipsManagerType, ByVal iFontSize As Single)
        Me.strID = strID
        Me.strText = strText
        Me.eMainTipsManagerType = eMainTipsManager
        Me.iFontSize = iFontSize
    End Sub
End Class

Public Enum enumMainTipsConfirmType
    Normal = 0
    [Continue]
    Abort
End Enum
Public Enum enumMainTipsManagerType
    Normal = 0
    Confirm
    Warning
    Alarm
    Crash
    PLC
    PLCAlarm
    Mode
    UnKown
    NormalAndNoLog
    ConfirmDisableContine
End Enum
