Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Action
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ParentProgramForm
    Implements IParentProgramUI
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private mMainForm As MainForm
    Private cHMIPLC As clsHMIPLC
    Private cErrorMessageManager As clsErrorMessageManager
    Private cVariantManager As clsVariantManager
    Private cFormFontResize As clsFormFontResize
    Private cChangePage As clsChangePage
    Private cActionLibManager As clsActionLibManager
    Private cMachineManager As clsMachineManager
    Private cHMIActionBase As clsHMIActionBase
    Private cActionManager As clsActionManager
    Private cTextManager As clsTextManager
    Private cPictureManager As clsPictureManager
    Private cDeviceManager As clsDeviceManager
    Private IActionUI As IActionUI
    Private cLanguageManager As clsLanguageManager
    Private cUserManager As clsUserManager
    Private lListTreeView As New Dictionary(Of String, TreeViewWidthCheckBox)
    Private cThread As Thread
    Private bExit As Boolean
    Private strButtonName As String
    Private cLocalFormFontResize As clsFormFontResize
    Private cSystemManager As clsSystemManager
    Private lListStatusButton As New Dictionary(Of enumProgramButtonType, Boolean)
    Private strLastType As String = String.Empty
    Private strLastParameter As String = String.Empty
    Private cnodeFont As Font
    Private focusBounds As Rectangle
    Private cGlobalProgramManager As clsGlobalProgramManager
    Private cProgramDebug As ProgramDebug
    Private _Object As New Object
    Private cVariantCfg As New clsVariantCfg
    Private cLocalVariant As clsVariantManager
    Public Property ButtonName As String Implements IParentProgramUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IParentProgramUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public ReadOnly Property LocalElement As Dictionary(Of String, Object) Implements IParentUI.LocalElement
        Get
            Return cLocalElement
        End Get
    End Property

    Public ReadOnly Property ComboBox_ActionType As HMIComboBox Implements IParentProgramUI.ComboBox_ActionType
        Get
            Return HmiComboBox_ActionType
        End Get
    End Property

    Public ReadOnly Property TextBox_Component As HMITextBoxWithButtonAnd2Layer Implements IParentProgramUI.TextBox_Component
        Get
            Return HmiTextBox_Component
        End Get
    End Property

    Public ReadOnly Property TextBox_Description As HMITextBoxWithButtonAnd2Layer Implements IParentProgramUI.TextBox_Description
        Get
            Return HmiTextBox_Description
        End Get
    End Property

    Public ReadOnly Property TextBox_Number As HMITextBox Implements IParentProgramUI.TextBox_Number
        Get
            Return HmiTextBox_Number
        End Get
    End Property

    Public ReadOnly Property TextBox_Picture As HMITextBoxWithButtonAnd2Layer Implements IParentProgramUI.TextBox_Picture
        Get
            Return HmiTextBox_Picture
        End Get
    End Property
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentProgramUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cErrorMessageManager = New clsErrorMessageManager
            cErrorMessageManager.Init(cSystemElement)
            cErrorMessageManager.RegisterManager(TableLayoutPanel)
            cLocalFormFontResize = New clsFormFontResize
            cLocalElement.Add(clsFormFontResize.Name, cLocalFormFontResize)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cActionManager = CType(cSystemElement(clsActionManager.Name), clsActionManager)
            cTextManager = CType(cSystemElement(clsTextManager.Name), clsTextManager)
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
            cErrorMessageManager.RegisterSaveFunction(AddressOf SaveFunction)
            cErrorMessageManager.RegisterAbortFunction(AddressOf AbortFunction)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cGlobalProgramManager = CType(cSystemElement(clsGlobalProgramManager.Name), clsGlobalProgramManager)
            cHMIPLC = cDeviceManager.GetPLCDevice()

            cLocalVariant = New clsVariantManager
            cLocalVariant.Init(cSystemElement)
            cLocalVariant.LoadVariantCfg()

            If cLocalElement.ContainsKey(clsVariantManager.Name) Then
                cLocalElement(clsVariantManager.Name) = cLocalVariant
            Else
                cLocalElement.Add(clsVariantManager.Name, cLocalVariant)
            End If

            cChangePage = New clsChangePage
            cChangePage.Init(cLocalElement, cSystemElement)
            cChangePage.ErrorMessageManager = cErrorMessageManager
            cChangePage.RegisterManager(Panel_UI_Bottom, Me.Panel_UI)
            InitForm()
            InitControlText()
            cThread = New Thread(AddressOf RefreshUI)
            cThread.IsBackground = True
            cThread.Start()
            cLocalElement.Add(clsChangePage.Name, cChangePage)
            cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
            cLocalElement.Add(enumUIName.ParentProgramForm.ToString, Me)
            cProgramDebug = New ProgramDebug
            cProgramDebug.Init(cLocalElement, cSystemElement)
            TabPage2.Controls.Add(cProgramDebug.UI)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ParentProgramForm.ToString))
            Return False
        End Try
        Return False
    End Function

    Public Function InitForm() As Boolean
        Panel_Right.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        Panel_Mid.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False

        HmiComboBox_CopyVariant.ComboBox.SelectedIndex = -1
        HmiComboBox_CopyVariant.ComboBox.Enabled = False
        HmiButton_CopyConfirm.Button.Enabled = False

        HmiComboBox_StationCopy.ComboBox.SelectedIndex = -1
        HmiComboBox_StationCopy.ComboBox.Enabled = False
        HmiButton_StationCopy.Button.Enabled = False

        HmiComboBox_ExchangeOrigin.ComboBox.SelectedIndex = -1
        HmiComboBox_ExchangeOrigin.ComboBox.Enabled = False
        HmiComboBox_ExchangeTarget.ComboBox.SelectedIndex = -1
        HmiComboBox_ExchangeTarget.ComboBox.Enabled = False
        HmiButton_ExchangeCopy.Button.Enabled = False
        Button_SaveAs.Enabled = False

        Dim lListKey As New Dictionary(Of String, String)


        lListKey.Clear()
        lListKey.Add(clsHMIGlobalParameter.Manual_Screw_Repeat, cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Manual_Screw_Repeat))
        lListKey.Add("Continue", "Continue")
        HmiTextBox_Repeat.RegisterButton(lListKey, enumInsertType.Replace)
        For Each element In [Enum].GetValues(GetType(enumProgramButtonType))
            lListStatusButton.Add(element, True)
        Next
        CreateTabPage()
        CleanData()

        HmiComboBox_Variant.ComboBox.Items.Clear()
        Dim iCnt As Integer = 0

        For Each element In cGlobalProgramManager.GetGlobalProgramListKey
            Dim cGlobalProgramCfg As clsGlobalProgramCfg = cGlobalProgramManager.GetGlobalProgramCfgFromKey(element)
            HmiComboBox_Variant.ComboBox.Items.Add(cGlobalProgramCfg.GlobalProgram)
            iCnt = iCnt + 1
        Next

        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
            If cVariantManager.CurrentVariantCfg.Variant = element.Variant Then
                HmiComboBox_Variant.ComboBox.SelectedIndex = iCnt
                LoadData(True)
            End If
            iCnt = iCnt + 1
        Next
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Confirm.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiButton_Confirm")
        HmiButton_Confirm.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_CopyConfirm.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiButton_CopyConfirm")
        HmiButton_CopyConfirm.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_StationCopy.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiButton_StationCopy")
        HmiButton_StationCopy.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_ExchangeCopy.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiButton_ExchangeCopy")
        HmiButton_ExchangeCopy.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_CopyVariant.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_StationCopy.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_ExchangeOrigin.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_ExchangeTarget.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_CopyVariant.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_CopyVariant")
        HmiLabel_CopyVariant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_StationCopy.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_StationCopy")
        HmiLabel_StationCopy.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Exchange.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Exchange")
        HmiLabel_Exchange.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        TabControl_Program.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage1.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage2.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage1.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "TabPage1")
        TabPage2.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "TabPage2")
        TabPage1.Name = "Action"
        TabPage2.Name = "Debug"

        HmiLabel_ID.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_ID")
        HmiLabel_ID.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Number.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Number")
        HmiLabel_Number.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Component.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Component")
        HmiLabel_Component.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Repeat.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Repeat")
        HmiLabel_Repeat.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Picture.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Picture")
        HmiLabel_Picture.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Type.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Type")
        HmiLabel_Type.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Detail.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Detail")
        HmiLabel_Detail.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_Y.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "RadioButton_Y")
        RadioButton_Y.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_N.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "RadioButton_N")
        RadioButton_N.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Number.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Description.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Description2.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Picture.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Component.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Repeat.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Repeat.TextBox.Name = "HmiTextBox_Repeat"
        HmiComboBox_ActionType.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Choose.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_ID.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ID.TextBoxReadOnly = True


        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "Button_Save")
        Button_SaveAs.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "Button_SaveAs")
        GroupBox_Ts.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "GroupBox_Ts")
        GroupBox_Parameter.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "GroupBox_Parameter")
        ' GroupBox_Property.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "GroupBox_Property")
        GroupBox_Action.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "GroupBox_Action")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "Button_Save")
        HmiButton_Choose.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiButton_Choose")
        Button_Save.Enabled = cActionManager.IsChanged
        ' HmiTextBox_Picture.TextBox.ReadOnly = True
        HmiButton_Confirm.Button.Enabled = False
        HmiButton_CopyConfirm.Button.Enabled = False
        ' HmiTextBox_Picture.TextBox.ReadOnly = False
        If cLanguageManager.SecondLanguageEnable Then
            HmiLabel_Description.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Description1", cLanguageManager.FirtLanguage)
            HmiLabel_Description.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
            HmiLabel_Description2.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Description2", cLanguageManager.SecondLanguage)
            HmiLabel_Description2.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        Else
            HmiLabel_Description.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Description3")
            HmiLabel_Description.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
            HmiLabel_Description2.Text = cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "HmiLabel_Description3")
            HmiLabel_Description2.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        End If

        If cMachineManager.MachineStatus.bulPowerON Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode", False)
            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", True)
        End If
        AddHandler HmiTextBox_Number.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiComboBox_ActionType.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_CopyVariant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_StationCopy.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_ExchangeOrigin.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_ExchangeTarget.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiButton_Choose.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Confirm.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_CopyConfirm.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Number.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Description.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Description2.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Picture.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Component.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Repeat.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler Button_Add.Click, AddressOf Button_Click
        AddHandler Button_Del.Click, AddressOf Button_Click
        AddHandler Button_Up.Click, AddressOf Button_Click
        AddHandler Button_Down.Click, AddressOf Button_Click
        AddHandler Button_Save.Click, AddressOf Button_Click
        AddHandler Button_SaveAs.Click, AddressOf Button_Click
        AddHandler HmiButton_StationCopy.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_ExchangeCopy.Button.Click, AddressOf Button_Click
        AddHandler TabControl_TS.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
        AddHandler PopupCopy.Click, AddressOf Copy_Click
        AddHandler PopupPaste.Click, AddressOf Paste_Click
        AddHandler PopupPasteAll.Click, AddressOf Paste_All_Click
        AddHandler RadioButton_Y.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler TabControl_Program.SelectedIndexChanged, AddressOf TabControl_Program_SelectedIndexChanged
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim iCnt As Integer = 0
            For i = 0 To lListStatusButton.Count - 1
                If lListStatusButton(lListStatusButton.Keys(i)) Then
                    iCnt = iCnt + 1
                End If
            Next

            For Each element As RowStyle In TableLayoutPanel_Right_Item.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Number.TextBox.Height + 6 + 6
            Next
            If cLanguageManager.SecondLanguageEnable Then
                TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6) * iCnt + 26
                ' GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height + 26
            Else
                TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6) * iCnt + 10
                '  GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height + 2
            End If
            iCnt = 1
            For Each element As RowStyle In TableLayoutPanel_Body_Mid_Head.RowStyles
                If lListStatusButton.ContainsKey(iCnt) Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    If lListStatusButton(iCnt) Then
                        If cLanguageManager.SecondLanguageEnable Then
                            If iCnt = 3 Or iCnt = 4 Then
                                element.Height = HmiTextBox_Number.TextBox.Height + 6 + 12
                            Else
                                element.Height = HmiTextBox_Number.TextBox.Height + 6
                            End If
                        Else
                            element.Height = HmiTextBox_Number.TextBox.Height + 6
                        End If
                    Else
                        element.Height = 0
                    End If
                Else
                    element.SizeType = System.Windows.Forms.SizeType.AutoSize
                End If
                iCnt = iCnt + 1
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
        End Try
    End Sub

    Public Function HiddenButton(ByVal eProgramButtonType As UI.enumProgramButtonType) As Boolean Implements UI.IParentProgramUI.HiddenButton
        SyncLock _Object
            If lListStatusButton(eProgramButtonType) Then
                Select Case eProgramButtonType
                    Case enumProgramButtonType.HmiComboBox_ActionType
                        HmiLabel_Type.Hide()
                        HmiComboBox_ActionType.Dock = DockStyle.None
                        HmiComboBox_ActionType.Size = New Size(0, 0)

                    Case enumProgramButtonType.HmiTextBox_Component
                        HmiLabel_Component.Hide()
                        HmiTextBox_Component.Dock = DockStyle.None
                        HmiTextBox_Component.Size = New Size(0, 0)
                        ' HmiTextBox_Component.TextBox.Text = ""
                    Case enumProgramButtonType.HmiTextBox_Description
                        HmiLabel_Description.Hide()
                        HmiTextBox_Description.Dock = DockStyle.None
                        HmiTextBox_Description.Size = New Size(0, 0)
                        ' HmiTextBox_Description.TextBox.Text = ""
                    Case enumProgramButtonType.HmiTextBox_Description2
                        HmiLabel_Description2.Hide()
                        HmiTextBox_Description2.Dock = DockStyle.None
                        HmiTextBox_Description2.Size = New Size(0, 0)
                        '   HmiTextBox_Description2.TextBox.Text = ""
                    Case enumProgramButtonType.HmiTextBox_Number
                        HmiLabel_Number.Hide()
                        HmiTextBox_Number.Dock = DockStyle.None
                        HmiTextBox_Number.Size = New Size(0, 0)
                        '  HmiTextBox_Number.TextBox.Text = ""
                    Case enumProgramButtonType.HmiTextBox_Picture
                        HmiLabel_Picture.Hide()
                        HmiTextBox_Picture.Dock = DockStyle.None
                        HmiTextBox_Picture.Size = New Size(0, 0)
                        ' HmiTextBox_Picture.TextBox.Text = ""
                        HmiButton_Choose.Dock = DockStyle.None
                        HmiButton_Choose.Size = New Size(0, 0)
                    Case enumProgramButtonType.HmiTextBox_Repeat
                        HmiLabel_Repeat.Hide()
                        HmiTextBox_Repeat.Dock = DockStyle.None
                        HmiTextBox_Repeat.Size = New Size(0, 0)
                        '   HmiTextBox_Repeat.TextBox.Text = ""
                    Case enumProgramButtonType.HmiLabel_Detail
                        HmiLabel_Detail.Hide()
                        RadioButton_Y.Dock = DockStyle.None
                        RadioButton_Y.Size = New Size(0, 0)
                        RadioButton_N.Dock = DockStyle.None
                        RadioButton_N.Size = New Size(0, 0)
                End Select
                lListStatusButton(eProgramButtonType) = False
                Dim iCnt As Integer = 0
                For i = 0 To lListStatusButton.Count - 1
                    If lListStatusButton(lListStatusButton.Keys(i)) Then
                        iCnt = iCnt + 1
                    End If
                Next
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6) * iCnt + 26
                    ' GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height + 26
                Else
                    TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6) * iCnt + 10
                    ' GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height + 2
                End If
                iCnt = 1
                For Each element As RowStyle In TableLayoutPanel_Body_Mid_Head.RowStyles
                    If lListStatusButton.ContainsKey(iCnt) Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        If lListStatusButton(iCnt) Then
                            If cLanguageManager.SecondLanguageEnable Then
                                If iCnt = 3 Or iCnt = 4 Then
                                    element.Height = HmiTextBox_Number.TextBox.Height + 6 + 12
                                Else
                                    element.Height = HmiTextBox_Number.TextBox.Height + 6
                                End If
                            Else
                                element.Height = HmiTextBox_Number.TextBox.Height + 6
                            End If
                        Else
                            element.Height = 0
                        End If
                    Else
                        element.SizeType = System.Windows.Forms.SizeType.AutoSize
                    End If
                    iCnt = iCnt + 1
                Next

            End If
            Return True
        End SyncLock
    End Function

    Public Function ShowButton(ByVal eProgramButtonType As UI.enumProgramButtonType) As Boolean Implements UI.IParentProgramUI.ShowButton
        SyncLock _Object
            If Not lListStatusButton(eProgramButtonType) Then

                lListStatusButton(eProgramButtonType) = True
                Dim iCnt As Integer = 0
                For i = 0 To lListStatusButton.Count - 1
                    If lListStatusButton(lListStatusButton.Keys(i)) Then
                        iCnt = iCnt + 1
                    End If
                Next
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6) * iCnt + 32
                    '  GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height + 26
                Else
                    TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_Number.TextBox.Height + 6 + 6) * iCnt + 12
                    ' GroupBox_Property.Height = (HmiTextBox_Number.TextBox.Height + 6 ) * iCnt + HmiTextBox_Number.TextBox.Height
                End If
                iCnt = 1
                For Each element As RowStyle In TableLayoutPanel_Body_Mid_Head.RowStyles
                    If lListStatusButton.ContainsKey(iCnt) Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        If lListStatusButton(iCnt) Then
                            If cLanguageManager.SecondLanguageEnable Then
                                If iCnt = 3 Or iCnt = 4 Then
                                    element.Height = HmiTextBox_Number.TextBox.Height + 6 + 12
                                ElseIf iCnt = 8 Then
                                    element.Height = HmiTextBox_Number.TextBox.Height + 6 + 6
                                Else
                                    element.Height = HmiTextBox_Number.TextBox.Height + 6
                                End If
                            Else
                                element.Height = HmiTextBox_Number.TextBox.Height + 6 + 6
                            End If
                        Else
                            element.Height = 0
                        End If
                    Else
                        element.SizeType = System.Windows.Forms.SizeType.AutoSize
                    End If
                    iCnt = iCnt + 1
                Next

                Select Case eProgramButtonType
                    Case enumProgramButtonType.HmiComboBox_ActionType
                        HmiLabel_Type.Show()
                        HmiComboBox_ActionType.Dock = DockStyle.Fill
                        HmiComboBox_ActionType.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Component
                        HmiLabel_Component.Show()
                        HmiTextBox_Component.Dock = DockStyle.Fill
                        HmiTextBox_Component.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Description
                        HmiLabel_Description.Show()
                        HmiTextBox_Description.Dock = DockStyle.Fill
                        HmiTextBox_Description.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Description2
                        HmiLabel_Description2.Show()
                        HmiTextBox_Description2.Dock = DockStyle.Fill
                        HmiTextBox_Description2.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Number
                        HmiLabel_Number.Show()
                        HmiTextBox_Number.Dock = DockStyle.Fill
                        HmiTextBox_Number.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Picture
                        HmiLabel_Picture.Show()
                        HmiTextBox_Picture.Dock = DockStyle.Fill
                        HmiTextBox_Picture.Size = New Size(100, 100)
                        HmiButton_Choose.Dock = DockStyle.Fill
                        HmiButton_Choose.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiTextBox_Repeat
                        HmiLabel_Repeat.Show()
                        HmiTextBox_Repeat.Dock = DockStyle.Fill
                        HmiTextBox_Repeat.Size = New Size(100, 100)
                    Case enumProgramButtonType.HmiLabel_Detail
                        HmiLabel_Detail.Show()
                        RadioButton_Y.Dock = DockStyle.Fill
                        RadioButton_Y.Size = New Size(100, 100)
                        RadioButton_N.Dock = DockStyle.Fill
                        RadioButton_N.Size = New Size(100, 100)
                End Select
            End If

            Return True
        End SyncLock
    End Function

    Public Function SetRepeat(ByVal eProgramCounType As UI.enumProgramCounType, Optional ByVal iCnt As Integer = 0) As Boolean Implements UI.IParentProgramUI.SetRepeat
        SyncLock _Object
            Select Case eProgramCounType
                Case enumProgramCounType.Manual_Screw_Repeat
                    HmiTextBox_Repeat.TextBox.Text = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]"
                Case enumProgramCounType.Manual_Continue
                    HmiTextBox_Repeat.TextBox.Text = "[Continue]"
                Case enumProgramCounType.Manual_Insert
                    HmiTextBox_Repeat.TextBox.Text = iCnt.ToString
            End Select
            Return True
        End SyncLock
    End Function

    Public Function CreateTabPage() As Boolean
        SyncLock _Object
            Try
                HmiComboBox_ExchangeOrigin.ComboBox.Items.Clear()
                HmiComboBox_ExchangeTarget.ComboBox.Items.Clear()
                HmiComboBox_StationCopy.ComboBox.Items.Clear()
                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    HmiComboBox_ExchangeOrigin.ComboBox.Items.Add(element.StationName)
                    '  HmiComboBox_ExchangeTarget.ComboBox.Items.Add(element.StationName)
                    If element.ID <> "1" Then HmiComboBox_StationCopy.ComboBox.Items.Add(element.StationName)
                    Dim SubTabPage As New TabPage
                    SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                    SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                    SubTabPage.Font = New System.Drawing.Font("Calibri", 12.0!)
                    SubTabPage.Name = element.ID
                    SubTabPage.Text = element.StationName

                    Dim TreeView_Action As New TreeViewWidthCheckBox
                    TreeView_Action.Dock = System.Windows.Forms.DockStyle.Fill
                    TreeView_Action.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
                    TreeView_Action.Margin = New System.Windows.Forms.Padding(0)
                    TreeView_Action.Location = New System.Drawing.Point(0, 0)
                    TreeView_Action.CheckBoxes = True
                    TreeView_Action.DrawMode = TreeViewDrawMode.OwnerDrawText
                    TreeView_Action.HideSelection = False
                    TreeView_Action.Name = element.ID
                    SubTabPage.Controls.Add(TreeView_Action)
                    TabControl_TS.Controls.Add(SubTabPage)
                    lListTreeView.Add(TreeView_Action.Name, TreeView_Action)
                    AddHandler TreeView_Action.AfterSelect, AddressOf TreeView_AfterSelect
                    AddHandler TreeView_Action.NodeMouseClick, AddressOf TreeView_NodeMouseClick
                    AddHandler TreeView_Action.AfterCheck, AddressOf TreeView_AfterCheck
                Next
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CreateActionData() As Boolean
        SyncLock _Object
            Try
                For Each elementStation As String In cActionManager.GetCurrentActionListKey
                    lListTreeView(elementStation).Nodes.Clear()
                    For Each elementType As String In cActionManager.GetCurrentActionCfgFromKey(elementStation).GetStepListKey
                        Dim TempTreeNode As clsCheckTreeNodes = New clsCheckTreeNodes
                        TempTreeNode.Text = elementType
                        TempTreeNode.Checked = True
                        TempTreeNode.CheckState = TempTreeNode.Checked
                        For Each elementIndex As Integer In cActionManager.GetCurrentActionCfgFromKey(elementStation).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainStep As clsMainStepCfg = cActionManager.GetCurrentActionCfgFromKey(elementStation).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            Dim TempMainTreeNode As clsCheckTreeNodes = New clsCheckTreeNodes
                            TempMainTreeNode.Text = elementMainStep.MainStepParameter(HMIMainStepKeys.Name)
                            TempMainTreeNode.Checked = IIf(elementMainStep.MainStepParameter(HMIMainStepKeys.Enable) = "TRUE", True, False)
                            TempMainTreeNode.CheckState = IIf(elementMainStep.MainStepParameter(HMIMainStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                            Dim bDisableAll As Boolean = True
                            Dim bMix As Boolean = False
                            For Each elementSubIndex As Integer In cActionManager.GetCurrentActionCfgFromKey(elementStation).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(CInt(elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) - 1).GetSubStepListKey
                                Dim bSubDisableAll As Boolean = True
                                Dim bSubMix As Boolean = False
                                Dim elementSubStep As clsSubStepCfg = cActionManager.GetCurrentActionCfgFromKey(elementStation).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(CInt(elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) - 1).GetSubStepCfgFromKey(elementSubIndex)
                                If elementSubStep.SubStepParameter(HMISubStepKeys.SubActionType) <> enumSubActionType.SubAction.ToString Then
                                    Continue For
                                End If
                                Dim TempSubTreeNode As clsCheckTreeNodes = New clsCheckTreeNodes
                                TempSubTreeNode.Text = elementSubStep.SubStepParameter(HMISubStepKeys.Name)
                                TempSubTreeNode.Checked = IIf(elementSubStep.SubStepParameter(HMISubStepKeys.Enable) = "TRUE", True, False)
                                TempSubTreeNode.CheckState = IIf(elementSubStep.SubStepParameter(HMISubStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                                If elementSubStep.SubStepParameter(HMISubStepKeys.Enable) = "TRUE" Then
                                    bDisableAll = False
                                    bSubDisableAll = False
                                End If
                                If elementSubStep.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                                    bMix = True
                                    bSubMix = True
                                End If

                                'If elementType = enumActionType.Action.ToString Then
                                CreateSubActionType(TempSubTreeNode, elementStation, elementType, CInt(elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) - 1, elementSubStep.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.PreSubAction.ToString, bDisableAll, bMix, bSubDisableAll, bSubMix)
                                CreateSubActionType(TempSubTreeNode, elementStation, elementType, CInt(elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) - 1, elementSubStep.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionPass.ToString, bDisableAll, bMix, bSubDisableAll, bSubMix)
                                CreateSubActionType(TempSubTreeNode, elementStation, elementType, CInt(elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) - 1, elementSubStep.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionFailure.ToString, bDisableAll, bMix, bSubDisableAll, bSubMix)
                                '   End If
                                If bSubDisableAll Then
                                    CType(TempSubTreeNode, clsCheckTreeNodes).CheckState = CheckState.Unchecked
                                ElseIf bSubMix Then
                                    CType(TempSubTreeNode, clsCheckTreeNodes).CheckState = CheckState.Indeterminate
                                End If
                                TempMainTreeNode.Nodes.Add(TempSubTreeNode)
                            Next

                            If bDisableAll Then
                                CType(TempMainTreeNode, clsCheckTreeNodes).CheckState = CheckState.Unchecked
                            ElseIf bMix Then
                                CType(TempMainTreeNode, clsCheckTreeNodes).CheckState = CheckState.Indeterminate
                            End If
                            TempTreeNode.Nodes.Add(TempMainTreeNode)
                        Next
                        lListTreeView(elementStation).Nodes.Add(TempTreeNode)
                    Next
                Next
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub CreateSubActionType(ByVal MainTreeNode As clsCheckTreeNodes, ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iSubStepID As String, ByVal SubActionType As String, ByRef brootDisableAll As Boolean, ByRef brootMix As Boolean, ByRef bsubDisableAll As Boolean, ByRef bsubMix As Boolean)
        SyncLock _Object
            Dim TempTreeNode As clsCheckTreeNodes = New clsCheckTreeNodes

            TempTreeNode.Text = SubActionType
            MainTreeNode.Nodes.Add(TempTreeNode)
            Dim lListSubAction As List(Of clsSubStepCfg) = cActionManager.GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIDIndex, iSubStepID, SubActionType)
            If Not IsNothing(lListSubAction) Then
                For Each element As clsSubStepCfg In lListSubAction
                    Dim TempSubTreeNode As clsCheckTreeNodes = New clsCheckTreeNodes
                    TempSubTreeNode.Text = element.SubStepParameter(HMISubStepKeys.Name)
                    TempSubTreeNode.Checked = IIf(element.SubStepParameter(HMISubStepKeys.Enable) = "TRUE", True, False)
                    TempSubTreeNode.CheckState = IIf(element.SubStepParameter(HMISubStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                    TempTreeNode.Nodes.Add(TempSubTreeNode)
                    If element.SubStepParameter(HMISubStepKeys.Enable) = "TRUE" Then
                        bsubDisableAll = False
                        brootDisableAll = False
                    End If
                    If element.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                        bsubMix = True
                        brootMix = True
                    End If
                Next
            End If

        End SyncLock
    End Sub

    Private Sub CreateType()
        SyncLock _Object
            Dim cHMIActionType As enumHMIActionType
            Dim cHMISubActionType As enumHMISubActionType
            Dim cHMIGlobalActionType As enumHMIActionType
            HmiComboBox_ActionType.ComboBox.SelectedIndex = -1
            HmiComboBox_ActionType.ComboBox.Items.Clear()
            If IsNothing(TabControl_TS) Then
                Return
            End If
            If IsNothing(TabControl_TS.SelectedTab) Then
                Return
            End If
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                cHMISubActionType = enumHMISubActionType.SubSubAction
            Else
                cHMISubActionType = enumHMISubActionType.SubAction
            End If

            Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(TabControl_TS.SelectedTab.Name)
            Select Case cMachineStationCfg.MachineStationType
                Case enumMachineStationType.Auto
                    cHMIActionType = enumHMIActionType.Auto
                Case enumMachineStationType.Manual
                    cHMIActionType = enumHMIActionType.Manual
            End Select


            If cGlobalProgramManager.HasGlobalProgram(cActionManager.VariantCfg.Variant) Then
                cHMIGlobalActionType = enumHMIActionType.Global
            End If
            For Each element As String In cActionLibManager.GetListDllKey
                Dim cActionLibCfg As clsActionLibCfg = cActionLibManager.GetActionLibCfgFromKey(element)
                If cHMIGlobalActionType = enumHMIActionType.Global Then
                    If cActionLibCfg.HMIActionType <> cHMIActionType And cActionLibCfg.HMIActionType <> enumHMIActionType.ManualAuto And cActionLibCfg.HMIActionType <> cHMIGlobalActionType Then Continue For
                Else
                    If cActionLibCfg.HMIActionType <> cHMIActionType And cActionLibCfg.HMIActionType <> enumHMIActionType.ManualAuto Then Continue For
                    If cActionLibCfg.HMISubActionType <> cHMISubActionType And cActionLibCfg.HMISubActionType <> enumHMISubActionType.All Then Continue For
                End If
                HmiComboBox_ActionType.ComboBox.Items.Add(cActionLibCfg.ActionName)
            Next

            HmiComboBox_StationCopy.ComboBox.Items.Clear()
            For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                If element.ID <> TabControl_TS.SelectedTab.Name Then HmiComboBox_StationCopy.ComboBox.Items.Add(element.StationName)
            Next
        End SyncLock
    End Sub

    Private Sub ComboBox_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                Select Case sender.name
                    Case "HmiComboBox_Variant"
                        If IsNothing(TabControl_TS.SelectedTab) Then
                            Return
                        End If
                        '  If cActionManager.VariantCfg.Variant = HmiComboBox_Variant.ComboBox.Text Then
                        HmiButton_Confirm.Button.Enabled = True
                        HmiComboBox_CopyVariant.ComboBox.SelectedIndex = -1
                        HmiComboBox_CopyVariant.ComboBox.Enabled = False
                        HmiButton_CopyConfirm.Button.Enabled = False

                        HmiComboBox_StationCopy.ComboBox.SelectedIndex = -1
                        HmiComboBox_StationCopy.ComboBox.Enabled = False
                        HmiButton_StationCopy.Button.Enabled = False

                        HmiComboBox_ExchangeOrigin.ComboBox.SelectedIndex = -1
                        HmiComboBox_ExchangeOrigin.ComboBox.Enabled = False
                        HmiComboBox_ExchangeTarget.ComboBox.SelectedIndex = -1
                        HmiComboBox_ExchangeTarget.ComboBox.Enabled = False
                        HmiButton_ExchangeCopy.Button.Enabled = False

                    Case "HmiComboBox_CopyVariant"
                        If IsNothing(TabControl_TS.SelectedTab) Then
                            Return
                        End If
                        If Not HmiButton_Confirm.Button.Enabled Then HmiButton_CopyConfirm.Button.Enabled = True
                    Case "HmiComboBox_StationCopy"
                        If IsNothing(TabControl_TS.SelectedTab) Then
                            Return
                        End If
                        If Not HmiButton_Confirm.Button.Enabled Then HmiButton_StationCopy.Button.Enabled = True

                    Case "HmiComboBox_ExchangeOrigin"
                        If IsNothing(TabControl_TS.SelectedTab) Then
                            Return
                        End If
                        HmiComboBox_ExchangeTarget.ComboBox.Items.Clear()
                        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                            If element.StationName <> HmiComboBox_ExchangeOrigin.ComboBox.Text Then
                                HmiComboBox_ExchangeTarget.ComboBox.Items.Add(element.StationName)
                            End If

                        Next
                        If Not HmiButton_Confirm.Button.Enabled Then
                            If HmiComboBox_ExchangeOrigin.ComboBox.SelectedIndex > -1 And HmiComboBox_ExchangeTarget.ComboBox.SelectedIndex > -1 Then
                                If HmiComboBox_ExchangeOrigin.ComboBox.Text <> HmiComboBox_ExchangeTarget.ComboBox.Text Then
                                    HmiButton_ExchangeCopy.Button.Enabled = True
                                End If
                            End If
                        End If
                    Case "HmiComboBox_ExchangeTarget"
                        If Not HmiButton_Confirm.Button.Enabled Then
                            If HmiComboBox_ExchangeOrigin.ComboBox.SelectedIndex > -1 And HmiComboBox_ExchangeTarget.ComboBox.SelectedIndex > -1 Then
                                If HmiComboBox_ExchangeOrigin.ComboBox.Text <> HmiComboBox_ExchangeTarget.ComboBox.Text Then
                                    HmiButton_ExchangeCopy.Button.Enabled = True
                                End If
                            End If
                        End If

                    Case "HmiComboBox_ActionType"
                        If IsNothing(TabControl_TS.SelectedTab) Then
                            Return
                        End If
                        TableLayoutPanel_Body_Mid_Head.Refresh()

                        If Not IsNothing(cHMIActionBase) Then
                            RemoveHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
                            cHMIActionBase.ActionUI.Quit(cLocalElement, cSystemElement)
                            Panel_Action.Controls.Clear()
                        End If

                        If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                                If HmiComboBox_ActionType.ComboBox.SelectedIndex <> -1 Then
                                    cActionManager.ChangeCurrentSubParameter(
                                        TabControl_TS.SelectedTab.Name,
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                        sender.name.ToString.Split("_")(1),
                                        CType(sender, ComboBox).Text
                                        )

                                    Dim cMainStepCfg As clsMainStepCfg = cActionManager.GetCurrentMainStepFromName(
                                                                                                                                                       TabControl_TS.SelectedTab.Name,
                                                                                                                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                                                                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentText
                                                                                                                                                       )
                                    If cLocalElement.ContainsKey(clsMainStepCfg.Name) Then
                                        cLocalElement(clsMainStepCfg.Name) = cMainStepCfg
                                    Else
                                        cLocalElement.Add(clsMainStepCfg.Name, cMainStepCfg)
                                    End If

                                    Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(TabControl_TS.SelectedTab.Name,
                                                                                               lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                                               lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                                               lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                                                               )
                                    If cLocalElement.ContainsKey(clsSubStepCfg.Name) Then
                                        cLocalElement(clsSubStepCfg.Name) = cSubStepCfg
                                    Else
                                        cLocalElement.Add(clsSubStepCfg.Name, cSubStepCfg)
                                    End If

                                    If cLocalElement.ContainsKey(clsActionManager.Name) Then
                                        cLocalElement(clsActionManager.Name) = cActionManager
                                    Else
                                        cLocalElement.Add(clsActionManager.Name, cActionManager)
                                    End If

                                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(TabControl_TS.SelectedTab.Name)
                                    If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                                        cLocalElement(clsMachineStationCfg.Name) = cMachineStationCfg.Clone
                                    Else
                                        cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg.Clone)
                                    End If

                                    If cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                                        cLocalElement(clsVariantCfg.Name) = cVariantCfg
                                    Else
                                        cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
                                    End If
                                    cHMIActionBase = cActionLibManager.GetActionLibCfgFromKey(HmiComboBox_ActionType.ComboBox.Text).Source
                                    AddHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
                                    cHMIActionBase.CreateActionUI(cLocalElement, cSystemElement)
                                    IActionUI = cHMIActionBase.ActionUI
                                    IActionUI.Init(cLocalElement, cSystemElement)
                                    Panel_Action.Controls.Clear()
                                    cFormFontResize.SetControls(cFormFontResize.CurrentRate, IActionUI.UI)
                                    Panel_Action.Controls.Add(IActionUI.UI)

                                    'Dim cTempSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromName(TabControl_TS.SelectedTab.Name,
                                    '       lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentParentText,
                                    '       lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                    '       lListTreeView(  
                                    'If Not IsNothing(cTempSubStepCfg) Then
                                    If strLastType = HmiComboBox_ActionType.ComboBox.Text Then
                                        IActionUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(strLastParameter))
                                    Else
                                        IActionUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(""))
                                    End If
                                    'Else
                                    '    IActionUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(""))
                                    'End If
                                End If
                            End If
                        End If
                End Select
                Button_Save.Enabled = cActionManager.IsChanged
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Select Case sender.name
                Case "Button_Add"
                    Add()
                Case "Button_Del"
                    Del()
                Case "Button_Up"
                    Up()
                Case "Button_Down"
                    Down()
                Case "Button_Save"
                    Save()
                Case "Button_SaveAs"
                    SaveAs()
                Case "HmiButton_Confirm"
                    LoadData(True)
                Case "HmiButton_CopyConfirm"
                    CopyData()
                Case "HmiButton_Choose"
                    Choose()
                Case "HmiButton_StationCopy"
                    StationCopy()
                Case "HmiButton_ExchangeCopy"
                    ExchangeCopy()
            End Select
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub Add()
        SyncLock _Object
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 0 Then
                Dim cMainStepCfg As clsMainStepCfg = cActionManager.AddMainStep(
                                                           TabControl_TS.SelectedTab.Name,
                                                           lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectText
                                                           )
                lListTreeView(TabControl_TS.SelectedTab.Name).AddFirstNode(cMainStepCfg.MainStepParameter(HMIMainStepKeys.Name))
                UpdateListTreeName()
            End If

            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 1 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Clone
                Dim strSubActionType As String = ""
                Dim bCreatSubSub As Boolean = True
                If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 1 And lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level <= 2 Then
                    strSubActionType = enumSubActionType.SubAction.ToString
                ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Then
                    strSubActionType = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text
                ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                    strSubActionType = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Text
                End If
                ' If lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText <> enumActionType.Action.ToString Then
                'strSubActionType = enumSubActionType.SubAction.ToString
                bCreatSubSub = True
                ' End If

                If strSubActionType = enumSubActionType.PreSubAction.ToString Or strSubActionType = enumSubActionType.SubActionPass.ToString Or strSubActionType = enumSubActionType.SubActionFailure.ToString Then
                    bCreatSubSub = False
                End If

                Dim bCheck As Boolean = True
                If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Then
                    If CType(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked Then
                        bCheck = False
                    End If
                End If
                If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                    If CType(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked Then
                        bCheck = False
                    End If
                End If
                Dim cSubStepCfg As clsSubStepCfg = cActionManager.AddSubStepByIndex(
                                                       TabControl_TS.SelectedTab.Name,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex,
                                                       strSubActionType,
                                                       bCheck
                                                       )
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).AddSecondNode(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex, strSubActionType, bCheck, bCreatSubSub)

                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = SelectActionNode
            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub Del()
        SyncLock _Object
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Clone
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex < 0 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.DelMainStepByIndex(
                                            TabControl_TS.SelectedTab.Name,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex
                                            )
                lListTreeView(TabControl_TS.SelectedTab.Name).RemoveSelectNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                If nSelIndex < 0 Then Return
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect

                cActionManager.DelAllSubStepByIndex(
                                            TabControl_TS.SelectedTab.Name,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                            )
                lListTreeView(TabControl_TS.SelectedTab.Name).RemoveSelectNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                If nSelIndex < 0 Then Return
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.DelSubStepByIndex(
                                            TabControl_TS.SelectedTab.Name,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                            )
                lListTreeView(TabControl_TS.SelectedTab.Name).RemoveSelectNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                If nSelIndex < 0 Then Return
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedNode()
            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub Up()
        SyncLock _Object
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then

                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                Dim nSelParentIndex As Integer = SelectActionNode.Parent.Index
                If nSelIndex <= 0 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.UpMainStepByIndex(TabControl_TS.SelectedTab.Name, SelectActionNode.Parent.Text, SelectActionNode.Index)
                lListTreeView(TabControl_TS.SelectedTab.Name).UpSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedUpNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex <= 0 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.UpAllSubStepByIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                lListTreeView(TabControl_TS.SelectedTab.Name).UpSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedUpNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex <= 0 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.UpSubStepByIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                lListTreeView(TabControl_TS.SelectedTab.Name).UpSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedUpNode()
            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub Down()
        SyncLock _Object
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex >= SelectActionNode.Parent.Nodes.Count - 1 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.DownMainStepByIndex(TabControl_TS.SelectedTab.Name, SelectActionNode.Parent.Text, SelectActionNode.Index)
                lListTreeView(TabControl_TS.SelectedTab.Name).DownSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedDownNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex >= SelectActionNode.Parent.Nodes.Count - 1 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.DownAllSubStepByIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                lListTreeView(TabControl_TS.SelectedTab.Name).DownSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedDownNode()

            ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex >= SelectActionNode.Parent.Nodes.Count - 1 Then Return
                RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                cActionManager.DownSubStepByIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                lListTreeView(TabControl_TS.SelectedTab.Name).DownSelectedNode()
                UpdateListTreeName()
                AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedDownNode()
            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub
    Private Sub Paste_All_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cpy As clsCopyInfo = clsCopyInfo.GetClipboardContent()
        If cpy Is Nothing Then Return
        If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Then
            If cpy.Type = ListType.PreSub Or cpy.Type = ListType.SubFail Or cpy.Type = ListType.SubPass Then
                Dim SelectActionNode As TreeNode = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode
                Dim cSubStepCfg As clsSubStepCfg = CType(cpy.StepData, clsSubStepCfg)
                Dim viewData As clsCheckTreeNodes = CType(cpy.ViewData, clsCheckTreeNodes).Clone
                Dim SubActionType As String = ""
                Select Case cpy.Type
                    Case ListType.PreSub
                        SubActionType = enumSubActionType.PreSubAction.ToString
                    Case ListType.SubFail
                        SubActionType = enumSubActionType.SubActionFailure.ToString
                    Case ListType.SubPass
                        SubActionType = enumSubActionType.SubActionPass.ToString
                End Select

                Dim PastSubActionType As String = ""
                Select Case lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Index
                    Case 0
                        PastSubActionType = enumSubActionType.PreSubAction.ToString
                    Case 1
                        PastSubActionType = enumSubActionType.SubActionPass.ToString
                    Case 2
                        PastSubActionType = enumSubActionType.SubActionFailure.ToString

                End Select
                cActionManager.PastAllPreSubStepByIndex(
                                        TabControl_TS.SelectedTab.Name,
                                        cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType),
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                        cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) - 1,
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                        cSubStepCfg.SubStepParameter(HMISubStepKeys.KeyID),
                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex,
                                        SubActionType,
                                        PastSubActionType
                                     )



                LoadData(False)
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = SelectActionNode
                Dim i1 As Integer = SelectActionNode.Parent.Parent.Parent.Index
                Dim i2 As Integer = SelectActionNode.Parent.Parent.Index
                Dim i3 As Integer = SelectActionNode.Parent.Index
                Dim i4 As Integer = SelectActionNode.Index
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = lListTreeView(TabControl_TS.SelectedTab.Name).Nodes(i1).Nodes(i2).Nodes(i3).Nodes(i4)
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.ExpandAll()
            End If
        End If
        Button_Save.Enabled = cActionManager.IsChanged
    End Sub
    Private Sub Paste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                Dim cpy As clsCopyInfo = clsCopyInfo.GetClipboardContent()
                If cpy Is Nothing Then Return
                If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 0 Then
                    If cpy.Type <> ListType.MainStep Then Return
                    Dim cMainStepCfg As clsMainStepCfg = CType(cpy.StepData, clsMainStepCfg)
                    Dim viewData As clsCheckTreeNodes = CType(cpy.ViewData, clsCheckTreeNodes).Clone
                    RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                    cActionManager.AddMainStep(
                                                TabControl_TS.SelectedTab.Name,
                                                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text,
                                                cMainStepCfg
                    )
                    lListTreeView(TabControl_TS.SelectedTab.Name).PastSelectedNode(viewData)
                    UpdateListTreeName()
                    AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                    lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                    lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedCurrentNode()

                ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                    If cpy.Type <> ListType.SubStep Then Return
                    Dim cSubStepCfg As clsSubStepCfg = CType(cpy.StepData, clsSubStepCfg)
                    Dim viewData As clsCheckTreeNodes = CType(cpy.ViewData, clsCheckTreeNodes).Clone
                    RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                    cActionManager.PastAllSubStepByIndex(
                                            TabControl_TS.SelectedTab.Name,
                                            cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType),
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                            cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) - 1,
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                            cSubStepCfg.SubStepParameter(HMISubStepKeys.KeyID),
                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex
                                            )


                    lListTreeView(TabControl_TS.SelectedTab.Name).PastSelectedNode(viewData)
                    UpdateListTreeName()
                    AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                    lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                    lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedCurrentNode()
                ElseIf lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                    If cpy.Type = ListType.SubSubStep Then
                        Dim cSubStepCfg As clsSubStepCfg = CType(cpy.StepData, clsSubStepCfg)
                        Dim viewData As clsCheckTreeNodes = CType(cpy.ViewData, clsCheckTreeNodes).Clone
                        RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect

                        Dim PastSubActionType As String = ""
                        If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Then
                            Select Case lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Index
                                Case 0
                                    PastSubActionType = enumSubActionType.PreSubAction.ToString
                                Case 1
                                    PastSubActionType = enumSubActionType.SubActionPass.ToString
                                Case 2
                                    PastSubActionType = enumSubActionType.SubActionFailure.ToString

                            End Select
                        Else
                            PastSubActionType = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Text
                        End If

                        cActionManager.PastSubStepByIndex(
                                                TabControl_TS.SelectedTab.Name,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType),
                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) - 1,
                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.KeyID),
                                                 lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex,
                                                 PastSubActionType
                                                )


                        lListTreeView(TabControl_TS.SelectedTab.Name).PastSelectedNode(viewData)
                        UpdateListTreeName()
                        AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                        lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                        lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedCurrentNode()
                    End If

                    If cpy.Type = ListType.PreSub Or cpy.Type = ListType.SubFail Or cpy.Type = ListType.SubPass Then
                        Dim cSubStepCfg As clsSubStepCfg = CType(cpy.StepData, clsSubStepCfg)
                        Dim viewData As clsCheckTreeNodes = CType(cpy.ViewData, clsCheckTreeNodes).Clone
                        RemoveHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                        Dim SubActionType As String = ""

                        Select Case cpy.Type
                            Case ListType.PreSub
                                SubActionType = enumSubActionType.PreSubAction.ToString
                            Case ListType.SubFail
                                SubActionType = enumSubActionType.SubActionFailure.ToString
                            Case ListType.SubPass
                                SubActionType = enumSubActionType.SubActionPass.ToString

                        End Select

                        Dim PastSubActionType As String = ""

                        Select Case lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Index
                            Case 0
                                PastSubActionType = enumSubActionType.PreSubAction.ToString
                            Case 1
                                PastSubActionType = enumSubActionType.SubActionPass.ToString
                            Case 2
                                PastSubActionType = enumSubActionType.SubActionFailure.ToString

                        End Select
                        cActionManager.PastPreSubStepByIndex(
                                                TabControl_TS.SelectedTab.Name,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType),
                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) - 1,
                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                cSubStepCfg.SubStepParameter(HMISubStepKeys.KeyID),
                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualInsertIndex,
                                                SubActionType,
                                                PastSubActionType
                                             )


                        lListTreeView(TabControl_TS.SelectedTab.Name).PastPreSelectedNode(viewData)
                        UpdateListTreeName()
                        AddHandler lListTreeView(TabControl_TS.SelectedTab.Name).AfterSelect, AddressOf TreeView_AfterSelect
                        lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = Nothing
                        lListTreeView(TabControl_TS.SelectedTab.Name).ChooseSelectedCurrentNode()
                    End If
                End If
                Button_Save.Enabled = cActionManager.IsChanged
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Private Sub UpdateListTreeName()
        SyncLock _Object
            Try
                Dim iCnt As Integer = 0
                Dim jCnt As Integer = 0
                For Each elementTreeNode As TreeNode In lListTreeView(TabControl_TS.SelectedTab.Name).Nodes
                    jCnt = 0
                    iCnt = 0
                    Dim lListMainStepCfg As List(Of clsMainStepCfg) = cActionManager.GetMainStepCfgListFromCurrent(TabControl_TS.SelectedTab.Name, elementTreeNode.Text)
                    Dim lListSubStepCfg As List(Of clsSubStepCfg) = cActionManager.GetSubStepCfgListFromCurrent(TabControl_TS.SelectedTab.Name, elementTreeNode.Text)
                    For Each MainelementTreeNode As TreeNode In elementTreeNode.Nodes
                        If MainelementTreeNode.Text <> lListMainStepCfg(iCnt).MainStepParameter(HMIMainStepKeys.Name) Then
                            MainelementTreeNode.Text = lListMainStepCfg(iCnt).MainStepParameter(HMIMainStepKeys.Name)
                        End If
                        ' Action
                        For Each SubelementTreeNode As TreeNode In MainelementTreeNode.Nodes
                            Dim bFindSubAction As Boolean = False
                            'sub Action
                            For Each SubSubelementTreeNode As TreeNode In SubelementTreeNode.Nodes
                                'Sub Sub Action
                                For Each SubSubSubelementTreeNode As TreeNode In SubSubelementTreeNode.Nodes
                                    'Pre Action搜索完毕
                                    If Not bFindSubAction And SubSubSubelementTreeNode.Parent.Index <> 0 Then
                                        '主Sub Action更新
                                        If SubelementTreeNode.Text <> lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name) Then
                                            SubelementTreeNode.Text = lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name)
                                        End If
                                        jCnt = jCnt + 1
                                        bFindSubAction = True
                                    End If

                                    If SubSubSubelementTreeNode.Text <> lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name) Then
                                        SubSubSubelementTreeNode.Text = lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name)
                                    End If
                                    jCnt = jCnt + 1
                                Next
                            Next
                            If Not bFindSubAction Then
                                If SubelementTreeNode.Text <> lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name) Then
                                    SubelementTreeNode.Text = lListSubStepCfg(jCnt).SubStepParameter(HMISubStepKeys.Name)
                                End If
                                jCnt = jCnt + 1
                                bFindSubAction = True
                            End If
                        Next
                        iCnt = iCnt + 1
                    Next
                Next
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub


    Private Sub Save()
        SyncLock _Object
            Try
                Button_Save.Enabled = False
                cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
                If cActionManager.CheckCurrentActionCfg() Then
                    If cActionManager.SaveCurrentActionCfg(cActionManager.VariantCfg.Variant) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "1"), enumExceptionType.Normal, enumUIName.ParentProgramForm.ToString))
                    Else
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "2"), enumExceptionType.Normal, enumUIName.ParentProgramForm.ToString))
                    End If
                End If
                Button_Save.Enabled = cActionManager.IsChanged
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub

    Public Sub SaveAs()
        SyncLock _Object
            Try
                Button_SaveAs.Enabled = False
                Dim strProgramPath As String = String.Empty
                SaveFileDialogIni.Filter = "*.ini|*.ini"
                SaveFileDialogIni.InitialDirectory = cSystemManager.Settings.VariantFolder
                If SaveFileDialogIni.ShowDialog() = DialogResult.OK Then
                    strProgramPath = SaveFileDialogIni.FileName.Substring(SaveFileDialogIni.FileName.LastIndexOf("\") + 1)
                    strProgramPath = strProgramPath.Replace(".ini", "")
                    If cActionManager.CheckCurrentActionCfg() Then
                        If cActionManager.SaveAsCurrentActionCfg(strProgramPath) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "1"), enumExceptionType.Normal, enumUIName.ParentProgramForm.ToString))
                        Else
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "2"), enumExceptionType.Normal, enumUIName.ParentProgramForm.ToString))
                        End If
                    End If
                End If
                Button_SaveAs.Enabled = True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub

    Private Sub LoadData(ByVal bReload As Boolean)
        SyncLock _Object
            If bReload Then
                If cActionManager.IsChanged Then
                    cChangePage.BackPage()
                    cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "5"), enumExceptionType.Confirm, enumUIName.ParentProgramForm.ToString))
                    Return
                End If

                clsCopyInfo.CopyToClipboard(Nothing)
                cActionManager.LoadActionCfg(HmiComboBox_Variant.ComboBox.Text)
            End If

            CleanData()
            cVariantCfg.Variant = HmiComboBox_Variant.ComboBox.Text
            CreateActionData()
            LoadDescrption()
            LoadPicture()
            LoadComponent()
            ShowAllStep()
            HmiButton_Confirm.Button.Enabled = False
            HmiButton_ExchangeCopy.Button.Enabled = False
            HmiButton_StationCopy.Button.Enabled = False
            HmiComboBox_CopyVariant.ComboBox.Enabled = True
            HmiComboBox_StationCopy.ComboBox.Enabled = True
            HmiComboBox_ExchangeOrigin.ComboBox.Enabled = True
            HmiComboBox_ExchangeTarget.ComboBox.Enabled = True
            Button_Save.Enabled = cActionManager.IsChanged
            If cGlobalProgramManager.HasGlobalProgram(cActionManager.VariantCfg.Variant) Then
                Button_SaveAs.Enabled = False
            Else
                Button_SaveAs.Enabled = True
            End If
        End SyncLock
    End Sub

    Private Sub LoadDescrption()
        If Not cGlobalProgramManager.HasGlobalProgram(HmiComboBox_Variant.ComboBox.Text) Then
            cLocalVariant.ChangeVariant(HmiComboBox_Variant.ComboBox.Text)
        End If
        Dim lListKey As New Dictionary(Of String, Dictionary(Of String, String))
        Dim lListValue As New Dictionary(Of String, String)
        For Each ElementIndex As Integer In cTextManager.GetTextListKey
            Dim Element As clsTextCfg = cTextManager.GetTextCfgFromKey(ElementIndex)
            lListValue.Add(Element.Key, Element.ActiveMessage)
        Next
        lListKey.Add("TextList", lListValue)

        lListValue = New Dictionary(Of String, String)
        lListValue.Add("Variant", cLocalVariant.CurrentVariantCfg.Variant)
        lListValue.Add("SFC", cLocalVariant.CurrentVariantCfg.SFC)
        For Each ElementKey As String In cLocalVariant.CurrentVariantCfg.ListElement.Keys
            lListValue.Add(ElementKey, cLocalVariant.CurrentVariantCfg.ListElement(ElementKey).ToString)
        Next
        lListKey.Add("Variant", lListValue)
        Dim lListLine As New List(Of Integer)
        Dim lListNameWitheTextLine As New List(Of Integer)
        HmiTextBox_Description.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Insert, False)
        HmiTextBox_Description2.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Insert, False)
    End Sub
    Private Sub LoadComponent()
        If Not cGlobalProgramManager.HasGlobalProgram(HmiComboBox_Variant.ComboBox.Text) Then
            cLocalVariant.ChangeVariant(HmiComboBox_Variant.ComboBox.Text)
        End If

        Dim lListKey As New Dictionary(Of String, Dictionary(Of String, String))
        Dim lListValue As New Dictionary(Of String, String)
        lListValue.Add("Variant", cLocalVariant.CurrentVariantCfg.Variant)
        lListValue.Add("SFC", cLocalVariant.CurrentVariantCfg.SFC)
        For Each ElementKey As String In cLocalVariant.CurrentVariantCfg.ListElement.Keys
            lListValue.Add(ElementKey, cLocalVariant.CurrentVariantCfg.ListElement(ElementKey).ToString)
        Next
        lListKey.Add("Variant", lListValue)
        Dim lListLine As New List(Of Integer)
        Dim lListNameWitheTextLine As New List(Of Integer)
        HmiTextBox_Component.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Replace, False)
    End Sub

    Private Sub LoadPicture()
        If Not cGlobalProgramManager.HasGlobalProgram(HmiComboBox_Variant.ComboBox.Text) Then
            cLocalVariant.ChangeVariant(HmiComboBox_Variant.ComboBox.Text)
        End If

        Dim lListKey As New Dictionary(Of String, Dictionary(Of String, String))
        Dim lListValue As New Dictionary(Of String, String)
        For Each ElementIndex As Integer In cPictureManager.GetPictureListKey
            Dim Element As clsPictureCfg = cPictureManager.GetPictureCfgFromKey(ElementIndex)
            lListValue.Add(Element.Key, Element.Path.Split("\").Last)
        Next
        lListKey.Add("PictureList", lListValue)

        lListValue = New Dictionary(Of String, String)
        lListValue.Add("Picture", cLocalVariant.CurrentVariantCfg.PicturePath.Split("\").Last)
        For Each ElementKey As String In cLocalVariant.CurrentVariantCfg.ListElement.Keys
            If cLocalVariant.CurrentVariantCfg.ListElement(ElementKey).ToString.IndexOf(".") > 0 Then
                lListValue.Add(ElementKey, cLocalVariant.CurrentVariantCfg.ListElement(ElementKey).ToString.Split("\").Last)
            End If
        Next
        lListKey.Add("Variant", lListValue)
        Dim lListLine As New List(Of Integer)
        Dim lListNameWitheTextLine As New List(Of Integer)
        HmiTextBox_Picture.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Replace, False)
    End Sub

    Private Sub CopyData()
        SyncLock _Object
            CleanData()
            cActionManager.CopyActionCfg(HmiComboBox_CopyVariant.ComboBox.Text)
            CreateActionData()
            ShowAllStep()
            HmiButton_CopyConfirm.Button.Enabled = False
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub


    Private Sub StationCopy()
        SyncLock _Object
            CleanData()
            cActionManager.CopyStationCfg(TabControl_TS.SelectedTab.Name, HmiComboBox_StationCopy.ComboBox.Text)
            CreateActionData()
            ShowAllStep()
            HmiButton_CopyConfirm.Button.Enabled = False
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub ExchangeCopy()
        SyncLock _Object
            CleanData()
            cActionManager.ExchangeStationCfg(HmiComboBox_ExchangeOrigin.ComboBox.Text, HmiComboBox_ExchangeTarget.ComboBox.Text)
            CreateActionData()
            ShowAllStep()
            HmiButton_CopyConfirm.Button.Enabled = False
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub

    Private Sub CleanData()
        SyncLock _Object
            For Each element As TreeViewWidthCheckBox In lListTreeView.Values
                element.Nodes.Clear()
            Next
            ShowAllStep()
            AddHmiComboBox_CopyVariant()
        End SyncLock
    End Sub

    Private Sub Choose()
        SyncLock _Object
            Try
                OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                            "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                             "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
                OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.PictureFolder
                OpenFileDialog_Path.RestoreDirectory = True
                OpenFileDialog_Path.FilterIndex = 1
                Dim cDialogResult As New System.Windows.Forms.DialogResult
                cDialogResult = OpenFileDialog_Path.ShowDialog()
                If cDialogResult = DialogResult.OK Then
                    HmiTextBox_Picture.TextBox.Text = OpenFileDialog_Path.FileName
                End If
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub

    Public Sub AddHmiComboBox_CopyVariant()
        SyncLock _Object
            Dim mCurrentVariant As String = String.Empty
            Dim iCnt As Integer = 0
            mCurrentVariant = HmiComboBox_CopyVariant.ComboBox.Text
            HmiComboBox_CopyVariant.ComboBox.SelectedIndex = -1
            HmiButton_CopyConfirm.Button.Enabled = False
            HmiComboBox_CopyVariant.ComboBox.Items.Clear()

            If cGlobalProgramManager.HasGlobalProgram(HmiComboBox_Variant.ComboBox.Text) Then
                For Each elementIndex As Integer In cGlobalProgramManager.GetGlobalProgramListKey
                    Dim element As clsGlobalProgramCfg = cGlobalProgramManager.GetGlobalProgramCfgFromKey(elementIndex)
                    If element.GlobalProgram <> HmiComboBox_Variant.ComboBox.Text Then
                        HmiComboBox_CopyVariant.ComboBox.Items.Add(element.GlobalProgram)
                        If mCurrentVariant = element.GlobalProgram Then
                            HmiComboBox_CopyVariant.ComboBox.SelectedIndex = iCnt
                        End If
                        iCnt = iCnt + 1
                    End If
                Next
            Else
                For Each elementIndex As Integer In cVariantManager.GetVariantListKey
                    Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
                    If element.Variant <> HmiComboBox_Variant.ComboBox.Text Then
                        HmiComboBox_CopyVariant.ComboBox.Items.Add(element.Variant)
                        If mCurrentVariant = element.Variant Then
                            HmiComboBox_CopyVariant.ComboBox.SelectedIndex = iCnt
                        End If
                        iCnt = iCnt + 1
                    End If
                Next
            End If
        End SyncLock
    End Sub
    Private Sub TreeView_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        SyncLock _Object
            If e.Node.Level = 1 Then
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = e.Node
                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.Normal

                cActionManager.ChangeCurrentMainParameter(
                                                         TabControl_TS.SelectedTab.Name,
                                                         e.Node.Parent.Text,
                                                         e.Node.Index,
                                                         HMIMainStepKeys.Enable,
                                                         IIf(cActionManager.GetCurrentMainStepFromIndex(TabControl_TS.SelectedTab.Name, e.Node.Parent.Text, e.Node.Index).MainStepParameter(HMIMainStepKeys.Enable) = "TRUE", "FALSE", "TRUE")
                                                          )
                CType(e.Node, clsCheckTreeNodes).CheckState = IIf(cActionManager.GetCurrentMainStepFromIndex(TabControl_TS.SelectedTab.Name, e.Node.Parent.Text, e.Node.Index).MainStepParameter(HMIMainStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.OwnerDrawText
            End If
            If e.Node.Level = 2 Then
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = e.Node
                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.Normal
                cActionManager.ChangeCurrentSubParameter(
                                                         TabControl_TS.SelectedTab.Name,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                         HMISubStepKeys.Enable,
                                                         IIf(cActionManager.GetCurrentSubStepFromIndex(
                                                             TabControl_TS.SelectedTab.Name,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                             ).SubStepParameter(HMISubStepKeys.Enable) = "TRUE", "FALSE", "TRUE"))
                CType(e.Node, clsCheckTreeNodes).CheckState = IIf(cActionManager.GetCurrentSubStepFromIndex(
                                                             TabControl_TS.SelectedTab.Name,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                             ).SubStepParameter(HMISubStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                '更改PreSubAction
                For Each element As clsCheckTreeNodes In e.Node.Parent.Nodes
                    For Each subelement As clsCheckTreeNodes In element.Nodes
                        For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                            subsubelement.CheckState = CType(e.Node, clsCheckTreeNodes).CheckState
                        Next
                    Next
                Next

                Dim bDisableAll As Boolean = True
                Dim bMix As Boolean = False
                For Each element As clsCheckTreeNodes In e.Node.Parent.Nodes
                    If element.CheckState = CheckState.Checked Then
                        bDisableAll = False
                    End If
                    If element.CheckState = CheckState.Unchecked Then
                        bMix = True
                    End If
                    For Each subelement As clsCheckTreeNodes In element.Nodes
                        For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                            If subsubelement.CheckState = CheckState.Checked Then
                                bDisableAll = False
                            End If
                            If subsubelement.CheckState = CheckState.Unchecked Then
                                bMix = True
                            End If
                        Next
                    Next
                Next
                If bDisableAll Then
                    CType(e.Node.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked
                ElseIf bMix Then
                    CType(e.Node.Parent, clsCheckTreeNodes).CheckState = CheckState.Indeterminate
                Else
                    CType(e.Node.Parent, clsCheckTreeNodes).CheckState = CheckState.Checked
                End If

                

                cActionManager.ChangeAllSubStepByIndex(
                                                       TabControl_TS.SelectedTab.Name,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                       lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                       IIf(CType(e.Node, clsCheckTreeNodes).CheckState = CheckState.Unchecked, "FALSE", "TRUE"))


                cActionManager.ChangeCurrentMainParameter(
                                                         TabControl_TS.SelectedTab.Name,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Parent.Text,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Index,
                                                         HMIMainStepKeys.Enable,
                                                         IIf(CType(e.Node.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked, "FALSE", "TRUE")
                                                          )

                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.OwnerDrawText
            End If

            If e.Node.Level = 4 Then
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = e.Node
                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.Normal
                Dim strCheckValue As String = ""
                strCheckValue = IIf(cActionManager.GetCurrentSubStepFromIndex(
                                                            TabControl_TS.SelectedTab.Name,
                                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                            lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                            ).SubStepParameter(HMISubStepKeys.Enable) = "TRUE", "FALSE", "TRUE")

                If CType(e.Node.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked Then
                    strCheckValue = "FALSE"
                End If
                cActionManager.ChangeCurrentSubParameter(
                                                         TabControl_TS.SelectedTab.Name,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                         HMISubStepKeys.Enable,
                                                         strCheckValue
                                                        )
                CType(e.Node, clsCheckTreeNodes).CheckState = IIf(cActionManager.GetCurrentSubStepFromIndex(
                                                             TabControl_TS.SelectedTab.Name,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                             ).SubStepParameter(HMISubStepKeys.Enable) = "TRUE", CheckState.Checked, CheckState.Unchecked)
                Dim bDisableAll As Boolean = True
                Dim bMix As Boolean = False

                'SubAction
                bMix = False
                bDisableAll = True
                For Each element As clsCheckTreeNodes In lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Parent.Nodes
                    For Each subelement As clsCheckTreeNodes In element.Nodes
                        If subelement.CheckState = CheckState.Unchecked Then
                            bMix = True
                        End If
                        If subelement.CheckState = CheckState.Checked Then
                            bDisableAll = False
                        End If
                    Next
                Next

                If bMix Then
                    CType(e.Node.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Indeterminate
                Else
                    CType(e.Node.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Checked
                End If


                'Action 层
                bMix = False
                bDisableAll = True
                For Each element As clsCheckTreeNodes In e.Node.Parent.Parent.Parent.Nodes
                    If element.CheckState = CheckState.Checked Then
                        bDisableAll = False
                    End If
                    If element.CheckState = CheckState.Unchecked Then
                        bMix = True
                    End If
                    For Each subelement As clsCheckTreeNodes In element.Nodes

                        For Each subsubelement As clsCheckTreeNodes In subelement.Nodes
                            If subsubelement.CheckState = CheckState.Checked Then
                                bDisableAll = False
                            End If
                            If subsubelement.CheckState = CheckState.Unchecked Then
                                bMix = True
                            End If
                        Next
                    Next
                Next
                If bDisableAll Then
                    CType(e.Node.Parent.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked
                ElseIf bMix Then
                    CType(e.Node.Parent.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Indeterminate
                Else
                    CType(e.Node.Parent.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Checked
                End If

                cActionManager.ChangeCurrentMainParameter(
                                                        TabControl_TS.SelectedTab.Name,
                                                        lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Parent.Parent.Parent.Text,
                                                        lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Parent.Parent.Parent.Index,
                                                        HMIMainStepKeys.Enable,
                                                        IIf(CType(e.Node.Parent.Parent.Parent, clsCheckTreeNodes).CheckState = CheckState.Unchecked, "FALSE", "TRUE")
                                                         )
                lListTreeView(TabControl_TS.SelectedTab.Name).DrawMode = TreeViewDrawMode.OwnerDrawText

            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub
    Private Sub TreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        SyncLock _Object
            lListTreeView(TabControl_TS.SelectedTab.Name).Enabled = False
            Try

                If e.Node.Level = 0 Then
                    ShowAllStep()
                End If
                If e.Node.Level = 1 Then
                    ShowMainStep()
                    Dim cMainStepCfg As clsMainStepCfg = cActionManager.GetCurrentMainStepFromIndex(
                                                                                                TabControl_TS.SelectedTab.Name,
                                                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex
                                                                                                )
                    If Not cMainStepCfg Is Nothing Then
                        HmiTextBox_ID.TextBox.Text = cMainStepCfg.MainStepParameter(HMIMainStepKeys.ID)
                        HmiTextBox_Number.TextBox.Text = cMainStepCfg.MainStepParameter(HMIMainStepKeys.Name)
                        HmiTextBox_Description.TextBox.Text = cMainStepCfg.MainStepParameter(HMIMainStepKeys.Description)
                        HmiTextBox_Description2.TextBox.Text = cMainStepCfg.MainStepParameter(HMIMainStepKeys.Description2)
                        RadioButton_Y.Checked = IIf(cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail).ToUpper = "FALSE", False, True)
                        RadioButton_N.Checked = IIf(cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail).ToUpper = "FALSE", True, False)
                    End If
                End If

                If e.Node.Level = 3 Then
                    HmiTextBox_ID.TextBox.Text = ""
                    HmiTextBox_Number.TextBox.Text = ""
                    HmiTextBox_Description.TextBox.Text = ""
                    HmiTextBox_Description2.TextBox.Text = ""
                    HmiTextBox_Component.TextBox.Text = ""
                    HmiTextBox_Picture.TextBox.Text = ""
                    HmiTextBox_Repeat.TextBox.Text = ""
                    strLastType = ""
                    strLastParameter = ""
                    HmiComboBox_ActionType.ComboBox.SelectedIndex = -1

                End If
                If e.Node.Level = 2 Or e.Node.Level = 4 Then
                    CreateType()
                    ShowSubStep()
                    Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(TabControl_TS.SelectedTab.Name,
                                                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex
                                                                                                )
                    If Not cSubStepCfg Is Nothing Then
                        HmiTextBox_ID.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                        HmiTextBox_Number.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Name)
                        HmiTextBox_Description.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Description)
                        HmiTextBox_Description2.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Description2)
                        HmiTextBox_Component.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Component)
                        HmiTextBox_Picture.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Picture)
                        HmiTextBox_Repeat.TextBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.Repeat)
                        strLastType = cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)
                        strLastParameter = cSubStepCfg.SubStepParameter(HMISubStepKeys.Parameter)
                        If cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType) <> "" Then HmiComboBox_ActionType.ComboBox.Text = cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)
                    End If
                End If
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
            lListTreeView(TabControl_TS.SelectedTab.Name).Enabled = True
        End SyncLock
    End Sub

    Private Sub ShowMainStep()
        SyncLock _Object
            ShowButton(enumProgramButtonType.HmiTextBox_ID)
            ShowButton(enumProgramButtonType.HmiTextBox_Number)
            ShowButton(enumProgramButtonType.HmiTextBox_Description)
            If cLanguageManager.SecondLanguageEnable Then
                ShowButton(enumProgramButtonType.HmiTextBox_Description2)
            Else
                HiddenButton(enumProgramButtonType.HmiTextBox_Description2)
            End If
            If cGlobalProgramManager.HasGlobalProgram(cActionManager.VariantCfg.Variant) Then
                HiddenButton(enumProgramButtonType.HmiLabel_Detail)
            Else
                If Not IsNothing(TabControl_TS) AndAlso Not IsNothing(TabControl_TS.SelectedTab) And Not IsNothing(TabControl_TS.SelectedTab.Name) Then
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromID(TabControl_TS.SelectedTab.Name)
                    If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                        HiddenButton(enumProgramButtonType.HmiLabel_Detail)
                    Else
                        ShowButton(enumProgramButtonType.HmiLabel_Detail)
                    End If
                Else
                    ShowButton(enumProgramButtonType.HmiLabel_Detail)
                End If
            End If

            HiddenButton(enumProgramButtonType.HmiTextBox_Component)
            HiddenButton(enumProgramButtonType.HmiTextBox_Picture)
            HiddenButton(enumProgramButtonType.HmiTextBox_Repeat)
            HiddenButton(enumProgramButtonType.HmiComboBox_ActionType)
            HmiComboBox_ActionType.ComboBox.SelectedIndex = -1
            '    CleanFormData()
        End SyncLock
    End Sub

    Private Sub ShowSubStep()
        SyncLock _Object
            ShowButton(enumProgramButtonType.HmiTextBox_ID)
            ShowButton(enumProgramButtonType.HmiTextBox_Number)
            ShowButton(enumProgramButtonType.HmiTextBox_Description)
            If cLanguageManager.SecondLanguageEnable Then
                ShowButton(enumProgramButtonType.HmiTextBox_Description2)
            Else
                HiddenButton(enumProgramButtonType.HmiTextBox_Description2)
            End If
            ShowButton(enumProgramButtonType.HmiTextBox_Component)
            ShowButton(enumProgramButtonType.HmiTextBox_Picture)
            HiddenButton(enumProgramButtonType.HmiTextBox_Repeat)
            HiddenButton(enumProgramButtonType.HmiLabel_Detail)
            ShowButton(enumProgramButtonType.HmiComboBox_ActionType)
            HmiComboBox_ActionType.ComboBox.SelectedIndex = -1
            '   CleanFormData()
        End SyncLock
    End Sub

    Private Sub ShowAllStep()
        SyncLock _Object
            ShowButton(enumProgramButtonType.HmiTextBox_ID)
            ShowButton(enumProgramButtonType.HmiTextBox_Number)
            HiddenButton(enumProgramButtonType.HmiTextBox_Description)
            If cLanguageManager.SecondLanguageEnable Then
                HiddenButton(enumProgramButtonType.HmiTextBox_Description2)
            Else
                HiddenButton(enumProgramButtonType.HmiTextBox_Description2)
            End If
            HiddenButton(enumProgramButtonType.HmiTextBox_Component)
            HiddenButton(enumProgramButtonType.HmiTextBox_Picture)
            HiddenButton(enumProgramButtonType.HmiLabel_Detail)
            HiddenButton(enumProgramButtonType.HmiTextBox_Repeat)
            HiddenButton(enumProgramButtonType.HmiComboBox_ActionType)
            CleanFormData()
        End SyncLock
    End Sub

    Private Sub CleanFormData()
        SyncLock _Object
            HmiTextBox_ID.TextBox.Text = ""
            HmiTextBox_Number.TextBox.Text = ""
            HmiTextBox_Description.TextBox.Text = ""
            HmiTextBox_Description2.TextBox.Text = ""
            HmiTextBox_Component.TextBox.Text = ""
            HmiTextBox_Picture.TextBox.Text = ""
            HmiTextBox_Repeat.TextBox.Text = ""
            HmiComboBox_ActionType.ComboBox.SelectedIndex = -1
        End SyncLock
    End Sub

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            clsCopyInfo.CopyToClipboard(Nothing)
            For Each elment As TreeViewWidthCheckBox In lListTreeView.Values
                elment.SelectedNode = Nothing
            Next
            ShowAllStep()
        End SyncLock
    End Sub

    Public Sub ParameterChanged(ByVal sender As Object, ByVal e As Kochi.HMI.MainControl.Action.ParameterEvent)
        SyncLock _Object
            Try
                If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                    If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                        If HmiComboBox_ActionType.ComboBox.SelectedIndex <> -1 Then
                            If strLastType = HmiComboBox_ActionType.ComboBox.Text Then
                                strLastParameter = clsParameter.ToString(e.ListParameter)
                            End If

                            cActionManager.ChangeCurrentSubParameter(
                                                                TabControl_TS.SelectedTab.Name,
                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                                HMISubStepKeys.Parameter,
                                                                clsParameter.ToString(e.ListParameter)
                                                                )
                        End If
                    End If
                End If
                Button_Save.Enabled = cActionManager.IsChanged
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub

    Private Sub TextBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        SyncLock _Object
            Select Case sender.name
                Case "HmiTextBox_Number"
                    If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                        If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                            If cActionManager.HasCurrentMainStepName(
                                                                TabControl_TS.SelectedTab.Name,
                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectText,
                                                                HmiTextBox_Number.TextBox.Text
                                                                ) Then
                                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "3", HmiTextBox_Number.TextBox.Text), enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                            End If
                            cActionManager.ChangeCurrentMainParameter(
                                                                    TabControl_TS.SelectedTab.Name,
                                                                    lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                                    lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                                    HMIMainStepKeys.Name,
                                                                    HmiTextBox_Number.TextBox.Text
                                                                    )
                            lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text = HmiTextBox_Number.TextBox.Text
                        End If
                        If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                            If cActionManager.HasCurrentSubStepName(
                                                                TabControl_TS.SelectedTab.Name,
                                                                lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentParentText,
                                                                HmiTextBox_Number.TextBox.Text
                                                                ) Then
                                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "4", HmiTextBox_Number.TextBox.Text), enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                            End If
                            cActionManager.ChangeCurrentSubParameter(
                                                                 TabControl_TS.SelectedTab.Name,
                                                                 lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentParentText,
                                                                 lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentParentIndex,
                                                                 lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                                 HMIMainStepKeys.Name,
                                                                 HmiTextBox_Number.TextBox.Text
                                                                 )
                            lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text = HmiTextBox_Number.TextBox.Text
                        End If
                    End If
            End Select
        End SyncLock
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                Select Case sender.name
                    Case "HmiTextBox_Number"
                        If IsNothing(TabControl_TS) Then Return
                        If IsNothing(TabControl_TS.SelectedTab) Then Return
                        If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 AndAlso HmiTextBox_Number.TextBox.Text <> "" Then
                                cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
                                If cActionManager.HasCurrentMainStepName(
                                                                         TabControl_TS.SelectedTab.Name,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                                         HmiTextBox_Number.TextBox.Text
                                                                         ) Then
                                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "3", HmiTextBox_Number.TextBox.Text), enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                                    Return
                                End If
                                cActionManager.ChangeCurrentMainParameter(
                                                                          TabControl_TS.SelectedTab.Name,
                                                                          lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                                          lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                                          HMIMainStepKeys.Name, HmiTextBox_Number.TextBox.Text
                                                                          )
                                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text = HmiTextBox_Number.TextBox.Text
                            End If
                            If (lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4) AndAlso HmiTextBox_Number.TextBox.Text <> "" Then
                                cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
                                If cActionManager.HasCurrentSubStepName(
                                                                        TabControl_TS.SelectedTab.Name,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                                        HmiTextBox_Number.TextBox.Text
                                                                        ) Then
                                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "4", HmiTextBox_Number.TextBox.Text), enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
                                End If
                                cActionManager.ChangeCurrentSubParameter(
                                                                        TabControl_TS.SelectedTab.Name,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                        lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                                        HMIMainStepKeys.Name,
                                                                        HmiTextBox_Number.TextBox.Text
                                                                        )
                                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Text = HmiTextBox_Number.TextBox.Text
                            End If
                        End If

                    Case "HmiTextBox_Description", "HmiTextBox_Description2", "HmiTextBox_Picture", "HmiTextBox_Component", "HmiTextBox_Repeat"
                        If IsNothing(TabControl_TS) Then Return
                        If IsNothing(TabControl_TS.SelectedTab) Then Return
                        If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                                cActionManager.ChangeCurrentMainParameter(
                                                                         TabControl_TS.SelectedTab.Name,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                                         sender.name.ToString.Split("_")(1),
                                                                         CType(sender, TextBox).Text
                                                                         )
                            End If
                            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Or lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                                cActionManager.ChangeCurrentSubParameter(
                                                                         TabControl_TS.SelectedTab.Name,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex,
                                                                         lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex,
                                                                         sender.name.ToString.Split("_")(1),
                                                                         CType(sender, TextBox).Text
                                                                         )
                            End If
                        End If
                End Select
                Button_Save.Enabled = cActionManager.IsChanged
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Private Sub TreeView_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        SyncLock _Object
            If e.Button = Windows.Forms.MouseButtons.Right Then
                lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode = e.Node
                PopupCopy.Enabled = e.Node.Level > 0
                Dim cpy As clsCopyInfo = clsCopyInfo.GetClipboardContent()
                If Not cpy Is Nothing Then
                    Me.PopupPaste.Enabled = (e.Node.Level = 0 And cpy.Type = ListType.MainStep) _
                                Or (e.Node.Level = 1 And cpy.Type = ListType.SubStep) _
                                Or (e.Node.Level = 2 And cpy.Type = ListType.SubStep) _
                                Or (e.Node.Level = 3 And (cpy.Type = ListType.PreSub Or cpy.Type = ListType.SubPass Or cpy.Type = ListType.SubFail Or cpy.Type = ListType.SubSubStep)) _
                                Or (e.Node.Level = 4 And cpy.Type = ListType.SubSubStep)
                Else
                    Me.PopupPaste.Enabled = False
                End If

                If Not cpy Is Nothing Then
                    Me.PopupPasteAll.Enabled = (e.Node.Level = 3 And ((cpy.Type = ListType.PreSub And e.Node.Index = 0) Or (cpy.Type = ListType.SubPass And e.Node.Index = 1) Or (cpy.Type = ListType.SubFail And e.Node.Index = 2)))
                Else
                    Me.PopupPasteAll.Enabled = False
                End If

                PopupCopy.Font = New Font(lListTreeView(TabControl_TS.SelectedTab.Name).Font.Name, lListTreeView(TabControl_TS.SelectedTab.Name).Font.Size)
                PopupPaste.Font = New Font(lListTreeView(TabControl_TS.SelectedTab.Name).Font.Name, lListTreeView(TabControl_TS.SelectedTab.Name).Font.Size)
                PopupPasteAll.Font = New Font(lListTreeView(TabControl_TS.SelectedTab.Name).Font.Name, lListTreeView(TabControl_TS.SelectedTab.Name).Font.Size)
                ContextMenuStrip_Menu.Show(lListTreeView(TabControl_TS.SelectedTab.Name), e.Location)
            End If
        End SyncLock
    End Sub

    Private Sub Copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                Dim cpy As New clsCopyInfo
                cpy.Type = ListType.MainStep
                cpy.StepData = cActionManager.GetCurrentMainStepFromIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex)
                cpy.ViewData = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Clone
                clsCopyInfo.CopyToClipboard(cpy)
            End If

            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 2 Then
                Dim cpy As New clsCopyInfo
                cpy.Type = ListType.SubStep
                cpy.StepData = cActionManager.GetCurrentSubStepFromIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                cpy.ViewData = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Clone
                clsCopyInfo.CopyToClipboard(cpy)
            End If

            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 3 Then
                Dim cTreeViewWidthCheckBox As TreeViewWidthCheckBox = lListTreeView(TabControl_TS.SelectedTab.Name).Clone
                Dim c As TreeNode = cTreeViewWidthCheckBox.SelectedNode
                cTreeViewWidthCheckBox.SelectedNode = cTreeViewWidthCheckBox.SelectedNode.Parent
                Dim cpy As New clsCopyInfo
                Select Case lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Index
                    Case 0
                        cpy.Type = ListType.PreSub
                    Case 1
                        cpy.Type = ListType.SubPass
                    Case 2
                        cpy.Type = ListType.SubFail
                End Select

                cpy.StepData = cActionManager.GetCurrentSubStepFromIndex(TabControl_TS.SelectedTab.Name, cTreeViewWidthCheckBox.GetVirtualSelectParentParentText, cTreeViewWidthCheckBox.GetVirtualSelectParentIndex, cTreeViewWidthCheckBox.GetVirtualSelectIndex)
                cTreeViewWidthCheckBox.SelectedNode = c
                cpy.ViewData = cTreeViewWidthCheckBox.SelectedNode.Clone
                clsCopyInfo.CopyToClipboard(cpy)
            End If

            If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 4 Then
                Dim cpy As New clsCopyInfo
                cpy.Type = ListType.SubSubStep
                cpy.StepData = cActionManager.GetCurrentSubStepFromIndex(TabControl_TS.SelectedTab.Name, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentParentText, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectParentIndex, lListTreeView(TabControl_TS.SelectedTab.Name).GetVirtualSelectIndex)
                cpy.ViewData = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Clone
                clsCopyInfo.CopyToClipboard(cpy)
            End If
        End SyncLock
    End Sub

  
    Private Sub RefreshUI()
        While True
            Try
                If bExit Then Exit While
                Application.DoEvents()
                mMainForm.InvokeAction(Sub() CheckButton())
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(ex, enumExceptionType.Alarm)
            End Try
            System.Threading.Thread.Sleep(100)
        End While
    End Sub

    Private Sub CheckButton()
        If IsNothing(TabControl_TS.SelectedTab) Then
            Return
        End If
        If Not lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode Is Nothing Then
            Button_Add.Enabled = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 0
            Button_Del.Enabled = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 1 And lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level <> 3
            Button_Up.Enabled = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 1 And lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level <> 3
            Button_Down.Enabled = lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level >= 1 And lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level <> 3
        Else
            Button_Add.Enabled = False
            Button_Del.Enabled = False
            Button_Up.Enabled = False
            Button_Down.Enabled = False
        End If
    End Sub

    Private Sub SaveFunction()
        SyncLock _Object
            Save()
        End SyncLock
    End Sub

    Private Sub AbortFunction()
        SyncLock _Object
            Try
                If Not IsNothing(IActionUI) Then IActionUI.Quit(cLocalElement, cSystemElement)
                cActionManager.CancelCurrentActionCfg()
                LoadData(True)
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
            End Try
        End SyncLock
    End Sub
    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentProgramUI.Quit
        Try
            If cActionManager.IsChanged Then
                cChangePage.BackPage()
                cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "5"), enumExceptionType.Confirm, enumUIName.ParentProgramForm.ToString))
                Return False
            End If
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
            If Not IsNothing(IActionUI) Then IActionUI.Quit(cLocalElement, cSystemElement)
            If Not IsNothing(cHMIActionBase) Then
                RemoveHandler cHMIActionBase.ParameterChanged, AddressOf ParameterChanged
                cHMIActionBase.ActionUI.Quit(cLocalElement, cSystemElement)
                Panel_Action.Controls.Clear()
            End If
            If Not IsNothing(cProgramDebug) Then cProgramDebug.Quit(cLocalElement, cSystemElement)
            cErrorMessageManager.Clean(enumUIName.ParentProgramForm.ToString)
            cErrorMessageManager.DisposeAbortFunction()
            cErrorMessageManager.DisposeSaveFunction()
            cErrorMessageManager.Dispose()
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function


    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub


    Private Sub Panel_UI_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(cLocalFormFontResize) Then
            cLocalFormFontResize.WinFromH = mMainForm.Panel_Body.Height
            cLocalFormFontResize.newExH = Panel_UI.Height
            If cFormFontResize.Resized And cLocalFormFontResize.Resized Then
                cLocalFormFontResize.cons = Panel_UI
                cLocalFormFontResize.ChangeFontSize()
            End If
            cLocalFormFontResize.OldExH = Panel_UI.Height
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If Not IsNothing(lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode) Then
                If lListTreeView(TabControl_TS.SelectedTab.Name).SelectedNode.Level = 1 Then
                    cActionManager.ChangeCurrentMainParameter(
                                                             TabControl_TS.SelectedTab.Name,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectParentText,
                                                             lListTreeView(TabControl_TS.SelectedTab.Name).GetSelectIndex,
                                                             HMIMainStepKeys.ShowDetail,
                                                             IIf(RadioButton_Y.Checked, "TRUE", "FALSE")
                                                             )
                End If
            End If
            Button_Save.Enabled = cActionManager.IsChanged
        End SyncLock
    End Sub
    Private Sub TabControl_Program_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            If TabControl_Program.SelectedTab.Name = "Action" Then
                cProgramDebug.StopRefreshUI()
            End If
            If TabControl_Program.SelectedTab.Name = "Debug" Then
                cProgramDebug.StartRefreshUI()
            End If
        End SyncLock
    End Sub

End Class