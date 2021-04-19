Public Class ApplicationMenuModel
    Inherits ViewModelBase

    Private ReadOnly _testApplication As Framework.Base.Components.ITestApplication
    Private ReadOnly _collectionMenuItems As New ObjectModel.ObservableCollection(Of MenuItemModel)

    Public ReadOnly Property MenuItems As ObjectModel.ObservableCollection(Of MenuItemModel)
        Get
            Return _collectionMenuItems
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(testApplication As Framework.Base.Components.ITestApplication)
        _testApplication = testApplication
        If _testApplication IsNot Nothing Then
            'AddHandler _testApplication.ApplicationCounter.PropertyChanged, AddressOf TestApplication_ApplicationCounter_PropertyChanged
            'TestApplication_ApplicationCounter_PropertyChanged(Nothing, New ComponentModel.PropertyChangedEventArgs("Success"))
            TestApplication_ApplicationCounter_PropertyChanged(Nothing, New ComponentModel.PropertyChangedEventArgs("Fail"))
        End If

        Dim mi12 As New MenuItemModel("Design")
        MenuItems.Add(mi12)

        Dim menuitemApplicationState As New MenuItemModel("State")
        menuitemApplicationState.AddMenuItem("Turn To Previous State", New Action(Sub() _testApplication.ApplicationState.TurnToPrev()))
        menuitemApplicationState.AddMenuItem("Turn To Next State", New Action(Sub() _testApplication.ApplicationState.TurnToNext()))
        MenuItems.Add(menuitemApplicationState)

        Dim modelUserMenu As New UserMenuModel(testApplication)
        MenuItems.Add(modelUserMenu.MenuItemModel)

        MenuItems.Add(Nothing)
        For Each testSystem As Kostal.Testman.Framework.Base.Components.ITestSystem In _testApplication.TestSystems
            Dim modelSystemMenu As New Station.StationMenuModel(testSystem)
            MenuItems.Add(modelSystemMenu.MenuItemModel)
            For Each testStation As Kostal.Testman.Framework.Base.Components.ITestStation In testSystem.TestStations
                Dim modelStationMenu As New Station.StationMenuModel(testStation)
                MenuItems.Add(modelStationMenu.MenuItemModel)
            Next
        Next
        MenuItems.Add(Nothing)

        Dim menuitemApplicationExit As New MenuItemModel("Exit")
        MenuItems.Add(menuitemApplicationExit)
    End Sub


#Region "Properties (and their private members)"

#End Region

    Private Sub TestApplication_ApplicationCounter_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
        Select Case e.PropertyName
            Case "Success"
            Case "Fail"
            Case Else
                Return
        End Select
    End Sub

End Class