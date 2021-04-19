Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics
Imports System.Drawing.Printing
Imports System.Drawing
Imports TestInfo

Imports System.Xml
Imports System.Windows.Forms


Public Class StateObject
    ' Client socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 256
    ' Receive buffer.
    Public buffer(BufferSize) As Byte
    ' Received data string.
    Public sb As String
End Class

Public Class EpsonLan

    Public _client As Socket = Nothing
    Private _remoteEP As IPEndPoint = Nothing

    ' ManualResetEvent instances signal completion.
    Private _connectDone As New ManualResetEvent(False)
    Private _sendDone As New ManualResetEvent(False)
    Private _response As String = String.Empty
    Private _asyncResult As IAsyncResult

    Public Function Init(ByVal ip As String, ByVal iPort As Integer) As Boolean
        Dim isTestRusult As Boolean = False
        Try
            Dim ipAddress As IPAddress = IPAddress.Parse(ip)
            If _remoteEP Is Nothing Then _remoteEP = New IPEndPoint(ipAddress, iPort)
            If _client Is Nothing Then _client = New Socket(_remoteEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            If Not _client.Connected Then
                _connectDone.Reset()
                _asyncResult = _client.BeginConnect(_remoteEP, New AsyncCallback(AddressOf ConnectCallback), _client)
                isTestRusult = _connectDone.WaitOne(2000)
            Else
                Return True
            End If

            If Not isTestRusult Then Throw New Exception("Connect failed !")
        Catch ex As Exception
            Throw New Exception("Function:Init " + ex.Message.ToString)
            Return False
        End Try
        Return isTestRusult
    End Function

    Private Sub ConnectCallback(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            If Not client.Connected Then Return
            client.EndConnect(ar)
            _connectDone.Set()
            Receive(client)
        Catch ex As Exception
            Throw New Exception("Function:ConnectCallback " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub Receive(ByVal client As Socket)
        Try
            Dim state As New StateObject
            state.workSocket = client
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
        Catch ex As Exception
            Throw New Exception("Function:Receive " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub ReceiveCallback(ByVal ar As IAsyncResult)
        Try
            Dim state As StateObject = CType(ar.AsyncState, StateObject)
            Dim client As Socket = state.workSocket
            Dim bytesRead As Integer = client.EndReceive(ar)

            If bytesRead > 0 Then
                state.sb = Encoding.ASCII.GetString(state.buffer, 0, bytesRead)
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
                _response += state.sb
            End If
        Catch ex As Exception
            Throw New Exception("Function:ReceiveCallback " + ex.Message.ToString)
        End Try

    End Sub

    Private Sub Send(ByVal client As Socket, ByVal byteData() As Byte)
        Try
            client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
        Catch ex As Exception
            Throw New Exception("Function:Send " + ex.Message.ToString)
        End Try
    End Sub

    Private Sub SendCallback(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer = client.EndSend(ar)
            _sendDone.Set()
        Catch ex As Exception
            Throw New Exception("Function:SendCallback " + ex.Message.ToString)
        End Try
    End Sub

    Public Function Print(ByVal failInfo As TestInfo.TestInfo, ByVal ProjectName As String, ByVal SN As String) As Boolean
        Try
            Dim strMsg As String
            strMsg = "  ProjectName: " + ProjectName + vbCrLf &
                     "  Date: " + Now.ToString("dd.MM.yy HH:mm") + vbCrLf &
                     "  ArticleNo: " + failInfo.GetArticleNo + vbCrLf &
                     "  SerialNo: " + SN + vbCrLf &
                     "  FailStep: " + failInfo.GetFailStep + vbCrLf &
                     "  LowLimit: " + failInfo.GetFailLoLimit + vbCrLf &
                     "  UpLimit: " + failInfo.GetFailUpLimit + vbCrLf &
                     "  NormalValue: " + failInfo.GetFailNomvalue + vbCrLf &
                     "  MeasureValue: " + failInfo.GetMeasuredValue + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf + vbCrLf

            Dim byteData() As Byte
            byteData = Encoding.ASCII.GetBytes(strMsg)
            _sendDone.Reset()
            Send(_client, byteData)
            _sendDone.WaitOne()

            If Not CutLabel() Then Return False
            Return True
        Catch ex As Exception
            Throw New Exception("Function:Printer " + ex.Message.ToString)
            Return False
        End Try
    End Function
    Private Function CutLabel() As Boolean
        Try
            Dim bytCut() As Byte = {&H1B, &H40, &H1B, &H69, &H1}
            Send(_client, bytCut)
            _sendDone.WaitOne()
            Return True
        Catch ex As Exception
            Throw New Exception("Function:Printer " + ex.Message.ToString)
            Return False
        End Try
    End Function
    Public Function Quit() As Boolean
        Try
            If _client IsNot Nothing Then
                _client.Close()
            End If
        Catch ex As Exception
            Throw New Exception("Function:Quit " + ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

End Class
