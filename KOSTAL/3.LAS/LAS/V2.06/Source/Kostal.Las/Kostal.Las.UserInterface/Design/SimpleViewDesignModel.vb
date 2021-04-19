Namespace Design

    ''' <summary>
    ''' Holds and give the possibility to change and add text and images for special application states.
    ''' The text and images are used in the simple (operator) view.
    ''' </summary>
    ''' <remarks>The application state mananger also uses the SystemStates enum</remarks>
    Public Class SimpleViewDesignModel
        Private ReadOnly _collectionTexts As New Dictionary(Of Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates, String)
        Private ReadOnly _collectionSizes As New Dictionary(Of Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates, Integer)
        Private ReadOnly _collectionImages As New Dictionary(Of Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates, MainImages)

        Friend Sub New()
            Dim localizer As New Globalization.Localizer(Me)
            SetTextAndSizeForApplicationState(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Off, localizer.GetLocalizedString("The system is off"), 100, MainImages.None)
            SetTextAndSizeForApplicationState(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.On, localizer.GetLocalizedString("The system is on"), 100, MainImages.None)
            SetTextAndSizeForApplicationState(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Problems, localizer.GetLocalizedString("Please solve the problem"), 100, MainImages.Bad)
            SetTextAndSizeForApplicationState(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Run, localizer.GetLocalizedString("Insert part and start the test"), 100, MainImages.Hand)
            SetTextAndSizeForApplicationState(Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Test, localizer.GetLocalizedString("Test is running..."), 80, MainImages.None)
        End Sub

        ''' <summary>
        ''' Sets the text with font size and an imagetype for a special application state.
        ''' The text and image is shown in the simple (operator) view when the application is in the particular state.
        ''' </summary>
        ''' <param name="state">The state for which the other values are used.</param>
        ''' <param name="text">The text.</param>
        ''' <param name="size">The font size used for the text.</param>
        ''' <param name="imageType">The image type of the <see cref="MainImages"/></param>
        Public Sub SetTextAndSizeForApplicationState(state As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates, text As String, size As Integer, imageType As MainImages)
            _collectionTexts(state) = text
            _collectionSizes(state) = size
            _collectionImages(state) = imageType
        End Sub

        ''' <summary>
        ''' Gets the text, stored for the given system state
        ''' </summary>
        ''' <param name="state">The state.</param>
        ''' <returns>The text, if a text was stored for the application state, otherwise Nothing.</returns>
        Public Function GetText(state As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates) As String
            Dim text As String = Nothing
            If _collectionTexts.TryGetValue(state, text) Then Return text
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the font size, stored for the given application state
        ''' </summary>
        ''' <param name="state">The font size.</param>
        ''' <returns>The size, if a size was stored for the application state, otherwise 0.</returns>
        Public Function GetFontSize(state As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates) As Integer
            Dim fontSize As Integer = 0
            If _collectionSizes.TryGetValue(state, fontSize) Then Return fontSize
            Return 0
        End Function

        ''' <summary>
        ''' Gets the MainImages type value, stored for the given application state
        ''' </summary>
        ''' <param name="state">The image.</param>
        ''' <returns>The image type, if an imagetype was stored for the application state, otherwise MainImages.None.</returns>
        Public Function GetImage(state As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates) As MainImages
            Dim image As MainImages = 0
            If _collectionImages.TryGetValue(state, image) Then Return image
            Return MainImages.None
        End Function

    End Class

End Namespace