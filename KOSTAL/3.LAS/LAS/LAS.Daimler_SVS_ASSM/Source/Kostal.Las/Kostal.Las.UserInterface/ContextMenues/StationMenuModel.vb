Namespace Station

    Public Class StationMenuModel
        Inherits ViewModelBase

        Private _testStation As Kostal.Testman.Framework.Base.Components.ITestStation
        Private _modelMenuItem As MenuItemModel

        Public Sub New(testStation As Kostal.Testman.Framework.Base.Components.ITestStation)
            _testStation = testStation
            AddHandler _testStation.StationState.PropertyChanged, AddressOf TestStation_StationState_PropertyChanged
            TestStation_StationState_PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("Value"))
            _modelMenuItem = New MenuItemModel(_testStation.Text)


            Dim menuitemStationState As MenuItemModel = _modelMenuItem.AddMenuItem("State", Nothing)
            menuitemStationState.AddMenuItem("Turn To Previous State", New Action(Sub() _testStation.StationState.TurnToPrev()))
            menuitemStationState.AddMenuItem("Turn To Next State", New Action(Sub() _testStation.StationState.TurnToNext()))
            menuitemStationState.AddSeparator()
            menuitemStationState.AddMenuItem("Disable/Enable", New Action(Sub()
                                                                              If _testStation.StationState.Value > Kostal.Testman.Framework.Base.IStationStateManager.StationStates.IgnoreNext Then
                                                                                  _testStation.StationState.SetDisabled(False)
                                                                              Else
                                                                                  _testStation.StationState.SetEnabled()
                                                                              End If
                                                                          End Sub))
        End Sub

        Public ReadOnly Property MenuItemModel As MenuItemModel
            Get
                Return _modelMenuItem
            End Get
        End Property

        Private Sub TestStation_StationState_PropertyChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "Value"
                    Select Case _testStation.StationState.Value
                        Case Kostal.Testman.Framework.Base.IStationStateManager.StationStates.Run

                    End Select
            End Select
        End Sub

#Region "Properties (and their private members)"

#End Region

#Region "State-Actions"

#End Region

    End Class

End Namespace