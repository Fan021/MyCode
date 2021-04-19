Namespace Csv
    ''' <summary>
    ''' Defines the kind column used as the key (main identifier) of the 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ArticleKeyReference
        ''' <summary>
        ''' Uses the first column of the csv file as key of 
        ''' </summary>
        ''' <remarks></remarks>
        FirstColumn
        ''' <summary>
        ''' Uses a specific column referenced by a <see cref="mapping.KeyColumnMapping"/> as key of 
        ''' </summary>
        ''' <remarks></remarks>
        SpecificColumn
    End Enum
End Namespace