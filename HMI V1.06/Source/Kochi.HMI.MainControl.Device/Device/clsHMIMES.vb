Public MustInherit Class clsHMIMESBase
    Inherits clsHMIDeviceBase
    MustOverride Property Running As Boolean
    MustOverride ReadOnly Property Enable As Boolean
    MustOverride ReadOnly Property NotInqueue As String
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function Start(ByVal strSN As String, ByRef strResult As String, Optional ByVal strRecipe As String = "") As Boolean
    Public MustOverride Function Complete(ByVal strSN As String, ByRef strResult As String) As Boolean
    Public MustOverride Function logNonConformance(ByVal strSN As String, ByVal lListNcData As List(Of clsNcDataCfg), ByRef strResult As String, Optional ByVal bRetry As Boolean = False) As Boolean
    Public MustOverride Function logParameters(ByVal strSN As String, ByVal lListlogParameter As List(Of clslogParameterCfg), ByRef strResult As String) As Boolean
    Public MustOverride Function GetHUDetails(ByVal strHU1 As String, ByVal strHU2 As String, ByRef strResult As String) As Boolean

    Public MustOverride Function Assemble(ByVal strSN As String, ByVal quantity As Decimal, ByVal lListComponentData As List(Of clsComponentDataCfg), ByRef strResult As String) As Boolean
    Public MustOverride Function LogResourceState(ByVal lListResourceState As List(Of clsResourceStateCfg), ByRef strResult As String) As Boolean

    Public MustOverride Function getSfcStatus(ByVal strSFC As String, ByRef strResult As String) As Boolean
    Public MustOverride Function checkRecipe(ByVal strRecipe As String, ByVal strRecipeVersion As String, ByRef strResult As String) As Boolean
    Public MustOverride Function changeResourceState(ByVal strNewState As String, ByRef strResult As String) As Boolean
    Public MustOverride Function validateBOM(ByVal strSFC As String, ByVal lListbillOfMaterialCfg As List(Of clsbillOfMaterialCfg), ByRef strResult As String) As Boolean
    Public MustOverride Function validateSfc(ByVal strMainSFC As String, ByVal strSFC As String, ByRef strResult As String) As Boolean
    Public MustOverride Function SetupComponent(ByVal strSFC As String, ByRef strResult As String) As Boolean
    Public MustOverride Function getSFCOperation(ByVal strSFC As String, ByRef strResult As String) As Integer
End Class

Public Class clsbillOfMaterialCfg
    Private strItem As String = ""
    Public Property Item As String
        Set(ByVal value As String)
            strItem = value
        End Set
        Get
            Return strItem
        End Get
    End Property
    Sub New()

    End Sub
    Sub New(ByVal strItem As String)
        Me.strItem = strItem
    End Sub
End Class

Public Class clsNcDataCfg
    Private strIdentifier As String = ""
    Private strNcComment As String = ""
    Private strLocation As String = ""
    Private strCompareMode As String = ""
    Private strLowerLimit As String = ""
    Private strValue As String = ""
    Private strUpperLimit As String = ""
    Private strReferenceDesignator As String = ""

    Public Property ReferenceDesignator As String
        Set(ByVal value As String)
            strReferenceDesignator = value
        End Set
        Get
            Return strReferenceDesignator
        End Get
    End Property

    Public Property Identifier As String
        Set(ByVal value As String)
            strIdentifier = value
        End Set
        Get
            Return strIdentifier
        End Get
    End Property

    Public Property NcComment As String
        Set(ByVal value As String)
            strNcComment = value
        End Set
        Get
            Return strNcComment
        End Get
    End Property

    Public Property Location As String
        Set(ByVal value As String)
            strLocation = value
        End Set
        Get
            Return strLocation
        End Get
    End Property

    Public Property CompareMode As String
        Set(ByVal value As String)
            strCompareMode = value
        End Set
        Get
            Return strCompareMode
        End Get
    End Property

    Public Property LowerLimit As String
        Set(ByVal value As String)
            strLowerLimit = value
        End Set
        Get
            Return strLowerLimit
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            strValue = value
        End Set
        Get
            Return strValue
        End Get
    End Property

    Public Property UpperLimit As String
        Set(ByVal value As String)
            strUpperLimit = value
        End Set
        Get
            Return strUpperLimit
        End Get
    End Property

    Sub New()

    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String, ByVal strLocation As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
        Me.strLocation = strLocation
    End Sub

    Sub New(ByVal strIdentifier As String, ByVal strNcComment As String, ByVal strLocation As String, ByVal strCompareMode As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String)
        Me.strIdentifier = strIdentifier
        Me.strNcComment = strNcComment
        Me.strLocation = strLocation
        Me.strCompareMode = strCompareMode
        Me.strLowerLimit = strLowerLimit
        Me.strValue = strValue
        Me.strUpperLimit = strUpperLimit
    End Sub
End Class

