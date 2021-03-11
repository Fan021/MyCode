Imports Kostal.VisuEL.Client
Imports StdTesterIF
Imports System.Globalization

Public Class KDX_VisuEL
    Public _localStorageTarget As String = ""
    Private _testStartTimeUtc As DateTime = DateTime.UtcNow
    Public _VisuELClient As VisuElClient = Nothing
    Public _maturity As MaturityDef = MaturityDef.Production
    Public _detail As New List(Of Detail)
    Public _server As String
    Public _queueOverride As String = Nothing
    Private username As String
    Private password As String

    Private root As StdTesterRoot
    Private currentTestMachine As TestMachine
    Private productVariant As ProductVariant
    Private currentDUT As DUT
    Private currentTest As Test
    Private currentStep As TestStep
    Private ResourceId As String
    Private _ArticleNum As Integer
    Private _ArticleIndex As Integer
    Private TestName As String
    Private ProcessName As String
    Private Description As String
    Private ExcecutionNumber As Integer

    Public Sub New()
        username = "testman"
        password = "rabbit4testman"
    End Sub
    Public Sub Init(ByVal server As String, ByVal TestName As String, ByVal ProcessName As String, ByVal Description As String, ByVal ResourceId As String)
        Try
            _server = server
            Me.TestName = TestName
            Me.ProcessName = ProcessName
            Me.Description = Description
            Me.ResourceId = ResourceId
            If Me._VisuELClient Is Nothing Then
                ' If String.IsNullOrWhiteSpace(Me._queueOverride) Then
                If Me._queueOverride Is Nothing Then
                    Me._VisuELClient = New VisuElClient(Me._localStorageTarget, Me._server, username, password)
                Else
                    Me._VisuELClient = New VisuElClient(Me._localStorageTarget, Me._server, username, password, Me._queueOverride)
                End If
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub NewRoot()
        root = New StdTesterRoot
        currentTestMachine = New TestMachine
        With currentTestMachine
            .Name = TestName
            .ProcessName = ProcessName
            .Description = Description
            .ResourceId = ResourceId
        End With

        root.AddChildTestMachines(currentTestMachine)
    End Sub
    Public Sub Quit()

    End Sub

    Public Property ArticleNum
        Set(ByVal value)
            _ArticleNum = value
        End Set
        Get
            Return _ArticleNum
        End Get
    End Property

    Public Property ArticleIndex
        Set(ByVal value)
            _ArticleIndex = value
        End Set
        Get
            Return _ArticleIndex
        End Get
    End Property
    Public Sub SetArticleInfo()
        Try

            productVariant = New ProductVariant
            With productVariant
                .MaterialIndex = _ArticleIndex
                .MaterialNumber = _ArticleNum
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Public Sub SetDUTInfo(ByVal SerialNum As String)
        Try
            ExcecutionNumber = 1
            currentDUT = New DUT
            With currentDUT
                .MaterialIndex = _ArticleIndex
                .MaterialNumber = _ArticleNum
                .SerialNumber = SerialNum
            End With

            If currentDUT.SerialNumber = "" Then
                currentDUT.SerialNumber = "unknown"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub SetTestInfo(ByVal ExecutionTime As Double, ByVal io As Boolean)
        Try
            currentTest = New Test
            With currentTest
                .FinishedUtc = DateTime.UtcNow
                .UniqueRunId = currentTestMachine.ToString + "@" + _testStartTimeUtc.ToString() + "@" +
                                          currentDUT.SerialNumber
                .DUT = currentDUT
                .ParentTestMachine = currentTestMachine
                .TotalDuration_s = ExecutionTime / 1000.0
                .Pass = io
                .Maturity = _maturity
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub SaveStart()
        Try
            NewRoot()
            _testStartTimeUtc = DateTime.UtcNow
            _VisuELClient.LocalStorageTarget = _localStorageTarget
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub AddSaveData()
        Try

            currentTestMachine.AddChildProductVariants(productVariant)
            currentTestMachine.AddChildTests(currentTest)
            currentDUT.AddTests(currentTest)
            root.AddChildDUTs(currentDUT)

            Dim m_detail = New Detail

            With m_detail
                .Key = "VisuEL.Client.Version"
                .Type = DetailTypeDef.Attribute
                .Value = GetType(VisuElClient).Assembly.GetName().Version.ToString
            End With
            _detail.Add(m_detail)

            m_detail = New Detail
            With m_detail
                .Key = "DEPRAG_VisuEL.DEPRAG_DataSave.Version"
                .Type = DetailTypeDef.Attribute
                .Value = GetType(KDX_VisuEL).Assembly.GetName().Version.ToString
            End With
            _detail.Add(m_detail)

            If _detail.Any Then
                For Each d As Detail In _detail
                    currentTest.AddChildDetails(d)
                Next
            End If
            _detail.Clear()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AddTeststep(ByVal TestStepNumber As String, ByVal StepName As String, ByVal Unit As String, ByVal LowLimit As Double, ByVal UpLimit As Double, ByVal value As Double, ByVal io As Boolean, ByVal StepDuration As Double, ByVal RetryCounter As Integer, ByVal FailureText As String)
        Dim tempstr() As String
        Dim parentVariant As ProductVariant

        Try
            currentTest = currentTestMachine.ChildTests.First
            tempstr = TestStepNumber.Split(".")

            'If Not RetryCounter > 0 Then
            currentStep = New TestStep

            With currentStep
                .Name = StepName
                .MajorId = Int32.Parse(tempstr(0), NumberStyles.Integer, CultureInfo.InvariantCulture)
                .MinorId = Int32.Parse(tempstr(1), NumberStyles.Integer, CultureInfo.InvariantCulture)
                .ValidFromUtc = DateTime.UtcNow
            End With

            If currentStep.MajorId <> 0 Or currentStep.MajorId <> 0 Then
                currentStep.QRelevances.Add(QualityLevelDef.LQV)
            End If
            If (Not currentStep.QRelevances.Any()) Then
                currentStep.QRelevances.Add(QualityLevelDef.None)
            End If
            ' Else
            '  currentStep = currentTestMachine.GetChildProductVariantsById(_ArticleIndex, _ArticleNum).GetChildTestStepsById(Int32.Parse(tempstr(0), NumberStyles.Integer, CultureInfo.InvariantCulture), Int32.Parse(tempstr(1), NumberStyles.Integer, CultureInfo.InvariantCulture), "0")
            '  End If


            currentStep.CompareMode = CompareModeDef.InLimit

            currentStep.Unit = Unit

            currentStep.LowerLimitNum = LowLimit
            currentStep.UpperLimitNum = UpLimit

            currentStep.DataType = DataTypeDef.Dbl
            Dim item As New DblResult
            With item
                .Pass = io
                .StepDuration_s = StepDuration / 1000.0
                .Value = value
                .RetryCounter = RetryCounter
                .FailureText = FailureText
                .TestStep = currentStep
                .SeqNr = ExcecutionNumber
            End With
            currentTest.AddChildDblResults(item)

            ExcecutionNumber += 1

            parentVariant = currentTestMachine.GetChildProductVariantsById(_ArticleIndex, _ArticleNum)

            If parentVariant Is Nothing Then
                currentTestMachine.AddChildProductVariants(productVariant)
                parentVariant = currentTestMachine.GetChildProductVariantsById(_ArticleIndex, _ArticleNum)
            End If

            If parentVariant.GetChildTestStepsById(Int32.Parse(tempstr(0), NumberStyles.Integer, CultureInfo.InvariantCulture), Int32.Parse(tempstr(1), NumberStyles.Integer, CultureInfo.InvariantCulture), "0") IsNot Nothing Then
                parentVariant.RemoveChildTestStepsById(Int32.Parse(tempstr(0), NumberStyles.Integer, CultureInfo.InvariantCulture), Int32.Parse(tempstr(1), NumberStyles.Integer, CultureInfo.InvariantCulture), "0")
            End If

            parentVariant.AddChildTestSteps(currentStep)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Sub DataSave()

        Try
            'FileSystem.ChDir("C:\Program Files\VET")
            'MsgBox(CurDir)
            Dim m_serializer As New StdTesterIFSerializer
            Dim m_Parser As New StdTesterIFParser
            Dim m_Errors As New List(Of String)
            'm_serializer.SaveToXml(root, "D:\KDX.xml", False)

            'root = m_Parser.LoadFromXml("D:\KDX.xml", m_Errors, False)
            'If m_Errors.Count = 0 Then
            _VisuELClient.Store(root)
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


End Class

