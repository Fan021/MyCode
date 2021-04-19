Namespace Design

    Public Class StationDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _modelDesign As DesignModel
        Private ReadOnly _menuitemmodelStationDesign As MenuItemModel

        Private ReadOnly _modelSelector As StationSelectorDesignModel
        Private ReadOnly _modelOverview As StationOverviewDesignModel
        Private ReadOnly _modelDetail As StationDetailDesignModel

        Private ReadOnly _menuitemmodelStateContextMenuOnStationViews As MenuItemModel

        Private ReadOnly _menuitemmodelMessage As MenuItemModel
        Private ReadOnly _menuitemmodelShowColorInMessages As MenuItemModel

        Private ReadOnly _menuitemmodelJob As MenuItemModel
        Private ReadOnly _menuitemmodelJobContextMenuOnStationViews As MenuItemModel

        Public Sub New(modelDesign As DesignModel, menuitemmodelStationDesign As MenuItemModel)
            _modelDesign = modelDesign
            _menuitemmodelStationDesign = menuitemmodelStationDesign

            _modelSelector = New StationSelectorDesignModel(modelDesign, _menuitemmodelStationDesign.AddMenuItem("Selector"))
            _modelOverview = New StationOverviewDesignModel(modelDesign, _menuitemmodelStationDesign.AddMenuItem("Overview"))
            _modelDetail = New StationDetailDesignModel(modelDesign, _menuitemmodelStationDesign.AddMenuItem("Detail"))

            _menuitemmodelMessage = _menuitemmodelStationDesign.AddMenuItem("State/Message")
            _menuitemmodelStateContextMenuOnStationViews = _menuitemmodelMessage.AddMenuItem("Show State Contextmenu on Stationviews", New Action(Sub() ShowContextMenuForStateOnStationViews = ShowContextMenuForStateOnStationViews Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelShowColorInMessages = _menuitemmodelMessage.AddMenuItem("Show Color for Messages in Stationviews", New Action(Sub() ShowColorInMessages = ShowColorInMessages Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelJob = _menuitemmodelStationDesign.AddMenuItem("Job/Serial")
            _menuitemmodelJobContextMenuOnStationViews = _menuitemmodelJob.AddMenuItem("Show Job Contextmenu on Stationviews", New Action(Sub() ShowContextMenuForJobOnStationViews = ShowContextMenuForJobOnStationViews Xor _modelDesign.GetUserLevelVisibility()))

            AddHandler _modelDesign.PropertyChanged, Sub(sender, e)
                                                         If e.PropertyName = Member.Of(Function() _modelDesign.TestApplication) Then
                                                             AddHandler modelDesign.TestApplication.User.PropertyChanged, AddressOf User_PropertyChanged
                                                             User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _modelDesign.TestApplication.User.CurrentLevel)))
                                                         End If
                                                     End Sub
        End Sub


        Private Sub User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            _menuitemmodelStateContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForStateOnStationViews)
            _menuitemmodelShowColorInMessages.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showColorInMessages)
            _menuitemmodelJobContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForJobOnStationViews)
        End Sub

        Public ReadOnly Property Selector As StationSelectorDesignModel
            Get
                Return _modelSelector
            End Get
        End Property

        Public ReadOnly Property Overview As StationOverviewDesignModel
            Get
                Return _modelOverview
            End Get
        End Property

        Public ReadOnly Property Detail As StationDetailDesignModel
            Get
                Return _modelDetail
            End Get
        End Property

#Region "State"

        Private _showContextMenuForStateOnStationViews As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the context menu for states in the station views
        ''' </summary>
        Public Property ShowContextMenuForStateOnStationViews As Visibilities
            Get
                Return _showContextMenuForStateOnStationViews
            End Get
            Set(value As Visibilities)
                If _showContextMenuForStateOnStationViews = value Then Return
                _showContextMenuForStateOnStationViews = value
                OnPropertyChanged()
                _menuitemmodelStateContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForStateOnStationViews)
            End Set
        End Property

#End Region

#Region "StateButtons"

        Private _stateSecondButton As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the second state button in the application state control
        ''' </summary>
        Public Property StateSecondButton As Visibilities
            Get
                Return _stateSecondButton
            End Get
            Set(value As Visibilities)
                If _stateSecondButton = value Then Return
                _stateSecondButton = value
                OnPropertyChanged()
                OnPropertyChanged(Member.Of(Function() StateThirdButton))
            End Set
        End Property

        Private _stateThirdButton As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the third state button in the application state control
        ''' </summary>
        Public Property StateThirdButton As Visibilities
            Get
                Return _stateThirdButton And _stateSecondButton
            End Get
            Set(value As Visibilities)
                If _stateThirdButton = value Then Return
                _stateThirdButton = value
                OnPropertyChanged()
            End Set
        End Property

#End Region

#Region "Message/Prompts"

        Private _showColorInMessages As Visibilities = Visibilities.All

        ''' <summary>
        ''' Gets or sets if the messages in the station views are shown with color
        ''' </summary>
        Public Property ShowColorInMessages As Visibilities
            Get
                Return _showColorInMessages
            End Get
            Set(value As Visibilities)
                If _showColorInMessages = value Then Return
                _showColorInMessages = value
                OnPropertyChanged()
                _menuitemmodelShowColorInMessages.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showColorInMessages)
            End Set
        End Property

        Private _showContextMenuForJobOnStationViews As Visibilities = Visibilities.Developer

        ''' <summary>
        ''' Gets or sets the visibility of the context menu for jobs in the station views
        ''' </summary>
        Public Property ShowContextMenuForJobOnStationViews As Visibilities
            Get
                Return _showContextMenuForJobOnStationViews
            End Get
            Set(value As Visibilities)
                If _showContextMenuForJobOnStationViews = value Then Return
                _showContextMenuForJobOnStationViews = value
                OnPropertyChanged()
                _menuitemmodelJobContextMenuOnStationViews.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_showContextMenuForJobOnStationViews)
            End Set
        End Property

#End Region

    End Class

End Namespace