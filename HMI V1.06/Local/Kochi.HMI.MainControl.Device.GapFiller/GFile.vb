Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent
Imports TwinCAT.Ads

Public Class GFile
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
    Public Const FormName As String = "GapFillerControlUI"
    Private iStep As Integer = 0
    Private cDeviceCfg As clsDeviceCfg
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private lListR As New Dictionary(Of String, clsRCfg)
    Private _Object As New Object
    Private lListROldText As New Dictionary(Of String, String)
    Private lListGFile As New Dictionary(Of String, clsGFilePathCfg)
    Protected lListActionStep As New Dictionary(Of String, clsActionPointCfg)
    Protected strLastValue As String = String.Empty
    Protected strLastDisplayValue As String = String.Empty
    Private strLastTemp As String = String.Empty
    Private strNowLine1 As String = String.Empty
    Private strNowLine2 As String = String.Empty
    Private strNowLine3 As String = String.Empty
    Private strLastLine1 As String = String.Empty
    Private strLastLine2 As String = String.Empty
    Private strLastLine3 As String = String.Empty
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
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cGapFiller.Name)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
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
        HmiLabel2_X.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_X2")
        HmiLabel2_X.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel2_Y.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Y2")
        HmiLabel2_Y.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel2_Z.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Z2")
        HmiLabel2_Z.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label2_X.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        Label2_Y.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        Label2_Z.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiLabel_MFunction.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_MFunction")
        HmiLabel_MFunction.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButton_Start.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Start")
        HmiButton_Start.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_Stop.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Stop")
        HmiButton_Stop.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_Build.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Build")
        HmiButton_Build.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_Release.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Release")
        HmiButton_Release.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_Load.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Load")
        HmiButton_Load.Font = New System.Drawing.Font("Calibri", iFontSize - 1)

        HmiButton_HS_Confirm.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_HS_Confirm")
        HmiButton_HS_Confirm.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiButton_Reset.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Reset")
        HmiButton_Reset.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        HmiLabel_HS_Confirm.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_HS_Confirm")
        HmiLabel_HS_Confirm.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Reset.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Reset")
        HmiLabel_Reset.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_HS_Confirm.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Reset.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        TabControl_GFile.Font = New System.Drawing.Font("Calibri", iFontSize)

        TabPage1_GFile.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage1_GFile")
        TabPage2_GFile.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage2_GFile")
        TabPage1_GFile.Font = New System.Drawing.Font("Calibri", iFontSize)
        TabPage2_GFile.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Name1.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Name1")
        HmiLabel_Name1.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_GFileName1.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_GFileName1")
        HmiLabel_GFileName1.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_GFileLinkChoose1.Button.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_GFileLinkChoose1")
        HmiButton_GFileLinkChoose1.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_GFileLinkModify1.Button.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_GFileLinkModify1")
        HmiButton_GFileLinkModify1.Button.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Name1.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_GFileLink1.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_GFileName2.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_GFileName2")
        HmiLabel_GFileName2.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_GFileLink2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_LoadVariant.Button.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_LoadVariant")
        HmiButton_LoadVariant.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_GFileLinkChoose2.Button.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_GFileLinkChoose2")
        HmiButton_GFileLinkChoose2.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_GFileLinkModify2.Button.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_GFileLinkModify2")
        HmiButton_GFileLinkModify2.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_GFileLink2.TextBoxReadOnly = True
        HmiTextBox_GFileLink1.TextBoxReadOnly = True
        HmiTextBox_Reset.TextBoxReadOnly = True
        HmiTextBox_HS_Confirm.TextBoxReadOnly = True

        HmiLabel_NC_ErrorCode.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_NC_ErrorCode")
        HmiLabel_NC_ErrorCode.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 3)

        HmiTextBox_NCErrorCode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_NCErrorCode.TextBoxReadOnly = True

        HmiLabel_GFile.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_GFile")
        HmiLabel_GFile.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 3)

        HmiTextBox_GFile.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_GFile.TextBoxReadOnly = True
        HmiButtonWithIndicate_RRead.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_RRead")
        HmiButtonWithIndicate_RRead.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButtonWithIndicate_RWrite.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_RWrite")
        HmiButtonWithIndicate_RWrite.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_NCErrorCode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_GFile.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        Label2_X.Text = OldStructGapFiller.fdPLCXPosition.ToString("0.00")
        Label2_Y.Text = OldStructGapFiller.fdPLCYPosition.ToString("0.00")
        Label2_Z.Text = OldStructGapFiller.fdPLCZPosition.ToString("0.00")
        HmiTextBox_HS_Confirm.Text = OldStructGapFiller.fdHMIHshakeNo.ToString("0")
        HmiTextBox_Reset.Text = OldStructGapFiller.fdPLCErrorCode.ToString("0")
        HmiButton_Start.Enabled = False
        HmiButton_Stop.Enabled = False

        TextBox_Read.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        TextBox_Read.ReadOnly = True
        If bReadOnly Then
            HmiButton_Start.Enabled = False
            HmiButton_Stop.Enabled = False
            Button_Open.Enabled = False
            Button_Save.Enabled = False
            Button_New.Enabled = False
            HmiButton_MotorEnable.Enabled = False
            HmiButton_Build.Enabled = False
            HmiButton_Release.Enabled = False
            HmiButton_Load.Enabled = False
            HmiButton_HS_Confirm.Enabled = False
            HmiButton_Reset.Enabled = False
            HmiTextBox_HS_Confirm.TextBoxReadOnly = True
            HmiTextBox_Reset.TextBoxReadOnly = True
            RichTextBox_GCode.ReadOnly = True
            HmiButton_GFileLinkChoose1.Enabled = False
            HmiButton_GFileLinkChoose2.Enabled = False
            HmiButton_GFileLinkModify1.Enabled = False
            HmiButton_GFileLinkModify2.Enabled = False
            HmiButton_LoadVariant.Enabled = False
            HmiTextBox_Name1.TextBoxReadOnly = True
            HmiComboBox_Variant.Enabled = False
            HmiButtonWithIndicate_RWrite.Enabled = False
            HmiButtonWithIndicate_RRead.Enabled = False
            HmiButton_Start.Enabled = False
            HmiButton_Stop.Enabled = False

        End If

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Engineer Then
            HmiTextBox_HS_Confirm.TextBoxReadOnly = False
            HmiTextBox_Reset.TextBoxReadOnly = False
            RichTextBox_GCode.ReadOnly = False
            HmiTextBox_Name1.TextBoxReadOnly = False
        End If


        Dim iCnt As Integer = 0
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
            If cVariantManager.CurrentVariantCfg.Variant = element.Variant Then
                HmiComboBox_Variant.ComboBox.SelectedIndex = iCnt
            End If
            iCnt = iCnt + 1

        Next




        HmiDataView_GFile.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile.Rows.Clear()
        HmiDataView_GFile.Columns.Clear()
        HmiDataView_GFile.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_GFile.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Name")
        PostTest_Name.Name = "PostTest_Name"
        HmiDataView_GFile.Columns.Add(PostTest_Name)

        Dim PostTest_Path As New DataGridViewTextBoxColumn
        PostTest_Path.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "GFileName")
        PostTest_Path.Name = "PostTest_Path"
        HmiDataView_GFile.Columns.Add(PostTest_Path)

        HmiDataView_GFile2.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile2.Rows.Clear()
        HmiDataView_GFile2.Columns.Clear()
        HmiDataView_GFile2.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile2.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_GFile2.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_GFile2.Columns.Add(PostTest_id)

        PostTest_Name = New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Name")
        PostTest_Name.Name = "PostTest_Name"
        HmiDataView_GFile2.Columns.Add(PostTest_Name)

        PostTest_Path = New DataGridViewTextBoxColumn
        PostTest_Path.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "GFileName")
        PostTest_Path.Name = "PostTest_Path"
        HmiDataView_GFile2.Columns.Add(PostTest_Path)

      
        HmiTextBox_NCErrorCode.TextBox.Text = "0"
        CreateMFuction()
        CreateRFuction()
        LoadGFile()
        ' AddHandler HmiTextBox_HS_Confirm.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiButton_Stop.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_Stop.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButtonWithIndicate_RWrite.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButtonWithIndicate_RWrite.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButtonWithIndicate_RRead.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButtonWithIndicate_RRead.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_HS_Confirm.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_HS_Confirm.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_Reset.MouseDown, AddressOf Button_MouseDown
        AddHandler HmiButton_Reset.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_Build.Click, AddressOf Button_Click
        AddHandler HmiButton_Release.Click, AddressOf Button_Click
        AddHandler HmiButton_Load.Click, AddressOf Button_Click
        AddHandler Button_Open.Click, AddressOf Button_Click
        AddHandler Button_Save.Click, AddressOf Button_Click
        AddHandler Button_New.Click, AddressOf Button_Click
        AddHandler HmiButton_Start.Click, AddressOf Button_Click
        AddHandler HmiButton_MotorEnable.Click, AddressOf Button_Click
        AddHandler HmiButton_LoadVariant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_GFileLinkChoose1.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_GFileLinkModify1.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_GFileLinkChoose2.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_GFileLinkModify2.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Name1.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiTextBox_GFile.TextBox.TextChanged, AddressOf TextBox_TextChanged
        If Not bReadOnly Then
            AddHandler HmiDataView_GFile.CellClick, AddressOf HmiDataView_CellClick
            AddHandler HmiDataView_GFile2.CellClick, AddressOf HmiDataView_CellClick
        End If
        Return True
    End Function


    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_GFileLink1Right.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Name1.TextBox.Height + 6 + 6
        Next
        For Each element As RowStyle In TableLayoutPanel_GFileLink2Right.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Name1.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiComboBox_Variant"
                    HmiButton_LoadVariant.Button.Enabled = True
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub HmiDataView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Select Case sender.name
            Case "HmiDataView_GFile"
                If IsNothing(HmiDataView_GFile.CurrentRow) Then Return
                If HmiDataView_GFile.CurrentRow.Index <= HmiDataView_GFile.Rows.Count - 1 Then
                    HmiTextBox_Name1.TextBox.Text = HmiDataView_GFile.Rows(HmiDataView_GFile.CurrentRow.Index).Cells(1).Value
                    HmiTextBox_GFileLink1.TextBox.Text = HmiDataView_GFile.Rows(HmiDataView_GFile.CurrentRow.Index).Cells(2).Value
                    LoadGFile(HmiTextBox_GFileLink1.TextBox.Text)
                End If
            Case "HmiDataView_GFile2"
                If IsNothing(HmiDataView_GFile2.CurrentRow) Then Return
                If HmiDataView_GFile2.CurrentRow.Index <= HmiDataView_GFile2.Rows.Count - 1 Then
                    HmiTextBox_GFileLink2.TextBox.Text = HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(2).Value
                    LoadGFile(HmiTextBox_GFileLink2.TextBox.Text)
                End If
        End Select

    End Sub

    Private Sub LoadAction(ByVal strVariant As String)
        Try
            HmiButton_LoadVariant.Button.Enabled = False
            HmiTextBox_GFileLink2.TextBox.Text = ""
            If strVariant = "" Then Return
            Dim iCnt As Integer = 1
            Dim i As Integer = 0
            Dim j As Integer = 0
            lListActionStep.Clear()
            HmiDataView_GFile2.Rows.Clear()
            cActionManager.LoadActionCfg(strVariant)
            For Each element As String In cActionManager.GetActionListKey()
                For Each elementAction In cActionManager.GetActionCfgFromKey(element).GetStepListKey
                    i = 0
                    Dim lListMainStepCfg As List(Of clsMainStepCfg) = cActionManager.GetMainStepCfgList(element, elementAction)
                    For Each elementMainStepCfg As clsMainStepCfg In lListMainStepCfg
                        j = 0
                        For Each elementSubKey As String In elementMainStepCfg.GetSubStepListKey
                            Dim lListParameter() As String = clsParameter.ToList(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter)).ToArray
                            If lListParameter.Length < 1 Then
                                j = j + 1
                                Continue For
                            End If
                            If elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationGapFiller" Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationIndex.ToString <> lListParameter(0) Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationID.ToString <> element Then
                                j = j + 1
                                Continue For
                            End If
                            Dim cPointCfg As New clsActionPointCfg
                            cPointCfg.Station = element
                            cPointCfg.Action = elementAction
                            cPointCfg.MainStepIndex = i
                            cPointCfg.SubStepIndex = j
                            cPointCfg.Parameter = elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter)
                            lListActionStep.Add(iCnt.ToString, cPointCfg)
                            Dim mTemp As String = ""
                            For i = 1 To lListParameter.Count - 1
                                If mTemp = "" Then
                                    mTemp = lListParameter(i)
                                Else
                                    mTemp = mTemp + "\" + lListParameter(i)
                                End If
                            Next

                            HmiDataView_GFile2.Rows.Add(iCnt.ToString,
                                                       elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Name),
                                                       mTemp
                                                       )
                            iCnt = iCnt + 1
                            j = j + 1
                        Next
                        i = i + 1

                    Next


                Next
            Next
            Showdefault()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub LoadGFile()
        HmiDataView_GFile.Rows.Clear()
        lListGFile.Clear()
        Dim mTempValue As String = String.Empty
        Dim cGFile(19) As StructGFile
        For i = 0 To 19
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Name")
            Dim cGFilePathCfg As New clsGFilePathCfg
            cGFilePathCfg.Name = mTempValue
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Path")
            cGFilePathCfg.Path = mTempValue
            lListGFile.Add(i.ToString, cGFilePathCfg)
            cGFile(i) = New StructGFile
            cGFile(i).strName = lListGFile(i).Name
            cGFile(i).strPath = lListGFile(i).Path
            HmiDataView_GFile.Rows.Add((i + 1).ToString, lListGFile(i).Name, lListGFile(i).Path)
        Next
    End Sub

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

    Public Sub CreateRFuction()
        Dim mTempValue As String = String.Empty
        For j = 0 To 9
            Dim cRCfg As New clsRCfg
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "R" + j.ToString, "Text")
            cRCfg.strText = mTempValue
            If cRCfg.strText = "" Then
                cRCfg.strText = "R" + j.ToString
            End If

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "R" + j.ToString, "Index")
            If mTempValue = "" Then
                cRCfg.iIndex = j
            Else
                cRCfg.iIndex = CInt(mTempValue)
            End If

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "R" + j.ToString, "Reserve")
            cRCfg.Reserve = IIf(mTempValue = "True", True, False)
            Dim cLabel As New Label
            cLabel.Dock = System.Windows.Forms.DockStyle.Fill
            cLabel.Font = New System.Drawing.Font("Calibri", iFontSize)
            cLabel.Name = "R" + j.ToString
            cLabel.BackColor = Color.White
            cLabel.TextAlign = ContentAlignment.MiddleCenter
            cLabel.Size = New System.Drawing.Size(223, 32)
            If cRCfg.Reserve Then
                cLabel.Text = cLanguageManager.GetUserTextLine("GapFiller", "Reserve")
            Else
                cLabel.Text = cRCfg.strText
            End If
            cLabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
            HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(cLabel, 0, 5 + j + 1)
            HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(cLabel, 2)
            If Not bReadOnly Then AddHandler cLabel.MouseDown, AddressOf Button_Down
            cRCfg.cLabel = cLabel

            Dim cText As New HMITextBox
            cText.Dock = System.Windows.Forms.DockStyle.Fill
            cText.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
            cText.Name = "R" + j.ToString
            ' cText.Size = New System.Drawing.Size(223, 32)
            cText.Text = "0"
            cText.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(cText, 1, 5 + j + 1)
            cRCfg.cText = cText
            cText.ValueType = GetType(Double)
            If cRCfg.Reserve Then
                cText.TextBoxReadOnly = True
            End If
            AddHandler cText.TextBox.TextChanged, AddressOf RTextBox_TextChanged
            cRCfg.ID = "R" + j.ToString
            lListR.Add("R" + j.ToString, cRCfg)
            lListROldText.Add("R" + j.ToString, "")
        Next

    End Sub

    Public Sub CreateMFuction()
        Dim mTempValue As String = String.Empty
        HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowCount = 16
        For j = 1 To 15
            HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.67!))
        Next
        For j = 1 To 15
            HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles(j - 1) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.67!)
        Next
        HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles(15) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)
        For j = 1 To 15

            Dim iMFucntion As New MFunction
            Dim cMFunctionCfg As New clsMFunctionCfg
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Text")
            cMFunctionCfg.Text = mTempValue
            If cMFunctionCfg.Text = "" Then
                cMFunctionCfg.Text = "M" + j.ToString
            End If

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Index")
            If mTempValue = "" Then
                cMFunctionCfg.Index = j - 1
            Else
                cMFunctionCfg.Index = CInt(mTempValue)
            End If

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Reserve")
            cMFunctionCfg.Reserve = IIf(mTempValue = "True", True, False)
            iMFucntion.Dock = System.Windows.Forms.DockStyle.Fill
            iMFucntion.MainButton.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
            iMFucntion.MainButton.Name = j.ToString
            iMFucntion.Size = New System.Drawing.Size(223, 32)
            If cMFunctionCfg.Reserve Then
                iMFucntion.MainButton.Text = cLanguageManager.GetUserTextLine("GapFiller", "Reserve")
            Else
                iMFucntion.MainButton.Text = cMFunctionCfg.Text
            End If
            iMFucntion.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Controls.Add(iMFucntion, 0, j - 1)
            If Not bReadOnly Then AddHandler iMFucntion.MainButton.MouseDown, AddressOf Button_Down
            cMFunctionCfg.MFunction = iMFucntion
            lListIO.Add(j.ToString, cMFunctionCfg)
        Next

    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_HS_Confirm"
                CheckHSConfirm()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_GFileLink2"
                If HmiTextBox_GFileLink2.TextBox.Text = "" Then
                    HmiButton_GFileLinkModify2.Enabled = Not bReadOnly
                Else
                    HmiButton_GFileLinkModify2.Enabled = False
                End If

            Case "HmiTextBox_GFile"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMICurrentGFilePath", HmiTextBox_GFile.TextBox.Text, New Integer() {HmiTextBox_GFile.TextBox.Text.Length})
        End Select
    End Sub

    Private Sub RTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If sender.Text = "" Then sender.Text = "0"
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_RFucntion[" + (lListR(sender.Name).iIndex).ToString + "]", Double.Parse(sender.Text))
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Index", lListR(sender.Name).iIndex.ToString)
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Value", sender.Text)

        End SyncLock
    End Sub


    Private Sub CheckHSConfirm()
        Try
            If HmiTextBox_HS_Confirm.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_HS_Confirm.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "14"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIHshakeNo", Int16.Parse(HmiTextBox_HS_Confirm.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIHshakeNo", Int16.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Button_Open"
                LoadG()
            Case "Button_Save"
                SaveG()
            Case "Button_New"
                NewG()
            Case "HmiButton_Start"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIStart", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStart", Not dNewValue)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStop", False)
                TextBox_Read.Text = ""
                strLastDisplayValue = ""
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", Not dNewValue)
            Case "HmiButton_Build"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIBuild3D", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIBuild3D", dNewValue)
            Case "HmiButton_Release"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRelease3D", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)
            Case "HmiButton_Load"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMILoadGFile", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMILoadGFile", dNewValue)

            Case "HmiButton_LoadVariant"
                LoadAction(HmiComboBox_Variant.ComboBox.Text)

            Case "HmiButton_GFileLinkChoose1"
                LoodGFileLinkChoose1()
                '  LoadGFile(HmiTextBox_GFileLink1.TextBox.Text)
            Case "HmiButton_GFileLinkChoose2"
                LoodGFileLinkChoose2()
                '  LoadGFile(HmiTextBox_GFileLink2.TextBox.Text)

            Case "HmiButton_GFileLinkModify1"
                GFileModify1()

            Case "HmiButton_GFileLinkModify2"
                GFileModify2()
        End Select
    End Sub

    Private Sub GFileModify1()
        If IsNothing(HmiDataView_GFile.CurrentRow) Then Return
        If HmiDataView_GFile.CurrentRow.Index <= HmiDataView_GFile.Rows.Count - 1 Then
            HmiDataView_GFile.Rows(HmiDataView_GFile.CurrentRow.Index).Cells(1).Value = HmiTextBox_Name1.TextBox.Text
            HmiDataView_GFile.Rows(HmiDataView_GFile.CurrentRow.Index).Cells(2).Value = HmiTextBox_GFileLink1.TextBox.Text
            lListGFile(HmiDataView_GFile.CurrentRow.Index).Name = HmiTextBox_Name1.TextBox.Text
            lListGFile(HmiDataView_GFile.CurrentRow.Index).Path = HmiTextBox_GFileLink1.TextBox.Text
        End If
        SaveGFile()
    End Sub

    Private Sub SaveGFile()
        For i = 0 To lListGFile.Count - 1
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Name", lListGFile(i).Name)
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "GFile" + i.ToString, "Path", lListGFile(i).Path)
        Next
        Dim cGFile(19) As StructGFile
        For i = 0 To 19
            cGFile(i) = New StructGFile
            cGFile(i).strName = lListGFile(i).Name
            cGFile(i).strPath = lListGFile(i).Path
        Next
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_SystemGFile", cGFile)
    End Sub

    Private Sub GFileModify2()
        Try
            If IsNothing(HmiDataView_GFile2.CurrentRow) Then Return
            If HmiDataView_GFile2.CurrentRow.Index <= HmiDataView_GFile2.Rows.Count - 1 Then
                HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(2).Value = HmiTextBox_GFileLink2.TextBox.Text
                Dim lListParameter As List(Of String) = clsParameter.ToList(lListActionStep(HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(0).Value).Parameter)
                Dim lListParameterNew As New List(Of String)
                lListParameterNew.Add(lListParameter(0))
                Dim cValue() As String = HmiTextBox_GFileLink2.TextBox.Text.Split("\")
                For i = 0 To cValue.Length - 1
                    lListParameterNew.Add(cValue(i))
                Next

                cActionManager.ChangeCurrentSubParameter(lListActionStep(HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(0).Value).Station,
                                                         lListActionStep(HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(0).Value).Action,
                                                         lListActionStep(HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                         lListActionStep(HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(0).Value).SubStepIndex,
                                                         HMISubStepKeys.Parameter,
                                                         clsParameter.ToString(lListParameterNew))
                cActionManager.CheckCurrentActionCfg()
                cActionManager.SaveCurrentActionCfg(cActionManager.VariantCfg.Variant)
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoodGFileLinkChoose1()
        Dim strFilePath As String = String.Empty
        OpenFileDialog_Path.Filter = "*.nc|*.nc"
        OpenFileDialog_Path.FilterIndex = 1
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            HmiTextBox_GFileLink1.TextBox.Text = OpenFileDialog_Path.FileName
        End If
    End Sub

    Private Sub LoodGFileLinkChoose2()
        Dim strFilePath As String = String.Empty
        OpenFileDialog_Path.Filter = "*.nc|*.nc"
        OpenFileDialog_Path.FilterIndex = 1
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            HmiTextBox_GFileLink2.TextBox.Text = OpenFileDialog_Path.FileName
        End If
    End Sub

    Private Sub Button_Down(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            If sender.Name.IndexOf("R") >= 0 Then
                Dim cIOParameter As New RParameterForm
                cIOParameter.TextFont = HmiButton_Start.Font
                cIOParameter.TextBox_ID.Text = sender.Name
                cIOParameter.isReserve = lListR(sender.Name).Reserve
                cIOParameter.Index = lListR(sender.Name).iIndex
                cIOParameter.TextBox_Text.Text = lListR(sender.Name).cLabel.Text
                cIOParameter.Init(cLocalElement, cSystemElement)
                If cIOParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cIOParameter.ComboBox_Model.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "27"), enumExceptionType.Alarm))
                    End If
                    lListR(sender.Name).cLabel.Text = cIOParameter.TextBox_Text.Text
                    lListR(sender.Name).iIndex = cIOParameter.Index
                    lListR(sender.Name).Reserve = cIOParameter.isReserve
                    'lListR(sender.Name).cText.Text = "0"
                    If lListR(sender.Name).Reserve Then
                        lListR(sender.Name).cLabel.Text = cLanguageManager.GetUserTextLine("GapFiller", "Reserve")
                        lListR(sender.Name).cText.TextBoxReadOnly = True
                    Else
                        lListR(sender.Name).cText.TextBoxReadOnly = False
                    End If
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Index", lListR(sender.Name).iIndex.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Text", lListR(sender.Name).cLabel.Text.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Reserve", lListR(sender.Name).Reserve.ToString)
                End If
            Else
                Dim cIOParameter As New ParameterForm
                cIOParameter.TextFont = HmiButton_Start.Font
                cIOParameter.TextBox_ID.Text = sender.Name
                cIOParameter.isReserve = lListIO(sender.Name).Reserve
                cIOParameter.Index = lListIO(sender.Name).Index
                cIOParameter.Init(cLocalElement, cSystemElement)
                If cIOParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cIOParameter.ComboBox_Model.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "15"), enumExceptionType.Alarm))
                    End If
                    lListIO(sender.Name).Text = cIOParameter.ComboBox_Model.Text
                    lListIO(sender.Name).Index = cIOParameter.Index
                    lListIO(sender.Name).Reserve = cIOParameter.isReserve
                    If lListIO(sender.Name).Reserve Then
                        lListIO(sender.Name).MFunction.MainButton.Text = cLanguageManager.GetUserTextLine("GapFiller", "Reserve")
                    Else
                        lListIO(sender.Name).MFunction.MainButton.Text = lListIO(sender.Name).Text
                    End If
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Index", lListIO(sender.Name).Index.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Text", lListIO(sender.Name).Text.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", sender.Name, "Reserve", lListIO(sender.Name).Reserve.ToString)
                End If
            End If
        End If
    End Sub

    Private Sub LoadG()
        Dim strFilePath As String = String.Empty
        'OpenFileDialog_Path.InitialDirectory = "C:\TwinCAT\CNC"
        OpenFileDialog_Path.Filter = "*.nc|*.nc"
        OpenFileDialog_Path.FilterIndex = 1
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            strFilePath = OpenFileDialog_Path.FileName
            RichTextBox_GCode.Text = ""
            LoadGFile(strFilePath)
        End If
        HmiButton_Start.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Stop.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
    End Sub

    Private Sub LoadGFile(ByVal strFileName As String)
        If strFileName = "" Then Return
        If Not File.Exists(strFileName) Then Return
        RichTextBox_GCode.Text = ""
        Dim iFile As New StreamReader(strFileName)
        Dim strLastSection As String = String.Empty
        Dim mTemp As String = iFile.ReadLine
        Do While Not IsNothing(mTemp)
            RichTextBox_GCode.AppendText(mTemp + vbCrLf)
            mTemp = iFile.ReadLine
        Loop
        iFile.Close()
        HmiTextBox_GFile.TextBox.Text = strFileName
        HmiButton_Start.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Stop.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)

    End Sub

    Private Sub SaveG()
        Dim strFilePath As String = String.Empty
        '  SaveFileDialog_Path.InitialDirectory = "C:\TwinCAT\CNC"
        SaveFileDialog_Path.FileName = HmiTextBox_GFile.TextBox.Text
        SaveFileDialog_Path.Filter = "*.nc|.nc"
        SaveFileDialog_Path.FilterIndex = 1
        If SaveFileDialog_Path.ShowDialog() = DialogResult.OK Then
            strFilePath = SaveFileDialog_Path.FileName
            Dim tempArray() As String
            Dim iFile As New StreamWriter(strFilePath)
            tempArray = RichTextBox_GCode.Lines
            For Each element As String In tempArray
                iFile.WriteLine(element)
            Next
            iFile.Close()
        End If
        HmiButton_Start.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Stop.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)

    End Sub

    Private Sub NewG()
        RichTextBox_GCode.Text = ""
        HmiButton_Start.Enabled = False
        HmiButton_Stop.Enabled = False
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_Start"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStart", dNewValue)
            Case "HmiButton_Stop"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStop", dNewValue)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStart", False)
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", dNewValue)
            Case "HmiButton_Build"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIBuild3D", dNewValue)
            Case "HmiButton_Release"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)
            Case "HmiButton_Load"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMILoadGFile", dNewValue)
            Case "HmiButton_HS_Confirm"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIHSConfirm", dNewValue)
            Case "HmiButton_Reset"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMI3DReset", dNewValue)
            Case "HmiButtonWithIndicate_RRead"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRRead", dNewValue)
            Case "HmiButtonWithIndicate_RWrite"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRWrite", dNewValue)

        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "HmiButton_Start"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStart", dNewValue)
            Case "HmiButton_Stop"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStop", dNewValue)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIStart", False)
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", dNewValue)
            Case "HmiButton_Build"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIBuild3D", dNewValue)
            Case "HmiButton_Release"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)
            Case "HmiButton_Load"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMILoadGFile", dNewValue)
            Case "HmiButton_HS_Confirm"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIHSConfirm", dNewValue)
            Case "HmiButton_Reset"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMI3DReset", dNewValue)
            Case "HmiButtonWithIndicate_RRead"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRRead", dNewValue)
            Case "HmiButtonWithIndicate_RWrite"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRWrite", dNewValue)
        End Select
    End Sub

    Public Sub Showdefault()
        If HmiDataView_GFile2.Rows.Count > 0 Then HmiDataView_GFile2.CurrentCell = HmiDataView_GFile2.Rows(0).Cells(0)
        If IsNothing(HmiDataView_GFile2.CurrentRow) Then Return
        If HmiDataView_GFile2.CurrentRow.Index <= HmiDataView_GFile2.Rows.Count - 1 Then
            HmiTextBox_GFileLink2.TextBox.Text = HmiDataView_GFile2.Rows(HmiDataView_GFile2.CurrentRow.Index).Cells(2).Value
            LoadGFile(HmiTextBox_GFileLink2.TextBox.Text)
        End If

    End Sub

    Private Sub Read()
        SyncLock _Object
            Dim stream As New TwinCAT.Ads.AdsStream(2048)
            Dim nByte As Integer = 0
            Dim nRet As Integer = cGapFiller.TcAds.Read(&H2300 + 2, &H20000001, stream)
            Dim S As New AdsBinaryReader(stream)
            ' Dim strTemp As String = S.ReadPlcString(2048, System.Text.Encoding.ASCII)
            Dim strTemp As String = S.ReadPlcAnsiString(2048)
            S.Close()
            stream.Close()
            stream.Dispose()

            'll.Add(strTemp)

            If strTemp.Split(vbCrLf).Length >= 3 And strTemp <> strLastTemp Then
                strLastValue = strLastValue + strTemp
                strLastTemp = strTemp
            End If

            Dim cValue() As String = strLastValue.Split(vbLf)
            If cValue.Length >= 2 Then
                strLastValue = ""
                strNowLine1 = cValue(0).Replace(vbCr, "")
                If cValue.Length >= 2 Then
                    strNowLine2 = cValue(1).Replace(vbCr, "")
                Else
                    strNowLine2 = ""
                End If

                If cValue.Length >= 3 Then
                    strNowLine3 = cValue(2).Replace(vbCr, "")
                Else
                    strNowLine3 = ""
                End If

                If strNowLine1 <> strLastLine1 And strNowLine1 <> strLastLine2 And strNowLine1 <> strLastLine3 Then
                    cGapFiller.strLastProgram = strNowLine1
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine1
                    For i = 1 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
                If strNowLine2 <> strLastLine1 And strNowLine2 <> strLastLine2 And strNowLine2 <> strLastLine3 And strNowLine2 <> "" Then
                    cGapFiller.strLastProgram = strNowLine2
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine2
                    For i = 2 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
                If strNowLine3 <> strLastLine1 And strNowLine3 <> strLastLine2 And strNowLine3 <> strLastLine3 And strNowLine3 <> "" Then
                    cGapFiller.strLastProgram = strNowLine3
                    strLastLine1 = strLastLine2
                    strLastLine2 = strLastLine3
                    strLastLine3 = strNowLine3
                    For i = 3 To cValue.Length - 2
                        strLastValue = strLastValue + cValue(i).Replace(vbCr, vbCrLf)
                    Next
                    Return
                End If
            End If

            If strTemp = "" Then
                strLastValue = ""
                strNowLine1 = ""
                strNowLine2 = ""
                strNowLine3 = ""
                strLastLine1 = ""
                strLastLine2 = ""
                strLastLine3 = ""
            End If
            If cGapFiller.strLastProgram <> strLastDisplayValue Then
                strLastDisplayValue = cGapFiller.strLastProgram
                mMainForm.InvokeAction(Sub()
                                           Dim mTemp As String = strLastDisplayValue
                                           TextBox_Read.Focus()
                                           TextBox_Read.Select(TextBox_Read.TextLength, 0)
                                           TextBox_Read.ScrollToCaret()
                                           If TextBox_Read.Text = "" Then
                                               TextBox_Read.AppendText(strLastDisplayValue)
                                           Else
                                               TextBox_Read.AppendText(strLastDisplayValue)
                                           End If
                                           System.Threading.Thread.Sleep(2)
                                           If TextBox_Read.TextLength - mTemp.Length >= 0 Then
                                               TextBox_Read.Select(TextBox_Read.TextLength - mTemp.Length + 1, 0)
                                           End If

                                       End Sub)

            End If

        End SyncLock
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
                    iStep = iStep + 1

                Case 4
                    TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))
                    mMainForm.InvokeAction(Sub()
                                               For Each element As clsMFunctionCfg In lListIO.Values
                                                   If element.Index >= 0 Then
                                                       element.MFunction.SetIndicateBackColor(TempStructGapFiller.PLC_MFucntion(element.Index))
                                                   End If

                                               Next
                                           End Sub)
                    mMainForm.InvokeAction(Sub()
                                               For Each element As clsRCfg In lListR.Values
                                                   If element.iIndex >= 0 Then
                                                       If lListROldText(element.ID) <> TempStructGapFiller.PLC_RFucntion(element.iIndex).ToString Then
                                                           element.cText.TextBox.Text = TempStructGapFiller.PLC_RFucntion(element.iIndex).ToString
                                                           lListROldText(element.ID) = TempStructGapFiller.PLC_RFucntion(element.iIndex).ToString
                                                       End If
                                                   End If
                                               Next
                                           End Sub)

                    If TempStructGapFiller.fdHMIHshakeNo <> OldStructGapFiller.fdHMIHshakeNo Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_HS_Confirm.TextBox.Text = TempStructGapFiller.fdHMIHshakeNo.ToString("0")
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdPLCErrorCode <> OldStructGapFiller.fdPLCErrorCode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_Reset.TextBox.Text = TempStructGapFiller.fdPLCErrorCode.ToString("0")
                                               End Sub)
                    End If


                    If TempStructGapFiller.fdPLCXPosition <> OldStructGapFiller.fdPLCXPosition Or TempStructGapFiller.fdPLCYPosition <> OldStructGapFiller.fdPLCYPosition Or TempStructGapFiller.fdPLCZPosition <> OldStructGapFiller.fdPLCZPosition Then
                        mMainForm.InvokeAction(Sub()
                                                   Label2_X.Text = TempStructGapFiller.fdPLCXPosition.ToString("0.00")
                                                   Label2_Y.Text = TempStructGapFiller.fdPLCYPosition.ToString("0.00")
                                                   Label2_Z.Text = TempStructGapFiller.fdPLCZPosition.ToString("0.00")
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIMotorEnable <> OldStructGapFiller.bulHMIMotorEnable Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_MotorEnable.SetIndicateBackColor(TempStructGapFiller.bulHMIMotorEnable)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIHSConfirm <> OldStructGapFiller.bulHMIHSConfirm Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_HS_Confirm.SetIndicateBackColor(TempStructGapFiller.bulHMIHSConfirm)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMI3DReset <> OldStructGapFiller.bulHMI3DReset Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Reset.SetIndicateBackColor(TempStructGapFiller.bulHMI3DReset)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIStart <> OldStructGapFiller.bulHMIStart Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Start.SetIndicateBackColor(TempStructGapFiller.bulHMIStart)
                                               End Sub)
                        If Not TempStructGapFiller.bulHMIStart Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Start.BackColor = Color.Transparent
                                                   End Sub)
                        End If
                    End If

                    If TempStructGapFiller.bulHMIStop <> OldStructGapFiller.bulHMIStop Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Stop.SetIndicateBackColor(TempStructGapFiller.bulHMIStop)
                                               End Sub)
                    End If

                    'bulHMIBuild3D
                    If TempStructGapFiller.bulHMIBuild3D.bulHMIDoAction <> OldStructGapFiller.bulHMIBuild3D.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Build.SetIndicateColor(TempStructGapFiller.bulHMIBuild3D.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdPLCNCErrorCode <> OldStructGapFiller.fdPLCNCErrorCode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_NCErrorCode.TextBox.Text = TempStructGapFiller.fdPLCNCErrorCode.ToString
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail <> OldStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass <> OldStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Build.SetIndicateColor(TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass, TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIBuild3D", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass

                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIBuild3D", dNewValue)
                            End If
                        End If
                    End If

                    'bulHMIRelease3D
                    If TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction <> OldStructGapFiller.bulHMIRelease3D.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Release.SetIndicateColor(TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail <> OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass <> OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Release.SetIndicateColor(TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass, TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRelease3D", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)
                            End If
                        End If
                    End If

                    'bulHMILoadGFile
                    If TempStructGapFiller.bulHMILoadGFile.bulHMIDoAction <> OldStructGapFiller.bulHMILoadGFile.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Load.SetIndicateColor(TempStructGapFiller.bulHMILoadGFile.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail <> OldStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail Or TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass <> OldStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Load.SetIndicateColor(TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass, TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail Or TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMILoadGFile", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMILoadGFile", dNewValue)
                            End If
                        End If
                    End If

                    If TempStructGapFiller.bulHMIRRead <> OldStructGapFiller.bulHMIRRead Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_RRead.SetIndicateBackColor(TempStructGapFiller.bulHMIRRead)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIRWrite <> OldStructGapFiller.bulHMIRWrite Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_RWrite.SetIndicateBackColor(TempStructGapFiller.bulHMIRWrite)
                                               End Sub)
                    End If
                    Read()
                    OldStructGapFiller.bulHMIRRead = TempStructGapFiller.bulHMIRRead
                    OldStructGapFiller.bulHMIRWrite = TempStructGapFiller.bulHMIRWrite
                    OldStructGapFiller.fdPLCNCErrorCode = TempStructGapFiller.fdPLCNCErrorCode
                    OldStructGapFiller.bulHMIMotorEnable = TempStructGapFiller.bulHMIMotorEnable
                    OldStructGapFiller.bulHMIStart = TempStructGapFiller.bulHMIStart
                    OldStructGapFiller.bulHMIStop = TempStructGapFiller.bulHMIStop
                    OldStructGapFiller.bulHMIHSConfirm = TempStructGapFiller.bulHMIHSConfirm
                    OldStructGapFiller.bulHMI3DReset = TempStructGapFiller.bulHMI3DReset

                    OldStructGapFiller.fdPLCErrorCode = TempStructGapFiller.fdPLCErrorCode
                    OldStructGapFiller.fdHMIHshakeNo = TempStructGapFiller.fdHMIHshakeNo
                    OldStructGapFiller.fdPLCXPosition = TempStructGapFiller.fdPLCXPosition
                    OldStructGapFiller.fdPLCYPosition = TempStructGapFiller.fdPLCYPosition
                    OldStructGapFiller.fdPLCZPosition = TempStructGapFiller.fdPLCZPosition

                    OldStructGapFiller.bulHMIBuild3D.bulHMIDoAction = TempStructGapFiller.bulHMIBuild3D.bulHMIDoAction
                    OldStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass = TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail = TempStructGapFiller.bulHMIBuild3D.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIRelease3D.bulHMIDoAction = TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction
                    OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass = TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail = TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail

                    OldStructGapFiller.bulHMILoadGFile.bulHMIDoAction = TempStructGapFiller.bulHMILoadGFile.bulHMIDoAction
                    OldStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass = TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsPass
                    OldStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail = TempStructGapFiller.bulHMILoadGFile.bulPlcActionIsFail
            End Select
        Catch ex As Exception
            If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try


    End Sub

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        iStep = 1
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        iStep = 1
        Return True
    End Function
End Class


Public Class clsActionPointCfg
    Public Station As String
    Public Action As String
    Public MainStepIndex As Integer
    Public SubStepIndex As Integer
    Public Parameter As String
End Class

Public Class clsRCfg
    Public cLabel As Label
    Public cText As HMITextBox
    Public Reserve As Boolean
    Public iIndex As Integer
    Public strText As String = String.Empty
    Public strValue As String = String.Empty
    Public ID As String = String.Empty
End Class