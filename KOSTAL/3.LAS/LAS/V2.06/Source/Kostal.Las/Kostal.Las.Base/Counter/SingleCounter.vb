
Public Class SingleCounter
  Inherits NotifyingObject
  Implements ISingleCounter

  Private _value As Integer
  Private _enabled As Boolean = True

  Public ReadOnly Property Value() As Integer Implements ISingleCounter.Value
    Get
      Return CInt(_value)
    End Get
  End Property

  Public Sub Reset() Implements ISingleCounter.Reset
    SetValue(0)
  End Sub

  Public Sub SetValue(ByVal newValue As Integer) Implements ISingleCounter.SetValue
    If Enabled Then
      If Me.Value = newValue Then Return
      If newValue >= 0 Then
        _value = newValue
      Else
        _value = 0
      End If
    End If
    OnPropertyChanged(Member.Of(Function() Me.Value))
  End Sub

  Public Sub Increase() Implements ISingleCounter.Increase
    SetValue(Value + 1)
  End Sub

  Public Sub Decrease() Implements ISingleCounter.Decrease
    SetValue(Value - 1)
  End Sub

  ''' <summary>
  ''' Enables the counter.
  ''' </summary>
  Public Property Enabled() As Boolean
    Get
      Return _enabled
    End Get
    Set(ByVal value As Boolean)
      _enabled = value
      OnPropertyChanged()
    End Set
  End Property

End Class