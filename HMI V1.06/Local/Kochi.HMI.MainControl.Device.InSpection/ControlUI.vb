Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent

Public Class ControlUI
    Implements IControlUI

    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cVariantManager As clsVariantManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Private mMainForm As IMainUI
    Private cInSpection As clsInSpection
    Private OldStructInSpection As New StructInSpection
    Private TempStructInSpection As New StructInSpection
    Public Const FormName As String = "InSpectionControlUI"
    Private cProgramForm As ProgramForm
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cInSpection = value
        End Set
        Get
            Return cInSpection
        End Get
    End Property
    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cProgramForm = New ProgramForm
            cProgramForm.ObjectSource = cInSpection
            cProgramForm.Init(cLocalElement, cSystemElement)
            InitForm()
            InitControlText()
            AddHandler cProgramForm.ValueChanged, AddressOf ValueChanged
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Private Sub ValueChanged()
        HmiComboBox_Pro.ComboBox.Items.Clear()
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cInSpection.Name)
        For iCnt = 0 To 32
            Dim strTemp As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Program" + iCnt.ToString, "Name")
            If strTemp = "" Then
                HmiComboBox_Pro.ComboBox.Items.Add(iCnt.ToString)
            Else
                HmiComboBox_Pro.ComboBox.Items.Add(iCnt.ToString + "-" + strTemp)
            End If

        Next
    End Sub
    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Result.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Result")
        HmiLabel_Result.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_Variant.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiButton_Variant")
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_DoAction.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiButton_DoAction")
        HmiButton_DoAction.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.SelectedIndex = -1
        TabControl_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage1.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage2.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage1.Text = cLanguageManager.GetUserTextLine("InSpection", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("InSpection", "TabPage2")
        TabPage2.Controls.Add(cProgramForm.UI)
        Dim iCnt As Integer = 0
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
            If cVariantManager.CurrentVariantCfg.Variant = element.Variant Then
                HmiComboBox_Variant.ComboBox.SelectedIndex = iCnt
            End If
            iCnt = iCnt + 1

        Next

        iCnt = 1
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cInSpection.Name)
        For iCnt = 0 To 32
            Dim strTemp As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Program" + iCnt.ToString, "Name")
            If strTemp = "" Then
                HmiComboBox_Pro.ComboBox.Items.Add(iCnt.ToString)
            Else
                HmiComboBox_Pro.ComboBox.Items.Add(iCnt.ToString + "-" + strTemp)
            End If

        Next

        HmiDataView_Point.Rows.Clear()
        HmiDataView_Point.Columns.Clear()
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "Action")
        PostTest_Action.Name = "PostTest_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)


        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "Program")
        PostTest_Program.Name = "PostTest_Program"
        HmiDataView_Point.Columns.Add(PostTest_Program)


        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If

        HmiButton_Variant.Button.Enabled = False
        HmiButton_DoAction.Button.Enabled = False
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_DoAction.Button.Click, AddressOf Button_Click
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_DoAction.Button.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_DoAction.Button.MouseUp, AddressOf Button_MouseUp

        Return True
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiComboBox_Variant"
                    HmiButton_Variant.Button.Enabled = True
                Case "HmiComboBox_Pro"
                    CheckPro()
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(HmiComboBox_Pro.ComboBox.SelectedIndex + 1))

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoadAction(ByVal strVariant As String)
        Try
            HmiComboBox_Pro.ComboBox.SelectedIndex = -1
            Dim cLocalElement As New Dictionary(Of String, Object)
            Dim iCnt As Integer = 1
            Dim i As Integer = 0
            Dim j As Integer = 0
            HmiDataView_Point.Rows.Clear()
            cActionManager.LoadActionCfg(strVariant)
            Dim cVariantCfg As New clsVariantCfg
            cVariantCfg.Variant = strVariant
            If Not cLocalElement.ContainsKey(clsActionManager.Name) Then
                cLocalElement.Add(clsActionManager.Name, Me)
            End If
            If Not cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
            End If
            For Each element As String In cActionManager.GetActionListKey()
                For Each elementAction In cActionManager.GetActionCfgFromKey(element).GetStepListKey
                    i = 0
                    Dim lListMainStepCfg As List(Of clsMainStepCfg) = cActionManager.GetMainStepCfgList(element, elementAction)
                    For Each elementMainStepCfg As clsMainStepCfg In lListMainStepCfg
                        j = 0
                        For Each elementSubKey As String In elementMainStepCfg.GetSubStepListKey
                            Dim lListParameter() As String = clsParameter.ToList(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement)).ToArray
                            If lListParameter.Length < 1 Then
                                j = j + 1
                                Continue For
                            End If
                            If elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationInSpection" And elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "ManualStationInSpection" Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cInSpection.Name).StationIndex.ToString <> lListParameter(0) Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cInSpection.Name).StationID.ToString <> element Then
                                j = j + 1
                                Continue For
                            End If
                            HmiDataView_Point.Rows.Add(iCnt.ToString,
                                                       elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Name),
                                                       lListParameter(3)
                                                       )
                            iCnt = iCnt + 1
                            j = j + 1
                        Next
                        i = i + 1
                    Next
                Next
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            If HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(1).Value <> "" Then
                HmiComboBox_Pro.ComboBox.SelectedIndex = CInt(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(1).Value) - 1
            Else
                HmiComboBox_Pro.ComboBox.SelectedIndex = -1
            End If

        End If
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Pro"
                CheckPro()
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(sender.text))
        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Variant"
                LoadVariant()
        End Select
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_DoAction"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIDoAciton", dNewValue)
        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_DoAction"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIDoAciton", dNewValue)
        End Select
    End Sub

    Private Sub CheckPro()
        Try
            'If HmiComboBox_Pro.ComboBox.Text <> "" Then
            '    ' If Not IsNumeric(HmiComboBox_Pro.ComboBox.Text) Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "5"), enumExceptionType.Alarm, ControlUI.FormName))
            '    'End If
            'End If
            HmiButton_DoAction.Button.Enabled = True

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub LoadVariant()
        Try
            cActionManager.LoadActionCfg(HmiComboBox_Variant.ComboBox.Text)
            LoadAction(HmiComboBox_Variant.ComboBox.Text)
            HmiButton_Variant.Button.Enabled = False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter

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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructInSpection), New StructInSpection)
                        iStep = iStep + 1

                    Case 4
                        TempStructInSpection = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructInSpection))
                        mMainForm.InvokeAction(Sub()
                                                   HmiComboBox_Pro.ComboBox.Text = TempStructInSpection.fdHMIProg
                                               End Sub)
                        iStep = iStep + 1
                    Case 5
                        TempStructInSpection = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructInSpection.bulPLCResult <> OldStructInSpection.bulPLCResult Then
                            mMainForm.InvokeAction(Sub()
                                                       Panel_Indicate_Result.SetIndicateBackColor(TempStructInSpection.bulPLCResult)
                                                   End Sub)
                        End If
                        OldStructInSpection.bulPLCResult = TempStructInSpection.bulPLCResult
                        iStep = 5

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
       
        Me.Dispose()
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
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

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempInSpection As New StructInSpection
        If lListInitParameter.Count >= 1 Then cHMIPLC.WriteAny(lListInitParameter(0), TempInSpection)
        Return True
    End Function
End Class
