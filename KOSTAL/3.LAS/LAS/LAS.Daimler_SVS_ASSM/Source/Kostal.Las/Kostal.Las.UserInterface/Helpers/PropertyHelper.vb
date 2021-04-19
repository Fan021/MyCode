Friend Module PropertyHelper

    ''' <summary>
    ''' </summary>
    ''' <param name="o">object</param>
    <System.Runtime.CompilerServices.Extension> _
    Friend Function GetPropertyName(o As System.Object) As String
        Return Member.Of(Function() o)
    End Function

End Module