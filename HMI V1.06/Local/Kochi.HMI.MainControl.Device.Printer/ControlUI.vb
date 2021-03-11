Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UserDefine

Public Class ControlUI
    Implements IControlUI

    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListActionParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cVariantManager As clsVariantManager
    Private cLocalVariantManager As clsVariantManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Protected lListPrinterCfg As New List(Of clsPrinterCfg)
    Protected lListStation As New Dictionary(Of String, String)
    Protected strErrorMessage As String
    Private mMainForm As IMainUI
    Private cPrinter As clsZebraPrinter
    Private cPrinterFild As New clsPrinterFild
    Private bStartPrint As Boolean = False
    Private cHMILKSN As clsHMILKSN
    Public Const FormName As String = "PrinterControlUI"

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cPrinter = value
        End Set
        Get
            Return cPrinter
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
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cLocalVariantManager = New clsVariantManager
            cLocalVariantManager.Init(cSystemElement)
            cLocalVariantManager.LoadVariantCfg()
            If Not cLocalElement.ContainsKey(clsVariantManager.Name) Then
                cLocalElement.Add(clsVariantManager.Name, cLocalVariantManager)
            End If
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
        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("Printer", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Variant.Text = cLanguageManager.GetUserTextLine("Printer", "HmiButton_Variant")
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Printer.Text = cLanguageManager.GetUserTextLine("Printer", "HmiButton_Printer")
        HmiButton_Printer.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Variant.Button.Enabled = False
        HmiButton_Printer.Button.Enabled = False

        Dim iCnt As Integer = 0
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
            If cVariantManager.CurrentVariantCfg.Variant = element.Variant Then
                HmiComboBox_Variant.ComboBox.SelectedIndex = iCnt
                cLocalVariantManager.ChangeVariant(element.Variant)
            End If
            iCnt = iCnt + 1

        Next

        HmiDataView_Point.Rows.Clear()
        HmiDataView_Point.Columns.Clear()
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("Printer", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = cLanguageManager.GetUserTextLine("Printer", "Action")
        PostTest_Action.Name = "PostTest_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)


        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("Printer", "LKSN")
        PostTest_Program.Name = "PostTest_LKSN"
        HmiDataView_Point.Columns.Add(PostTest_Program)

        Dim PostTest_FormatFile As New DataGridViewTextBoxColumn
        PostTest_FormatFile.HeaderText = cLanguageManager.GetUserTextLine("Printer", "FormatFile")
        PostTest_FormatFile.Name = "PostTest_FormatFile"
        HmiDataView_Point.Columns.Add(PostTest_FormatFile)

        Dim PostTest_PrintFile As New DataGridViewTextBoxColumn
        PostTest_PrintFile.HeaderText = cLanguageManager.GetUserTextLine("Printer", "PrintFile")
        PostTest_PrintFile.Name = "PostTest_PrintFile"
        HmiDataView_Point.Columns.Add(PostTest_PrintFile)


        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If


        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Printer.Button.Click, AddressOf Button_Click

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
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoadAction(ByVal strVariant As String)
        Try
            Dim iCnt As Integer = 1
            Dim i As Integer = 0
            Dim j As Integer = 0
            HmiDataView_Point.Rows.Clear()
            lListStation.Clear()
            cActionManager.LoadActionCfg(strVariant)
            cLocalVariantManager.ChangeVariant(strVariant)
            For Each element As String In cActionManager.GetActionListKey()
                For Each elementAction In cActionManager.GetActionCfgFromKey(element).GetStepListKey
                    i = 0
                    Dim lListMainStepCfg As List(Of clsMainStepCfg) = cActionManager.GetMainStepCfgList(element, elementAction)
                    For Each elementMainStepCfg As clsMainStepCfg In lListMainStepCfg
                        j = 0
                        For Each elementSubKey As String In elementMainStepCfg.GetSubStepListKey
                            Dim lListParameter() As String = clsParameter.ToList(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter)).ToArray
                            lListActionParameter = clsParameter.ToList(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter))
                            If lListParameter.Length < 1 Then
                                j = j + 1
                                Continue For
                            End If
                            If elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "ManualStationPrint" Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cPrinter.Name).StationIndex.ToString <> lListParameter(0) Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cPrinter.Name).StationID.ToString <> element Then
                                j = j + 1
                                Continue For
                            End If

                            Dim lListViewParameter As New List(Of String)
                            lListViewParameter.Add(iCnt.ToString)
                            lListViewParameter.Add(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Name))
                            lListViewParameter.Add(lListParameter(1))
                            lListViewParameter.Add(lListParameter(2))
                            lListViewParameter.Add(lListParameter(3))
                            HmiDataView_Point.Rows.Add(lListViewParameter.ToArray)
                            lListStation.Add(iCnt.ToString, element)
                            iCnt = iCnt + 1
                            j = j + 1
                        Next
                        i = i + 1
                    Next
                Next
            Next
            If HmiDataView_Point.Rows.Count > 0 Then
                lListActionParameter.Clear()
                lListActionParameter.Add(HmiDataView_Point.Rows(0).Cells(0).Value)
                lListActionParameter.Add(HmiDataView_Point.Rows(0).Cells(2).Value)
                lListActionParameter.Add(HmiDataView_Point.Rows(0).Cells(3).Value)
                lListActionParameter.Add(HmiDataView_Point.Rows(0).Cells(4).Value)
                HmiButton_Printer.Button.Enabled = True
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Point.CellClick
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiButton_Printer.Button.Enabled = True
            lListActionParameter.Clear()
            lListActionParameter.Add(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value)
            lListActionParameter.Add(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value)
            lListActionParameter.Add(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value)
            lListActionParameter.Add(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value)
        End If
    End Sub


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Variant"
                LoadVariant()
            Case "HmiButton_Printer"
                If Not IsNothing(HmiDataView_Point.CurrentRow) AndAlso HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
                    bStartPrint = True
                End If
        End Select
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
                        If bStartPrint Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Printer.Button.Enabled = False
                                                   End Sub)
                            iStep = iStep + 1
                        End If

                    Case 2
                        If Not cPrinter.Running Then
                            cPrinter.Running = True
                            iStep = iStep + 1
                        End If


                    Case 3
                        cPrinter.LoadFormatFile(lListActionParameter(2))
                        iStep = iStep + 1

                    Case 4
                        cPrinter.LoadPrintFile(lListActionParameter(3))
                        iStep = iStep + 1

                    Case 5
                        cHMILKSN = cDeviceManager.GetDeviceFromTypeAndStationIndex(lListStation(lListActionParameter(0)), lListActionParameter(1), GetType(clsHMILKSN)).Source
                        If Not cPrinterFild.GetPrinterFild(cPrinter.Name, cLocalElement, cSystemElement, cHMILKSN, lListPrinterCfg, strErrorMessage) Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Printer.Button.Enabled = True
                                                   End Sub)
                            Throw New clsHMIException(strErrorMessage, enumExceptionType.Alarm)
                            cPrinter.Running = False
                            bStartPrint = False
                            iStep = 1
                        Else
                            iStep = iStep + 1
                        End If

                    Case 6
                        For Each element As clsPrinterCfg In lListPrinterCfg
                            cPrinter.SetField(lListActionParameter(3), "^FN" & element.iIndex.ToString & "^FD", "^FS", element.strValue)
                        Next
                        iStep = iStep + 1

                    Case 7
                        cPrinter.PrintLabel(lListActionParameter(3))
                        iStep = iStep + 1

                    Case 8
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Printer.Button.Enabled = True
                                               End Sub)
                        cPrinter.Running = False
                        bStartPrint = False
                        iStep = 1

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try

        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If cLocalElement.ContainsKey(clsVariantManager.Name) Then
            cLocalElement.Remove(clsVariantManager.Name)
        End If
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
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Return True
    End Function
End Class
