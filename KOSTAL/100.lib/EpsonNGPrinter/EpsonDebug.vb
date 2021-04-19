Imports System.Drawing
Imports System.Drawing.Printing

Public Class EpsonDebug


    Private Const PRINTER_NAME As String = "EPSON Receipt"
    Private WithEvents pdPrint As PrintDocument
    Dim printclass As New EpsonNGPrinter.EpsonDocument

    Private _print As New EpsonSerial

    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage
        Dim printFont As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        e.Graphics.PageUnit = GraphicsUnit.Point
        e.Graphics.DrawString(Me.TextBox1.Text, printFont, Brushes.Black, 6, 4)
        e.HasMorePages = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'printclass.Init()
        'printclass.PrintFailLabel("TS1080.05; BETWEEN; 0; 1 ;; 9.9E+37", "10100709")

        _print.PrintT(textBoxTitle.Text, TextBox1.Text, "12345678")

    End Sub

    Private Sub EpsonDebug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If EpsonSerial._com Is Nothing Then
            _print.Init(10, "9600,N,8,1")
        End If

    End Sub

End Class