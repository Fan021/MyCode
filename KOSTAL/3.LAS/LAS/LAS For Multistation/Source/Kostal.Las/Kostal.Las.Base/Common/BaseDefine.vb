Imports System.Windows.Forms
Public Enum enumHMI_ERROR_TYPE
    None = 0
    Message
    MasterMessage
    [Error]
    MasterError
    Tips
End Enum

Public Enum enumSTATION_RUN_MODE
    Unknown = 0
    FromAdsToCompare = 1
    FromInsideToCompare = 2
    From_3 = 3
    From_4 = 4
    FromAdsToGetBarcode = 5
End Enum

Public Enum enumLAS_REQUEST_TYPE
    [DEFAULT] = 0
    DoCompare = 1
    DoQuery = 2
End Enum

Public Enum enumINDICATOR_STATRUS
    Unknown = 0
    Gray = 1
    Red = 2
    Green = 3
End Enum

Public Enum enumLANGUAGE
    LOCAL = 1
    ENGLISH = 2
    CHINESE = 3
    SPAINISH = 4
End Enum

Public Class BaseDefine
    Public Sub New()

    End Sub


End Class

Public Interface IArticleCounter
    Sub Add_Record(ByVal ArticleNumber As String)

    Sub Add_Pass(ByVal ArticleNumber As String)

    Sub Add_Fail(ByVal ArticleNumber As String)

End Interface

Public Interface IMaintenance

    Property ShowAlarm() As Boolean
    Property ShowTips() As label
    Function Inc_Count() As String

End Interface


Friend Class CommonServers

    Public Const LAS As String = "LAS"

    Public Const DOT As Char = "."c
    Public Shared Function BuildTag(ByVal ErrorType As enumHMI_ERROR_TYPE) As String

        Dim result As String = ""

        result = LAS + DOT + ErrorType.ToString

        Return result

    End Function



End Class

