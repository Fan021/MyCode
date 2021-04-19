Imports System.Windows.Forms

Public Class ScheduleUI
    Implements IScheduleUI

    Public Event ScheduleChangeTo(ByVal IndicatedName As String, ByVal IgnorePassword As Boolean) Implements IScheduleUI.ScheduleChangeTo

    Public Event ResetScheduleChange() Implements IScheduleUI.AbortScheduleChange

    Public Event ComboxScheduleChangeTo(ByVal ID As String) Implements IScheduleUI.ComboxScheduleChangeTo

    Protected Sub Schedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property Panel As Panel Implements IScheduleUI.Panel
        Get
            Return DockPanel
        End Get
    End Property


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
            Return btnScheduleSelected
        End Get
    End Property

    Public ReadOnly Property ResetButton As Button Implements IScheduleUI.ResetButton
        Get
            Return btnAlternateScheduleAbort
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

    Protected Sub cmbSchedules_01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSchedules_01.SelectedIndexChanged
        If cmbSchedules_01.Text <> "" Then RaiseEvent ComboxScheduleChangeTo(cmbSchedules_01.Text)
    End Sub

    Protected Sub btnScheduleSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScheduleSelected.Click
        btnScheduleSelected.Enabled = False
        RaiseEvent ScheduleChangeTo(cmbSchedules_01.Text, False)
        ' btnAlternateScheduleAbort.Enabled = True
    End Sub

    Protected Sub btnAlternateScheduleAbort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlternateScheduleAbort.Click
        '   btnAlternateScheduleAbort.Enabled = False
        RaiseEvent ResetScheduleChange()
    End Sub


End Class