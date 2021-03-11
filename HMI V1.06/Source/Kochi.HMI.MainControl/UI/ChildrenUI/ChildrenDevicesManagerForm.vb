Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenDevicesManagerForm
    Implements IChildrenDevicesManagerUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cDeviceManager As clsDeviceManager
    Private cDeviceLibManger As clsDeviceLibManager
    Private cHmiDevice As clsHMIDeviceBase
    Private cFormFontResize As clsFormFontResize
    Private cErrorMessageManager As clsErrorMessageManager
    Private cLanguageManager As clsLanguageManager
    Private IDeviceUI As IInitUI
    Private strButtonName As String
    Private cMachineManager As clsMachineManager
    Private strLastType As String = String.Empty
    Private strLastParameter As String = String.Empty
    Private _Object As New Object
    Private cMainFormButtonManager As clsMainButtonManager
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

    Public ReadOnly Property ComboBox_Type As HMIComboBox Implements IChildrenDevicesManagerUI.ComboBox_Type
        Get
            Return HmiComboBox_Type
        End Get
    End Property

    Public ReadOnly Property TextBox_ID As HMITextBox Implements IChildrenDevicesManagerUI.TextBox_ID
        Get
            Return HmiTextBox_ID
        End Get
    End Property

    Public ReadOnly Property TextBox_Name As HMITextBox Implements IChildrenDevicesManagerUI.TextBox_Name
        Get
            Return HmiTextBox_Name
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenDevicesManagerUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cDeviceLibManger = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cErrorMessageManager.RegisterSaveFunction(AddressOf SaveFunction)
            cErrorMessageManager.RegisterAbortFunction(AddressOf AbortFunction)
            InitControlText()
            InitForm()
            cLocalElement.Add(enumUIName.ChildrenDevicesManagerForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenDevicesManagerForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        RemoveHandler Button_Up.Click, AddressOf Button_Up_Click
        RemoveHandler Button_Down.Click, AddressOf Button_Down_Click
        RemoveHandler Button_Add.Click, AddressOf Button_Add_Click
        RemoveHandler Button_Del.Click, AddressOf Button_Del_Click
        RemoveHandler HmiTextBox_ID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        RemoveHandler HmiTextBox_Name.TextBox.KeyUp, AddressOf TextBox_Name_KeyUp
        RemoveHandler HmiComboBox_Type.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        RemoveHandler HmiComboBox_StationID.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect

        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        HmiTextBox_ID.TextBox.Text = ""
        HmiTextBox_Index.TextBox.Text = ""
        HmiTextBox_Name.TextBox.Text = ""
        HmiComboBox_StationID.ComboBox.SelectedIndex = -1
        HmiComboBox_Type.ComboBox.SelectedIndex = -1
        Panel_Init.Controls.Clear()
        TreeView_Devices.SelectedNode = Nothing
        TopLevel = False
        Me.TreeView_Devices.Nodes.Clear()
        Me.TreeView_Devices.Nodes.Add("Devices", "Devices")
        For Each devIndex As Integer In cDeviceManager.GetDevicesListKey
            Dim dev As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(devIndex)
            Me.TreeView_Devices.Nodes(0).Nodes.Add(dev.Name, dev.Name)
        Next
        Me.TreeView_Devices.Nodes(0).Expand()

        HmiComboBox_Type.ComboBox.Items.Clear()
        For Each elementIndex As String In cDeviceLibManger.GetListDllKey
            Dim element As clsDeviceLibCfg = cDeviceLibManger.GetDeviceLibCfgFromKey(elementIndex)
            HmiComboBox_Type.ComboBox.Items.Add(element.DeviceName)
        Next

        HmiComboBox_StationID.ComboBox.Items.Clear()
        HmiComboBox_StationID.ComboBox.Items.Add(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "Global"))
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            HmiComboBox_StationID.ComboBox.Items.Add(element.StationName)
        Next
        AddHandler Button_Add.Click, AddressOf Button_Add_Click
        AddHandler Button_Del.Click, AddressOf Button_Del_Click
        AddHandler Button_Up.Click, AddressOf Button_Up_Click
        AddHandler Button_Down.Click, AddressOf Button_Down_Click
        AddHandler HmiTextBox_ID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_Name.TextBox.KeyUp, AddressOf TextBox_Name_KeyUp
        AddHandler HmiComboBox_Type.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler HmiComboBox_StationID.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
        AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect

        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "HmiLabel_ID")
        HmiLabel_Name.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "HmiLabel_Name")
        HmiLabel_Type.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "HmiLabel_Type")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "Button_Save")
        GroupBox_Devices.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "GroupBox_Devices")
        GroupBox_Parameter.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "GroupBox_Parameter")
        GroupBox_Property.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "GroupBox_Property")
        GroupBox_Init.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "GroupBox_Init")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "Button_Save")
        HmiLabel_Index.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "HmiLabel_Index")
        HmiLabel_StationID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "HmiLabel_StationID")
        HmiTextBox_ID.TextBoxReadOnly = True
        HmiTextBox_Index.TextBoxReadOnly = True

        Button_Save.Enabled = cDeviceManager.IsChanged
            Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body_Mid.RowStyles(0).Height = (HmiTextBox_ID.TextBox.Height + 6 + 6) * TableLayoutPanel_Body_Mid_Head.RowCount + HmiTextBox_ID.TextBox.Height + 6
            GroupBox_Property.Height = (HmiTextBox_ID.TextBox.Height + 6 + 6) * TableLayoutPanel_Body_Mid_Head.RowCount + HmiTextBox_ID.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Mid_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_ID.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub


    Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.AddCurrentDevice()
            RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
            Me.TreeView_Devices.Nodes(0).Nodes.Add(cDeviceCfg.Name, cDeviceCfg.Name)
            UpdateListTreeName()
            AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
            TreeView_Devices.SelectedNode = Nothing
            TreeView_Devices.SelectedNode = TreeView_Devices.Nodes(0).Nodes(cDeviceManager.GetStationDownMaxIndex("0") - 1)
            Me.TreeView_Devices.Nodes(0).Expand()
            Me.HmiComboBox_Type.ComboBox.SelectedIndex = -1
            Button_Save.Enabled = cDeviceManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub Button_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
                If Me.TreeView_Devices.SelectedNode.Level = 1 Then
                    Dim SelectActionNode As TreeNode = TreeView_Devices.SelectedNode
                    Dim nSelIndex As Int32 = SelectActionNode.Index
                    If nSelIndex < 1 Then nSelIndex = 1
                    RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                    cDeviceManager.DeleteCurrentDevice(Integer.Parse(HmiTextBox_ID.TextBox.Text))
                    Me.TreeView_Devices.SelectedNode.Remove()
                    UpdateListTreeName()
                    AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                    TreeView_Devices.SelectedNode = Nothing
                    If TreeView_Devices.Nodes(0).Nodes.Count >= 1 Then
                        TreeView_Devices.SelectedNode = TreeView_Devices.Nodes(0).Nodes(nSelIndex - 1)
                    Else
                        HmiTextBox_ID.TextBox.Text = ""
                        HmiTextBox_Name.TextBox.Text = ""
                        HmiTextBox_Index.TextBox.Text = ""
                        HmiComboBox_StationID.ComboBox.SelectedIndex = -1
                        SelectedType("")
                    End If

                End If
            End If
            Button_Save.Enabled = cDeviceManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub Button_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
            If TreeView_Devices.SelectedNode.Level = 1 Then
                Dim SelectActionNode As TreeNode = TreeView_Devices.SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex <= 0 Then Return
                RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                cDeviceManager.UpDeviceByIndex(nSelIndex)
                TreeView_Devices.UpFirstNode()
                UpdateListTreeName()
                AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                TreeView_Devices.SelectedNode = Nothing
                TreeView_Devices.SelectedNode = TreeView_Devices.Nodes(0).Nodes(nSelIndex - 1)
                Button_Save.Enabled = cDeviceManager.IsChanged
            End If

        End If
    End Sub

    Private Sub Button_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
            If TreeView_Devices.SelectedNode.Level = 1 Then
                Dim SelectActionNode As TreeNode = TreeView_Devices.SelectedNode
                Dim nSelIndex As Int32 = SelectActionNode.Index
                If nSelIndex + 1 >= TreeView_Devices.SelectedNode.Parent.Nodes.Count Then Return
                RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                cDeviceManager.DownDeviceByIndex(nSelIndex)
                TreeView_Devices.DownFirstNode()
                UpdateListTreeName()
                AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                TreeView_Devices.SelectedNode = Nothing
                TreeView_Devices.SelectedNode = TreeView_Devices.Nodes(0).Nodes(nSelIndex + 1)
                Button_Save.Enabled = cDeviceManager.IsChanged
            End If
        End If
    End Sub

    Private Sub UpdateListTreeName()
        For Each elementTreeNode As TreeNode In TreeView_Devices.Nodes(0).Nodes
            elementTreeNode.Text = cDeviceManager.GetCurrentDeviceCfgFromKey(elementTreeNode.Index).Name
        Next
    End Sub

    Private Sub TreeView_Devices_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        SyncLock _Object
            Try
                If e.Node.Level = 1 Then
                    Dim dev As clsDeviceCfg = cDeviceManager.GetCurrentDeviceFromName(TreeView_Devices.SelectedNode.Text)
                    If Not dev Is Nothing Then
                        '  RemoveHandler HmiComboBox_StationID.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
                        HmiTextBox_ID.TextBox.Text = dev.ID
                        HmiTextBox_Name.TextBox.Text = dev.Name
                        HmiTextBox_Index.TextBox.Text = dev.StationIndex.ToString
                        strLastType = dev.DeviceType
                        strLastParameter = dev.InitParameter
                        If HmiComboBox_StationID.ComboBox.Items.Count >= CInt(dev.StationID) Then
                            HmiComboBox_StationID.ComboBox.SelectedIndex = CInt(dev.StationID)
                        End If
                        SelectedType(dev.DeviceType)
                        ' AddHandler HmiComboBox_StationID.ComboBox.SelectedIndexChanged, AddressOf ComboBox_Type_SelectedIndexChanged
                    End If
                End If
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
            End Try
        End SyncLock
    End Sub


    Private Sub TextBox_Name_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            cErrorMessageManager.Clean(enumUIName.ChildrenDevicesManagerForm.ToString, New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "1")))
            If HmiTextBox_Name.TextBox.Text = "" Then Return
            If TreeView_Devices.SelectedNode Is Nothing Then Return
            Me.TreeView_Devices.SelectedNode.Text = HmiTextBox_Name.TextBox.Text
            If Not cDeviceManager.HasCurrentDevice(HmiTextBox_Name.TextBox.Text, TreeView_Devices.SelectedNode.Index) Then
                cDeviceManager.ChangeCurrentDeviceName(HmiTextBox_ID.TextBox.Text, HmiTextBox_Name.TextBox.Text)
            Else
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
            End If
            Button_Save.Enabled = cDeviceManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub


    Private Sub ComboBox_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                Select Case sender.name
                    Case "HmiComboBox_Type"
                        If Not IsNothing(cHmiDevice) Then
                            RemoveHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
                            cHmiDevice.InitUI.Quit(cLocalElement, cSystemElement)
                        End If

                        If HmiComboBox_Type.ComboBox.SelectedIndex <> -1 Then
                            If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
                                If Me.TreeView_Devices.SelectedNode.Level = 1 Then
                                    cDeviceManager.ChangeCurrentDeviceType(HmiTextBox_ID.TextBox.Text, HmiComboBox_Type.ComboBox.Text)
                                    Dim dev As clsDeviceCfg = cDeviceManager.GetCurrentDeviceFromName(TreeView_Devices.SelectedNode.Text)
                                    If Not dev Is Nothing Then
                                        HmiTextBox_Index.TextBox.Text = dev.StationIndex.ToString
                                    End If
                                    cHmiDevice = cDeviceLibManger.GetDeviceLibCfgFromKey(HmiComboBox_Type.ComboBox.Text).Source
                                    cDeviceManager.ChangeCurrentSource(HmiTextBox_ID.TextBox.Text, cHmiDevice)
                                    AddHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
                                    cHmiDevice.CreateInitUI(cLocalElement, cSystemElement)
                                    IDeviceUI = cHmiDevice.InitUI
                                    IDeviceUI.Init(cLocalElement, cSystemElement)
                                    Panel_Init.Controls.Clear()
                                    If strLastType = HmiComboBox_Type.ComboBox.Text Then
                                        IDeviceUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetCurrentDeviceFromID(HmiTextBox_ID.TextBox.Text).InitParameter))
                                    Else
                                        IDeviceUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(""))
                                    End If

                                    cFormFontResize.SetControls(cFormFontResize.CurrentRate, IDeviceUI.UI)
                                    Panel_Init.Controls.Add(IDeviceUI.UI)
                                End If
                            End If
                        End If

                    Case "HmiComboBox_StationID"
                        ' If HmiComboBox_Type.ComboBox.SelectedIndex <> -1 Then
                        If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
                            If Me.TreeView_Devices.SelectedNode.Level = 1 Then
                                Dim strDeviceName As String = TreeView_Devices.SelectedNode.Text
                                RemoveHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                                cDeviceManager.ChangeCurrentStationID(HmiTextBox_ID.TextBox.Text, HmiComboBox_StationID.ComboBox.SelectedIndex)
                                UpdateListTreeName()
                                AddHandler TreeView_Devices.AfterSelect, AddressOf TreeView_Devices_AfterSelect
                                TreeView_Devices.SelectedNode = Nothing
                                If cDeviceManager.HasCurrentDevice(strDeviceName) Then
                                    TreeView_Devices.SelectedNode = TreeView_Devices.Nodes(0).Nodes(CInt(cDeviceManager.GetCurrentDeviceFromName(strDeviceName).ID) - 1)
                                End If
                            End If
                        End If
                End Select
                Button_Save.Enabled = cDeviceManager.IsChanged
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
            End Try
        End SyncLock
    End Sub

    Public Sub ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
        Try
            If HmiComboBox_Type.ComboBox.SelectedIndex <> -1 Then
                If Not IsNothing(Me.TreeView_Devices.SelectedNode) Then
                    If Me.TreeView_Devices.SelectedNode.Level = 1 Then
                        If strLastType = HmiComboBox_Type.ComboBox.Text Then
                            strLastParameter = clsParameter.ToString(e.ListParameter)
                        End If
                        cDeviceManager.ChangeCurrentParameter(HmiTextBox_ID.TextBox.Text, clsParameter.ToString(e.ListParameter))
                    End If
                End If
            End If
            Button_Save.Enabled = cDeviceManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub


    Private Sub SelectedType(ByVal DeviceType As String)
        HmiComboBox_Type.ComboBox.SelectedIndex = -1
        Panel_Init.Controls.Clear()
        If Not IsNothing(DeviceType) Then
            HmiComboBox_Type.ComboBox.Text = DeviceType
        End If

    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click
        Try
            If cDeviceManager.CheckCurrentDeviceCfg(cLocalElement) Then
                If cDeviceManager.SaveCurrentDeviceCfg() Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "2"), enumExceptionType.Normal, enumUIName.ChildrenDevicesManagerForm.ToString))
                Else
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "3"), enumExceptionType.Normal, enumUIName.ChildrenDevicesManagerForm.ToString))
                End If
                cMainFormButtonManager.DisableMainLeftButtonEx()
                Button_Save.Enabled = cDeviceManager.IsChanged
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub SaveFunction()
        Try
            If cDeviceManager.CheckCurrentDeviceCfg(cLocalElement) Then
                If cDeviceManager.SaveCurrentDeviceCfg() Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "2"), enumExceptionType.Normal, enumUIName.ChildrenDevicesManagerForm.ToString))
                Else
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "3"), enumExceptionType.Normal, enumUIName.ChildrenDevicesManagerForm.ToString))
                End If
                cMainFormButtonManager.DisableMainLeftButtonEx()
                Button_Save.Enabled = cDeviceManager.IsChanged
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Private Sub AbortFunction()
        Try
            cDeviceManager.CancelCurrentDeviceCfg()
            InitForm()
            Button_Save.Enabled = cDeviceManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
        End Try
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenDevicesManagerUI.Quit
        Try
            If cDeviceManager.IsChanged Then
                Button_Save.Enabled = False
                cErrorMessageManager.Clean(enumUIName.ChildrenDevicesManagerForm.ToString)
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenDevicesManagerForm.ToString, "4"), enumExceptionType.Confirm, enumUIName.ChildrenDevicesManagerForm.ToString))
                Return False
            End If
            If Not IsNothing(cHmiDevice) Then
                RemoveHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
                cHmiDevice.InitUI.Quit(cLocalElement, cSystemElement)
            End If
            cLocalElement.Remove(enumUIName.ChildrenDevicesManagerForm.ToString)
            cErrorMessageManager.Clean(enumUIName.ChildrenDevicesManagerForm.ToString)
            
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenDevicesManagerForm.ToString))
            Return False
        End Try
    End Function

End Class