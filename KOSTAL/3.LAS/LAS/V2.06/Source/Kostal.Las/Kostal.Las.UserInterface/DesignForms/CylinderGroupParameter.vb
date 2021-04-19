
Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class CylinderGroupParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    'Private cLanguageManager As clsLanguageManager
    Private cTextFont As Font
    Private iCurrentIndex As Integer = 0
    Private iMaxIndex As Integer = 0
    Private bDisableMove As Boolean = True
    Private cLanguageManager As Language
    Private cTips As clsTips

    Public Property DisableMove As Boolean
        Set(ByVal value As Boolean)
            bDisableMove = value
        End Set
        Get
            Return bDisableMove
        End Get
    End Property


    Public Property CurrentIndex As Integer
        Set(ByVal value As Integer)
            iCurrentIndex = value
        End Set
        Get
            Return iCurrentIndex
        End Get
    End Property

    Public Property MaxIndex As Integer
        Set(ByVal value As Integer)
            iMaxIndex = value
        End Set
        Get
            Return iMaxIndex
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


    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        AddHandler TextBox_ID.SizeChanged, AddressOf TextBox_SizeChanged
        cLanguageManager = CType(cSystemElement(Language.Name), Language)
        cTips = CType(cSystemElement(clsTips.Name), clsTips)
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont
        TextBox_ID.Font = cTextFont
        ComboBoxEx_Position.Font = cTextFont
        Label_Position.Font = cTextFont

        Label_NameA.Text = cLanguageManager.Read("CylinderGroupParameter", "Label_NameA")
        Label_NameA2.Text = cLanguageManager.Read("CylinderGroupParameter", "Label_NameA2")
        Label_ID.Text = cLanguageManager.Read("CylinderGroupParameter", "Label_ID")
        Label_Position.Text = cLanguageManager.Read("CylinderGroupParameter", "Label_Position")
        Button_Save.Text = cLanguageManager.Read("CylinderGroupParameter", "Button_Save")
        Button_Cancel.Text = cLanguageManager.Read("CylinderGroupParameter", "Button_Cancel")

        ComboBoxEx_Position.Items.Clear()
        ComboBoxEx_Position.Items.Add("")
        For i = 1 To iMaxIndex
            If i = iCurrentIndex Then Continue For
            ComboBoxEx_Position.Items.Add(i.ToString)
        Next
        If bDisableMove Then ComboBoxEx_Position.Enabled = False
        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Me.Height = (TextBox_ID.Height + 6) * 4 + 150
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = TextBox_ID.Height + 6

                iCnt = iCnt + 1
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

End Class
