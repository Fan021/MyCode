
Imports Kostal.Testman.Framework


Namespace Prompts

    Public Class ApplicationPromptsController
        Inherits NotifyingObject
        Implements Prompts.IApplicationPromptsController

        Private ReadOnly _logger As NLog.Logger = NLog.LogManager.GetCurrentClassLogger
        Private ReadOnly _listStationPromptsController As New List(Of Prompts.IStationPromptsController)
        Private ReadOnly _collectionPrompts As New ObjectModel.ObservableCollection(Of Prompts.IPrompt)
        Private ReadOnly _readonlycollectionPrompts As System.Collections.ObjectModel.ReadOnlyObservableCollection(Of Prompts.IPrompt)

        Private ReadOnly _controllerHardware As Kostal.Testman.Framework.Head.HardwareController

        Private _currentPrompt As Prompts.IPrompt = Nothing
        Private _objPromptSetter As Object = New Object

        Private _threadBlink As Threading.Thread
        Private _blinkIsOn As Boolean
        Private _datetimeLastBlink As System.DateTime = System.DateTime.Now
        Private _abortBlinkThread As Boolean = False

        Public Sub New(controllerHardware As Kostal.Testman.Framework.Head.HardwareController)
            _readonlycollectionPrompts = New System.Collections.ObjectModel.ReadOnlyObservableCollection(Of Prompts.IPrompt)(_collectionPrompts)

            _controllerHardware = controllerHardware

            _threadBlink = New Threading.Thread(AddressOf DoBlinking)
            _threadBlink.IsBackground = True
            _threadBlink.Name = "ApplicationPromptControllerBlinkThread"
            _threadBlink.Priority = System.Threading.ThreadPriority.BelowNormal
            _threadBlink.Start()
        End Sub

        ''Friend Sub AddStationPromptsControllers(testSystems As IEnumerable(Of Components.ITestSystem))
        ''    For Each system As Components.ITestSystem In testSystems
        ''        AddStationPromptsController(system.MyStation.Prompts)
        ''        For Each station As Components.ITestStation In system.SubStations
        ''            AddStationPromptsController(station.Prompts)
        ''        Next
        ''    Next
        ''End Sub

        Friend Sub AddStationPromptsController(controllerStationPrompts As Prompts.IStationPromptsController)
            _listStationPromptsController.Add(controllerStationPrompts)
            AddHandler controllerStationPrompts.PropertyChanged, AddressOf StationPromptsContoller_PropertyChanged
            StationPromptsContoller_PropertyChanged(controllerStationPrompts, New System.ComponentModel.PropertyChangedEventArgs(Member.Of(Function() controllerStationPrompts.Current)))
        End Sub

        Public ReadOnly Property Current() As Prompts.IPrompt Implements Prompts.IApplicationPromptsController.Current
            <System.Diagnostics.DebuggerStepThrough>
            Get
                Return _currentPrompt
            End Get
        End Property

        Private Sub SetCurrent(newPrompt As Prompts.IPrompt)

            'Ist die Anzeige gleich, so nicht beachten
            If _currentPrompt Is newPrompt Then Return

            'Ggf. Signale rücksetzen und alle Abfragen deaktivieren
            If _currentPrompt IsNot Nothing Then

                'Response-Kanäle abmelden
                For Each response As Prompts.IResponse In _currentPrompt.PossibleResponses
                    For Each channelId As String In response.DigitalInputChannelIds
                        ''Dim digitalinputchannelResponse As Kostal.Hal.Core.DigitalInput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalInput)(channelId)
                        ''If digitalinputchannelResponse Is Nothing Then Continue For
                        ''AddHandler digitalinputchannelResponse.ValueChanged, AddressOf digitalinputchannelResponse_ValueChanged
                        ''_controllerHardware.Polling.RemoveChannel(Me, digitalinputchannelResponse)
                    Next
                Next

                'Output-Kanäle auf 0 setzen
                For Each channelId As String In _currentPrompt.DigitalOutputChannelIds
                    ''Dim ch As Kostal.Hal.Core.DigitalOutput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalOutput)(channelId)
                    ''If ch Is Nothing Then Continue For
                    ''ch.UpdateTo(False)
                Next

            End If

            SyncLock _objPromptSetter
                'Anzeige übernehmen
                _currentPrompt = newPrompt

                'Ggf. Signale setzen und alle Abfragen aktivieren
                If _currentPrompt IsNot Nothing Then

                    'Response-Kanäle anmelden
                    For Each response As Prompts.IResponse In _currentPrompt.PossibleResponses
                        For Each channelId As String In response.DigitalInputChannelIds
                            ''Dim digitalinputchannelResponse As Kostal.Hal.Core.DigitalInput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalInput)(channelId)
                            ''If digitalinputchannelResponse Is Nothing Then Continue For
                            ''AddHandler digitalinputchannelResponse.ValueChanged, AddressOf digitalinputchannelResponse_ValueChanged
                            ''_controllerHardware.Polling.AddChannel(Me, digitalinputchannelResponse, 100)
                        Next
                    Next

                    SetOutputChannels(True, _currentPrompt)
                End If

                'Änderung mitteilen
                OnPropertyChangedWithSender(Me, Member.Of(Function() Me.Current))
            End SyncLock
        End Sub

        Private Sub DoBlinking()
            Try
                _abortBlinkThread = False
                Do
                    System.Threading.Thread.Sleep(10)
                    If _abortBlinkThread Then Return

                    'Current Prompt umladen, ansonsten Probleme
                    Dim tempCurrentPrompt As Prompts.IPrompt = Current
                    If tempCurrentPrompt Is Nothing Then Continue Do
                    If tempCurrentPrompt.DigitalOutputChannelIds Is Nothing OrElse tempCurrentPrompt.DigitalOutputChannelIds.Count < 1 Then Continue Do
                    If tempCurrentPrompt.BlinkTimeOff < 1 OrElse tempCurrentPrompt.BlinkTimeOn < 1 Then Continue Do

                    If _blinkIsOn Then
                        If System.DateTime.Now.Subtract(_datetimeLastBlink).TotalMilliseconds < tempCurrentPrompt.BlinkTimeOn Then Continue Do
                        SetOutputChannels(False, tempCurrentPrompt)
                    Else
                        If System.DateTime.Now.Subtract(_datetimeLastBlink).TotalMilliseconds < tempCurrentPrompt.BlinkTimeOff Then Continue Do
                        SetOutputChannels(True, tempCurrentPrompt)
                    End If
                Loop While Not _abortBlinkThread
                ''Catch hwex As Kostal.Hal.Core.HardwareException
                ''    _logger.Error(hwex, "Error polling Alarm channels.")
            Catch ex As Exception
                _logger.Error(ex, "Error polling Alarm channels. General Exception thrown by hardware component")
            End Try

            ' TODO Throw Exception
        End Sub

        ''' <summary>
        ''' Output-Kanäle auf 1 setzen
        ''' </summary>
        ''' <param name="setToOn"></param>
        ''' <remarks></remarks>
        Private Sub SetOutputChannels(setToOn As Boolean, curPrompt As Prompts.IPrompt)
            For Each channelId As String In curPrompt.DigitalOutputChannelIds
                ''Dim digitaloutputchannelIndication As Kostal.Hal.Core.DigitalOutput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalOutput)(channelId)
                ''If digitaloutputchannelIndication Is Nothing Then Continue For
                ''digitaloutputchannelIndication.UpdateTo(setToOn)
            Next
            _blinkIsOn = setToOn
            _datetimeLastBlink = System.DateTime.Now
        End Sub

        Public Function ShowForStation(ByVal controllerStationMessages As Prompts.IStationPromptsController) As Boolean Implements Prompts.IApplicationPromptsController.ShowForStation
            If controllerStationMessages.Current IsNot Nothing Then
                SetCurrent(controllerStationMessages.Current)
                Return True
            End If
            Return False
        End Function

        Private Sub StationPromptsContoller_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            If e.PropertyName <> Member.Of(Function() _listStationPromptsController(0).Current) Then Return

            Dim listPromptsAlreadyExisting As New List(Of Prompts.IPrompt)
            SyncLock _collectionPrompts
                For Each stationPromptsController As Prompts.IStationPromptsController In _listStationPromptsController
                    Dim promptStation As Prompts.IPrompt = stationPromptsController.Current
                    If promptStation IsNot Nothing Then

                        'Locale prompt are not affected here
                        If (promptStation.DisplayOption And Prompts.DisplayOptions.Local) = Prompts.DisplayOptions.Local Then Continue For

                        listPromptsAlreadyExisting.Add(promptStation)
                        If Not _collectionPrompts.Contains(stationPromptsController.Current) Then _collectionPrompts.Add(stationPromptsController.Current)
                    End If
                Next

                Dim listToCheckIfRemoved As Prompts.IPrompt() = _collectionPrompts.ToArray()
                For Each promptToCheck As Prompts.IPrompt In listToCheckIfRemoved
                    If Not listPromptsAlreadyExisting.Contains(promptToCheck) Then
                        _collectionPrompts.Remove(promptToCheck)
                    End If
                Next
            End SyncLock

            'ggf. die nicht mehr existierende Nachricht vom Current nehmen
            If _currentPrompt IsNot Nothing AndAlso Not listPromptsAlreadyExisting.Contains(_currentPrompt) Then
                SetCurrent(Nothing)
            End If

            'Gibt es keine aktuelle Nachricht oder eine aktuelle Nachricht ohne Response, so nach einer Nachricht mit Response suchen
            If _currentPrompt Is Nothing OrElse (_currentPrompt IsNot Nothing AndAlso _currentPrompt.PossibleResponses.Count() < 1) Then
                Dim listToCheckIfRemoved As Prompts.IPrompt() = _collectionPrompts.ToArray()
                For Each prompt As Prompts.IPrompt In listToCheckIfRemoved
                    If prompt.PossibleResponses.Count > 0 Then
                        SetCurrent(prompt)
                        Return
                    End If
                Next
            End If

            'ggf. eine beliebige Nachricht als Current setzen
            If _currentPrompt Is Nothing Then
                Dim listToCheckIfRemoved As Prompts.IPrompt() = _collectionPrompts.ToArray()
                For Each prompt As Prompts.IPrompt In listToCheckIfRemoved
                    SetCurrent(prompt)
                    Return
                Next
            End If
        End Sub

        Public ReadOnly Property SystemPrompts As System.Collections.ObjectModel.ReadOnlyObservableCollection(Of Prompts.IPrompt) Implements Prompts.IApplicationPromptsController.Prompts
            <System.Diagnostics.DebuggerStepThrough>
            Get
                Return _readonlycollectionPrompts
            End Get
        End Property

        ''Private Sub digitalinputchannelResponse_ValueChanged(channel As Hal.Core.Channel)
        ''    If Not System.Convert.ToBoolean(channel.Value) Then Return
        ''    If _currentPrompt Is Nothing Then Return

        ''    For Each responsePossible As Base.Prompts.IResponse In _currentPrompt.PossibleResponses
        ''        For Each channelId As String In responsePossible.DigitalInputChannelIds
        ''            If channelId = channel.Name Then
        ''                _currentPrompt.SetResponse(responsePossible.Id)
        ''                Exit For
        ''            End If
        ''        Next
        ''    Next
        ''End Sub

    End Class

End Namespace