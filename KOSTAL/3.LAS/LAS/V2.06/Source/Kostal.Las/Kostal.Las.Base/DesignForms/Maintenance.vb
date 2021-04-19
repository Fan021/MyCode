Imports System.Drawing
Imports System.Windows.Forms
Imports Kostal.Las.Base

Public Class Maintenance
    Inherits System.Windows.Forms.Form
    Implements IMaintenance

    Dim uM As New clsMaintenance
    Dim Ds As New DataSet
    Private _Language As Language
    Private _FileHandler As New FileHandler
    Private _ShowMaintain As Boolean
    Private _ShowAlarming As Boolean
    Private _mPassword As PassWordForm
    Private _i As New Station
    Private AppSettings As New Settings
    Private _xmlHandler As New XmlHandler
    Public Const sName As String = "_Maintenance"
    Private iLastIndex As Integer = -99
    Private _showLabel As Label
    Private _showMaintenance As ShowMaintenance
    Private _msg As String = String.Empty
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property

    Public Property ShowMaintain() As Boolean
        Get
            Return _ShowMaintain
        End Get
        Set(ByVal value As Boolean)
            _ShowMaintain = value
        End Set
    End Property
    Public Property ShowAlarm() As Boolean Implements IMaintenance.ShowAlarm
        Get
            Return _ShowAlarming
        End Get
        Set(ByVal value As Boolean)
            _ShowAlarming = value
        End Set
    End Property

    Public Property ShowTips As Label Implements IMaintenance.ShowTips
        Get
            Return _showLabel
        End Get
        Set(value As Label)
            _showLabel = value
        End Set
    End Property

    Public Sub Run()
        If AppSettings.LineType <> enumLineType.MultiLine Then
            If Not _ShowMaintain Then
                If Me.Visible Then Me.Visible = False
            Else
                If Not Me.Visible Then Me.Visible = True
            End If
        End If
        If _ShowAlarming Then
            _ShowAlarming = False
            If Not Me.Visible Then Me.Visible = False
            Me.Opacity = 0
            While True
                _showMaintenance = New ShowMaintenance
                _showMaintenance.Init(_i, AppSettings, _Language)
                _showMaintenance.TextBox_Msg.Text = _msg
                _showMaintenance.TextBox_Msg.Select(0, 0)
                If _showMaintenance.ShowDialog = DialogResult.OK Then
                    _mPassword.ChangeMode = False
                    _mPassword.ShowDialog()
                    If _mPassword.PassWordValid Then
                        If iLastIndex >= 0 Then DG_Maintain.Rows(iLastIndex).Cells("Count").Value = CStr(0)
                        If iLastIndex >= 0 Then DG_Maintain.Rows(iLastIndex).Cells("Count").Style.BackColor = Color.Green
                        iLastIndex = -99
                        _showLabel.Text = ""
                        _showLabel.SendToBack()
                        Me.Opacity = 1
                        Exit While
                    End If
                End If
            End While
        End If
    End Sub
    Private Sub Maintenance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
            _ShowMaintain = False
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        DG_Maintain.Rows.Add("", "", "0", "", "", "0")
        LanguageInit()
    End Sub

    Private Sub DG1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG_Maintain.CellContentClick
        On Error Resume Next
        If e.ColumnIndex = DG_Maintain.Columns("Reset").Index Then
            _mPassword.ChangeMode = False
            _mPassword.ShowDialog()
            If _mPassword.PassWordValid Then
                DG_Maintain.Rows(e.RowIndex).Cells("Count").Value = CStr(0)
                DG_Maintain.Rows(e.RowIndex).Cells("Count").Style.BackColor = Color.Green
                uM.ModifyData((e.RowIndex + 1).ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(0).Value.ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(1).Value.ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(2).Value.ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(3).Value.ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(4).Value.ToString,
                              DG_Maintain.Rows(e.RowIndex).Cells(5).Value.ToString)
                uM.SaveData()
            End If

        End If
    End Sub

    Public Function Inc_Count() As String Implements IMaintenance.Inc_Count
        'Refresh column color
        For i = 0 To DG_Maintain.Rows.Count - 1
            DG_Maintain.Rows(i).Cells("Count").Value = CStr(CLng(DG_Maintain.Rows(i).Cells("Count").Value) + 1)
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Red
            End If
            If (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value)) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Green
            End If
        Next
        'return show message
        For i = 0 To DG_Maintain.Rows.Count - 1
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                iLastIndex = i
                If _Language.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                    _msg = _Language.Read("ShowMaintenance", "Station") + ":" + DG_Maintain.Rows(i).Cells("Station").Value.ToString + vbCrLf +
                           _Language.Read("ShowMaintenance", "Component") + ":" + DG_Maintain.Rows(i).Cells("Component").Value.ToString + vbCrLf +
                           _Language.Read("ShowMaintenance", "Text") + ":" + DG_Maintain.Rows(i).Cells("AlarmMessage_English").Value.ToString
                    Return _msg
                Else
                    _msg = _Language.Read("ShowMaintenance", "Station") + ":" + DG_Maintain.Rows(i).Cells("Station").Value.ToString + vbCrLf +
                           _Language.Read("ShowMaintenance", "Component") + ":" + DG_Maintain.Rows(i).Cells("Component").Value.ToString + vbCrLf +
                           _Language.Read("ShowMaintenance", "Text") + ":" + DG_Maintain.Rows(i).Cells("AlarmMessage_Chinese").Value.ToString
                    Return _msg
                End If
            End If
        Next
        Return ""
    End Function

    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        If IsNothing(DG_Maintain.CurrentRow) Then Return
        If DG_Maintain.CurrentRow.Index < 0 Then Return

        _mPassword.ChangeMode = False
            _mPassword.ShowDialog()
        If _mPassword.PassWordValid Then
            If DG_Maintain.Rows.Count > 0 Then
                DG_Maintain.Rows.RemoveAt(DG_Maintain.CurrentRow.Index)
            End If
            If DG_Maintain.Rows.Count = 0 Then
                uM.DeleteData()
                uM.SaveData()
            Else
                uM.DeleteData()
                For i = 0 To DG_Maintain.Rows.Count - 1
                    uM.ModifyData((i + 1).ToString,
                                      DG_Maintain.Rows(i).Cells(0).Value.ToString,
                                      DG_Maintain.Rows(i).Cells(1).Value.ToString,
                                      DG_Maintain.Rows(i).Cells(2).Value.ToString,
                                      DG_Maintain.Rows(i).Cells(3).Value.ToString,
                                      DG_Maintain.Rows(i).Cells(4).Value.ToString,
                                      DG_Maintain.Rows(i).Cells(5).Value.ToString)
                Next
                uM.SaveData()
            End If
        End If
    End Sub

    Public Function Init(ByVal MyParent As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        Dim sResult As String

        AppSettings = _AppSettings
        _Language = MyLanguage

        _i.Name = "Maintenance"
        _i.IdString = MyParent.IdString + "_" + _i.Name

        _mPassword = New PassWordForm
        _mPassword.Init(_i, AppSettings, "UserPassWord")

        Me.Top = CInt((CDbl(My.Computer.Screen.WorkingArea.Height) / CDbl(2)) - (CDbl(Me.Height) / CDbl(2)))
        Me.Left = CInt((CDbl(My.Computer.Screen.WorkingArea.Width) / CDbl(2)) - (CDbl(Me.Width) / CDbl(2)))


        sResult = _xmlHandler.GetSectionInformation(AppSettings.ApplicationFolder, AppSettings.RootIniName, "Environment", "Screen")
        If IsNumeric(sResult) Then
            Me.Left = Me.Left + CInt(sResult) * My.Computer.Screen.WorkingArea.Width
        Else
            Me.Left = 0
        End If

        uM.Init(_i, AppSettings, MyLanguage)
        'uM.TruncateTable("las.maintenance")
        'uM.InSertData("st1", "screw", "5", "7", "请换1", "请换2", "8", "2017年")
        uM.SelectToDataView(Ds)
        If Ds.Tables(0).Rows.Count <> 0 Then
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                DG_Maintain.Rows.Add(Ds.Tables(0).Rows(i).ItemArray)
            Next
        End If

        For i = 0 To DG_Maintain.Rows.Count - 1
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) > CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Red
            End If
            If (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value)) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Green
            End If
        Next

        LanguageInit()
        AddHandler DG_Maintain.CellValueChanged, AddressOf DG1_CellValueChanged
        Return True
    End Function

    Private Sub DG1_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        uM.ModifyData((e.RowIndex + 1).ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(0).Value.ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(1).Value.ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(2).Value.ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(3).Value.ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(4).Value.ToString,
              DG_Maintain.Rows(e.RowIndex).Cells(5).Value.ToString)
        uM.SaveData()

    End Sub

    Public Sub LanguageInit()
        Dim l As Integer
        '   _Language.ReadControlText(Me)
        DG_Maintain.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
        DG_Maintain.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
        DG_Maintain.RowsDefaultCellStyle.Font = New Font("Calibri", 10)

        DG_Maintain.Columns(0).HeaderCell.Value = _Language.Read("ShowMaintenance", "Station")
        DG_Maintain.Columns(1).HeaderCell.Value = _Language.Read("ShowMaintenance", "Component")
        DG_Maintain.Columns(2).HeaderCell.Value = _Language.Read("ShowMaintenance", "AlarmUpLimit")
        DG_Maintain.Columns(3).HeaderCell.Value = _Language.Read("ShowMaintenance", "MessageChinese")
        DG_Maintain.Columns(4).HeaderCell.Value = _Language.Read("ShowMaintenance", "MessageEnglish")
        DG_Maintain.Columns(5).HeaderCell.Value = _Language.Read("ShowMaintenance", "Count")
        DG_Maintain.Columns(6).HeaderCell.Value = _Language.Read("ShowMaintenance", "Reset")


        For l = 0 To DG_Maintain.Rows.Count - 1
            DG_Maintain.Rows(l).Cells(6).Value = DG_Maintain.Columns(6).HeaderText
        Next
    End Sub


End Class