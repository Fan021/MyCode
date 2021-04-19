Imports System.Text.RegularExpressions

Namespace Ept.Mapping

    ''' <summary>
    ''' Represents a condition that provides functionality to decide whether a mapped value matches a criterium or not.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Condition

        Private _mapping As Mapping
        Private _criterium As String
        Private _regexCriterium As Regex

        ''' <summary>
        ''' Creates a new instance of <see cref="Condition"/>
        ''' </summary>
        ''' <param name="mapping">Mapping of a value.</param>
        ''' <param name="criterium">Criterium the mapped value has to be compared with.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal mapping As Mapping, ByVal criterium As String)
            Me.Mapping = mapping
            Me.Criterium = criterium
        End Sub

        Public Sub New(ByVal mapping As Mapping, ByVal regexCriterium As Regex)
            Me.Mapping = mapping
            _regexCriterium = regexCriterium
        End Sub


        Public Property Mapping() As Mapping
            Get
                Return _mapping
            End Get
            Set(ByVal value As Mapping)
                _mapping = value
            End Set
        End Property


        Public Property Criterium() As String
            Get
                Return _criterium
            End Get
            Set(ByVal value As String)
                _criterium = value
            End Set
        End Property


        Public Property RegexCriterium() As Regex
            Get
                Return _regexCriterium
            End Get
            Set(ByVal value As Regex)
                _regexCriterium = value
            End Set
        End Property


        ''' <summary>
        ''' Compares a mapped value with the specified criterium.
        ''' </summary>
        ''' <param name="propertyValues">Property values of a xml variant.</param>
        ''' <returns>TRUE if mapped value matches the specified criterium, otherwise FALSE. A FALSE will be also returned if the mapped value should not be added to article configuration.</returns>
        ''' <remarks></remarks>
        Public Function IsMatch(ByVal propertyValues As IEnumerable(Of String)) As Boolean

            Dim result As MappedValue = Mapping.GetValue(propertyValues)

            If result.AddToConfiguration Then
                If Not RegexCriterium Is Nothing Then
                    Return RegexCriterium.IsMatch(result.Value)
                Else
                    Return result.Value = Criterium
                End If
            Else
                Return False
            End If

        End Function


    End Class

End Namespace