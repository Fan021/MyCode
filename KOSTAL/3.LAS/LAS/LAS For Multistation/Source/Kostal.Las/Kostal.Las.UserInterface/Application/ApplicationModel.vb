Imports System.ComponentModel
Imports System.Windows
Imports Kostal.Testman.Framework.Base
Imports Kostal.Testman.Framework.Base.Components
Imports Kostal.Windows.Presentation
Imports Kostal.Hal.Core
Imports Kostal.Testman.Framework.Core
Imports Kostal.Testman.UserInterface.Helpers

Public Class ApplicationModel
    Inherits Global.Kostal.Windows.Presentation.ViewModelBase

    'Defines a list for storing the delegates
    Private ReadOnly _listEventHandlers As New ArrayList

    'Defines the Click event using the custom event syntax.
    'The RaiseEvent always invokes the delegates asynchronously
    Public Custom Event PermanentTick As EventHandler
        AddHandler(ByVal value As EventHandler)
            _listEventHandlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            _listEventHandlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            For Each handler As EventHandler In _listEventHandlers
                If handler Is Nothing Then Continue For
                Try
                    'handler.BeginInvoke(sender, e, Nothing, Nothing)
                    handler.Invoke(sender, e)
                Catch ex As Exception
#If DEBUG Then
                    Dialogs.ExceptionDialog("Exception while invoking event handler", ex)
