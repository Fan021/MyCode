Namespace Helpers
    Public Class FontHelper

        Private Shared ReadOnly _fontArial As System.Windows.Media.FontFamily
        Private Shared ReadOnly _fontWebdings As System.Windows.Media.FontFamily
        Private Shared ReadOnly _fontWingdings1 As System.Windows.Media.FontFamily
        Private Shared ReadOnly _fontWingdings2 As System.Windows.Media.FontFamily
        Private Shared ReadOnly _fontWingdings3 As System.Windows.Media.FontFamily

        Shared Sub New()
            _fontArial = New System.Windows.Media.FontFamily("Calibri")
            _fontWebdings = New System.Windows.Media.FontFamily("Webdings")
            _fontWingdings1 = New System.Windows.Media.FontFamily("Wingdings")
            _fontWingdings2 = New System.Windows.Media.FontFamily("Wingdings 2")
            _fontWingdings3 = New System.Windows.Media.FontFamily("Wingdings 3")
        End Sub

        Public Shared ReadOnly Property Arial As System.Windows.Media.FontFamily
            Get
                Return _fontArial
            End Get
        End Property

        Public Shared ReadOnly Property Webdings As System.Windows.Media.FontFamily
            Get
                Return _fontWebdings
            End Get
        End Property

        Public Shared ReadOnly Property Wingdings1 As System.Windows.Media.FontFamily
            Get
                Return _fontWingdings1
            End Get
        End Property

        Public Shared ReadOnly Property Wingdings2 As System.Windows.Media.FontFamily
            Get
                Return _fontWingdings2
            End Get
        End Property

        Public Shared ReadOnly Property Wingdings3 As System.Windows.Media.FontFamily
            Get
                Return _fontWingdings3
            End Get
        End Property

    End Class
End NameSpace