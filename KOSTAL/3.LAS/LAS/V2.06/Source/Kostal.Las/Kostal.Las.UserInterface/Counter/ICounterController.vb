Imports System.ComponentModel


''' <summary>
''' Controller to count the successful and failed testruns and provide statistical data.
''' </summary>
Public Interface ICounterController
  Inherits INotifyPropertyChanged

  ''' <summary>
  ''' Gets the counter counting failed testruns.
  ''' </summary>
  ''' <returns>The counter counting failed testruns as <see cref="ISingleCounter"/></returns>
  ReadOnly Property Fail() As ISingleCounter


  ''' <summary>
  ''' Gets the counter counting successful testruns.
  ''' </summary>
  ''' <returns>The counter counting successful testruns as <see cref="ISingleCounter"/></returns>
  ReadOnly Property Success() As ISingleCounter


  ''' <summary>
  ''' Gets or sets a value representing the current counter state.
  ''' </summary>
  ''' <value>
  ''' The new state of counter as <see cref="CounterState"/>.
  ''' </value>
  ''' <returns>Current state of counter as <see cref="CounterState"/></returns>
  Property State() As CounterState


  ''' <summary>
  ''' Gets or sets the value indicating the failures in 'Parts per million'.
  ''' </summary>
  ''' <value>
  ''' New ppm value.
  ''' </value>
  ''' <returns>current ppm value</returns>
  Property Ppm() As Double


  ''' <summary>
  ''' Gets the value indicating the percentage of failures in amount of overall measured items.
  ''' </summary>
  ''' <returns>current percentage of failures.</returns>
  ReadOnly Property Percent() As String


  ''' <summary>
  ''' Clears the result indication.
  ''' </summary>
  Sub ClearResultIndication()


  ''' <summary>
  ''' Resets this instance.
  ''' </summary>
  Sub Reset()


  ''' <summary>
  ''' Restores the counter using specified counter name.
  ''' </summary>
  ''' <param name="counterName">Name of the counter.</param>
  Sub Restore(ByVal counterName As String)


  ''' <summary>
  ''' Stores this object using specified counter name.
  ''' </summary>
  ''' <param name="counterName">Name of the counter.</param>
  Sub Store(ByVal counterName As String)

  ''' <summary>
  ''' Set a fix Countername if this Name is set
  ''' </summary>
  ''' <remarks></remarks>
  Property ArticleIndependent() As Boolean

  ''' <summary>
  ''' Enables the counter
  ''' </summary>
  ''' <remarks></remarks>
  Property Enabled() As Boolean

End Interface