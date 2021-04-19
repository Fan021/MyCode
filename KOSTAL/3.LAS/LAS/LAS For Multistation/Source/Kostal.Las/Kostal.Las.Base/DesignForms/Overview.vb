
Imports System.Reflection
Imports Kostal.Las.Base

Public Class Overview

    Private _dgViewColumnList As List(Of String)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _dgViewColumnList = New List(Of String)

        Init()

    End Sub

    Private Sub AddColmnList()

        _dgViewColumnList.Clear()

        For Each item As String In StationInformation.GetMemberList

            _dgViewColumnList.Add(item)

        Next

    End Sub


    Public ReadOnly Property GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property


    Public Function Init() As Boolean


        AddColmnList()

        dgView.BorderStyle = BorderStyle.Fixed3D

        dgView.Columns.Clear()

        Dim colWidth As Integer = (dgView.Width - dgView.RowHeadersWidth) / _dgViewColumnList.Count

        For Each item As String In _dgViewColumnList

            dgView.Columns.Add(item, item)

            dgView.Columns(item).Width = colWidth

        Next

        dgView.Rows.Clear()

        dgView.AllowUserToAddRows = True

        dgView.Rows.Add(6)

        For i As Integer = 1 To 6
            dgView.Rows(i - 1).HeaderCell.Value = i.ToString
        Next

        dgView.AllowUserToAddRows = False

        dgView.ScrollBars = ScrollBars.None


        Dim ic As IOControl
        ic = New IOControl(enum_STATION_KEY.Station01.ToString, IOType.INPUT) With {.Name = enum_STATION_KEY.Station01.ToString}
        'ic.Dock = DockStyle.None
        ic.Anchor = AnchorStyles.Bottom
        'ic.Left = (Me.picBoxSystem.Width - ic.Width) / 2
        'ic.Top = Me.picBoxSystem.Height * 3 / 4
        Me.picBoxSystem.Controls.Add(ic)
        ic = Nothing

        ic = New IOControl(enum_STATION_KEY.Station02.ToString, IOType.INPUT) With {.Name = enum_STATION_KEY.Station02.ToString}
        'ic.Dock = DockStyle.None
        ic.Anchor = AnchorStyles.Right
        'ic.Left = Me.picBoxSystem.Width - ic.Width * 2
        'ic.Top = Me.picBoxSystem.Height / 3
        Me.picBoxSystem.Controls.Add(ic)
        ic = Nothing

        ic = New IOControl(enum_STATION_KEY.Station03.ToString, IOType.INPUT) With {.Name = enum_STATION_KEY.Station03.ToString}
        'ic.Dock = DockStyle.None
        ic.Anchor = AnchorStyles.Left
        'ic.Left = ic.Width / 2
        'ic.Top = Me.picBoxSystem.Height / 3
        Me.picBoxSystem.Controls.Add(ic)
        ic = Nothing
        'dgView.Columns("Time").AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

        Return True

    End Function


    Public Function UpdateRow(ByVal stataionKey As enum_STATION_KEY, ByVal stationInfo As StationInformation) As Boolean

        Try

            For Each item As String In StationInformation.GetMemberList

                Dim fi As PropertyInfo = stationInfo.GetType.GetProperty(item)

                Dim value As String = fi.GetValue(stationInfo).ToString

                If dgView.Rows(stataionKey).Cells(item).Value <> value Then

                    dgView.Rows(stataionKey).Cells(item).Value = value

                End If

            Next

            Return True


        Catch ex As Exception

        End Try


        Return False

    End Function

    Public Function UpdateAll(ByVal infos As IEnumerable(Of StationInformation)) As Boolean

        For Each item As StationInformation In infos

            UpdateRow(item.StationKey, item)

        Next

        Return True

    End Function

    Private Sub dgView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellContentClick

        'dgView.

    End Sub

    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged

        If e.ColumnIndex < 0 Then Return

        If dgView.Columns(e.ColumnIndex).HeaderText = "Result" Then

            Select Case dgView.Rows(e.RowIndex).Cells("Result").Value.ToString
                Case True.ToString
                    dgView.Rows(e.RowIndex).Cells("Result").Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)'Color.Green
                Case False.ToString
                    dgView.Rows(e.RowIndex).Cells("Result").Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED) 'Color.Red
                Case Else
                    dgView.Rows(e.RowIndex).Cells("Result").Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
            End Select


        End If

    End Sub

    Private Sub dgView_Resize(sender As Object, e As EventArgs) Handles dgView.Resize

        If _dgViewColumnList Is Nothing Then Return

        If _dgViewColumnList.Count < 1 Then Return

        Dim colWidth As Integer = (dgView.Width - dgView.RowHeadersWidth) / _dgViewColumnList.Count

        For Each item As String In _dgViewColumnList

            dgView.Columns(item).Width = colWidth

        Next

    End Sub

    Private Sub picBoxSystem_Click(sender As Object, e As EventArgs) Handles picBoxSystem.Click

    End Sub

    Private Sub picBoxSystem_Validated(sender As Object, e As EventArgs) Handles picBoxSystem.Validated

    End Sub

    Private Sub picBoxSystem_Resize(sender As Object, e As EventArgs) Handles picBoxSystem.Resize
        Try



            Me.picBoxSystem.Controls("Station01").Left = Me.picBoxSystem.Width - Me.picBoxSystem.Controls("Station01").Width * 2
            Me.picBoxSystem.Controls("Station01").Top = Me.picBoxSystem.Height / 2

            Me.picBoxSystem.Controls("Station02").Left = Me.picBoxSystem.Width - Me.picBoxSystem.Controls("Station02").Width
            Me.picBoxSystem.Controls("Station02").Top = Me.picBoxSystem.Height / 3

            Me.picBoxSystem.Controls("Station03").Left = Me.picBoxSystem.Controls("Station03").Width
            Me.picBoxSystem.Controls("Station03").Top = Me.picBoxSystem.Height / 3

            Me.picBoxSystem.BackgroundImage = My.Resources.overview
            Me.picBoxSystem.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgView_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgView.ColumnHeaderMouseClick

    End Sub
End Class
