Imports System.IO
Public Class clsCsvHandler
    Private _Object As New Object
    Public Const Name As String = "CsvHandler"


    Public Sub SaveData(ByVal strFilePath As String, ByVal cDataSet As DataSet)
        SyncLock _Object
            Try
                Dim strText As String = String.Empty
                If strFilePath.IndexOf(".csv") < 0 Then
                    strFilePath = strFilePath + ".csv"
                End If
                If IsNothing(cDataSet) Then Return
                Dim cFile As StreamWriter
                cFile = New StreamWriter(strFilePath, False)
                For Each mDc As DataColumn In cDataSet.Tables(0).Columns
                    If strText = "" Then
                        strText = mDc.ColumnName
                    Else
                        strText = strText + "," + mDc.ColumnName
                    End If
                Next
                cFile.WriteLine(strText)
                For Each mDr As DataRow In cDataSet.Tables(0).Rows
                    strText = ""
                    For Each mDc As DataColumn In cDataSet.Tables(0).Columns

                        If strText = "" Then
                            strText = mDr(mDc).ToString()
                        Else
                            strText = strText + "," + mDr(mDc).ToString()
                        End If
                    Next
                    cFile.WriteLine(strText)
                Next
                cFile.Close()

            Catch ex As Exception
                Throw ex
            End Try
        End SyncLock
    End Sub
End Class