#End If
                    Debug.WriteLine("Exception while invoking event handler: " & ex.ToString())
                End Try
            Next
        End RaiseEvent
    End Event

    Private ReadOnly _logger As NLog.Logger = NLog.LogManager.GetCurrentClassLogger
    Private ReadOnly _testApplication As ITestApplication
    Private ReadOnly _modelApplicationContent As IApplicationContentModel

    Private ReadOnly _menuitemmodelApplication As MenuItemModel
    Private ReadOnly _menuitemmodelApplicationDiagnostics As MenuItemModel
    'Private ReadOnly _menuitemmodelDesign As MenuItemModel '
    Private ReadOnly _mennuItemModelMaximized As MenuItemModel
    Private ReadOnly _userInterface As KostalRunningUserInterface

    Private ReadOnly _menuItemOperationModel As MenuItemModel
    Private ReadOnly _menuItemProcessModel As MenuItemModel
    Private ReadOnly _menuItemArticleModel As MenuItemModel
    Private ReadOnly _appearanceMenuItem As MenuItemModel
    Private ReadOnly _apperanceDesignMenuItem As MenuItemModel

    Private ReadOnly _collectionStationModels As New System.Collections.ObjectModel.ObservableCollection(Of StationModel)
    'Private ReadOnly _threadTick As System.Threading.Thread

    Public Sub New(userInterface As KostalRunningUserInterface, testApplication As ITestApplication)
        MyBase.New()
        Me.DisplayName = "ApplicationModel"

        _userInterface = userInterface
        _testApplication = testApplication

        Dim s As String = _testApplication.Runtime.Resolve(Of Testman.Plugin.Base.ITesterDataProvider).GetInfo(Of String)(Kostal.Testman.Plugin.Base.TesterProperty.KeepMainWindowPosition)
        If s.IsNullOrEmpty() OrElse Not Boolean.TryParse(s, _keepMainWindowPosition) Then
            _keepMainWindowPosition = True ' default if not present is True
        End If
        If Not _keepMainWindowPosition Then _windowState = System.Windows.WindowState.Maximized ' When not saving the Position set initial state to Maximized

        SimpleMaximizeCommand = New Kostal.Windows.Presentation.RelayCommand(New Action(Sub() OnSimpleMaximizeCommandPressed()))
        SimpleMinimizeCommand = New Kostal.Windows.Presentation.RelayCommand(New Action(Sub() OnSimpleMinimizeCommandPressed()))

        _menuitemmodelApplication = New MenuItemModel("Application")

        '        Dim iap As IGuiController
        '        iap = _testApplication.ApplicationRuntime.Resolve(Of IGuiController)()

        Me.DesignModel = _userInterface.Design
        _appearanceMenuItem = _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Appearance"))
        _mennuItemModelMaximized = New MenuItemModel(Me.Localizer.GetLocalizedString("Maximized"),
                                                         New Action(Sub()
                                                                        If WindowState = WindowState.Maximized Then
                                                                            WindowState = WindowState.Normal
                                                                        Else
                                                                            WindowState = System.Windows.WindowState.Maximized
                                                                        End If
                                                                    End Sub))
        _appearanceMenuItem.AddMenuItem(_mennuItemModelMaximized)
        _appearanceMenuItem.AddMenuItem("1280x728", AddressOf NormalizeWindowToWxgaWith)
        _appearanceMenuItem.AddMenuItem("1280x964", AddressOf NormalizeWindowToSxgaWith)
        _appearanceMenuItem.AddMenuItem("1280x1024", AddressOf NormalizeWindowToSxgaWithout)
        _appearanceMenuItem.AddMenuItem("1920x1020", AddressOf NormalizeWindowToFhdWith)
        _appearanceMenuItem.AddMenuItem("1920x1080", AddressOf NormalizeWindowToFHdWithout)

        'If _userInterface.Design.DesignMenuItem Then
        _apperanceDesignMenuItem = _appearanceMenuItem.AddMenuItem(Me.DesignModel.MenuItemModel)
        'End If

        Me.ApplicationLogoModel = New ApplicationLogoModel(_testApplication, _userInterface.Design.MenuItemModel)
        Me.ApplicationStateModel = New ApplicationStateModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("State")))
        Me.UserStateModel = New ApplicationUserModel(_testApplication, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("User")))

        _menuItemOperationModel = _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Operation Mode"))
        Me.ApplicationOperationModel = New ApplicationOperationModel(_testApplication, _menuItemOperationModel)

        _menuItemProcessModel = _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Process"))
        Me.ApplicationProcessModel = New ApplicationProcessModel(_testApplication, _menuItemProcessModel)

        _menuItemArticleModel = _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Article"))
        Me.ApplicationArticleModel = New ApplicationArticleModel(Me, _menuItemArticleModel)

        Me.ApplicationCounterModel = New ApplicationCounterModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Counter")))
        Me.ApplicationHealthItemsModel = New ApplicationHealthItemsModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Health")))
        Me.ApplicationPromptsModel = New ApplicationPromptsModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Messages"), Sub() Me.ApplicationContentModel.CurrentContentType = IApplicationContentModel.ApplicationContentType.Prompts))
        Me.ApplicationAlarmsModel = New ApplicationAlarmsModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Alarms"), Sub() Me.ApplicationContentModel.CurrentContentType = IApplicationContentModel.ApplicationContentType.Alarms))
        Me.ApplicationJobsModel = New ApplicationJobsModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Jobs"), Sub() Me.ApplicationContentModel.CurrentContentType = IApplicationContentModel.ApplicationContentType.Jobs))
        Me.ApplicationLogsModel = New ApplicationLogsModel(Me, _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Logs"), Sub() Me.ApplicationContentModel.CurrentContentType = IApplicationContentModel.ApplicationContentType.Logs))
        Me.ApplicationCommandButtonsModel = New ApplicationCommandButtonsModel(Me) ', _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Commands")))
        Me.ApplicationStatusModel = New ApplicationStatusModel(Me)



        ' get test station view models
        _menuitemmodelApplication.AddSeparator()
        For Each testSystem As ITestSystem In _testApplication.TestSystems
            _collectionStationModels.Add(New StationModel(Me, testSystem.MyStation, _menuitemmodelApplication.AddMenuItem(testSystem.MyStation.Text)))
            For Each testStation As ITestStation In testSystem.SubStations
                _collectionStationModels.Add(New StationModel(Me, testStation, _menuitemmodelApplication.AddMenuItem(testStation.Text)))
            Next
        Next

        'Final menu items
        'DirectCast(_viewbuilder, KostalViewBuilder).CreateUserInterface(_testApplication)
        _menuitemmodelApplication.AddSeparator()
        _menuitemmodelApplicationDiagnostics = _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Diagnostics"))
        _menuitemmodelApplicationDiagnostics.AddMenuItem(Me.Localizer.GetLocalizedString("Logging"),
                                                         New RelayCommand(AddressOf ShowLogging, New Func(Of Boolean)(Function() Not IsOperator())))
        _menuitemmodelApplicationDiagnostics.AddMenuItem(Me.Localizer.GetLocalizedString("Hardware"),
                                                         New RelayCommand(AddressOf ShowHardwarePanel, New Func(Of Boolean)(Function() Not IsOperator())))
        _menuitemmodelApplicationDiagnostics.AddMenuItem(Me.Localizer.GetLocalizedString("Plugin Prototyping"),
                                                         New RelayCommand(AddressOf ShowPlugins, New Func(Of Boolean)(Function() Not IsOperator() AndAlso Not IsService())))
        _menuitemmodelApplicationDiagnostics.AddMenuItem(Me.Localizer.GetLocalizedString("Information"),
                                                         New RelayCommand(AddressOf ShowInfo))
        _menuitemmodelApplication.AddSeparator()

        SimpleExitCommand = New Kostal.Windows.Presentation.RelayCommand(AddressOf OnSimpleExitCommandPressed, AddressOf SimpleExitCanExecuteCommand)
        _menuitemmodelApplication.AddMenuItem(Me.Localizer.GetLocalizedString("Exit"), SimpleExitCommand)


        Select Case testApplication.TestSystems.Count()
            Case 0
            Case 1
                If testApplication.TestSystems(0).SubStations.Any Then
                    _modelApplicationContent = New SingleSystemMultiStationsModel(Me, _collectionStationModels)
                Else
                    _modelApplicationContent = New SingleSystemSingleStationModel(Me, _collectionStationModels(0))
                End If
            Case Else

                Dim ok As Boolean = True
                For Each testSystem As ITestSystem In testApplication.TestSystems
                    If testSystem.SubStations.Any Then
                        ok = False
                        Exit For
                    End If
                Next

                If ok Then _modelApplicationContent = New MultiSystemsSingleStationModel(Me, _collectionStationModels)
        End Select

        If _modelApplicationContent Is Nothing Then
            Throw New NotSupportedException("Your system/station configuration is not supported")
        End If

        Me.ApplicationNavigationModel = New ApplicationNavigationModel(_testApplication, _modelApplicationContent)

        AddHandler Me.ApplicationArticleModel.ArticleSelectorPressed, AddressOf TestApplication_ArticleSelectorPressed

        AddHandler _testApplication.Operation.PropertyChanged, AddressOf TestApplication_Operation_PropertyChanged
        AddHandler _testApplication.User.PropertyChanged, AddressOf TestApplication_User_PropertyChanged
        TestApplication_User_PropertyChanged(Nothing, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() _testApplication.User.CurrentLevel)))

        CreateTickThread()
    End Sub

