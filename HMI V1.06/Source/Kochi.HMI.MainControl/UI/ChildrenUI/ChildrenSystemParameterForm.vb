Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Action
Imports Kochi.HMI.MainControl.Device

Public Class ChildrenSystemParameterForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cListParameterNewName As New Dictionary(Of String, String)
    Private cListParameterOldName As New Dictionary(Of String, String)
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDeviceManager As clsDeviceManager
    Private cMachineManager As clsMachineManager
    Private cUserManager As clsUserManager
    Private mMainForm As MainForm
    Private mParentMainForm As ParentMainForm
    Private cLanguageManager As clsLanguageManager
    Private cHMIActionBase As clsHMIActionBase
    Private cHMIDeviceBase As clsHMIDeviceBase
    Private cActionLibManager As clsActionLibManager
    Private cDeviceLibManager As clsDeviceLibManager
    Private cErrorCodeManager As clsErrorCodeManager
    Private cMainFormButtonManager As clsMainButtonManager
    Private strButtonName As String
    Private iErrorPosition As Integer = 0
    Private cFormFontResize As clsFormFontResize
    Private cProgramButton As clsProgramButton
    Private cVariantManager As clsVariantManager
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private lRowListValue As New Dictionary(Of String, clsListValueCfg)
    Private lColumnListValue As New Dictionary(Of String, clsListValueCfg)
    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            mParentMainForm = CType(cSystemElement(enumUIName.ParentMainForm.ToString), ParentMainForm)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
            cDeviceLibManager = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorCodeManager = CType(cSystemElement(clsErrorCodeManager.Name), clsErrorCodeManager)
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cErrorMessageManager.RegisterSaveFunction(AddressOf SaveFunction)
            cErrorMessageManager.RegisterAbortFunction(AddressOf AbortFunction)
            cErrorMessageManager.RegisterResetFunction(AddressOf ResetFunction)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)

            InitForm()
            InitControlText()
            CreateTabPage()
            CreateVariantPage()
            cLocalElement.Add(enumUIName.ChildrenSystemParameterForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenSystemParameterForm.ToString))
            Return False
        End Try
    End Function


    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        MachineListView_Data.Rows.Clear()
        MachineListView_Data.Columns.Clear()
        cListParameterNewName.Clear()
        cListParameterOldName.Clear()
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Data.Columns.Add(PostTest_id)

        Dim PostTest_Device As New DataGridViewTextBoxColumn
        PostTest_Device.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Type")
        PostTest_Device.Name = "PostTest_Type"
        PostTest_Device.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Device)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Name")
        PostTest_Name.Name = "PostTest_Name"
        PostTest_Name.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Name)

       

        Dim PostTest_Variant As New DataGridViewTextBoxColumn
        PostTest_Variant.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Select Variant")
        PostTest_Variant.Name = "PostTest_Variant"
        PostTest_Variant.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Variant)


        Dim PostTest_CheckArticle As New DataGridViewTextBoxColumn
        PostTest_CheckArticle.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "CheckArticle")
        PostTest_CheckArticle.Name = "PostTest_CheckArticle"
        PostTest_CheckArticle.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator
        MachineListView_Data.Columns.Add(PostTest_CheckArticle)

        Dim PostTest_CheckSN As New DataGridViewTextBoxColumn
        PostTest_CheckSN.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "CheckSN")
        PostTest_CheckSN.Name = "PostTest_CheckSN"
        PostTest_CheckSN.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator
        MachineListView_Data.Columns.Add(PostTest_CheckSN)


        Dim PostTest_AutoChangePage As New DataGridViewTextBoxColumn
        PostTest_AutoChangePage.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Auto Change Page")
        PostTest_AutoChangePage.Name = "PostTest_AutoChangePage"
        PostTest_AutoChangePage.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_AutoChangePage)

        Dim PostTest_HMIError As New DataGridViewTextBoxColumn
        PostTest_HMIError.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HMI Error Code")
        PostTest_HMIError.Name = "PostTest_HMIError"
        PostTest_HMIError.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_HMIError)


        Dim PostTest_Index As New DataGridViewTextBoxColumn
        PostTest_Index.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Index")
        PostTest_Index.Name = "PostTest_Index"
        PostTest_Index.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Index)

        Dim PostTest_ShowWaitingMessage As New DataGridViewTextBoxColumn
        PostTest_ShowWaitingMessage.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ShowWaitingMessage")
        PostTest_ShowWaitingMessage.Name = "PostTest_ShowWaitingMessage"
        PostTest_ShowWaitingMessage.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_ShowWaitingMessage)


        Dim PostTest_AutoConfirm As New DataGridViewTextBoxColumn
        PostTest_AutoConfirm.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "AutoConfirm")
        PostTest_AutoConfirm.Name = "PostTest_AutoConfirm"
        PostTest_AutoConfirm.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_AutoConfirm)

        Dim PostTest_FailAutoRun As New DataGridViewTextBoxColumn
        PostTest_FailAutoRun.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "FailAutoRun")
        PostTest_FailAutoRun.Name = "PostTest_FailAutoRun"
        PostTest_FailAutoRun.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_FailAutoRun)

        Dim PostTest_FailAutoRunNext As New DataGridViewTextBoxColumn
        PostTest_FailAutoRunNext.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "FailAutoRunNext")
        PostTest_FailAutoRunNext.Name = "PostTest_FailAutoRunNext"
        PostTest_FailAutoRunNext.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_FailAutoRunNext)

        Dim PostTest_MES As New DataGridViewTextBoxColumn
        PostTest_MES.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "MES")
        PostTest_MES.Name = "PostTest_MES"
        PostTest_MES.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_MES)

        Dim PostTest_ESTOP As New DataGridViewTextBoxColumn
        PostTest_ESTOP.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ESTOP")
        PostTest_ESTOP.Name = "PostTest_ESTOP"
        PostTest_ESTOP.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_ESTOP)

        Dim PostTest_NotInqueue As New DataGridViewTextBoxColumn
        PostTest_NotInqueue.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "NotInqueue")
        PostTest_NotInqueue.Name = "PostTest_NotInqueue"
        PostTest_NotInqueue.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_NotInqueue)

        Dim PostTest_Carrier As New DataGridViewTextBoxColumn
        PostTest_Carrier.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Carrier")
        PostTest_Carrier.Name = "PostTest_Carrier"
        PostTest_Carrier.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Carrier)


        Dim PostTest_ResetCarrier As New DataGridViewTextBoxColumn
        PostTest_ResetCarrier.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ResetCarrier")
        PostTest_ResetCarrier.Name = "PostTest_ResetCarrier"
        PostTest_ResetCarrier.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_ResetCarrier)

        Dim PostTest_Description As New DataGridViewTextBoxColumn
        PostTest_Description.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Description")
        PostTest_Description.Name = "PostTest_Description"
        PostTest_Description.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Description)



        Dim PostTest_Complete As New DataGridViewTextBoxColumn
        PostTest_Complete.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Complete")
        PostTest_Complete.Name = "PostTest_Complete"
        PostTest_Complete.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_Data.Columns.Add(PostTest_Complete)

        HmiTextBox_Project.TextBox.Text = cMachineManager.MachineCellManager.MachineCellCfg.ProjectName
        HmiTextBox_Cell_Name.TextBox.Text = cMachineManager.MachineCellManager.MachineCellCfg.CellName
        HmiTextBox_Cell_Description.TextBox.Text = cMachineManager.MachineCellManager.MachineCellCfg.CellDescription
        HmiTextBox_Cell_Picture.TextBox.Text = cMachineManager.MachineCellManager.MachineCellCfg.CellPicture

        Dim cListValueCfg As New clsListValueCfg
        Dim tempListObject As New List(Of Object)
        lColumnListValue.Clear()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Supplier Then
            tempListObject.Add(enumMachineStationType.Manual.ToString)
            tempListObject.Add(enumMachineStationType.Auto.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_Type", cListValueCfg)
            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumMachineStationType.Auto.ToString)
            tempListObject.Add(enumMachineStationType.Manual.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_Variant", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_CheckArticle", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_CheckSN", cListValueCfg)
            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_AutoChangePage", cListValueCfg)


            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_ShowWaitingMessage", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_AutoConfirm", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_FailAutoRun", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_FailAutoRunNext", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(String)
            tempListObject.Add("NONE")
            For Each element In cErrorCodeManager.GetErrorCodeListKey
                Dim cErrorCodeCfg As clsErrorCodeCfg = cErrorCodeManager.GetErrorCodeCfgFromKey(element)
                tempListObject.Add(cErrorCodeCfg.Key)
            Next
            lColumnListValue.Add("PostTest_HMIError", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add("NONE")
            tempListObject.Add("1")
            tempListObject.Add("2")
            tempListObject.Add("3")
            tempListObject.Add("4")
            tempListObject.Add("5")
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_MES", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_ESTOP", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add("Blue")
            tempListObject.Add("Red")
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_NotInqueue", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_Carrier", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_ResetCarrier", cListValueCfg)

            tempListObject = New List(Of Object)
            For i = 1 To cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationListKey.Count
                tempListObject.Add(i.ToString)
            Next

            cListValueCfg = New clsListValueCfg
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            lColumnListValue.Add("PostTest_Index", cListValueCfg)
        ElseIf cUserManager.CurrentUserCfg.Level >= enumUserLevel.Administrator Then

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_CheckArticle", cListValueCfg)

            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            tempListObject.Add(enumCheckType.True.ToString)
            tempListObject.Add(enumCheckType.False.ToString)
            cListValueCfg.ListValue = tempListObject
            lColumnListValue.Add("PostTest_CheckSN", cListValueCfg)

        End If

        MachineListView_Data.lColumnListValue = lColumnListValue
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            MachineListView_Data.Rows.Add(element.ID, element.MachineStationType.ToString, element.StationName, element.VariantType.ToString, element.CheckArticleType.ToString, element.CheckSNType.ToString, element.AutoChangePage.ToString, element.HMIError.ToString, element.Index, element.ShowWaitingMessage.ToString, element.AutoConfirm.ToString, element.FailAutoRun.ToString, element.FailRunNextStation.ToString, element.MES.ToString, element.Estop.ToString, element.NotInqueueColor.ToString, element.CheckCarrier.ToString, element.ResetCarrier.ToString, element.Description, element.CompleteStep)
        Next

        MachineListView_Parameter.Rows.Clear()
        MachineListView_Parameter.Columns.Clear()
        Dim Parameter_id As New DataGridViewTextBoxColumn
        Parameter_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ID")
        Parameter_id.Name = "Parameter_id"
        Parameter_id.ReadOnly = True
        Parameter_id.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_id)

        Dim Parameter_Name As New DataGridViewTextBoxColumn
        Parameter_Name.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Name")
        Parameter_Name.Name = "Parameter_Name"
        Parameter_Name.ReadOnly = True
        Parameter_Name.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_Name)

        Dim Parameter_Value As New DataGridViewTextBoxColumn
        Parameter_Value.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Value")
        Parameter_Value.Name = "Parameter_Value"
        Parameter_Value.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_Value)


        MachineListView_VariantParameter.Rows.Clear()
        MachineListView_VariantParameter.Columns.Clear()
        cListParameterNewName.Clear()
        cListParameterOldName.Clear()
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_VariantParameter.Columns.Add(PostTest_id)

        Parameter_Name = New DataGridViewTextBoxColumn
        Parameter_Name.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Name")
        Parameter_Name.Name = "Parameter_Name"
        Parameter_Name.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_VariantParameter.Columns.Add(Parameter_Name)

        Parameter_Value = New DataGridViewTextBoxColumn
        Parameter_Value.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Value")
        Parameter_Value.Name = "Parameter_Value"
        Parameter_Value.ReadOnly = cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier
        MachineListView_VariantParameter.Columns.Add(Parameter_Value)


        Dim lRowListValue As New Dictionary(Of String, clsListValueCfg)
        tempListObject = New List(Of Object)
        For Each element As clsGlobalParameterCfg In cMachineManager.MachineGlobalParameter.HMIGlobalParameter.GetUserDefinedKeys
            If cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier Then
                If element.Name <> clsHMIGlobalParameter.MES And
                    element.Name <> clsHMIGlobalParameter.Process And
                    element.Name <> clsHMIGlobalParameter.Manual_Screw_Repeat And
                    element.Name <> clsHMIGlobalParameter.Manual_Screw_ToleranceX And
                    element.Name <> clsHMIGlobalParameter.Manual_Screw_ToleranceY And
                    element.Name <> clsHMIGlobalParameter.Manual_Screw_ToleranceZ Then Continue For
            End If
            If element.ValueList.Count > 0 Then
                cListValueCfg = New clsListValueCfg
                cListValueCfg.ListValue = element.ValueList
                cListValueCfg.ListType = element.ValueType
            Else
                cListValueCfg = New clsListValueCfg
                cListValueCfg.ListValue = New List(Of Object)
                cListValueCfg.ListType = element.ValueType
            End If
            If cListParameterNewName.ContainsKey(element.Name) Then
                cListParameterNewName(element.Name) = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, element.Name)
            Else
                cListParameterNewName.Add(element.Name, cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, element.Name))
            End If
            If cListParameterOldName.ContainsKey(cListParameterNewName(element.Name)) Then
                cListParameterOldName(element.Name) = element.Name
            Else
                cListParameterOldName.Add(cListParameterNewName(element.Name), element.Name)
            End If
            lRowListValue.Add(cListParameterNewName(element.Name), cListValueCfg)
        Next
        Dim iCnt As Integer = 1
        MachineListView_Parameter.lRowListValue = lRowListValue
        For Each element As String In cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterListKey
            If cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier Then
                If element <> clsHMIGlobalParameter.MES And
                    element <> clsHMIGlobalParameter.Process And
                    element <> clsHMIGlobalParameter.Manual_Screw_Repeat And
                    element <> clsHMIGlobalParameter.Manual_Screw_ToleranceX And
                    element <> clsHMIGlobalParameter.Manual_Screw_ToleranceY And
                    element <> clsHMIGlobalParameter.Manual_Screw_ToleranceZ Then Continue For

            End If
            MachineListView_Parameter.Rows.Add(iCnt.ToString, cListParameterNewName(element), cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(element))
            iCnt = iCnt + 1
        Next


        MachineListView_Variant.Columns.Clear()
        MachineListView_Variant.Font = New System.Drawing.Font("Calibri", 10.0!)
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "VariantID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Variant.Columns.Add(PostTest_id)

        Dim PostTest_step As New DataGridViewTextBoxColumn
        PostTest_step.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "VariantName")
        PostTest_step.Name = "PostTest_name"
        '   PostTest_step.ReadOnly = True
        MachineListView_Variant.Columns.Add(PostTest_step)

        iCnt = 1
        For Each element As String In cMachineManager.VaiantElememtManager.ListElement
            MachineListView_Variant.Rows.Add(iCnt.ToString, element)
            iCnt = iCnt + 1
        Next

        MachineListView_MachineStatus.Rows.Clear()
        MachineListView_MachineStatus.Columns.Clear()
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_MachineStatus.Columns.Add(PostTest_id)

        PostTest_Device = New DataGridViewTextBoxColumn
        PostTest_Device.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Type")
        PostTest_Device.Name = "PostTest_Type"
        MachineListView_MachineStatus.Columns.Add(PostTest_Device)

        PostTest_Name = New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "Name")
        PostTest_Name.Name = "PostTest_Name"
        MachineListView_MachineStatus.Columns.Add(PostTest_Name)


        For Each element As String In cMachineManager.MachineStatusParameterManager.CurrentListElement.Keys
            MachineListView_MachineStatus.Rows.Add(MachineListView_MachineStatus.Rows.Count + 1, element, cMachineManager.MachineStatusParameterManager.CurrentListElement(element))
        Next

        TabPage_Config.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_Config")
        TabPage_Parameter.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_Parameter")
        TabPage_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_Variant")
        TabPage_ActionParameter.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_ActionParameter")
        TabPage_DeviceParameter.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_DeviceParameter")
        TabPage_MachineStatus.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "TabPage_MachineStatus")
        GroupBox_Parameter.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "GroupBox_Parameter")
        GroupBox_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)
        GroupBox_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "GroupBox_Variant")
        GroupBox_Variant.Font = New System.Drawing.Font("Calibri", 10.0!)

        TreeViewWidthKeyDown_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)

        GroupBox_Devices.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "GroupBox_Devices")
        GroupBox_Devices.Font = New System.Drawing.Font("Calibri", 10.0!)
        TreeViewWidthKeyDown_Devices.Font = New System.Drawing.Font("Calibri", 12.0!)
        TreeViewWidthKeyDown_Variant.Font = New System.Drawing.Font("Calibri", 12.0!)

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier Then
            TabControl_Body.Controls.Clear()
            TabControl_Body.Controls.Add(TabPage_Config)
            TabControl_Body.Controls.Add(TabPage_Parameter)
            TabControl_Body.Controls.Add(TabPage_VariantParameter)
            TabControl_Body.Controls.Add(TabPage_Variant)
            PostToolBar.Enabled = True
            ToolStripButton_AddVariantParameter.Enabled = True
            ToolStripButton_DelVariantParamete.Enabled = True
        End If

        ListView_Data_Variant_Resize(MachineListView_Variant, New EventArgs)
        Button_Save.Enabled = cMachineManager.IsChanged
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Project.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HmiLabel_Project")
        HmiLabel_Cell_Name.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HmiLabel_Cell_Name")
        HmiLabel_Cell_Description.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HmiLabel_Cell_Description")
        HmiLabel_Cell_Picture.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HmiLabel_Cell_Picture")
        HmiButton_Cell_Picture.Button.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "HmiButton_Cell_Picture")
        HmiTextBox_Cell_Picture.TextBoxReadOnly = True

        AddHandler HmiTextBox_Project.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_Project.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Cell_Picture.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Cell_Name.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Cell_Description.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        AddHandler ToolStripButton_AddVariantParameter.Click, AddressOf PostTest_AddVariantParameter_Click
        AddHandler ToolStripButton_DelVariantParamete.Click, AddressOf PostTest_DelVariantParameter_Click
        AddHandler PostTest_Down.Click, AddressOf PostTest_Down_Click
        AddHandler PostTest_Up.Click, AddressOf PostTest_Up_Click
        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler HmiButton_Cell_Picture.Button.Click, AddressOf Button_Choose_Click
        AddHandler MachineListView_Data.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        '    AddHandler MachineListView_Data.MouseMove, AddressOf MachineListView_Data_CellClick
        AddHandler MachineListView_Parameter.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        AddHandler MachineListView_Variant.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        AddHandler MachineListView_MachineStatus.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        AddHandler MachineListView_VariantParameter.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        AddHandler ListView_Data_Variant_Add.Click, AddressOf ListView_Data_Variant_Add_Click
        AddHandler ListView_Data_Variant_Del.Click, AddressOf ListView_Data_Variant_Del_Click
        AddHandler ListView_Data_Variant_Up.Click, AddressOf ListView_Data_Variant_Up_Click
        AddHandler ListView_Data_Variant_Down.Click, AddressOf ListView_Data_Variant_Down_Click
        AddHandler TreeViewWidthKeyDown_Parameter.AfterSelect, AddressOf TreeViewWidthKeyDown_Parameter_AfterSelect
        AddHandler TreeViewWidthKeyDown_Variant.AfterSelect, AddressOf TreeViewWidthKeyDown_Variant_AfterSelect
        AddHandler TreeViewWidthKeyDown_Devices.AfterSelect, AddressOf TreeViewWidthKeyDown_Devices_AfterSelect
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body_Config.RowStyles(0).Height = (HmiTextBox_Project.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Project.TextBox.Height + 6
            GroupBox_Project.Height = (HmiTextBox_Project.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Project.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Config_Head_Project.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Project.TextBox.Height + 6 + 6
            Next

            TableLayoutPanel_Body_Config.RowStyles(1).Height = (HmiTextBox_Project.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Project.TextBox.Height + 6
            GroupBox_Cell.Height = (HmiTextBox_Project.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Project.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Config_Head_Station.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Project.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Public Function CreateVariantPage() As Boolean
        TreeViewWidthKeyDown_Variant.Nodes.Clear()
        For Each elementIndex As String In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            TreeViewWidthKeyDown_Variant.Nodes.Add(element.Variant, element.Variant)
        Next
        Return True
    End Function
    Public Function CreateTabPage() As Boolean
        Try
            Panel_Parameter.Controls.Clear()
            TreeViewWidthKeyDown_Parameter.Nodes.Clear()
            For Each elementIndex As String In cActionLibManager.GetListDllKey
                Dim element As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(elementIndex)
                TreeViewWidthKeyDown_Parameter.Nodes.Add(element.ActionName, element.ActionName)
            Next

            Panel_Devices.Controls.Clear()
            TreeViewWidthKeyDown_Devices.Nodes.Clear()
            For Each elementIndex As String In cDeviceLibManager.GetListDllKey
                Dim element As clsDeviceLibCfg = cDeviceLibManager.GetDeviceLibCfgFromKey(elementIndex)
                If IsNothing(element.Source) Then Continue For
                element.Source.CreateParameterUI(cLocalElement, cSystemElement)
                If IsNothing(element.Source.ParameterUI) Then Continue For
                TreeViewWidthKeyDown_Devices.Nodes.Add(element.DeviceName, element.DeviceName)
            Next

            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentDevicesForm.ToString))
            Return False
        End Try
    End Function

    Private Sub TreeViewWidthKeyDown_Parameter_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        Try
            Dim element As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(TreeViewWidthKeyDown_Parameter.SelectedNode.Text)
            If Not IsNothing(element) Then
                If Not IsNothing(cHMIActionBase) Then
                    RemoveHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
                End If
                cHMIActionBase = element.Source
                If IsNothing(cHMIActionBase) Then Return
                AddHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
                cHMIActionBase.CreateParameterUI(cLocalElement, cSystemElement)
                If IsNothing(cHMIActionBase.ParameterUI) Then Return
                cHMIActionBase.ParameterUI.Init(cLocalElement, cSystemElement)
                cHMIActionBase.ParameterUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cMachineManager.ActionParameterManager.CurrentListElement(element.ActionName)))
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, cHMIActionBase.ParameterUI.UI)
                Panel_Parameter.Controls.Clear()
                Panel_Parameter.Controls.Add(cHMIActionBase.ParameterUI.UI)
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub TreeViewWidthKeyDown_Variant_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        Try
            lRowListValue.Clear()
            Dim cListValueCfg As New clsListValueCfg
            Dim tempListObject As New List(Of Object)
            tempListObject = New List(Of Object)
            MachineListView_VariantParameter.Rows.Clear()
            For Each element As String In cMachineManager.MachineVariantParameter.GetMachineGlobalParameterFromKey(TreeViewWidthKeyDown_Variant.SelectedNode.Text).GetCurrentMachineGlobalParameterListKey
                If cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(element).ValueList.Count > 0 Then
                    cListValueCfg = New clsListValueCfg
                    cListValueCfg.ListValue = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(element).ValueList
                    cListValueCfg.ListType = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(element).ValueType
                Else
                    cListValueCfg = New clsListValueCfg
                    cListValueCfg.ListValue = New List(Of Object)
                    cListValueCfg.ListType = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(element).ValueType

                End If
                lRowListValue.Add(element, cListValueCfg)
            Next

            Dim lColumnListValue As New Dictionary(Of String, clsListValueCfg)
            If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Supplier Then
                cListValueCfg = New clsListValueCfg
                tempListObject = New List(Of Object)
                For Each element As clsGlobalParameterCfg In cMachineManager.MachineVariantParameter.HMIVariantParameter.GetUserDefinedKeys
                    tempListObject.Add(element.Name.ToString)
                Next
                cListValueCfg.ListValue = tempListObject
                lColumnListValue.Add("Parameter_Name", cListValueCfg)
            End If


            Dim iCnt As Integer = 1
            MachineListView_VariantParameter.lRowListValue = lRowListValue
            MachineListView_VariantParameter.lColumnListValue = lColumnListValue
            For Each element As String In cMachineManager.MachineVariantParameter.GetMachineGlobalParameterFromKey(TreeViewWidthKeyDown_Variant.SelectedNode.Text).GetCurrentMachineGlobalParameterListKey
                MachineListView_VariantParameter.Rows.Add(iCnt.ToString, cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, element), cMachineManager.MachineVariantParameter.GetMachineGlobalParameterFromKey(TreeViewWidthKeyDown_Variant.SelectedNode.Text).GetCurrentMachineGlobalParameterFromKey(element))
                iCnt = iCnt + 1
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub TreeViewWidthKeyDown_Devices_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        Try
            Dim element As clsDeviceLibCfg = cDeviceLibManager.GetDeviceLibCfgFromKey(TreeViewWidthKeyDown_Devices.SelectedNode.Text)
            If Not IsNothing(element) Then
                If Not IsNothing(cHMIDeviceBase) Then
                    RemoveHandler cHMIDeviceBase.ParameterChanged, AddressOf ParameterChanged
                End If
                cHMIDeviceBase = element.Source
                If IsNothing(cHMIDeviceBase) Then Return
                AddHandler cHMIDeviceBase.ParameterChanged, AddressOf ParameterChanged
                cHMIDeviceBase.CreateParameterUI(cLocalElement, cSystemElement)
                If IsNothing(cHMIDeviceBase.ParameterUI) Then Return
                cHMIDeviceBase.ParameterUI.Init(cLocalElement, cSystemElement)
                cHMIDeviceBase.ParameterUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cMachineManager.DeviceParameterManager.CurrentListElement(element.DeviceName)))
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, cHMIDeviceBase.ParameterUI.UI)
                Panel_Devices.Controls.Clear()
                Panel_Devices.Controls.Add(cHMIDeviceBase.ParameterUI.UI)
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Public Sub ParameterChanged(ByVal sender As Object, ByVal e As Kochi.HMI.MainControl.Device.ParameterEvent)
        Try
            cMachineManager.DeviceParameterManager.ChangeCurrentData(TreeViewWidthKeyDown_Devices.SelectedNode.Text, clsParameter.ToString(e.ListParameter))
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Public Sub ParameterChanged(ByVal sender As Object, ByVal e As Kochi.HMI.MainControl.Action.ParameterEvent)
        Try
            cMachineManager.ActionParameterManager.ChangeCurrentData(TreeViewWidthKeyDown_Parameter.SelectedNode.Text, clsParameter.ToString(e.ListParameter))
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Project"
                cMachineManager.MachineCellManager.CurrentMachineCfg.ProjectName = CType(sender, TextBox).Text
            Case "HmiTextBox_Cell_Name"
                cMachineManager.MachineCellManager.CurrentMachineCfg.CellName = CType(sender, TextBox).Text
            Case "HmiTextBox_Cell_Picture"
                cMachineManager.MachineCellManager.CurrentMachineCfg.CellPicture = CType(sender, TextBox).Text
            Case "HmiTextBox_Cell_Description"
                cMachineManager.MachineCellManager.CurrentMachineCfg.CellDescription = CType(sender, TextBox).Text
        End Select
        Button_Save.Enabled = cMachineManager.IsChanged
    End Sub


    Private Sub ListView_Data_Variant_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_Variant.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_Variant.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Variant.Width / 100) * 20
                Case "PostTest_name"
                    element.Width = (MachineListView_Variant.Width / 100) * 80
            End Select
        Next
    End Sub

    Private Sub ListView_Data_Variant_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cMachineManager.VaiantElememtManager.AddCurrentData()
        MachineListView_Variant.Rows.Add((MachineListView_Variant.Rows.Count + 1).ToString, "", "")
        Button_Save.Enabled = cMachineManager.IsChanged
    End Sub

    Private Sub ListView_Data_Variant_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Variant.CurrentRow Is Nothing Then Return
        cMachineManager.VaiantElememtManager.DeleteCurrentData(MachineListView_Variant.CurrentRow.Index)
        MachineListView_Variant.Rows.Remove(MachineListView_Variant.CurrentRow)
        For i = 0 To MachineListView_Variant.Rows.Count - 1
            MachineListView_Variant.Rows(i).Cells(0).Value = (i + 1).ToString
        Next
        Button_Save.Enabled = cMachineManager.IsChanged
    End Sub

    Private Sub ListView_Data_Variant_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Variant.CurrentRow Is Nothing Then Return
            If MachineListView_Variant.CurrentRow.Index = 0 Then Return
            cMachineManager.VaiantElememtManager.UpRowCurrentData(MachineListView_Variant.CurrentRow.Index)
            Dim iID As Integer = MachineListView_Variant.CurrentRow.Index + 1
            UpRow(iID, MachineListView_Variant)
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub ListView_Data_Variant_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Variant.CurrentRow Is Nothing Then Return
            If MachineListView_Variant.CurrentRow.Index = MachineListView_Variant.Rows.Count - 1 Then Return
            cMachineManager.VaiantElememtManager.DownRowCurrentData(MachineListView_Variant.CurrentRow.Index)
            Dim iID As Integer = MachineListView_Variant.CurrentRow.Index + 1
            DownRow(iID, MachineListView_Variant)
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim tempMachineStationCfg = cMachineManager.MachineCellManager.AddCurrentMachineStation()
            MachineListView_Data.Rows.Add(tempMachineStationCfg.ID, tempMachineStationCfg.MachineStationType.ToString, tempMachineStationCfg.StationName, tempMachineStationCfg.VariantType.ToString, tempMachineStationCfg.CheckArticleType.ToString, tempMachineStationCfg.CheckSNType.ToString, tempMachineStationCfg.AutoChangePage.ToString, tempMachineStationCfg.HMIError.ToString, tempMachineStationCfg.Index.ToString, tempMachineStationCfg.ShowWaitingMessage.ToString, tempMachineStationCfg.AutoConfirm.ToString, tempMachineStationCfg.FailAutoRun.ToString, tempMachineStationCfg.FailRunNextStation.ToString, tempMachineStationCfg.MES.ToString, tempMachineStationCfg.Estop.ToString, tempMachineStationCfg.NotInqueueColor.ToString, tempMachineStationCfg.CheckCarrier.ToString, tempMachineStationCfg.ResetCarrier.ToString, tempMachineStationCfg.Description, tempMachineStationCfg.CompleteStep)

            Dim cListValueCfg As New clsListValueCfg
            Dim tempListObject As New List(Of Object)
            tempListObject = New List(Of Object)
            For i = 1 To cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationListKey.Count
                tempListObject.Add(i.ToString)
            Next
            cListValueCfg = New clsListValueCfg
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            MachineListView_Data.lColumnListValue("PostTest_Index") = cListValueCfg
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Data.CurrentRow Is Nothing Then Return
            Dim iID As Integer = MachineListView_Data.CurrentRow.Index + 1
            MachineListView_Data.Rows.Remove(MachineListView_Data.CurrentRow)
            cMachineManager.MachineCellManager.DeleteCurrentMachineStation(iID)
            For Each t As DataGridViewRow In MachineListView_Data.Rows
                t.Cells(0).Value = (t.Index + 1).ToString
            Next
            Dim cListValueCfg As New clsListValueCfg
            Dim tempListObject As New List(Of Object)
            tempListObject = New List(Of Object)
            For i = 1 To cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationListKey.Count
                tempListObject.Add(i.ToString)
            Next
            cListValueCfg = New clsListValueCfg
            cListValueCfg.ListValue = tempListObject
            cListValueCfg.ListType = GetType(Integer)
            MachineListView_Data.lColumnListValue("PostTest_Index") = cListValueCfg
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub


    Private Sub PostTest_AddVariantParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If TreeViewWidthKeyDown_Variant.SelectedNode Is Nothing Then Return
            MachineListView_VariantParameter.Rows.Add((MachineListView_VariantParameter.Rows.Count + 1).ToString, "", "")
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub PostTest_DelVariantParameter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If TreeViewWidthKeyDown_Variant.SelectedNode Is Nothing Then Return
            If MachineListView_VariantParameter.CurrentRow Is Nothing Then Return
            cMachineManager.MachineVariantParameter.DelCurrentGlobalParameter(TreeViewWidthKeyDown_Variant.SelectedNode.Text, MachineListView_VariantParameter.Rows(MachineListView_VariantParameter.CurrentRow.Index).Cells(1).Value)
            MachineListView_VariantParameter.Rows.Remove(MachineListView_VariantParameter.CurrentRow)
            For Each t As DataGridViewRow In MachineListView_VariantParameter.Rows
                t.Cells(0).Value = (t.Index + 1).ToString
            Next
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub PostTest_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Data.CurrentRow Is Nothing Then Return
            If MachineListView_Data.CurrentRow.Index = 0 Then Return

            Dim iID As Integer = MachineListView_Data.CurrentRow.Index + 1
            If iID <= 1 Then Return
            cMachineManager.MachineCellManager.UpRowCurrentMachineStation(iID)
            UpRowWithIndex(iID, MachineListView_Data)
            For Each i In cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationListKey
                MachineListView_Data.Rows(i).Cells(8).Value = cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromKey(i).Index
            Next
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub PostTest_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Data.CurrentRow Is Nothing Then Return
            If MachineListView_Data.CurrentRow.Index = MachineListView_Data.Rows.Count - 1 Then Return
            Dim iID As Integer = MachineListView_Data.CurrentRow.Index + 1
            If iID >= MachineListView_Data.Rows.Count Then Return

            cMachineManager.MachineCellManager.DownRowCurrentMachineStation(iID)
            DownRowWithIndex(iID, MachineListView_Data)
            For Each i In cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationListKey
                MachineListView_Data.Rows(i).Cells(8).Value = cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromKey(i).Index
            Next
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub UpRow(ByVal id As Integer, ByRef v As DataGridView)
        If id <= 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id - 1).ToString
        v.Rows(id - 2).Cells(0).Value = id.ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id - 2, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub


    Private Sub UpRowWithIndex(ByVal id As Integer, ByRef v As DataGridView)
        If id <= 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id - 1).ToString
        v.Rows(id - 2).Cells(0).Value = id.ToString
        v.Rows(id - 1).Cells(8).Value = v.Rows(id - 1).Cells(0).Value
        v.Rows(id - 2).Cells(8).Value = v.Rows(id - 2).Cells(0).Value
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id - 2, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub

    Private Sub DownRow(ByVal id As Integer, ByRef v As DataGridView)
        If id > v.Rows.Count - 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id + 1).ToString
        v.Rows(id).Cells(0).Value = (id).ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub

    Private Sub DownRowWithIndex(ByVal id As Integer, ByRef v As DataGridView)
        If id > v.Rows.Count - 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id + 1).ToString
        v.Rows(id).Cells(0).Value = (id).ToString
        v.Rows(id - 1).Cells(8).Value = v.Rows(id - 1).Cells(0).Value
        v.Rows(id).Cells(8).Value = v.Rows(id).Cells(8).Value
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub
    Private Sub MachineListView_Data_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Private Sub MachineListView_Data_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            Select Case sender.name
                Case "MachineListView_Data"
                    If MachineListView_Data.CurrentCell Is Nothing Then Return
                    Dim Obj As Object = MachineListView_Data.CurrentRow.Cells(e.ColumnIndex).Value
                    Select Case MachineListView_Data.Columns(e.ColumnIndex).Name
                        Case "PostTest_id"
                            Return
                        Case "PostTest_Type"
                            cMachineManager.MachineCellManager.ChangeCurrentMachineStationType(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumMachineStationType), Obj))
                        Case "PostTest_Variant"
                            cMachineManager.MachineCellManager.ChangeCurrentVariantType(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumMachineStationType), Obj))
                        Case "PostTest_CheckArticle"
                            cMachineManager.MachineCellManager.ChangeCurrentCheckArticleType(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_CheckSN"
                            cMachineManager.MachineCellManager.ChangeCurrentCheckSNType(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_AutoChangePage"
                            cMachineManager.MachineCellManager.ChangeCurrentMachineStationChangePage(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, CInt(Obj))
                        Case "PostTest_HMIError"
                            cMachineManager.MachineCellManager.ChangeCurrentMachineStationHMIError(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)
                        Case "PostTest_Index"
                            cMachineManager.MachineCellManager.ChangeCurrentMachineStationIndex(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, CInt(Obj))
                        Case "PostTest_ShowWaitingMessage"
                            cMachineManager.MachineCellManager.ChangeCurrentShowWaitingMessage(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_AutoConfirm"
                            cMachineManager.MachineCellManager.ChangeCurrentAutoConfirm(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_FailAutoRun"
                            cMachineManager.MachineCellManager.ChangeCurrentFailAutoRun(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_FailAutoRunNext"
                            cMachineManager.MachineCellManager.ChangeCurrentFailRunNextStation(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_MES"
                            cMachineManager.MachineCellManager.ChangeCurrentMES(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)
                        Case "PostTest_NotInqueue"
                            cMachineManager.MachineCellManager.ChangeCurrentNotInqueueColor(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)
                        Case "PostTest_ESTOP"
                            cMachineManager.MachineCellManager.ChangeCurrentESTOP(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_Carrier"
                            cMachineManager.MachineCellManager.ChangeCurrentCarrier(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_ResetCarrier"
                            cMachineManager.MachineCellManager.ChangeCurrentResetCarrier(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, [Enum].Parse(GetType(enumCheckType), Obj))
                        Case "PostTest_Complete"
                            cMachineManager.MachineCellManager.ChangeCurrentComplete(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)

                        Case "PostTest_Name"
                            If Not cMachineManager.MachineCellManager.HasCurrentMachineStation(Obj.ToString) Then
                                cMachineManager.MachineCellManager.ChangeCurrentMachineStationName(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)
                            Else
                                iErrorPosition = 1
                                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "4", MachineListView_Data.CurrentRow.Cells(0).Value.ToString), enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
                            End If
                        Case "PostTest_Description"
                            cMachineManager.MachineCellManager.ChangeCurrentMachineStationDescription(MachineListView_Data.CurrentRow.Cells(0).Value.ToString, Obj.ToString)
                    End Select

                Case "MachineListView_Parameter"
                    If MachineListView_Parameter.CurrentCell Is Nothing Then Return
                    Dim Obj As Object = MachineListView_Parameter.CurrentRow.Cells(e.ColumnIndex).Value
                    Select Case MachineListView_Parameter.Columns(e.ColumnIndex).Name
                        Case "Parameter_id"
                            Return
                        Case "Parameter_Name"
                            Return
                        Case "Parameter_Value"
                            cMachineManager.MachineGlobalParameter.SetCurrentGlobalParameter(cListParameterOldName(MachineListView_Parameter.CurrentRow.Cells(1).Value.ToString), Obj.ToString)
                    End Select

                Case "MachineListView_Variant"
                    If MachineListView_Variant.CurrentCell Is Nothing Then Return
                    Dim Obj As Object = MachineListView_Variant.CurrentRow.Cells(e.ColumnIndex).Value
                    Select Case MachineListView_Variant.Columns(e.ColumnIndex).Name
                        Case "PostTest_id"
                            Return
                        Case "PostTest_name"
                            If Not cMachineManager.VaiantElememtManager.HasCurrentData(MachineListView_Variant.CurrentRow.Index, Obj.ToString) Then
                                cMachineManager.VaiantElememtManager.ChangeCurrentData(MachineListView_Variant.CurrentRow.Index, Obj.ToString)
                            Else
                                iErrorPosition = 2
                                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "5", (MachineListView_Variant.CurrentRow.Index + 1).ToString), enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
                            End If
                    End Select

                Case "MachineListView_MachineStatus"
                    If MachineListView_MachineStatus.CurrentCell Is Nothing Then Return
                    Dim Obj As Object = MachineListView_MachineStatus.CurrentRow.Cells(e.ColumnIndex).Value
                    If IsNothing(Obj) Then Obj = ""
                    Select Case MachineListView_MachineStatus.Columns(e.ColumnIndex).Name
                        Case "PostTest_Name"
                            cMachineManager.MachineStatusParameterManager.ChangeCurrentData(MachineListView_MachineStatus.CurrentRow.Cells(1).Value, Obj.ToString)
                    End Select

                Case "MachineListView_VariantParameter"
                    If MachineListView_VariantParameter.CurrentCell Is Nothing Then Return
                    Dim Obj As Object = MachineListView_VariantParameter.CurrentRow.Cells(e.ColumnIndex).Value
                    Select Case MachineListView_VariantParameter.Columns(e.ColumnIndex).Name
                        Case "Parameter_id"
                            Return
                        Case "Parameter_Name"
                            If MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString = "" Then Return
                            Dim cListValueCfg As New clsListValueCfg
                            Dim tempListObject As New List(Of Object)
                            tempListObject = New List(Of Object)
                            Dim lRowListValue As New Dictionary(Of String, clsListValueCfg)

                            If cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString).ValueList.Count > 0 Then
                                cListValueCfg = New clsListValueCfg
                                cListValueCfg.ListValue = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString).ValueList
                                cListValueCfg.ListType = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString).ValueType
                            Else
                                cListValueCfg = New clsListValueCfg
                                cListValueCfg.ListValue = New List(Of Object)
                                cListValueCfg.ListType = cMachineManager.MachineVariantParameter.HMIVariantParameter.GetParameterKeysFromName(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString).ValueType

                            End If
                            If lRowListValue.ContainsKey(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString) Then
                                lRowListValue(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString) = cListValueCfg
                            Else
                                lRowListValue.Add(MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString, cListValueCfg)
                            End If
                            MachineListView_VariantParameter.lRowListValue = lRowListValue
                            cMachineManager.MachineVariantParameter.AddCurrentGlobalParameter(TreeViewWidthKeyDown_Variant.SelectedNode.Text, Obj.ToString)
                        Case "Parameter_Value"
                            If MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString = "" Then Return
                            cMachineManager.MachineVariantParameter.SetCurrentGlobalParameter(TreeViewWidthKeyDown_Variant.SelectedNode.Text, MachineListView_VariantParameter.CurrentRow.Cells(1).Value.ToString, Obj.ToString)
                    End Select


            End Select
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception

            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Button_Save.Enabled = False
            SaveFunction()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub Button_Choose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                       "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                        "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
            OpenFileDialog_Path.RestoreDirectory = True
            OpenFileDialog_Path.FilterIndex = 1
            If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
                HmiTextBox_Cell_Picture.TextBox.Text = OpenFileDialog_Path.FileName
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_Parameter.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_Parameter.Columns
            Select Case element.Name
                Case "Parameter_id"
                    element.Width = (MachineListView_Parameter.Width / 100) * 20
                Case "Parameter_Name"
                    element.Width = (MachineListView_Parameter.Width / 100) * 50
                Case "Parameter_Value"
                    element.Width = (MachineListView_Parameter.Width / 100) * 30
            End Select
        Next
    End Sub

    Private Sub MachineListView_VariantParameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_VariantParameter.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_VariantParameter.Columns
            Select Case element.Name
                Case "Parameter_id"
                    element.Width = (MachineListView_VariantParameter.Width / 100) * 20
                Case "Parameter_Name"
                    element.Width = (MachineListView_VariantParameter.Width / 100) * 50
                Case "Parameter_Value"
                    element.Width = (MachineListView_VariantParameter.Width / 100) * 30
            End Select
        Next
    End Sub

    Private Sub MachineListView_MachineStatus_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_MachineStatus.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_MachineStatus.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_MachineStatus.Width / 100) * 30
                Case "PostTest_Type"
                    element.Width = (MachineListView_MachineStatus.Width / 100) * 30
                Case "PostTest_Name"
                    element.Width = (MachineListView_MachineStatus.Width / 100) * 40
            End Select
        Next
    End Sub

    Private Sub SaveFunction()
        Try
            If cMachineManager.CheckCurrentCfg Then
                If cMachineManager.SaveCurrentCfg() Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "1"), enumExceptionType.Normal, enumUIName.ChildrenSystemParameterForm.ToString))
                Else
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "2"), enumExceptionType.Normal, enumUIName.ChildrenSystemParameterForm.ToString))
                End If
                cDeviceManager.SaveCurrentDeviceCfg()
                mMainForm.ShowTitle()
                mMainForm.ShowCell()
                mMainForm.ShowMes()
                cLanguageManager.LoadActiveLanguage()
                cProgramButton.LoadData()
                cProgramCylinderButton.LoadData()
                mParentMainForm.EnableMainLeftButton()
                cMainFormButtonManager.DisableMainLeftButtonEx()
                Button_Save.Enabled = cMachineManager.IsChanged
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Private Sub AbortFunction()
        Try
            cMachineManager.CancelCurrentCfg()
            CreateTabPage()
            CreateVariantPage()
            InitForm()
            Button_Save.Enabled = cMachineManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
        End Try
    End Sub

    Sub ResetFunction()
        If iErrorPosition = 1 Then
            iErrorPosition = 0
            InitForm()
            Button_Save.Enabled = cMachineManager.IsChanged
        End If

        If iErrorPosition = 2 Then
            iErrorPosition = 0
            If Not IsNothing(MachineListView_Variant.CurrentRow) Then
                MachineListView_Variant.CurrentRow.Cells(1).Value = cMachineManager.VaiantElememtManager.CurrentListElement(MachineListView_Variant.CurrentRow.Index)
            End If
            Button_Save.Enabled = cMachineManager.IsChanged
        End If
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        Try
            If cMachineManager.IsChanged Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSystemParameterForm.ToString, "3"), enumExceptionType.Confirm, enumUIName.ChildrenSystemParameterForm.ToString))
                Return False
            End If

            For Each elementIndex As String In cActionLibManager.GetListDllKey
                Dim element As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(elementIndex)
                cHMIActionBase = element.Source
                If IsNothing(cHMIActionBase) Then Continue For
                RemoveHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
            Next

            For Each elementIndex As String In cDeviceLibManager.GetListDllKey
                Dim element As clsDeviceLibCfg = cDeviceLibManager.GetDeviceLibCfgFromKey(elementIndex)
                cHMIDeviceBase = element.Source
                If IsNothing(cHMIDeviceBase) Then Continue For
                RemoveHandler cHMIDeviceBase.ParameterChanged, AddressOf ParameterChanged
            Next

            cLocalElement.Remove(enumUIName.ChildrenSystemParameterForm.ToString)
            cErrorMessageManager.Clean(enumUIName.ChildrenSystemParameterForm.ToString)
            cErrorMessageManager.DisposeResetFunction()
            cErrorMessageManager.DisposeAbortFunction()
            cErrorMessageManager.DisposeSaveFunction()
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
            Return False
        End Try
    End Function
End Class