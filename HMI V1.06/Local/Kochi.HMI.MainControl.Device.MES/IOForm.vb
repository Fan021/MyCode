Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System.Reflection

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
    Protected cMES As clsMES
    Protected bReadOnly As Boolean = False
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private iFontSize As Integer = 10
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    Private cMachineStatusManager As clsMachineStatusManager
    Private cProductionMESData As New clsProductionMESData
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
    Public Property TOR As clsMES
        Set(ByVal value As clsMES)
            cMES = value
        End Set
        Get
            Return cMES
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cMES.Name)
        cProductionMESData = New clsProductionMESData
        cProductionMESData.Init(cSystemElement)
        GetPageMode()
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer And Not [ReadOnly] Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Public Function InitControlText() As Boolean


        HmiLabel_ErrorMessage.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_ErrorMessage.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_ErrorMessage")
        HmiLabel_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_SFC.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_SFC")
        Button_Abort.Font = New System.Drawing.Font("Calibri", iFontSize)
        Button_Start.Font = New System.Drawing.Font("Calibri", iFontSize)
        Button_Complete.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Variant.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        Button_Abort.Text = cLanguageManager.GetUserTextLine("MES", "Button_Abort")
        HmiTextBox_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Result.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_ErrorMessage.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        Button_Start.Text = cLanguageManager.GetUserTextLine("MES", "Button_Start")
        Button_Complete.Text = cLanguageManager.GetUserTextLine("MES", "Button_Complete")
        Button_Abort.Text = cLanguageManager.GetUserTextLine("MES", "Button_Abort")
        HmiTextBox_Result.ReadOnly = True
        Dim c As clsMachineStatusCfg = cMachineStatusManager.GetMachineStatusCfgFromName(cDeviceCfg.StationID)
        If Not IsNothing(c) Then
            HmiTextBox_SFC.TextBox.Text = cMachineStatusManager.GetMachineStatusCfgFromName(cDeviceCfg.StationID).VariantCfg.SFC
            HmiTextBox_Variant.TextBox.Text = cMachineStatusManager.GetMachineStatusCfgFromName(cDeviceCfg.StationID).VariantCfg.Variant
        End If
        If ePageMode = enumPageMode.Debug Then
            Button_Abort.Enabled = False
        End If
        HmiTextBox_ErrorMessage.TextBox.Text = "Abort"
        AddHandler Button_Abort.Click, AddressOf Button_Click
        AddHandler Button_Start.Click, AddressOf Button_Click
        AddHandler Button_Complete.Click, AddressOf Button_Click
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If HmiTextBox_SFC.TextBox.Text = "" Then Return
        sender.Enabled = False
        Select Case sender.name
            Case "Button_Abort"
                Dim mErrorMessage As String = HmiTextBox_ErrorMessage.TextBox.Text
                If mErrorMessage = "" Then
                    mErrorMessage = "Abort"
                End If
                HmiTextBox_Result.AppendText("Abort" + vbCrLf)
                Dim lListNcData As New List(Of clsNcDataCfg)
                Dim cNcDataCfg As New clsNcDataCfg
                Dim strResult As String = String.Empty
                cNcDataCfg.NcComment = mErrorMessage
                lListNcData.Add(cNcDataCfg)
                Dim mResult As String = ""
                cMES.logNonConformance(HmiTextBox_SFC.TextBox.Text, lListNcData, mResult)
                HmiTextBox_Result.AppendText(mResult + vbCrLf)
                cMES.Complete(HmiTextBox_SFC.TextBox.Text, mResult)
                HmiTextBox_Result.AppendText(mResult + vbCrLf)

                If cDeviceCfg.StationID > 1 Then
                    cProductionMESData.InSertData(HmiTextBox_Variant.TextBox.Text, HmiTextBox_SFC.TextBox.Text, (CInt(cDeviceCfg.StationID) - 1).ToString, "Abort")
                End If
                cProductionMESData.InSertData(HmiTextBox_Variant.TextBox.Text, HmiTextBox_SFC.TextBox.Text, cDeviceCfg.StationID.ToString, "Abort")
                Dim cHMIProcessConfig As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cDeviceCfg.StationID.ToString, "1", GetType(Kochi.HMI.MainControl.Device.ProcessControl.clsProcessControl))

                If Not IsNothing(cHMIProcessConfig) Then
                    Dim cHMIProcessControl As Kochi.HMI.MainControl.Device.ProcessControl.clsProcessControl = cHMIProcessConfig.Source
                    mResult = ""
                    cHMIProcessControl.logNonConformance(HmiTextBox_SFC.TextBox.Text, mResult)
                    HmiTextBox_Result.AppendText(mResult + vbCrLf)
                End If

                Dim cHMICellConfig As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cDeviceCfg.StationID.ToString, "1", GetType(Kochi.HMI.MainControl.Device.CellControl.clsCellControl))

                If Not IsNothing(cHMICellConfig) Then
                    Dim cHMICellControl As Kochi.HMI.MainControl.Device.CellControl.clsCellControl = cHMICellConfig.Source
                    mResult = ""
                    cHMICellControl.logNonConformance(HmiTextBox_SFC.TextBox.Text, mResult)
                    HmiTextBox_Result.AppendText(mResult + vbCrLf)
                End If
            Case "Button_Start"
                HmiTextBox_Result.AppendText("Start" + vbCrLf)
                Dim mResult As String = ""
                cMES.Start(HmiTextBox_SFC.TextBox.Text, mResult)
                HmiTextBox_Result.AppendText(mResult + vbCrLf)

            Case "Button_Complete"
                HmiTextBox_Result.AppendText("Complete" + vbCrLf)
                Dim mResult As String = ""
                cMES.Complete(HmiTextBox_SFC.TextBox.Text, mResult)
                HmiTextBox_Result.AppendText(mResult + vbCrLf)
        End Select
        sender.Enabled = True
    End Sub


    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter

        Return True
    End Function

    Private Sub RefreshUI()
        Return
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count >= 1 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

 

End Class
