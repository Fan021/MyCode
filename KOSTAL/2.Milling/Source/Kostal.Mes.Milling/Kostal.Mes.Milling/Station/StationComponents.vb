Imports System.Windows.Forms

Public Class StationCfg
    Public Station As String = String.Empty
    Public Name As String = String.Empty
    Public ReadIO As Integer = 0
    Public WriteIO As Integer = 0
    Public BarLength As Integer = 0
    Public SMTBarLength As Integer = 0
    Public DeviceConfig As New InterfaceConfig
    Public DelayTime As Integer = 0
End Class



Public Interface IStationTypeBase
    ReadOnly Property IsMasterError As Boolean
    ReadOnly Property StationCfg As StationCfg
    Function Init() As Boolean
    Function ReLoadLanguage() As Boolean
    Sub Run()
    Sub Dispose()

End Interface
Public MustInherit Class StationTypeBase
    Implements IStationTypeBase

    Protected IsDisposed As Boolean

    Protected _i As Station
    Protected _Logger As Logger
    Protected _Messager As New Messager
    Protected _Settings As New Settings
    Protected _StationCfg As StationCfg
    Protected _FileHandler As New FileHandler
    Protected _FirstPulse As Boolean
    Protected _FirstFlag As Boolean
    Protected _ManualOffPulse As Boolean
    Protected _ManualFlag As Boolean
    Protected _ManualMode As Boolean
    Protected _ManualRefresh As Boolean
    Protected _InternPass As Boolean
    Protected _InternFail As Boolean
    Protected _InternMsg As String
    Protected _isRun As Boolean = False
    Protected _Current As String = ""
    Protected _Last As String = ""
    Protected _StepMsg As String = ""
    Protected _object As Object = ""
    Public isHome As Boolean = False
    Protected _UI As UI
    Protected _ListLcSn As New List(Of LineControlElement)

#Region "Properties"

    Public Property isRun As Boolean

        Set(ByVal value As Boolean)
            SyncLock _object
                _isRun = value
            End SyncLock
        End Set
        Get
            SyncLock _object
                Return _isRun
            End SyncLock
        End Get

    End Property

    Public ReadOnly Property IsMasterError As Boolean Implements IStationTypeBase.IsMasterError
        Get
            Return _i.IsMasterError
        End Get
    End Property

    Public ReadOnly Property StationCfg As StationCfg Implements IStationTypeBase.StationCfg
        Get
            Return _StationCfg
        End Get
    End Property

#End Region


    Public Sub New(ByVal StationCfg As StationCfg, ByVal Settings As Settings, ByVal UI As UI)
        _Settings = Settings
        _StationCfg = StationCfg
        _i = New Station(_StationCfg.Name)
        _Logger = New Logger(_Settings)
        _UI = UI
    End Sub

    Protected Sub SetFailText(ByVal strText As String)
        _UI.TextBox_ErrorMsg.Text = strText
    End Sub

    Protected Sub SetSNText(ByVal strText As String)
        _UI.TextBox_SN.Text = strText
    End Sub

    Protected Sub SetScapText(ByVal strText As String)
        _UI.TextBox_Scap.Text = strText
    End Sub

    Protected Sub SetStepText(ByVal strText As String)
        _UI.Label_Msg.Text = strText
        _UI.Label_Msg.ForeColor = Drawing.Color.Blue
    End Sub

    Public MustOverride Sub Run() Implements IStationTypeBase.Run
    Public MustOverride Function Init() As Boolean Implements IStationTypeBase.Init
    Public MustOverride Function ReLoadLanguage() As Boolean Implements IStationTypeBase.ReLoadLanguage
    Public MustOverride Sub Dispose() Implements IStationTypeBase.Dispose

End Class
