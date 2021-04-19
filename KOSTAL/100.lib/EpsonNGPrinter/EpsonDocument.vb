Imports System.Drawing.Printing
Imports System.Drawing
Public Class EpsonDocument
    Private WithEvents pdPrint As PrintDocument
    Dim printTxt As String
    Public Sub Init()
        pdPrint = New PrintDocument
    End Sub
    ''TS1080.05; BETWEEN; 0; 1 ;; 9.9E+37
    Public Sub Quit()
        pdPrint.Dispose()
    End Sub

    Public Sub PrintFailLabel(ByVal ngInfo As String, ByVal article As String)
        printTxt = GerneratePrintTxt(ngInfo, article)
        pdPrint.Print()
    End Sub

    Private Function GerneratePrintTxt(ByVal nginfo As String, ByVal articleName As String) As String
        Dim resultstr As String = ""
        Dim newLine As String = vbCrLf
        Dim tempstr() As String
        Dim splitChar As String = ";"
        Dim labelTitle As String = "--------------MQB/Audi SCM Failure Part --------------" & vbCrLf
        tempstr = nginfo.Split(splitChar)
        If tempstr.Length < 5 Then
        Else
            resultstr = labelTitle & newLine & "Date: " & Format(Now, "yyyy/MM/dd HH:mm") & newLine
            resultstr += newLine & "Article: " & articleName & newLine
            resultstr += newLine & "Test Step: " & tempstr(0) & newLine
            resultstr += newLine & "Compare Mode: " & tempstr(1) & newLine
            resultstr += newLine & "LLimit: " & tempstr(2) & newLine
            resultstr += newLine & "ULimit: " & tempstr(3) & newLine
            resultstr += newLine & "Nomal Value: " & tempstr(4) & newLine
            resultstr += newLine & "Actual Value: " & tempstr(5) & newLine
            resultstr += newLine & "---------------------------------------------------"
        End If
        Return resultstr
    End Function

    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdPrint.PrintPage
        Dim printFont As New Font("Arial", 5, FontStyle.Regular, GraphicsUnit.Point)
        e.Graphics.PageUnit = GraphicsUnit.Point
        e.Graphics.DrawString(printTxt, printFont, Brushes.Black, 6, 4)
        e.HasMorePages = False
    End Sub
End Class
