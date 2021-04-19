Namespace Helpers

    Public Class ColorHelper

        Private Shared ReadOnly KostalBlueOriginal As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(30, 70, 125)
        Private Shared ReadOnly KostalBlueLightDark As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(75, 115, 165)
        Private Shared ReadOnly KostalBlueLightMedium As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(140, 175, 210)
        Private Shared ReadOnly KostalOrange As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(250, 195, 55)
        Private Shared ReadOnly KostalRed As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(155, 0, 0)
        Private Shared ReadOnly KostalGreen As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(180, 205, 0)
        Private Shared ReadOnly KostalBlack As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(0, 0, 0)
        Private Shared ReadOnly KostalWhite As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 255)

        Private Shared ReadOnly Gray112 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(112, 112, 112)
        Private Shared ReadOnly Gray160 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(160, 160, 160)
        Private Shared ReadOnly Gray208 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(208, 208, 208)
        Private Shared ReadOnly Gray224 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(224, 224, 224)
        Private Shared ReadOnly Gray240 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(240, 240, 240)

        Private Shared ReadOnly GreenLight As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(0, 224, 0)

        Private Shared ReadOnly YellowLight As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 127)
        Private Shared ReadOnly YellowWhite As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 204)

        Private Shared ReadOnly RedDark As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(192, 0, 0)
        Private Shared ReadOnly RedLight As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 64, 64)

        '---------------------------------------------------------------------------------------------------------------------------------------
        ' Colors to be deleted ....

        Private Shared ReadOnly KostalBlueDark As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(15, 35, 62)
        Private Shared ReadOnly KostalBlueLightLight As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(210, 227, 235)

        Private Shared ReadOnly KostalGray64 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(64, 64, 64)
        Private Shared ReadOnly KostalGray128 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(128, 128, 128)
        Private Shared ReadOnly KostalGray176 As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(176, 176, 176)

        Private Shared ReadOnly KostalViolet As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(110, 40, 100)
        Private Shared ReadOnly KostalPink As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 0, 140)
        Private Shared ReadOnly KostalYellow As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 255, 0)
        Private Shared ReadOnly KostalTuerkisBlue As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(0, 125, 200)
        Private Shared ReadOnly KostalTuerkisGreen As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(0, 155, 155)
        Private Shared ReadOnly KostalTuerkisGreenLight As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(200, 235, 220)
        Private Shared ReadOnly RealRed As System.Windows.Media.Color = System.Windows.Media.Color.FromRgb(255, 64, 64)

#Region "Common"

        Shared ReadOnly Property Transparent As System.Windows.Media.Color
            Get
                Return System.Windows.Media.Colors.Transparent
            End Get
        End Property

        Public Shared ReadOnly Property NormalTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Public Shared ReadOnly Property InverseTextColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

#End Region

#Region "MainWindow"

        Shared ReadOnly Property BorderOperatorColor As System.Windows.Media.Color
            Get
                Return KostalBlueOriginal
            End Get
        End Property

        Shared ReadOnly Property BorderServiceColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property BorderDeveloperColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property TopOperatorColor As System.Windows.Media.Color
            Get
                Return KostalBlueOriginal
            End Get
        End Property

        Shared ReadOnly Property TopServiceColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property TopDeveloperColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property BackgroundOperatorColor As System.Windows.Media.Color
            Get
                Return KostalBlueOriginal
            End Get
        End Property

        Shared ReadOnly Property BackgroundServiceColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property BackgroundDeveloperColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

#End Region

#Region "UserState"

        Shared ReadOnly Property UserStateOperatorColor As System.Windows.Media.Color
            Get
                Return KostalGreen
            End Get
        End Property

        Shared ReadOnly Property UserStateServiceColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property UserStateDeveloperColor As System.Windows.Media.Color
            Get
                Return KostalRed
            End Get
        End Property

#End Region

