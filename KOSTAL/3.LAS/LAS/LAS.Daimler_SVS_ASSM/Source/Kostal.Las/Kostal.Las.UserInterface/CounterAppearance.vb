Imports System.ComponentModel

Public Class CounterAppearance
  Inherits NotifyingObject

  Private _statisticVisible As Boolean = True
  Private _resultIndicationOnly As Boolean = False

  Public Property StatisticVisible() As Boolean
    Get
      Return _statisticVisible
    End Get
    Set(ByVal value As Boolean)
      _statisticVisible = value
      OnPropertyChanged()
    End Set
  End Property

  Public Property ResultIndicationOnly() As Boolean
    Get
      Return _resultIndicationOnly
    End Get
    Set(ByVal value As Boolean)
      _resultIndicationOnly = value
      OnPropertyChanged()
    End Set
  End Property

End Class
