Public MustInherit Class ViewModelBase
    Inherits NotifyingObject
    Implements IViewModelBase

#Region "Constructor"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ViewModelBase"/> class.
    ''' The uiDispatcher will be initialized with the CurrentDispatcher Thread
    ''' If you did not create this class from the main (UI) thread you get an exeption
    ''' </summary>
    ''' <exception cref="System.NotSupportedException">Creating a ViewModelBase class is not allowed, if not done via UI-thread.</exception>
    Protected Sub New()
        MyClass.New(False)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ViewModelBase"/> class.
    ''' The uiDispatcher will be initialized with the CurrentDispatcher Thread
    ''' </summary>
    ''' <param name="preventCrossThreadCheck">if set to <c>true</c> there is no check if this class is created from a UI-thread.</param>
    ''' <exception cref="System.NotSupportedException">Creating a ViewModelBase class is not allowed, if not done via UI-thread.</exception>
    Protected Sub New(preventCrossThreadCheck As Boolean)
        _uiDispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher
        If preventCrossThreadCheck AndAlso _uiDispatcher.Thread.ManagedThreadId <> 1 Then
            Throw New System.NotSupportedException("Creating a ViewModelBase class is not allowed, if not done via UI-thread.")
        End If
        'AddHandler Utilities.Instance.SelectedCultureChanged, AddressOf GlobalizationUtilities_TheInstance_SelectedCultureChanged
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ViewModelBase"/> class.
    ''' </summary>
    ''' <param name="uiDispatcher">The UI dispatcher.</param>
    Protected Sub New(uiDispatcher As System.Windows.Threading.Dispatcher)
        _uiDispatcher = uiDispatcher
    End Sub

#End Region ' Constructor

#Region "Localizer"

    Private _localizer As Kostal.Globalization.Localizer = Nothing
    ''' <summary>
    ''' Gets the localizer of this viewmodel
    ''' </summary>
    Protected ReadOnly Property Localizer As Kostal.Globalization.Localizer Implements IViewModelBase.Localizer
        Get
            If _localizer Is Nothing Then _localizer = New Kostal.Globalization.Localizer(Me)
            Return _localizer
        End Get
    End Property

#End Region

#Region "UIDispatcher"

    Private ReadOnly _uiDispatcher As System.Windows.Threading.Dispatcher
    ''' <summary>
    ''' Gets the UI Dispatcher of the viewmodel
    ''' </summary>
    Public ReadOnly Property UiDispatcher As System.Windows.Threading.Dispatcher Implements IViewModelBase.UiDispatcher
        Get
            Return _uiDispatcher
        End Get
    End Property

#End Region ' UIDispatcher

#Region "DisplayName"

    Private _displayName As String

    ''' <summary>
    ''' Returns the user-friendly name of this object.
    ''' Child classes can set this property to a new value,
    ''' or override it to determine the value on-demand.
    ''' </summary>
    Public Overridable Property DisplayName As String Implements IViewModelBase.DisplayName
        Get
            Return _displayName
        End Get
        Protected Set(ByVal value As String)
            _displayName = value
            OnPropertyChanged()
        End Set
    End Property


#End Region ' DisplayName

#Region "ToString"
    ''' <summary>
    ''' Returns a string that represents the current object.
    ''' </summary>
    ''' <returns>
    ''' A string that represents the current object.
    ''' </returns>
    ''' <filterpriority>2</filterpriority>
    Public Overrides Function ToString() As String
        If Me.DisplayName.IsNullOrEmpty() Then
            Return MyBase.ToString()
        Else
            Return String.Format("Viewmodel: {0} Type: {1}", Me.DisplayName, Me.GetType().ToString())
        End If
    End Function
#End Region

End Class