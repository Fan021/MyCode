Namespace Design

    Public Class ProgramDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private ReadOnly _menuitemmodelDesign As MenuItemModel

        Private ReadOnly _menuitemmodelApplicationOptimizeForTouch As MenuItemModel

        Private ReadOnly _windowsApplication As WindowsApplication

        Public Sub New(menuitemmodelDesign As MenuItemModel, windowsApplication As WindowsApplication)
            _menuitemmodelDesign = menuitemmodelDesign
            _windowsApplication = windowsApplication

            _menuitemmodelApplicationOptimizeForTouch = _menuitemmodelDesign.AddMenuItem("Optimize For Touch", New Action(Sub() OptimizeForTouch = Not OptimizeForTouch))

            Me.ContextMenuItemFontSizeForTouch = 24
            Me.ContextMenuItemMarginForTouch = 5    ' Use setter of property to initialize _contextMenuItemMarginThickNessForTouch

            Me.AppearanceMenuItem = Visibilities.Developer
            Me.DesignMenuItem = Visibilities.All

        End Sub

#Region "OptimizeForTouch"

        Private _optimizeForTouch As Boolean
        Private _contextMenuItemFontSizeForTouch As Double
        Private _contextMenuItemMarginForTouch As Integer
        Private _contextMenuItemMarginThickNessForTouch As System.Windows.Thickness

        ''' <summary>
        ''' If set to <c>True</c> the design will be optimized for touch screen use. 
        ''' The contextmenu items will be bigger and the distance between the items will be increased
        ''' </summary>
        Public Property OptimizeForTouch As Boolean
            Get
                Return _optimizeForTouch
            End Get
            Set(value As Boolean)
                If _optimizeForTouch = value Then Return
                _optimizeForTouch = value
                OnPropertyChanged()
                _menuitemmodelApplicationOptimizeForTouch.Checked = _optimizeForTouch
                SetWindowApplicationResourcesForTouch()
            End Set
        End Property

        ''' <summary>
        ''' Gets or the sets the top and bottom margin of all context menu items.
        ''' Default is 5
        ''' </summary>
        Public Property ContextMenuItemMarginForTouch As Integer
            Get
                Return _contextMenuItemMarginForTouch
            End Get
            Set(value As Integer)
                If _contextMenuItemMarginForTouch = value Then Return
                _contextMenuItemMarginForTouch = value
                _contextMenuItemMarginThickNessForTouch = New System.Windows.Thickness(0, value, 0, value)
                OnPropertyChanged()

                SetWindowApplicationResourcesForTouch()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the font size of all context menu items.
        ''' Default is 24
        ''' </summary>
        Public Property ContextMenuItemFontSizeForTouch As Double
            Get
                Return _contextMenuItemFontSizeForTouch
            End Get
            Set(value As Double)
                If _contextMenuItemFontSizeForTouch = value Then Return
                _contextMenuItemFontSizeForTouch = value
                OnPropertyChanged()

                SetWindowApplicationResourcesForTouch()
            End Set
        End Property

        Private Sub SetWindowApplicationResourcesForTouch()
            If Me.OptimizeForTouch Then
                _windowsApplication.Resources("ContextMenuFontSize") = Me.ContextMenuItemFontSizeForTouch
                _windowsApplication.Resources("ContextMenuItemMargin") = Me._contextMenuItemMarginThickNessForTouch
            Else
                _windowsApplication.Resources("ContextMenuFontSize") = Nothing
                _windowsApplication.Resources("ContextMenuItemMargin") = Nothing
            End If
        End Sub
#End Region

#Region "Appearance"

        Private _appearanceMenuItem As Visibilities = Nothing
        Private _designMenuItem As Visibilities = Nothing
        Private _show As Boolean
        Private _backgroundColors As Object

        ''' <summary>
        ''' Gets or sets the visibility of the Appearance Menuitem in the Application Menu.
        ''' </summary>
        ''' <value>
        ''' The Appearance MenuItem visibilities.
        ''' </value>
        Public Property AppearanceMenuItem As Visibilities
            Get
                Return _appearanceMenuItem
            End Get
            Set(value As Visibilities)
                If _appearanceMenuItem = value Then Return
                _appearanceMenuItem = value
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        '''  Gets or sets the visibility of the Design Menuitem in the Apperance Menu.
        ''' </summary>
        ''' <value>
        ''' The Design MenuItem visibilities.
        ''' </value>
        Public Property DesignMenuItem As Visibilities
            Get
                Return _designMenuItem
            End Get
            Set(value As Visibilities)
                If _designMenuItem = value Then Return
                _designMenuItem = value
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether the Design control (Showcase) in the application area of the user interface is shown.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if shown; otherwise, <c>false</c>.
        ''' </value>
        Public Property Show As Boolean
            Get
                Return _show
            End Get
            Set(value As Boolean)
                _show = value
                OnPropertyChanged()
            End Set
        End Property

#End Region

#Region "BackgroundColor"

        Private _dictionaryBackgroundColors As New Dictionary(Of String, Dictionary(Of Global.Kostal.Testman.Framework.Base.UserLevel, System.Windows.Media.Color))

        Friend ReadOnly Property BackgroundColors As Dictionary(Of String, Dictionary(Of Global.Kostal.Testman.Framework.Base.UserLevel, System.Windows.Media.Color))
            Get
                Return _dictionaryBackgroundColors
            End Get
        End Property

        Sub SetBackgroundColor(idOfOperationMode As String, userLevel As Framework.Base.UserLevel, color As System.Windows.Media.Color)

            Dim dictionaryUserLevelColors As Dictionary(Of Global.Kostal.Testman.Framework.Base.UserLevel, System.Windows.Media.Color) = Nothing
            If Not _dictionaryBackgroundColors.TryGetValue(idOfOperationMode, dictionaryUserLevelColors) Then
                dictionaryUserLevelColors = New Dictionary(Of Global.Kostal.Testman.Framework.Base.UserLevel, System.Windows.Media.Color)
                _dictionaryBackgroundColors.Add(idOfOperationMode, dictionaryUserLevelColors)
            End If
            dictionaryUserLevelColors(userLevel) = color
        End Sub

#End Region

    End Class

End Namespace