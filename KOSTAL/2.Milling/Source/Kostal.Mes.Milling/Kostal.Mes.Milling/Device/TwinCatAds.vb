Imports TwinCAT.Ads
Imports System.Reflection
Imports System.Runtime.InteropServices
Public Class TwinCatAds
    Implements IDisposable

    Protected IsDisposed As Boolean = False

    Protected _AmsNetId As String = String.Empty
    Protected _Port As Integer = 0
    Protected _Setting As Settings
    Protected _PLCVairablesHandles As New Dictionary(Of String, Integer)
    Protected _notificationHandles As New ArrayList
    Protected _ListDeviceNotificationEx As New Dictionary(Of String, Object)
    Protected WithEvents TcAds As TcAdsClient
    Protected _StatusDescription As String
    Protected mStateInfo As String = String.Empty
    Protected _Object As New Object
    Protected _Stations As New Dictionary(Of String, IStationTypeBase)

    Public ReadOnly Property StateInfo() As String
        Get
            If IsNothing(TcAds) Then
                Return "nothing"
            Else
                Return TcAds.ReadState.AdsState.ToString
            End If
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public Property PLCVairablesHandles As Dictionary(Of String, Integer)
        Set(ByVal value As Dictionary(Of String, Integer))
            _PLCVairablesHandles = value
        End Set
        Get
            Return _PLCVairablesHandles
        End Get
    End Property

    Public Property ListDeviceNotificationEx As Dictionary(Of String, Object)
        Set(ByVal value As Dictionary(Of String, Object))
            _ListDeviceNotificationEx = value
        End Set
        Get
            Return _ListDeviceNotificationEx
        End Get
    End Property

    Public Function Init(ByVal Setting As Settings, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Try
            _Setting = Setting
            TcAds = New TcAdsClient
            _Stations = Stations

            TcAds.Connect(_Setting.PLCConfig.TwinCatAmsNetId, _Setting.PLCConfig.TwinCatPort)
            _Port = TcAds.ServerPort
            If _Port >= 300 AndAlso _Port <= 399 Then   'Ports 300-399 SystemManager do not support this Info
                _StatusDescription = "Connected"
            Else
                _StatusDescription = TcAds.ReadState.AdsState.ToString
                mStateInfo = TcAds.ReadState.AdsState.ToString
            End If
            If Not CloseIo() Then Return False
            Return True
        Catch ex As Exception
            _StatusDescription = "Twicat Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try

    End Function

    Public Function CloseIo() As Boolean
        Try
            For Each substation As IStationTypeBase In _Stations.Values
                If TypeOf substation Is MESStation Then
                    Dim station As MESStation = CType(substation, MESStation)
                    SetDOBit(station.StationCfg.WriteIO, False)
                End If
            Next
        Catch ex As Exception
            _StatusDescription = "Twicat Run Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function
    Public Function Run() As Boolean
        Try
            For Each substation As IStationTypeBase In _Stations.Values
                If TypeOf substation Is MESStation Then
                    Dim station As MESStation = CType(substation, MESStation)
                    If station.NeadRead Then
                        SetDOBit(1, False)
                        station.Read = GetDIBit(station.StationCfg.ReadIO)
                    End If
                    If station.WriteTrue Then
                        SetDOBit(1, False)
                        SetDOBit(station.StationCfg.WriteIO, True)
                        'SetDOBit(1, True)
                        station.WriteTrue = False
                    End If
                    If station.WriteFalse Then
                        SetDOBit(1, False)
                        SetDOBit(station.StationCfg.WriteIO, False)
                        'SetDOBit(1, False)
                        station.WriteFalse = False
                    End If

                    If station.isHome Then
                        SetDOBit(1, GetDIBit(station.StationCfg.ReadIO) And Not station.WriteTrue)
                    End If

                    If station.WritePLCAddress Then
                        WritePLCAddress(station._strPLcAddress)
                        station.WritePLCAddress = False
                    End If


                End If
            Next
        Catch ex As Exception
            _StatusDescription = "Twicat Run Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Sub WritePLCAddress(ByVal strAddress As String)
        SetDOBit(2, False)
        SetDOBit(3, False)
        SetDOBit(4, False)
        SetDOBit(5, False)
        SetDOBit(6, False)
        SetDOBit(7, False)

        Select Case strAddress
            Case "3"
                SetDOBit(2, True)
            Case "4"
                SetDOBit(3, True)
            Case "5"
                SetDOBit(4, True)
            Case "6"
                SetDOBit(5, True)
            Case "7"
                SetDOBit(6, True)
            Case "8"
                SetDOBit(7, True)
        End Select

    End Sub
    Public Function GetDIBit(ByVal nChannel As Integer) As Boolean
        Try
            Dim stream As New TwinCAT.Ads.AdsStream(1)
            Dim nByte As Integer = 0

            Dim nRet As Integer = TcAds.Read(_Setting.PLCConfig.ReadIndexGroup, nChannel + _Setting.PLCConfig.ReadIndexOffset, stream)

            Dim S As New IO.BinaryReader(stream)
            nByte = S.ReadByte()

            S.Close()

            stream.Close()
            stream.Dispose()
            If nByte = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            _StatusDescription = "Twicat GetDIBit Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
        End Try
    End Function

    Public Overridable Sub SetDOBit(ByVal nChannel As Integer, ByVal bValue As Boolean)
        Try
            Dim stream As New TwinCAT.Ads.AdsStream(1)
            Dim nByte As Byte = IIf(bValue, 1, 0)
            Dim S As New IO.BinaryWriter(stream)
            S.Write(nByte)
            TcAds.Write(_Setting.PLCConfig.WriteIndexGroup, nChannel + _Setting.PLCConfig.WriteIndexOffset, stream, 0, 1)

            S.Close()
            stream.Close()
            stream.Dispose()
        Catch ex As Exception
            _StatusDescription = "Twicat SetDOBit Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
        End Try
    End Sub
    ''' <summary>
    ''' 手动添加Notification.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function AddDeviceNotificationEx(ByVal indexGrop As Long, ByVal indexOffset As Long, ByVal strName As String, ByVal objectValue As Object) As Boolean
        Try
            Dim iHandel As Integer
            iHandel = TcAds.AddDeviceNotificationEx(indexGrop, indexOffset, AdsTransMode.OnChange, 10, 10, strName, objectValue.GetType)
            _notificationHandles.Add(iHandel)
            _ListDeviceNotificationEx.Add(strName, objectValue)
        Catch ex As Exception
            _StatusDescription = "Twicat AddDeviceNotificationEx Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' 手动添加Notification.
    ''' </summary>
    ''' <param name="VariableName"></param>
    ''' <remarks></remarks>
    Public Function AddNotificationEx(ByVal VariableName As String, ByVal ObjectType As Object) As Boolean
        Try
            Dim iHandel As Integer
            iHandel = TcAds.AddDeviceNotificationEx(VariableName, AdsTransMode.OnChange, 10, 10, VariableName, ObjectType.GetType)
            _notificationHandles.Add(iHandel)
            _ListDeviceNotificationEx.Add(VariableName, ObjectType)
        Catch ex As Exception
            _StatusDescription = "Twicat AddDeviceNotificationEx Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Notification回调函数.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub adsClient_AdsNotificationEx(ByVal sender As Object, ByVal e As TwinCAT.Ads.AdsNotificationExEventArgs) Handles TcAds.AdsNotificationEx
        SyncLock _Object
            Dim ObjectValue As New Object
            If _ListDeviceNotificationEx.ContainsKey(CType(e.UserData, String)) Then
                _ListDeviceNotificationEx(CType(e.UserData, String)) = e.Value
            End If
        End SyncLock
    End Sub

    Public Function GetDeviceNotificationEx(ByVal Name As String) As Object
        SyncLock _Object
            If _ListDeviceNotificationEx.ContainsKey(Name) Then
                Return _ListDeviceNotificationEx(Name)
            Else
                Throw New Exception("_ListDeviceNotificationEx not ContainsKey:" + Name)
            End If
        End SyncLock
        Return Nothing
    End Function


    Public Function AddAdsVariable(ByVal VariableName As String) As Boolean
        Try
            Dim hVariable As Integer = -1
            If VariableName.IndexOf(".") < 0 Then
                VariableName = "." + VariableName
            End If
            If _PLCVairablesHandles.ContainsKey(VariableName.Trim) Then
                _StatusDescription = "VariableName have existed"
                Return False
            End If
            hVariable = TcAds.CreateVariableHandle(VariableName.Trim)
            _PLCVairablesHandles.Add(VariableName.Trim, hVariable)
        Catch ex As Exception
            _StatusDescription = "AddAdsVariable Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function WriteAny(ByVal indexGroup As Long, ByVal indexOffset As Long, ByVal value As Object) As Boolean

        Try
            If Not TcAds.IsConnected Then Return Nothing
            TcAds.WriteAny(indexGroup, indexOffset, value)
            Return True
        Catch ex As Exception
            _StatusDescription = "WriteAny Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try

    End Function

    Public Function WriteAny(ByVal name As String, ByVal value As Object) As Boolean
        Try

            If Not TcAds.IsConnected Then Return Nothing
            If Not _PLCVairablesHandles.ContainsKey(name) Then
                _StatusDescription = "PLCVairablesHandles not contains Key:" + name
                Return False
            End If
            Try
                TcAds.WriteAny(_PLCVairablesHandles(name), value)
                Return True
            Catch ex As Exception
                _StatusDescription = "WriteAny Fail. Message:" + ex.Message
                Return False
            End Try
        Catch ex As Exception
            _StatusDescription = "WriteAny Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function

    Public Function ReadAny(ByVal name As String, ByVal type As Type, Optional ByVal args() As Integer = Nothing) As Object
        Try
            Dim objResult As Object = Nothing
            If Not TcAds.IsConnected Then Return Nothing
            If Not _PLCVairablesHandles.ContainsKey(name) Then
                _StatusDescription = "PLCVairablesHandles not contains Key:" + name
                Throw New Exception(_StatusDescription)
                Return Nothing
            End If
            Try
                objResult = TcAds.ReadAny(_PLCVairablesHandles(name), type, args)
                Return objResult
            Catch ex As Exception
                _StatusDescription = "ReadAny Fail. Message:" + ex.Message
                Throw New Exception(_StatusDescription)
                Return Nothing
            End Try
        Catch ex As Exception
            _StatusDescription = "ReadAny Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return ""
        End Try
    End Function

    Public Function ReadAny(ByVal indexGroup As Long, ByVal indexOffset As Long, ByVal type As Type, Optional ByVal args() As Integer = Nothing) As Object
        Try
            Dim objResult As Object = Nothing
            If Not TcAds.IsConnected Then Return Nothing
            Try
                objResult = TcAds.ReadAny(indexGroup, indexOffset, type, args)
                Return objResult
            Catch ex As Exception
                _StatusDescription = "ReadAny Fail. Message:" + ex.Message
                Throw New Exception(_StatusDescription)
                Return Nothing
            End Try
        Catch ex As Exception
            _StatusDescription = "ReadAny Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return ""
        End Try
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose

        If Not IsDisposed Then
            Try
                Dim iHandle As Integer
                For Each iHandle In _PLCVairablesHandles.Values
                    _TcAds.DeleteVariableHandle(iHandle)
                Next

                For Each iHandle In _notificationHandles
                    _TcAds.DeleteDeviceNotification(iHandle)
                Next

            Catch ex As Exception
                Dim strErr As String = ex.Message
            End Try
            _PLCVairablesHandles.Clear()
            _PLCVairablesHandles = Nothing

            GC.SuppressFinalize(Me)
            Finalize()
        End If

    End Sub

    Protected Overrides Sub Finalize()
        Try
            TcAds.Dispose()
            TcAds = Nothing
        Catch ex As Exception
            '
        End Try
        IsDisposed = True
        MyBase.Finalize()
    End Sub
End Class
