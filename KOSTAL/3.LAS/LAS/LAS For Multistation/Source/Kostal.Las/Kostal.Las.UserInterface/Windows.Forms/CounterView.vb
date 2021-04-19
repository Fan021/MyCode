Imports Kostal.Library
Imports System.Drawing
Imports Kostal.Las.Base
Imports System.ComponentModel


''' <summary>
''' Gui for the counters Success, Failed, Percent and PPM
''' </summary>
''' <remarks></remarks>
Public Class CounterView

    Private ReadOnly _model As ICounterController
    Private ReadOnly _counterAppearance As CounterAppearance
    Private ReadOnly _defaultColor As Color

    '''' <summary>
    '''' new inctance of Easy Gui
    '''' </summary>
    '''' <remarks></remarks>
    'Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Public Sub New(ByVal CountController As CounterController)
        'ByVal runtime As IRuntimeManager, ByVal counterAppearance As CounterAppearance
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

        _defaultColor = CorporateIdentity.Colors.White

        Me.BackColor = CorporateIdentity.Colors.White
        Me.easyPanel.BackColor = CorporateIdentity.Colors.White
        Me.CounterTable.BackColor = CorporateIdentity.Colors.White

        Me.lblIO.BackColor = Me._defaultColor
        Me.lblNIO.BackColor = Me._defaultColor
        Me.lblPercent.BackColor = Me._defaultColor
        Me.lblPercent.FlatAppearance.BorderColor = CorporateIdentity.Colors.GrayDark
        Me.lblPpm.BackColor = Me._defaultColor
        Me.lblPpm.FlatAppearance.BorderColor = CorporateIdentity.Colors.GrayDark
        Me.lblCycleTime.BackColor = Me._defaultColor
        Me.lblCycleTime.FlatAppearance.BorderColor = CorporateIdentity.Colors.GrayDark

        Me.lblIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
        Me.lblNIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
        Me.lblPercent.FlatAppearance.MouseDownBackColor = Me._defaultColor
        Me.lblPpm.FlatAppearance.MouseDownBackColor = Me._defaultColor

        Me.lblIO.FlatAppearance.MouseOverBackColor = Me._defaultColor
        Me.lblNIO.FlatAppearance.MouseOverBackColor = Me._defaultColor
        Me.lblPercent.FlatAppearance.MouseOverBackColor = Me._defaultColor
        Me.lblPpm.FlatAppearance.MouseOverBackColor = Me._defaultColor

        _model = CountController
        _counterAppearance = CountController.CounterAppearance

        AddHandler _model.Fail.PropertyChanged, AddressOf PropertyChanged
        AddHandler _model.Success.PropertyChanged, AddressOf PropertyChanged
        AddHandler _model.PropertyChanged, AddressOf PropertyChanged
        AddHandler _counterAppearance.PropertyChanged, AddressOf PropertyChanged

    End Sub

    Public Sub UpdateCycleTime(ByVal CycleTime As Double)

        Dim TimeString As String = String.Format("{0:F1} mS", CycleTime)

        If TimeString <> lblCycleTime.Text Then lblCycleTime.Text = TimeString

    End Sub

    '''' <summary>
    '''' GetPanel
    '''' </summary>
    '''' <returns>returns Panel as Gui</returns>
    '''' <remarks></remarks>
    'Public Function GetPanel() As System.Windows.Forms.Panel Implements IShow.GetPanel
    ' Return Me.easyPanel
    'End Function

    Private Sub Renew()

        If Me.easyPanel.InvokeRequired Then
            Me.easyPanel.BeginInvoke(Sub() Renew())
            Return
        End If

        lblIO.Text = _model.Success.Value.ToString
        lblNIO.Text = _model.Fail.Value.ToString

        lblPercent.Visible = _counterAppearance.StatisticVisible
        lblPpm.Visible = _counterAppearance.StatisticVisible

        lblPercent.Text = String.Format("{0:P2}", _model.Ppm)           'Format(_model.Ppm * 100, "#0.00").ToString + " %"
        lblPpm.Text = String.Format("{0:F0} ppm", _model.Ppm * 1000000)  'Format(_model.Ppm * 1000000, "#0").ToString + " ppm"

        Me.lblCycleTime.BackColor = Me._defaultColor
        Me.lblCycleTime.FlatAppearance.MouseDownBackColor = Me._defaultColor
        Me.lblCycleTime.FlatAppearance.MouseOverBackColor = Me._defaultColor

        Select Case _model.State
            Case CounterState.Waiting
                Me.lblPercent.BackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblPpm.BackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblIO.FlatAppearance.MouseOverBackColor = Me._defaultColor
                Me.lblIO.BackColor = Me._defaultColor

                Me.lblNIO.BackColor = Me._defaultColor
                Me.lblNIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblNIO.FlatAppearance.MouseOverBackColor = Me._defaultColor

            Case CounterState.Failed

                Me.lblPercent.BackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblPpm.BackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblIO.FlatAppearance.MouseOverBackColor = Me._defaultColor
                Me.lblIO.BackColor = Me._defaultColor

                Me.lblNIO.BackColor = Color.Crimson
                Me.lblNIO.FlatAppearance.MouseDownBackColor = Color.Crimson
                Me.lblNIO.FlatAppearance.MouseOverBackColor = Color.Crimson

            Case CounterState.Successfully

                Me.lblPercent.BackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPercent.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblPpm.BackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblPpm.FlatAppearance.MouseOverBackColor = Me._defaultColor

                Me.lblIO.FlatAppearance.MouseDownBackColor = Color.MediumSeaGreen
                Me.lblIO.FlatAppearance.MouseOverBackColor = Color.MediumSeaGreen
                Me.lblIO.BackColor = Color.MediumSeaGreen

                Me.lblNIO.BackColor = Me._defaultColor
                Me.lblNIO.FlatAppearance.MouseDownBackColor = Me._defaultColor
                Me.lblNIO.FlatAppearance.MouseOverBackColor = Me._defaultColor

        End Select

        SetResultIndication()
        ' SetStatisticVisibility()
        Me.easyPanel.Refresh()
        'System.Windows.Forms.Application.DoEvents()

    End Sub

    Public Function GetPannel() As System.Windows.Forms.Control
        Return Me.easyPanel
    End Function

    Private Sub PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Renew()
    End Sub

    Private Sub SetResultIndication()

        If _counterAppearance.ResultIndicationOnly = True Then
            lblIO.ForeColor = lblIO.BackColor
            lblNIO.ForeColor = lblNIO.BackColor
            lblPercent.ForeColor = lblPercent.BackColor
            lblPpm.ForeColor = lblPpm.BackColor
        Else
            lblIO.ForeColor = Color.Black
            lblNIO.ForeColor = Color.Black
            lblPercent.ForeColor = Color.Black
            lblPpm.ForeColor = Color.Black
        End If

    End Sub
    Private Sub SetStatisticVisibility()

        Dim cellwith As Single = CType(Me.CounterTable.Width / 5, Single)

        lblPercent.Visible = _counterAppearance.StatisticVisible
        lblPpm.Visible = _counterAppearance.StatisticVisible

        If _counterAppearance.StatisticVisible = True Then
            Me.CounterTable.ColumnStyles(0).Width = 0
            Me.CounterTable.ColumnStyles(1).Width = 0
            Me.CounterTable.ColumnStyles(2).Width = cellwith
            Me.CounterTable.ColumnStyles(3).Width = cellwith
            Me.CounterTable.ColumnStyles(4).Width = cellwith
            Me.CounterTable.ColumnStyles(5).Width = cellwith
            Me.CounterTable.ColumnStyles(6).Width = cellwith
        Else
            Me.CounterTable.ColumnStyles(0).Width = cellwith / 2
            Me.CounterTable.ColumnStyles(1).Width = cellwith / 2
            Me.CounterTable.ColumnStyles(2).Width = cellwith
            Me.CounterTable.ColumnStyles(3).Width = cellwith
            Me.CounterTable.ColumnStyles(4).Width = cellwith / 2
            Me.CounterTable.ColumnStyles(5).Width = cellwith / 2
            Me.CounterTable.ColumnStyles(6).Width = cellwith
        End If

    End Sub

    'Private Sub Counterview_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
    '  SetResultIndication()
    '  SetStatisticVisibility()
    'End Sub

End Class