#Region "MachineState"

        Shared ReadOnly Property MachineStateDownColor As System.Windows.Media.Color
            Get
                Return Gray224
            End Get
        End Property


        Shared ReadOnly Property MachineStateOffColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property MachineStateOnColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property MachineStateRunColor As System.Windows.Media.Color
            Get
                Return GreenLight
            End Get
        End Property

        Shared ReadOnly Property MachineStateTestColor As System.Windows.Media.Color
            Get
                Return KostalBlueLightMedium ' System.Windows.Media.Color.FromRgb(0, 125, 255) 'KostalTuerkisBlue
            End Get
        End Property

        Shared ReadOnly Property MachineStateIgnoringColor As System.Windows.Media.Color
            Get
                Return Gray208
            End Get
        End Property

        Shared ReadOnly Property MachineStateDisabledColor As System.Windows.Media.Color
            Get
                Return Gray208
            End Get
        End Property

        Shared ReadOnly Property MachineStateProblemColor As System.Windows.Media.Color
            Get
                Return RedLight
            End Get
        End Property

#End Region

#Region "Counter"

        ''' <summary>
        ''' Gets the color of the counter state good.
        ''' </summary>
        Shared ReadOnly Property CounterStateGoodColor As System.Windows.Media.Color
            Get
                Return GreenLight 'KostalGreen
            End Get
        End Property

        ''' <summary>
        ''' Gets the color of the counter state bad.
        ''' </summary>
        Shared ReadOnly Property CounterStateBadColor As System.Windows.Media.Color
            Get
                Return RedLight 'KostalRed
            End Get
        End Property

        ''' <summary>
        ''' Gets the color of the counter state ignored.
        ''' </summary>
        Shared ReadOnly Property CounterStateIgnoredColor As System.Windows.Media.Color
            Get
                Return Gray208
            End Get
        End Property

        ''' <summary>
        ''' Gets the color of the counter innerframe.
        ''' </summary>
        Shared ReadOnly Property CounterStateInnerFrameColor As System.Windows.Media.Color
            Get
                Return Gray240
            End Get
        End Property

        ''' <summary>
        ''' Gets the color of the counter state none or in testing
        ''' </summary>
        Shared ReadOnly Property CounterStateNoneColor As System.Windows.Media.Color
            Get
                Return Gray224
            End Get
        End Property

        ''' <summary>
        ''' Gets the color of border of the PPM view.
        ''' </summary>
        Shared ReadOnly Property CounterPPMBorderColor As System.Windows.Media.Color
            Get
                Return Gray208
            End Get
        End Property

#End Region

#Region "Information"

        Shared ReadOnly Property InformationStateOtherColor As System.Windows.Media.Color
            Get
                Return ApplicationLabelAndValueValueColor
            End Get
        End Property

        Shared ReadOnly Property InformationStateOkColor As System.Windows.Media.Color
            Get
                Return GreenLight
            End Get
        End Property

        Shared ReadOnly Property InformationStateWarningColor As System.Windows.Media.Color
            Get
                Return KostalOrange
            End Get
        End Property

        Shared ReadOnly Property InformationStateProblemColor As System.Windows.Media.Color
            Get
                Return RedLight
            End Get
        End Property

#End Region

#Region "ApplicationLabelAndValue"

        Shared ReadOnly Property ApplicationLabelAndValueBackcolor As System.Windows.Media.Color
            Get
                Return KostalBlueLightDark ' KostalBlueLightMedium 'GentzmerGray5 ' Gray224
            End Get
        End Property

        Shared ReadOnly Property ApplicationLabelAndValueLabelColor As System.Windows.Media.Color
            Get
                Return Gray208 'KostalBlueLightMedium ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property ApplicationLabelAndValueValueColor As System.Windows.Media.Color
            Get
                Return KostalWhite ' New System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(150, 150, 255)) ' KostalBlueLightMedium
            End Get
        End Property

#End Region

#Region "StationLabelAndValue"

        Shared ReadOnly Property StationLabelAndValueBackcolor As System.Windows.Media.Color
            Get
                Return InnerBackground 'KostalWhiteMedium
            End Get
        End Property

        Shared ReadOnly Property StationLabelAndValueLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 'KostalGray128 'KostalGray176
            End Get
        End Property

        Shared ReadOnly Property StationLabelAndValueValueColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "MainContent"

        Shared ReadOnly Property MainContentBorderThickness As System.Windows.Thickness
            Get
                Return New System.Windows.Thickness(5)
            End Get
        End Property

        Shared ReadOnly Property MainContentBorderColor As System.Windows.Media.Color
            Get
                Return StationSelectionBackcolor 'StationSelectedBackcolor
            End Get
        End Property

        Shared ReadOnly Property MainContentBackgroundColor As System.Windows.Media.Color
            Get
                Return StationSelectionBackcolor 'StationSelectedBackcolor
            End Get
        End Property

