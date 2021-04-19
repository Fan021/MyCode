Imports System
Imports System.Diagnostics

Namespace Helper

  Module Diagnostics

    ''' <summary>
    ''' Print the stackframes of the current thread to console
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PrintStackFrames()

      Dim st As New StackTrace(True)
      Dim i As Integer

      For i = 0 To st.FrameCount - 1
        Dim sf As StackFrame = st.GetFrame(i)
        Console.WriteLine("Method: {0} {1}", sf.GetMethod(), sf.GetFileLineNumber())
      Next i

    End Sub

  End Module

End Namespace
