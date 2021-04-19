Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.CompilerServices

''' <summary>
''' Base class for the correct use of <see cref="INotifyPropertyChanged"/>
''' </summary>
Public MustInherit Class NotifyingObject
  Implements INotifyPropertyChanged

  ''' <summary>
  '''   Raised when a property on this object has a new value.
  ''' </summary>
  Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

  ' ''' <summary>
  ' ''' If set to <c>True<c> no notifycation will be raised
  ' ''' </summary>
  'Protected Property IsPropertyChangeNotificationSuspended() As Boolean
  '  Get
  '    Return m_IsPropertyChangeNotificationSuspended
  '  End Get
  '  Set(value As Boolean)
  '    m_IsPropertyChangeNotificationSuspended = value
  '  End Set
  'End Property

  'Private m_IsPropertyChangeNotificationSuspended As Boolean

  ''' <summary>
  ''' Raises this object's PropertyChanged event.
  ''' </summary>
  ''' <param name="propertyName">
  ''' The property that has a new value.
  ''' If property name is Nothing the name of the calling Property will be used aus property name
  ''' </param>
  Protected Overridable Sub OnPropertyChanged(<CallerMemberNameAttribute> Optional ByVal propertyName As String = Nothing)

    'If Me.IsPropertyChangeNotificationSuspended Then
    '  Return
    'End If

    'Me.VerifyPropertyName(propertyName)

    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))

  End Sub
  ''' <summary>
  ''' Raises this object's PropertyChanged event.
  ''' </summary>
  ''' <param name="sender">
  ''' The sender of the event
  ''' </param>
  ''' <param name="propertyName">
  ''' The property that has a new value.
  ''' If property name is Nothing the name of the calling Property will be used aus property name
  ''' </param>
  Protected Overridable Sub OnPropertyChangedWithSender(ByVal sender As Object, <CallerMemberName> Optional ByVal propertyName As String = Nothing)

    'If Me.IsPropertyChangeNotificationSuspended Then
    '  Return
    'End If

    'Me.VerifyPropertyName(propertyName)

    RaiseEvent PropertyChanged(sender, New PropertyChangedEventArgs(propertyName))

  End Sub


#Region "Debugging Aides"

  ''' <summary>
  '''   Returns whether an exception is thrown, or if a Debug.Fail() is used
  '''   when an invalid property name is passed to the VerifyPropertyName method.
  '''   The default value is false, but subclasses used by unit tests might 
  '''   set this property's getter to return true.
  ''' </summary>
  Protected Property ThrowOnInvalidPropertyName() As Boolean
    Get
      Return _throwOnInvalidPropertyName
    End Get
    Set(value As Boolean)
      _throwOnInvalidPropertyName = value
    End Set
  End Property
  Private _throwOnInvalidPropertyName As Boolean

  ''' <summary>
  ''' Warns the developer if this object does not have
  '''   a public property with the specified name. This 
  '''   method does not exist in a Release build.
  ''' </summary>
  <DebuggerStepThrough> _
  <Conditional("DEBUG")>
  Private Sub VerifyPropertyName(propertyName As String)
    ' Verify that the property name matches a real,  
    ' public, instance property on this object.
    If TypeDescriptor.GetProperties(Me)(propertyName) IsNot Nothing Then
      Return
    End If

    'For Each getProperty As PropertyDescriptor In TypeDescriptor.GetProperties(Me)
    '  Trace.WriteLine("Prop" & getProperty.Name)
    '  OutputDebugString("Prop" & getProperty.Name)
    'Next

        Dim msg As String = "Invalid property name: " & propertyName

    If Me.ThrowOnInvalidPropertyName Then
      Throw New Exception(msg)
    End If


    End Sub

#End Region

End Class
