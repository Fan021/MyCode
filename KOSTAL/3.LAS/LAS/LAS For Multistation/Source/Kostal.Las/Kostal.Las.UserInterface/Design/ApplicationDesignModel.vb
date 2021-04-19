Namespace Design

    Public Class ApplicationDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _modelDesign As DesignModel

        Private ReadOnly _menuitemmodelApplicationDesign As MenuItemModel
        Private ReadOnly _menuitemmodelControlbarOld As MenuItemModel
        Private ReadOnly _menuitemmodelControlbarNew As MenuItemModel

        Private ReadOnly _menuitemmodelSelector As MenuItemModel
        Private ReadOnly _menuitemmodelSelectorNone As MenuItemModel
        Private ReadOnly _menuitemmodelSelectorLeft As MenuItemModel
        Private ReadOnly _menuitemmodelSelectorRight As MenuItemModel

        Private ReadOnly _menuitemmodelButtons As MenuItemModel
        Private ReadOnly _menuitemmodelButtonsNone As MenuItemModel
        Private ReadOnly _menuitemmodelButtonsLeft As MenuItemModel
        Private ReadOnly _menuitemmodelButtonsRight As MenuItemModel

        Private ReadOnly _menuitemmodelCompanyLogo As MenuItemModel
        Private ReadOnly _menuitemmodelCompanyLogoNone As MenuItemModel
        Private ReadOnly _menuitemmodelCompanyLogoLeft As MenuItemModel
        Private ReadOnly _menuitemmodelCompanyLogoRight As MenuItemModel


        Private ReadOnly _menuitemmodelCounter As MenuItemModel
        Private ReadOnly _menuitemmodelStatusbar As MenuItemModel
        Private ReadOnly _menuitemmodelArticleSelector As MenuItemModel
        Private ReadOnly _menuitemmodelOperationSelector As MenuItemModel
        'Private ReadOnly _menuitemmodelArticleClearFunction As MenuItemModel
        Private ReadOnly _menuitemmodelProcessSelector As MenuItemModel

        'Private ReadOnly _menuitemmodelState As MenuItemModel
        'Private ReadOnly _menuitemmodelStateOneButton As MenuItemModel
        'Private ReadOnly _menuitemmodelStateTwoButton As MenuItemModel
        'Private ReadOnly _menuitemmodelStateRight As MenuItemModel

        Public Sub New(modelDesign As DesignModel, menuitemmodelApplicationDesign As MenuItemModel)
            _modelDesign = modelDesign
            _menuitemmodelApplicationDesign = menuitemmodelApplicationDesign

            _menuitemmodelControlbarOld = _menuitemmodelApplicationDesign.AddMenuItem("Controlbar Old", New Action(Sub() ControlbarOld = Not ControlbarOld))
            _menuitemmodelControlbarOld.Visible = False

            _menuitemmodelControlbarNew = _menuitemmodelApplicationDesign.AddMenuItem("Controlbar", New Action(Sub() ControlbarNew = Not ControlbarNew))
            _menuitemmodelControlbarNew.Visible = False

            _menuitemmodelSelector = _menuitemmodelApplicationDesign.AddMenuItem("Station Selector")
            _menuitemmodelSelectorNone = _menuitemmodelSelector.AddMenuItem("None", New Action(Sub() StationSelector = HorizontalPositions.None))
            _menuitemmodelSelectorLeft = _menuitemmodelSelector.AddMenuItem("Left", New Action(Sub() StationSelector = HorizontalPositions.Left))
            _menuitemmodelSelectorRight = _menuitemmodelSelector.AddMenuItem("Right", New Action(Sub() StationSelector = HorizontalPositions.Right))

            _menuitemmodelButtons = _menuitemmodelApplicationDesign.AddMenuItem("Buttons")
            _menuitemmodelButtonsNone = _menuitemmodelButtons.AddMenuItem("None", New Action(Sub() Buttons = HorizontalPositions.None))
            _menuitemmodelButtonsLeft = _menuitemmodelButtons.AddMenuItem("Left", New Action(Sub() Buttons = HorizontalPositions.Left))
            _menuitemmodelButtonsRight = _menuitemmodelButtons.AddMenuItem("Right", New Action(Sub() Buttons = HorizontalPositions.Right))

            _menuitemmodelCompanyLogo = _menuitemmodelApplicationDesign.AddMenuItem("CompanyLogo")
            _menuitemmodelCompanyLogoNone = _menuitemmodelCompanyLogo.AddMenuItem("None", New Action(Sub() CompanyLogo = HorizontalPositions.None))
            _menuitemmodelCompanyLogoLeft = _menuitemmodelCompanyLogo.AddMenuItem("Left", New Action(Sub() CompanyLogo = HorizontalPositions.Left))
            _menuitemmodelCompanyLogoRight = _menuitemmodelCompanyLogo.AddMenuItem("Right", New Action(Sub() CompanyLogo = HorizontalPositions.Right))

            _menuitemmodelCounter = _menuitemmodelApplicationDesign.AddMenuItem("Counter", New Action(Sub() Counter = Counter Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelStatusbar = _menuitemmodelApplicationDesign.AddMenuItem("Statusbar", New Action(Sub() Statusbar = Statusbar Xor _modelDesign.GetUserLevelVisibility()))

            _menuitemmodelOperationSelector = _menuitemmodelApplicationDesign.AddMenuItem("Operation Selector", New Action(Sub() OperationSelector = OperationSelector Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelProcessSelector = _menuitemmodelApplicationDesign.AddMenuItem("Process Selector", New Action(Sub() ProcessSelector = ProcessSelector Xor _modelDesign.GetUserLevelVisibility()))
            _menuitemmodelArticleSelector = _menuitemmodelApplicationDesign.AddMenuItem("Article Selector", New Action(Sub() ArticleSelector = ArticleSelector Xor _modelDesign.GetUserLevelVisibility()))
            '_menuitemmodelArticleClearFunction = _menuitemmodelApplicationDesign.AddMenuItem("Article Clear Function", New Action(Sub() ArticleClearButton = ArticleClearButton Xor _modelDesign.GetUserLevelVisibility()))


            ' Initial positions
            ControlbarOld = False
            ControlbarNew = True
            Counter = Visibilities.All
            Statusbar = Visibilities.None
            StateSecondButton = Visibilities.Developer
            StateThirdButton = Visibilities.Developer
            Buttons = HorizontalPositions.None
            CompanyLogo = HorizontalPositions.None
            StationSelector = HorizontalPositions.Left
            ProcessSelector = Visibilities.All
            ArticleSelector = Visibilities.All
            ArticleClearButton = Visibilities.All
            AddHandler _modelDesign.PropertyChanged, Sub(sender, e)
                                                         If e.PropertyName = Member.Of(Function() _modelDesign.TestApplication) Then
                                                             AddHandler modelDesign.TestApplication.User.PropertyChanged, AddressOf User_PropertyChanged
                                                             User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _modelDesign.TestApplication.User.CurrentLevel)))
                                                         End If
                                                     End Sub
        End Sub

        Private Sub User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            _menuitemmodelOperationSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_operationSelector)
            _menuitemmodelProcessSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_processSelector)
            _menuitemmodelArticleSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_articleSelector)
            _menuitemmodelCounter.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_counter)
            _menuitemmodelStatusbar.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_statusbar)
            '_menuitemmodelArticleClearFunction.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_articleClearButton)
        End Sub

