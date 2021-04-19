'Version 2012_06_28_00 - Frank Dümpelmann
'   Change CounterFolder to _Settings.CounterFolder

Imports Kostal.Las.Base
Imports System.IO
Imports System.Windows.Forms
Public Class ArticleCounter
    Implements IArticleCounter
    Private _xmlHandler As New XmlHandler
    Private _FileHandler As New FileHandler
    Private _FileName_Data As String
    Private _Settings As New Settings
    Private _Language As Language
    Private _i As New Station
    Private _Log As Logger
    Private _IsInit As Boolean

    Private Const _FileExtension As String = ".dat"
    Private Const _BackUpFileExtension As String = ".bak"
    Private _CounterFolder As String = String.Empty

    Private Const _MAX_COLUMN As Integer = 4
    Private Const _BUTTON_COLUMN As Integer = 0
    Private Const _ARTICLE_COLUMN As Integer = 1
    Private Const _IN_WORK_COLUMN As Integer = 2
    Private Const _PASS_COLUMN As Integer = 3
    Private Const _FAIL_COLUMN As Integer = 4
    Private _ShowCounter As Boolean
    Private _mPassword As PassWordForm
    Public Const sName As String = "_mLineArticleCounter"
    '

    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Public Property ShowCounter() As Boolean
        Get
            Return _ShowCounter
        End Get
        Set(ByVal value As Boolean)
            _ShowCounter = value
        End Set
    End Property

    Private Sub ArticleCounter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _IsInit = False
    End Sub

    Private Sub ArticleCounter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
            _ShowCounter = False
        End If
    End Sub


    Public Function Init(ByVal MyParent As Station, ByVal MySettings As Settings, ByVal MyLanguage As Language) As Boolean
        Dim sResult As String, mData() As String, mCompleteFileName As String, mFile As StreamReader, sLine As String

        _Settings = MySettings
        _Language = MyLanguage

        _Log = New Logger(_Settings)

        _i.Name = "ArticleCounter"
        _i.IdString = "Main" + "_" + _i.Name
        LanguageInit()

        _mPassword = New PassWordForm
        _mPassword.Init(_i, _Settings, "UserPassWord")

        Me.StartPosition = FormStartPosition.Manual
        Me.Top = CInt((CDbl(My.Computer.Screen.WorkingArea.Height) / CDbl(2)) - (CDbl(Me.Height) / CDbl(2)))
        Me.Left = CInt((CDbl(My.Computer.Screen.WorkingArea.Width) / CDbl(2)) - (CDbl(Me.Width) / CDbl(2)))


        sResult = _xmlHandler.GetSectionInformation(_Settings.ApplicationFolder, _Settings.RootIniName, "Environment", "Screen")
        If IsNumeric(sResult) Then
            Me.Left = Me.Left + CInt(sResult) * My.Computer.Screen.WorkingArea.Width
        Else
            Me.Left = 0
        End If

        _FileName_Data = _Settings.CounterFolder + _Settings.ApplicationName + "_" + _i.IdString

        DG_Counter.Rows.Clear()
        mCompleteFileName = _FileName_Data & _FileExtension
        If File.Exists(mCompleteFileName) Then
            mFile = File.OpenText(mCompleteFileName)
            Do While Not mFile.EndOfStream
                sLine = mFile.ReadLine
                mData = Split(sLine, ";")
                If mData.GetUpperBound(0) = _MAX_COLUMN Then
                    If Not IsNumeric(mData(_IN_WORK_COLUMN)) Then
                        mData(_IN_WORK_COLUMN) = "0"
                    End If
                    If Not IsNumeric(mData(_PASS_COLUMN)) Then
                        mData(_PASS_COLUMN) = "0"
                    End If
                    If Not IsNumeric(mData(_FAIL_COLUMN)) Then
                        mData(_FAIL_COLUMN) = "0"
                    End If
                    DG_Counter.Rows.Add(mData)
                Else
                    _Log.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAIL_IN_COUNTER_FILE, mCompleteFileName, sLine), Me.Name)
                End If
            Loop
            mFile.Close()
        End If
        LanguageInit()
        '  Me.Show()

        _IsInit = True
        _Log.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_INIT, "Successful"), "Counter.Init")
        Return True
    End Function


    Public Sub Run()
        If _Settings.LineType <> enumLineType.MultiLine Then

            If Not _ShowCounter Then
                If Me.Visible Then Me.Visible = False
            Else
                If Not Me.Visible Then Me.Visible = True
            End If
        End If
    End Sub


    Public Sub Add_InWork(ByVal ArticleNumber As String) Implements IArticleCounter.Add_Record
        Dim RowPos As Integer
        RowPos = IsArticleInList(ArticleNumber)
        If RowPos < 0 Then
            AddRow(ArticleNumber, _IN_WORK_COLUMN)
        Else
            DG_Counter.Rows(RowPos).Cells(_IN_WORK_COLUMN).Value = CStr(CLng(DG_Counter.Rows(RowPos).Cells(_IN_WORK_COLUMN).Value) + 1)
        End If
    End Sub

    Public Sub Add_Pass(ByVal ArticleNumber As String) Implements IArticleCounter.Add_Pass
        Dim RowPos As Integer
        RowPos = IsArticleInList(ArticleNumber)
        If RowPos < 0 Then
            AddRow(ArticleNumber, _PASS_COLUMN)
        Else
            DG_Counter.Rows(RowPos).Cells(_PASS_COLUMN).Value = CStr(CLng(DG_Counter.Rows(RowPos).Cells(_PASS_COLUMN).Value) + 1)
        End If
        '  DecInWork(ArticleNumber)
    End Sub


    Public Sub Add_Fail(ByVal ArticleNumber As String) Implements IArticleCounter.Add_Fail
        Dim RowPos As Integer
        RowPos = IsArticleInList(ArticleNumber)
        If RowPos < 0 Then
            AddRow(ArticleNumber, _FAIL_COLUMN)
        Else
            DG_Counter.Rows(RowPos).Cells(_FAIL_COLUMN).Value = CStr(CLng(DG_Counter.Rows(RowPos).Cells(_FAIL_COLUMN).Value) + 1)
        End If
        ' DecInWork(ArticleNumber)
    End Sub


    Private Function IsArticleInList(ByVal ArticleNumber As String) As Integer
        Dim mRow As DataGridViewRow

        Try
            For Each mRow In DG_Counter.Rows
                If mRow.Cells(_ARTICLE_COLUMN).Value.ToString = ArticleNumber Then
                    Return mRow.Index
                    Exit Function
                End If
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function


    Private Sub AddRow(ByVal ArticleNumber As String, ByVal IncPos As Integer)


        DG_Counter.Rows.Add()
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_BUTTON_COLUMN).Value = DG_Counter.Columns(_BUTTON_COLUMN).HeaderText
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_ARTICLE_COLUMN).Value = ArticleNumber

        If IncPos = _IN_WORK_COLUMN Then
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_IN_WORK_COLUMN).Value = "1"
        Else
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_IN_WORK_COLUMN).Value = "0"
        End If

        If IncPos = _PASS_COLUMN Then
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_PASS_COLUMN).Value = "1"
        Else
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_PASS_COLUMN).Value = "0"
        End If

        If IncPos = _FAIL_COLUMN Then
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_FAIL_COLUMN).Value = "1"
        Else
            DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells(_FAIL_COLUMN).Value = "0"
        End If
        LanguageInit()
    End Sub


    Public Sub LanguageInit()
        Dim l As Integer
        _Language.ReadControlText(Me)
        DG_Counter.Columns(_ARTICLE_COLUMN).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Counter.Columns(" & Trim(CStr(_ARTICLE_COLUMN)) & ").HeaderCell")
        DG_Counter.Columns(_IN_WORK_COLUMN).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Counter.Columns(" & Trim(CStr(_IN_WORK_COLUMN)) & ").HeaderCell")
        DG_Counter.Columns(_PASS_COLUMN).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Counter.Columns(" & Trim(CStr(_PASS_COLUMN)) & ").HeaderCell")
        DG_Counter.Columns(_FAIL_COLUMN).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Counter.Columns(" & Trim(CStr(_FAIL_COLUMN)) & ").HeaderCell")
        DG_Counter.Columns(_BUTTON_COLUMN).HeaderCell.Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, Me.Name, "DG_Counter.Columns(" & Trim(CStr(_BUTTON_COLUMN)) & ").HeaderCell")

        For l = 0 To DG_Counter.Rows.Count - 1
            DG_Counter.Rows(l).Cells(_BUTTON_COLUMN).Value = DG_Counter.Columns(_BUTTON_COLUMN).HeaderText
        Next
    End Sub


    Private Sub Terminate()
        Dim mCompleteFileName As String, mCompleteBackUpFileName As String, mData() As String
        Dim mRow As DataGridViewRow, mCell As DataGridViewCell
        mCompleteFileName = _FileName_Data & _FileExtension
        mCompleteBackUpFileName = _FileName_Data & _BackUpFileExtension

        If File.Exists(mCompleteFileName) Then
            File.Copy(mCompleteFileName, mCompleteBackUpFileName, True)
        End If
        If DG_Counter.Rows.Count = 0 Then
            File.Delete(mCompleteFileName)
        Else
            ReDim mData(DG_Counter.Rows.Count - 1)
            For Each mRow In DG_Counter.Rows
                For Each mCell In mRow.Cells
                    If IsNothing(mCell) Then
                        '
                    ElseIf mCell.ColumnIndex = _BUTTON_COLUMN Then
                        mData(mRow.Index) = mData(mRow.Index) & ";"
                    ElseIf mCell.ColumnIndex = _MAX_COLUMN Then
                        mData(mRow.Index) = mData(mRow.Index) & mCell.Value.ToString
                    Else
                        mData(mRow.Index) = mData(mRow.Index) & mCell.Value.ToString & ";"
                    End If
                Next
            Next
            File.WriteAllLines(mCompleteFileName, mData)
        End If
    End Sub


    Private Sub DG_Counter_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG_Counter.CellContentClick
        On Error Resume Next
        _mPassword.ChangeMode = False
        _mPassword.ShowDialog()
        If _mPassword.PassWordValid Then
            DG_Counter.Rows().RemoveAt(e.RowIndex)
        End If
    End Sub
    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.Dispose()
        Return True
    End Function
End Class















