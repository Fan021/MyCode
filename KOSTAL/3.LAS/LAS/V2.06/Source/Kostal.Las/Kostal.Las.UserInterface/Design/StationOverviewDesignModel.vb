Namespace Design

    Public Class StationOverviewDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _modelDesign As DesignModel
        Private ReadOnly _menuitemmodelStationDesign As MenuItemModel


        Private ReadOnly _menuitemmodelState As MenuItemModel
        'Private ReadOnly _menuitemmodelStateContextMenuOnStationViews As MenuItemModel
        'Private ReadOnly _menuitemmodelStateBigSpecial As MenuItemModel

        Private ReadOnly _menuitemmodelMessage As MenuItemModel
        'Private ReadOnly _menuitemmodelShowColorInMessages As MenuItemModel

        Private ReadOnly _menuitemmodelJob As MenuItemModel
        'Private ReadOnly _menuitemmodelJobContextMenuOnStationViews As MenuItemModel

        Private ReadOnly _menuitemmodelProcess As MenuItemModel

        Private ReadOnly _menuitemmodelCurrentStep As MenuItemModel

        Private ReadOnly _menuitemmodelFirstFailedStep As MenuItemModel

        'Private ReadOnly _menuitemmodelResult As MenuItemModel

        Private ReadOnly _menuitemmodelResults As MenuItemModel

        Private ReadOnly _menuitemmodelCounter As MenuItemModel

        'Private ReadOnly _menuitemmodelMiscellaneous As MenuItemModel
        'Private ReadOnly _menuitemmodelMiscControlButtons As MenuItemModel

        Public Sub New(modelDesign As DesignModel, menuitemmodelStationOverviewDesign As MenuItemModel)
            _modelDesign = modelDesign
            _menuitemmodelStationDesign = menuitemmodelStationOverviewDesign

            _menuitemmodelState = _menuitemmodelStationDesign.AddMenuItem("State (Button)", New Action(Sub() State = State Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelMessage = _menuitemmodelStationDesign.AddMenuItem("State/Message", New Action(Sub() Message = Message Xor _modelDesign.GetUserLevelVisibility()))
            '_menuitemmodelStateContextMenuOnStationViews = _menuitemmodelMessage.AddMenuItem("Show State Contextmenu on Stationviews", New Action(Sub() ShowContextMenuForStateOnStationViews = ShowContextMenuForStateOnStationViews Xor _modelDesign.GetUserLevelVisibility()))
            '_menuitemmodelShowColorInMessages = _menuitemmodelMessage.AddMenuItem("Show Color for Messages in Stationviews", New Action(Sub() ShowColorInMessages = ShowColorInMessages Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelJob = _menuitemmodelStationDesign.AddMenuItem("Job/Serial", New Action(Sub() Job = Job Xor _modelDesign.GetUserLevelVisibility()))
            '_menuitemmodelJobContextMenuOnStationViews = _menuitemmodelJob.AddMenuItem("Show Job Contextmenu on Stationviews", New Action(Sub() ShowContextMenuForJobOnStationViews = ShowContextMenuForJobOnStationViews Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelProcess = _menuitemmodelStationDesign.AddMenuItem("Process/Article", New Action(Sub() Process = Process Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelCurrentStep = _menuitemmodelStationDesign.AddMenuItem("Current Step", New Action(Sub() CurrentStep = CurrentStep Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelFirstFailedStep = _menuitemmodelStationDesign.AddMenuItem("First Failed Step", New Action(Sub() FirstFailedStep = FirstFailedStep Xor _modelDesign.GetUserLevelVisibility()))

            '_menuitemmodelResult = _menuitemmodelStationDesign.AddMenuItem("Result", New Action(Sub() Result = Result Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelResults = _menuitemmodelStationDesign.AddMenuItem("Results", New Action(Sub() Results = Results Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelCounter = _menuitemmodelStationDesign.AddMenuItem("Counter", New Action(Sub() Counter = Counter Xor _modelDesign.GetUserLevelVisibility()))

            '_menuitemmodelMiscellaneous = _menuitemmodelStationDesign.AddMenuItem("Miscellaneous")
            '_menuitemmodelMiscControlButtons = _menuitemmodelMiscellaneous.AddMenuItem("Show Control Button on Detail", New Action(Sub() ShowControlButtons = ShowControlButtons Xor _modelDesign.GetUserLevelVisibility()))


            AddHandler _modelDesign.PropertyChanged, Sub(sender, e)
                                                         If e.PropertyName = Member.Of(Function() _modelDesign.TestApplication) Then
                                                             AddHandler modelDesign.TestApplication.User.PropertyChanged, AddressOf User_PropertyChanged
                                                             User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _modelDesign.TestApplication.User.CurrentLevel)))
                                                         End If
                                                     End Sub
        End Sub


        Private Sub User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            _menuitemmodelState.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_state)
            '_menuitemmodelStateContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForStateOnStationViews)
            _menuitemmodelMessage.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_message)
            '_menuitemmodelShowColorInMessages.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showColorInMessages)

            _menuitemmodelJob.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_job)
            '_menuitemmodelJobContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForJobOnStationViews)

            _menuitemmodelProcess.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_process)
            _menuitemmodelCurrentStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_currentStep)
            _menuitemmodelFirstFailedStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_firstFailedStep)
            '_menuitemmodelResult.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_result)
            _menuitemmodelResults.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_results)
            _menuitemmodelCounter.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_counter)

            '_menuitemmodelMiscControlButtons.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showControlButtons)
        End Sub

