Namespace Design

    Public Class StationSelectorDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _modelDesign As DesignModel
        Private ReadOnly _menuitemmodelStationDesign As MenuItemModel

        Private ReadOnly _menuitemmodelState As MenuItemModel
        Private ReadOnly _menuitemmodelJob As MenuItemModel
        Private ReadOnly _menuitemmodelResults As MenuItemModel
        Private ReadOnly _menuitemmodelCounter As MenuItemModel

        Public Sub New(modelDesign As DesignModel, menuitemmodelStationSelectorDesign As MenuItemModel)
            _modelDesign = modelDesign
            _menuitemmodelStationDesign = menuitemmodelStationSelectorDesign

            _menuitemmodelState = _menuitemmodelStationDesign.AddMenuItem("State", New Action(Sub() State = State Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelJob = _menuitemmodelStationDesign.AddMenuItem("Job/Result", New Action(Sub() Job = Job Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelResults = _menuitemmodelStationDesign.AddMenuItem("Results", New Action(Sub() Results = Results Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelCounter = _menuitemmodelStationDesign.AddMenuItem("Counter", New Action(Sub() Counter = Counter Xor _modelDesign.GetUserLevelVisibility()))

            AddHandler _modelDesign.PropertyChanged, Sub(sender, e)
                                                         If e.PropertyName = Member.Of(Function() _modelDesign.TestApplication) Then
                                                             AddHandler modelDesign.TestApplication.User.PropertyChanged, AddressOf User_PropertyChanged
                                                             User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _modelDesign.TestApplication.User.CurrentLevel)))
                                                         End If
                                                     End Sub
        End Sub


        Private Sub User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            _menuitemmodelState.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_state)
            _menuitemmodelJob.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_job)
            _menuitemmodelResults.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_results)
            _menuitemmodelCounter.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_counter)
        End Sub

        Private _state As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the statebutton in the station selector
        ''' </summary>
        Public Property State As Visibilities
            Get
                Return _state
            End Get
            Set(value As Visibilities)
                If _state = value Then Return
                _state = value
                OnPropertyChanged()
                _menuitemmodelState.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_state)
            End Set
        End Property

        Private _job As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the job information item in the station selector
        ''' </summary>
        Public Property Job As Visibilities
            Get
                Return _job
            End Get
            Set(value As Visibilities)
                If _job = value Then Return
                _job = value
                OnPropertyChanged()
                _menuitemmodelJob.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_job)
            End Set
        End Property

        Private _results As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the runnables information in the station selector
        ''' </summary>
        Public Property Results As Visibilities
            Get
                Return _results
            End Get
            Set(value As Visibilities)
                If _results = value Then Return
                _results = value
                OnPropertyChanged()
                _menuitemmodelResults.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_results)
            End Set
        End Property

        Private _counter As Visibilities = Visibilities.ServiceAndDeveloper

        ''' <summary>
        ''' Gets or sets the visibility of the counterin the station selector
        ''' </summary>
        Public Property Counter As Visibilities
            Get
                Return _counter
            End Get
            Set(value As Visibilities)
                If _counter = value Then Return
                _counter = value
                OnPropertyChanged()
                _menuitemmodelCounter.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_counter)
            End Set
        End Property

    End Class

End Namespace