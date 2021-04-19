Imports System.Windows.Forms

Public Class SystemLog
    Implements IViewDefine

    Public ReadOnly Property SystemMainLogger As ListBox
        Get
            Return Me.MainLogger
        End Get
    End Property


    Public ReadOnly Property GetPannel As Panel Implements IViewDefine.GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property


End Class