#Region "ControlbarOld"

        Private _controlbarOld As Boolean

        ''' <summary>
        ''' Gets or sets if the old control bar area is visible.
        ''' Normally it should be Not visible
        ''' </summary>
        Public Property ControlbarOld As Boolean
            Get
                Return _controlbarOld
            End Get
            Set(value As Boolean)
                If _controlbarOld = value Then Return
                _controlbarOld = value
                OnPropertyChanged()
                _menuitemmodelControlbarOld.Checked = _controlbarOld
            End Set
        End Property

#End Region

#Region "ControlbarNew"

        Private _controlbarNew As Boolean

        ''' <summary>
        ''' Gets or sets if the control bar area is visible.
        ''' Normally it should be visible
        ''' </summary>
        Public Property ControlbarNew As Boolean
            Get
                Return _controlbarNew
            End Get
            Set(value As Boolean)
                If _controlbarNew = value Then Return
                _controlbarNew = value
                OnPropertyChanged()
                _menuitemmodelControlbarNew.Checked = _controlbarNew
            End Set
        End Property

#End Region

#Region "Counter"

        Private _counter As Visibilities = Nothing

        ''' <summary>
        ''' Get or sets the visibility of the application counter
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

#Region "StatusBar"

        Private _statusbar As Visibilities = Nothing

        ''' <summary>
        ''' Get or sets the visibility of the status bar
        ''' </summary>
        Public Property Statusbar As Visibilities
            Get
                Return _statusbar
            End Get
            Set(value As Visibilities)
                If _statusbar = value Then Return
                _statusbar = value
                OnPropertyChanged()
                _menuitemmodelStatusbar.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_statusbar)
            End Set
        End Property

#End Region

#Region "StateButtons"

        Private _stateSecondButton As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of second state button.
        ''' </summary>
        ''' <value>
        ''' The state second button.
        ''' </value>
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

        Private _stateThirdButton As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of third state button.
        ''' </summary>
        ''' <value>
        ''' The state second button.
        ''' </value>
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

#Region "Buttons"

        Private _buttons As HorizontalPositions = Nothing

        ''' <summary>
        ''' Gets or sets the position and visibility of the buttons for article selection and process selection...
        ''' Normally NOT visible
        ''' </summary>
        Public Property Buttons As HorizontalPositions
            Get
                Return _buttons
            End Get
            Set(value As HorizontalPositions)
                If _buttons = value Then Return
                _buttons = value
                OnPropertyChanged()
                _menuitemmodelButtonsNone.Checked = CBool(IIf(_buttons = HorizontalPositions.None, True, False))
                _menuitemmodelButtonsLeft.Checked = CBool(IIf(_buttons = HorizontalPositions.Left, True, False))
                _menuitemmodelButtonsRight.Checked = CBool(IIf(_buttons = HorizontalPositions.Right, True, False))
            End Set
        End Property

#End Region

#Region "CompanyLogo"

        Private _companyLogo As HorizontalPositions = Nothing
        ''' <summary>
        ''' Gets or sets the visibility of the company logo
        ''' </summary>
        Public Property CompanyLogo As HorizontalPositions
            Get
                Return _companyLogo
            End Get
            Set(value As HorizontalPositions)
                If _companyLogo = value Then Return
                _companyLogo = value
                OnPropertyChanged()
                _menuitemmodelCompanyLogoNone.Checked = CBool(IIf(_companyLogo = HorizontalPositions.None, True, False))
                _menuitemmodelCompanyLogoLeft.Checked = CBool(IIf(_companyLogo = HorizontalPositions.Left, True, False))
                _menuitemmodelCompanyLogoRight.Checked = CBool(IIf(_companyLogo = HorizontalPositions.Right, True, False))
            End Set
        End Property