#Region "State"
        Private _state As Visibilities = Visibilities.ServiceAndDeveloper

        ''' <summary>
        ''' Gets or sets the visibility of the statebutton in the station overview
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

        'Private _showContextMenuForStateOnStationViews As Visibilities = Visibilities.Developer

        ' ''' <summary>
        ' ''' Gets or sets the visibility of the context menu for states in the station views
        ' ''' </summary>
        'Public Property ShowContextMenuForStateOnStationViews As Visibilities
        '    Get
        '        Return _showContextMenuForStateOnStationViews
        '    End Get
        '    Set(value As Visibilities)
        '        If _showContextMenuForStateOnStationViews = value Then Return
        '        _showContextMenuForStateOnStationViews = value
        '        OnPropertyChanged()
        '        _menuitemmodelStateContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForStateOnStationViews)
        '    End Set
        'End Property
#End Region

#Region "StateButtons"

        '        Private _stateSecondButton As Visibilities = Visibilities.Developer

        '        ''' <summary>
        '        ''' Gets or sets the visibility of the second state button in the application state control
        '        ''' </summary>
        '        Public Property StateSecondButton As Visibilities
        '            Get
        '                Return _stateSecondButton
        '            End Get
        '            Set(value As Visibilities)
        '                If _stateSecondButton = value Then Return
        '                _stateSecondButton = value
        '                OnPropertyChanged()
        '                OnPropertyChanged(Member.Of(Function() StateThirdButton))
        '            End Set
        '        End Property

        '        Private _stateThirdButton As Visibilities = Visibilities.Developer

        '        ''' <summary>
        '        ''' Gets or sets the visibility of the third state button in the application state control
        '        ''' </summary>
        '        Public Property StateThirdButton As Visibilities
        '            Get
        '                Return _stateThirdButton And _stateSecondButton
        '            End Get
        '            Set(value As Visibilities)
        '                If _stateThirdButton = value Then Return
        '                _stateThirdButton = value
        '                OnPropertyChanged()
        '            End Set
        '        End Property

#End Region

