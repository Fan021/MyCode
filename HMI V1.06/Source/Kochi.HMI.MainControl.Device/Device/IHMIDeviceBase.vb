Imports System.Windows.Forms
Imports System.Collections.Concurrent


Public Interface IHMIDeviceBase
    Property Name As String
    Property DeviceState As enumDeviceState
    Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function CreateShortcutUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function CreateParameterUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface


Public Interface IPrinter
    Function GetPrinterFild(ByVal strDeviceName As String, ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal cHMILKSN As clsHMILKSN, ByRef lListPrinterCfg As List(Of clsPrinterCfg), ByRef strErrorMessage As String) As Boolean
End Interface

Public Interface IScanner
    Function Scanner(ByVal strDeviceName As String, ByVal strActionName As String, ByVal eScannerMethod As String, ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal strScanMsg As String, ByRef strErrorMessage As String) As Boolean
End Interface




Public Class clsPrinterCfg
    Public iIndex As Integer = 0
    Public strValue As String = ""
    Public Sub New(ByVal iIndex As Integer, ByVal strValue As String)
        Me.iIndex = iIndex
        Me.strValue = strValue
    End Sub
End Class

Public MustInherit Class clsHMIDeviceBase
    Implements IHMIDeviceBase
    Implements IDisposable
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Protected iInitUI As IInitUI
    Protected iControlUI As IControlUI
    Protected iShortcutUI As IShortcutUI
    Protected iParameterUI As IParameterUI
    Protected iProgramUI As IProgramUI
    Protected strName As String
    Protected eDeviceState As enumDeviceState

    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Public MustOverride Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IHMIDeviceBase.Init
    Public MustOverride Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateInitUI
    Public MustOverride Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateControlUI
    Public MustOverride Function CreateParameterUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateParameterUI
    Public MustOverride Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

    Public MustOverride Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.Quit


    Public Property Name As String Implements IHMIDeviceBase.Name
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
        End Set
    End Property

    Public Property DeviceState As enumDeviceState Implements IHMIDeviceBase.DeviceState
        Get
            Return eDeviceState
        End Get
        Set(ByVal value As enumDeviceState)
            eDeviceState = value
        End Set
    End Property


    Public ReadOnly Property InitUI As IInitUI
        Get
            Return iInitUI
        End Get
    End Property

    Public ReadOnly Property ControlUI As IControlUI
        Get
            Return iControlUI
        End Get
    End Property


    Public ReadOnly Property ShortcutUI As IShortcutUI
        Get
            Return iShortcutUI
        End Get
    End Property

    Public ReadOnly Property ParameterUI As IParameterUI
        Get
            Return iParameterUI
        End Get
    End Property


    Public ReadOnly Property ProgramUI As IProgramUI
        Get
            Return iProgramUI
        End Get
    End Property

    Public Sub Parameter_ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
        RaiseEvent ParameterChanged(Me, e)
    End Sub

    Public Function Clone(ByRef SrcList As List(Of String), ByRef TarList As List(Of String)) As Boolean
        Try
            TarList.Clear()
            For Each element As String In SrcList
                TarList.Add(element)
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    Public Overridable Function CreateShortcutUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateShortcutUI
        Return True
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Enum enumDeviceState
    IDLE = 0
    OPEN
    CLOSE
    [ERROR]
    UNKNOW
End Enum

Public Interface IDeviceUI
    ReadOnly Property UI As Panel

    Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface

Public Interface IInitUI
    Inherits IDeviceUI
    Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean
    Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean
    Function ChangeParameterToIni(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String), ByRef lTargetListParameter As List(Of String)) As Boolean
    Function ChangeIniToParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String), ByRef lTargetListParameter As List(Of String)) As Boolean

End Interface

Public Interface IParameterUI
    Inherits IDeviceUI
    Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean
    Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean
End Interface

Public Interface IControlUI
    Inherits IDeviceUI
    Property ObjectSource As Object
    Function StartRefresh(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function StopRefresh(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListControlParameter As List(Of String)) As Boolean
    Function CloseIO(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
End Interface

Public Interface IProgramUI
    Inherits IDeviceUI
    Property ObjectSource As Object
    ReadOnly Property Cancel As Boolean
    Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String), ByVal lListParameter As List(Of String)) As Boolean
    Function GetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String), ByRef lListParameter As List(Of String)) As Boolean
    Function CloseIO(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean

End Interface

Public Interface IShortcutUI
    Inherits IDeviceUI
    Property ObjectSource As Object
    Function StartRefresh(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function StopRefresh(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
End Interface