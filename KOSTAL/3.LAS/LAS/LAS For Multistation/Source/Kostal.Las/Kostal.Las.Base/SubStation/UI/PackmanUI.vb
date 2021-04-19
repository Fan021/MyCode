Imports System.Windows.Forms
Imports Kostal.Las.Base

Public Class PackmanUI
    Implements IPackmanUI

    Private _panel As Panel

    Sub New()

    End Sub

    Sub New(panel As Panel)
        _panel = panel
    End Sub

    Public ReadOnly Property Panel As Panel Implements IStationPanel.Panel
        Get
            Return _panel
        End Get
    End Property

    Public ReadOnly Property StepID As Label Implements IStationPanel.StepID
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Msg As Label Implements IStationPanel.Msg
        Get
            Return Nothing
        End Get
    End Property

End Class