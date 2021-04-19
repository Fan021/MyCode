
'Author		: Frank Dümpelmann

'Version	: 1.0.0.0 Build 2010_01_01_00

'Version	: 1.0.0.1 Build 2011_08_26_00
'	Preset variables with default values.

'TYPE StructFailedPartInfo :
'STRUCT
'		strFailKostalNr							: STRING(20);		(* Kostal number without Index, e.g. 10110201*)
'		strFailSerialNr							: STRING(50);		(* Kostal serial number. e.g. 13 digitals *)
'		strFailScheduleName						: STRING(20);		(* Schedule test name,  e.g. Normal_test*)
'		strFailTestStatus							: STRING(20);		(* Test status or mode,  e.g. Assemably, PRE, EOL*)
'		strFailCarrierNr							: STRING(20);		(* What carrier's number,  e.g. WT10*)
'		strFailStationNr							: STRING(20);		(* Station number of failed part,  e.g. ST10*)
'		strFailTestStep							: STRING(20);		(* Failed test step number , e.g. 1000.101*)
'		strFailCode								: STRING(20);		(* Error code of failed part, e.g. A0F0*)
'		strFailText								: STRING(20);		(* Description of failed step, e.g. Spring doesn't exist!*)
'		strFailValue								: STRING(20);		(* Failed Value, e.g. 13.95*)
'		strFailLowerLimit							: STRING(20);		(* Lower test limit, e.g. 12.50 *)
'		strFailUpperLimit							: STRING(20);		(* Upper test limit, e.g. 13.60 *)
'		strFailUnit								: STRING(20);		(* Value Unit, e.g. Voltage*)
'END_STRUCT
'END_TYPE

Public Class WT
	Inherits PartFailInfo
    Protected _Number As Byte = 0
    Protected _ArticleNumber As String = ""
    Protected _SerialNumber As String = ""
    Protected _Status As String = ""
    Protected _Target As String = ""
    Protected _Schedule As String = ""
    Protected _TestResult As Boolean = False
    Protected _ReferencePart As Boolean = False

#Region "Properties"

    Public Property Number() As Byte
        Get
            Return _Number
        End Get
        Set(ByVal value As Byte)
            _Number = value
        End Set
    End Property

    Public Property Schedule() As String
        Get
            Return _Schedule
        End Get
        Set(ByVal value As String)
            _Schedule = value
        End Set
    End Property

    Public Property ArticleNumber() As String
        Get
            Return _ArticleNumber
        End Get
        Set(ByVal value As String)
            _ArticleNumber = value
        End Set
    End Property

    Public Property SerialNumber() As String
        Get
            Return _SerialNumber
        End Get
        Set(ByVal value As String)
            _SerialNumber = value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    Public Property Target() As String
        Get
            Return _Target
        End Get
        Set(ByVal value As String)
            _Target = value
        End Set
    End Property

    Public Property TestResult() As Boolean
        Get
            Return _TestResult
        End Get
        Set(ByVal value As Boolean)
            _TestResult = value
        End Set
    End Property

    Public Property ReferencePart() As Boolean
        Get
            Return _ReferencePart
        End Get
        Set(ByVal value As Boolean)
            _ReferencePart = value
        End Set
    End Property

#End Region

End Class

Public Class PartFailInfo
    Protected _PartFailLocation As String = ""
    Protected _PartFailTestStep As String = ""
    Protected _PartFailCode As String = ""
    Protected _PartFailText As String = ""
    Protected _PartFailValue As String = ""
    Protected _PartFailUpperLimit As String = ""
    Protected _PartFailLowerLimit As String = ""
    Protected _PartFailUnit As String = ""
#Region "Properties"

    Public Property PartFailLocation() As String
        Get
            Return _PartFailLocation
        End Get
        Set(ByVal value As String)
            _PartFailLocation = value
        End Set
    End Property

    Public Property PartFailTestStep() As String
        Get
            Return _PartFailTestStep
        End Get
        Set(ByVal value As String)
            _PartFailTestStep = value
        End Set
    End Property

    Public Property PartFailCode() As String
        Get
            Return _PartFailCode
        End Get
        Set(ByVal value As String)
            _PartFailCode = value
        End Set
    End Property

    Public Property PartFailText() As String
        Get
            Return _PartFailText
        End Get
        Set(ByVal value As String)
            _PartFailText = value
        End Set
    End Property

    Public Property PartFailValue() As String
        Get
            Return _PartFailValue
        End Get
        Set(ByVal value As String)
            _PartFailValue = value
        End Set
    End Property

    Public Property PartFailUpperLimit() As String
        Get
            Return _PartFailUpperLimit
        End Get
        Set(ByVal value As String)
            _PartFailUpperLimit = value
        End Set
    End Property

    Public Property PartFailLowerLimit() As String
        Get
            Return _PartFailLowerLimit
        End Get
        Set(ByVal value As String)
            _PartFailLowerLimit = value
        End Set
    End Property

    Public Property PartFailUnit() As String
        Get
            Return _PartFailUnit
        End Get
        Set(ByVal value As String)
            _PartFailUnit = value
        End Set
    End Property


#End Region

End Class
