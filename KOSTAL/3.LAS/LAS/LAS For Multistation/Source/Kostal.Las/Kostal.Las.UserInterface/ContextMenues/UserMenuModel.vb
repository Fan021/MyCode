Public Class UserMenuModel
    Inherits ViewModelBase

    ''Private _testApplication As Components.ITestApplication
    Private _modelMenuItem As MenuItemModel

    ''Public Sub New(testApplication As Components.ITestApplication)
    ''    _testApplication = testApplication

    ''    _modelMenuItem = New MenuItemModel("User")
    ''    _modelMenuItem.AddMenuItem("Turn To Operator", New Action(Sub() Return))
    ''    _modelMenuItem.AddMenuItem("Turn To Service", New Action(Sub() Return))
    ''    _modelMenuItem.AddMenuItem("Turn To Developer", New Action(Sub() Return))

    ''    AddHandler _testApplication.User.PropertyChanged, AddressOf TestApplication_User_PropertyChanged
    ''    TestApplication_User_PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Value"))
    ''End Sub

    Public ReadOnly Property MenuItemModel As MenuItemModel
        Get
            Return _modelMenuItem
        End Get
    End Property

    Private Sub TestApplication_User_PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
        Select Case e.PropertyName
            Case "Value"

        End Select
    End Sub

#Region "Properties (and their private members)"

#End Region

#Region "State-Actions"

#End Region

End Class