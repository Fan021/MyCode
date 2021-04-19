Imports System.IO
Imports System.Text
Namespace Ept
    ''' <summary>
    ''' Contains routines to check the encoding of a file by analysing the 'Byte Order Mask' (BOM) of a file
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EncodingDetector


        ''' <summary>
        ''' Detects the byte order mark of a file and returns
        ''' an appropriate encoding for the file.
        ''' </summary>
        ''' <param name="srcFile">the file to be checked</param>
        ''' <returns></returns>
        Public Shared Function GetFileEncoding(ByVal srcFile As String) As Encoding
            ' *** Use Default of Encoding.Default (Ansi CodePage)
            Dim enc As Encoding = Encoding.Default

            '*** Detect byte order mark if any - otherwise assume default
            Dim buffer(5) As Byte
            Using file As FileStream = New FileStream(srcFile, FileMode.Open)
                file.Read(buffer, 0, 5)
                file.Close()

                If (buffer(0) = &HEF AndAlso buffer(1) = &HBB AndAlso buffer(2) = &HBF) Then

                    enc = Encoding.UTF8

                ElseIf (buffer(0) = &HFE AndAlso buffer(1) = &HFF) Then

                    enc = Encoding.Unicode

                ElseIf (buffer(0) = 0 AndAlso buffer(1) = 0 AndAlso buffer(2) = &HFE AndAlso buffer(3) = &HFF) Then

                    enc = Encoding.UTF32

                ElseIf (buffer(0) = &H2B AndAlso buffer(1) = &H2F AndAlso buffer(2) = &H76) Then

                    enc = Encoding.UTF7

                End If

            End Using

            Return enc

        End Function


    End Class
End Namespace
