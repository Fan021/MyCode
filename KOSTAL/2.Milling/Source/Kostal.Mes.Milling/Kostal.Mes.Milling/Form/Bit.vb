﻿Imports System.Windows.Forms
Public Class Bit
    Private Setting As Settings
    Private Sql As BitStore
    Private ds As DataSet
    Private dtInfo As DataTable
    Private pageSize As Integer = 10
    Private nMax As Integer
    Private pageBit As Integer
    Private pageCurrent As Integer
    Private nCurrent As Integer
    Private mLanguage As Language

    Private Sub Bit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Sql = New BitStore
        Sql.Init(Setting)
        DisplayData
    End Sub

    Public Function Init(ByVal Devices As Dictionary(Of String, Object)) As Boolean
        mLanguage = CType(Devices(Language.Name), Language)
        Setting = CType(Devices(Settings.Name), Settings)
        Return True
    End Function

    Private Function DisplayData() As Boolean

        DataGridView1.DataSource = Nothing
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_ID.Text, TextBox_Article.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("Loading Data Fail")
            Return False
        End If
        Return True
    End Function

    Private Sub InitDataSet()

        nMax = dtInfo.Rows.Count
        pageBit = nMax \ pageSize
        If ((nMax Mod pageSize) > 0) Then pageBit = pageBit + 1
        pageCurrent = 1
        nCurrent = 0
        If (nMax = 0) Then pageBit = 1
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim nStartPos As Integer = 0
        Dim nEndPos As Integer = 0
        Dim dtTemp As DataTable = dtInfo.Clone
        If (pageCurrent = pageBit) Then
            nEndPos = nMax
        Else
            nEndPos = pageSize * pageCurrent
        End If
        nStartPos = nCurrent

        lblPageBit.Text = "/" + pageBit.ToString()
        txtCurrentPage.Text = Convert.ToString(pageCurrent)
        For i = nStartPos To nEndPos - 1
            dtTemp.ImportRow(dtInfo.Rows(i))
            nCurrent = nCurrent + 1
        Next
        bdsInfo.DataSource = dtTemp
        bdnInfo.BindingSource = bdsInfo
        DataGridView1.DataSource = bdsInfo
    End Sub

    Private Sub Button_Check_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Check.Click
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_ID.Text, TextBox_Article.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("Load Data Fail")
        End If
    End Sub

    Private Sub Button_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Reset.Click
        TextBox_ID.Text = ""
        TextBox_Article.Text = ""
        ds = New DataSet
        If Sql.SelectToDataSet(TextBox_ID.Text, TextBox_Article.Text, ds) Then
            dtInfo = ds.Tables(0)
            InitDataSet()
        Else
            MessageBox.Show("Load Data Fail")
        End If
    End Sub



    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If DataGridView1.CurrentRow.Index < DataGridView1.Rows.Count - 1 Then
            TextBox_Edit_Id.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value
            TextBox_Edit_Article.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(1).Value
            TextBox_Edit_Bit.Text = DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(2).Value
        End If
    End Sub

    Private Sub Button_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Add.Click
        If TextBox_Edit_Article.Text = "" Then
            MessageBox.Show("Article is Null")
            Return
        End If
        If TextBox_Edit_Bit.Text = "" Then
            MessageBox.Show("Bit is Null")
            Return
        End If
        If Not IsNumeric(TextBox_Edit_Bit.Text) Then
            MessageBox.Show("Bit is not Number")
            Return
        End If

        If Not Sql.GetBit(TextBox_Edit_Article.Text) Then
            MessageBox.Show("Article have existed")
            Return
        End If
        If Not Sql.InsertBit(TextBox_Edit_Article.Text, TextBox_Edit_Bit.Text) Then
            MessageBox.Show("Add Fail")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("Add Successful")
        End If
    End Sub

    Private Sub Button_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Modify.Click
        If TextBox_Edit_Article.Text = "" Then
            MessageBox.Show("Article is Null")
            Return
        End If
        If TextBox_Edit_Bit.Text = "" Then
            MessageBox.Show("Bit is Null")
            Return
        End If
        If Not IsNumeric(TextBox_Edit_Bit.Text) Then
            MessageBox.Show("Bit is not Number")
            Return
        End If

        If Sql.GetBit2(TextBox_Edit_Id.Text, TextBox_Edit_Article.Text) Then
            MessageBox.Show("Article not existed")
            Return
        End If
        If Not Sql.UpdateBit(TextBox_Edit_Id.Text, TextBox_Edit_Article.Text, TextBox_Edit_Bit.Text) Then
            MessageBox.Show("Modify Fail")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("Modify Success")
        End If
    End Sub

    Private Sub Button_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Delete.Click
        If TextBox_Edit_Id.Text = "" Then
            MessageBox.Show("Article is Null")
            Return
        End If

        If Sql.GetBit2(TextBox_Edit_Id.Text, TextBox_Edit_Article.Text) Then
            MessageBox.Show("Article not existed")
            Return
        End If

        If Not Sql.DelectBit(TextBox_Edit_Id.Text) Then
            MessageBox.Show("Delect Fail")
            Return
        End If
        If DisplayData() Then
            MessageBox.Show("Delect Successful")
        End If
    End Sub

    Private Sub bdnInfo_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles bdnInfo.ItemClicked
        If (e.ClickedItem.Name = "ToolStripLabel1") Then
            pageCurrent = pageCurrent - 1
            If (pageCurrent <= 0) Then
                pageCurrent = 0
                Return
            Else
                nCurrent = pageSize * (pageCurrent - 1)
            End If
            LoadData()
        End If

        If (e.ClickedItem.Name = "ToolStripLabel2") Then
            pageCurrent = pageCurrent + 1
            If (pageCurrent > pageBit) Then
                pageCurrent = pageBit
                Return
            Else
                nCurrent = pageSize * (pageCurrent - 1)
            End If
            LoadData()
        End If
    End Sub

    Private Sub Bit_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        mLanguage.ReadControlText(Me)
    End Sub
End Class