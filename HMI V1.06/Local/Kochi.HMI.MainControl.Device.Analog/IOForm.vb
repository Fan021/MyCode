Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing

Public Class IOForm
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Public Const FormName As String = "IOForm"
    Protected cLanguageManager As clsLanguageManager
    Protected cIO As clsAnalog
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    Private _Object As New Object
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private lListIO As New Dictionary(Of String, clsParameterCfg)
    Private cVariantManager As clsVariantManager
    Private OldAnalogParameter(19) As StructAnalogParameter
    Private AnalogParameter(19) As StructAnalogParameter
    Private bEdit As Boolean = False
    Private HmiButton_Edit As Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)

    Public Property [ReadOnly] As Boolean
        Set(ByVal value As Boolean)
            bReadOnly = value
        End Set
        Get
            Return bReadOnly
        End Get
    End Property

    Public Property IO As clsAnalog
        Set(ByVal value As clsAnalog)
            cIO = value
        End Set
        Get
            Return cIO
        End Get
    End Property
    Public Property FontSize As Integer
        Set(ByVal value As Integer)
            iFontSize = value
        End Set
        Get
            Return iFontSize
        End Get
    End Property
    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.cLocalElement = cLocalElement
        Me.cSystemElement = cSystemElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cIO.Name)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        GetPageMode()
        InitForm()
        InitControlText()
        CreateParameterButton()


        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        HmiTableLayoutPanel_UI.RowCount = 20
        HmiTableLayoutPanel_UI.Dock = DockStyle.Fill
        HmiTableLayoutPanel_UI.RowStyles.Clear()
        HmiTableLayoutPanel_UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))

        For j = 1 To 10
            HmiTableLayoutPanel_UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
            HmiTableLayoutPanel_UI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Next
        HmiTableLayoutPanel_UI.Refresh()

        For i = 0 To 19
            OldAnalogParameter(i) = New StructAnalogParameter
            AnalogParameter(i) = New StructAnalogParameter
        Next
        Return True
    End Function
    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Public Sub CreateParameterButton()
        SyncLock _Object
            Dim mTempValue As String = String.Empty
            HmiButton_Edit = New Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
            HmiButton_Edit.BackColor = System.Drawing.SystemColors.Control
            HmiButton_Edit.Dock = System.Windows.Forms.DockStyle.Fill
            HmiButton_Edit.Location = New System.Drawing.Point(3, 3)
            HmiButton_Edit.Name = "HmiButton_Edit"
            HmiButton_Edit.Size = New System.Drawing.Size(43, 524)
            HmiButton_Edit.TabIndex = 58
            HmiButton_Edit.Font = New System.Drawing.Font("Calibri", iFontSize)
            HmiButton_Edit.Text = cLanguageManager.GetUserTextLine("Analog", "HmiButton_Edit")
            HmiButton_Edit.UseVisualStyleBackColor = True
            If ePageMode = enumPageMode.Edit Then
                HmiButton_Edit.Enabled = True
            Else
                HmiButton_Edit.Enabled = False
            End If
            AddHandler HmiButton_Edit.Click, AddressOf Button_Click
            HmiTableLayoutPanel_UI.Controls.Add(HmiButton_Edit, 5, 1)

            For j = 0 To 19
                Dim cIOCfg As New clsParameterCfg
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Text")
                cIOCfg.strText = mTempValue
                If cIOCfg.strText = "" Then
                    cIOCfg.strText = "Parameter:" + (j + 1).ToString
                End If

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Index")
                If mTempValue = "" Then
                    cIOCfg.iIndex = j
                Else
                    cIOCfg.iIndex = CInt(mTempValue)
                End If

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "ReadOnly")
                cIOCfg.bReadOnly = IIf(mTempValue = "True", True, False)

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "VariantValue")
                cIOCfg.bVariantValue = IIf(mTempValue = "True", True, False)

                Dim cLabel As New Label
                cLabel.Dock = System.Windows.Forms.DockStyle.Fill
                cLabel.Font = New System.Drawing.Font("Calibri", iFontSize)
                cLabel.Name = "Parameter" + j.ToString
                cLabel.BackColor = Color.White
                cLabel.TextAlign = ContentAlignment.MiddleRight
                cLabel.Text = cIOCfg.strText
                cLabel.Size = New System.Drawing.Size(223, 32)

                cLabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
                If j <= 9 Then
                    HmiTableLayoutPanel_UI.Controls.Add(cLabel, 1, j * 2 + 1)
                Else
                    HmiTableLayoutPanel_UI.Controls.Add(cLabel, 3, j * 2 + 1 - 20)
                End If

                AddHandler cLabel.MouseDown, AddressOf Button_Down
                cIOCfg.cLabel = cLabel

                Dim cText As New HMITextBox
                cText.Dock = System.Windows.Forms.DockStyle.Fill
                cText.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
                cText.Name = "Parameter" + j.ToString
                ' cText.Size = New System.Drawing.Size(223, 32)
                cText.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
                If j <= 9 Then
                    HmiTableLayoutPanel_UI.Controls.Add(cText, 2, j * 2 + 1)
                Else
                    HmiTableLayoutPanel_UI.Controls.Add(cText, 4, j * 2 + 1 - 20)
                End If

                cText.Text = ""
                cIOCfg.cText = cText
                cText.TextBoxReadOnly = True

                AddHandler cText.TextBox.TextChanged, AddressOf TextBox_TextChanged
                cIOCfg.ID = "Parameter" + j.ToString
                lListIO.Add("Parameter" + j.ToString, cIOCfg)
            Next
        End SyncLock
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        bEdit = Not bEdit
        HmiButton_Edit.SetIndicateBackColor(bEdit)
        EnableEdit(bEdit)
        If bEdit Then
            StopRefresh(Me.cLocalElement, Me.cSystemElement)
        Else
            StartRefresh(Me.cLocalElement, Me.cSystemElement)
        End If

    End Sub

    Private Sub WriteValue()
        SyncLock _Object
            For Each cIOCfg As clsParameterCfg In lListIO.Values
                Dim strValue As String = ""
                If Not cIOCfg.bReadOnly Then
                    If cIOCfg.bVariantValue Then
                        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                            strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cVariantManager.CurrentVariantCfg.Variant, cIOCfg.iIndex.ToString)
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                        Else
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                        End If
                    Else
                        strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Value")
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                    End If
                End If
                '   mMainForm.InvokeAction(Sub()
                '         cIOCfg.cText.TextBox.Text = strValue
                '     End Sub)

            Next
        End SyncLock
    End Sub

    Private Sub EnableEdit(ByVal bEdit As Boolean)
        SyncLock _Object
            For Each element As clsParameterCfg In lListIO.Values
                If bEdit Then
                    If Not element.bReadOnly Then
                        element.cText.TextBoxReadOnly = Not bEdit
                    Else
                        element.cText.TextBoxReadOnly = True
                    End If
                Else
                    element.cText.TextBoxReadOnly = True
                End If

            Next
        End SyncLock
    End Sub
    Private Sub Button_Down(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            If Not bEdit Then Return
            Dim cIOParameter As New ParameterForm
            cIOParameter.IO = Me.IO
            cIOParameter.Init(cLocalElement, cSystemElement)
            Dim cIOCfg As clsParameterCfg = lListIO(sender.name)
            cIOParameter.TextFont = HmiButton_Edit.Font
            cIOParameter.Index = cIOCfg.iIndex.ToString
            cIOParameter.TextValue = cIOCfg.strText
            cIOParameter.isReadOnly = cIOCfg.bReadOnly
            cIOParameter.isVariantChange = cIOCfg.bVariantValue

            If cIOParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                cIOCfg.strText = cIOParameter.TextValue
                cIOCfg.bReadOnly = cIOParameter.isReadOnly
                cIOCfg.bVariantValue = cIOParameter.isVariantChange
                cIOCfg.cLabel.Text = cIOCfg.strText
                cIOCfg.cText.TextBoxReadOnly = cIOCfg.bReadOnly
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Text", cIOCfg.strText.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "ReadOnly", cIOCfg.bReadOnly.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "VariantValue", cIOCfg.bVariantValue.ToString)

                Dim strValue As String = ""
                If Not cIOCfg.bReadOnly Then
                    If cIOCfg.bVariantValue Then
                        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                            strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cVariantManager.CurrentVariantCfg.Variant, cIOCfg.iIndex.ToString)
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                        Else
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                        End If
                    Else
                        strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Value")
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                    End If

                    For i = 0 To cIOParameter.ListView_Variant.Rows.Count - 1
                        cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOParameter.ListView_Variant.Rows(i).Cells(1).Value, cIOCfg.iIndex.ToString, cIOParameter.ListView_Variant.Rows(i).Cells(2).Value)
                    Next
                End If
                mMainForm.InvokeAction(Sub()
                                           cIOCfg.cText.TextBox.Text = strValue
                                       End Sub)
            End If

        End If
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If Not bEdit Then Return
            Dim cIOCfg As clsParameterCfg = lListIO(sender.name)
            If cIOCfg.bReadOnly Then Return
            If cIOCfg.bVariantValue Then
                If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cVariantManager.CurrentVariantCfg.Variant, cIOCfg.iIndex.ToString, sender.text)
                End If
            Else
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Value", sender.text)
            End If

            cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", sender.text, New Integer() {sender.text.Length})

        End SyncLock
    End Sub

    Public Function InitControlText() As Boolean
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
                        For i = 0 To 19
                            OldAnalogParameter(i) = New StructAnalogParameter
                            AnalogParameter(i) = New StructAnalogParameter
                        Next
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
                        WriteValue()
                        Dim cDefaultValue() As StructAnalogParameter = Enumerable.Repeat(New StructAnalogParameter, 20).ToArray()
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructAnalogParameter()), cDefaultValue, New Integer() {20})
                        iStep = iStep + 1

                    Case 3
                        AnalogParameter = cHMIPLC.GetValue(lListInitParameter(0))
                        For i = 0 To 19
                            If OldAnalogParameter(i).strParameter <> AnalogParameter(i).strParameter Then
                                Dim strTemp As String = AnalogParameter(i).strParameter.ToString
                                Dim cParameterCfg As clsParameterCfg = lListIO("Parameter" + i.ToString)
                                mMainForm.InvokeAction(Sub()
                                                           cParameterCfg.cText.TextBox.Text = strTemp
                                                       End Sub
                                                       )

                                OldAnalogParameter(i).strParameter = AnalogParameter(i).strParameter
                            End If
                        Next

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, IOForm.FormName))
            End Try


        End While
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
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

End Class

