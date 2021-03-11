Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class IOGroupParameter
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager
    Private cTextFont As Font
    Private iCurrentIndex As Integer = 0
    Private iMaxIndex As Integer = 0

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
        Label_ID.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_ID")
        Label_Position.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_Position")
        Label_Model.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_Model")
        Button_Save.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Button_Save")
        Button_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Button_Cancel")
        TextBox_NameA.Font = cTextFont
        TextBox_NameA2.Font = cTextFont
        Button_Save.Font = cTextFont
        Button_Cancel.Font = cTextFont
        Label_ID.Font = cTextFont
        Label_NameA.Font = cTextFont
        Label_NameA2.Font = cTextFont
        TextBox_ID.Font = cTextFont
        ComboBox_Model.Font = cTextFont
        Label_Position.Font = cTextFont
        ComboBoxEx_Position.Font = cTextFont

        If cLanguageManager.SecondLanguageEnable Then
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_NameA", cLanguageManager.FirtLanguage)
            Label_NameA2.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_NameA2", cLanguageManager.SecondLanguage)
        Else
            Label_NameA2.Hide()
            TextBox_NameA2.Hide()
            TableLayoutPanel_Body.RowStyles(2).Height = 0
            Label_NameA.Text = cLanguageManager.GetTextLine(enumUIName.IOGroupParameter.ToString, "Label_NameA3")
        End If
    
        ComboBox_Model.Items.Clear()
        For Each element In [Enum].GetValues(GetType(enumIOType))
            ComboBox_Model.Items.Add(element.ToString)
        Next

        ComboBoxEx_Position.Items.Clear()
        ComboBoxEx_Position.Items.Add("")
        For i = 1 To iMaxIndex
            If i = iCurrentIndex Then Continue For
            ComboBoxEx_Position.Items.Add(i.ToString)
        Next

        AddHandler ComboBox_Model.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler Button_Save.Click, AddressOf Button_Save_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Cancel_Click

        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If cLanguageManager.SecondLanguageEnable Then
                Me.Height = (TextBox_ID.Height + 6) * 5 + 150
            Else
                Me.Height = (TextBox_ID.Height + 6) * 4 + 150
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

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "ComboBox_Model"
                Dim strTempValue As String = ComboBox_Model.Text
                TextBox_NameA.Text = strTempValue
                TextBox_NameA2.Text = strTempValue
        End Select

    End Sub

End Class
