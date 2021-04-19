Imports System.Drawing
Imports System.Windows.Forms
Imports Kostal.Las.Base
Public Class ScheduleSelectView

    Implements IScheduleUI
    Private cLanguageManager As Language

    Public Event ScheduleChangeTo(ByVal IndicatedName As String, ByVal IgnorePassword As Boolean) Implements IScheduleUI.ScheduleChangeTo

    Public Event ComboxScheduleChangeTo(ByVal ID As String) Implements IScheduleUI.ComboxScheduleChangeTo

    Public Event AbortScheduleChange() Implements IScheduleUI.AbortScheduleChange


    Public Event ScheduleChanging(sender As Object, e As LasViewEventArgs)

    Protected Sub Schedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Public ReadOnly Property ScheduleList As ComboBox Implements IScheduleUI.ScheduleList
        Get
            Return cmbSchedules_01
        End Get
    End Property

    Public ReadOnly Property ScheduleData As DataGridView Implements IScheduleUI.ScheduleData
        Get
            Return DG_Schedule
        End Get
    End Property

    Public ReadOnly Property OKButton As Button Implements IScheduleUI.OKButton
        Get
            Return btnOK
        End Get
    End Property

    Public ReadOnly Property ResetButton As Button Implements IScheduleUI.ResetButton
        Get
            Return btnAbort
        End Get
    End Property

    Public ReadOnly Property ScheduleTitle As Label Implements IScheduleUI.ScheduleTitle
        Get
            Return lblCurrentSchedule
        End Get
    End Property
    Public ReadOnly Property ScheduleName As Label Implements IScheduleUI.ScheduleName
        Get
            Return lblCurrentScheduleName
        End Get
    End Property

    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IStationPanel.Msg
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IStationPanel.StepID
        Get
            Return Nothing
        End Get
    End Property

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            cLanguageManager = CType(Devices(Language.Name), Language)
            InitLanugage()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Sub InitLanugage()
        lblMessage.Text = cLanguageManager.Read("ScheduleSelectView", "lblMessage")
        btnOK.Text = cLanguageManager.Read("ScheduleSelectView", "btnOK")
        btnCancel.Text = cLanguageManager.Read("ScheduleSelectView", "btnCancel")
        btnAbort.Text = cLanguageManager.Read("ScheduleSelectView", "btnAbort")
        lblCurrentSchedule.Text = cLanguageManager.Read("ScheduleSelectView", "lblCurrentSchedule")
    End Sub

    Protected Sub cmbSchedules_01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSchedules_01.TextChanged

        If cmbSchedules_01.Text <> "" Then RaiseEvent ComboxScheduleChangeTo(cmbSchedules_01.Text)

    End Sub

    Protected Sub btnScheduleSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        btnOK.Enabled = False

        RaiseEvent ScheduleChangeTo(cmbSchedules_01.Text, False)


        RaiseEvent ScheduleChanging(Me, New LasViewEventArgs With {.IsMakeSure = True})

    End Sub

    Public Sub ShortToRaiseClearMode()

        For Each item As String In ScheduleList.Items

            If item.Contains(LAS_ScheduleMode.ClearMode.ToString) Then

                RaiseEvent ScheduleChangeTo(item, True)

            End If

        Next

    End Sub

    Public Sub btnAlternateScheduleAbort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbort.Click

        btnAbort.Enabled = False

        RaiseEvent AbortScheduleChange()

    End Sub

    Public ReadOnly Property Pannel As Panel Implements IScheduleUI.Panel
        Get
            Return New Panel
        End Get
    End Property

    Public ReadOnly Property GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Private Sub ScheduleSelectView_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        '  Me.cmbSchedules_01.Width = panSelect.Width - 2.1 * btnCancel.Width

    End Sub
    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSchedules_01.SizeChanged
        Try
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = cmbSchedules_01.Height + 6
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(2).Height = cmbSchedules_01.Height + 6
            TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(3).Height = cmbSchedules_01.Height + 6
            TableLayoutPanel1.RowStyles(5).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(5).Height = cmbSchedules_01.Height + 6
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        RaiseEvent ScheduleChanging(Me, New LasViewEventArgs With {.IsMakeSure = False})

    End Sub


    Private Sub lblCurrentSchedule_Resize(sender As Object, e As EventArgs) Handles lblCurrentSchedule.Resize
        Dim g As Graphics = lblCurrentSchedule.CreateGraphics()
        Dim length As SizeF = g.MeasureString(lblCurrentSchedule.Text, lblCurrentSchedule.Font)
        TableLayoutPanel4.ColumnStyles(0).Width = length.Width * 1.2

    End Sub
End Class