Public Class clslogParameterCfg
    Private strIdentifier As String = ""
    Private strCompareMode As String = ""
    Private strDataType As String = ""
    Private strLowerLimit As String = ""
    Private strValue As String = ""
    Private strUpperLimit As String = ""
    Private bPassFailed As Boolean = False

    Public Property Identifier As String
        Set(ByVal value As String)
            strIdentifier = value
        End Set
        Get
            Return strIdentifier
        End Get
    End Property

    Public Property CompareMode As String
        Set(ByVal value As String)
            strCompareMode = value
        End Set
        Get
            Return strCompareMode
        End Get
    End Property

    Public Property DataType As String
        Set(ByVal value As String)
            strDataType = value
        End Set
        Get
            Return strDataType
        End Get
    End Property


    Public Property LowerLimit As String
        Set(ByVal value As String)
            strLowerLimit = value
        End Set
        Get
            Return strLowerLimit
        End Get
    End Property

    Public Property Value As String
        Set(ByVal value As String)
            strValue = value
        End Set
        Get
            Return strValue
        End Get
    End Property

    Public Property UpperLimit As String
        Set(ByVal value As String)
            strUpperLimit = value
        End Set
        Get
            Return strUpperLimit
        End Get
    End Property

    Public Property PassFailed As Boolean
        Set(ByVal value As Boolean)
            bPassFailed = value
        End Set
        Get
            Return bPassFailed
        End Get
    End Property

    Sub New()

    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strValue As String)
        Me.strIdentifier = strIdentifier
        Me.strValue = strValue
    End Sub
    Sub New(ByVal strIdentifier As String, ByVal strValue As String, ByVal strDataType As String)
        Me.strIdentifier = strIdentifier
        Me.strValue = strValue
        Me.strDataType = strDataType
    End Sub

    Sub New(ByVal strIdentifier As String, ByVal strCompareMode As String, ByVal strDataType As String, ByVal strLowerLimit As String, ByVal strValue As String, ByVal strUpperLimit As String, ByVal bPassFailed As Boolean)
        Me.strIdentifier = strIdentifier
        Me.strCompareMode = strCompareMode
        Me.strDataType = strDataType
        Me.strLowerLimit = strLowerLimit
        Me.strValue = strValue
        Me.strUpperLimit = strUpperLimit
        Me.bPassFailed = bPassFailed
    End Sub
End Class


Public Class clsComponentDataCfg
    Private strInventory As String = ""
    Private strMaterialId As String = ""
    Private strmaterialRevision As String = ""
    Private dQuantity As Decimal = 0


    Public Property Inventory As String
        Set(ByVal value As String)
            strInventory = value
        End Set
        Get
            Return strInventory
        End Get
    End Property

    Public Property MaterialId As String
        Set(ByVal value As String)
            strMaterialId = value
        End Set
        Get
            Return strMaterialId
        End Get
    End Property


    Public Property MaterialRevision As String
        Set(ByVal value As String)
            strmaterialRevision = value
        End Set
        Get
            Return strmaterialRevision
        End Get
    End Property

    Public Property Quantity As Decimal
        Set(ByVal value As Decimal)
            dQuantity = value
        End Set
        Get
            Return dQuantity
        End Get
    End Property


    Sub New()

    End Sub

    Sub New(ByVal strInventory As String, ByVal strMaterialId As String, ByVal strmaterialRevision As String, ByVal dQuantity As Decimal)
        Me.strInventory = strInventory
        Me.strMaterialId = strMaterialId
        Me.strmaterialRevision = strmaterialRevision
        Me.dQuantity = dQuantity
    End Sub

End Class


Public Class clsResourceStateCfg
    Private strEntryReasonId As String = ""
    Private strEntryReasonText As String = ""
    Private cEntryTimeStamp As Date = Now
    Private cLeaveTimeStamp As Date = Now


    Public Property EntryReasonId As String
        Set(ByVal value As String)
            strEntryReasonId = value
        End Set
        Get
            Return strEntryReasonId
        End Get
    End Property

    Public Property EntryReasonText As String
        Set(ByVal value As String)
            strEntryReasonText = value
        End Set
        Get
            Return strEntryReasonText
        End Get
    End Property

    Public Property EntryTimeStamp As Date
        Set(ByVal value As Date)
            cEntryTimeStamp = value
        End Set
        Get
            Return cEntryTimeStamp
        End Get
    End Property

    Public Property LeaveTimeStamp As Date
        Set(ByVal value As Date)
            cLeaveTimeStamp = value
        End Set
        Get
            Return cLeaveTimeStamp
        End Get
    End Property
    Sub New()

    End Sub

    Sub New(ByVal strEntryReasonId As String, ByVal strEntryReasonText As String, ByVal cEntryTimeStamp As Date)
        Me.strEntryReasonId = strEntryReasonId
        Me.strEntryReasonText = strEntryReasonText
        Me.cEntryTimeStamp = cEntryTimeStamp
    End Sub
    Sub New(ByVal strEntryReasonId As String, ByVal strEntryReasonText As String, ByVal cEntryTimeStamp As Date, ByVal cLeaveTimeStamp As Date)
        Me.strEntryReasonId = strEntryReasonId
        Me.strEntryReasonText = strEntryReasonText
        Me.cEntryTimeStamp = cEntryTimeStamp
        Me.cLeaveTimeStamp = cLeaveTimeStamp
    End Sub
End Class