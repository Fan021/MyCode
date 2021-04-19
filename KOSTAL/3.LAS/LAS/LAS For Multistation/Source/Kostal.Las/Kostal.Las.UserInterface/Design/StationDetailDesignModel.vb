Namespace Design

    Public Class StationDetailDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _modelDesign As DesignModel
        Private ReadOnly _menuitemmodelStationDesign As MenuItemModel

        Private ReadOnly _menuitemmodelPosition As MenuItemModel
        Private ReadOnly _menuitemmodelPositionNone As MenuItemModel
        Private ReadOnly _menuitemmodelPositionLeft As MenuItemModel
        Private ReadOnly _menuitemmodelPositionRight As MenuItemModel

        Private ReadOnly _menuitemmodelState As MenuItemModel

        Private ReadOnly _menuitemmodelMessage As MenuItemModel

        Private ReadOnly _menuitemmodelJob As MenuItemModel

        Private ReadOnly _menuitemmodelProcess As MenuItemModel

        'Private ReadOnly _menuitemmodelResult As MenuItemModel

        Private ReadOnly _menuitemmodelCurrentStep As MenuItemModel

        Private ReadOnly _menuitemmodelFirstFailedStep As MenuItemModel

        Private ReadOnly _menuitemmodelResults As MenuItemModel

        Private ReadOnly _menuitemmodelCounter As MenuItemModel

        Private ReadOnly _menuitemmodelControlButtons As MenuItemModel

        Public Sub New(modelDesign As DesignModel, menuitemmodelStationSelectorDesign As MenuItemModel)
            _modelDesign = modelDesign
            _menuitemmodelStationDesign = menuitemmodelStationSelectorDesign

            _menuitemmodelPosition = _menuitemmodelStationDesign.AddMenuItem("Position View Selector")
            _menuitemmodelPositionNone = _menuitemmodelPosition.AddMenuItem("None", New Action(Sub() ViewSelectorPosition = HorizontalPositions.None))
            _menuitemmodelPositionLeft = _menuitemmodelPosition.AddMenuItem("Left", New Action(Sub() ViewSelectorPosition = HorizontalPositions.Left))
            _menuitemmodelPositionRight = _menuitemmodelPosition.AddMenuItem("Right", New Action(Sub() ViewSelectorPosition = HorizontalPositions.Right))

            _menuitemmodelState = _menuitemmodelStationDesign.AddMenuItem("State (Button)", New Action(Sub() State = State Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelMessage = _menuitemmodelStationDesign.AddMenuItem("State/Message", New Action(Sub() Message = Message Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelJob = _menuitemmodelStationDesign.AddMenuItem("Job/Serial", New Action(Sub() Job = Job Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelProcess = _menuitemmodelStationDesign.AddMenuItem("Process/Article", New Action(Sub() Process = Process Xor _modelDesign.GetUserLevelVisibility()))

            '_menuitemmodelResult = _menuitemmodelStationDesign.AddMenuItem("Result", New Action(Sub() Result = Result Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelCurrentStep = _menuitemmodelStationDesign.AddMenuItem("Current Step", New Action(Sub() CurrentStep = CurrentStep Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelFirstFailedStep = _menuitemmodelStationDesign.AddMenuItem("First Failed Step", New Action(Sub() FirstFailedStep = FirstFailedStep Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelResults = _menuitemmodelStationDesign.AddMenuItem("Results", New Action(Sub() Results = Results Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelCounter = _menuitemmodelStationDesign.AddMenuItem("Counter", New Action(Sub() Counter = Counter Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelControlButtons = _menuitemmodelStationDesign.AddMenuItem("Control Buttons", New Action(Sub() ShowControlButtons = ShowControlButtons Xor _modelDesign.GetUserLevelVisibility()))

            '_modelAttributes = New AttributesDesignModel(Me)

            AddHandler _modelDesign.PropertyChanged, Sub(sender, e)
                                                         If e.PropertyName = Member.Of(Function() _modelDesign.TestApplication) Then
                                                             AddHandler modelDesign.TestApplication.User.PropertyChanged, AddressOf User_PropertyChanged
                                                             User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _modelDesign.TestApplication.User.CurrentLevel)))
                                                         End If
                                                     End Sub
        End Sub


        Private Sub User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)

            _menuitemmodelPositionNone.Checked = CBool(IIf(ViewSelectorPosition = HorizontalPositions.None, True, False))
            _menuitemmodelPositionLeft.Checked = CBool(IIf(ViewSelectorPosition = HorizontalPositions.Left, True, False))
            _menuitemmodelPositionRight.Checked = CBool(IIf(ViewSelectorPosition = HorizontalPositions.Right, True, False))

            _menuitemmodelState.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_stateDetail)

            _menuitemmodelMessage.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_message)

            _menuitemmodelJob.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_job)

            _menuitemmodelProcess.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_process)

            _menuitemmodelCurrentStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_currentStep)

            _menuitemmodelFirstFailedStep.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_firstFailedStep)

            '_menuitemmodelResult.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_result)

            _menuitemmodelResults.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_results)

            _menuitemmodelCounter.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_counter)

            _menuitemmodelControlButtons.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showControlButtons)
        End Sub

