Imports System.Windows.Forms
Imports Kostal.Las.Base

Public Class Debug
    Implements IUserInterface
    Private cIOForm As ChildrenIOForm
    Public Const sName As String = "Debug"
    Public Const TabControlStationsName As String = "Debug"
    Private _lstTabControlStations As New List(Of TabControl)
    Private _lstTabControlSystem As New List(Of TabControl)
    Public Event UserCancelled(sender As Object, e As LasViewEventArgs) Implements IUserInterface.UserCancelled
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        lblMessage.ForeColor = KostalLasColors.KOSTALBLUE
    End Sub

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.DesignPanel
        End Get
    End Property
    Public Sub GetTabControls(ByVal f As Control, ByVal list As List(Of TabControl))
        If f Is Nothing Then Return

        For Each cc In f.Controls
            If TypeOf cc Is TabControl Then
                list.Add(cc)
            End If

            GetTabControls(cc, list)
        Next
    End Sub

    Private Sub TabControlStations_Click(sender As Object, e As EventArgs) Handles TabControl_Debug.Click, TabControl_Debug.Click
        TabControl_Debug.SelectedTab.BackColor = KostalLasColors.LIGHTBLUE
        lblMessage.Text = TabControl_Debug.SelectedTab.Text
        'TabControlStations.SelectedTab.
    End Sub


    Public Sub ChildTabControl_Click(sender As Object, e As EventArgs)

        TabControlStations_Click(sender, e)

    End Sub





    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        RaiseEvent UserCancelled(Me, New LasViewEventArgs With {.Name = sName})

    End Sub
End Class