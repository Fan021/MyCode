Public Class ApplicationProcessModel
    Inherits Kostal.Windows.Presentation.ViewModelBase

    Private ReadOnly _notSelectedText As String
    Private ReadOnly _testApplication As Framework.Base.Components.ITestApplication
    Private ReadOnly _menuitemmodelApplicationProcess As MenuItemModel
    Private ReadOnly _listMyMenuItems As New List(Of MenuItemModel)

    Public Sub New(testApplication As Framework.Base.Components.ITestApplication, menuitemmodelApplicationProcess As MenuItemModel)
        _testApplication = testApplication
        _menuitemmodelApplicationProcess = menuitemmodelApplicationProcess
        If _testApplication Is Nothing Then Return

        _notSelectedText = Me.Localizer.GetLocalizedString("[not selected]")
        ButtonText = _notSelectedText

        AddHandler _testApplication.Process.PropertyChanged, AddressOf TestApplication_Process_PropertyChanged
        TestApplication_Process_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _testApplication.Process.Selected)))
        'ProcessChangeCommand = New Kostal.Windows.Presentation.RelayCommand(New Action(Sub() OnProcessSelectorPressed()))

        AddHandler _testApplication.Process.ProcessAdded, AddressOf TestApplication_Process_ProcessAdded
        For Each itemProcess As Kostal.Testman.Framework.Base.IProcessItem In _testApplication.Process.GetProcesses()
            Dim a As New MenuItemModel(itemProcess.Id, Sub(processId As String) _testApplication.Process.Select(processId))
            a.Visible = ((itemProcess.UserLevel And _testApplication.User.CurrentLevel) = _testApplication.User.CurrentLevel)
            _menuitemmodelApplicationProcess.AddMenuItem(a)
            _listMyMenuItems.Add(a)
        Next

        AddHandler _testApplication.User.PropertyChanged, AddressOf TestApplication_User_PropertyChanged
        TestApplication_User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _testApplication.Process.Selected)))
    End Sub

    Private Sub TestApplication_Process_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
        If Not Me.UiDispatcher.CheckAccess() Then
            Me.UiDispatcher.RunInDispatcherAsync(Sub() TestApplication_Process_PropertyChanged(sender, e))
            Return
        End If

        Select Case e.PropertyName
            Case Member.Of(Function() _testApplication.Process.Selected)
                If _testApplication.Process.Selected Is Nothing Then
                    ButtonText = _notSelectedText
                Else
                    ButtonText = _testApplication.Process.Selected.Id
                End If
            Case Else
                Return
        End Select
    End Sub

    Private Sub TestApplication_User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
        If Not Me.UiDispatcher.CheckAccess() Then
            Me.UiDispatcher.RunInDispatcherAsync(Sub() TestApplication_User_PropertyChanged(sender, e))
            Return
        End If

        For Each menuItemModel As MenuItemModel In _listMyMenuItems
            For Each processItem As Kostal.Testman.Framework.Base.IProcessItem In _testApplication.Process.GetProcesses()
                If menuItemModel.Header = processItem.Id Then
                    menuItemModel.Visible = ((processItem.UserLevel And _testApplication.User.CurrentLevel) = _testApplication.User.CurrentLevel)
                    Exit For
                End If
            Next
        Next
    End Sub

#Region "Properties (and their private members)"

    'Private _commandProcessChange As System.Windows.Input.ICommand
    'Public Property ProcessChangeCommand As System.Windows.Input.ICommand
    '    Get
    '        Return _commandProcessChange
    '    End Get
    '    Set(ByVal value As System.Windows.Input.ICommand)
    '        _commandProcessChange = value
    '        OnPropertyChanged()
    '    End Set
    'End Property

    Private _buttonText As String

    Public Property ButtonText As String
        Get
            Return _buttonText
        End Get
        Private Set(value As String)
            If _buttonText = value Then Return
            _buttonText = value
            OnPropertyChanged()
        End Set
    End Property

    Public Event ProcessSelectorPressed As EventHandler

    'Private Sub OnProcessSelectorPressed()
    '    RaiseEvent ProcessSelectorPressed(Me, New EventArgs())
    'End Sub

#Region "ContextMenu"

    Public ReadOnly Property MenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
        Get
            Return _menuitemmodelApplicationProcess.MenuItems
        End Get
    End Property

#End Region

#End Region

    Private Sub TestApplication_Process_ProcessAdded(sender As Object, e As Kostal.Testman.Framework.Base.ProcessEventArgs)
        If Not Me.UiDispatcher.CheckAccess() Then
            Me.UiDispatcher.RunInDispatcherAsync(Sub() TestApplication_Process_ProcessAdded(sender, e))
            Return
        End If

        Dim a As New MenuItemModel(e.Process.Id, Sub(processId As String) _testApplication.Process.Select(processId))
        a.Visible = ((e.Process.UserLevel And _testApplication.User.CurrentLevel) = _testApplication.User.CurrentLevel)
        _menuitemmodelApplicationProcess.AddMenuItem(a)
        _listMyMenuItems.Add(a)
    End Sub

End Class