Public Class clsParameterCfg
    Public Name As String = String.Empty
    Public Value As String = ""
    Public Type As Type
    Public ListValue As New List(Of String)
    Public VariantChange As Boolean = False
    Public VariantElement As String = ""
    Public PLC As String = ""
    Public AdsName As String = ""
    Public Enable As Boolean = False
    Public Shared Function ChangeStringToLasType(ByVal strValue As String) As Type
        Select Case strValue
            Case "Boolean"
                Return GetType(Boolean)
            Case "Byte"
                Return GetType(Byte)
            Case "Integer"
                Return GetType(Int16)
            Case "Single"
                Return GetType(Single)
            Case "String"
                Return GetType(String)
            Case Else
                Throw New Exception("Don't support Type:" + strValue)
        End Select

    End Function
    Public Shared Operator <>(ByVal x As clsParameterCfg, ByVal y As clsParameterCfg) As Boolean

        If x Is Nothing Or y Is Nothing Then Return False
        Return x.Name <> y.Name Or x.Value <> y.Value Or x.Type <> y.Type Or x.ListValue.Count <> y.ListValue.Count Or x.VariantChange <> y.VariantChange Or x.VariantElement <> y.VariantElement Or x.PLC <> y.PLC Or x.AdsName <> y.AdsName Or x.Enable <> y.Enable
    End Operator
    Public Shared Operator =(ByVal x As clsParameterCfg, ByVal y As clsParameterCfg) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        Return x.Name = y.Name And x.Value = y.Value And x.Type = y.Type And x.ListValue.Count = y.ListValue.Count And x.VariantChange = y.VariantChange And x.VariantElement = y.VariantElement And x.PLC = y.PLC And x.AdsName = y.AdsName And x.Enable = y.Enable
    End Operator

    Public Function Clone() As clsParameterCfg
        Dim cTemp As New clsParameterCfg
        cTemp.PLC = PLC
        cTemp.Name = Name
        cTemp.Value = Value
        cTemp.Enable = Enable
        cTemp.Type = Type
        cTemp.AdsName = AdsName
        cTemp.ListValue = ListValue
        cTemp.VariantChange = VariantChange
        cTemp.VariantElement = VariantElement
        Return cTemp
    End Function
End Class
