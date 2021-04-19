Imports System.Windows.Forms
Imports System.ComponentModel
Imports Kostal.Las.Base

Public Class PrinterUI
    Implements IPrinterUI

    Public Event RePrint()
    Protected Sub Printer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property Panel As Panel Implements IPrinterUI.Panel
        Get
            Return DockPanel
        End Get
    End Property


    Public ReadOnly Property Print As Button Implements IPrinterUI.Print
        Get
            Return btnReprint
        End Get
    End Property


    Public ReadOnly Property StepID As Label Implements IPrinterUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Msg As Label Implements IPrinterUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property DataList As DataGridView Implements IPrinterUI.DataList
        Get
            Return DG_Printer
        End Get
    End Property

    Public ReadOnly Property Count As TextBox Implements IPrinterUI.Count
        Get
            Return TextBox_Count
        End Get
    End Property

    Private Sub btnReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReprint.Click
        RaiseEvent RePrint()
    End Sub


    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal Fileds As String(), ByVal bResult As Boolean) As Boolean
        If DG_Printer.Rows.Count >= 20 Then
            DG_Printer.Rows.RemoveAt(DG_Printer.Rows.Count - 1)
        End If
        DG_Printer.Rows.Add()
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        If Not IsNothing(Fileds) Then
            For i = 0 To Fileds.Length - 1
                DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Fild" & i).Value = Fileds(i)
                DG_Printer.Columns("Fild" & i).Visible = True
            Next
        End If
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_Printer.Rows(DG_Printer.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Printer.Sort(DG_Printer.Columns("Time"), ListSortDirection.Descending)

        Return True
    End Function


    Public Function AddColumns(ByVal iFileNumber As Integer) As Boolean
        DG_Printer.Columns.Clear()
        DG_Printer.Columns.Add("SN", "SN")
        DG_Printer.Columns.Add("Result", "Result")
        DG_Printer.Columns.Add("Article", "Article")
        DG_Printer.Columns.Add("Customer", "Customer")
        DG_Printer.Columns.Add("ProductFamily", "ProductFamily")
        For i = 0 To iFileNumber
            DG_Printer.Columns.Add("Fild" & i, "Fild" & i)
            '  DG_Printer.Columns("Fild" & i).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            DG_Printer.Columns("Fild" & i).Visible = False
        Next

        DG_Printer.Columns.Add("Time", "Time")

        'AutoResize(DG_Printer)
        Return True
    End Function

    Private Function AutoResize(ByRef DGV As DataGridView) As Boolean
        For i As Integer = 0 To DGV.Columns.Count
            DGV.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells)
        Next

        Return True
    End Function

    Private Sub TextBoxValue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox_Count.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub
End Class