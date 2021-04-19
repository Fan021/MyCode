Namespace Ept.Mapping

    ''' <summary>
    ''' Defines mapping between xml property and  property
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Mapping

        Public Enum OnEmptyEptPropertyOption As Integer
            Unknown = 0
            NullEntry = 1
            Skip = 2
        End Enum

        ''' <summary>
        ''' Inserts all available property names into the mapping.
        ''' </summary>
        ''' <param name="xmlPropertyNames">List of property names from xml file</param>
        ''' <remarks></remarks>
        Friend MustOverride Sub SetupPropertyNames(ByVal xmlPropertyNames As IEnumerable(Of String))

        ''' <summary>
        ''' Inserts a single line of values read from csv file and expectes a single value extracted based on mapping configuration.
        ''' </summary>
        ''' <param name="xmlPropertyValues">All values of all single variant properties</param>
        ''' <returns>the single value evaluated by the mapping</returns>
        ''' <remarks></remarks>
        Friend MustOverride Function GetValue(ByVal xmlPropertyValues As IEnumerable(Of String)) As MappedValue

        Friend MustOverride Property EmptyEptPropertyOption As OnEmptyEptPropertyOption

        Friend MustOverride Property ShowInArticleSelector As Boolean

    End Class

End Namespace