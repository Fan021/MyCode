Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class CylinderGroupParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Private cTextFont As Font
    Private iCurrentIndex As Integer = 0
    Private iMaxIndex As Integer = 0
    Private bDisableMove As Boolean = True


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
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Label_ID.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Label_ID")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Button_Cancel")
        Label_Position.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_Position")
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont
        TextBox_ID.Font = cTextFont
        ComboBoxEx_Position.Font = cTextFont
        If cLanguageManager.SecondLanguageEnable Then
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Label_NameA", cLanguageManager.FirtLanguage)
            Label_NameA2.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Label_NameA2", cLanguageManager.SecondLanguage)
        Else
            Label_NameA2.Hide()
            TextBox_NameA2.Hide()
            TableLayoutPanel_Body.RowStyles(2).Height = 0
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.CylinderGroupParameter.ToString, "Label_NameA3")
        End If

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
            If cLanguageManager.SecondLanguageEnable Then
                Me.Height = (TextBox_ID.Height + 6) * 4 + 150
            Else
                Me.Height = (TextBox_ID.Height + 6) * 3 + 150
            End If
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
                If iCnt = 2 And Not cLanguageManager.SecondLanguageEnable Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                Else
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = TextBox_ID.Height + 6
                End If
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
