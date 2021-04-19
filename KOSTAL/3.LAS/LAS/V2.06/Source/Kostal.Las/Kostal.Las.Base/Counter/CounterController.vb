
''' <summary>
''' Counter Controller functions
''' </summary>
''' <remarks></remarks>
Public Class CounterController
    Inherits NotifyingObject
    Implements ICounterController

    Private ReadOnly _failCounter As New SingleCounter
    Private ReadOnly _successCounter As New SingleCounter
    Private _ppm As Double
    Private _counterState As CounterState
    'Private _persistentEngine As IItemDao
    Private _articleIndependent As Boolean = False
    Private _enabled As Boolean = True

    Private Const FixCounterName As String = "LAS"

    Private _counterAppearance As CounterAppearance
    'Private _logger As NLog.Logger

    Public Sub New()
        '_logger = NLog.LogManager.GetCurrentClassLogger

        AddHandler _successCounter.PropertyChanged, AddressOf OnSuccessCounterPropertyChanged
        AddHandler _failCounter.PropertyChanged, AddressOf OnFailCounterPropertyChanged

        _counterAppearance = New CounterAppearance
        'Try
        '  SetpersistentEngine(PersistentData.Instance.Item())
        'Catch ex As Exception
        '  _logger.ErrorException("Binding to Persistent Engine not possible. No counter data will be saved or restored.", ex)
        'End Try
    End Sub

    'Public Sub SetpersistentEngine(ByVal persistentEngine As IItemDao)
    '  _persistentEngine = persistentEngine
    'End Sub

    Public ReadOnly Property CounterAppearance As CounterAppearance
        Get
            Return _counterAppearance
        End Get
    End Property

    ''' <summary>
    ''' Clear BackColor Indication
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearResultIndication() Implements ICounterController.ClearResultIndication
        State = CounterState.Waiting
        Update()
    End Sub

    ''' <summary>
    ''' returns the CounterState
    ''' </summary>
    ''' <returns>CounterState -> Successfully or Failed or Waiting</returns>
    ''' <remarks></remarks>
    Public Property State() As CounterState Implements ICounterController.State
    Get
      Return _counterState
    End Get
    Set(ByVal value As CounterState)
      _counterState = value
      OnPropertyChanged()
    End Set
  End Property

  ''' <summary>
  '''  Reset Counter 
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub Reset() Implements ICounterController.Reset
    _failCounter.Reset()
    _successCounter.Reset()
    ClearResultIndication()
  End Sub

    '''' <summary>
    '''' Resore Counter 
    '''' </summary>
    '''' <param name="counterName">Name of Counter</param>
    '''' <remarks></remarks>
    Public Sub Restore(ByVal SurfaceCounter As SurfaceCounter) Implements ICounterController.Restore

        'Dim key As String

        'Try
        '  If _persistentEngine Is Nothing Then
        '    _logger.Warn("Persistent Engine was not created, set initial counter values to 0.")
        '    Success.Reset()
        '    Fail.Reset()
        '  Else
        '    If _articleIndependent Then
        '      key = FixCounterName
        '    Else
        'key = counterName
        '    End If

        '    Dim item As DataMapping.IItem = _persistentEngine.Item(key)
        If SurfaceCounter IsNot Nothing Then
            '      Dim entity As CounterEntity = TryCast(item.Value, CounterEntity)
            Success.SetValue(CInt(SurfaceCounter.Pass))
            Fail.SetValue(CInt(SurfaceCounter.Fail))
        Else
            Success.Reset()
            Fail.Reset()
        End If

        '  End If
        'Catch ex As Exception
        '  _logger.ErrorException("Exception while restoring Counter. Countervalues are set to 0. Exception: ", ex)
        '  Success.Reset()
        '  Fail.Reset()
        'End Try

        ClearResultIndication()

    End Sub

    '''' <summary>
    '''' Store Counter
    '''' </summary>
    '''' <param name="counterName">Name of Counter</param>
    '''' <remarks></remarks>
    Public Sub Store(ByVal SurfaceCounter As SurfaceCounter) Implements ICounterController.Store
        'If _persistentEngine Is Nothing Then
        '  _logger.Warn("Persistent Engine was not created, counter values could NOT be stored.")
        'Else
        '  Dim key As String
        '  Try
        '    If _articleIndependent Then
        '      key = FixCounterName
        '    Else
        '      key = counterName
        '    End If

        '    If key <> "" Then
        '      Dim entity As New CounterEntity
        '      entity.Fail = Fail.Value
        '      entity.Pass = Success.Value
        '      _persistentEngine.AddOrUpdate(New DataMapping.Item(key, entity))
        '    End If
        '  Catch ex As Exception
        '    _logger.ErrorException("Exception while writing Counter. Exception: ", ex)
        '  End Try
        'End If
    End Sub

    ''' <summary>
    ''' Update Values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Update()
    If _successCounter.Value + Fail.Value = 0 Then
      Ppm = 0
    Else
      Ppm = Fail.Value / (Success.Value + Fail.Value)
    End If
  End Sub

  Private Sub OnSuccessCounterPropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
    ' e.PropertyName could be "Value" or "Enabled" !
    _counterState = CounterState.Successfully
    Update()
    OnPropertyChanged(Member.Of(Function() Me.Success))
  End Sub

  Private Sub OnFailCounterPropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
    ' e.PropertyName could be "Value" or "Enabled" !
    _counterState = CounterState.Failed
    Update()
    OnPropertyChanged(Member.Of(Function() Me.Fail))
  End Sub

  Public ReadOnly Property Fail() As Base.ISingleCounter Implements Base.ICounterController.Fail
    Get
      Return _failCounter
    End Get
  End Property

  Public ReadOnly Property Success() As Base.ISingleCounter Implements Base.ICounterController.Success
    Get
      Return _successCounter
    End Get
  End Property

  ''' <summary>
  ''' returns PPM Value
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property Ppm() As Double Implements ICounterController.Ppm
    Get
      Return _ppm
    End Get

    Set(ByVal value As Double)
      _ppm = value
    End Set
  End Property

  ''' <summary>
  ''' returns Percent Value
  ''' </summary>
  ''' <returns>Percent String Value e.g. "23,78 %"</returns>
  ''' <remarks></remarks>
  Public ReadOnly Property Percent() As String Implements ICounterController.Percent
    Get
      'Return Microsoft.VisualBasic.Strings.Format(Ppm * 100, "#0.00") + " %"
      Return String.Format("{0:P2}", Ppm)
    End Get
  End Property

  Public Property ArticleIndependent() As Boolean Implements Base.ICounterController.ArticleIndependent
    Get
      Return _articleIndependent
    End Get

    Set(ByVal value As Boolean)
      _articleIndependent = value
    End Set
  End Property

  Public Property Enabled() As Boolean Implements Base.ICounterController.Enabled
    Get
      Return _enabled
    End Get

    Set(ByVal value As Boolean)
      _enabled = value
      _failCounter.Enabled = _enabled
      _successCounter.Enabled = _enabled
    End Set
  End Property

End Class