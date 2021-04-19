Public Class Station

    Protected mUser As Long
    Protected mIdString As String
    Protected mName As String

    Protected mBeforeInputNumber As Long
    Protected mAfterInputNumber As Long
    Protected mStepInputNumber As Long
    Protected mStepOutputNumber As Long
    Protected mStepFromNumber As Long
    Protected mToggle As Boolean

    Protected mAddress_Pass As Long
    Protected mAddress_Fail As Long

    Protected mAddress_Origin As Long
    Protected mAddress_Home As Long
    Protected mAddress_Ready As Long
    Protected mAddress_ReadPreviousStamp As Long
    Protected mAddress_WriteCurrentStamp As Long
    Protected mAddress_WriteCurrentStamp_PASS As Long
    Protected mAddress_WriteCurrentStamp_FAIL As Long
    Protected mAddress_NewPart As Long
    Protected mAddress_Return As Long
    Protected mAddress_Dummy As Long
    Protected mAddress_RepairSchedule As Long
    Protected mAddress_Scan As Long
    Protected mAddress_Print As Long
    Protected mAddress_Teach As Long
    Protected mAddress_WaitForPlc As Long
    Protected mStack As String

    Protected mIsInit As Boolean
    Protected mIsOn As Boolean
    Protected mIsStart As Boolean
    Protected mIsMessage As Boolean
    Protected mIsError As Boolean
    Protected mIsMasterError As Boolean
    Protected mIsAutomatic As Boolean
    Protected mIsManuall As Boolean
    Protected mIsReady As Boolean
    Protected mIsVisible As Boolean

    Protected mText As String
    Protected mStepTextLine As String
    Protected mParameters As New Collection