#Region "TickThread"
    Private Function CreateTickThread() As System.Threading.Thread
        Dim threadTick As System.Threading.Thread = New System.Threading.Thread(AddressOf Tick_Tick)
        threadTick.Name = "ApplicationModelTickThread"
        threadTick.IsBackground = True
        threadTick.Priority = System.Threading.ThreadPriority.BelowNormal
        threadTick.Start()
        Return threadTick
    End Function

    Private Sub Tick_Tick()
        Try
            While True
                System.Threading.Thread.Sleep(50)
                RaiseEvent PermanentTick(Me, System.EventArgs.Empty)
            End While
        Catch ex As Exception
            _logger.Error(ex, "ApplicationModelTickThread stopped!")
        End Try
    End Sub
#End Region

#Region "Show User Views"
    Private Sub ShowLogging()
        Dim loggingViewer As LoggingViewer = New LoggingViewer
        loggingViewer.Show()
    End Sub

    Private Sub ShowHardwarePanel()
        Dim hardwarePanel As New Hal.Gui.HWLayerPanel2()

        Dim fontName As String = Nothing
        Dim fontSizeString As String = Nothing

        fontName = _testApplication.Runtime.Resolve(Of Testman.Plugin.Base.ITesterDataProvider).GetInfo(Of String)(Kostal.Testman.Plugin.Base.TesterProperty.TestStepAndResultSpecialFontName)
        fontSizeString = _testApplication.Runtime.Resolve(Of Testman.Plugin.Base.ITesterDataProvider).GetInfo(Of String)(Kostal.Testman.Plugin.Base.TesterProperty.TestStepAndResultSpecialFontSize)

        If fontName IsNot Nothing Then
            Dim fontSize As Single = 9.0!
            If fontSizeString IsNot Nothing Then
                If Not Single.TryParse(fontSizeString, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, fontSize) Then
                    fontSize = 9.0!
                End If
            End If
            Dim newFont As System.Drawing.Font = New System.Drawing.Font(fontName, fontSize!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
            SetFontOfAllControlsIncludingChilds(hardwarePanel, newFont)
        End If
        CType(hardwarePanel, IHardwareUser).SetHardwareDevice(_testApplication.Runtime.Resolve(Of Device))
        hardwarePanel.Show()
    End Sub

    Private Shared Sub SetFontOfAllControlsIncludingChilds(controls As System.Windows.Forms.Control, font As Drawing.Font)
        For Each control As System.Windows.Forms.Control In controls.Controls
            If control.HasChildren Then
                SetFontOfAllControlsIncludingChilds(control, font)
            End If
            Select Case control.Name
                Case "txtValueToWrite", "txtSettingValue", "lblSettingValue", "lstChannels", "testSystemTree"
                    control.Font = font
            End Select
        Next
    End Sub

    Private Sub ShowPlugins()
        Dim fileDef As New TestFileDefinition("Prototyping")
        Dim sectionDef As New TestSectionDefinition(fileDef, "PROTOTYPING")
        Dim stepDef As New TestStepDefinition(sectionDef, "TEST")
        stepDef.CompareMode = CompareModes.FixFalse

        Dim formPrototyping As New System.Windows.Forms.Form
        formPrototyping.Size = New System.Drawing.Size(900, 660)
        formPrototyping.Text = "Plugin Prototyping"
        formPrototyping.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        formPrototyping.ShowInTaskbar = True
        formPrototyping.MinimizeBox = False

        Dim comboboxStation As System.Windows.Forms.ComboBox = New System.Windows.Forms.ComboBox
        comboboxStation.Dock = System.Windows.Forms.DockStyle.Fill
        comboboxStation.DisplayMember = "Text"
        comboboxStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        For Each sys As ITestSystem In _testApplication.TestSystems
            comboboxStation.Items.Add(sys.MyStation)
            For Each st As ITestStation In sys.SubStations
                comboboxStation.Items.Add(st)
            Next
        Next
        Dim controlStepEditor As Framework.Editor.StepEditorControl

        controlStepEditor = New Framework.Editor.StepEditorControl(True, New Kostal.Testman.Framework.Editor.MainController(formPrototyping, True))
        controlStepEditor.Dock = System.Windows.Forms.DockStyle.Fill
        controlStepEditor.EditingEnabled = True

        AddHandler comboboxStation.SelectedIndexChanged, Sub(sender, args)
                                                             controlStepEditor.StationController.Current = DirectCast(DirectCast(sender, System.Windows.Forms.ComboBox).SelectedItem, ITestStation)
                                                         End Sub

        Dim sp As System.Windows.Forms.SplitContainer = New System.Windows.Forms.SplitContainer
        sp.ClientSize = New System.Drawing.Size(890, 660)
        sp.Orientation = System.Windows.Forms.Orientation.Horizontal
        sp.Dock = System.Windows.Forms.DockStyle.Fill
        sp.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        sp.SplitterDistance = comboboxStation.Height
        sp.SplitterWidth = 1
        sp.IsSplitterFixed = True

        sp.Panel1MinSize = comboboxStation.Height
        sp.Panel1.Controls.Add(comboboxStation)
        sp.Panel2.Controls.Add(controlStepEditor)

        formPrototyping.Controls.Add(sp)
        formPrototyping.Show()  ' Add a parent window or ShowinTaskBar = True
        comboboxStation.SelectedIndex = 0
    End Sub

    Private Sub ShowInfo()
        Dim appInfo As New TestmanInformationView
        appInfo.Article = _testApplication.Runtime.Resolve(Of Kostal.Testman.Framework.Base.SystemState.IArticleState).Selected
        If appInfo.Article Is Nothing Then
            System.Windows.MessageBox.Show(Me.Localizer.GetLocalizedString("Please select article first!"), Me.Localizer.GetLocalizedString("Show Information"), MessageBoxButton.OK, MessageBoxImage.Stop)
            Return
        End If

        Dim testerNames As List(Of String) = New List(Of String)(4)
        For Each testSystem As ITestSystem In _testApplication.TestSystems
            testerNames.Add(_testApplication.Runtime.Resolve(Of Testman.Plugin.Base.ITesterDataProvider).GetInfo(Of String)(testSystem.Id + Plugin.Base.Constants.ContextPathSeparator + Plugin.Base.TesterProperty.TesterName.ToString()))
        Next
        appInfo.Name = String.Join("; ", testerNames)
        appInfo.ShowInfo()
    End Sub

#End Region

#Region "EventHandler"

    Private Sub TestApplication_User_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
        If Me.UiDispatcher.TryRunInDispatcherAsync(Sub() TestApplication_User_PropertyChanged(sender, e)) Then Return

        _menuItemProcessModel.Visible = Design.DesignModel.GetVisibilityValidForCurrentUserLevel(_userInterface.Design.Application.ProcessSelector)
        _menuItemArticleModel.Visible = Design.DesignModel.GetVisibilityValidForCurrentUserLevel(_userInterface.Design.Application.ArticleSelector)
        _appearanceMenuItem.Visible = Design.DesignModel.GetVisibilityValidForCurrentUserLevel(_userInterface.Design.Program.AppearanceMenuItem)
        _apperanceDesignMenuItem.Visible = Design.DesignModel.GetVisibilityValidForCurrentUserLevel(_userInterface.Design.Program.DesignMenuItem)

        CheckColors()
    End Sub

    Private Sub TestApplication_Operation_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
        If Me.UiDispatcher.TryRunInDispatcherAsync(Sub() TestApplication_Operation_PropertyChanged(sender, e)) Then Return
        CheckColors()
    End Sub

    Private Sub CheckColors()

        Dim dictionaryUserLevelColors As Dictionary(Of Global.Kostal.Testman.Framework.Base.UserLevel, System.Windows.Media.Color) = Nothing
        If _testApplication.Operation.Selected IsNot Nothing Then
            If Not Me.DesignModel.Program.BackgroundColors.TryGetValue(_testApplication.Operation.Selected.Id, dictionaryUserLevelColors) Then
                Me.DesignModel.Program.BackgroundColors.TryGetValue(System.String.Empty, dictionaryUserLevelColors)
            End If
        Else
            Me.DesignModel.Program.BackgroundColors.TryGetValue(System.String.Empty, dictionaryUserLevelColors)
        End If

        If dictionaryUserLevelColors IsNot Nothing Then
            For Each kvpUserLevelColor As KeyValuePair(Of UserLevel, Media.Color) In dictionaryUserLevelColors
                If (kvpUserLevelColor.Key And _testApplication.User.CurrentLevel) = _testApplication.User.CurrentLevel Then
                    BorderBackcolor = kvpUserLevelColor.Value
                    WindowBackcolor = kvpUserLevelColor.Value
                    TopBackcolor = kvpUserLevelColor.Value
                    Return
                End If
            Next
        End If

        Select Case _testApplication.User.CurrentLevel
            Case Global.Kostal.Testman.Framework.Base.UserLevel.Operator
                BorderBackcolor = ColorHelper.BorderOperatorColor
                WindowBackcolor = ColorHelper.BackgroundOperatorColor
                TopBackcolor = ColorHelper.TopOperatorColor
            Case Global.Kostal.Testman.Framework.Base.UserLevel.Service
                BorderBackcolor = ColorHelper.BorderServiceColor
                WindowBackcolor = ColorHelper.BackgroundServiceColor
                TopBackcolor = ColorHelper.TopServiceColor
            Case Global.Kostal.Testman.Framework.Base.UserLevel.Developer
                BorderBackcolor = ColorHelper.BorderDeveloperColor
                WindowBackcolor = ColorHelper.BackgroundDeveloperColor
                TopBackcolor = ColorHelper.TopDeveloperColor
        End Select
    End Sub

#End Region

    ReadOnly Property Data As Kostal.Testman.Plugin.Base.ITesterDataProvider
        Get
            Return _testApplication.Runtime.Resolve(Of Kostal.Testman.Plugin.Base.ITesterDataProvider)()
        End Get
    End Property

#Region "Properties"

    Private _borderBackcolor As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(230, 70, 125)

    Public Property BorderBackcolor As System.Windows.Media.Color
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _borderBackcolor
        End Get
        Set(value As System.Windows.Media.Color)
            If Equals(value, _borderBackcolor) Then Return
            _borderBackcolor = value
            OnPropertyChanged()
        End Set
    End Property

    Private _windowBackcolor As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(30, 170, 125)

    Public Property WindowBackcolor As System.Windows.Media.Color
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _windowBackcolor
        End Get
        Set(value As System.Windows.Media.Color)
            If Equals(value, _windowBackcolor) Then Return
            _windowBackcolor = value
            OnPropertyChanged()
        End Set
    End Property

    Private _topBackcolor As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(30, 70, 225)

    Public Property TopBackcolor As System.Windows.Media.Color
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _topBackcolor
        End Get
        Set(value As System.Windows.Media.Color)
            If Equals(value, _topBackcolor) Then Return
            _topBackcolor = value
            OnPropertyChanged()
        End Set
    End Property

#End Region

#Region "ViewModels"

    Private _articleSelectorWindowView As ArticleSelectorWindowView
    Private _articleSelectorViewModel As ArticleSelectorViewModel

    Private Sub TestApplication_ArticleSelectorPressed(sender As Object, e As EventArgs)
        ShowArticleDialog()
    End Sub

    Public Sub ShowArticleDialog()
        Dim article As ArticleCollection = _testApplication.Article.GetArticles()
        If article IsNot Nothing AndAlso article.Any Then

            Dim testerContext As Testman.Plugin.Base.ITesterDataProvider = _testApplication.Runtime.Resolve(Of Testman.Plugin.Base.ITesterDataProvider)()
            Dim firstFocusIndex As Integer = testerContext.GetInfo(Of Integer)(Plugin.Base.TesterProperty.SelectorFirstFocus)
            Dim selectorSearchCustomer As Boolean = testerContext.GetInfo(Of Boolean)(Plugin.Base.TesterProperty.SelectorSearchCustomer)
            Dim selectorFontSize As Integer = testerContext.GetInfo(Of Integer)(Plugin.Base.TesterProperty.SelectorFontSize)
            Dim selectorFontWeight As FontWeight = FontWeights.Normal
            Dim fntWeightString As String = testerContext.GetInfo(Of String)(Plugin.Base.TesterProperty.SelectorFontWeight)
            If Not fntWeightString.IsNullOrEmpty() Then
                Try
                    selectorFontWeight = CType(New FontWeightConverter().ConvertFromString(fntWeightString), FontWeight)
                Catch ex As Exception
                    _logger.Warn(ex, "Problem when analyzing the FontWeight for article selector. Read: {0}", fntWeightString)
                    selectorFontWeight = FontWeights.Normal
                End Try
            End If

            If _articleSelectorViewModel Is Nothing Then
                _articleSelectorViewModel = New ArticleSelectorViewModel(Me, True, article, firstFocusIndex, selectorFontSize, selectorFontWeight, selectorSearchCustomer)
                AddHandler _articleSelectorViewModel.ArticleDialogCompleted, AddressOf ArticleSelectorViewModel_ArticleDialogCompleted
            End If
            If _testApplication.Article.Selected IsNot Nothing AndAlso Not Equals(_testApplication.Article.Selected.Key, _articleSelectorViewModel.SelectedArticle.Key) Then
                _articleSelectorViewModel.SetSelectedArticle(_testApplication.Article.Selected.Key)
            End If

            _articleSelectorWindowView = New ArticleSelectorWindowView(_articleSelectorViewModel)

            _articleSelectorWindowView.Owner = RunningWindow.Instance.View
            _articleSelectorWindowView.ShowDialog()
        End If
    End Sub

    Private Sub ArticleSelectorViewModel_ArticleDialogCompleted(ByVal sender As Object, ByVal e As Kostal.Testman.UserInterface.ArticleSelector.ArticleDialogCompletedEventArgs)
        If e.IsAborted Then
        ElseIf e.SelectedArticle Is Nothing Then
            _testApplication.Article.[Select](DirectCast(Nothing, Kostal.Testman.Framework.Base.ArticleConfigurationSet))
        ElseIf e.SelectedArticle IsNot Nothing Then
            _testApplication.Article.[Select](e.SelectedArticle.ConfigurationSet)
        End If

        If _articleSelectorWindowView Is Nothing Then Return
        _articleSelectorWindowView.Close()
        Return
    End Sub

    'Private _modelStationSelector As StationSelectorModel

    Public ReadOnly Property ApplicationContentModel As IApplicationContentModel
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _modelApplicationContent
        End Get
    End Property

    Public ReadOnly Property StationModels As System.Collections.ObjectModel.ObservableCollection(Of StationModel)
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _collectionStationModels
        End Get
    End Property

    Public ReadOnly Property Version As String
        Get
#If DEBUG Then
            Return "4.0.3.0"
#End If
            Return Kostal.Testman.Framework.Base.TestmanInformation.GetTestmanVersion
        End Get
    End Property

    Public ReadOnly Property VersionColor As System.Windows.Media.Color
        Get
            If Version.StartsWith("0") Then
                ' 0.x.x.x Orange
                Return ColorHelper.InformationStateWarningColor
            Else
                If Version.EndsWith(".0") Then
                    ' 4.x.x.0 OK
                    Return ColorHelper.ApplicationLabelAndValueValueColor
                Else
                    ' 0.x.x.xxxx Red
                    Return ColorHelper.InformationStateProblemColor
                End If
            End If
        End Get
    End Property


    Public Property ApplicationLogoModel As ApplicationLogoModel

    Public Property DesignModel As Design.DesignModel

    Public Property ApplicationStateModel As ApplicationStateModel

    Public Property UserStateModel As ApplicationUserModel

    Public Property ApplicationNavigationModel As ApplicationNavigationModel

    Public Property ApplicationCounterModel As ApplicationCounterModel

    Public Property ApplicationPromptsModel As ApplicationPromptsModel

    Public Property ApplicationCommandButtonsModel As ApplicationCommandButtonsModel

    Public Property ApplicationJobsModel As ApplicationJobsModel

    Public Property ApplicationLogsModel As ApplicationLogsModel

    Public Property ApplicationAlarmsModel As ApplicationAlarmsModel

    Public Property ApplicationHealthItemsModel As ApplicationHealthItemsModel

    Public Property ApplicationOperationModel As ApplicationOperationModel

    Public Property ApplicationProcessModel As ApplicationProcessModel

    Public Property ApplicationArticleModel As ApplicationArticleModel

    Public Property ApplicationStatusModel As ApplicationStatusModel

#End Region

    Private _width As Integer = 1280

    Public Property Width As Integer
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _width
        End Get
        Set(value As Integer)
            If _width = value Then Return
            _width = value
            OnPropertyChanged()
        End Set
    End Property


    Private _height As Integer = 768

    Public Property Height As Integer
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _height
        End Get
        Set(value As Integer)
            If _height = value Then Return
            _height = value
            OnPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property MinWidth As Integer
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return 1000
        End Get
    End Property

    Public ReadOnly Property MinHeight As Integer
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return 700
        End Get
    End Property

    Private ReadOnly _keepMainWindowPosition As Boolean = True

    Public ReadOnly Property KeepMainWindowPosition As Boolean
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _keepMainWindowPosition
        End Get
    End Property

    Private _windowState As System.Windows.WindowState = System.Windows.WindowState.Normal

    Public Property WindowState As System.Windows.WindowState
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _windowState
        End Get
        Set(value As System.Windows.WindowState)
            If _windowState = value Then Return
            _windowState = value
            OnPropertyChanged()
            OnPropertyChanged(Member.Of(Function() Me.MaximizeTooltipText))

            ' TODO Testcode
            If _windowState = WindowState.Maximized Then
                _mennuItemModelMaximized.Checked = True
            Else
                _mennuItemModelMaximized.Checked = False
            End If
        End Set
    End Property
    Public ReadOnly Property MaximizeTooltipText As String
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            If Me.WindowState = System.Windows.WindowState.Maximized Then
                Return Me.Localizer.GetLocalizedString("Restore")
            End If
            Return Me.Localizer.GetLocalizedString("Maximize")
        End Get
    End Property
    Public ReadOnly Property MinimizeTooltipText As String
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return Me.Localizer.GetLocalizedString("Minimize")
        End Get
    End Property

    Public ReadOnly Property Background As System.Windows.Media.Color
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return ColorHelper.BackgroundOperatorColor
        End Get
        'Set(value As System.Windows.Media.SolidColorBrush)

        'End Set
    End Property

    Public ReadOnly Property MenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
        Get
            If _menuitemmodelApplication Is Nothing Then Return Nothing
            Return _menuitemmodelApplication.MenuItems
        End Get
    End Property

    Private Sub NormalizeWindowToWxgaWith()
        ' Is 1280 x 768
        NormalizeWindowTo(1280, 708)
    End Sub
    Private Sub NormalizeWindowToSxgaWith()
        NormalizeWindowTo(1280, 964)
    End Sub

    Private Sub NormalizeWindowToSxgaWithout()
        NormalizeWindowTo(1280, 1024)
    End Sub

    Private Sub NormalizeWindowToFhdWith()
        NormalizeWindowTo(1920, 1020)
    End Sub

    Private Sub NormalizeWindowToFHdWithout()
        NormalizeWindowTo(1920, 1080)
    End Sub

    Private Sub NormalizeWindowToHd(withToolbar As Boolean)
        WindowState = System.Windows.WindowState.Normal
        Width = 1280
        If withToolbar Then
            Height = 964
        Else
            Height = 1024
        End If
    End Sub

    Private Sub NormalizeWindowToFullHd(withToolbar As Boolean)
        WindowState = System.Windows.WindowState.Normal
        Width = 1920
        If withToolbar Then
            Height = 1020
        Else
            Height = 1080
        End If
    End Sub

    Private Sub NormalizeWindowTo(width As Integer, height As Integer)
        WindowState = System.Windows.WindowState.Normal
        Me.Width = width
        Me.Height = height
    End Sub

    Public ReadOnly Property TestApplication As Framework.Base.Components.ITestApplication
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _testApplication
        End Get
    End Property

    Public Property SimpleMaximizeCommand As System.Windows.Input.ICommand
    Private Sub OnSimpleMaximizeCommandPressed()
        If Me.WindowState = System.Windows.WindowState.Maximized Then
            Me.WindowState = System.Windows.WindowState.Normal
        Else
            Me.WindowState = System.Windows.WindowState.Maximized
        End If
    End Sub

    Public Property SimpleMinimizeCommand As System.Windows.Input.ICommand
    Private Sub OnSimpleMinimizeCommandPressed()
        Me.WindowState = System.Windows.WindowState.Minimized
    End Sub

    Public ReadOnly Property DesignMenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _userInterface.Design.MenuItemModel.MenuItems
        End Get
    End Property

#Region "Helper"

    Public Function IsExitAllowed() As Boolean

        For Each testSystem As ITestSystem In _testApplication.TestSystems
            If testSystem.MyStation.State.Value > IStationStateManager.StationStates.Off Then Return False
            For Each testStation As ITestStation In testSystem.SubStations
                If testStation.State.Value > IStationStateManager.StationStates.Off Then Return False
            Next
        Next

        If IsOperator() Then
            If OptionHelper.GetBoolean("Application.Operator", "ShutdownAllowed", True) Then
                Return True
            End If
        Else
            Return True
        End If
        Return False
    End Function

    Public Function IsOperator() As Boolean
        Return _testApplication.User.CurrentLevel = Global.Kostal.Testman.Framework.Base.UserLevel.Operator
    End Function
    Public Function IsService() As Boolean
        Return _testApplication.User.CurrentLevel = Global.Kostal.Testman.Framework.Base.UserLevel.Service
    End Function
    Public Function IsDeveloper() As Boolean
        Return _testApplication.User.CurrentLevel = Global.Kostal.Testman.Framework.Base.UserLevel.Developer
    End Function

#End Region

    Public Property SimpleExitCommand As System.Windows.Input.ICommand

    Private Function SimpleExitCanExecuteCommand() As Boolean
        Return IsExitAllowed()
    End Function

    Private Sub OnSimpleExitCommandPressed()
        _testApplication.State.TurnToDown()

        'If _modelApplication.IsInOffState() Then
        '    If _modelApplication.IsOperator() Then
        '        If OptionHelper.GetBoolean("Application.Operator", "ShutdownAllowed", True) Then
        '            System.Windows.Application.Current.Shutdown()
        '        End If
        '    Else
        '        System.Windows.Application.Current.Shutdown()
        '    End If
        'Else
        '    '_modelApplication.TestApplication.State.TurnToPrev()
        'End If
    End Sub




End Class