#Region "Selector Position (Detail, Grid, pluginviews)"

        Private _viewSelectorPosition As HorizontalPositions = HorizontalPositions.Right

        ''' <summary>
        ''' Gets or sets the visibility and position of the view selector (Details, Grid, Plugin Views) in the station detail view
        ''' </summary>
        Public Property ViewSelectorPosition As HorizontalPositions
            Get
                Return _viewSelectorPosition
            End Get
            Set(value As HorizontalPositions)
                If _viewSelectorPosition = value Then Return
                _viewSelectorPosition = value
                OnPropertyChanged()
                _menuitemmodelPositionNone.Checked = CBool(IIf(value = HorizontalPositions.None, True, False))
                _menuitemmodelPositionLeft.Checked = CBool(IIf(value = HorizontalPositions.Left, True, False))
                _menuitemmodelPositionRight.Checked = CBool(IIf(value = HorizontalPositions.Right, True, False))
            End Set
        End Property

#End Region

#Region "State"

        Private _stateDetail As Visibilities = Visibilities.ServiceAndDeveloper

        ''' <summary>
        ''' Gets or sets the visibility of the statebutton in the station detail view
        ''' </summary>
        Public Property State As Visibilities
            Get
                Return _stateDetail
            End Get
            Set(value As Visibilities)
                If _stateDetail = value Then Return
                _stateDetail = value
                OnPropertyChanged()
                _menuitemmodelState.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_stateDetail)
            End Set
        End Property

#End Region

#Region "State and Message"

        Private _message As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the state and messages controls in the station detail view
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

#End Region

#Region "Job and Serial"

        Private _job As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the job control in the station detail view
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

#End Region

#Region "Process and Article"

        Private _process As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of the process control in the station detail view
        ''' </summary>
        Public Property Process As Visibilities
            Get
                Return _process
            End Get
            Set(value As Visibilities)
                If _process = value Then Return
                _process = value
                OnPropertyChanged()
                _menuitemmodelProcess.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_process)
            End Set
        End Property

#End Region

#Region "Result"

        '        Private _result As Visibilities = Visibilities.All

        '        ''' <summary>
        '        ''' Gets or sets the visibility of the result (runnable) control in the station detail view
        '        ''' </summary>
        '        Public Property Result As Visibilities
        '            Get
        '                Return _result
        '            End Get
        '            Set(value As Visibilities)
        '                If _result = value Then Return
        '                _result = value
        '                OnPropertyChanged()
        '                _menuitemmodelResult.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_result)
        '            End Set
        '        End Property

#End Region

#Region "Current Step"

        Private _currentStep As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets the visibility of current step in the station detail view
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

#Region "Results"

        Private _results As Visibilities = Visibilities.ServiceAndDeveloper

        ''' <summary>
        ''' Gets or sets the visibility of the last results performed on this station area in the station detail view
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
        ''' Gets or sets the visibility of the counter in the station detail view
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

#Region "Control Buttons (Editor, Monitor, ...)"

        Private _showControlButtons As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the Control buttons in the station detail view
        ''' </summary>
        Public Property ShowControlButtons As Visibilities
            Get
                Return _showControlButtons
            End Get
            Set(value As Visibilities)
                If _showControlButtons = value Then Return
                _showControlButtons = value
                OnPropertyChanged()
                _menuitemmodelControlButtons.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showControlButtons)
            End Set
        End Property

#End Region

#Region "StateButtons"

        '        ' Todo 4.0 in dieser Klasse richtig?
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

        '        ' Todo 4.0 in dieser Klasse richtig?
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

#Region "Attributes"

        Private ReadOnly _modelAttributes As AttributesDesignModel

        Public ReadOnly Property Attributes As AttributesDesignModel
            Get
                Return _modelAttributes
            End Get
        End Property

#End Region

    End Class

End Namespace