#Region "Message"
        Private _message As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the state and messages controls in the station overview
        ''' </summary>
        Public Property Message As Visibilities
            Get
                Return _message
            End Get
            Set(value As Visibilities)
                If _message = value Then Return
                _message = value
                OnPropertyChanged()
                _menuitemmodelMessage.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_message)
            End Set
        End Property

        'Private _showColorInMessages As Visibilities = Visibilities.All

        ' ''' <summary>
        ' ''' Gets or sets if the messages in the station detail view are shown with color
        ' ''' </summary>
        'Public Property ShowColorInMessages As Visibilities
        '    Get
        '        Return _showColorInMessages
        '    End Get
        '    Set(value As Visibilities)
        '        If _showColorInMessages = value Then Return
        '        _showColorInMessages = value
        '        OnPropertyChanged()
        '        _menuitemmodelShowColorInMessages.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showColorInMessages)
        '    End Set
        'End Property
#End Region

#Region "Job"
        Private _job As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the job control in the station overview
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

        'Private _showContextMenuForJobOnStationViews As Visibilities = Visibilities.Developer

        ' ''' <summary>
        ' ''' Gets or sets the visibility of the context menu for jobs in the station views
        ' ''' </summary>
        'Public Property ShowContextMenuForJobOnStationViews As Visibilities
        '    Get
        '        Return _showContextMenuForJobOnStationViews
        '    End Get
        '    Set(value As Visibilities)
        '        If _showContextMenuForJobOnStationViews = value Then Return
        '        _showContextMenuForJobOnStationViews = value
        '        OnPropertyChanged()
        '        _menuitemmodelJobContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForJobOnStationViews)
        '    End Set
        'End Property
#End Region

#Region "Process"
        Private _process As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the process control in the station overview
        ''' </summary>
        Public Property Process As Visibilities
            Get
                Return _process
            End Get
            Set(value As Visibilities)
                If _process = value Then Return
                _process = value
                OnPropertyChanged()
                _menuitemmodelJob.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_process)
            End Set
        End Property
#End Region

#Region "Current Step"
        Private _currentStep As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of current step in the overview detail view
        ''' </summary>
        Public Property CurrentStep As Visibilities
            Get
                Return _currentStep
            End Get
            Set(value As Visibilities)
                If _currentStep = value Then Return
                _currentStep = value
                OnPropertyChanged()
                _menuitemmodelCurrentStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_currentStep)
            End Set
        End Property
#End Region

#Region "First Failed Step"
        Private _firstFailedStep As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of first failed step in the station detail view
        ''' </summary>
        Public Property FirstFailedStep As Visibilities
            Get
                Return _firstFailedStep
            End Get
            Set(value As Visibilities)
                If _firstFailedStep = value Then Return
                _firstFailedStep = value
                OnPropertyChanged()
                _menuitemmodelFirstFailedStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_firstFailedStep)
            End Set
        End Property
#End Region

#Region "Result"
        'Private _result As Visibilities = Visibilities.Developer

        '''' <summary>
        '''' Gets or sets the visibility of the runnable control in the station overview
        '''' </summary>
        'Public Property Result As Visibilities
        '    Get
        '        Return _result
        '    End Get
        '    Set(value As Visibilities)
        '        If _result = value Then Return
        '        _result = value
        '        OnPropertyChanged()
        '        _menuitemmodelResult.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_result)
        '    End Set
        'End Property
#End Region

#Region "Results"
        Private _results As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the runnables control in the station overview
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
#End Region

#Region "Counter"
        Private _counter As Visibilities = Visibilities.ServiceAndDeveloper

        ''' <summary>
        ''' Gets or sets the visibility of the counter in the station overview
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
#End Region

#Region "Miscellaneous"
        'Private _showControlButtons As Visibilities = Visibilities.Developer

        ' ''' <summary>
        ' ''' Gets or sets the visibility of the counter in the station detail view
        ' ''' </summary>
        'Public Property ShowControlButtons As Visibilities
        '    Get
        '        Return _showControlButtons
        '    End Get
        '    Set(value As Visibilities)
        '        If _showControlButtons = value Then Return
        '        _showControlButtons = value
        '        OnPropertyChanged()
        '        _menuitemmodelMiscControlButtons.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showControlButtons)
        '    End Set
        'End Property
#End Region

    End Class

End Namespace