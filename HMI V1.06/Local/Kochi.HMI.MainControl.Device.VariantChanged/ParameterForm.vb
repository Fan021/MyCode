Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.Windows.Forms
Public Class ParameterForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cTextFont As Font
    Private cVariantManager As clsVariantManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    Private cDeviceManager As clsDeviceManager
    Protected cIO As clsVariantChanged
    Private cMachineManager As clsMachineManager
    Public Property IO As clsVariantChanged
        Set(ByVal value As clsVariantChanged)
            cIO = value
        End Set
        Get
            Return cIO
        End Get
    End Property
    Public Property TextFont As Font
        Set(ByVal value As Font)
            cTextFont = value
        End Set
        Get
            Return cTextFont
        End Get
    End Property

    Public Property isReadOnly As Boolean
        Set(ByVal value As Boolean)
            ReadOnly_Y.Checked = value
            ReadOnly_N.Checked = Not value
        End Set
        Get
            Return ReadOnly_Y.Checked
        End Get
    End Property

    Public Property isVariantChange As Boolean
        Set(ByVal value As Boolean)
            VariantChange_Y.Checked = value
            VariantChange_N.Checked = Not value
        End Set
        Get
            Return VariantChange_Y.Checked
        End Get
    End Property

    Public Property TextValue As String
        Set(ByVal value As String)
            TextBox_Text.Text = value
        End Set
        Get
            Return TextBox_Text.Text
        End Get
    End Property

    Public Property Index As Integer
        Set(ByVal value As Integer)
            TextBox_ID.Text = value
        End Set
        Get
            Return TextBox_ID.Text
        End Get
    End Property

    Public Property AdsLength As String
        Set(ByVal value As String)
            HmiTextBox_AdsLength.TextBox.Text = value
        End Set
        Get
            Return HmiTextBox_AdsLength.TextBox.Text
        End Get
    End Property

    Public Property AdsName As String
        Set(ByVal value As String)
            HmiTextBox_AdsName.TextBox.Text = value
        End Set
        Get
            Return HmiTextBox_AdsName.TextBox.Text
        End Get
    End Property

    Public Property AdsType As String
        Set(ByVal value As String)
            HmiComboBox_AdsType.ComboBox.SelectedIndex = -1
            For i = 0 To HmiComboBox_AdsType.ComboBox.Items.Count - 1
                If HmiComboBox_AdsType.ComboBox.Items(i) = value Then
                    HmiComboBox_AdsType.ComboBox.SelectedIndex = i
                End If
            Next
        End Set
        Get
            Return HmiComboBox_AdsType.ComboBox.Text
        End Get
    End Property

    Public Property VariantName As String
        Set(ByVal value As String)
            HmiComboBox_VariantName.ComboBox.SelectedIndex = -1
            For i = 0 To HmiComboBox_VariantName.ComboBox.Items.Count - 1
                If HmiComboBox_VariantName.ComboBox.Items(i) = value Then
                    HmiComboBox_VariantName.ComboBox.SelectedIndex = i
                End If
            Next
        End Set
        Get
            Return HmiComboBox_VariantName.ComboBox.Text
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cIO.Name)

        HmiComboBox_VariantName.ComboBox.Items.Clear()
        For Each element As String In cMachineManager.VaiantElememtManager.ListElement
            HmiComboBox_VariantName.ComboBox.Items.Add(element)
        Next
        HmiComboBox_AdsType.ComboBox.Items.Clear()
        HmiComboBox_AdsType.ComboBox.Items.Add("Boolean")
        HmiComboBox_AdsType.ComboBox.Items.Add("Integer")
        HmiComboBox_AdsType.ComboBox.Items.Add("Single")
        HmiComboBox_AdsType.ComboBox.Items.Add("Double")
        HmiComboBox_AdsType.ComboBox.Items.Add("String")
        Return True
    End Function

    Private Sub IOParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        Label_ID.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_ID")
        Label_ReadOnly.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_ReadOnly")
        Label_VariantChange.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_VariantChange")
        Label_VariantName.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_VariantName")
        Label_AdsName.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_AdsName")
        Label_AdsType.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_AdsType")
        Label_AdsLength.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_AdsLength")
        Label_Text.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Label_Text")
        Button_Save.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetUserTextLine("VariantChanged", "Button_Cancel")
        Label_ID.Font = cTextFont
        TextBox_ID.Font = cTextFont
        Label_ReadOnly.Font = cTextFont
        ReadOnly_Y.Font = cTextFont
        ReadOnly_N.Font = cTextFont
        Label_VariantChange.Font = cTextFont
        VariantChange_Y.Font = cTextFont
        VariantChange_N.Font = cTextFont
        Label_Text.Font = cTextFont
        TextBox_Text.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_VariantName.Font = cTextFont
        HmiComboBox_VariantName.ComboBox.Font = cTextFont
        Label_AdsName.Font = cTextFont
        HmiTextBox_AdsName.TextBox.Font = cTextFont
        Label_AdsType.Font = cTextFont
        HmiComboBox_AdsType.ComboBox.Font = cTextFont
        Label_AdsLength.Font = cTextFont
        HmiTextBox_AdsLength.TextBox.Font = cTextFont
        HmiTextBox_AdsLength.ValueType = GetType(Integer)
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Height = (TextBox_ID.Height + 6) * 8 + 180
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = TextBox_ID.Height + 6
            Next
            '   ListView_Variant.Dock = DockStyle.Fill
            '  ListView_Variant.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            '   ListView_Variant.Refresh()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Me.Close()
    End Sub
End Class
