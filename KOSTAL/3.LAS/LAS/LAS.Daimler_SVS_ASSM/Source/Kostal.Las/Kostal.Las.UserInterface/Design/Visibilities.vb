Namespace Design

    ''' <summary>
    ''' Indicates the user dependent visibility of an ui element
    ''' </summary>
    <Flags()>
    Public Enum Visibilities        
        ''' <summary>
        ''' Never visible
        ''' </summary>
        None = 0        
        ''' <summary>
        ''' Visible only for the operator
        ''' </summary>
        [Operator] = 1
        ''' <summary>
        ''' Visible only for the service
        ''' </summary>
        Service = 2
        ''' <summary>
        ''' Visible for the operator and service
        ''' </summary>
        OperatorAndService = 3
        ''' <summary>
        ''' Visible only for the developer
        ''' </summary>
        Developer = 4
        ''' <summary>
        ''' Visible for the operator and developer
        ''' </summary>
        OperatorAndDeveloper = 5
        ''' <summary>
        ''' Visible for the service and developer
        ''' </summary>
        ServiceAndDeveloper = 6
        'Administrator = 8
        ''' <summary>
        ''' Visible for all user levels
        ''' </summary>
        All = 7
    End Enum


    '
    ' Summary:
    '     Available levels of access within testman framework
    <Flags>
    Public Enum UserLevel
        '
        ' Summary:
        '     No valid UserLevel.
        None = 0
        '
        ' Summary:
        '     Simplest level for operating personal.
        [Operator] = 1
        '
        ' Summary:
        '     The level used by service personal
        Service = 2
        '
        ' Summary:
        '     The level identifying a developer
        Developer = 4
        '
        ' Summary:
        '     Operator, Service or Developer
        All = 7
    End Enum
End Namespace