Namespace Design

    Public Class DesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Private Shared _testApplication As Kostal.Testman.Framework.Base.Components.ITestApplication
        Private ReadOnly _menuitemmodelDesign As MenuItemModel
        Private ReadOnly _modelProgramDesign As ProgramDesignModel
        Private ReadOnly _modelApplicationDesign As ApplicationDesignModel
        Private ReadOnly _modelStationDesign As StationDesignModel
        Private ReadOnly _modelSimpleView As SimpleViewDesignModel

        Private ReadOnly _windowsApplication As WindowsApplication

        Public Sub New(menuitemmodelDesign As MenuItemModel, windowsApplication As WindowsApplication)
            MyBase.New()
            _menuitemmodelDesign = menuitemmodelDesign
            _windowsApplication = windowsApplication

            _modelProgramDesign = New ProgramDesignModel(_menuitemmodelDesign.AddMenuItem("Program"), _windowsApplication)
            _modelApplicationDesign = New ApplicationDesignModel(Me, _menuitemmodelDesign.AddMenuItem("Application"))
            _modelStationDesign = New StationDesignModel(Me, _menuitemmodelDesign.AddMenuItem("Station"))
            _modelSimpleView = New SimpleViewDesignModel()
        End Sub

        ''' <summary>
        ''' Gets the test application.
        ''' </summary>
        ''' <value>
        ''' The test application.
        ''' </value>
        Public ReadOnly Property TestApplication As Kostal.Testman.Framework.Base.Components.ITestApplication
            Get
                Return _testApplication
            End Get
        End Property

        Friend WriteOnly Property TestApplicationSetter As Kostal.Testman.Framework.Base.Components.ITestApplication
            ' The setter is called only in the Application Startup of the  
            Set(value As Kostal.Testman.Framework.Base.Components.ITestApplication)
                _testApplication = value
                OnPropertyChanged(Member.Of(Function() Me.TestApplication))
            End Set
        End Property

        ''' <summary>
        ''' Returns the Visibilities flag that correspondent to the current user level
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function GetUserLevelVisibility() As Visibilities
            If _testApplication Is Nothing Then Return Visibilities.Operator
            Select Case _testApplication.User.CurrentLevel
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Developer
                    Return Visibilities.Developer
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Service
                    Return Visibilities.Service
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Operator
                    Return Visibilities.Operator
            End Select
            Throw New NotImplementedException("Unknown UserLevel")
        End Function

        ''' <summary>
        ''' Check if the visibilities flag corresponding to the current user is included in the Visibility of the given parameter
        ''' </summary>
        ''' <param name="givenVisibilities">The visibilities (flags) that has to be checked</param>
        ''' <returns><c>true</c> if the visibilities flag for the current user level is set, otherwise <c>false</c> </returns>
        Friend Shared Function GetVisibilityValidForCurrentUserLevel(givenVisibilities As Visibilities) As Boolean
            If _testApplication Is Nothing Then Return False
            Select Case _testApplication.User.CurrentLevel
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Developer
                    Return (givenVisibilities And Visibilities.Developer) = Visibilities.Developer
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Service
                    Return (givenVisibilities And Visibilities.Service) = Visibilities.Service
                Case Global.Kostal.Testman.Framework.Base.UserLevel.Operator
                    Return (givenVisibilities And Visibilities.Operator) = Visibilities.Operator
            End Select
            Throw New NotImplementedException("Unknown UserLevel")
        End Function

        Friend ReadOnly Property MenuItemModel As MenuItemModel
            Get
                Return _menuitemmodelDesign
            End Get
        End Property

        ''' <summary>
        ''' Gets the program design model.
        ''' </summary>
        ''' <value>
        ''' The program design model.
        ''' </value>
        Public ReadOnly Property Program As ProgramDesignModel
            Get
                Return _modelProgramDesign
            End Get
        End Property


        ''' <summary>
        ''' Gets the application design model.
        ''' </summary>
        ''' <value>
        ''' The application design model.
        ''' </value>
        Public ReadOnly Property Application As ApplicationDesignModel
            Get
                Return _modelApplicationDesign
            End Get
        End Property

        ''' <summary>
        ''' Gets the station design model.
        ''' </summary>
        ''' <value>
        ''' The station design model
        ''' </value>
        Public ReadOnly Property Station As StationDesignModel
            Get
                Return _modelStationDesign
            End Get
        End Property

        ''' <summary>
        ''' Gets the simple (operator) view design model.
        ''' </summary>
        ''' <value>
        ''' The simple view design model.
        ''' </value>
        Public ReadOnly Property SimpleView As SimpleViewDesignModel
            Get
                Return _modelSimpleView
            End Get
        End Property

    End Class

End Namespace