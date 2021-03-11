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
    Protected cIO As clsAnalog
    Public Property IO As clsAnalog
        Set(ByVal value As clsAnalog)
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

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cIO.Name)
        Return True
    End Function

    Private Sub IOParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        Label_ID.Text = cLanguageManager.GetUserTextLine("Analog", "Label_ID")
        Label_ReadOnly.Text = cLanguageManager.GetUserTextLine("Analog", "Label_ReadOnly")
        Label_VariantChange.Text = cLanguageManager.GetUserTextLine("Analog", "Label_VariantChange")
        Label_Text.Text = cLanguageManager.GetUserTextLine("Analog", "Label_Text")
        Button_Save.Text = cLanguageManager.GetUserTextLine("Analog", "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetUserTextLine("Analog", "Button_Cancel")
        TabPage1.Text = cLanguageManager.GetUserTextLine("Analog", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("Analog", "TabPage2")
        TabControl1.Font = cTextFont
        TabPage1.Font = cTextFont
        TabPage2.Font = cTextFont
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

        Dim PostTest_ID As New DataGridViewTextBoxColumn
        PostTest_ID = New DataGridViewTextBoxColumn
        PostTest_ID.HeaderText = cLanguageManager.GetUserTextLine("Analog", "PostTest_ID")
        PostTest_ID.Name = "PostTest_ID"
        PostTest_ID.ReadOnly = True
        ListView_Variant.Columns.Add(PostTest_ID)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name = New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("Analog", "PostTest_Name")
        PostTest_Name.Name = "PostTest_Name"
        PostTest_Name.ReadOnly = True
        ListView_Variant.Columns.Add(PostTest_Name)

        Dim PostTest_Value As New DataGridViewTextBoxColumn
        PostTest_Value = New DataGridViewTextBoxColumn
        PostTest_Value.HeaderText = cLanguageManager.GetUserTextLine("Analog", "PostTest_Value")
        PostTest_Value.Name = "PostTest_Value"
        PostTest_Value.ReadOnly = False
        ListView_Variant.Columns.Add(PostTest_Value)

        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            Dim strValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", element.Variant, Index.ToString)
            ListView_Variant.Rows.Add((ListView_Variant.Rows.Count + 1).ToString, element.Variant, strValue)
        Next
        '  ListView_Variant.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Height = (TextBox_ID.Height + 6) * 5 + 180
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