#End Region

#Region "Station Selector"

        Private _stationSelector As HorizontalPositions = Nothing

        ''' <summary>
        ''' Gets or sets the postion and visibility of the station selector
        ''' </summary>
        Public Property StationSelector As HorizontalPositions
            Get
                Return _stationSelector
            End Get
            Set(value As HorizontalPositions)
                If _stationSelector = value Then Return
                _stationSelector = value
                OnPropertyChanged()
                _menuitemmodelSelectorNone.Checked = CBool(IIf(_stationSelector = HorizontalPositions.None, True, False))
                _menuitemmodelSelectorLeft.Checked = CBool(IIf(_stationSelector = HorizontalPositions.Left, True, False))
                _menuitemmodelSelectorRight.Checked = CBool(IIf(_stationSelector = HorizontalPositions.Right, True, False))
            End Set
        End Property

#End Region

#Region "OperationSelector"

        Private _operationSelector As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of Operation Selector.
        ''' </summary>
        ''' <value>
        ''' The Operation Selector visibilities.
        ''' </value>
        Public Property OperationSelector As Visibilities
            Get
                Return _operationSelector
            End Get
            Set(value As Visibilities)
                If _operationSelector = value Then Return
                _operationSelector = value
                OnPropertyChanged()
                _menuitemmodelOperationSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_operationSelector)
            End Set
        End Property
#End Region

#Region "ProcessSelector"

        Private _processSelector As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of Process Selector.
        ''' </summary>
        ''' <value>
        ''' The Process Selector visibilities.
        ''' </value>
        Public Property ProcessSelector As Visibilities
            Get
                Return _processSelector
            End Get
            Set(value As Visibilities)
                If _processSelector = value Then Return
                _processSelector = value
                OnPropertyChanged()
                _menuitemmodelProcessSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_processSelector)
            End Set
        End Property
#End Region

#Region "ArticleSelector"

        Private _articleSelector As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of Article Selector
        ''' </summary>
        ''' <value>
        ''' The Article Selector visibilities.
        ''' </value>
        Public Property ArticleSelector As Visibilities
            Get
                Return _articleSelector
            End Get
            Set(value As Visibilities)
                If _articleSelector = value Then Return
                _articleSelector = value
                OnPropertyChanged()
                _menuitemmodelArticleSelector.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_articleSelector)
            End Set
        End Property
#End Region

#Region "ArticleClear"

        Private _articleClearButton As Visibilities = Nothing

        ''' <summary>
        ''' Gets or sets the visibility of Article Clear button and also the Menuitem of the Article selector menu.
        ''' </summary>
        ''' <value>
        ''' The Article Clear Button / MenuItem visibilities.
        ''' </value>
        Public Property ArticleClearButton As Visibilities
            Get
                Return _articleClearButton
            End Get
            Set(value As Visibilities)
                If _articleClearButton = value Then Return
                _articleClearButton = value
                OnPropertyChanged()
                '_menuitemmodelArticleClearFunction.Checked = DesignModel.GetVisibilityValidForCurrentUserLevel(_articleClearButton)
            End Set
        End Property
#End Region

    End Class

End Namespace