Public Class TimeDelay
    Protected _Running As Boolean
    Protected Delegate Function DelegateDelay(ByVal iTimeOut As Integer) As Boolean
    Protected pRoutine As New DelegateDelay(AddressOf _Run)
    Protected pCallBack As AsyncCallback = New AsyncCallback(AddressOf CallBack)


    ReadOnly Property Running As Boolean
        Get
            Return _Running
        End Get
    End Property

    Public Function Run(ByVal iTimeOut As Integer) As Boolean
        _Running = True
        pRoutine.BeginInvoke(iTimeOut, pCallBack, Nothing)
        Return True
    End Function
    Protected Function _Run(ByVal iTimeOut As Integer) As Boolean
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < iTimeOut
            System.Threading.Thread.Sleep(1)
        Loop
        sw.Stop()
        Return True
    End Function

    Protected Sub CallBack(ByVal Result As IAsyncResult)
        pRoutine.EndInvoke(Result)
        _Running = False
    End Sub

End Class
