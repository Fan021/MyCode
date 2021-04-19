Imports System.Reflection
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

#Region "User-defined Class of ArticleKeys"

Public Class KostalArticleKeys

    '================================================================
    'Important - First Key Loading - change if necessary
    '================================================================

    'Public Enum Kostal.Las.ArticleProvider.ArticleAttribute
    'ID = 0
    'ArticleNumber = 1
    'ArticleIndex = 2
    'ArticleName = 3
    'ArticleInfo = 4
    'ArticleFamily = 5
    'CustomerNumber = 6
    'HardwareVersion
    'SoftwareVersion
    'Q
    'Picture
    'ValidFrom
    'ValidTo
    'Enabled
    'HardwareMapping
    'ScheduleName
    'End Enum
    '================================================================
    'DO NOT change them here!!!
    '================================================================

    <ArticleKeyAttribute()> Public Const KEY_ID As String = "ID"
    <ArticleKeyAttribute()> Public Const KEY_ARTICLE_NUMBER As String = "ArticleNumber"
    <ArticleKeyAttribute()> Public Const KEY_ARTICLE_INDEX As String = "ArticleIndex"
    <ArticleKeyAttribute()> Public Const KEY_ARTICLE_NAME As String = "ArticleName"
    <ArticleKeyAttribute()> Public Const KEY_ARTICLE_INFO As String = "ArticleInfo"
    <ArticleKeyAttribute()> Public Const KEY_ARTICLE_FAMILY As String = "ArticleFamily"
    <ArticleKeyAttribute()> Public Const KEY_CUSTOMER_NUMBER As String = "CustomerNumber"
    <ArticleKeyAttribute()> Public Const KEY_HARDWARE_VERSION As String = "HardwareVersion"
    <ArticleKeyAttribute()> Public Const KEY_SOFTWARE_VERSION As String = "SoftwareVersion"
    <ArticleKeyAttribute()> Public Const KEY_KOSTAL_Q_INDEX As String = "Q"
    <ArticleKeyAttribute()> Public Const KEY_PICTURE As String = "Picture"
    <ArticleKeyAttribute()> Public Const KEY_VALID_FROM As String = "ValidFrom"
    <ArticleKeyAttribute()> Public Const KEY_VALID_TO As String = "ValidTo"
    <ArticleKeyAttribute()> Public Const KEY_SCHEDULE_NAME As String = "ScheduleName"
    <ArticleKeyAttribute()> Public Const KEY_SERIAL_NUMBER As String = "SN"
    <ArticleKeyAttribute()> Public Const KEY_LASER_TEMPLATE As String = "LaserTemplateName"
    <ArticleKeyAttribute()> Public Const KEY_ALTERNATE_SCHEDULE_ACTIV As String = "ALTERNATE_SCHEDULE"
    <ArticleKeyAttribute()> Public Const KEY_MASK_FILE As String = "MaskFile"
    <ArticleKeyAttribute()> Public Const KEY_MASK_NAME As String = "MaskName"
    <ArticleKeyAttribute()> Public Const KEY_USER_DEFINED As String = "UserDefined"
    '================================================================
    'you can expand any amount of keys here.
    '================================================================
    <ArticleKeyAttribute()> Public Const KEY_LK_PCB_STUFFED_NO As String = "lk_pcb_stuffed_no"
    <ArticleKeyAttribute()> Public Const KEY_PCB_SN_PREFIX As String = "pcb_sn_prefix"
    <ArticleKeyAttribute()> Public Const KEY_SRC_SN_PREFIX As String = "src_sn_prefix"
    <ArticleKeyAttribute()> Public Const KEY_LK_SRC_NO As String = "lk_src_no"
    '       ......

    '================================================================
    'Important Functions as below, DO NOT change them!!
    '================================================================
    Protected Shared _articlekeylist As List(Of String)

    Shared Sub New()
        _articlekeylist = New List(Of String)
        ScanVariablesOnce()
    End Sub

    Public Shared Function GetUserDefinedKeys() As IEnumerable(Of String)

        Return _articlekeylist

    End Function


    Protected Shared Sub ScanVariablesOnce()
        Dim t As Type = GetType(KostalArticleKeys)
        Dim sp As FieldInfo() = t.GetFields()
        For Each f As FieldInfo In sp
            If f.IsDefined(GetType(ArticleKeyAttribute), True) Then
                Try
                    Dim o As Object = f.GetValue(Nothing)
                    _articlekeylist.Add(o.ToString())
                Catch ex As Exception
                    Dim strErr As String = ex.Message
                End Try
            End If
        Next
    End Sub

    Protected Class ArticleKeyAttribute : Inherits Attribute
        Public Sub New()
            'do nothing
        End Sub
    End Class

End Class


Public Class KostalScheduleKeys

    <ScheduleKeyAttribute()> Public Const KEY_ID As String = "ID"
    <ScheduleKeyAttribute()> Public Const KEY_SCHEDULE_INDEX As String = "ScheduleIndex"
    <ScheduleKeyAttribute()> Public Const KEY_SCHEDULE_NAME As String = "ScheduleName"
    <ScheduleKeyAttribute()> Public Const KEY_SCHEDULE_DESCRIPTION As String = "ScheduleDescription"
    <ScheduleKeyAttribute()> Public Const KEY_USER_VERIFICATION As String = "UserVerification"
    <ScheduleKeyAttribute()> Public Const KEY_REFERENCE_SCHEDULE As String = "ReferenceSchedule"
    <ScheduleKeyAttribute()> Public Const KEY_SECURITY_CHECKSUM As String = "SecurityChecksum"
    <ScheduleKeyAttribute()> Public Const COMMON_KEY_PASS_STATION As String = "PassST"
    <ScheduleKeyAttribute()> Public Const COMMON_KEY_FAIL_STATION As String = "FailST"
    <ScheduleKeyAttribute()> Public Const KEY_SCHEDULE As String = "Schedule"
    <ScheduleKeyAttribute()> Public Const KEY_MECHATRONIC As String = "Mechatronic"

    '       ......

    '================================================================
    'Important Functions as below, DO NOT change them!!
    '================================================================
    Protected Shared _articlekeylist As List(Of String)

    Shared Sub New()

        _articlekeylist = New List(Of String)
        ScanVariablesOnce()

    End Sub

    Public Shared Function GetUserDefinedKeys() As IEnumerable(Of String)
        Return _articlekeylist
    End Function


    Protected Shared Sub ScanVariablesOnce()
        Dim t As Type = GetType(ScheduleKeyAttribute)
        Dim sp As FieldInfo() = t.GetFields()
        For Each f As FieldInfo In sp
            If f.IsDefined(GetType(ScheduleKeyAttribute), True) Then
                Try
                    Dim o As Object = f.GetValue(Nothing)
                    _articlekeylist.Add(o.ToString())
                Catch ex As Exception
                    Dim strErr As String = ex.Message
                End Try
            End If
        Next

    End Sub

    Public Shared Function KEY_PASS_STATION(ByVal index As UInteger) As String
        Return COMMON_KEY_PASS_STATION & index.ToString
    End Function

    Public Shared Function KEY_FAIL_STATION(ByVal index As UInteger) As String
        Return COMMON_KEY_FAIL_STATION & index.ToString
    End Function

    Protected Class ScheduleKeyAttribute : Inherits Attribute

        Public Sub New()
            'do nothing
        End Sub

    End Class

End Class

#End Region