#Region "Properties"

    Public Property Parameters() As Collection
        Get
            Return mParameters
        End Get
        Set(ByVal value As Collection)
            mParameters = value
        End Set
    End Property

    Public Property User() As Long
        Get
            Return mUser
        End Get
        Set(ByVal value As Long)
            mUser = value
        End Set
    End Property

    Public Property IdString() As String
        Get
            Return mIdString
        End Get
        Set(ByVal value As String)
            mIdString = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Public Property StepInputNumber() As Long
        Get
            Return mStepInputNumber
        End Get
        Set(ByVal value As Long)
            mStepInputNumber = value
        End Set
    End Property
    Public Property BeforeStepNumber() As Long
        Get
            Return mBeforeInputNumber
        End Get
        Set(ByVal value As Long)
            mBeforeInputNumber = value
        End Set
    End Property

    Public Property AfterInputNumber() As Long
        Get
            Return mAfterInputNumber
        End Get
        Set(ByVal value As Long)
            mAfterInputNumber = value
        End Set
    End Property

    Public Property StepFromNumber() As Long
        Get
            Return mStepFromNumber
        End Get
        Set(ByVal value As Long)
            mStepFromNumber = value
        End Set
    End Property

    Public Property StepOutputNumber() As Long
        Get
            Return mStepOutputNumber
        End Get
        Set(ByVal value As Long)
            mStepOutputNumber = value
        End Set
    End Property

    Public Property Toggle() As Boolean
        Get
            Return mToggle
        End Get
        Set(ByVal value As Boolean)
            mToggle = value
        End Set
    End Property

    Public Property Address_Fail() As Long
        Get
            Return mAddress_Fail
        End Get
        Set(ByVal value As Long)
            mAddress_Fail = value
        End Set
    End Property

    Public Property Address_Origin() As Long
        Get
            Return mAddress_Origin
        End Get
        Set(ByVal value As Long)
            mAddress_Origin = value
        End Set
    End Property

    Public Property Address_Home() As Long
        Get
            Return mAddress_Home
        End Get
        Set(ByVal value As Long)
            mAddress_Home = value
        End Set
    End Property

    Public Property Address_Ready() As Long
        Get
            Return mAddress_Ready
        End Get
        Set(ByVal value As Long)
            mAddress_Ready = value
        End Set
    End Property

    Public Property Address_NewPart() As Long
        Get
            Return mAddress_NewPart
        End Get
        Set(ByVal value As Long)
            mAddress_NewPart = value
        End Set
    End Property

    Public Property Address_Pass() As Long
        Get
            Return mAddress_Pass
        End Get
        Set(ByVal value As Long)
            mAddress_Pass = value
        End Set
    End Property

    Public Property Address_ReadPreviousStamp() As Long
        Get
            Return mAddress_ReadPreviousStamp
        End Get
        Set(ByVal value As Long)
            mAddress_ReadPreviousStamp = value
        End Set
    End Property

    Public Property Address_WriteCurrentStamp() As Long
        Get
            Return mAddress_WriteCurrentStamp
        End Get
        Set(ByVal value As Long)
            mAddress_WriteCurrentStamp = value
        End Set
    End Property

    Public Property Address_WriteCurrentStamp_PASS() As Long
        Get
            Return mAddress_WriteCurrentStamp_PASS
        End Get
        Set(ByVal value As Long)
            mAddress_WriteCurrentStamp_PASS = value
        End Set
    End Property

    Public Property Address_WriteCurrentStamp_FAIL() As Long
        Get
            Return mAddress_WriteCurrentStamp_FAIL
        End Get
        Set(ByVal value As Long)
            mAddress_WriteCurrentStamp_FAIL = value
        End Set
    End Property

    Public Property Address_Return() As Long
        Get
            Return mAddress_Return
        End Get
        Set(ByVal value As Long)
            mAddress_Return = value
        End Set
    End Property

    Public Property Address_Dummy() As Long
        Get
            Return mAddress_Dummy
        End Get
        Set(ByVal value As Long)
            mAddress_Dummy = value
        End Set
    End Property

    Public Property Address_RepairSchedule() As Long
        Get
            Return mAddress_RepairSchedule
        End Get
        Set(ByVal value As Long)
            mAddress_RepairSchedule = value
        End Set
    End Property

    Public Property Address_Print() As Long
        Get
            Return mAddress_Print
        End Get
        Set(ByVal value As Long)
            mAddress_Print = value
        End Set
    End Property

    Public Property Address_Scan() As Long
        Get
            Return mAddress_Scan
        End Get
        Set(ByVal value As Long)
            mAddress_Scan = value
        End Set
    End Property

    Public Property Address_Teach() As Long
        Get
            Return mAddress_Teach
        End Get
        Set(ByVal value As Long)
            mAddress_Teach = value
        End Set
    End Property


    Public Property Address_WaitForPlc() As Long
        Get
            Return mAddress_WaitForPlc
        End Get
        Set(ByVal value As Long)
            mAddress_WaitForPlc = value
        End Set
    End Property


    Public Property IsInit() As Boolean
        Get
            Return mIsInit
        End Get
        Set(ByVal value As Boolean)
            mIsInit = value
        End Set
    End Property

    Public Property IsOn() As Boolean
        Get
            Return mIsOn
        End Get
        Set(ByVal value As Boolean)
            mIsOn = value
        End Set
    End Property

    Public Property IsStart() As Boolean
        Get
            Return mIsStart
        End Get
        Set(ByVal value As Boolean)
            mIsStart = value
        End Set
    End Property

    Public Property IsMessage() As Boolean
        Get
            Return mIsMessage
        End Get
        Set(ByVal value As Boolean)
            mIsMessage = value
        End Set
    End Property

    Public Property IsError() As Boolean
        Get
            Return mIsError
        End Get
        Set(ByVal value As Boolean)
            mIsError = value
        End Set
    End Property

    Public Property IsMasterError() As Boolean
        Get
            Return mIsMasterError
        End Get
        Set(ByVal value As Boolean)
            mIsMasterError = value
        End Set
    End Property

    Public Property IsAutomatic() As Boolean
        Get
            Return mIsAutomatic
        End Get
        Set(ByVal value As Boolean)
            mIsAutomatic = value
        End Set
    End Property

    Public Property IsManuall() As Boolean
        Get
            Return mIsManuall
        End Get
        Set(ByVal value As Boolean)
            mIsManuall = value
        End Set
    End Property

    Public Property IsReady() As Boolean
        Get
            Return mIsReady
        End Get
        Set(ByVal value As Boolean)
            mIsReady = value
        End Set
    End Property

    Public Property IsVisible() As Boolean
        Get
            Return mIsVisible
        End Get
        Set(ByVal value As Boolean)
            mIsVisible = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return mText
        End Get
        Set(ByVal value As String)
            mText = value
        End Set
    End Property

    Public Property Stack() As String
        Get
            Return mStack
        End Get
        Set(ByVal value As String)
            mStack = value
        End Set
    End Property

    Public Property StepTextLine() As String
        Get
            Return mStepTextLine
        End Get
        Set(ByVal value As String)
            mStepTextLine = value
        End Set
    End Property

#End Region

    Public Sub New(Optional ByVal _mName As String = "")
        mName = _mName
        mIdString = _mName
        mAddress_Origin = -100
        mAddress_Home = 0
    End Sub

    Public Sub Ready(ByVal i As Station)
        i.IsReady = True
    End Sub

    Public Sub Reset(ByVal i As Station)
        i.IsMessage = False
    End Sub

    Public Overloads Sub NextStep(ByVal i As Station)
        i.StepInputNumber = i.StepOutputNumber + 1
    End Sub

    Public Overloads Sub NextStep(ByVal i As Station, ByVal Address As Long)
        i.Address_Return = i.StepOutputNumber + 1
        i.StepInputNumber = Address
    End Sub
End Class
