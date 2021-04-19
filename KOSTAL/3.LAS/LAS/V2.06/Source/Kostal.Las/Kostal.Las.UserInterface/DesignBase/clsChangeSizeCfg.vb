Public Class clsChangeSizeCfg
    Private ioldH As Integer = 0
    Private inewH As Integer = 0

    Public Property oldH As Integer
        Set(ByVal value As Integer)
            ioldH = value
        End Set
        Get
            Return ioldH
        End Get
    End Property

    Public Property newH As Integer
        Set(ByVal value As Integer)
            inewH = value
        End Set
        Get
            Return inewH
        End Get
    End Property
End Class