#End Region

#Region "Groups for Station and View Selection"

        Private Shared ReadOnly Property ViewSelectionBackcolor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

        Private Shared ReadOnly Property InnerBackground As System.Windows.Media.Color
            Get
                Return Gray240
            End Get
        End Property

        Private Shared ReadOnly Property StationSelectionBackcolor As System.Windows.Media.Color
            Get
                Return Gray160
            End Get
        End Property

#End Region

#Region "StationSelection"

        Shared ReadOnly Property StationUnselectedTextColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

        Shared ReadOnly Property StationUnselectedBackcolor As System.Windows.Media.Color
            Get
                Return KostalBlueLightDark
            End Get
        End Property
        Shared ReadOnly Property StationSelectedTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property StationSelectedBackcolor As System.Windows.Media.Color
            Get
                Return StationSelectionBackcolor 'KostalWhite
            End Get
        End Property

#End Region

#Region "ViewSelection"

        Shared ReadOnly Property ViewUnselectedTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property ViewUnselectedBackcolor As System.Windows.Media.Color
            Get
                Return Gray160
            End Get
        End Property

        Shared ReadOnly Property ViewSelectedTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property ViewSelectedBackcolor As System.Windows.Media.Color
            Get
                Return ViewSelectionBackcolor 'KostalGrayLight
            End Get
        End Property

#End Region

#If True Then

#Region "StationOverview"

        Shared ReadOnly Property StationOverviewBorderThickness As System.Windows.Thickness
            Get
                Return New System.Windows.Thickness(5)
            End Get
        End Property

        Shared ReadOnly Property StationOverviewBorderColor As System.Windows.Media.Color
            Get
                Return ViewSelectionBackcolor 'KostalGrayMedium
            End Get
        End Property

        Shared ReadOnly Property StationOverviewBackgroundColor As System.Windows.Media.Color
            Get
                Return ViewSelectionBackcolor 'KostalGrayMedium
            End Get
        End Property

        Shared ReadOnly Property StationOverviewCaptionBackgroundColor As System.Windows.Media.Color
            Get
                Return ViewSelectionBackcolor 'KostalGrayMedium
            End Get
        End Property

        Shared ReadOnly Property StationOverviewRunnablesBackgroundColor As System.Windows.Media.Color
            Get
                Return RedLight
            End Get
        End Property

#End Region

#Region "StationDetail"

        Shared ReadOnly Property StationDetailBorderThickness As System.Windows.Thickness
            Get
                Return New System.Windows.Thickness(0)
            End Get
        End Property

        Shared ReadOnly Property StationDetailBorderColor As System.Windows.Media.Color
            Get
                Return Gray240
            End Get
        End Property

        Shared ReadOnly Property StationDetailBackgroundColor As System.Windows.Media.Color
            Get
                Return ViewSelectionBackcolor 'KostalWhite
            End Get
        End Property

        Shared ReadOnly Property StationDetailCaptionBackgroundColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

#End Region

#Region "Prompt"

