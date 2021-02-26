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
    Protected mIsError As Boolean
    Protected mIsMasterError As Boolean

    Protected mAddress_Pass As Long
    Protected mAddress_Fail As Long

    Protected mAddress_Debug As Long
    Protected mAddress_Origin As Long
    Protected mAddress_Home As Long

    Protected mStack As String
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


    Public Property Address_Debug() As Long
        Get
            Return mAddress_Debug
        End Get
        Set(ByVal value As Long)
            mAddress_Debug = value
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


    Public Property Address_Pass() As Long
        Get
            Return mAddress_Pass
        End Get
        Set(ByVal value As Long)
            mAddress_Pass = value
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

#End Region

    Public Sub New(Optional ByVal _mName As String = "")
        mName = _mName
        mIdString = _mName
        mAddress_Origin = -100
        mAddress_Home = 0
    End Sub

    Public Overloads Sub NextStep(ByVal i As Station)
        i.StepInputNumber = i.StepOutputNumber + 1
    End Sub

    Public Overloads Sub NextStep(ByVal i As Station, ByVal Address As Long)
        i.StepInputNumber = Address
    End Sub
End Class
