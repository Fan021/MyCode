Imports Kochi.HMI.MainControl.UI
Public Class clsDataGridViewPage
    Private _RowsPerPage As Integer
    Private _TotalPage As Integer
    Private _curPage As Integer = 0
    Private _DataGridView As Windows.Forms.DataGridView
    Private _HMIDataViewPage As HMIDataViewPage
    Private _dv As DataView
    Public Const Name As String = "DataGridViewPage"

    Public Property RowsPerPage() As Integer
        Get
            Return _RowsPerPage
        End Get
        Set(ByVal value As Integer)
            _RowsPerPage = value
        End Set
    End Property

    Public ReadOnly Property TotalPage() As Integer
        Get
            Return _TotalPage
        End Get
    End Property

    Public Property curPage() As Integer
        Get
            Return _curPage
        End Get
        Set(ByVal value As Integer)
            _curPage = value
        End Set
    End Property

    Public WriteOnly Property SetHMIDataViewPage
        Set(ByVal value As Object)
            _HMIDataViewPage = value
        End Set
    End Property


    Public WriteOnly Property SetDataGridView()
        Set(ByVal value As Object)
            _DataGridView = value
        End Set
    End Property

    Public WriteOnly Property SetDataView()
        Set(ByVal value As Object)
            _dv = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Function RegisterManager(ByVal datagridview As Windows.Forms.DataGridView, ByVal hmiDataViewPage As HMIDataViewPage) As Boolean
        _DataGridView = datagridview
        _HMIDataViewPage = hmiDataViewPage
        AddHandler _HMIDataViewPage.Button_Down.Click, AddressOf Button_Click
        AddHandler _HMIDataViewPage.Button_DownLast.Click, AddressOf Button_Click
        AddHandler _HMIDataViewPage.Button_Go.Click, AddressOf Button_Click
        AddHandler _HMIDataViewPage.Button_Up.Click, AddressOf Button_Click
        AddHandler _HMIDataViewPage.Button_UpFirst.Click, AddressOf Button_Click
        Return True
    End Function

    Public Sub Paging(ByVal cViewPageType As enumViewPageType, Optional ByVal iPageNo As Integer = 0)
        If _dv.Count <= _RowsPerPage Then
            _TotalPage = 1
            GoLastPage()
            Exit Sub
        End If

        If _dv.Count Mod _RowsPerPage = 0 Then
            _TotalPage = Int(_dv.Count / _RowsPerPage)
        Else
            _TotalPage = Int(_dv.Count / _RowsPerPage) + 1
        End If
        If cViewPageType = enumViewPageType.FirstPage Then
            GoFirstPage()
        End If
        If cViewPageType = enumViewPageType.NoPage Then
            GoNoPage(_curPage)
        End If
        If cViewPageType = enumViewPageType.DefinePage Then
            GoNoPage(iPageNo)
        End If
        If cViewPageType = enumViewPageType.LastPage Then
            GoLastPage()
        End If

    End Sub

    Public Sub GoFirstPage()
        If _TotalPage = 1 Then
            GoLastPage()
            Exit Sub
        End If
        _curPage = 0
        GoNoPage(_curPage)
    End Sub

    Public Sub GoNextPage()
        _curPage += 1
        If _curPage > _TotalPage - 1 Then
            _curPage = _TotalPage - 1
            Exit Sub
        End If

        If _curPage = _TotalPage - 1 Then
            GoLastPage()
            Exit Sub
        End If

        GoNoPage(_curPage)
    End Sub

    Public Sub GoPrevPage()
        _curPage -= 1
        If _curPage < 0 Then
            _curPage = 0
            Exit Sub
        End If

        GoNoPage(_curPage)
    End Sub

    Public Sub GoLastPage()
        _curPage = _TotalPage - 1
        Dim i As Integer
        Dim dt As New DataTable
        dt = _dv.ToTable.Clone

        For i = (_TotalPage - 1) * _RowsPerPage To _dv.Count - 1
            Dim dr As DataRow = dt.NewRow
            dr.ItemArray = _dv.ToTable.Rows(i).ItemArray
            dt.Rows.Add(dr)
        Next
        _HMIDataViewPage.Button_DownEnable = IIf(_curPage = _TotalPage - 1, False, True)
        _HMIDataViewPage.Button_DownLastEnable = IIf(_curPage = _TotalPage - 1, False, True)
        _HMIDataViewPage.Button_UpEnable = IIf(_curPage = 0, False, True)
        _HMIDataViewPage.Button_UpFirstEnable = IIf(_curPage = 0, False, True)
        _HMIDataViewPage.Button_GoEnable = True
        _HMIDataViewPage.TotallPage = _TotalPage
        _HMIDataViewPage.CurrentPage = (_curPage + 1)
        _HMIDataViewPage.TotalRecord = _dv.Count
        _HMIDataViewPage.UpdateForm()
        _DataGridView.DataSource = dt
    End Sub

    Public Sub GoNoPage(ByVal PageNo As Integer)
        _curPage = PageNo
        If _curPage < 0 Then
            GoFirstPage()
            Exit Sub
        End If

        If _curPage >= _TotalPage Then
            _curPage = _TotalPage - 1
            Exit Sub
        End If

        If _curPage = _TotalPage - 1 Then
            GoLastPage()
            Exit Sub
        End If

        Dim dt As New DataTable
        dt = _dv.ToTable.Clone
        Dim i As Integer
        For i = PageNo * _RowsPerPage To (PageNo + 1) * _RowsPerPage - 1
            Dim dr As DataRow = dt.NewRow
            dr.ItemArray = _dv.ToTable.Rows(i).ItemArray
            dt.Rows.Add(dr)
        Next
        _HMIDataViewPage.Button_DownEnable = IIf(_curPage = _TotalPage - 1, False, True)
        _HMIDataViewPage.Button_DownLastEnable = IIf(_curPage = _TotalPage - 1, False, True)
        _HMIDataViewPage.Button_UpEnable = IIf(_curPage = 0, False, True)
        _HMIDataViewPage.Button_UpFirstEnable = IIf(_curPage = 0, False, True)
        _HMIDataViewPage.Button_GoEnable = True
        _HMIDataViewPage.TotallPage = _TotalPage
        _HMIDataViewPage.CurrentPage = (_curPage + 1)
        _HMIDataViewPage.TotalRecord = _dv.Count
        _HMIDataViewPage.UpdateForm()
        _DataGridView.DataSource = dt
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Button_Down"
                GoNextPage()
            Case "Button_DownLast"
                GoLastPage()
            Case "Button_Go"
                GoNoPage(_HMIDataViewPage.CurrentPage - 1)
            Case "Button_Up"
                GoPrevPage()
            Case "Button_UpFirst"
                GoFirstPage()
        End Select

    End Sub

End Class

Public Enum enumViewPageType
    FirstPage
    NoPage
    LastPage
    DefinePage
End Enum