#Region "Empty" ' Wie None 

        Shared ReadOnly Property PromptEmptyElementsBackground As System.Windows.Media.Color
            Get
                Return KostalBlueLightDark
            End Get
        End Property

        Shared ReadOnly Property PromptEmptyControlBackground As System.Windows.Media.Color
            Get
                Return KostalBlueOriginal
            End Get
        End Property

        Shared ReadOnly Property PromptEmptySymbolColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property PromptEmptyLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property PromptEmptyTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "None"

        Shared ReadOnly Property PromptNoneElementsBackground As System.Windows.Media.Color
            Get
                Return Gray208
            End Get
        End Property

        Shared ReadOnly Property PromptNoneControlBackground As System.Windows.Media.Color
            Get
                Return Gray240
            End Get
        End Property

        Shared ReadOnly Property PromptNoneSymbolColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property PromptNoneLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property PromptNoneTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "Information" ' Ich mag das helle Blau nicht - kann ich auch nicht richtig begründen - ist mir zu bunt dafür das es fast immer zu sehen ist -> deshalb in Grau

        Shared ReadOnly Property PromptInformationElementsBackground As System.Windows.Media.Color
            Get
                Return Gray208 'System.Windows.Media.Color.FromRgb(127, 191, 255)
            End Get
        End Property

        Shared ReadOnly Property PromptInformationControlBackground As System.Windows.Media.Color
            Get
                Return Gray240 'System.Windows.Media.Color.FromRgb(204, 229, 255)
            End Get
        End Property

        Shared ReadOnly Property PromptInformationSymbolColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property PromptInformationLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property PromptInformationTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "Question" 'Das Orange beißt sich mit dem Developer Mode -> deshalb in Gelb

        Shared ReadOnly Property PromptQuestionElementsBackground As System.Windows.Media.Color
            Get
                Return YellowLight 'System.Windows.Media.Color.FromRgb(255, 178, 102)
            End Get
        End Property

        Shared ReadOnly Property PromptQuestionControlBackground As System.Windows.Media.Color
            Get
                Return YellowWhite 'System.Windows.Media.Color.FromRgb(255, 229, 204)
            End Get
        End Property

        Shared ReadOnly Property PromptQuestionSymbolColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property PromptQuestionLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property PromptQuestionTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "Warning" 'Das Gelb finde ich ok

        Shared ReadOnly Property PromptWarningElementsBackground As System.Windows.Media.Color
            Get
                Return YellowLight
            End Get
        End Property

        Shared ReadOnly Property PromptWarningControlBackground As System.Windows.Media.Color
            Get
                Return YellowWhite
            End Get
        End Property

        Shared ReadOnly Property PromptWarningSymbolColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

        Shared ReadOnly Property PromptWarningLabelColor As System.Windows.Media.Color
            Get
                Return Gray112 ' KostalGray128
            End Get
        End Property

        Shared ReadOnly Property PromptWarningTextColor As System.Windows.Media.Color
            Get
                Return KostalBlack
            End Get
        End Property

#End Region

#Region "Problem" 'Das Gelb finde ich ok

        Shared ReadOnly Property PromptProblemElementsBackground As System.Windows.Media.Color
            Get
                Return RedDark
            End Get
        End Property

        Shared ReadOnly Property PromptProblemControlBackground As System.Windows.Media.Color
            Get
                Return RedLight
            End Get
        End Property

        Shared ReadOnly Property PromptProblemSymbolColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

        Shared ReadOnly Property PromptProblemLabelColor As System.Windows.Media.Color
            Get
                Return Gray160
            End Get
        End Property

        Shared ReadOnly Property PromptProblemTextColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

#End Region

#Region "Alarm" 'Das Gelb finde ich ok

        Shared ReadOnly Property PromptAlarmElementsBackground As System.Windows.Media.Color
            Get
                Return RedDark
            End Get
        End Property

        Shared ReadOnly Property PromptAlarmControlBackground As System.Windows.Media.Color
            Get
                Return RedLight
            End Get
        End Property

        Shared ReadOnly Property PromptAlarmSymbolColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

        Shared ReadOnly Property PromptAlarmLabelColor As System.Windows.Media.Color
            Get
                Return Gray160
            End Get
        End Property

        Shared ReadOnly Property PromptAlarmTextColor As System.Windows.Media.Color
            Get
                Return KostalWhite
            End Get
        End Property

#End Region

#End Region

#Else

#Region "StationMedium"

    Shared ReadOnly Property StationOverviewBorderThickness As System.Windows.Thickness
        Get
            Return New System.Windows.Thickness(5)
        End Get
    End Property

    Shared ReadOnly Property StationOverviewBorderColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.BlueViolet
        End Get
    End Property

    Shared ReadOnly Property StationOverviewBackgroundColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.Aqua
        End Get
    End Property

    Shared ReadOnly Property StationOverviewCaptionBackgroundColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.Aqua
        End Get
    End Property

#End Region

#Region "StationBig"

    Shared ReadOnly Property StationDetailBorderThickness As System.Windows.Thickness
        Get
            Return New System.Windows.Thickness(5)
        End Get
    End Property

    Shared ReadOnly Property StationDetailBorderColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.BlueViolet
        End Get
    End Property

    Shared ReadOnly Property StationDetailBackgroundColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.Aqua
        End Get
    End Property

    Shared ReadOnly Property StationDetailCaptionBackgroundColor As System.Windows.Media.Color
        Get
            Return System.Windows.Media.Colors.Aqua
        End Get
    End Property

#End Region

#End If

    End Class

End Namespace