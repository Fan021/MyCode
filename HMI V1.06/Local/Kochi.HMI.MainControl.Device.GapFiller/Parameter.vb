Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent

Public Class Parameter
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private lListPoint As New Dictionary(Of String, clsPointCfg)
    Private lListIO As New Dictionary(Of String, clsMFunctionCfg)
    Private lListWeightUI As New List(Of WeightUI)
    Private cVariantManager As clsVariantManager
    Protected cLanguageManager As clsLanguageManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Private mMainForm As IMainUI
    Private cIniHandler As clsIniHandler
    Private cGapFiller As clsGapFiller
    Private cSystemManager As clsSystemManager
    Private OldStructGapFiller As New StructGapFiller
    Private TempStructGapFiller As New StructGapFiller
    Private cMachineManager As clsMachineManager
    Private cMainTipsManager As clsMainTipsManager
    Private iStep As Integer = 1
    Public Const FormName As String = "GapFillerControlUI"
    Private strDeviceType As String = ""
    Private strDeviceIndex As String = ""
    Private cDeviceCfg As clsDeviceCfg
    Private strResult As String = ""
    Private cPLCAction As clsPLCAction
    Private strTriggerType As String = ""
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private cUserManager As clsUserManager
    Public Property [ReadOnly] As Boolean
        Set(ByVal value As Boolean)
            bReadOnly = value
        End Set
        Get
            Return bReadOnly
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

    Public Property ObjectSource As Object
        Set(ByVal value As Object)
            cGapFiller = value
        End Set
        Get
            Return cGapFiller
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cGapFiller.Name)
            cPLCAction = New clsPLCAction
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
        RemoveHandler HmiTextBox_PotLife.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_BlindTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_BlindNo.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_TimeFilling.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiButtonWithIndicate_BlindShort.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_Clean.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_TimeFilling.Click, AddressOf Button_Click

        GroupBox_BlindShot.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_BlindShot")
        GroupBox_BlindShot.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        Label_PotLife.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_PotLife")
        Label_PotLife.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        Label_BlindTime.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_BlindTime")
        Label_BlindTime.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        Label_BlindNo.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_BlindNo")
        Label_BlindNo.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PotLife.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_BlindTime.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_BlindNo.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_PLC_BlindTime.Font = New System.Drawing.Font("Calibri", iFontSize, FontStyle.Bold)
        Label_PLC_BlindNo.Font = New System.Drawing.Font("Calibri", iFontSize, FontStyle.Bold)
        HmiTextBox_TimeFilling.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        GroupBox_Function.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_Function")
        GroupBox_Function.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        Label_BlindShort.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_BlindShort")
        Label_BlindShort.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        Label_Clean.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Clean")
        Label_Clean.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_BlindShort.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_BlindShort")
        HmiButtonWithIndicate_BlindShort.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_Clean.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_Clean")
        HmiButtonWithIndicate_Clean.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        Label_TimeFilling.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_TimeFilling")
        Label_TimeFilling.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_TimeFilling.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_TimeFilling")
        HmiButtonWithIndicate_TimeFilling.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiTextBox_PotLife.ValueType = GetType(Double)
        HmiTextBox_BlindTime.ValueType = GetType(Double)
        HmiTextBox_BlindNo.ValueType = GetType(Integer)

        HmiTextBox_PotLife.Text = OldStructGapFiller.fdHMIPotLiftTime.ToString("0.00")
        HmiTextBox_BlindTime.Text = OldStructGapFiller.fdHMIBlindShotTime.ToString("0.00")
        HmiTextBox_BlindNo.Text = OldStructGapFiller.fdHMIBlindNo.ToString("0")
        Label_PLC_BlindTime.Text = OldStructGapFiller.fdPLCBlindShotTime.ToString("0.00")
        Label_PLC_BlindNo.Text = OldStructGapFiller.fdPLCBlindNo.ToString("0")
        HmiTextBox_TimeFilling.Text = OldStructGapFiller.fdHMITimeFillingTime.ToString("0")
        HmiTextBox_TimeFilling.ValueType = GetType(Double)
        If bReadOnly Then
            HmiTextBox_PotLife.TextBoxReadOnly = True
            HmiTextBox_BlindTime.TextBoxReadOnly = True
            HmiTextBox_BlindNo.TextBoxReadOnly = True
            HmiTextBox_TimeFilling.TextBoxReadOnly = True
            HmiButtonWithIndicate_BlindShort.Enabled = False
            HmiButtonWithIndicate_Clean.Enabled = False
            HmiButtonWithIndicate_TimeFilling.Enabled = False
        Else
            HmiTextBox_PotLife.TextBoxReadOnly = False
            HmiTextBox_BlindTime.TextBoxReadOnly = False
            HmiTextBox_BlindNo.TextBoxReadOnly = False
            HmiTextBox_TimeFilling.TextBoxReadOnly = False
            HmiButtonWithIndicate_BlindShort.Enabled = True
            HmiButtonWithIndicate_Clean.Enabled = True
            HmiButtonWithIndicate_TimeFilling.Enabled = True
        End If

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Engineer Then
            HmiButtonWithIndicate_BlindShort.Enabled = False
            HmiButtonWithIndicate_Clean.Enabled = False
            HmiButtonWithIndicate_TimeFilling.Enabled = False
        End If

        Dim strDevice As String = cMachineManager.DeviceParameterManager.GetParameterDevice("GapFiller", cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationID, 0)
        If strDevice <> "" Then
            strDeviceType = strDevice.Split("-")(0)
            strDeviceIndex = strDevice.Split("-")(1)
        End If
        AddHandler HmiTextBox_PotLife.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_BlindTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_BlindNo.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_TimeFilling.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiButtonWithIndicate_BlindShort.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_Clean.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_TimeFilling.Click, AddressOf Button_Click
        Return True
    End Function
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButtonWithIndicate_BlindShort"
                Dim oldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIStartBlindShot", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStartBlindShot", Not oldValue)
            Case "HmiButtonWithIndicate_Clean"
                Dim oldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIStartClean", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStartClean", Not oldValue)
            Case "HmiButtonWithIndicate_TimeFilling"
                Dim oldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIStartTimeFilling", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStartTimeFilling", Not oldValue)
        End Select
    End Sub
   
    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

  


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_PotLife"
                If HmiTextBox_PotLife.TextBox.Text = "" Then HmiTextBox_PotLife.TextBox.Text = "0.00"
                CheckHSPotLife()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_BlindTime"
                If HmiTextBox_BlindTime.TextBox.Text = "" Then HmiTextBox_BlindTime.TextBox.Text = "0.00"
                CheckBlindTime()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_BlindNo"
                If HmiTextBox_BlindNo.TextBox.Text = "" Then HmiTextBox_BlindNo.TextBox.Text = "0"
                CheckBlindNo()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_TimeFilling"
                If HmiTextBox_TimeFilling.TextBox.Text = "" Then HmiTextBox_TimeFilling.TextBox.Text = "0.00"
                If HmiTextBox_TimeFilling.TextBox.Text <> "" Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMITimeFillingTime", Single.Parse(HmiTextBox_TimeFilling.TextBox.Text))
                Else
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMITimeFillingTime", Single.Parse(0))
                End If
        End Select
    End Sub

    Private Sub CheckHSPotLife()
        Try
            If HmiTextBox_PotLife.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_PotLife.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "16"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIPotLiftTime", Single.Parse(HmiTextBox_PotLife.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIPotLiftTime", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckBlindNo()
        Try
            If HmiTextBox_BlindNo.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_BlindNo.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "18"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindNo", Int16.Parse(HmiTextBox_BlindNo.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindNo", Int16.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckBlindTime()
        Try
            If HmiTextBox_BlindTime.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_BlindTime.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "17"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindShotTime", Single.Parse(HmiTextBox_BlindTime.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIBlindShotTime", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

   
    Public Sub RefreshUI()
        Try
            Select Case iStep

                Case 1
                    iStep = iStep + 1

                Case 2
                    TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                    iStep = iStep + 1

                Case 3
                    HmiTextBox_PotLife.Text = TempStructGapFiller.fdHMIPotLiftTime.ToString("0.00")
                    HmiTextBox_BlindTime.Text = TempStructGapFiller.fdHMIBlindShotTime.ToString("0.00")
                    HmiTextBox_BlindNo.Text = TempStructGapFiller.fdHMIBlindNo.ToString("0")
                    iStep = iStep + 1

                Case 4
                    TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))
                    If TempStructGapFiller.fdPLCBlindShotTime <> OldStructGapFiller.fdPLCBlindShotTime Then
                        mMainForm.InvokeAction(Sub()
                                                   Label_PLC_BlindTime.Text = TempStructGapFiller.fdPLCBlindShotTime.ToString("0.00")
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdPLCBlindNo <> OldStructGapFiller.fdPLCBlindNo Then
                        mMainForm.InvokeAction(Sub()
                                                   Label_PLC_BlindNo.Text = "[" + TempStructGapFiller.fdPLCBlindNo.ToString("0") + "]"
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIStartBlindShot <> OldStructGapFiller.bulHMIStartBlindShot Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_BlindShort.SetIndicateBackColor(TempStructGapFiller.bulHMIStartBlindShot)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIStartClean <> OldStructGapFiller.bulHMIStartClean Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_Clean.SetIndicateBackColor(TempStructGapFiller.bulHMIStartClean)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulPLCStartBlindShot <> OldStructGapFiller.bulPLCStartBlindShot Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_BlindShort.SetIndicateBackColor(TempStructGapFiller.bulPLCStartBlindShot)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulPLCStartClean <> OldStructGapFiller.bulPLCStartClean Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_Clean.SetIndicateBackColor(TempStructGapFiller.bulPLCStartClean)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIStartTimeFilling <> OldStructGapFiller.bulHMIStartTimeFilling Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_TimeFilling.SetIndicateBackColor(TempStructGapFiller.bulHMIStartTimeFilling)
                                               End Sub)
                    End If

                    OldStructGapFiller.bulHMIStartBlindShot = TempStructGapFiller.bulHMIStartBlindShot
                    OldStructGapFiller.bulHMIStartClean = TempStructGapFiller.bulHMIStartClean
                    OldStructGapFiller.bulPLCStartBlindShot = TempStructGapFiller.bulPLCStartBlindShot
                    OldStructGapFiller.bulPLCStartClean = TempStructGapFiller.bulPLCStartClean
                    OldStructGapFiller.bulHMIStartTimeFilling = TempStructGapFiller.bulHMIStartTimeFilling

                    OldStructGapFiller.fdPLCBlindShotTime = TempStructGapFiller.fdPLCBlindShotTime
                    OldStructGapFiller.fdPLCBlindNo = TempStructGapFiller.fdPLCBlindNo

            End Select
        Catch ex As Exception
            If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try

    End Sub

       Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        '  iStep = 1
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
        '   iStep = 1
        Return True
    End Function

End Class
