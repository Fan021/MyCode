Namespace Ept.Mapping

    ''' <summary>
    ''' Represents a mapped value
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MappedValue

        Private _value As String
        Private _addToConfiguration As Boolean

        ''' <summary>
        ''' Creates new instance of <see cref="MappedValue"/>
        ''' </summary>
        ''' <param name="value">The mapped value</param>
        ''' <param name="addToConfiguration">A boolean flag that decides whether the specified value will be added to the article configuration or not.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal value As String, ByVal addToConfiguration As Boolean)
            _value = value
            _addToConfiguration = addToConfiguration
        End Sub

        ''' <summary>
        ''' Creates new instance of <see cref="MappedValue"/>
        ''' </summary>
        ''' <param name="value">The mapped value</param>
        ''' <remarks>The specified value will always be added to the article configuration.</remarks>
        Public Sub New(ByVal value As String)
            Me.New(value, True)
        End Sub

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property AddToConfiguration As Boolean
            Get
                Return _addToConfiguration
            End Get
        End Property

    End Class


End Namespace
