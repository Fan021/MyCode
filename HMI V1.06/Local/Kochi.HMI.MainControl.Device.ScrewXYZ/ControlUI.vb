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
    Private mMainForm As Form
    Private TempStructScrewXYZ As StructScrewXYZ
    Public Const FormName As String = "ScrewXYZControlUI"

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
        HmiLabel_X.Label.Text = "X(mm):"
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Y.Label.Text = "Y(mm):"
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Z.Label.Text = "Z(mm):"
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Step.Label.Text = "Step:"
        HmiLabel_Step.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Speed.Label.Text = "Speed:"
        HmiLabel_Speed.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Variant.Label.Text = "Variant:"
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = "Move X:"
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveY.Label.Text = "Move Y:"
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveZ.Label.Text = "Move Z:"
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_AST.Label.Text = "AST:"
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Pro.Label.Text = "Program:"
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Function.Label.Text = "Point:"
        HmiLabel_Function.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Variant.Text = "Load"
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Teach.Text = "Teach"
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Move.Text = "Move"
        HmiButton_Move.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Modify.Text = "Modify"
        HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Screw.Text = "Screw"
        HmiButton_Screw.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Save.Text = "Save"
        HmiButton_Save.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiComboBox_AST.ComboBox.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiComboBox_Variant.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
        Next

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromType(GetType(clsHMIAST))
        HmiComboBox_AST.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_AST.ComboBox.Items.Add(element.Name)
            Next
        End If
        HmiComboBox_Pro.ComboBox.Items.Clear()
        For i = 1 To 15
            HmiComboBox_Pro.ComboBox.Items.Add(i.ToString)
        Next

        HmiDataView_Point.Rows.Clear()
        HmiDataView_Point.Columns.Clear()
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = "ID"
        PostTest_id.Name = "PostTestCmd_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = "Action"
        PostTest_Action.Name = "PostTestCmd_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)

        Dim PostTest_X As New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = "X"
        PostTest_X.Name = "PostTestCmd_X"
        HmiDataView_Point.Columns.Add(PostTest_X)

        Dim PostTest_Y As New DataGridViewTextBoxColumn
        PostTest_Y.HeaderText = "Y"
        PostTest_Y.Name = "PostTestCmd_Y"
        HmiDataView_Point.Columns.Add(PostTest_Y)

        Dim PostTest_Z As New DataGridViewTextBoxColumn
        PostTest_Z.HeaderText = "Z"
        PostTest_Z.Name = "PostTestCmd_Z"
        HmiDataView_Point.Columns.Add(PostTest_Z)

        HmiButton_Variant.Button.Enabled = False
        HmiButton_Teach.Button.Enabled = False
        HmiButton_Move.Button.Enabled = False
        HmiButton_Modify.Button.Enabled = False
        HmiButton_Screw.Button.Enabled = False
        HmiButton_Save.Button.Enabled = False
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Speed.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler RadioButton1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton4.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler ButtonXAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonXDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZDec.MouseUp, AddressOf Button_MouseUp
        AddHandler TrackBar_Speed.Scroll, AddressOf TrackBar_Speed_Scroll
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
                Case "HmiComboBox_AST"
                    HmiButton_Screw.Button.Enabled = IIf(HmiComboBox_AST.ComboBox.SelectedIndex < 0 Or HmiComboBox_Pro.ComboBox.SelectedIndex < 0, False, True)
                Case "HmiComboBox_Pro"
                    HmiButton_Screw.Button.Enabled = IIf(HmiComboBox_AST.ComboBox.SelectedIndex < 0 Or HmiComboBox_Pro.ComboBox.SelectedIndex < 0, False, True)

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_MoveX", "HmiTextBox_MoveY", "HmiTextBox_MoveZ"
                CheckMovePostion()
            Case "HmiTextBox_Speed"
                CheckSpeed()
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
            Case "ButtonXAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXForward", dNewValue)
            Case "ButtonXDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXBackward", dNewValue)
            Case "ButtonYAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYForward", dNewValue)
            Case "ButtonYDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYBackward", dNewValue)
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "ButtonXAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXForward", dNewValue)
            Case "ButtonXDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXBackward", dNewValue)
            Case "ButtonYAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYForward", dNewValue)
            Case "ButtonYDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYBackward", dNewValue)
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
        End Select
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If RadioButton1.Checked Then
            Dim fNewValue As Single = 0.1
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
        End If
        If RadioButton2.Checked Then
            Dim fNewValue As Single = 1.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
        End If
        If RadioButton3.Checked Then
            Dim fNewValue As Single = 10.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
        End If
        If RadioButton4.Checked Then
            Dim fNewValue As Single = 100.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
        End If

    End Sub

    Private Sub TrackBar_Speed_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HmiTextBox_Speed.TextBox.Text = TrackBar_Speed.Value.ToString
    End Sub

    Private Sub CheckMovePostion()
        Try
            If HmiTextBox_MoveX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException("MoveX Point is not Numeric", enumExceptionType.Alarm, ControlUI.FormName))
                End If
            End If
            If HmiTextBox_MoveY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException("MoveY Point is not Numeric", enumExceptionType.Alarm, ControlUI.FormName))
                End If
            End If
            If HmiTextBox_MoveZ.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveZ.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException("MoveZ Point is not Numeric", enumExceptionType.Alarm, ControlUI.FormName))
                End If
            End If
            HmiButton_Modify.Button.Enabled = IIf(HmiTextBox_MoveX.TextBox.Text = "" Or HmiTextBox_MoveY.TextBox.Text = "" Or HmiTextBox_MoveZ.TextBox.Text = "", False, True)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckSpeed()
        Try
            If HmiTextBox_Speed.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Speed.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException("Speed is not Numeric", enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                If CInt(HmiTextBox_Speed.TextBox.Text) <= 0 Or CInt(HmiTextBox_Speed.TextBox.Text) > 100 Then
                    cErrorMessageManager.AddHMIException(New clsHMIException("Speed rang is 0-100", enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                TrackBar_Speed.Value = CInt(HmiTextBox_Speed.TextBox.Text)
                Dim fNewValue As Int16 = CInt(HmiTextBox_Speed.TextBox.Text)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", fNewValue)
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoadVariant()
        Try
            cActionManager.LoadActionCfg(HmiComboBox_Variant.ComboBox.SelectedText)
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
                            cErrorMessageManager.AddHMIException(New clsHMIException("PLC is Nothing, Please Add first", enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException("Device:" + cHMIPLC.Name + " Status:" + cHMIPLC.DeviceState.ToString, enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewXYZ), New StructScrewXYZ)
                        iStep = iStep + 1

                    Case 4
                        TempStructScrewXYZ = cHMIPLC.GetValue(lListInitParameter(0))
                        mMainForm.Invoke(Sub()
                                             HmiTextBox_Speed.TextBox.Text = TempStructScrewXYZ.fdHMISpeed.ToString
                                             RadioButton1.Checked = IIf(TempStructScrewXYZ.fdHMIStep = 0.1, True, False)
                                             RadioButton2.Checked = IIf(TempStructScrewXYZ.fdHMIStep = 1 Or TempStructScrewXYZ.fdHMIStep <= 0, True, False)
                                             RadioButton3.Checked = IIf(TempStructScrewXYZ.fdHMIStep = 10, True, False)
                                             RadioButton4.Checked = IIf(TempStructScrewXYZ.fdHMIStep = 100, True, False)
                                         End Sub)

                        iStep = iStep + 1

                    Case 5
                        TempStructScrewXYZ = cHMIPLC.GetValue(lListInitParameter(0))
                        mMainForm.Invoke(Sub()
                                             Label_X.Text = TempStructScrewXYZ.fdPLCXPosition.ToString("0.00")
                                             Label_Y.Text = TempStructScrewXYZ.fdPLCYPosition.ToString("0.00")
                                             Label_Z.Text = TempStructScrewXYZ.fdPLCZPosition.ToString("0.00")
                                         End Sub)

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(ControlUI.FormName, ex, enumExceptionType.Alarm)
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

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)

        End Set
        Get
            Return Nothing
        End Get
    End Property

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StartRefresh
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.Start()
        cThread.IsBackground = True
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
            If Not IsNothing(cHMIPLC) Then If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function
End Class