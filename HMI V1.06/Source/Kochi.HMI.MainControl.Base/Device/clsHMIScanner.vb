Public MustInherit Class clsHMIScanner
    Inherits clsHMIDeviceBase

    Public Overrides Function CreateControlUI(ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean

    End Function

    Public Overrides Function CreateInitUI(ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean

    End Function

    Public Overrides Function Init(ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean

    End Function

    Public Overrides Function Quit(ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean

    End Function

    Public Overrides Function Run(ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean

    End Function
End Class
