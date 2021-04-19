Imports System.Collections.ObjectModel
Imports Kostal.Windows.Presentation

Public Class MenuItemModel
    Inherits ViewModelBaseDisposable

    Private _text As String = System.String.Empty
    Private _visible As Boolean = True
    Private _checked As Boolean = False
    Private _enabled As Boolean = True
    Private ReadOnly _isSeparator As Boolean = False
    Private ReadOnly _object As Object
    Private ReadOnly _action0 As Action = Nothing
    Private ReadOnly _action1 As Action(Of String) = Nothing
    Private ReadOnly _action2 As Action(Of Object) = Nothing
    Private ReadOnly _canExecute As Func(Of Object, Boolean) = Nothing
    Private ReadOnly _command As System.Windows.Input.ICommand
    'Private ReadOnly _collectionChildMenuItems As New List(Of MenuItemModel)
    Private ReadOnly _observableCollectionChildMenuItems As New ObjectModel.ObservableCollection(Of MenuItemModel)
    Private ReadOnly _readOnlyObservableCollectionChildMenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)

    Private Sub New()
        MyBase.New()
        Dim filterList As List(Of Predicate(Of MenuItemModel)) = New List(Of Predicate(Of MenuItemModel))
        filterList.Add(New Predicate(Of MenuItemModel)(Function(menuItemModel)
                                                           Try
                                                               Return menuItemModel Is Nothing OrElse menuItemModel.Visible
                                                           Catch ex As Exception
                                                               OutputDebugStringConditionalDebug(ex.ToString())
                                                           End Try
                                                           Return True
                                                       End Function))
        _readOnlyObservableCollectionChildMenuItems = New ReadOnlyObservableCollectionEx(Of MenuItemModel)(_observableCollectionChildMenuItems, filterList)
    End Sub

    Private Sub New(isSeparator As Boolean)
        Me.New()

        If isSeparator Then
            _isSeparator = True
            Return
        End If
        Throw New ArgumentException("Only True is allowed to set a MenuItem as separator.")
    End Sub

    Public Sub New(text As String)
        Me.New()
        _text = text
        _command = New RelayCommand(AddressOf Execute, _canExecute)
    End Sub

    Public Sub New(text As String, action0 As Action)
        Me.New(text, action0, Nothing)
    End Sub
    Public Sub New(text As String, command As System.Windows.Input.ICommand)
        Me.New()
        _text = text
        _command = command
    End Sub
    Public Sub New(text As String, command As System.Windows.Input.ICommand, o As Object)
        Me.New()
        _text = text
        _command = command
        _object = o
    End Sub
    Public Sub New(text As String, action0 As Action, canExecute As Func(Of Object, Boolean))
        Me.New()
        _text = text
        _action0 = action0
        _canExecute = canExecute
        _command = New RelayCommand(AddressOf Execute, _canExecute)
    End Sub
    Public Sub New(text As String, action1 As Action(Of String))
        Me.New(text, action1, Nothing)
    End Sub

    Public Sub New(text As String, action1 As Action(Of String), canExecute As Func(Of Object, Boolean))
        Me.New()
        _text = text
        _action1 = action1
        _canExecute = canExecute
        _command = New RelayCommand(AddressOf Execute, _canExecute)
    End Sub

    Public Sub New(text As String, action2 As Action(Of Object), o As Object)
        Me.New(text, action2, o, Nothing)
    End Sub

    Public Sub New(text As String, action2 As Action(Of Object), o As Object, canExecute As Func(Of Object, Boolean))
        Me.New()
        _text = text
        _action2 = action2
        _object = o
        _canExecute = canExecute
        _command = New RelayCommand(AddressOf Execute, _canExecute)
    End Sub

    Public Property Header As String
        Get
            Return _text
        End Get
        Set(value As String)
            If _text = value Then Return
            _text = value
            OnPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Gets the menu items that are Visible = True
    ''' </summary>
    ''' <value>
    ''' The visible menu items.
    ''' </value>
    Public ReadOnly Property MenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
        Get
            Return _readOnlyObservableCollectionChildMenuItems
        End Get
    End Property

    ''' <summary>
    ''' Gets all menu items. Also the Visible = False Items
    ''' </summary>
    ''' <value>
    ''' All menu items.
    ''' </value>
    Public ReadOnly Property AllMenuItems As IReadOnlyList(Of MenuItemModel)
        Get
            Return New ReadOnlyCollection(Of MenuItemModel)(_observableCollectionChildMenuItems)
        End Get
    End Property


    Public ReadOnly Property Command As System.Windows.Input.ICommand
        Get
            Return _command
        End Get
    End Property

    Public Property Visible As Boolean
        Get
            Return _visible
        End Get
        Set(value As Boolean)
            If _visible = value Then Return
            _visible = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Checked As Boolean
        Get
            Return _checked
        End Get
        Set(value As Boolean)
            If _checked = value Then Return
            _checked = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return _enabled
        End Get
        Set(value As Boolean)
            If _enabled = value Then Return
            _enabled = value
            OnPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property InternalObject As Object
        Get
            Return _object
        End Get
    End Property

    Public ReadOnly Property IsSeparator As Boolean
        Get
            Return _isSeparator
        End Get
    End Property

    Private Sub Execute()
        If _action0 IsNot Nothing Then _action0() '.Windows.MessageBox.Show("Clicked at " + Header)
        If _action1 IsNot Nothing Then _action1(_text) '.Windows.MessageBox.Show("Clicked at " + Header)
        If _action2 IsNot Nothing Then
            If _object Is Nothing Then
                _action2(Me) '.Windows.MessageBox.Show("Clicked at " + Header)
            Else
                _action2(_object) '.Windows.MessageBox.Show("Clicked at " + Header)
            End If
        End If
    End Sub

    Public Function AddMenuItem(text As String) As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(text)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Public Function AddMenuItem(text As String, myCommand As System.Windows.Input.ICommand) As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(text, myCommand)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Public Function AddMenuItem(text As String, myCommand As System.Windows.Input.ICommand, o As Object) As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(text, myCommand, o)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Public Function AddMenuItem(text As String, action As Action) As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(text, action)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Public Function AddMenuItem(text As String, action As Action, canExecute As Func(Of Object, Boolean)) As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(text, action, canExecute)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Public Function AddMenuItem(modelMenuItem As MenuItemModel) As MenuItemModel
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function
    Private Sub AddMenuItemHelper(modelMenuItem As MenuItemModel)
        AddHandler modelMenuItem.PropertyChanged, AddressOf MenuItemModel_PropertyChanged
        _observableCollectionChildMenuItems.Add(modelMenuItem)
    End Sub

    Public Function AddSeparator() As MenuItemModel
        Dim modelMenuItem As New MenuItemModel(True)
        AddMenuItemHelper(modelMenuItem)
        Return modelMenuItem
    End Function

    Private Sub MenuItemModel_PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
        If e.PropertyName <> Member.Of(Function() Me.Visible) Then Return

        _readOnlyObservableCollectionChildMenuItems.UpdateFilteringAndSorting()
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0} - Text = {1} - Visible = {2}, ChildItemCount = {3} - InternalObject = {4}", MyBase.ToString(), _text, Me._visible, Me.AllMenuItems.Count, Me.InternalObject)
    End Function

    ''' <summary>
    ''' Child classes can override this method to perform 
    ''' clean-up logic, such as removing event handlers.
    ''' </summary>
    Protected Overrides Sub Dispose(ByVal disposingFromCode As Boolean)
        If Not Disposed Then
            If disposingFromCode Then
                For Each item As MenuItemModel In _observableCollectionChildMenuItems
                    RemoveHandler item.PropertyChanged, AddressOf MenuItemModel_PropertyChanged
                Next
            End If
            ' Free your own state (unmanaged objects).
            ' Set large fields to null.
        End If

        MyBase.Dispose(disposingFromCode)
    End Sub


End Class


'Public Class CommandViewModel
'    Implements System.Windows.Input.ICommand

'    Private ReadOnly _action As Action
'    Private ReadOnly _canExecuteFunc As Func(Of Object, Boolean)

'    Public Sub New(action As Action, canExecuteFunc As Func(Of Object, Boolean))
'        _action = action
'        _canExecuteFunc = canExecuteFunc
'    End Sub

'    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
'        If _canExecuteFunc Is Nothing Then Return True

'        Return _canExecuteFunc(parameter)
'    End Function

'    Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
'        If _action IsNot Nothing Then _action()
'    End Sub

'    Public Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged

'End Class