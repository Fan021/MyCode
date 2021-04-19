Namespace Prompts

    Public Interface IApplicationPromptsController
        Inherits ComponentModel.INotifyPropertyChanged

        ReadOnly Property Current As IPrompt

        Function ShowForStation(controllerStationPrompts As IStationPromptsController) As Boolean

        ReadOnly Property Prompts As System.Collections.ObjectModel.ReadOnlyObservableCollection(Of IPrompt)

    End Interface

End Namespace