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
    Private _Settings As New Settings
    Private _xmlHandler As New XmlHandler
    Public Const sName As String = "_Maintenance"


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
    Public Property ShowAlarm() As Boolean
        Get
            Return _ShowAlarming
        End Get
        Set(ByVal value As Boolean)
            _ShowAlarming = value
        End Set
    End Property

    Private Property IMaintenance_ShowAlarm As Boolean Implements IMaintenance.ShowAlarm
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub Run()
        If _Settings.LineType <> enumLineType.MultiLine Then
            If Not _ShowMaintain Then
                If Me.Visible Then Me.Visible = False
            Else
                If Not Me.Visible Then Me.Visible = True
            End If
        End If
        If _ShowAlarming Then
            _ShowAlarming = False
            If Not Me.Visible Then Me.Visible = True
            Me.Opacity = 0
            While True
                If MsgBox("Please DO Maintenance!!!", MsgBoxStyle.OkCancel, "Confirm Maintenance") = MsgBoxResult.Ok Then
                    _mPassword.ChangeMode = False
                    _mPassword.ShowDialog()
                    If _mPassword.PassWordValid Then
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

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs)
        DG_Maintain.Rows.Add("", "", "0", "0", "", "", "0")
        LanguageInit()
    End Sub

    Private Sub DG1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        On Error Resume Next
        If e.ColumnIndex = DG_Maintain.Columns("Reset").Index Then
            _mPassword.ChangeMode = False
            _mPassword.ShowDialog()
            If _mPassword.PassWordValid Then
                DG_Maintain.Rows(e.RowIndex).Cells("Count").Value = CStr(0)
                DG_Maintain.Rows(e.RowIndex).Cells("Count").Style.BackColor = Color.Green
            End If

        End If
    End Sub

    Public Function Inc_Count() As String
        'Refresh column color
        For i = 0 To DG_Maintain.Rows.Count - 1
            DG_Maintain.Rows(i).Cells("Count").Value = CStr(CLng(DG_Maintain.Rows(i).Cells("Count").Value) + 1)
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("HintUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Yellow
            End If
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Red
            End If
            If (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("HintUpLimit").Value)) And (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value)) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Green
            End If
        Next
        'return show message
        For i = 0 To DG_Maintain.Rows.Count - 1
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                Return DG_Maintain.Rows(i).Cells("Station").Value.ToString + ":" + DG_Maintain.Rows(i).Cells("AlarmMessage").Value.ToString
            End If
        Next
        For i = 0 To DG_Maintain.Rows.Count - 1
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) >= CLng(DG_Maintain.Rows(i).Cells("HintUpLimit").Value) Then
                Return DG_Maintain.Rows(i).Cells("Station").Value.ToString + ":" + DG_Maintain.Rows(i).Cells("HintMessage").Value.ToString
            End If
        Next
        Return ""
    End Function

    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs)
        If DG_Maintain.Rows.Count > 0 Then
            DG_Maintain.Rows.RemoveAt(DG_Maintain.CurrentRow.Index)
        End If
        If DG_Maintain.Rows.Count = 0 Then
            uM.TruncateTable("las.maintenance")
        Else
            uM.TruncateTable("las.maintenance")
            For i = 0 To DG_Maintain.Rows.Count - 1
                uM.InSertData(DG_Maintain.Rows(i).Cells(0).Value.ToString, DG_Maintain.Rows(i).Cells(1).Value.ToString, DG_Maintain.Rows(i).Cells(2).Value.ToString, DG_Maintain.Rows(i).Cells(3).Value.ToString, DG_Maintain.Rows(i).Cells(4).Value.ToString, DG_Maintain.Rows(i).Cells(5).Value.ToString, DG_Maintain.Rows(i).Cells(6).Value.ToString, Format(DateTime.Now, "yyyy/MM/dd hh:mm:ss"))
            Next
        End If
    End Sub

    Public Function Init(ByVal MyParent As Station, ByVal MySettings As Settings, ByVal MyLanguage As Language) As Boolean
        Dim sResult As String

        _Settings = MySettings
        _Language = MyLanguage

        _i.Name = "ArticleCounter"
        _i.IdString = MyParent.IdString + "_" + _i.Name

        _mPassword = New PassWordForm
        _mPassword.Init(_i, _Settings, "UserPassWord")

        Me.Top = CInt((CDbl(My.Computer.Screen.WorkingArea.Height) / CDbl(2)) - (CDbl(Me.Height) / CDbl(2)))
        Me.Left = CInt((CDbl(My.Computer.Screen.WorkingArea.Width) / CDbl(2)) - (CDbl(Me.Width) / CDbl(2)))


        sResult = _xmlHandler.GetSectionInformation(_Settings.ApplicationFolder, _Settings.RootIniName, "Environment", "Screen")
        If IsNumeric(sResult) Then
            Me.Left = Me.Left + CInt(sResult) * My.Computer.Screen.WorkingArea.Width
        Else
            Me.Left = 0
        End If

        uM.Init()
        'uM.TruncateTable("las.maintenance")
        'uM.InSertData("st1", "screw", "5", "7", "请换1", "请换2", "8", "2017年")
        uM.SelectToDataView("SELECT * FROM las.maintenance", Ds)
        If Ds.Tables(0).Rows.Count <> 0 Then
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                DG_Maintain.Rows.Add("", "", "0", "0", "", "", "0")
                For j = 0 To Ds.Tables(0).Columns.Count - 3
                    DG_Maintain.Rows(i).Cells(j).Value = Ds.Tables(0).Rows(i).Item(j + 1).ToString
                Next
            Next
        End If

        For i = 0 To DG_Maintain.Rows.Count - 1
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) > CLng(DG_Maintain.Rows(i).Cells("HintUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Yellow
            End If
            If CLng(DG_Maintain.Rows(i).Cells("Count").Value) > CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Red
            End If
            If (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("HintUpLimit").Value)) And (CLng(DG_Maintain.Rows(i).Cells("Count").Value) < CLng(DG_Maintain.Rows(i).Cells("AlarmUpLimit").Value)) Then
                DG_Maintain.Rows(i).Cells("Count").Style.BackColor = Color.Green
            End If
        Next

        LanguageInit()

        Return True
    End Function

    Private Sub DG1_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        If DG_Maintain.Rows.Count = 0 Then
            uM.TruncateTable("las.maintenance")
        Else
            uM.TruncateTable("las.maintenance")
            For i = 0 To DG_Maintain.Rows.Count - 1
                uM.InSertData(DG_Maintain.Rows(i).Cells(0).Value.ToString, DG_Maintain.Rows(i).Cells(1).Value.ToString, DG_Maintain.Rows(i).Cells(2).Value.ToString, DG_Maintain.Rows(i).Cells(3).Value.ToString, DG_Maintain.Rows(i).Cells(4).Value.ToString, DG_Maintain.Rows(i).Cells(5).Value.ToString, DG_Maintain.Rows(i).Cells(6).Value.ToString, Format(DateTime.Now, "yyyy/MM/dd hh:mm:ss"))
            Next
        End If

    End Sub

    Public Sub LanguageInit()
        Dim l As Integer
        _Language.ReadControlText(Me)
        DG_Maintain.Columns(0).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(0).HeaderCell")
        DG_Maintain.Columns(1).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(1).HeaderCell")
        DG_Maintain.Columns(2).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(2).HeaderCell")
        DG_Maintain.Columns(3).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(3).HeaderCell")
        DG_Maintain.Columns(4).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(4).HeaderCell")
        DG_Maintain.Columns(5).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(5).HeaderCell")
        DG_Maintain.Columns(6).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(6).HeaderCell")
        DG_Maintain.Columns(7).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Maintain.Columns(7).HeaderCell")

        For l = 0 To DG_Maintain.Rows.Count - 1
            DG_Maintain.Rows(l).Cells(7).Value = DG_Maintain.Columns(7).HeaderText
        Next
    End Sub

    Private Function IMaintenance_Inc_Count() As String Implements IMaintenance.Inc_Count
        Return ""
    End Function
End Class