Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base

Public Interface IUserDefine
    ReadOnly Property ErrorMsg As String
End Interface

Public Interface IGlobalListSubStep
    Inherits IUserDefine
    Function GetGlobalListSubStep(ByVal i As clsRunnerCfg, ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object), ByRef lListSubStepCfg As List(Of clsSubStepCfg)) As Boolean
End Interface

Public MustInherit Class clsRunnerBase
    Protected cRunnerCfg As clsRunnerCfg
    Protected cLocalElement As New Dictionary(Of String, Object)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cRunnerElement As Dictionary(Of String, Object)
    Protected i As New clsStep
    Public MustOverride Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object))
    Public MustOverride Function Run() As Boolean
    Public MustOverride Function Reset() As Boolean
    Public MustOverride Function Quit() As Boolean
End Class

Public MustInherit Class clsStationRunnerBase
    Protected cRunnerCfg As clsRunnerCfg
    Protected cLocalElement As New Dictionary(Of String, Object)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cRunnerElement As Dictionary(Of String, Object)
    Protected i As New clsStep
    Protected isRunning As Boolean = False
    Protected cMachineStationCfg As clsMachineStationCfg
    Protected cPictureShowManager As clsPictureShowManager
    Public Property MachineStationCfg As clsMachineStationCfg
        Set(ByVal value As clsMachineStationCfg)
            cMachineStationCfg = value
        End Set
        Get
            Return cMachineStationCfg
        End Get
    End Property

    Public Property Running As Boolean
        Set(ByVal value As Boolean)
            isRunning = value
        End Set
        Get
            Return isRunning
        End Get
    End Property
    Public ReadOnly Property PictureShowManager As clsPictureShowManager
        Get
            Return cPictureShowManager
        End Get
    End Property
    Public MustOverride Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function Run() As Boolean
    Public MustOverride Function Reset() As Boolean
    Public MustOverride Function Quit() As Boolean

End Class

Public Class clsRunnerCfg
    Public Const Name As String = "RunnerCfg"
    Protected strStationName As String
    Public Property StationName As String
        Set(ByVal value As String)
            strStationName = value
        End Set
        Get
            Return strStationName
        End Get
    End Property
    Sub New()

    End Sub
    Sub New(ByVal strStationName As String)
        Me.strStationName = strStationName
    End Sub
End Class

Public Class clsStep
    Protected mStepInputNumber As Long = -100
    Protected mStepOutputNumber As Long = -100
    Protected mToggle As Boolean
    Protected mAddress_Origin As Long = -100
    Protected mAddress_Home As Long = 0
    Protected mAddress_Pass As Long = 0
    Protected mAddress_Fail As Long = 0
    Protected mAddress_StationIndex As Long = 0


    Public Property Address_StationIndex() As Long
        Get
            Return mAddress_StationIndex
        End Get
        Set(ByVal value As Long)
            mAddress_StationIndex = value
        End Set
    End Property

    Public Property Address_Pass() As Long
        Get
            Return mAddress_Pass
        End Get
        Set(ByVal value As Long)
            mAddress_Pass = value
        End Set
    End Property

    Public Property Address_Fail() As Long
        Get
            Return mAddress_Fail
        End Get
        Set(ByVal value As Long)
            mAddress_Fail = value
        End Set
    End Property
    Public Property Address_Origin() As Long
        Get
            Return mAddress_Origin
        End Get
        Set(ByVal value As Long)
            mAddress_Origin = value
        End Set
    End Property

    Public Property Address_Home() As Long
        Get
            Return mAddress_Home
        End Get
        Set(ByVal value As Long)
            mAddress_Home = value
        End Set
    End Property

    Public Property StepInputNumber() As Long
        Get
            Return mStepInputNumber
        End Get
        Set(ByVal value As Long)
            mStepInputNumber = value
        End Set
    End Property

    Public Property StepOutputNumber() As Long
        Get
            Return mStepOutputNumber
        End Get
        Set(ByVal value As Long)
            mStepOutputNumber = value
        End Set
    End Property

    Public Property Toggle() As Boolean
        Get
            Return mToggle
        End Get
        Set(ByVal value As Boolean)
            mToggle = value
        End Set
    End Property
End Class

Public Class clsRunActionCfg
    Private _Object As New Object
    Private strActionName As String = String.Empty
    Private strMessage As String = String.Empty
    Private lListParameter As New List(Of Object)
    Private bResult As Boolean = False
    Private bIsRunning As Boolean = False

    Public Property ActionName As String
        Set(ByVal value As String)
            SyncLock _Object
                strActionName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strActionName
            End SyncLock
        End Get
    End Property

    Public Property Message As String
        Set(ByVal value As String)
            SyncLock _Object
                strMessage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strMessage
            End SyncLock
        End Get
    End Property

    Public Property Result As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bResult = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bResult
            End SyncLock
        End Get
    End Property

    Public Property IsRunning As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bIsRunning = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bIsRunning
            End SyncLock
        End Get
    End Property

    Public Sub Clean()
        SyncLock _Object
            lListParameter.Clear()
        End SyncLock
    End Sub

    Public Function GetParameter(ByVal iIndex As Object) As Object
        Return lListParameter(iIndex)
    End Function

    Public Sub AddParameter(ByVal oParameter As Object)
        lListParameter.Add(oParameter)
    End Sub
End Class
