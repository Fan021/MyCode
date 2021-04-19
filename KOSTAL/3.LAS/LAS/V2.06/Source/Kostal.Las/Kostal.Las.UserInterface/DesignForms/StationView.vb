Imports Kostal.Las.Base
Imports Kostal.Las.UserInterface

Public Class StationView
    Implements IUserInterface

    Public Const sName As String = "StationView"
    Public Const TabControlStationsName As String = "Stations"
    Public Const TabControlSystemName As String = "System"
    Public Const TabControlDebugName As String = "TabControlDebugName"
    Private _lstTabControlStations As New List(Of TabControl)
    Private _lstTabControlSystem As New List(Of TabControl)
    Private cLanguageManager As Language
    Public Event UserCancelled(sender As Object, e As LasViewEventArgs) Implements IUserInterface.UserCancelled


    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        ' Add any initialization after the InitializeComponent() call.

        cLanguageManager = CType(Devices(Language.Name), Language)
        HmiButton_Cancal.Button.Text = cLanguageManager.Read("StationView", "HmiButton_Cancal")
        HmiButton_Cancal.Dock = DockStyle.None

        HmiButton_Cancal.Height = lblMessage.Height * 2
        HmiButton_Cancal.Dock = DockStyle.Fill
        lblMessage.ForeColor = KostalLasColors.KOSTALBLUE
        HmiButton_Cancal.Button.Font = New System.Drawing.Font("Calibri", 16, FontStyle.Bold)
        Return True
    End Function


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.TabControlStations.Visible = False
        Me.TabControlStations.Dock = DockStyle.Fill
        Me.TabControlSystem.Visible = False
        Me.TabControlSystem.Dock = DockStyle.Fill
        AddHandler HmiButton_Cancal.Button.Click, AddressOf btnCancel_Click
    End Sub

    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Private Sub StationView_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        TabControlStations_Click(Me, Nothing)

    End Sub

    Public Sub GetTabControls(ByVal f As Control, ByVal list As List(Of TabControl))
        If f Is Nothing Then Return

        For Each cc In f.Controls
            If TypeOf cc Is TabControl Then
                list.Add(cc)
            End If

            GetTabControls(cc, list)
        Next

    End Sub

    Private Sub TabControlStations_Click(sender As Object, e As EventArgs) Handles TabControlStations.Click


        Dim sCaption As String

        Dim tabControl As TabControl = Nothing

        For Each value As TabControl In _lstTabControlStations
            If value.Name = TabControlStations.SelectedTab.Text Then

                tabControl = value
            End If
        Next

        If tabControl IsNot Nothing Then

            If tabControl.SelectedTab Is Nothing Then Return
            sCaption = " - " + tabControl.SelectedTab.Text
        Else
            sCaption = ""
        End If

        lblMessage.Text = TabControlStations.SelectedTab.Text + sCaption
        TabControlStations.SelectedTab.BackColor = KostalLasColors.LIGHTBLUE

        'TabControlStations.SelectedTab.

    End Sub


    Public Sub ChildTabControl_Click(sender As Object, e As EventArgs)

        TabControlStations_Click(sender, e)

    End Sub

    Private Sub TabControlStations_ControlAdded(sender As Object, e As ControlEventArgs) Handles TabControlStations.ControlAdded


        For Each item As Control In TabControlStations.Controls
            GetTabControls(item, _lstTabControlStations)
        Next

        TabControlStations_Click(Me, Nothing)

    End Sub

    Private Sub TabControlStations_ControlRemoved(sender As Object, e As ControlEventArgs) Handles TabControlStations.ControlRemoved

        For Each item As Control In TabControlStations.Controls
            GetTabControls(item, _lstTabControlStations)
        Next

        TabControlStations_Click(Me, Nothing)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)

        RaiseEvent UserCancelled(Me, New LasViewEventArgs With {.Name = sName})

    End Sub

    Private Sub TabControlSystem_Click(sender As Object, e As EventArgs) Handles TabControlSystem.Click

        Dim sCaption As String

        Dim tabControl As TabControl = Nothing

        For Each value As TabControl In _lstTabControlSystem
            If value.Name = TabControlSystem.SelectedTab.Text Then

                tabControl = value
            End If
        Next

        If tabControl IsNot Nothing Then

            If tabControl.SelectedTab Is Nothing Then Return
            sCaption = " - " + tabControl.SelectedTab.Text
        Else
            sCaption = ""
        End If

        lblMessage.Text = TabControlSystem.SelectedTab.Text + sCaption
        TabControlSystem.SelectedTab.BackColor = KostalLasColors.LIGHTBLUE

        'TabControlStations.SelectedTab.
    End Sub

    Private Sub TabControlSystem_ControlAdded(sender As Object, e As ControlEventArgs) Handles TabControlSystem.ControlAdded
        For Each item As Control In TabControlSystem.Controls
            GetTabControls(item, _lstTabControlSystem)
        Next

        TabControlSystem_Click(Me, Nothing)
    End Sub

    Private Sub TabControlSystem_ControlRemoved(sender As Object, e As ControlEventArgs) Handles TabControlSystem.ControlRemoved
        For Each item As Control In TabControlSystem.Controls
            GetTabControls(item, _lstTabControlSystem)
        Next

        TabControlSystem_Click(Me, Nothing)
    End Sub
    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMessage.SizeChanged
        Try
            TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(0).Height = lblMessage.Font.Size + 12
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class