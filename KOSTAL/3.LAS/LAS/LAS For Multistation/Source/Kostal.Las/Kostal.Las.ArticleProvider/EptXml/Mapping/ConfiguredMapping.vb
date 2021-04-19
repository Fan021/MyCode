Namespace Ept.Mapping

    Public Class ConfiguredMapping

        Private _name As String
        Private _overwrite As Boolean
        Private _mapping As Mapping

        Public Sub New(ByVal name As String, ByVal mapping As Mapping)
            Me.New(name, mapping, False)
        End Sub

        Public Sub New(ByVal name As String, ByVal mapping As Mapping, ByVal overwrite As Boolean)
            Me.Name = ContextNameValidator.ValidateAndUpdateContextName(name)
            Me.Mapping = mapping

            _overwrite = overwrite
        End Sub

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = ContextNameValidator.ValidateAndUpdateContextName(value)
            End Set
        End Property

        Public Property Mapping() As Mapping
            Get
                Return _mapping
            End Get
            Set(ByVal value As Mapping)
                _mapping = value
            End Set
        End Property

        Public ReadOnly Property Overwrite() As Boolean
            Get
                Return _overwrite
            End Get
        End Property

    End Class


End Namespace

