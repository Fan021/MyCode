'Imports Kostal.Testman.Plugin
Public Class DEPRAG_DataProcess
    ' Inherits Kostal.Testman.Plugin.Base.DevicePluginAdapter

    Private Const KIND_CONST As String = "USAGE"
    Private Const PROGRAMME_NUM As Int32 = 16
    Private Const Url_default1 As String = "/cgi-bin/cgiread?changeprog="
    Private Const Url_default2 As String = "&site=10&add=&steps=10%2C&marked=p01&where=a&program=1&torqueunit=0&head_title=Program+2&p01_block=10&p01_num=1&p01_1=8000&p01_2=14.40&p01_3=12.96&p01_4=15.84&p01_21=111&p01_25=10.08&p01_22=44&p01_9=0&p01_5=0&p01_7=0&p01_8=0"
    Private Const Url_default3 As String = "/cgi-bin/cgiread?site=13&dlfile=/mnt/mmc/finaldata/actual.csv"
    Public m_DataProcess As New ClsDataProcess
    Public m_ControllerIP As String
    'Private _acutralProgrammeNum As Integer
    'Private _acutralScrewIndex As Integer
    'Private _acutralStepType As ClsDataProcess.StepType
    'Private _acutualValueType As ClsDataProcess.ValueType


    'Public Property ControllerIP
    '    Get
    '        Return m_ControllerIP
    '    End Get
    '    Set(value)
    '        m_ControllerIP = value
    '    End Set
    'End Property
    'Public Property ActuralStepType
    '    Set(value)
    '        _acutralStepType = value
    '    End Set
    '    Get
    '        Return _acutralStepType
    '    End Get
    'End Property

    'Public Property ActuralValueType
    '    Set(value)
    '        _acutualValueType = value
    '    End Set
    '    Get
    '        Return _acutualValueType
    '    End Get
    'End Property
    'Public Property acutralProgrammeNum
    '    Set(value)
    '        _acutralProgrammeNum = value
    '    End Set
    '    Get
    '        Return _acutralProgrammeNum
    '    End Get
    'End Property

    'Public Property acutralScrewIndex
    '    Set(value)
    '        _acutralScrewIndex = value
    '    End Set
    '    Get
    '        Return _acutralScrewIndex
    '    End Get
    'End Property

    Public Function InitController(ByVal ControllerIP As String) As Boolean
        Dim p As System.Net.NetworkInformation.Ping = New System.Net.NetworkInformation.Ping()
        Dim options As System.Net.NetworkInformation.PingOptions = New System.Net.NetworkInformation.PingOptions()
        options.DontFragment = True
        Dim reply As System.Net.NetworkInformation.PingReply = p.Send(ControllerIP, 5000)
        If reply.Status <> System.Net.NetworkInformation.IPStatus.Success Then
            Return False
        End If
        m_ControllerIP = ControllerIP
        Return True
    End Function
    Public Sub Quit()

    End Sub
    Public Function LoadActuralData() As Boolean
        Try
            If LoadAcutralInfo() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function
    ''Public Overrides Function Init(initData As String) As Integer
    ''    Return MyBase.Init(initData)
    ''End Function
    '<Base.IsPlugInCommand("Set", "ControllerIP", "set the IP address of the controller")>
    '<Base.Parameter(0, "ControllerIP", "the IP address of the controller", "10.10.25.100")>
    'Private Sub SetControllerIP(ByVal ts As Base.ITestStepPlugIn)
    '    Me.ControllerIP = ts.Parameter(0).str
    'End Sub

    '<Base.IsPlugInCommand(KIND_CONST, "LoadPara", "Load parametes from the controller")>
    'Private Sub LoadParameters(ByVal ts As Base.ITestStepPlugIn)
    '    Dim i As Int32
    '    Dim url As String

    '    Array.Resize(m_DataProcess.m_ProgrammeInfo, PROGRAMME_NUM)
    '    For i = 1 To PROGRAMME_NUM
    '        url = "http://" + Me.ControllerIP + Url_default1 + i.ToString + Url_default2
    '        If m_DataProcess.LoadParameters(url, i - 1) = -1 Then
    '            ts.Value.bool = False
    '        End If
    '    Next

    '    ts.Value.bool = True
    'End Sub
    Public Function LoadProgram(ByVal iProgramNumber As Integer) As Integer
        Dim url As String
        url = "http://" + m_ControllerIP + Url_default1 + iProgramNumber.ToString + Url_default2
        Return m_DataProcess.LoadParameters(url, iProgramNumber)
    End Function

    Public Function ChangeParameter(ByVal iProgramNumber As Integer) As Boolean
        If m_DataProcess.ChangeParameters(iProgramNumber) <> 0 Then
            Return False
        End If
        Return True
    End Function

    '<Base.IsPlugInCommand("ACTURAL", "LoadAcutalInfo", "Load the acutral informations from the file:actural.csv")>
    'Private Sub LoadAcutralInfo(ByVal ts As Base.ITestStepPlugIn)
    '    Dim url As String
    '    url = "http://" + Me.ControllerIP + Url_default2
    '    m_DataProcess.LoadActuralValues(url)

    '    ts.Value.bool = True
    'End Sub
    Public Function LoadAcutralInfo() As Boolean
        Dim url As String
        url = "http://" + m_ControllerIP + Url_default3
        m_DataProcess.LoadActuralValues(url)

        Return True
    End Function
    '<Base.IsPlugInCommand("ACTURAL", "LoadLimitInfo", "Load the limits values from the Programme Info")>
    '<Base.Parameter(0, "StepNum", "the step number for special test", 2)>
    '<Base.Parameter(1, "ValueType", "the type of the special test result", GetType(ClsDataProcess.ValueType), "Angle")>
    '<Base.Parameter(2, "LimitMode", "the mode of the Limit", GetType(ClsDataProcess.LimitMode), "Down")>
    'Private Sub LoadActuralLimitsInfo(ByVal ts As Base.ITestStepPlugIn)
    '    Select Case ts.Parameter(1).AsEnum(Of ClsDataProcess.ValueType)
    '        Case ClsDataProcess.ValueType.Angle
    '            Select Case ts.Parameter(2).AsEnum(Of ClsDataProcess.LimitMode)
    '                Case ClsDataProcess.LimitMode.Down
    '                    ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Angle_LLimit
    '                Case ClsDataProcess.LimitMode.Up
    '                    ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Angle_Ulimit
    '            End Select
    '        Case ClsDataProcess.ValueType.Torque
    '            Select Case ts.Parameter(2).AsEnum(Of ClsDataProcess.LimitMode)
    '                Case ClsDataProcess.LimitMode.Down
    '                    ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Torque_Llimit
    '                Case ClsDataProcess.LimitMode.Up
    '                    ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Torque_Ulimit
    '            End Select
    '    End Select

    'End Sub
    Private Function LoadActuralLimitsInfo(ByVal StepNum As Integer, ByVal ValueType As ClsDataProcess.ValueType, ByVal LimitMode As ClsDataProcess.LimitMode) As Double
        Select Case ValueType
            Case ClsDataProcess.ValueType.Angle
                Select Case LimitMode
                    Case ClsDataProcess.LimitMode.Down
                        Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Angle_LLimit
                    Case ClsDataProcess.LimitMode.Up
                        Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Angle_Ulimit
                End Select
            Case ClsDataProcess.ValueType.Torque
                Select Case LimitMode
                    Case ClsDataProcess.LimitMode.Down
                        Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Torque_Llimit
                    Case ClsDataProcess.LimitMode.Up
                        Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Torque_Ulimit
                End Select
        End Select
        Return 0
    End Function
    '<Base.IsPlugInCommand("ACTURAL", "LoadAcutralValue", "the acutral result")>
    '<Base.Parameter(0, "StepNum", "the step number for special test", 2)>
    '<Base.Parameter(1, "ValueType", "the type of the special test result", GetType(ClsDataProcess.ValueType), "Angle")>
    'Private Sub LoadAcutralValue(ByVal ts As Base.ITestStepPlugIn)
    '    Select Case ts.Parameter(1).AsEnum(Of ClsDataProcess.ValueType)
    '        Case ClsDataProcess.ValueType.Angle
    '            ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Angle_value
    '        Case ClsDataProcess.ValueType.Torque
    '            ts.Value.dbl = m_DataProcess.ScrewResults(0).Steplist(ts.Parameter(0).int).m_Torque_value
    '    End Select

    'End Sub
    Private Function LoadAcutralValue(ByVal StepNum As Integer, ByVal ValueType As ClsDataProcess.ValueType) As Double
        Select Case ValueType
            Case ClsDataProcess.ValueType.Angle
                Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Angle_value
            Case ClsDataProcess.ValueType.Torque
                Return m_DataProcess.ScrewResults(0).Steplist(StepNum).m_Torque_value
        End Select
        Return 0
    End Function
End Class
