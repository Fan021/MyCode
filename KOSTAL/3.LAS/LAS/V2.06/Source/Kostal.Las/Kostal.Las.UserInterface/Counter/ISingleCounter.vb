Imports System.ComponentModel


''' <summary>
''' Simple counter that manages an integer value on provides events when value changes
''' </summary>
Public Interface ISingleCounter
  Inherits INotifyPropertyChanged

  ''' <summary>
  ''' Gets or sets the value representing the count.
  ''' </summary>
  ''' <value>
  ''' The new counter value.
  ''' </value>
  ''' <returns>The current counter value.</returns>
  ReadOnly Property Value() As Integer


  ''' <summary>
  ''' Resets this counter instance.
  ''' </summary>
  Sub Reset()


  ''' <summary>
  ''' Sets the value of this counter instance.
  ''' </summary>
  ''' <param name="value">The new value.</param>
  Sub SetValue(ByVal value As Integer)


  ''' <summary>
  ''' Increases the value by one.
  ''' </summary>
  Sub Increase()


  ''' <summary>
  ''' Decreases the value by one.
  ''' </summary>
  Sub Decrease()

End Interface
