Imports System.Reflection
Imports System.Collections.Generic


Public Class StationInformation

    Public Const conRESULT As String = "Result"
    Public Const conAutoManual As String = "AutoManual"
    Public Const conCarrierNumber As String = "WT"
    Public Const conDestinationStation As String = "DS"
    Public Const conSerialNumber As String = "SerialNumber"
    Public Const conPlcStatus As String = "PlcStatus"
    Public Const conEnable As String = "Enable"
    Public Const conStationName As String = "StationName"
    Public Const conScheduleName As String = "ScheduleName"
    Public Const conTestmanStatus As String = "TestmanStatus"

    Private _StationKey As String
    Private _Number As Integer = -1
    Private _StationName As String = String.Empty
    Private _StationEnable As Boolean = False
    Private _PlcStatus As String = String.Empty
    Private _StepNumber As Integer = -1
    Private _CarrierNumber As Integer = 0
    Private _DestinationStation As Integer = 0
    Private _ArticleNumber As String = String.Empty
    Private _SerialNumber As String = String.Empty
    Private _AutoManual As String = String.Empty
    Private _TestmanStatus As String = String.Empty
    Private _TestmanPercent As String = String.Empty
    Private _ScheduleName As String = String.Empty
    Private _TestTime As String = String.Empty
    Private _TestResult As Boolean = False

    Private Shared _columnKeyList As List(Of String)

    Public Sub New(StationKey As String)

        _StationKey = StationKey

        ' _Number = CInt(_StationKey)

        _StationName = StationKey.ToString

    End Sub

    Private Class ColumnAttribute : Inherits Attribute

        Public Sub New()
            'do nothing
        End Sub

    End Class

    Public Shared Function GetMemberList() As IEnumerable(Of String)


        If _columnKeyList Is Nothing Then

            ScanMembersOnce()

        End If

        Return _columnKeyList


    End Function


    Private Shared Sub ScanMembersOnce()

        _columnKeyList = New List(Of String)

        Try

            Dim t As Type = GetType(StationInformation)

            Dim sp As PropertyInfo() = t.GetProperties(BindingFlags.Instance Or BindingFlags.Public)

            For Each p As PropertyInfo In sp

                For Each arrtri As Object In p.GetCustomAttributes(True)

                    If TypeOf arrtri Is ColumnAttribute Then

                        _columnKeyList.Add(p.Name)

                    End If

                Next

            Next

        Catch ex As Exception

            Dim strErr As String = ex.Message

        End Try

    End Sub

    Public ReadOnly Property StationKey As String
        Get
            Return _StationKey
        End Get
    End Property

    Public Property Number As Integer
        Get
            Return _Number
        End Get
        Set(value As Integer)
            _Number = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property StationName As String
        Get
            Return _StationName
        End Get
        Set(value As String)
            _StationName = value
        End Set
    End Property

    '<ColumnAttribute>
    Public Property Enable As Boolean
        Get
            Return _StationEnable
        End Get
        Set(value As Boolean)
            _StationEnable = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property AutoManual As String
        Get
            Return _AutoManual
        End Get
        Set(value As String)
            _AutoManual = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property PlcStatus As String
        Get
            Return _PlcStatus
        End Get
        Set(value As String)
            _PlcStatus = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property StepNr As Integer
        Get
            Return _StepNumber
        End Get
        Set(value As Integer)
            _StepNumber = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property WT As Integer  'alias as CarrierNumber
        Get
            Return _CarrierNumber
        End Get
        Set(value As Integer)
            _CarrierNumber = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property DS As Integer  'alias as CarrierNumber
        Get
            Return _DestinationStation
        End Get
        Set(value As Integer)
            _DestinationStation = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property ArticleNr As String
        Get
            Return _ArticleNumber
        End Get
        Set(value As String)
            _ArticleNumber = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property SerialNumber As String
        Get
            Return _SerialNumber
        End Get
        Set(value As String)
            _SerialNumber = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property TestmanStatus As String
        Get
            Return _TestmanStatus
        End Get
        Set(value As String)
            _TestmanStatus = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property TestmanPercent As String
        Get
            Return _TestmanPercent
        End Get
        Set(value As String)
            _TestmanPercent = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property ScheduleName As String
        Get
            Return _ScheduleName
        End Get
        Set(value As String)
            _ScheduleName = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property TestTime As String
        Get
            Return _TestTime
        End Get
        Set(value As String)
            _TestTime = value
        End Set
    End Property

    <ColumnAttribute>
    Public Property Result As Boolean
        Get
            Return _TestResult
        End Get
        Set(value As Boolean)
            _TestResult = value
        End Set
    End Property


End Class




