Namespace Alarm

    ''' <summary>
    ''' Defines the public members of the AlarmController
    ''' </summary>
    Public Interface IApplicationAlarmController
        Inherits ComponentModel.INotifyPropertyChanged

        ''' <summary>
        ''' Gets the summary state of all AlarmChannels.
        ''' </summary>
        ''' <value>
        ''' The state.
        ''' </value>
        ReadOnly Property State As AlarmStates

        ''' <summary>
        ''' Resets this alarm status.
        ''' </summary>
        ''' <returns>True, if successful, otherwise false.</returns>
        Function Reset() As Boolean

        ''' <summary>
        ''' Gets the collection of all active alarm channels.
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property PhysicalAlarms As System.Collections.ObjectModel.ReadOnlyObservableCollection(Of IAlarmIndication)

        ''' <summary>
        ''' Gets the collection of all active indications.
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Indications As System.Collections.ObjectModel.ReadOnlyObservableCollection(Of IAlarmIndication)

    End Interface

End Namespace