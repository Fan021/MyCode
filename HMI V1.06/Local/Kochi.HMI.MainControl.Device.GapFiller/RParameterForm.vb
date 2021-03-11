Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.Windows.Forms
Public Class RParameterForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cTextFont As Font
    Private iIndex As Integer
    Public Property TextFont As Font
        Set(ByVal value As Font)
            cTextFont = value
        End Set
        Get
            Return cTextFont
        End Get
    End Property

    Public Property isReserve As Boolean
        Set(ByVal value As Boolean)
            RadioButton_Y.Checked = value
            RadioButton_N.Checked = Not value
        End Set
        Get
            Return RadioButton_Y.Checked
        End Get
    End Property

    Public Property Index As Integer
        Set(ByVal value As Integer)
            iIndex = value
        End Set
        Get
            Return iIndex
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Return True
    End Function

    Private Sub IOParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        ComboBox_Model.Items.Clear()
        For i = 0 To 99
            ComboBox_Model.Items.Add("R" + i.ToString)
        Next
        ComboBox_Model.SelectedIndex = Index
        Label_ID.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_ID")
        Label_Reserve.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Reserve")
        Label_Text.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Text")
        Button_Save.Text = cLanguageManager.GetUserTextLine("GapFiller", "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetUserTextLine("GapFiller", "Button_Cancel")
        RadioButton_Y.Font = cTextFont
        RadioButton_N.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_Text.Font = cTextFont
        TextBox_Text.Font = cTextFont
        Label_Reserve.Font = cTextFont
        TextBox_ID.Font = cTextFont
        Label_Model.Font = cTextFont
        ComboBox_Model.Font = cTextFont
        AddHandler ComboBox_Model.SelectedIndexChanged, AddressOf ComboBox_Model_SelectedIndexChanged
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Height = (TextBox_ID.Height + 6) * 5 + 88
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = TextBox_ID.Height + 6
            Next
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

    Private Sub ComboBox_Model_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        iIndex = ComboBox_Model.SelectedIndex
        TextBox_Text.Text = ComboBox_Model.Text
    End Sub
End Class
