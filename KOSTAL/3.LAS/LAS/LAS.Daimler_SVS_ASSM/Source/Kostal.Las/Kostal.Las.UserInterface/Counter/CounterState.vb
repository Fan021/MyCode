
''' <summary>
''' Specifies the current state of counter.
''' It has either counted a successful or failed run or it's waiting for a result.
''' </summary>
<Serializable()> _
Public Enum CounterState As Integer
  Successfully
  Failed
  Waiting
End Enum