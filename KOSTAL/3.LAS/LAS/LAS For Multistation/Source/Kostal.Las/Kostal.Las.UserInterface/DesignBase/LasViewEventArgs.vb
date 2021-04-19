Public Class LasViewEventArgs


    Inherits EventArgs

    Private _IsMakeSure As Boolean = False

    Private _Name As String = String.Empty

    Private _ConText As String = String.Empty

    Public Property IsMakeSure As Boolean
        Get
            Return _IsMakeSure
        End Get
        Set(value As Boolean)
            _IsMakeSure = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property ConText As String
        Get
            Return _ConText
        End Get
        Set(value As String)
            _ConText = value
        End Set
    End Property

End Class
